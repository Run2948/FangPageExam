using System;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200000D RID: 13
	public class examsearch : AdminController
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000078AC File Offset: 0x00005AAC
		protected override void View()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
		}

		// Token: 0x0400002E RID: 46
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400002F RID: 47
		protected SortInfo sortinfo = new SortInfo();
	}
}
