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
	public class exam : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected int sortid;

		protected ExamResult examresult = new ExamResult();

		protected SortInfo sortinfo = new SortInfo();

		protected List<ExamResultTopic> examtopiclist = new List<ExamResultTopic>();

		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H"
		};

		protected ExamConfig examconfig = new ExamConfig();

		protected string thetime = "00:00:00";

		protected ExamInfo examinfo = new ExamInfo();

		protected override void Controller()
		{
			base.Response.Expires = 0;
			base.Response.CacheControl = "no-cache";
			base.Response.Cache.SetNoStore();
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examresult = ExamBll.GetExamResult(this.resultid);
			bool flag = this.examresult.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
			else
			{
				bool flag2 = this.examresult.uid != this.userid;
				if (flag2)
				{
					this.ShowErr("对不起，您不是本次考试的主人。");
				}
				else
				{
					bool flag3 = this.examresult.status == 1;
					if (flag3)
					{
						this.ShowErr("对不起，该考试已完成。");
					}
					else
					{
						this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examresult.examid);
						bool flag4 = this.pagename == "exam_multi.aspx" && this.examinfo.papertype == 1;
						if (flag4)
						{
							base.Response.Redirect("exam_single.aspx?resultid=" + this.examresult.id);
						}
						else
						{
							bool flag5 = this.pagename == "exam_single.aspx" && this.examinfo.papertype == 0;
							if (flag5)
							{
								base.Response.Redirect("exam_multi.aspx?resultid=" + this.examresult.id);
							}
						}
						bool flag6 = this.examinfo.status == 0;
						if (flag6)
						{
							bool flag7 = !this.isperm && this.examinfo.uid != this.userid;
							if (flag7)
							{
								this.ShowErr("对不起，该考试已关闭。");
								return;
							}
						}
						bool flag8 = this.examinfo.client["pc"] != "1";
						if (flag8)
						{
							this.ShowErr("对不起，该考试不能在电脑版上考试，请使用手机版打开。");
						}
						else
						{
							bool flag9 = this.examconfig.examstatus == 0;
							if (flag9)
							{
								this.ShowErr("对不起，系统考试尚未开放。");
							}
							else
							{
								bool flag10 = this.examconfig.examstatus > 1;
								if (flag10)
								{
									bool flag11 = this.examinfo.channelid == 2 && this.examconfig.examstatus != 2;
									if (flag11)
									{
										this.ShowErr("对不起，系统模拟考试已关闭。");
										return;
									}
									bool flag12 = this.examinfo.channelid == 4 && this.examconfig.examstatus != 3;
									if (flag12)
									{
										this.ShowErr("对不起，系统正式考试已关闭。");
										return;
									}
								}
								bool flag13 = this.examresult.examtype == 1;
								if (flag13)
								{
									string macAddress = ExamConifgs.GetMacAddress(this.ip);
									bool flag14 = this.examresult.ip != "";
									if (flag14)
									{
										bool flag15 = this.examresult.ip != this.ip || this.examresult.mac != macAddress;
										if (flag15)
										{
											this.ShowErr("对不起，请在固定位置上进行考试。");
											return;
										}
									}
									else
									{
										SqlParam[] sqlparams = new SqlParam[]
										{
											DbHelper.MakeUpdate("ip", this.ip),
											DbHelper.MakeUpdate("mac", macAddress),
											DbHelper.MakeAndWhere("id", this.resultid)
										};
										DbHelper.ExecuteUpdate<ExamResult>(sqlparams);
									}
								}
								this.sortid = this.examresult.sortid;
								this.sortinfo = SortBll.GetSortInfo(this.sortid);
								this.examresult.passmark = this.examresult.passmark * this.examresult.total / 100.0;
								this.examtopiclist = ExamBll.GetExamResultTopicList(this.resultid);
								int num = this.examresult.examtime * 60 - this.examresult.utime;
								int num2 = num / 3600;
								int num3 = (num - num2 * 3600) / 60;
								int num4 = (num - num2 * 3600 - num3 * 60) % 60;
								this.thetime = string.Concat(new string[]
								{
									num2.ToString("00"),
									":",
									num3.ToString("00"),
									":",
									num4.ToString("00")
								});
							}
						}
					}
				}
			}
		}

		protected List<ExamQuestion> GetQuestionList(ExamResultTopic resultinfo)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, resultinfo.questionlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPArray.SplitInt(resultinfo.questionlist);
			string[] array2 = FPArray.SplitString(resultinfo.answerlist, "§", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int num = 0;
			int[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = array3[i];
				foreach (ExamQuestion current in list)
				{
					bool flag = current.id == num2;
					if (flag)
					{
						current.useranswer = array2[num];
						list2.Add(current);
					}
				}
				num++;
			}
			return list2;
		}

		protected string FmAnswer(string content, string uanswer, int topicid, int qid, int en)
		{
			string[] array = FPArray.SplitString(content, new string[]
			{
				"(#answer)",
				"___",
				"____",
				"_____",
				"______"
			});
			string[] array2 = FPArray.SplitString(uanswer, ",", array.Length);
			content = "";
			int num = 0;
			string[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				string str = array3[i];
				bool flag = num < array.Length - 1;
				if (flag)
				{
					content = content + str + string.Format("<input type=\"text\" id=\"answer_{0}\" name=\"answer_{1}\" value=\"{2}\" class=\"tkt\"/>", topicid.ToString() + "_" + en.ToString(), qid, array2[num]);
				}
				else
				{
					content += str;
				}
				num++;
			}
			return content;
		}

		protected string Option(string[] opstr, int ascount)
		{
			string[] array = FPArray.SplitString("A,B,C,D,E,F,G,H");
			string text = "";
			bool flag = ascount > opstr.Length;
			if (flag)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					array[i],
					".",
					opstr[i],
					"<br/>"
				});
			}
			return text;
		}

		protected string GetPaper(int paper)
		{
			string result;
			switch (paper)
			{
			case 1:
				result = "A卷";
				break;
			case 2:
				result = "B卷";
				break;
			case 3:
				result = "C卷";
				break;
			case 4:
				result = "D卷";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}
	}
}
