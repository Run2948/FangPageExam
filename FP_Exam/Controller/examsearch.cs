using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;

namespace FP_Exam.Controller
{
	public class examsearch : AdminController
	{
		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected override void Controller()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
		}
	}
}
