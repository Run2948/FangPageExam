using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000025 RID: 37
	public class exampost : LoginController
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		protected override void View()
		{
			if (this.ispost)
			{
				this.examresult = ExamBll.GetExamResult(this.resultid);
				if (this.examresult.id == 0)
				{
					this.ShowErr("对不起，该考试不存在或已被删除。");
				}
				else if (this.examresult.uid != this.userid)
				{
					this.ShowErr("对不起，您不是该考试的主人。");
				}
				else if (this.examresult.status == 1)
				{
					this.ShowErr("对不起，该考试已完成，不能重复提交。");
				}
				else
				{
					this.examresult.utime = FPRequest.GetInt("utime");
					this.examresult.endtime = DbUtils.GetDateTime();
					this.examresult.status = 1;
					if (DbHelper.ExecuteUpdate<ExamResult>(this.examresult) > 0)
					{
						List<ExamResultTopic> examResultTopicList = ExamBll.GetExamResultTopicList(this.resultid);
						int num = 0;
						foreach (ExamResultTopic examResultTopic in examResultTopicList)
						{
							if (examResultTopic.questions == 0)
							{
								num++;
							}
							else
							{
								examResultTopicList[num].answerlist = "";
								examResultTopicList[num].scorelist = "";
								examResultTopicList[num].correctlist = "";
								List<ExamQuestion> questionList = QuestionBll.GetQuestionList(examResultTopic.questionlist);
								string[] array = FPUtils.SplitString(examResultTopic.optionlist, "|", questionList.Count);
								int num2 = 0;
								foreach (ExamQuestion examQuestion in questionList)
								{
									string text = FPRequest.GetString("answer_" + examQuestion.id);
									if (num2 == 0)
									{
										ExamResultTopic examResultTopic2 = examResultTopicList[num];
										examResultTopic2.answerlist += text;
									}
									else
									{
										ExamResultTopic examResultTopic3 = examResultTopicList[num];
										examResultTopic3.answerlist = examResultTopic3.answerlist + "§" + text;
									}
									if (examResultTopicList[num].scorelist != "")
									{
										ExamResultTopic examResultTopic4 = examResultTopicList[num];
										examResultTopic4.scorelist += "|";
									}
									examQuestion.optionlist = array[num2];
									int num3 = 0;
									double num4 = 0.0;
									if (examQuestion.type == 1 || examQuestion.type == 2)
									{
										num3 = examQuestion.ascount;
										examQuestion.answer = this.OptionAnswer(array[num2], examQuestion.answer);
										if (text == examQuestion.answer && text != "")
										{
											num4 = examResultTopic.perscore;
										}
										this.examresult.score1 += num4;
									}
									else if (examQuestion.type == 3)
									{
										num3 = examQuestion.ascount;
										if (text == examQuestion.answer && text != "")
										{
											num4 = examResultTopic.perscore;
										}
										this.examresult.score1 += num4;
									}
									else if (examQuestion.type == 4)
									{
										string[] array2;
										if (examQuestion.upperflg == 1)
										{
											array2 = FPUtils.SplitString(examQuestion.answer, ",");
										}
										else
										{
											array2 = FPUtils.SplitString(examQuestion.answer.ToLower(), ",");
										}
										text = exampost.DelSameAnser(text);
										string[] array3;
										if (examQuestion.upperflg == 1)
										{
											array3 = FPUtils.SplitString(text, ",", array2.Length);
										}
										else
										{
											array3 = FPUtils.SplitString(text.ToLower(), ",", array2.Length);
										}
										if (examQuestion.orderflg == 1)
										{
											for (int i = 0; i < array3.Length; i++)
											{
												if (FPUtils.InArray(array3[i], array2[i], "|"))
												{
													num3++;
												}
											}
										}
										else
										{
											for (int i = 0; i < array3.Length; i++)
											{
												if (FPUtils.InArray(array3[i], examQuestion.answer.Replace("|", ",")))
												{
													num3++;
												}
											}
										}
										if (num3 > 0)
										{
											if (examQuestion.ascount <= 0)
											{
												examQuestion.ascount = 1;
											}
											num4 = Math.Round(examResultTopic.perscore / (double)examQuestion.ascount * (double)num3, 1);
											this.examresult.score2 += num4;
										}
									}
									else if (examQuestion.type == 5)
									{
										foreach (string value in FPUtils.SplitString(examQuestion.answerkey))
										{
											if (text.IndexOf(value) >= 0)
											{
												num3++;
											}
										}
										if (num3 > 0 && text != "")
										{
											if (examQuestion.ascount > 0)
											{
												examQuestion.ascount = 1;
											}
											num4 = Math.Round(examResultTopic.perscore / (double)examQuestion.ascount * (double)num3, 1);
											this.examresult.score2 += num4;
										}
									}
									else if (examQuestion.type == 6)
									{
										for (int k = 0; k < examQuestion.title.Length; k++)
										{
											if (text.IndexOf(examQuestion.title.Substring(k, 1)) >= 0)
											{
												num3++;
											}
										}
										if (num3 > 0)
										{
											num4 = Math.Round(examResultTopic.perscore / (double)examQuestion.title.Length * (double)num3, 1);
											this.examresult.score2 += num4;
										}
									}
									this.examresult.score += num4;
									examResultTopicList[num].score += num4;
									ExamResultTopic examResultTopic5 = examResultTopicList[num];
									examResultTopic5.scorelist += num4.ToString();
									if (examResultTopicList[num].correctlist != "")
									{
										ExamResultTopic examResultTopic6 = examResultTopicList[num];
										examResultTopic6.correctlist += "|";
									}
									bool flag = false;
									if (num4 >= examResultTopic.perscore * 0.6)
									{
										ExamResultTopic examResultTopic7 = examResultTopicList[num];
										examResultTopic7.correctlist += "1";
									}
									else
									{
										ExamResultTopic examResultTopic8 = examResultTopicList[num];
										examResultTopic8.correctlist += "0";
										examResultTopicList[num].wrongs++;
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
									num2++;
								}
								DbHelper.ExecuteUpdate<ExamResultTopic>(examResultTopicList[num]);
								num++;
							}
						}
						this.expinfo = ExamBll.GetExamExpByScore(this.examresult.score, this.examresult.examid);
						this.examresult.exnote = this.expinfo.comment;
						this.examresult.exp = this.expinfo.exp;
						this.examresult.getcredits = this.expinfo.credits;
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [score1]={1},[score2]={2},[score]={3},[wrongs]={4},[exp]={5},[exnote]='{6}' WHERE [id]={7}|", new object[]
						{
							DbConfigs.Prefix,
							this.examresult.score1,
							this.examresult.score2,
							this.examresult.score,
							this.examresult.wrongs,
							this.examresult.exp,
							this.examresult.exnote,
							this.examresult.id
						});
						stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [score]=[score]+{1} WHERE [id]={2}", DbConfigs.Prefix, this.examresult.score, this.examresult.examid);
						this.msg = DbHelper.ExecuteSql(stringBuilder.ToString());
						if (this.examresult.exp > 0)
						{
							UserBll.UpdateUserExp(this.examresult.uid, this.examresult.exp);
						}
						if (this.examresult.getcredits > 0)
						{
							UserBll.UpdateUserCredit(new CreditInfo
							{
								uid = this.examresult.uid,
								name = "考试奖励积分",
								type = 0,
								credits = this.examresult.getcredits,
								doid = this.userid,
								doname = this.username
							});
						}
						if (this.msg != "")
						{
							this.ShowErr(this.msg);
						}
					}
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000103CC File Offset: 0x0000E5CC
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

		// Token: 0x060000A6 RID: 166 RVA: 0x00010440 File Offset: 0x0000E640
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

		// Token: 0x040000B6 RID: 182
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x040000B7 RID: 183
		protected ExamResult examresult = new ExamResult();

		// Token: 0x040000B8 RID: 184
		protected ExpInfo expinfo = new ExpInfo();
	}
}
