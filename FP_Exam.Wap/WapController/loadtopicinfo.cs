using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Exam.Model;

namespace FP_Exam.Wap.WapController
{
	// Token: 0x02000009 RID: 9
	public class loadtopicinfo : LoginController
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000031DC File Offset: 0x000013DC
		protected override void Controller()
		{
			ExamResultTopic examResultTopic = DbHelper.ExecuteModel<ExamResultTopic>(this.topicid);
			int[] array = FPArray.SplitInt(examResultTopic.questionlist);
			string[] array2 = FPArray.SplitString(examResultTopic.answerlist, "§", array.Length);
			string[] array3 = FPArray.SplitString(examResultTopic.scorelist, "|", array.Length);
			this.questioninfo = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
			bool flag = this.questioninfo.id == 0;
			if (flag)
			{
				base.WriteErr("该题在题库中不存在或已被删除。");
			}
			int num = 0;
			foreach (int num2 in array)
			{
				bool flag2 = num2 == this.questioninfo.id;
				if (flag2)
				{
					this.questioninfo.useranswer = array2[num];
					this.questioninfo.userscore = (double)FPUtils.StrToFloat(array3[num]);
				}
				num++;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000032C4 File Offset: 0x000014C4
		protected string FmAnswer(string content)
		{
			string[] array = FPArray.SplitString(content, "(#answer)");
			content = "";
			int num = 0;
			foreach (string str in array)
			{
				bool flag = num < array.Length - 1;
				if (flag)
				{
					content = content + str + "_____";
				}
				else
				{
					content += str;
				}
				num++;
			}
			return content;
		}

		// Token: 0x04000018 RID: 24
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x04000019 RID: 25
		protected int topicid = FPRequest.GetInt("topicid");

		// Token: 0x0400001A RID: 26
		protected ExamQuestion questioninfo = new ExamQuestion();

		// Token: 0x0400001B RID: 27
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H"
		};
	}
}
