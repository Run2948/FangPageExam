using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003D RID: 61
	public class sortappmanage : SuperController
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000C4D6 File Offset: 0x0000A6D6
		protected override void Controller()
		{
			if (this.ispost)
			{
				DbHelper.ExecuteDelete<SortAppInfo>(FPRequest.GetString("chkid"));
				CacheBll.RemoveSortCache();
				FPCache.Remove("FP_SORTAPPLIST");
			}
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC);
		}

		// Token: 0x040000A7 RID: 167
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();
	}
}
