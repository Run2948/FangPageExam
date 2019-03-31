using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class examhistory : LoginController
	{
		protected List<ExamResult> examresultlist = new List<ExamResult>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected override void Controller()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("uid", this.userid);
			this.examresultlist = DbHelper.ExecuteList<ExamResult>(this.pager, new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
