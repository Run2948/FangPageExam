using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003C RID: 60
	public class sortappadd : SuperController
	{
		// Token: 0x0600008F RID: 143 RVA: 0x0000C314 File Offset: 0x0000A514
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.sortappinfo = DbHelper.ExecuteModel<SortAppInfo>(this.id);
			}
			if (this.ispost)
			{
				this.sortappinfo = FPRequest.GetModel<SortAppInfo>(this.sortappinfo);
				if (this.sortappinfo.guid != "")
				{
					SetupInfo setupInfo = SetupBll.GetSetupInfo(this.sortappinfo.guid);
					this.sortappinfo.type = setupInfo.type;
					this.sortappinfo.installpath = setupInfo.installpath;
				}
				else
				{
					this.sortappinfo.type = "";
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
				FPCache.Remove("FP_SORTAPPLIST");
				base.Response.Redirect("sortappmanage.aspx");
			}
			foreach (SiteConfig siteconfig in SiteConfigs.GetSysSiteList())
			{
				this.setuplist.Add(SetupBll.GetSetupInfo(siteconfig));
			}
			foreach (AppConfig appconfig in AppConfigs.GetMapAppList())
			{
				this.setuplist.Add(SetupBll.GetSetupInfo(appconfig));
			}
		}

		// Token: 0x040000A4 RID: 164
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000A5 RID: 165
		protected SortAppInfo sortappinfo = new SortAppInfo();

		// Token: 0x040000A6 RID: 166
		protected List<SetupInfo> setuplist = new List<SetupInfo>();
	}
}
