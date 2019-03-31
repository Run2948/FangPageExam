using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Exam.Model;

namespace FP_Exam.Wap.WapController
{
	// Token: 0x0200000A RID: 10
	public class makenext : LoginController
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000033C4 File Offset: 0x000015C4
		protected override void Controller()
		{
			ExamResultTopic examResultTopic = DbHelper.ExecuteModel<ExamResultTopic>(this.topicid);
			bool flag = examResultTopic.id == 0;
			if (flag)
			{
				base.WriteErr("对不起，该考题已被删除或不存在。");
			}
			else
			{
				int num = FPArray.InArray(this.qid, examResultTopic.questionlist);
				bool flag2 = num == -1;
				if (flag2)
				{
					base.WriteErr("对不起，该试题不在该考试中。");
				}
				else
				{
					bool flag3 = examResultTopic.answerlist == "";
					if (flag3)
					{
						for (int i = 0; i < FPArray.SplitInt(examResultTopic.questionlist).Length - 1; i++)
						{
							ExamResultTopic examResultTopic2 = examResultTopic;
							examResultTopic2.answerlist += "§";
						}
					}
					string @string = FPArray.GetString(examResultTopic.answerlist, num, "§");
					examResultTopic.answerlist = FPArray.Replace(examResultTopic.answerlist, this.useranswer, num, "§");
					SqlParam[] array = new SqlParam[]
					{
						DbHelper.MakeUpdate("answerlist", examResultTopic.answerlist),
						DbHelper.MakeAndWhere("id", examResultTopic.id)
					};
					DbHelper.ExecuteUpdate<ExamResultTopic>(array);
					ExamQuestion examQuestion = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
					bool flag4 = examQuestion.id == 0;
					if (flag4)
					{
						base.WriteErr("对不起，题库中不存在该题目或已被删除。");
					}
					else
					{
						bool flag5 = examQuestion.answer.ToLower() == this.useranswer.ToLower();
						if (flag5)
						{
							var o = new
							{
								errcode = 0,
								errmsg = ""
							};
							FPResponse.WriteJson(o);
						}
						else
						{
							bool flag6 = @string == "";
							if (flag6)
							{
								var o2 = new
								{
									errcode = 5,
									errmsg = examQuestion.answer
								};
								FPResponse.WriteJson(o2);
							}
							else
							{
								var o3 = new
								{
									errcode = 6,
									errmsg = examQuestion.answer
								};
								FPResponse.WriteJson(o3);
							}
						}
					}
				}
			}
		}

		// Token: 0x0400001C RID: 28
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x0400001D RID: 29
		protected int topicid = FPRequest.GetInt("topicid");

		// Token: 0x0400001E RID: 30
		protected string useranswer = FPRequest.GetString("useranswer");

		// Token: 0x0400001F RID: 31
		protected int ismarker = FPRequest.GetInt("ismarker");
	}
}
