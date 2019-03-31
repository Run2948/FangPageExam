using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.Controller
{
	public class examanswer : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamConfig examconfig = new ExamConfig();

		protected ExamResult examresult = new ExamResult();

		protected List<ExamResultTopic> examtopiclist = new List<ExamResultTopic>();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			bool flag = this.examconfig.teststatus == 0;
			if (flag)
			{
				this.ShowErr("对不起，系统已关闭了用户练习，不能再查看答案。");
			}
			else
			{
				this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
				this.examtopiclist = ExamBll.GetExamResultTopicList(this.resultid);
			}
		}

		protected List<ExamQuestion> GetQuestionList(ExamResultTopic resultinfo)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, resultinfo.questionlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPArray.SplitInt(resultinfo.questionlist);
			string[] array2 = FPArray.SplitString(resultinfo.answerlist, "§", array.Length);
			string[] array3 = FPArray.SplitString(resultinfo.scorelist, "|", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int num = 0;
			int[] array4 = array;
			for (int i = 0; i < array4.Length; i++)
			{
				int num2 = array4[i];
				foreach (ExamQuestion current in list)
				{
					bool flag = current.id == num2;
					if (flag)
					{
						current.useranswer = array2[num];
						current.userscore = (double)FPUtils.StrToFloat(array3[num]);
						list2.Add(current);
					}
				}
				num++;
			}
			return list2;
		}
	}
}
