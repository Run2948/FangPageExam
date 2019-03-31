using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000013 RID: 19
	public class siteview : AdminController
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000044C4 File Offset: 0x000026C4
		protected override void Controller()
		{
			if (this.m_sitepath != "")
			{
				this.siteinfo = SiteConfigs.GetMapSiteConfig(this.m_sitepath);
			}
			if (this.siteinfo.sitepath == "")
			{
				this.ShowErr("对不起，该站点已被删除或不存在。");
				return;
			}
			string mapPath = FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath);
			this.siteinfo.size = FPFile.FormatBytesStr(FPFile.GetDirSize(mapPath));
		}

		// Token: 0x0400002C RID: 44
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x0400002D RID: 45
		protected new SiteConfig siteinfo = new SiteConfig();
	}
}
