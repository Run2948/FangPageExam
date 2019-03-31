using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x0200000C RID: 12
	public class SiteConfigs
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003924 File Offset: 0x00001B24
		public static List<SiteConfig> GetSysSiteList()
		{
			List<SiteConfig> list = new List<SiteConfig>();
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath);
			if (Directory.Exists(mapPath))
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(mapPath).GetDirectories())
				{
					if (File.Exists(directoryInfo.FullName + "\\site.config"))
					{
						SiteConfig siteConfig = FPSerializer.Load<SiteConfig>(directoryInfo.FullName + "\\site.config");
						siteConfig.sitepath = directoryInfo.Name;
						list.Add(siteConfig);
					}
				}
			}
			return list;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000039B0 File Offset: 0x00001BB0
		public static List<SiteConfig> GetMapSiteList()
		{
			List<SiteConfig> list = new List<SiteConfig>();
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "sites");
			if (Directory.Exists(mapPath))
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(mapPath).GetDirectories())
				{
					if (File.Exists(directoryInfo.FullName + "\\site.config") && directoryInfo.Name.ToLower() != "app" && directoryInfo.Name.ToLower() != "plugins")
					{
						SiteConfig siteConfig = FPSerializer.Load<SiteConfig>(directoryInfo.FullName + "\\site.config");
						siteConfig.sitepath = directoryInfo.Name;
						string mapPath2 = FPFile.GetMapPath(WebConfig.WebPath + directoryInfo.Name);
						if (Directory.Exists(mapPath2))
						{
							siteConfig.size = FPFile.FormatBytesStr(FPFile.GetDirSize(mapPath2));
						}
						else
						{
							siteConfig.size = FPFile.FormatBytesStr(FPFile.GetDirSize(directoryInfo.FullName));
						}
						list.Add(siteConfig);
					}
				}
			}
			return list;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public static SiteConfig GetMapSiteConfig(string sitepath)
		{
			SiteConfig result = new SiteConfig();
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "sites/" + sitepath + "/site.config");
			if (File.Exists(mapPath))
			{
				result = FPSerializer.Load<SiteConfig>(mapPath);
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003B14 File Offset: 0x00001D14
		public static SiteConfig GetSiteConfig(string guid)
		{
			SiteConfig result = new SiteConfig();
			List<SiteConfig> mapSiteList = SiteConfigs.GetMapSiteList();
			for (int i = 0; i < mapSiteList.Count; i++)
			{
				if (mapSiteList[i].guid.ToLower() == guid.ToLower())
				{
					result = mapSiteList[i];
					break;
				}
			}
			return result;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003B68 File Offset: 0x00001D68
		public static List<SiteConfig> GetSiteList()
		{
			object obj = FPCache.Get("FP_SITELIST");
			List<SiteConfig> result = new List<SiteConfig>();
			if (obj != null)
			{
				result = (obj as List<SiteConfig>);
			}
			else
			{
				result = new List<SiteConfig>();
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003B9C File Offset: 0x00001D9C
		public static SiteConfig GetSiteInfo(string sitepath)
		{
			List<SiteConfig> list = SiteConfigs.GetSiteList().FindAll((SiteConfig item) => item.sitepath.ToLower() == sitepath.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			SiteConfig siteConfig = SiteConfigs.LoadSiteConfig(sitepath);
			if (siteConfig.guid != "")
			{
				FPCache.Remove("FP_SITELIST");
				list.Add(siteConfig);
				FPCache.Insert("FP_SITELIST", list);
			}
			return siteConfig;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static SiteConfig LoadSiteConfig(string sitepath)
		{
			SiteConfig siteConfig = new SiteConfig();
			if (sitepath.ToLower() == "app")
			{
				siteConfig.name = "系统应用";
				siteConfig.sitepath = "app";
				siteConfig.version = "1.0.0";
				siteConfig.urltype = 0;
			}
			else if (sitepath.ToLower() == "plugins")
			{
				siteConfig.name = "系统插件";
				siteConfig.sitepath = "plugins";
				siteConfig.version = "1.0.0";
				siteConfig.urltype = 0;
			}
			else if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "sites/" + sitepath + "/site.config")))
			{
				siteConfig = FPSerializer.Load<SiteConfig>(FPFile.GetMapPath(WebConfig.WebPath + "sites/" + sitepath + "/site.config"));
			}
			else if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + sitepath + "/site.config")))
			{
				siteConfig = FPSerializer.Load<SiteConfig>(FPFile.GetMapPath(WebConfig.WebPath + sitepath + "/site.config"));
			}
			else if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "/site.config")))
			{
				siteConfig = FPSerializer.Load<SiteConfig>(FPFile.GetMapPath(WebConfig.WebPath + "/site.config"));
			}
			return siteConfig;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003D60 File Offset: 0x00001F60
		public static void SaveSiteConfig(SiteConfig siteconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "sites/" + siteconfig.sitepath + "/site.config");
			SiteConfigs.SaveSiteConfig(siteconfig, mapPath);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003D94 File Offset: 0x00001F94
		public static void SaveSiteConfig(SiteConfig siteconfig, string configfilepath)
		{
			List<SiteConfig> siteList = SiteConfigs.GetSiteList();
			FPCache.Remove("FP_SITELIST");
			if (siteList.Count == 0)
			{
				siteList.Add(siteconfig);
			}
			else
			{
				bool flag = false;
				for (int i = 0; i < siteList.Count; i++)
				{
					if (siteList[i].sitepath.ToLower() == siteconfig.sitepath.ToLower())
					{
						siteList[i] = siteconfig;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					siteList.Add(siteconfig);
				}
			}
			FPCache.Insert("FP_SITELIST", siteList);
			FPSerializer.Save<SiteConfig>(siteconfig, configfilepath);
		}

		// Token: 0x04000024 RID: 36
		private static object lockHelper = new object();
	}
}
