using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Web;

namespace FP_Exam.Controller
{
	public class examanswer : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected List<ExamResultTopic> examtopicresultlist = new List<ExamResultTopic>();

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

		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();

		protected ExamConfig examconfig = new ExamConfig();

		protected List<string> videoimg = new List<string>();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
			bool flag = this.examresult.id == 0;
			if (flag)
			{
				this.ShowErr("该考生的试卷不存在或已被删除。");
			}
			else
			{
				bool flag2 = this.examconfig.examstatus == 0 || this.examconfig.teststatus == 0;
				if (flag2)
				{
					this.ShowErr("对不起，系统已关闭了用户练习，不能再查看答案。");
				}
				else
				{
					bool flag3 = this.examconfig.examstatus > 1;
					if (flag3)
					{
						bool flag4 = this.examresult.channelid == 2 && this.examconfig.examstatus != 2;
						if (flag4)
						{
							this.ShowErr("对不起，系统模拟考试已关闭，不能查看答案。");
							return;
						}
						bool flag5 = this.examresult.channelid == 4 && this.examconfig.examstatus != 3;
						if (flag5)
						{
							this.ShowErr("对不起，系统正式考试已关闭，不能查看答案。");
							return;
						}
					}
					bool flag6 = this.examresult.status == 0;
					if (flag6)
					{
						this.ShowErr("对不起，该考试尚未完成，不能查看答案。");
					}
					else
					{
						ChannelInfo channelInfo = ChannelBll.GetChannelInfo("question");
						bool flag7 = this.examresult.attachid != "";
						if (flag7)
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("description", "考试上传视频图片");
							List<AttachInfo> attachList = AttachBll.GetAttachList(this.examresult.attachid, new SqlParam[]
							{
								sqlParam
							});
							bool flag8 = attachList.Count > 0;
							if (flag8)
							{
								this.videoimg.Add(attachList[0].filename);
							}
							bool flag9 = attachList.Count >= 2;
							if (flag9)
							{
								this.videoimg.Add(attachList[attachList.Count / 2].filename);
							}
							bool flag10 = attachList.Count >= 3;
							if (flag10)
							{
								this.videoimg.Add(attachList[attachList.Count - 1].filename);
							}
						}
						this.examtopicresultlist = ExamBll.GetExamResultTopicList(this.resultid);
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
			string[] array3 = FPArray.SplitString(resultinfo.scorelist, "|", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("qid", WhereType.In, resultinfo.questionlist),
				DbHelper.MakeAndWhere("uid", this.userid)
			};
			List<ExamNote> list3 = DbHelper.ExecuteList<ExamNote>(sqlparams);
			int num = 0;
			int[] array4 = array;
			for (int i = 0; i < array4.Length; i++)
			{
				int num2 = array4[i];
				foreach (ExamQuestion current in list)
				{
					bool flag = current.id == num2;
					if (flag)
					{
						current.useranswer = array2[num];
						current.userscore = (double)FPUtils.StrToFloat(array3[num]);
						foreach (ExamNote current2 in list3)
						{
							bool flag2 = current2.qid == current.id;
							if (flag2)
							{
								current.note = current2.note;
							}
						}
						bool flag3 = this.examloglist.ContainsKey(current.sortid);
						if (flag3)
						{
							ExamLogInfo examLogInfo = this.examloglist[current.sortid];
							bool flag4 = FPArray.InArray(current.id, examLogInfo.favlist) >= 0;
							if (flag4)
							{
								current.isfav = 1;
							}
						}
						list2.Add(current);
					}
				}
				num++;
			}
			return list2;
		}

		protected string FmAnswer(string content, int tid, string uanswer)
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
					content = content + str + string.Format("<input type=\"text\" id=\"answer_{0}\" name=\"answer_{0}\" value=\"{1}\" class=\"tkt\"/>", tid, array2[num]);
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

		protected string GetTime(int utime)
		{
			int num = utime / 3600;
			int num2 = utime % 3600 / 60;
			int num3 = utime % 60;
			bool flag = num < 10;
			string text;
			if (flag)
			{
				text = "0" + num + ":";
			}
			else
			{
				text = num.ToString() + ":";
			}
			bool flag2 = num2 < 10;
			if (flag2)
			{
				text = text + "0" + num2.ToString() + ":";
			}
			else
			{
				text = text + num2.ToString() + ":";
			}
			bool flag3 = num3 < 10;
			if (flag3)
			{
				text = text + "0" + num3.ToString();
			}
			else
			{
				text += num3.ToString();
			}
			return text;
		}

		protected string CalRate(double myscore, double total)
		{
			return (myscore / total * 100.0).ToString("0.0");
		}

		protected string HtmlEncode(string content)
		{
			return HttpUtility.HtmlEncode(content);
		}
	}
}
