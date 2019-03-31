using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.WapController
{
	public class createexam : LoginController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected ExamInfo examinfo = new ExamInfo();

		protected ExamConfig examconfig = new ExamConfig();

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				this.channelinfo = ChannelBll.GetChannelInfo("question");
				bool flag = this.channelinfo.id == 0;
				if (flag)
				{
					base.WriteErr("对不起，题库不存在或已被删除。");
				}
				else
				{
					this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
					bool flag2 = this.examinfo.id == 0;
					if (flag2)
					{
						base.WriteErr("对不起，该考试不存在或已被删除。");
					}
					else
					{
						bool flag3 = this.examinfo.status == 0;
						if (flag3)
						{
							base.WriteErr("对不起，该考试已关闭。");
						}
						else
						{
							bool flag4 = this.examconfig.teststatus == 0 && this.examinfo.type == 1;
							if (flag4)
							{
								base.WriteErr("对不起，考试系统已关闭了用户练习。");
							}
							else
							{
								bool flag5 = this.examinfo.examroles != "";
								if (flag5)
								{
									bool flag6 = !FPArray.Contain(this.examinfo.examroles, this.roleid) && !this.isperm;
									if (flag6)
									{
										base.WriteErr("对不起，您所在的角色不允许参加本场考试。");
										return;
									}
								}
								bool flag7 = this.examinfo.examdeparts != "";
								if (flag7)
								{
									bool flag8 = !FPArray.Contain(this.examinfo.examdeparts, this.user.departid) && !this.isperm;
									if (flag8)
									{
										base.WriteErr("对不起，您所在的部门不允许参加本场考试。");
										return;
									}
								}
								bool flag9 = this.examinfo.examuser != "";
								if (flag9)
								{
									bool flag10 = !FPArray.Contain(this.examinfo.examuser, this.userid) && !this.isperm;
									if (flag10)
									{
										base.WriteErr("对不起，您不允许参加本场考试。");
										return;
									}
								}
								bool flag11 = this.examinfo.islimit == 1;
								if (flag11)
								{
									bool flag12 = this.examinfo.starttime > DateTime.Now;
									if (flag12)
									{
										base.WriteErr("对不起，本场考试尚未到开考时间。");
										return;
									}
									bool flag13 = this.examinfo.endtime < DateTime.Now;
									if (flag13)
									{
										base.WriteErr("对不起，本场考试已超过了考试期限。");
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
								bool flag14 = examResult.id > 0;
								if (flag14)
								{
									var obj = new
									{
										errcode = 0,
										errmsg = "",
										resultid = examResult.id
									};
									FPResponse.WriteJson(obj);
								}
								bool flag15 = this.examinfo.repeats > 0;
								if (flag15)
								{
									SqlParam[] sqlparams2 = new SqlParam[]
									{
										DbHelper.MakeAndWhere("examid", this.examid),
										DbHelper.MakeAndWhere("uid", this.userid),
										DbHelper.MakeAndWhere("status", WhereType.GreaterThanEqual, 1)
									};
									int num = DbHelper.ExecuteCount<ExamResult>(sqlparams2);
									bool flag16 = num >= this.examinfo.repeats;
									if (flag16)
									{
										base.WriteErr("对不起，本场考试限制次数为" + this.examinfo.repeats + "次，您已考完不能再考。");
										return;
									}
								}
								Random random = new Random();
								int paper = random.Next(this.examinfo.papers) + 1;
								List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(this.examid, paper);
								int num2 = 0;
								for (int i = 0; i < examTopicList.Count; i++)
								{
									examTopicList[i].questionlist = QuestionBll.GetTopicQuestion(this.channelinfo.id, examTopicList[i]);
									string[] array = FPArray.SplitString(examTopicList[i].questionlist);
									bool flag17 = this.examinfo.display == 0;
									if (flag17)
									{
										examTopicList[i].questionlist = QuestionBll.GetRandom(array, array.Length);
									}
									num2 += array.Length;
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
								examResult2.examtime = this.examinfo.examtime;
								examResult2.total = this.examinfo.total;
								examResult2.passmark = this.examinfo.passmark;
								examResult2.credits = this.examinfo.credits;
								examResult2.questions = num2;
								examResult2.islimit = this.examinfo.islimit;
								bool flag18 = examResult2.islimit == 1;
								if (flag18)
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
								bool flag19 = examResult2.id > 0;
								if (flag19)
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
									bool flag20 = this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "";
									if (flag20)
									{
										string text = string.Format("UPDATE [{0}Exam_ExamInfo] SET [exams]=[exams]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
										DbHelper.ExecuteSql(text.ToString());
									}
								}
								var obj2 = new
								{
									errcode = 0,
									errmsg = "",
									resultid = examResult2.id
								};
								FPResponse.WriteJson(obj2);
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
