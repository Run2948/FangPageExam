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
	// Token: 0x02000034 RID: 52
	public class testpost : LoginController
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x000161B4 File Offset: 0x000143B4
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else if (this.ispost)
			{
				if (this.qidlist == "")
				{
					this.ShowErr("对不起，题目为空不能提交。");
				}
				else
				{
					List<ExamQuestion> questionList = QuestionBll.GetQuestionList(this.qidlist);
					this.examresult.uid = this.userid;
					this.examresult.channelid = this.channelinfo.id;
					this.examresult.examid = 0;
					this.examresult.examtype = 0;
					this.examresult.showanswer = 1;
					this.examresult.allowdelete = 1;
					this.examresult.examname = ((this.testtype == 1) ? "专项智能练习" : "快速智能练习");
					this.examresult.examtime = FPRequest.GetInt("utime") / 60;
					this.examresult.total = 100.0;
					this.examresult.passmark = 60.0;
					this.examresult.credits = 0;
					this.examresult.questions = questionList.Count;
					this.examresult.utime = FPRequest.GetInt("utime");
					this.examresult.starttime = FPRequest.GetDateTime("starttime");
					this.examresult.endtime = DbUtils.GetDateTime();
					this.examresult.status = 1;
					this.examresult.id = DbHelper.ExecuteInsert<ExamResult>(this.examresult);
					if (this.examresult.id > 0)
					{
						ExamResultTopic examResultTopic = new ExamResultTopic();
						examResultTopic.resultid = this.examresult.id;
						examResultTopic.type = 0;
						examResultTopic.title = ((this.testtype == 1) ? "专项智能练习" : "快速智能练习");
						examResultTopic.perscore = Math.Round(this.examresult.total / (double)this.examresult.questions, 1);
						examResultTopic.display = 1;
						examResultTopic.questions = questionList.Count;
						examResultTopic.questionlist = this.qidlist;
						int num = 0;
						foreach (ExamQuestion examQuestion in QuestionBll.GetQuestionList(examResultTopic.questionlist))
						{
							if (examResultTopic.optionlist != "")
							{
								ExamResultTopic examResultTopic2 = examResultTopic;
								examResultTopic2.optionlist += "|";
							}
							if (examQuestion.type == 1 || examQuestion.type == 2)
							{
								ExamResultTopic examResultTopic3 = examResultTopic;
								examResultTopic3.optionlist += this.OptionInt(examQuestion.ascount);
							}
							else
							{
								ExamResultTopic examResultTopic4 = examResultTopic;
								examResultTopic4.optionlist += "*";
							}
							string text = FPRequest.GetString("answer_" + examQuestion.id);
							if (num == 0)
							{
								ExamResultTopic examResultTopic5 = examResultTopic;
								examResultTopic5.answerlist += text;
							}
							else
							{
								ExamResultTopic examResultTopic6 = examResultTopic;
								examResultTopic6.answerlist = examResultTopic6.answerlist + "§" + text;
							}
							if (examResultTopic.scorelist != "")
							{
								ExamResultTopic examResultTopic7 = examResultTopic;
								examResultTopic7.scorelist += "|";
							}
							int num2 = 0;
							double num3 = 0.0;
							if (examQuestion.type <= 3)
							{
								if (text == examQuestion.answer && text != "")
								{
									num2 = examQuestion.ascount;
									num3 = examResultTopic.perscore;
									this.examresult.score1 += num3;
								}
							}
							else if (examQuestion.type == 4)
							{
								string[] array;
								if (examQuestion.upperflg == 1)
								{
									array = FPUtils.SplitString(examQuestion.answer, ",");
								}
								else
								{
									array = FPUtils.SplitString(examQuestion.answer.ToLower(), ",");
								}
								text = testpost.DelSameAnser(text);
								string[] array2;
								if (examQuestion.upperflg == 1)
								{
									array2 = FPUtils.SplitString(text, ",", array.Length);
								}
								else
								{
									array2 = FPUtils.SplitString(text.ToLower(), ",", array.Length);
								}
								if (examQuestion.orderflg == 1)
								{
									for (int i = 0; i < array2.Length; i++)
									{
										if (FPUtils.InArray(array2[i], array[i], "|"))
										{
											num2++;
										}
									}
								}
								else
								{
									for (int i = 0; i < array2.Length; i++)
									{
										if (FPUtils.InArray(array2[i], examQuestion.answer.Replace("|", ",")))
										{
											num2++;
										}
									}
								}
								if (num2 > 0)
								{
									if (examQuestion.ascount <= 0)
									{
										examQuestion.ascount = 1;
									}
									num3 = Math.Round(examResultTopic.perscore / (double)examQuestion.ascount * (double)num2, 1);
									this.examresult.score2 += num3;
								}
							}
							else if (examQuestion.type == 5)
							{
								foreach (string value in FPUtils.SplitString(examQuestion.answerkey))
								{
									if (text.IndexOf(value) >= 0)
									{
										num2++;
									}
								}
								if (num2 > 0 && text != "")
								{
									if (examQuestion.ascount > 0)
									{
										examQuestion.ascount = 1;
									}
									num3 = Math.Round(examResultTopic.perscore / (double)examQuestion.ascount * (double)num2, 1);
									this.examresult.score2 += num3;
								}
							}
							else if (examQuestion.type == 6)
							{
								for (int k = 0; k < examQuestion.title.Length; k++)
								{
									if (text.IndexOf(examQuestion.title.Substring(k, 1)) >= 0)
									{
										num2++;
									}
								}
								if (num2 > 0)
								{
									num3 = Math.Round(examResultTopic.perscore / (double)examQuestion.title.Length * (double)num2, 1);
									this.examresult.score2 += num3;
								}
							}
							this.examresult.score += num3;
							examResultTopic.score += num3;
							ExamResultTopic examResultTopic8 = examResultTopic;
							examResultTopic8.scorelist += num3.ToString();
							if (examResultTopic.correctlist != "")
							{
								ExamResultTopic examResultTopic9 = examResultTopic;
								examResultTopic9.correctlist += "|";
							}
							bool flag = false;
							if (num3 >= examResultTopic.perscore * 0.6)
							{
								ExamResultTopic examResultTopic10 = examResultTopic;
								examResultTopic10.correctlist += "1";
							}
							else
							{
								ExamResultTopic examResultTopic11 = examResultTopic;
								examResultTopic11.correctlist += "0";
								examResultTopic.wrongs++;
								this.examresult.wrongs++;
								if (text == "")
								{
									this.examresult.unanswer++;
								}
								flag = true;
							}
							string sqlstring;
							if (flag)
							{
								sqlstring = string.Format("UPDATE [{0}Exam_ExamQuestion] SET [exams]=[exams]+1,[wrongs]=[wrongs]+1 WHERE [id]={1}", DbConfigs.Prefix, examQuestion.id);
							}
							else
							{
								sqlstring = string.Format("UPDATE [{0}Exam_ExamQuestion] SET [exams]=[exams]+1 WHERE [id]={1}", DbConfigs.Prefix, examQuestion.id);
							}
							DbHelper.ExecuteSql(sqlstring);
							examQuestion.useranswer = text;
							SortInfo sortInfo = SortBll.GetSortInfo(examQuestion.sortid);
							ExamBll.UpdateExamLog(sortInfo, this.userid, examQuestion, flag);
							num++;
						}
						DbHelper.ExecuteInsert<ExamResultTopic>(examResultTopic);
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("score1", this.examresult.score1),
							DbHelper.MakeSet("score2", this.examresult.score2),
							DbHelper.MakeSet("score", this.examresult.score),
							DbHelper.MakeSet("wrongs", this.examresult.wrongs),
							DbHelper.MakeAndWhere("id", this.examresult.id)
						};
						DbHelper.ExecuteUpdate<ExamResult>(sqlparams);
					}
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00016B74 File Offset: 0x00014D74
		private static string DelSameAnser(string strIm)
		{
			string[] array = FPUtils.DelArraySame(FPUtils.SplitString(strIm));
			string text = "";
			foreach (string str in array)
			{
				if (text != "")
				{
					text += ",";
				}
				text += str;
			}
			return text;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00016BE8 File Offset: 0x00014DE8
		protected string OptionInt(int ascount)
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
			return text;
		}

		// Token: 0x0400010D RID: 269
		protected string qidlist = FPRequest.GetString("qidlist");

		// Token: 0x0400010E RID: 270
		protected int testtype = FPRequest.GetInt("testtype");

		// Token: 0x0400010F RID: 271
		protected ExamResult examresult = new ExamResult();

		// Token: 0x04000110 RID: 272
		protected ChannelInfo channelinfo = new ChannelInfo();
	}
}
