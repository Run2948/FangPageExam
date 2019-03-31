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
using System.Text;

namespace FP_Exam.Controller
{
	public class examreadpost : LoginController
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
				int status = this.examresult.status;
				this.examresult.status = 2;
				this.examresult.exnote = FPRequest.GetString("exnote");
				bool flag3 = DbHelper.ExecuteUpdate<ExamResult>(this.examresult) > 0;
				if (!flag3)
				{
					base.WriteErr("保存出现错误。");
					return;
				}
				List<ExamResultTopic> examResultTopicList = ExamBll.GetExamResultTopicList(this.resultid);
				int num = 0;
				double score = this.examresult.score;
				this.examresult.score = 0.0;
				this.examresult.score1 = 0.0;
				this.examresult.score2 = 0.0;
				this.examresult.wrongs = 0;
				foreach (ExamResultTopic current in examResultTopicList)
				{
					bool flag4 = current.questions == 0;
					if (flag4)
					{
						num++;
					}
					else
					{
						int[] array = FPArray.SplitInt(current.questionlist);
						string[] array2 = FPArray.SplitString(current.answerlist, "§", array.Length);
						string[] array3 = FPArray.SplitString(current.scorelist, "|", array.Length);
						examResultTopicList[num].scorelist = "";
						examResultTopicList[num].correctlist = "";
						examResultTopicList[num].score = 0.0;
						int num2 = 0;
						foreach (ExamQuestion current2 in QuestionBll.GetQuestionList(current.questionlist))
						{
							string a = array2[num2];
							bool flag5 = examResultTopicList[num].scorelist != "";
							if (flag5)
							{
								ExamResultTopic expr_219 = examResultTopicList[num];
								expr_219.scorelist += "|";
							}
							double num3 = 0.0;
							bool flag6 = current2.type == "TYPE_RADIO" || current2.type == "TYPE_MULTIPLE";
							if (flag6)
							{
								num3 = FPRequest.GetDouble("score_" + current2.id);
								this.examresult.score1 += num3;
							}
							else
							{
								bool flag7 = current2.type == "TYPE_TRUE_FALSE";
								if (flag7)
								{
									num3 = FPRequest.GetDouble("score_" + current2.id);
									this.examresult.score1 += num3;
								}
								else
								{
									bool flag8 = current2.type == "TYPE_BLANK";
									if (flag8)
									{
										num3 = FPRequest.GetDouble("score_" + current2.id);
										this.examresult.score1 += num3;
									}
									else
									{
										bool flag9 = current2.type == "TYPE_ANSWER" || current2.type == "TYPE_OPERATION";
										if (flag9)
										{
											num3 = FPRequest.GetDouble("score_" + current2.id);
											this.examresult.score2 += num3;
										}
									}
								}
							}
							this.examresult.score += num3;
							examResultTopicList[num].score += num3;
							ExamResultTopic expr_3D5 = examResultTopicList[num];
							expr_3D5.scorelist += num3.ToString();
							bool flag10 = examResultTopicList[num].correctlist != "";
							if (flag10)
							{
								ExamResultTopic expr_415 = examResultTopicList[num];
								expr_415.correctlist += "|";
							}
							bool iswrong = false;
							bool flag11 = num3 >= current.perscore * 0.6;
							if (flag11)
							{
								ExamResultTopic expr_457 = examResultTopicList[num];
								expr_457.correctlist += "1";
							}
							else
							{
								ExamResultTopic expr_47A = examResultTopicList[num];
								expr_47A.correctlist += "0";
								examResultTopicList[num].wrongs++;
								this.examresult.wrongs++;
								bool flag12 = a == "";
								if (flag12)
								{
									this.examresult.unanswer++;
								}
								iswrong = true;
							}
							SortInfo sortInfo = SortBll.GetSortInfo(current2.sortid);
							ExamBll.UpdateExamLog(sortInfo, this.examresult.uid, current2, iswrong);
							num2++;
						}
						DbHelper.ExecuteUpdate<ExamResultTopic>(examResultTopicList[num]);
						num++;
					}
				}
				ExpInfo examExpByScore = ExamBll.GetExamExpByScore(this.examresult.score, this.examresult.examid);
				StringBuilder stringBuilder = new StringBuilder();
				bool flag13 = status == 0;
				if (flag13)
				{
					stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [score1]={1},[score2]={2},[score]={3},[wrongs]={4},[exp]={5} WHERE [id]={6}|", new object[]
					{
						DbConfigs.Prefix,
						this.examresult.score1,
						this.examresult.score2,
						this.examresult.score,
						this.examresult.wrongs,
						examExpByScore.exp,
						this.examresult.id
					});
					stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [exams]=[exams]+1,[score]=[score]+{1} WHERE [id]={2}", DbConfigs.Prefix, this.examresult.score, this.examresult.examid);
				}
				else
				{
					stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [score1]={1},[score2]={2},[score]={3},[exp]={4} WHERE [id]={5}|", new object[]
					{
						DbConfigs.Prefix,
						this.examresult.score1,
						this.examresult.score2,
						this.examresult.score,
						examExpByScore.exp,
						this.examresult.id
					});
					stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [score]=[score]-{1} WHERE [id]={2}|", DbConfigs.Prefix, score, this.examresult.examid);
					stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [score]=[score]+{1} WHERE [id]={2}", DbConfigs.Prefix, this.examresult.score, this.examresult.examid);
					stringBuilder.AppendFormat("UPDATE [{0}WMS_UserInfo] SET [exp]=[exp]-{1} WHERE [id]={2}", DbConfigs.Prefix, this.examresult.exp, this.examresult.uid);
				}
				this.msg = DbHelper.ExecuteSql(stringBuilder.ToString());
				UserBll.UpdateUserExp(this.examresult.uid, examExpByScore.exp);
				bool flag14 = this.msg != "";
				if (flag14)
				{
					base.WriteSuccess("阅卷提交失败：" + this.msg);
				}
				else
				{
					base.WriteSuccess("阅卷提交成功!");
				}
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
