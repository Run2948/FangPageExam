using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;
using LitJson;

namespace FP_Exam.Controller
{
	// Token: 0x02000028 RID: 40
	public class examreadpost : LoginController
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00011E2C File Offset: 0x0001002C
		protected override void View()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErrMsg("对不起，您没有权限阅卷。");
					return;
				}
				this.examresult = ExamBll.GetExamResult(this.resultid);
				if (this.examresult.id == 0)
				{
					this.ShowErrMsg("对不起，该考试不存在或已被删除。");
					return;
				}
				int status = this.examresult.status;
				this.examresult.status = 2;
				this.examresult.exnote = FPRequest.GetString("exnote");
				if (DbHelper.ExecuteUpdate<ExamResult>(this.examresult) <= 0)
				{
					this.ShowErrMsg("保存出现错误。");
					return;
				}
				List<ExamResultTopic> examResultTopicList = ExamBll.GetExamResultTopicList(this.resultid);
				int num = 0;
				double score = this.examresult.score;
				this.examresult.score = 0.0;
				this.examresult.score1 = 0.0;
				this.examresult.score2 = 0.0;
				this.examresult.wrongs = 0;
				foreach (ExamResultTopic examResultTopic in examResultTopicList)
				{
					if (examResultTopic.questions == 0)
					{
						num++;
					}
					else
					{
						int[] array = FPUtils.SplitInt(examResultTopic.questionlist);
						string[] array2 = FPUtils.SplitString(examResultTopic.answerlist, "§", array.Length);
						string[] array3 = FPUtils.SplitString(examResultTopic.scorelist, "|", array.Length);
						examResultTopicList[num].scorelist = "";
						examResultTopicList[num].correctlist = "";
						examResultTopicList[num].score = 0.0;
						int num2 = 0;
						foreach (ExamQuestion examQuestion in QuestionBll.GetQuestionList(examResultTopic.questionlist))
						{
							string a = array2[num2];
							if (examResultTopicList[num].scorelist != "")
							{
								ExamResultTopic examResultTopic2 = examResultTopicList[num];
								examResultTopic2.scorelist += "|";
							}
							double num3 = 0.0;
							if (examQuestion.type == 1 || examQuestion.type == 2)
							{
								num3 = (double)FPRequest.GetFloat("score_" + examQuestion.id);
								this.examresult.score1 += num3;
							}
							else if (examQuestion.type == 3)
							{
								num3 = (double)FPRequest.GetFloat("score_" + examQuestion.id);
								this.examresult.score1 += num3;
							}
							else if (examQuestion.type == 4)
							{
								num3 = (double)FPRequest.GetFloat("score_" + examQuestion.id);
								this.examresult.score1 += num3;
							}
							else if (examQuestion.type == 5)
							{
								num3 = (double)FPRequest.GetFloat("score_" + examQuestion.id);
								this.examresult.score2 += num3;
							}
							else if (examQuestion.type == 6)
							{
								num3 = (double)FPRequest.GetFloat("score_" + examQuestion.id);
								this.examresult.score2 += num3;
							}
							this.examresult.score += num3;
							examResultTopicList[num].score += num3;
							ExamResultTopic examResultTopic3 = examResultTopicList[num];
							examResultTopic3.scorelist += num3.ToString();
							if (examResultTopicList[num].correctlist != "")
							{
								ExamResultTopic examResultTopic4 = examResultTopicList[num];
								examResultTopic4.correctlist += "|";
							}
							bool iswrong = false;
							if (num3 >= examResultTopic.perscore * 0.6)
							{
								ExamResultTopic examResultTopic5 = examResultTopicList[num];
								examResultTopic5.correctlist += "1";
							}
							else
							{
								ExamResultTopic examResultTopic6 = examResultTopicList[num];
								examResultTopic6.correctlist += "0";
								examResultTopicList[num].wrongs++;
								this.examresult.wrongs++;
								if (a == "")
								{
									this.examresult.unanswer++;
								}
								iswrong = true;
							}
							SortInfo sortInfo = SortBll.GetSortInfo(examQuestion.sortid);
							ExamBll.UpdateExamLog(sortInfo, this.examresult.uid, examQuestion, iswrong);
							num2++;
						}
						DbHelper.ExecuteUpdate<ExamResultTopic>(examResultTopicList[num]);
						num++;
					}
				}
				ExpInfo examExpByScore = ExamBll.GetExamExpByScore(this.examresult.score, this.examresult.examid);
				StringBuilder stringBuilder = new StringBuilder();
				if (status == 0)
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
				if (this.msg != "")
				{
					this.ShowErrMsg(this.msg);
					return;
				}
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["message"] = "";
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000126C8 File Offset: 0x000108C8
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

		// Token: 0x060000BA RID: 186 RVA: 0x0001273C File Offset: 0x0001093C
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000127A4 File Offset: 0x000109A4
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

		// Token: 0x040000CD RID: 205
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x040000CE RID: 206
		protected ExamResult examresult = new ExamResult();
	}
}
