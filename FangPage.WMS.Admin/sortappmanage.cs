using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000030 RID: 48
	public class sortappmanage : SuperController
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("chkid");
				DbHelper.ExecuteDelete<SortAppInfo>(@string);
				CacheBll.RemoveSortCache();
			}
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC);
		}

		// Token: 0x0400006F RID: 111
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();
	}
}
