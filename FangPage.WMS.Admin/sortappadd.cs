using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002F RID: 47
	public class sortappadd : SuperController
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000A7CC File Offset: 0x000089CC
		protected override void View()
		{
			if (this.id > 0)
			{
				this.sortappinfo = DbHelper.ExecuteModel<SortAppInfo>(this.id);
			}
			if (this.ispost)
			{
				this.sortappinfo = FPRequest.GetModel<SortAppInfo>(this.sortappinfo);
				if (this.sortappinfo.appid > 0)
				{
					AppInfo appInfo = DbHelper.ExecuteModel<AppInfo>(this.sortappinfo.appid);
					this.sortappinfo.installpath = appInfo.installpath;
				}
				else
				{
					this.sortappinfo.installpath = "";
				}
				if (this.sortappinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<SortAppInfo>(this.sortappinfo);
				}
				else
				{
					DbHelper.ExecuteInsert<SortAppInfo>(this.sortappinfo);
				}
				CacheBll.RemoveSortCache();
				base.Response.Redirect("sortappmanage.aspx");
			}
			this.applist = DbHelper.ExecuteList<AppInfo>(OrderBy.ASC);
		}

		// Token: 0x0400006C RID: 108
		protected int id = FPRequest.GetInt("id");

		// Token: 0x0400006D RID: 109
		protected SortAppInfo sortappinfo = new SortAppInfo();

		// Token: 0x0400006E RID: 110
		protected List<AppInfo> applist = new List<AppInfo>();
	}
}
