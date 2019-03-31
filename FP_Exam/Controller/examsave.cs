using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class examsave : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				this.examresult = ExamBll.GetExamResult(this.resultid);
				bool flag = this.examresult.id == 0;
				if (flag)
				{
					this.ShowErrMsg("对不起，该考试不存在或已被删除。");
					return;
				}
				bool flag2 = this.examresult.uid != this.userid;
				if (flag2)
				{
					this.ShowErrMsg("对不起，您不是该考试的主人。");
					return;
				}
				bool flag3 = this.examresult.status == 1;
				if (flag3)
				{
					this.ShowErrMsg("对不起，该考试已完成，不能重复提交。");
					return;
				}
				this.examresult.utime = FPRequest.GetInt("utime");
				bool flag4 = DbHelper.ExecuteUpdate<ExamResult>(this.examresult) > 0;
				if (!flag4)
				{
					this.ShowErrMsg("保存出现错误。");
					return;
				}
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
						bool flag6 = examResultTopicList[num].answerlist == "";
						if (flag6)
						{
							for (int i = 0; i < FPArray.SplitInt(current.questionlist).Length - 1; i++)
							{
								ExamResultTopic expr_137 = examResultTopicList[num];
								expr_137.answerlist += "§";
							}
						}
						int num2 = 0;
						foreach (ExamQuestion current2 in QuestionBll.GetQuestionList(current.questionlist))
						{
							string @string = FPRequest.GetString("answer_" + current2.id);
							string string2 = FPArray.GetString(examResultTopicList[num].answerlist, num2, "§");
							bool flag7 = @string != string2 && @string != "";
							if (flag7)
							{
								examResultTopicList[num].answerlist = FPArray.Replace(examResultTopicList[num].answerlist, @string, num2, "§");
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
			FPResponse.WriteJson(hashtable);
		}

		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			FPResponse.WriteJson(hashtable);
		}
	}
}
