using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Exam.Model;
using System;

namespace FP_Exam.Controller
{
	public class answer_save : LoginController
	{
		protected string qid = FPRequest.GetString("qid");

		protected int topicid = FPRequest.GetInt("topicid");

		protected string useranswer = FPRequest.GetString("useranswer");

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
							ExamResultTopic expr_7A = examResultTopic;
							expr_7A.answerlist += "§";
						}
					}
					examResultTopic.answerlist = FPArray.Replace(examResultTopic.answerlist, this.useranswer, num, "§");
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeUpdate("answerlist", examResultTopic.answerlist),
						DbHelper.MakeAndWhere("id", examResultTopic.id)
					};
					DbHelper.ExecuteUpdate<ExamResultTopic>(sqlparams);
					var obj = new
					{
						errcode = 0,
						errmsg = string.Concat(new object[]
						{
							this.topicid,
							"|",
							this.qid,
							"|",
							this.useranswer
						})
					};
					FPResponse.WriteJson(obj);
				}
			}
		}
	}
}
