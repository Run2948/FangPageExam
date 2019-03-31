using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP_Exam.Wap.Controller
{
	public class exampost : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected override void Controller()
		{
			ExamResult examResult = ExamBll.GetExamResult(this.resultid);
			ExpInfo expInfo = new ExpInfo();
			bool flag = examResult.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
			else
			{
				bool flag2 = examResult.uid != this.userid;
				if (flag2)
				{
					this.ShowErr("对不起，您不是该考试的主人。");
				}
				else
				{
					bool flag3 = examResult.status == 1;
					if (flag3)
					{
						this.ShowErr("对不起，该考试已完成，不能重复提交。");
					}
					else
					{
						ExamInfo examInfo = DbHelper.ExecuteModel<ExamInfo>(examResult.examid);
						examResult.utime += FPRequest.GetInt("utime");
						examResult.endtime = DbUtils.GetDateTime();
						examResult.status = 1;
						examResult.isvideo = examInfo.isvideo;
						examResult.client = "mobile";
						bool flag4 = DbHelper.ExecuteUpdate<ExamResult>(examResult) > 0;
						if (flag4)
						{
							List<ExamResultTopic> examResultTopicList = ExamBll.GetExamResultTopicList(this.resultid);
							int num = 0;
							foreach (ExamResultTopic current in examResultTopicList)
							{
								bool flag5 = current.questions == 0;
								if (flag5)
								{
									num++;
								}
								else
								{
									examResultTopicList[num].scorelist = "";
									examResultTopicList[num].correctlist = "";
									List<ExamQuestion> questionList = QuestionBll.GetQuestionList(current.questionlist);
									string[] array = FPArray.SplitString(current.answerlist, "§", questionList.Count);
									int num2 = 0;
									foreach (ExamQuestion current2 in questionList)
									{
										string text = array[num2];
										bool flag6 = examResultTopicList[num].scorelist != "";
										if (flag6)
										{
											ExamResultTopic expr_1C0 = examResultTopicList[num];
											expr_1C0.scorelist += "|";
										}
										int num3 = 0;
										double num4 = 0.0;
										bool flag7 = current2.type == "TYPE_RADIO" || current2.type == "TYPE_MULTIPLE";
										if (flag7)
										{
											num3 = current2.ascount;
											bool flag8 = text == current2.answer && text != "";
											if (flag8)
											{
												num4 = current.perscore;
											}
											examResult.score1 += num4;
										}
										else
										{
											bool flag9 = current2.type == "TYPE_TRUE_FALSE";
											if (flag9)
											{
												num3 = current2.ascount;
												bool flag10 = text == current2.answer && text != "";
												if (flag10)
												{
													num4 = current.perscore;
												}
												examResult.score1 += num4;
											}
											else
											{
												bool flag11 = current2.type == "TYPE_BLANK";
												if (flag11)
												{
													bool flag12 = current2.upperflg == 1;
													string[] array2;
													if (flag12)
													{
														array2 = FPArray.SplitString(current2.answer, new string[]
														{
															",",
															"&&"
														});
													}
													else
													{
														array2 = FPArray.SplitString(current2.answer.ToLower(), new string[]
														{
															",",
															"&&"
														});
													}
													text = exampost.DelSameAnser(text);
													bool flag13 = current2.upperflg == 1;
													string[] array3;
													if (flag13)
													{
														array3 = FPArray.SplitString(text, ",", array2.Length);
													}
													else
													{
														array3 = FPArray.SplitString(text.ToLower(), ",", array2.Length);
														current2.answer = current2.answer.ToLower();
													}
													bool flag14 = current2.orderflg == 1;
													if (flag14)
													{
														for (int i = 0; i < array3.Length; i++)
														{
															bool flag15 = FPArray.InArray(array3[i], array2[i], "|") >= 0;
															if (flag15)
															{
																num3++;
															}
														}
													}
													else
													{
														for (int j = 0; j < array3.Length; j++)
														{
															bool flag16 = FPArray.InArray(array3[j], current2.answer.Replace("|", ",")) >= 0;
															if (flag16)
															{
																num3++;
															}
														}
													}
													bool flag17 = num3 > 0;
													if (flag17)
													{
														bool flag18 = current2.ascount <= 0;
														if (flag18)
														{
															current2.ascount = 1;
														}
														num4 = Math.Round(current.perscore / (double)current2.ascount * (double)num3, 2);
														examResult.score1 += num4;
													}
												}
												else
												{
													bool flag19 = current2.type == "TYPE_ANSWER";
													if (flag19)
													{
														bool flag20 = current2.answerkey != "";
														if (flag20)
														{
															current2.ascount = 0;
															string[] array4 = FPArray.SplitString(current2.answerkey);
															for (int k = 0; k < array4.Length; k++)
															{
																string value = array4[k];
																bool flag21 = text.IndexOf(value) >= 0;
																if (flag21)
																{
																	num3++;
																}
																current2.ascount++;
															}
															bool flag22 = num3 > 0 && text != "";
															if (flag22)
															{
																bool flag23 = current2.ascount <= 0;
																if (flag23)
																{
																	current2.ascount = 1;
																}
																num4 = Math.Round(current.perscore / (double)current2.ascount * (double)num3, 2);
																examResult.score2 += num4;
															}
														}
													}
													else
													{
														bool flag24 = current2.type == "TYPE_OPERATION";
														if (flag24)
														{
															num4 = 0.0;
														}
													}
												}
											}
										}
										examResult.score += num4;
										examResultTopicList[num].score += num4;
										ExamResultTopic expr_5FE = examResultTopicList[num];
										expr_5FE.scorelist += num4.ToString();
										bool flag25 = examResultTopicList[num].correctlist != "";
										if (flag25)
										{
											ExamResultTopic expr_63E = examResultTopicList[num];
											expr_63E.correctlist += "|";
										}
										bool flag26 = false;
										bool flag27 = num4 >= current.perscore * 0.6;
										if (flag27)
										{
											ExamResultTopic expr_680 = examResultTopicList[num];
											expr_680.correctlist += "1";
										}
										else
										{
											ExamResultTopic expr_6A3 = examResultTopicList[num];
											expr_6A3.correctlist += "0";
											examResultTopicList[num].wrongs++;
											examResult.wrongs++;
											bool flag28 = text == "";
											if (flag28)
											{
												examResult.unanswer++;
											}
											flag26 = true;
										}
										bool flag29 = flag26;
										string sqlstring;
										if (flag29)
										{
											sqlstring = string.Format("UPDATE [{0}Exam_ExamQuestion] SET [exams]=[exams]+1,[wrongs]=[wrongs]+1 WHERE [id]={1}", DbConfigs.Prefix, current2.id);
										}
										else
										{
											sqlstring = string.Format("UPDATE [{0}Exam_ExamQuestion] SET [exams]=[exams]+1 WHERE [id]={1}", DbConfigs.Prefix, current2.id);
										}
										DbHelper.ExecuteSql(sqlstring);
										current2.useranswer = text;
										SortInfo sortInfo = SortBll.GetSortInfo(current2.sortid);
										ExamBll.UpdateExamLog(sortInfo, this.userid, current2, flag26);
										num2++;
									}
									DbHelper.ExecuteUpdate<ExamResultTopic>(examResultTopicList[num]);
									num++;
								}
							}
							expInfo = ExamBll.GetExamExpByScore(examResult.score, examResult.examid);
							examResult.exnote = expInfo.comment;
							examResult.exp = expInfo.exp;
							examResult.getcredits = expInfo.credits;
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [score1]={1},[score2]={2},[score]={3},[wrongs]={4},[exp]={5},[exnote]='{6}' WHERE [id]={7}|", new object[]
							{
								DbConfigs.Prefix,
								examResult.score1,
								examResult.score2,
								examResult.score,
								examResult.wrongs,
								examResult.exp,
								examResult.exnote,
								examResult.id
							});
							stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [score]=[score]+{1} WHERE [id]={2}", DbConfigs.Prefix, examResult.score, examResult.examid);
							this.msg = DbHelper.ExecuteSql(stringBuilder.ToString());
							bool flag30 = examResult.exp > 0;
							if (flag30)
							{
								UserBll.UpdateUserExp(examResult.uid, examResult.exp);
							}
							bool flag31 = examResult.getcredits > 0;
							if (flag31)
							{
								UserBll.UpdateUserCredit(new CreditInfo
								{
									uid = examResult.uid,
									name = "考试奖励积分",
									type = 0,
									credits = examResult.getcredits,
									doid = this.userid,
									doname = this.username
								});
							}
						}
						base.Response.Redirect("examresult.aspx?resultid=" + this.resultid);
					}
				}
			}
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

		private static string DelSameAnser(string strIm)
		{
			string[] array = FPArray.RemoveSame(FPArray.SplitString(strIm));
			string text = "";
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string str = array2[i];
				bool flag = text != "";
				if (flag)
				{
					text += ",";
				}
				text += str;
			}
			return text;
		}
	}
}
