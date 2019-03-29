using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x0200002C RID: 44
	public class examview : LoginController
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0001373C File Offset: 0x0001193C
		protected override void View()
		{
			base.Response.Expires = 0;
			base.Response.CacheControl = "no-cache";
			base.Response.Cache.SetNoStore();
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
				if (this.examinfo.id == 0)
				{
					this.ShowErr("对不起，该考试不存在或已被删除。");
				}
				else
				{
					if (this.examinfo.status == 0)
					{
						if (!this.isperm && this.examinfo.uid != this.userid)
						{
							this.ShowErr("对不起，该考试已关闭。");
							return;
						}
					}
					this.sortid = this.examinfo.sortid;
					this.sortinfo = SortBll.GetSortInfo(this.sortid);
					if (this.sortinfo.id == 0)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("sortid", this.sortid);
						DbHelper.ExecuteDelete<ExamInfo>(new SqlParam[]
						{
							sqlParam
						});
						this.ShowErr("对不起，考试栏目不存在或已被删除。");
					}
					else if (this.ispost)
					{
						if (this.examinfo.examroles != "")
						{
							if (!base.ischecked(this.roleid, this.examinfo.examroles) && !this.isperm)
							{
								this.ShowErr("对不起，您所在的角色不允许参加本场考试。");
								return;
							}
						}
						if (this.examinfo.examdeparts != "")
						{
							if (!base.ischecked(this.user.departid, this.examinfo.examdeparts) && !this.isperm)
							{
								this.ShowErr("对不起，您所在的部门不允许参加本场考试。");
								return;
							}
						}
						if (this.examinfo.examuser != "")
						{
							if (!base.ischecked(this.userid, this.examinfo.examuser) && !this.isperm)
							{
								this.ShowErr("对不起，您不允许参加本场考试。");
								return;
							}
						}
						if (this.examinfo.islimit == 1)
						{
							if (this.examinfo.starttime > DateTime.Now)
							{
								this.ShowErr("对不起，本场考试尚未到考试时间。");
								return;
							}
							if (this.examinfo.endtime < DateTime.Now)
							{
								this.ShowErr("对不起，本场考试已超过考试期限。");
								return;
							}
						}
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeAndWhere("examid", this.examid),
							DbHelper.MakeAndWhere("uid", this.userid),
							DbHelper.MakeAndWhere("status", 0)
						};
						int num = DbHelper.ExecuteCount<ExamResult>(sqlparams);
						if (num > 0)
						{
							this.ShowErr("对不起，本场考试您已经考过但尚未完成，请到考试历史那里查找。");
						}
						else
						{
							if (this.examinfo.repeats > 0)
							{
								SqlParam[] sqlparams2 = new SqlParam[]
								{
									DbHelper.MakeAndWhere("examid", this.examid),
									DbHelper.MakeAndWhere("uid", this.userid),
									DbHelper.MakeAndWhere("status", WhereType.GreaterThanEqual, 1)
								};
								num = DbHelper.ExecuteCount<ExamResult>(sqlparams2);
								if (num >= this.examinfo.repeats)
								{
									this.ShowErr("对不起，本场考试限制次数为" + this.examinfo.repeats + "次，您已考完不能再考。");
									return;
								}
							}
							if (this.examinfo.credits > 0 && !this.isperm && this.examinfo.uid != this.userid)
							{
								if (this.user.credits < this.examinfo.credits)
								{
									this.ShowErr("对不起，您的积分余额不足，不能参加本场考试。");
									return;
								}
								UserBll.UpdateUserCredit(this.userid, "参加考试", 0, this.examinfo.credits * -1);
							}
							Random random = new Random();
							int paper = random.Next(this.examinfo.papers) + 1;
							List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(this.examid, paper);
							int num2 = 0;
							for (int i = 0; i < examTopicList.Count; i++)
							{
								examTopicList[i].questionlist = QuestionBll.GetTopicQuestion(this.channelinfo.id, examTopicList[i]);
								string[] array = FPUtils.SplitString(examTopicList[i].questionlist);
								if (this.examinfo.display == 0)
								{
									examTopicList[i].questionlist = QuestionBll.GetRandom(array, array.Length);
								}
								examTopicList[i].optionlist = "";
								foreach (ExamQuestion examQuestion in QuestionBll.GetQuestionList(examTopicList[i].questionlist))
								{
									if (examTopicList[i].optionlist != "")
									{
										ExamTopic examTopic = examTopicList[i];
										examTopic.optionlist += "|";
									}
									if (examQuestion.type == 1 || examQuestion.type == 2)
									{
										ExamTopic examTopic2 = examTopicList[i];
										examTopic2.optionlist += this.OptionInt(examQuestion.ascount, this.examinfo.optiondisplay);
									}
									else
									{
										ExamTopic examTopic3 = examTopicList[i];
										examTopic3.optionlist += "*";
									}
								}
								num2 += array.Length;
								examTopicList[i].questions = array.Length;
							}
							ExamResult examResult = new ExamResult();
							examResult.uid = this.userid;
							examResult.examid = this.examid;
							examResult.channelid = this.channelinfo.id;
							examResult.sortid = this.examinfo.sortid;
							examResult.examtype = this.examinfo.examtype;
							examResult.showanswer = this.examinfo.showanswer;
							examResult.allowdelete = this.examinfo.allowdelete;
							examResult.examname = this.examinfo.name;
							examResult.examtime = this.examinfo.examtime;
							examResult.total = this.examinfo.total;
							examResult.passmark = this.examinfo.passmark;
							examResult.credits = this.examinfo.credits;
							examResult.questions = num2;
							examResult.islimit = this.examinfo.islimit;
							if (examResult.islimit == 1)
							{
								examResult.starttime = this.examinfo.starttime;
								examResult.endtime = this.examinfo.endtime;
							}
							else
							{
								examResult.starttime = DbUtils.GetDateTime();
								examResult.endtime = examResult.starttime.AddMinutes((double)this.examinfo.examtime);
							}
							examResult.examdatetime = DbUtils.GetDateTime();
							examResult.status = 0;
							examResult.paper = paper;
							examResult.ip = this.ip;
							examResult.mac = ExamConifgs.GetMacAddress(this.ip);
							examResult.id = DbHelper.ExecuteInsert<ExamResult>(examResult);
							if (examResult.id > 0)
							{
								foreach (ExamTopic examTopic4 in examTopicList)
								{
									DbHelper.ExecuteInsert<ExamResultTopic>(new ExamResultTopic
									{
										resultid = examResult.id,
										type = examTopic4.type,
										title = examTopic4.title,
										perscore = examTopic4.perscore,
										display = examTopic4.display,
										questions = examTopic4.questions,
										questionlist = examTopic4.questionlist,
										optionlist = examTopic4.optionlist
									});
								}
								if (this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "")
								{
									string text = string.Format("UPDATE [{0}Exam_ExamInfo] SET [exams]=[exams]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
									DbHelper.ExecuteSql(text.ToString());
								}
							}
							base.Response.Redirect("exam.aspx?resultid=" + examResult.id);
						}
					}
				}
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00014154 File Offset: 0x00012354
		protected string OptionInt(int ascount, int optiondisplay)
		{
			string text = "";
			for (int i = 0; i < ascount; i++)
			{
				if (text != "")
				{
					text += ",";
				}
				text += i.ToString();
			}
			if (optiondisplay == 0)
			{
				string[] array = FPUtils.SplitString(text);
				text = QuestionBll.GetRandom(array, array.Length);
			}
			return text;
		}

		// Token: 0x040000E6 RID: 230
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x040000E7 RID: 231
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000E8 RID: 232
		protected int sortid;

		// Token: 0x040000E9 RID: 233
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x040000EA RID: 234
		protected ExamInfo examinfo = new ExamInfo();
	}
}
