using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FP_Exam.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class examrepeatpost : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				bool flag = !this.isperm;
				if (flag)
				{
					base.WriteErr("对不起，您没有权限阅卷。");
					return;
				}
				this.examresult = ExamBll.GetExamResult(this.resultid);
				bool flag2 = this.examresult.id == 0;
				if (flag2)
				{
					base.WriteErr("对不起，该考试不存在或已被删除。");
					return;
				}
				List<ExamResultTopic> examResultTopicList = ExamBll.GetExamResultTopicList(this.resultid);
				this.examresult.score = 0.0;
				this.examresult.score1 = 0.0;
				this.examresult.score2 = 0.0;
				this.examresult.wrongs = 0;
				this.examresult.unanswer = 0;
				int num = 0;
				foreach (ExamResultTopic current in examResultTopicList)
				{
					bool flag3 = current.questions == 0;
					if (flag3)
					{
						num++;
					}
					else
					{
						List<ExamQuestion> questionList = QuestionBll.GetQuestionList(current.questionlist);
						string[] array = FPArray.SplitString(current.answerlist, "§", questionList.Count);
						string[] array2 = FPArray.SplitString(current.scorelist, "|", questionList.Count);
						examResultTopicList[num].score = 0.0;
						examResultTopicList[num].wrongs = 0;
						examResultTopicList[num].scorelist = "";
						examResultTopicList[num].correctlist = "";
						int num2 = 0;
						foreach (ExamQuestion current2 in questionList)
						{
							string text = FPRequest.GetString("answer_" + current2.id);
							bool flag4 = examResultTopicList[num].scorelist != "";
							if (flag4)
							{
								ExamResultTopic expr_1E7 = examResultTopicList[num];
								expr_1E7.scorelist += "|";
							}
							int num3 = 0;
							double num4 = 0.0;
							bool flag5 = current2.type == "TYPE_RADIO" || current2.type == "TYPE_MULTIPLE";
							if (flag5)
							{
								num3 = current2.ascount;
								bool flag6 = text == current2.answer && text != "";
								if (flag6)
								{
									num4 = current.perscore;
								}
								this.examresult.score1 += num4;
							}
							else
							{
								bool flag7 = current2.type == "TYPE_TRUE_FALSE";
								if (flag7)
								{
									num3 = current2.ascount;
									bool flag8 = text == current2.answer && text != "";
									if (flag8)
									{
										num4 = current.perscore;
									}
									this.examresult.score1 += num4;
								}
								else
								{
									bool flag9 = current2.type == "TYPE_BLANK";
									if (flag9)
									{
										examResultTopicList[num].answerlist = FPArray.Replace(examResultTopicList[num].answerlist, text, num2, "§");
										bool flag10 = current2.upperflg == 1;
										string[] array3;
										if (flag10)
										{
											array3 = FPArray.SplitString(current2.answer, new string[]
											{
												",",
												"&&"
											});
										}
										else
										{
											array3 = FPArray.SplitString(current2.answer.ToLower(), new string[]
											{
												",",
												"&&"
											});
										}
										text = examrepeatpost.DelSameAnser(text);
										bool flag11 = current2.upperflg == 1;
										string[] array4;
										if (flag11)
										{
											array4 = FPArray.SplitString(text, ",", array3.Length);
										}
										else
										{
											array4 = FPArray.SplitString(text.ToLower(), ",", array3.Length);
										}
										bool flag12 = current2.orderflg == 1;
										if (flag12)
										{
											for (int i = 0; i < array4.Length; i++)
											{
												bool flag13 = FPArray.InArray(array4[i], array3[i], "|") >= 0;
												if (flag13)
												{
													num3++;
												}
											}
										}
										else
										{
											for (int j = 0; j < array4.Length; j++)
											{
												bool flag14 = FPArray.InArray(array4[j], current2.answer.Replace("|", ",")) >= 0;
												if (flag14)
												{
													num3++;
												}
											}
										}
										bool flag15 = num3 > 0;
										if (flag15)
										{
											bool flag16 = current2.ascount <= 0;
											if (flag16)
											{
												current2.ascount = 1;
											}
											num4 = Math.Round(current.perscore / (double)current2.ascount * (double)num3, 2);
											this.examresult.score1 += num4;
										}
									}
									else
									{
										bool flag17 = current2.type == "TYPE_ANSWER" || current2.type == "TYPE_OPERATION";
										if (flag17)
										{
											examResultTopicList[num].answerlist = FPArray.Replace(examResultTopicList[num].answerlist, text, num2, "§");
											num4 = FPUtils.StrToDouble(array2[num2]);
											this.examresult.score2 += num4;
										}
									}
								}
							}
							this.examresult.score += num4;
							examResultTopicList[num].score += num4;
							ExamResultTopic expr_597 = examResultTopicList[num];
							expr_597.scorelist += num4.ToString();
							bool flag18 = examResultTopicList[num].correctlist != "";
							if (flag18)
							{
								ExamResultTopic expr_5D3 = examResultTopicList[num];
								expr_5D3.correctlist += "|";
							}
							bool iswrong = false;
							bool flag19 = num4 >= current.perscore * 0.6;
							if (flag19)
							{
								ExamResultTopic expr_613 = examResultTopicList[num];
								expr_613.correctlist += "1";
							}
							else
							{
								ExamResultTopic expr_634 = examResultTopicList[num];
								expr_634.correctlist += "0";
								examResultTopicList[num].wrongs++;
								this.examresult.wrongs++;
								bool flag20 = text == "";
								if (flag20)
								{
									this.examresult.unanswer++;
								}
								iswrong = true;
							}
							current2.useranswer = text;
							examResultTopicList[num].answerlist = FPArray.Replace(examResultTopicList[num].answerlist, text, num2, "§");
							SortInfo sortInfo = SortBll.GetSortInfo(current2.sortid);
							ExamBll.UpdateExamLog(sortInfo, this.userid, current2, iswrong);
							num2++;
						}
						DbHelper.ExecuteUpdate<ExamResultTopic>(examResultTopicList[num]);
						num++;
					}
				}
				string sqlstring = string.Format("UPDATE [{0}Exam_ExamResult] SET [score1]={1},[score2]={2},[score]={3},[wrongs]={4},[exp]={5},[exnote]='{6}' WHERE [id]={7}", new object[]
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
				DbHelper.ExecuteSql(sqlstring);
				base.WriteSuccess("阅卷提交成功!");
			}
			base.WriteErr("没有提交数据。");
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

		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			FPResponse.WriteJson(hashtable);
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
	}
}
