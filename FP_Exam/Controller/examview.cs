using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class examview : LoginController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int sortid;

		protected SortInfo sortinfo = new SortInfo();

		protected ExamInfo examinfo = new ExamInfo();

		protected ExamConfig examconfig = new ExamConfig();

		protected int resultid = 0;

		protected override void Controller()
		{
			base.Response.Expires = 0;
			base.Response.CacheControl = "no-cache";
			base.Response.Cache.SetNoStore();
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			bool flag = this.channelinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
				bool flag2 = this.examinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该考试不存在或已被删除。");
				}
				else
				{
					bool flag3 = this.examinfo.status == 0;
					if (flag3)
					{
						bool flag4 = !this.isperm && this.examinfo.uid != this.userid;
						if (flag4)
						{
							this.ShowErr("对不起，该考试已关闭。");
							return;
						}
					}
					this.examconfig = ExamConifgs.GetExamConfig();
					this.sortid = this.examinfo.sortid;
					this.sortinfo = SortBll.GetSortInfo(this.sortid);
					bool flag5 = this.sortinfo.id == 0;
					if (flag5)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("sortid", this.sortid);
						DbHelper.ExecuteDelete<ExamInfo>(new SqlParam[]
						{
							sqlParam
						});
						this.ShowErr("对不起，考试栏目不存在或已被删除。");
					}
					else
					{
						bool ispost = this.ispost;
						if (ispost)
						{
							bool flag6 = this.isfree == 1;
							if (flag6)
							{
								SqlParam sqlParam2 = DbHelper.MakeAndWhere("examid", this.examid);
								int num = DbHelper.ExecuteCount<ExamResult>(new SqlParam[]
								{
									sqlParam2
								});
								bool flag7 = num > 30;
								if (flag7)
								{
									this.ShowErr("对不起，免费版考试人数不能超过30人。");
									return;
								}
							}
							bool flag8 = this.examinfo.examroles != "";
							if (flag8)
							{
								bool flag9 = !FPArray.Contain(this.examinfo.examroles, this.roleid) && !this.isperm;
								if (flag9)
								{
									this.ShowErr("对不起，您所在的角色不允许参加本场考试。");
									return;
								}
							}
							bool flag10 = this.examinfo.examdeparts != "";
							if (flag10)
							{
								bool flag11 = !FPArray.Contain(this.examinfo.examdeparts, this.user.departid) && !this.isperm;
								if (flag11)
								{
									this.ShowErr("对不起，您所在的部门不允许参加本场考试。");
									return;
								}
							}
							bool flag12 = this.examinfo.examuser != "";
							if (flag12)
							{
								bool flag13 = !FPArray.Contain(this.examinfo.examuser, this.userid) && !this.isperm;
								if (flag13)
								{
									this.ShowErr("对不起，您不允许参加本场考试。");
									return;
								}
							}
							bool flag14 = this.examinfo.islimit == 1;
							if (flag14)
							{
								bool flag15 = this.examinfo.starttime > DateTime.Now;
								if (flag15)
								{
									this.ShowErr("对不起，本场考试尚未到开考时间。");
									return;
								}
								bool flag16 = this.examinfo.endtime < DateTime.Now;
								if (flag16)
								{
									this.ShowErr("对不起，本场考试已超过了考试期限。");
									return;
								}
							}
							SqlParam[] sqlparams = new SqlParam[]
							{
								DbHelper.MakeAndWhere("examid", this.examid),
								DbHelper.MakeAndWhere("uid", this.userid),
								DbHelper.MakeAndWhere("status", 0)
							};
							ExamResult examResult = DbHelper.ExecuteModel<ExamResult>(sqlparams);
							bool flag17 = examResult.id > 0;
							if (flag17)
							{
								this.resultid = examResult.id;
								bool flag18 = this.examinfo.papertype == 1;
								if (flag18)
								{
									base.Response.Redirect("exam_single.aspx?resultid=" + examResult.id);
								}
								else
								{
									base.Response.Redirect("exam_multi.aspx?resultid=" + examResult.id);
								}
							}
							bool flag19 = this.examinfo.repeats > 0;
							if (flag19)
							{
								SqlParam[] sqlparams2 = new SqlParam[]
								{
									DbHelper.MakeAndWhere("examid", this.examid),
									DbHelper.MakeAndWhere("uid", this.userid),
									DbHelper.MakeAndWhere("status", WhereType.GreaterThanEqual, 1)
								};
								int num2 = DbHelper.ExecuteCount<ExamResult>(sqlparams2);
								bool flag20 = num2 >= this.examinfo.repeats;
								if (flag20)
								{
									this.ShowErr("对不起，本场考试限制次数为" + this.examinfo.repeats + "次，您已考完不能再考。");
									return;
								}
							}
							bool flag21 = this.examinfo.credits > 0 && !this.isperm && this.examinfo.uid != this.userid;
							if (flag21)
							{
								bool flag22 = this.user.credits < this.examinfo.credits;
								if (flag22)
								{
									this.ShowErr("对不起，您的积分余额不足，不能参加本场考试。");
									return;
								}
								UserBll.UpdateUserCredit(this.userid, "参加考试", 0, this.examinfo.credits * -1);
							}
							Random random = new Random();
							int paper = random.Next(this.examinfo.papers) + 1;
							List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(this.examid, paper);
							int num3 = 0;
							for (int i = 0; i < examTopicList.Count; i++)
							{
								examTopicList[i].questionlist = QuestionBll.GetTopicQuestion(this.channelinfo.id, examTopicList[i]);
								string[] array = FPArray.SplitString(examTopicList[i].questionlist);
								bool flag23 = this.examinfo.display == 0;
								if (flag23)
								{
									examTopicList[i].questionlist = QuestionBll.GetRandom(array, array.Length);
								}
								num3 += array.Length;
								examTopicList[i].questions = array.Length;
							}
							ExamResult examResult2 = new ExamResult();
							examResult2.uid = this.userid;
							examResult2.examid = this.examid;
							examResult2.channelid = this.examinfo.channelid;
							examResult2.sortid = this.examinfo.sortid;
							examResult2.examtype = this.examinfo.examtype;
							examResult2.showanswer = this.examinfo.showanswer;
							examResult2.allowdelete = this.examinfo.allowdelete;
							examResult2.examname = this.examinfo.name;
							examResult2.title = this.examinfo.title;
							examResult2.address = this.examinfo.address;
							examResult2.examtime = this.examinfo.examtime;
							examResult2.total = this.examinfo.total;
							examResult2.passmark = this.examinfo.passmark;
							examResult2.credits = this.examinfo.credits;
							examResult2.questions = num3;
							examResult2.islimit = this.examinfo.islimit;
							bool flag24 = examResult2.islimit == 1;
							if (flag24)
							{
								examResult2.starttime = this.examinfo.starttime;
								examResult2.endtime = this.examinfo.endtime;
							}
							else
							{
								examResult2.starttime = DbUtils.GetDateTime();
								examResult2.endtime = examResult2.starttime.AddMinutes((double)this.examinfo.examtime);
							}
							examResult2.examdatetime = DbUtils.GetDateTime();
							examResult2.status = 0;
							examResult2.paper = paper;
							examResult2.ip = this.ip;
							examResult2.mac = ExamConifgs.GetMacAddress(this.ip);
							examResult2.attachid = FPRandom.CreateCode(30);
							examResult2.id = DbHelper.ExecuteInsert<ExamResult>(examResult2);
							bool flag25 = examResult2.id > 0;
							if (flag25)
							{
								foreach (ExamTopic current in examTopicList)
								{
									DbHelper.ExecuteInsert<ExamResultTopic>(new ExamResultTopic
									{
										resultid = examResult2.id,
										type = current.type,
										title = current.title,
										perscore = current.perscore,
										display = current.display,
										questions = current.questions,
										questionlist = current.questionlist
									});
								}
								bool flag26 = this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "";
								if (flag26)
								{
									string text = string.Format("UPDATE [{0}Exam_ExamInfo] SET [exams]=[exams]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
									DbHelper.ExecuteSql(text.ToString());
								}
							}
							this.resultid = examResult2.id;
							bool flag27 = this.examinfo.papertype == 1;
							if (flag27)
							{
								base.Response.Redirect("exam_single.aspx?resultid=" + examResult2.id);
							}
							else
							{
								base.Response.Redirect("exam_multi.aspx?resultid=" + examResult2.id);
							}
						}
					}
				}
			}
		}

		protected string OptionInt(int ascount, int optiondisplay)
		{
			string text = "";
			for (int i = 0; i < ascount; i++)
			{
				bool flag = text != "";
				if (flag)
				{
					text += ",";
				}
				text += i.ToString();
			}
			bool flag2 = optiondisplay == 0;
			if (flag2)
			{
				string[] array = FPArray.SplitString(text);
				text = QuestionBll.GetRandom(array, array.Length);
			}
			return text;
		}
	}
}
