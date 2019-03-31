using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Exam.Model;
using System;

namespace FP_Exam.Controller
{
	public class answer : LoginController
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
					ExamQuestion examQuestion = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
					int is_true = 0;
					bool flag3 = examQuestion.type == "TYPE_RADIO" || examQuestion.type == "TYPE_MULTIPLE";
					if (flag3)
					{
						int ascount = examQuestion.ascount;
						bool flag4 = this.useranswer == examQuestion.answer && this.useranswer != "";
						if (flag4)
						{
							is_true = 1;
						}
					}
					else
					{
						bool flag5 = examQuestion.type == "TYPE_TRUE_FALSE";
						if (flag5)
						{
							int ascount2 = examQuestion.ascount;
							bool flag6 = this.useranswer == examQuestion.answer && this.useranswer != "";
							if (flag6)
							{
								is_true = 1;
							}
							bool flag7 = examQuestion.answer == "Y";
							if (flag7)
							{
								examQuestion.answer = "正确";
							}
							else
							{
								examQuestion.answer = "错误";
							}
						}
					}
					var obj = new
					{
						errcode = 0,
						errmsg = "",
						is_true = is_true,
						answer = examQuestion.answer,
						explain = examQuestion.explain
					};
					FPResponse.WriteJson(obj);
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
	}
}
