using System;
using System.IO;

namespace FangPage.MVC
{
	// Token: 0x0200000B RID: 11
	public class SiteConfigs
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000038E8 File Offset: 0x00001AE8
		public static SiteConfig GetSiteInfo(string sitepath)
		{
			SiteConfig siteConfig = FPCache.Get<SiteConfig>("FP_SITECONFIG_" + sitepath);
			lock (SiteConfigs.lockHelper)
			{
				if (siteConfig == null)
				{
					string mapPath;
					if (sitepath == "")
					{
						mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "site.config");
					}
					else if (File.Exists(FPUtils.GetMapPath(WebConfig.WebPath + "sites/" + sitepath + "/site.config")))
					{
						mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "sites/" + sitepath + "/site.config");
					}
					else if (File.Exists(FPUtils.GetMapPath(WebConfig.WebPath + sitepath + "/site.config")))
					{
						mapPath = FPUtils.GetMapPath(WebConfig.WebPath + sitepath + "/site.config");
					}
					else
					{
						mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "site.config");
					}
					if (!File.Exists(mapPath))
					{
						FPSerializer.Save<SiteConfig>(mapPath);
					}
					FPCache.Insert("FP_SITECONFIG_" + sitepath, SiteConfigs.LoadConfig(mapPath), mapPath);
					siteConfig = FPCache.Get<SiteConfig>("FP_SITECONFIG_" + sitepath);
				}
			}
			siteConfig.sitepath = sitepath;
			return siteConfig;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003A6C File Offset: 0x00001C6C
		public static SiteConfig LoadConfig(string configfilepath)
		{
			return FPSerializer.Load<SiteConfig>(configfilepath);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003A84 File Offset: 0x00001C84
		public static bool SaveConfig(SiteConfig siteconfig, string configfilepath)
		{
			FPCache.Remove("FP_SITECONFIG_" + siteconfig.sitepath);
			return FPSerializer.Save<SiteConfig>(siteconfig, configfilepath);
		}

		// Token: 0x04000017 RID: 23
		private static object lockHelper = new object();
	}
}
