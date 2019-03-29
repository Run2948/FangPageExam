using System;
using System.Collections;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.Exam.Model;
using LitJson;

namespace FangPage.Exam.Controller
{
	// Token: 0x0200002B RID: 43
	public class examsave : LoginController
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000133F0 File Offset: 0x000115F0
		protected override void View()
		{
			if (this.ispost)
			{
				this.examresult = ExamBll.GetExamResult(this.resultid);
				if (this.examresult.id == 0)
				{
					this.ShowErrMsg("对不起，该考试不存在或已被删除。");
					return;
				}
				if (this.examresult.uid != this.userid)
				{
					this.ShowErrMsg("对不起，您不是该考试的主人。");
					return;
				}
				if (this.examresult.status == 1)
				{
					this.ShowErrMsg("对不起，该考试已完成，不能重复提交。");
					return;
				}
				this.examresult.utime = FPRequest.GetInt("utime");
				if (DbHelper.ExecuteUpdate<ExamResult>(this.examresult) <= 0)
				{
					this.ShowErrMsg("保存出现错误。");
					return;
				}
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
						int num2 = 0;
						foreach (ExamQuestion examQuestion in QuestionBll.GetQuestionList(examResultTopic.questionlist))
						{
							string @string = FPRequest.GetString("answer_" + examQuestion.id);
							if (num2 == 0)
							{
								ExamResultTopic examResultTopic2 = examResultTopicList[num];
								examResultTopic2.answerlist += @string;
							}
							else
							{
								ExamResultTopic examResultTopic3 = examResultTopicList[num];
								examResultTopic3.answerlist = examResultTopic3.answerlist + "§" + @string;
							}
							num2++;
						}
						DbHelper.ExecuteUpdate<ExamResultTopic>(examResultTopicList[num]);
						num++;
					}
				}
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["message"] = "";
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000136B0 File Offset: 0x000118B0
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x040000E4 RID: 228
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x040000E5 RID: 229
		protected ExamResult examresult = new ExamResult();
	}
}
