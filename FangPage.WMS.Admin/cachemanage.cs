using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000A RID: 10
	public class cachemanage : SuperController
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000031C4 File Offset: 0x000013C4
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("cache");
				foreach (string a in @string.Split(new char[]
				{
					','
				}))
				{
					if (a == "sysconfig")
					{
						this.sysconfig.passwordkey = WMSUtils.CreateAuthStr(10);
						SysConfigs.SaveConfig(this.sysconfig);
						WMSCookie.WriteCookie("password", DES.Encode(this.user.password, this.sysconfig.passwordkey));
						SysConfigs.ResetConfig();
					}
					if (a == "syssort")
					{
						CacheBll.RemoveSortCache();
					}
					if (a == "attachtype")
					{
						FPCache.Remove("FP_ATTACHTYPE", "image,flash,media,file");
					}
					if (a == "siteconfig")
					{
						List<SiteConfig> siteList = SiteBll.GetSiteList();
						foreach (SiteConfig siteConfig in siteList)
						{
							FPCache.Remove("FP_SITECONFIG_" + siteConfig.sitepath);
						}
					}
				}
				base.Response.Redirect("cachemanage.aspx");
			}
			base.SaveRightURL();
		}
	}
}
