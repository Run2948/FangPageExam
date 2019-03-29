using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200000D RID: 13
	public class BaseController : WMSController
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00003D28 File Offset: 0x00001F28
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.sortid > 0)
			{
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
			}
			this.parentid = this.sortinfo.parentid;
		}

		// Token: 0x0400001D RID: 29
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400001E RID: 30
		protected int parentid = 0;

		// Token: 0x0400001F RID: 31
		protected SortInfo sortinfo = new SortInfo();
	}
}
