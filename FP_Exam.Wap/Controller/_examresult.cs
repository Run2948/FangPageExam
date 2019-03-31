using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.Controller
{
	public class _examresult : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected List<ExamResultTopic> examtopiclist = new List<ExamResultTopic>();

		protected double maxscore = 0.0;

		protected double avgscore = 0.0;

		protected int testers = 0;

		protected int display = 0;

		protected List<string> videoimg = new List<string>();

		protected override void Controller()
		{
			this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
			bool flag = this.examresult.id == 0;
			if (flag)
			{
				this.ShowErr("该考生的试卷不存在或已被删除。");
			}
			else
			{
				bool flag2 = this.examresult.status == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该考试尚未完成。");
				}
				else
				{
					this.examtopiclist = ExamBll.GetExamResultTopicList(this.resultid);
				}
			}
		}

		protected string CalRate(double myscore, double total)
		{
			return (myscore / total * 100.0).ToString("0.0");
		}
	}
}
