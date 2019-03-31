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
	public class testpost : LoginController
	{
		protected string qidlist = FPRequest.GetString("qidlist");

		protected ExamResult examresult = new ExamResult();

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			bool flag = this.channelinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				bool ispost = this.ispost;
				if (ispost)
				{
					bool flag2 = this.qidlist == "";
					if (flag2)
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
						this.examresult.examname = "专项智能练习";
						this.examresult.examtime = FPRequest.GetInt("utime") / 60;
						this.examresult.total = 100.0;
						this.examresult.passmark = 60.0;
						this.examresult.credits = 0;
						this.examresult.questions = questionList.Count;
						this.examresult.utime = FPRequest.GetInt("utime");
						this.examresult.starttime = FPRequest.GetDateTime("starttime");
						this.examresult.endtime = DbUtils.GetDateTime();
						this.examresult.status = 1;
						DbHelper.ExecuteInsert<ExamResult>(this.examresult);
						bool flag3 = this.examresult.id > 0;
						if (flag3)
						{
							ExamResultTopic examResultTopic = new ExamResultTopic();
							examResultTopic.resultid = this.examresult.id;
							examResultTopic.type = "";
							examResultTopic.title = "自由练习";
							examResultTopic.perscore = Math.Round(this.examresult.total / (double)this.examresult.questions, 2);
							examResultTopic.display = 1;
							examResultTopic.questions = questionList.Count;
							examResultTopic.questionlist = this.qidlist;
							int num = 0;
							foreach (ExamQuestion current in QuestionBll.GetQuestionList(examResultTopic.questionlist))
							{
								string text = FPRequest.GetString("answer_" + current.id);
								bool flag4 = num == 0;
								if (flag4)
								{
									ExamResultTopic expr_283 = examResultTopic;
									expr_283.answerlist += text;
								}
								else
								{
									ExamResultTopic expr_29C = examResultTopic;
									expr_29C.answerlist = expr_29C.answerlist + "§" + text;
								}
								bool flag5 = examResultTopic.scorelist != "";
								if (flag5)
								{
									ExamResultTopic expr_2CF = examResultTopic;
									expr_2CF.scorelist += "|";
								}
								int num2 = 0;
								double num3 = 0.0;
								bool flag6 = current.type == "TYPE_RADIO" || current.type == "TYPE_MULTIPLE" || current.type == "TYPE_TRUE_FALSE";
								if (flag6)
								{
									bool flag7 = text == current.answer && text != "";
									if (flag7)
									{
										num2 = current.ascount;
										num3 = examResultTopic.perscore;
										this.examresult.score1 += num3;
									}
								}
								else
								{
									bool flag8 = current.type == "TYPE_BLANK";
									if (flag8)
									{
										bool flag9 = current.upperflg == 1;
										string[] array;
										if (flag9)
										{
											array = FPArray.SplitString(current.answer, ",");
										}
										else
										{
											array = FPArray.SplitString(current.answer.ToLower(), ",");
										}
										text = testpost.DelSameAnser(text);
										bool flag10 = current.upperflg == 1;
										string[] array2;
										if (flag10)
										{
											array2 = FPArray.SplitString(text, ",", array.Length);
										}
										else
										{
											array2 = FPArray.SplitString(text.ToLower(), ",", array.Length);
										}
										bool flag11 = current.orderflg == 1;
										if (flag11)
										{
											for (int i = 0; i < array2.Length; i++)
											{
												bool flag12 = FPArray.InArray(array2[i], array[i], "|") >= 0;
												if (flag12)
												{
													num2++;
												}
											}
										}
										else
										{
											for (int j = 0; j < array2.Length; j++)
											{
												bool flag13 = FPArray.InArray(array2[j], current.answer.Replace("|", ",")) >= 0;
												if (flag13)
												{
													num2++;
												}
											}
										}
										bool flag14 = num2 > 0;
										if (flag14)
										{
											bool flag15 = current.ascount <= 0;
											if (flag15)
											{
												current.ascount = 1;
											}
											num3 = Math.Round(examResultTopic.perscore / (double)current.ascount * (double)num2, 2);
											this.examresult.score2 += num3;
										}
									}
									else
									{
										bool flag16 = current.type == "TYPE_ANSWER";
										if (flag16)
										{
											current.ascount = 0;
											string[] array3 = FPArray.SplitString(current.answerkey);
											for (int k = 0; k < array3.Length; k++)
											{
												string value = array3[k];
												bool flag17 = text.IndexOf(value) >= 0;
												if (flag17)
												{
													num2++;
												}
												current.ascount++;
											}
											bool flag18 = num2 > 0 && text != "";
											if (flag18)
											{
												bool flag19 = current.ascount <= 0;
												if (flag19)
												{
													current.ascount = 1;
												}
												num3 = Math.Round(examResultTopic.perscore / (double)current.ascount * (double)num2, 2);
												this.examresult.score2 += num3;
											}
										}
									}
								}
								this.examresult.score += num3;
								examResultTopic.score += num3;
								ExamResultTopic expr_647 = examResultTopic;
								expr_647.scorelist += num3.ToString();
								bool flag20 = examResultTopic.correctlist != "";
								if (flag20)
								{
									ExamResultTopic expr_679 = examResultTopic;
									expr_679.correctlist += "|";
								}
								bool flag21 = false;
								bool flag22 = num3 >= examResultTopic.perscore * 0.6;
								if (flag22)
								{
									ExamResultTopic expr_6B4 = examResultTopic;
									expr_6B4.correctlist += "1";
								}
								else
								{
									ExamResultTopic expr_6D0 = examResultTopic;
									expr_6D0.correctlist += "0";
									examResultTopic.wrongs++;
									this.examresult.wrongs++;
									bool flag23 = text == "";
									if (flag23)
									{
										this.examresult.unanswer++;
									}
									flag21 = true;
								}
								bool flag24 = flag21;
								string sqlstring;
								if (flag24)
								{
									sqlstring = string.Format("UPDATE [{0}Exam_ExamQuestion] SET [exams]=[exams]+1,[wrongs]=[wrongs]+1 WHERE [id]={1}", DbConfigs.Prefix, current.id);
								}
								else
								{
									sqlstring = string.Format("UPDATE [{0}Exam_ExamQuestion] SET [exams]=[exams]+1 WHERE [id]={1}", DbConfigs.Prefix, current.id);
								}
								DbHelper.ExecuteSql(sqlstring);
								current.useranswer = text;
								SortInfo sortInfo = SortBll.GetSortInfo(current.sortid);
								ExamBll.UpdateExamLog(sortInfo, this.userid, current, flag21);
								num++;
							}
							DbHelper.ExecuteInsert<ExamResultTopic>(examResultTopic);
							SqlParam[] sqlparams = new SqlParam[]
							{
								DbHelper.MakeUpdate("score1", this.examresult.score1),
								DbHelper.MakeUpdate("score2", this.examresult.score2),
								DbHelper.MakeUpdate("score", this.examresult.score),
								DbHelper.MakeUpdate("wrongs", this.examresult.wrongs),
								DbHelper.MakeAndWhere("id", this.examresult.id)
							};
							DbHelper.ExecuteUpdate<ExamResult>(sqlparams);
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

		protected string OptionInt(int ascount)
		{
			string text = "";
			for (int i = 0; i < ascount; i++)
			{
				bool flag = text != "";
				if (flag)
				{
					text += ",";
				}
				text += i.ToString();
			}
			return text;
		}
	}
}
