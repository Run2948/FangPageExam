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
	public class examrepeat : LoginController
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

		protected double maxscore = 0.0;

		protected double avgscore = 0.0;

		protected int testers = 0;

		protected int display = 0;

		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();

		protected List<string> videoimg = new List<string>();

		protected override void Controller()
		{
			bool flag = !this.isperm;
			if (flag)
			{
				this.ShowErr("对不起，您没有权限阅卷。");
			}
			else
			{
				this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
				bool flag2 = this.examresult.id == 0;
				if (flag2)
				{
					this.ShowErr("该考生的试卷不存在或已被删除。");
				}
				else
				{
					bool flag3 = this.examresult.attachid != "";
					if (flag3)
					{
						List<AttachInfo> attachList = AttachBll.GetAttachList(this.examresult.attachid);
						bool flag4 = attachList.Count > 0;
						if (flag4)
						{
							this.videoimg.Add(attachList[0].filename);
						}
						bool flag5 = attachList.Count >= 2;
						if (flag5)
						{
							this.videoimg.Add(attachList[attachList.Count / 2].filename);
						}
						bool flag6 = attachList.Count >= 3;
						if (flag6)
						{
							this.videoimg.Add(attachList[attachList.Count - 1].filename);
						}
					}
					this.examloglist = ExamBll.GetExamLogList(this.examresult.channelid, this.userid);
					this.examtopicresultlist = ExamBll.GetExamResultTopicList(this.resultid);
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
						list2.Add(current);
					}
				}
				num++;
			}
			return list2;
		}

		protected string FmAnswer(string content, int tid, string uanswer)
		{
			string[] array = FPArray.SplitString(content, "(#answer)");
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

		private string OptionAnswer(string optionlist, string answer)
		{
			string[] array = FPArray.SplitString("A,B,C,D,E,F,G,H");
			int[] array2 = FPArray.SplitInt(optionlist);
			string text = "";
			for (int i = 0; i < array2.Length; i++)
			{
				bool flag = FPArray.InArray(array[array2[i]], answer) >= 0;
				if (flag)
				{
					bool flag2 = text != "";
					if (flag2)
					{
						text += ",";
					}
					text += array[i];
				}
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
	}
}
