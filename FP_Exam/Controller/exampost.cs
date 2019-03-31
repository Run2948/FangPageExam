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

namespace FP_Exam.Controller
{
	public class exampost : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected ExamInfo examinfo = new ExamInfo();

		protected ExpInfo expinfo = new ExpInfo();

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
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
						this.ShowErr("对不起，您不是该考试的主人。");
					}
					else
					{
						bool flag3 = this.examresult.status == 1;
						if (flag3)
						{
							this.ShowErr("对不起，该考试已完成，不能重复提交。");
						}
						else
						{
							this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examresult.examid);
							this.examresult.utime = FPRequest.GetInt("utime");
							this.examresult.endtime = DbUtils.GetDateTime();
							this.examresult.status = 1;
							this.examresult.isvideo = this.examinfo.isvideo;
							this.examresult.client = "pc";
							this.examresult.papertype = this.examinfo.papertype;
							bool flag4 = DbHelper.ExecuteUpdate<ExamResult>(this.examresult) > 0;
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
											string text = FPRequest.GetString("answer_" + current2.id);
											bool flag6 = examResultTopicList[num].scorelist != "";
											if (flag6)
											{
												ExamResultTopic expr_22C = examResultTopicList[num];
												expr_22C.scorelist += "|";
											}
											int num3 = 0;
											double num4 = 0.0;
											bool flag7 = text != array[num2];
											if (flag7)
											{
												array[num2] = text;
												examResultTopicList[num].answerlist = FPArray.Replace(examResultTopicList[num].answerlist, text, num2, "§");
											}
											bool flag8 = current2.type == "TYPE_RADIO" || current2.type == "TYPE_MULTIPLE";
											if (flag8)
											{
												num3 = current2.ascount;
												bool flag9 = text == current2.answer && text != "";
												if (flag9)
												{
													num4 = current.perscore;
												}
												this.examresult.score1 += num4;
											}
											else
											{
												bool flag10 = current2.type == "TYPE_TRUE_FALSE";
												if (flag10)
												{
													num3 = current2.ascount;
													bool flag11 = text == current2.answer && text != "";
													if (flag11)
													{
														num4 = current.perscore;
													}
													this.examresult.score1 += num4;
												}
												else
												{
													bool flag12 = current2.type == "TYPE_BLANK";
													if (flag12)
													{
														bool flag13 = current2.upperflg == 1;
														string[] array2;
														if (flag13)
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
														bool flag14 = current2.upperflg == 1;
														string[] array3;
														if (flag14)
														{
															array3 = FPArray.SplitString(text, ",", array2.Length);
														}
														else
														{
															array3 = FPArray.SplitString(text.ToLower(), ",", array2.Length);
														}
														bool flag15 = current2.orderflg == 1;
														if (flag15)
														{
															for (int i = 0; i < array3.Length; i++)
															{
																bool flag16 = FPArray.InArray(array3[i], array2[i], "|") >= 0;
																if (flag16)
																{
																	num3++;
																}
															}
														}
														else
														{
															for (int j = 0; j < array3.Length; j++)
															{
																bool flag17 = FPArray.InArray(array3[j], current2.answer.Replace("|", ",")) >= 0;
																if (flag17)
																{
																	num3++;
																}
															}
														}
														bool flag18 = num3 > 0;
														if (flag18)
														{
															bool flag19 = current2.ascount <= 0;
															if (flag19)
															{
																current2.ascount = 1;
															}
															num4 = Math.Round(current.perscore / (double)current2.ascount * (double)num3, 2);
															this.examresult.score1 += num4;
														}
													}
													else
													{
														bool flag20 = current2.type == "TYPE_ANSWER";
														if (flag20)
														{
															bool flag21 = current2.answerkey != "";
															if (flag21)
															{
																current2.ascount = 0;
																string[] array4 = FPArray.SplitString(current2.answerkey);
																for (int k = 0; k < array4.Length; k++)
																{
																	string value = array4[k];
																	bool flag22 = text.IndexOf(value) >= 0;
																	if (flag22)
																	{
																		num3++;
																	}
																	current2.ascount++;
																}
																bool flag23 = num3 > 0 && text != "";
																if (flag23)
																{
																	bool flag24 = current2.ascount <= 0;
																	if (flag24)
																	{
																		current2.ascount = 1;
																	}
																	num4 = Math.Round(current.perscore / (double)current2.ascount * (double)num3, 2);
																	this.examresult.score2 += num4;
																}
															}
														}
														else
														{
															bool flag25 = current2.type == "TYPE_OPERATION";
															if (flag25)
															{
															}
														}
													}
												}
											}
											this.examresult.score += num4;
											examResultTopicList[num].score += num4;
											ExamResultTopic expr_6AA = examResultTopicList[num];
											expr_6AA.scorelist += num4.ToString();
											bool flag26 = examResultTopicList[num].correctlist != "";
											if (flag26)
											{
												ExamResultTopic expr_6EA = examResultTopicList[num];
												expr_6EA.correctlist += "|";
											}
											bool flag27 = false;
											bool flag28 = num4 >= current.perscore * 0.6;
											if (flag28)
											{
												ExamResultTopic expr_72C = examResultTopicList[num];
												expr_72C.correctlist += "1";
											}
											else
											{
												ExamResultTopic expr_74F = examResultTopicList[num];
												expr_74F.correctlist += "0";
												examResultTopicList[num].wrongs++;
												this.examresult.wrongs++;
												bool flag29 = text == "";
												if (flag29)
												{
													this.examresult.unanswer++;
												}
												flag27 = true;
											}
											bool flag30 = flag27;
											string sqlstring;
											if (flag30)
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
											ExamBll.UpdateExamLog(sortInfo, this.userid, current2, flag27);
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
								bool flag31 = this.examresult.exp > 0;
								if (flag31)
								{
									UserBll.UpdateUserExp(this.examresult.uid, this.examresult.exp);
								}
								bool flag32 = this.examresult.getcredits > 0;
								if (flag32)
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
								bool flag33 = this.msg != "";
								if (flag33)
								{
									this.ShowErr(this.msg);
								}
							}
						}
					}
				}
			}
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
				text = "0" + num + "时";
			}
			else
			{
				text = num.ToString() + "时";
			}
			bool flag2 = num2 < 10;
			if (flag2)
			{
				text = text + "0" + num2.ToString() + "分";
			}
			else
			{
				text = text + num2.ToString() + "分";
			}
			bool flag3 = num3 < 10;
			if (flag3)
			{
				text = text + "0" + num3.ToString() + "秒";
			}
			else
			{
				text = text + num3.ToString() + "秒";
			}
			return text;
		}
	}
}
