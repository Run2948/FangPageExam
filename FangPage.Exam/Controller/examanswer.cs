using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000020 RID: 32
	public class examanswer : LoginController
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
			if (this.examresult.id == 0)
			{
				this.ShowErr("该考生的试卷不存在或已被删除。");
			}
			else if (this.examresult.status == 0)
			{
				this.ShowErr("对不起，该考试尚未完成，不能查看答案。");
			}
			else
			{
				this.examresult.passmark = this.examresult.passmark * this.examresult.total / 100.0;
				this.examloglist = ExamBll.GetExamLogList(this.examresult.channelid, this.userid);
				string commandText = string.Format("SELECT MAX([score]) AS [maxscore] FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [status]>0", DbConfigs.Prefix, this.examresult.examid);
				this.maxscore = Math.Round((double)FPUtils.StrToFloat(DbHelper.ExecuteScalar(commandText).ToString(), 0f), 1);
				if (this.maxscore > this.examresult.total)
				{
					this.maxscore = this.examresult.total;
				}
				commandText = string.Format("SELECT AVG([score]) AS [avgscore] FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [status]>0", DbConfigs.Prefix, this.examresult.examid);
				this.avgscore = Math.Round((double)FPUtils.StrToFloat(DbHelper.ExecuteScalar(commandText).ToString(), 0f), 1);
				SqlParam sqlParam = DbHelper.MakeAndWhere("examid", this.examresult.examid);
				this.testers = DbHelper.ExecuteCount<ExamResult>(new SqlParam[]
				{
					sqlParam
				});
				commandText = string.Format("SELECT COUNT(*) FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [score]>{2} AND [status]>0", DbConfigs.Prefix, this.examresult.examid, this.examresult.score);
				if (this.examresult.score > 0.0)
				{
					this.display = FPUtils.StrToInt(DbHelper.ExecuteScalar(commandText).ToString(), 0) + 1;
				}
				this.examtopicresultlist = ExamBll.GetExamResultTopicList(this.resultid);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000E908 File Offset: 0x0000CB08
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
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("qid", WhereType.In, resultinfo.questionlist),
				DbHelper.MakeAndWhere("uid", this.userid)
			};
			List<ExamNote> list3 = DbHelper.ExecuteList<ExamNote>(sqlparams);
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
						foreach (ExamNote examNote in list3)
						{
							if (examNote.qid == examQuestion.id)
							{
								examQuestion.note = examNote.note;
							}
						}
						if (this.examloglist.ContainsKey(examQuestion.sortid))
						{
							ExamLogInfo examLogInfo = this.examloglist[examQuestion.sortid];
							if (FPUtils.InArray(examQuestion.id, examLogInfo.favlist))
							{
								examQuestion.isfav = 1;
							}
						}
						list2.Add(examQuestion);
					}
				}
				num++;
			}
			return list2;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
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

		// Token: 0x06000098 RID: 152 RVA: 0x0000EC64 File Offset: 0x0000CE64
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

		// Token: 0x06000099 RID: 153 RVA: 0x0000ED58 File Offset: 0x0000CF58
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

		// Token: 0x0400009A RID: 154
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x0400009B RID: 155
		protected ExamResult examresult = new ExamResult();

		// Token: 0x0400009C RID: 156
		protected List<ExamResultTopic> examtopicresultlist = new List<ExamResultTopic>();

		// Token: 0x0400009D RID: 157
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F"
		};

		// Token: 0x0400009E RID: 158
		protected double maxscore = 0.0;

		// Token: 0x0400009F RID: 159
		protected double avgscore = 0.0;

		// Token: 0x040000A0 RID: 160
		protected int testers = 0;

		// Token: 0x040000A1 RID: 161
		protected int display = 0;

		// Token: 0x040000A2 RID: 162
		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();

		// Token: 0x040000A3 RID: 163
		protected ExamConfig examconfig = new ExamConfig();
	}
}
