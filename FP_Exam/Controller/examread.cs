using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000027 RID: 39
	public class examread : LoginController
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000117F4 File Offset: 0x0000F9F4
		protected override void View()
		{
			if (!this.isperm)
			{
				this.ShowErr("对不起，您没有权限阅卷。");
			}
			else
			{
				this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
				if (this.examresult.id == 0)
				{
					this.ShowErr("该考生的试卷不存在或已被删除。");
				}
				else
				{
					this.examloglist = ExamBll.GetExamLogList(this.examresult.channelid, this.userid);
					string commandText = string.Format("SELECT MAX([score]) AS [maxscore] FROM [{0}Exam_ExamResult] WHERE [id]={1}", DbConfigs.Prefix, this.resultid);
					this.maxscore = Math.Round((double)FPUtils.StrToFloat(DbHelper.ExecuteScalar(commandText).ToString(), 0f), 1);
					if (this.maxscore > this.examresult.total)
					{
						this.maxscore = this.examresult.total;
					}
					commandText = string.Format("SELECT AVG([score]) AS [avgscore] FROM [{0}Exam_ExamResult] WHERE [id]={1}", DbConfigs.Prefix, this.resultid);
					this.avgscore = Math.Round((double)FPUtils.StrToFloat(DbHelper.ExecuteScalar(commandText).ToString(), 0f), 1);
					SqlParam sqlParam = DbHelper.MakeAndWhere("examid", this.examresult.examid);
					this.testers = DbHelper.ExecuteCount<ExamResult>(new SqlParam[]
					{
						sqlParam
					});
					commandText = string.Format("SELECT COUNT(*) FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [score]>{2}", DbConfigs.Prefix, this.examresult.examid, this.examresult.score);
					if (this.examresult.score > 0.0)
					{
						this.display = FPUtils.StrToInt(DbHelper.ExecuteScalar(commandText).ToString(), 0) + 1;
					}
					this.examtopicresultlist = ExamBll.GetExamResultTopicList(this.resultid);
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000119C4 File Offset: 0x0000FBC4
		protected List<ExamQuestion> GetQuestionList(ExamResultTopic resultinfo)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, resultinfo.questionlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPUtils.SplitInt(resultinfo.questionlist);
			string[] array2 = FPUtils.SplitString(resultinfo.answerlist, "§", array.Length);
			string[] array3 = FPUtils.SplitString(resultinfo.scorelist, "|", array.Length);
			string[] array4 = FPUtils.SplitString(resultinfo.optionlist, "|", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int num = 0;
			foreach (int num2 in array)
			{
				foreach (ExamQuestion examQuestion in list)
				{
					if (examQuestion.id == num2)
					{
						examQuestion.useranswer = array2[num];
						examQuestion.userscore = (double)FPUtils.StrToFloat(array3[num]);
						examQuestion.optionlist = array4[num];
						if (examQuestion.type == 1 || examQuestion.type == 2)
						{
							examQuestion.answer = this.OptionAnswer(examQuestion.optionlist, examQuestion.answer);
						}
						list2.Add(examQuestion);
					}
				}
				num++;
			}
			return list2;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00011B60 File Offset: 0x0000FD60
		protected string FmAnswer(string content, int tid, string uanswer)
		{
			string[] array = FPUtils.SplitString(content, "(#answer)");
			string[] array2 = FPUtils.SplitString(uanswer, ",", array.Length);
			content = "";
			int num = 0;
			foreach (string str in array)
			{
				if (num < array.Length - 1)
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

		// Token: 0x060000B5 RID: 181 RVA: 0x00011C00 File Offset: 0x0000FE00
		protected string Option(string[] opstr, int ascount, string optionlist)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			int[] array2 = FPUtils.SplitInt(optionlist, ",", ascount);
			string text = "";
			if (ascount > opstr.Length)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				if (optionlist != "")
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						array[i],
						".",
						opstr[array2[i]],
						"<br/>"
					});
				}
				else
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						array[i],
						".",
						opstr[i],
						"<br/>"
					});
				}
			}
			return text;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		private string OptionAnswer(string optionlist, string answer)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			int[] array2 = FPUtils.SplitInt(optionlist);
			string text = "";
			for (int i = 0; i < array2.Length; i++)
			{
				if (FPUtils.InArray(array[array2[i]], answer))
				{
					if (text != "")
					{
						text += ",";
					}
					text += array[i];
				}
			}
			return text;
		}

		// Token: 0x040000C4 RID: 196
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x040000C5 RID: 197
		protected ExamResult examresult = new ExamResult();

		// Token: 0x040000C6 RID: 198
		protected List<ExamResultTopic> examtopicresultlist = new List<ExamResultTopic>();

		// Token: 0x040000C7 RID: 199
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F"
		};

		// Token: 0x040000C8 RID: 200
		protected double maxscore = 0.0;

		// Token: 0x040000C9 RID: 201
		protected double avgscore = 0.0;

		// Token: 0x040000CA RID: 202
		protected int testers = 0;

		// Token: 0x040000CB RID: 203
		protected int display = 0;

		// Token: 0x040000CC RID: 204
		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();
	}
}
