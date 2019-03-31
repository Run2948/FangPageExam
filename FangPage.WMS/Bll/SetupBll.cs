using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200002E RID: 46
	public class SetupBll
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public static SetupInfo GetSetupInfo(string guid)
		{
			List<SetupInfo> list = SetupBll.GetSetupList().FindAll((SetupInfo item) => item.guid.ToLower() == guid.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new SetupInfo();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00007C0C File Offset: 0x00005E0C
		public static SetupInfo GetSetupInfo(string type, string installpath)
		{
			SetupInfo result = new SetupInfo();
			if (type == "app")
			{
				result = SetupBll.GetSetupInfo(AppConfigs.GetAppInfo(installpath));
			}
			else if (type == "plugins")
			{
				result = SetupBll.GetSetupInfo(PluginConfigs.GetPluInfo(installpath));
			}
			else if (type == "sites")
			{
				result = SetupBll.GetSetupInfo(SiteConfigs.GetSiteInfo(installpath));
			}
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00007C70 File Offset: 0x00005E70
		public static SetupInfo GetSetupInfo(SiteConfig siteconfig)
		{
			return new SetupInfo
			{
				guid = siteconfig.guid,
				name = siteconfig.name,
				type = "sites",
				installpath = siteconfig.sitepath,
				icon = siteconfig.icon,
				version = siteconfig.version,
				markup = siteconfig.markup,
				platform = siteconfig.platform,
				dll = siteconfig.dll
			};
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00007CF0 File Offset: 0x00005EF0
		public static SetupInfo GetSetupInfo(AppConfig appconfig)
		{
			return new SetupInfo
			{
				guid = appconfig.guid,
				name = appconfig.name,
				type = "app",
				installpath = appconfig.installpath,
				icon = appconfig.icon,
				version = appconfig.version,
				markup = appconfig.markup,
				platform = appconfig.platform,
				dll = appconfig.dll
			};
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00007D70 File Offset: 0x00005F70
		public static SetupInfo GetSetupInfo(PluginConfig pluginconfig)
		{
			return new SetupInfo
			{
				guid = pluginconfig.guid,
				name = pluginconfig.name,
				type = "plugins",
				installpath = pluginconfig.installpath,
				icon = pluginconfig.icon,
				version = pluginconfig.version,
				markup = pluginconfig.markup,
				platform = pluginconfig.platform,
				dll = pluginconfig.dll
			};
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public static SetupInfo GetSetupInfoByMarkup(string markup)
		{
			List<SetupInfo> list = SetupBll.GetSetupList().FindAll((SetupInfo item) => item.markup.ToLower() == markup.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new SetupInfo();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00007E38 File Offset: 0x00006038
		public static List<SetupInfo> GetSetupList()
		{
			List<SetupInfo> list = new List<SetupInfo>();
			foreach (SiteConfig siteconfig in SiteConfigs.GetSysSiteList())
			{
				list.Add(SetupBll.GetSetupInfo(siteconfig));
			}
			foreach (AppConfig appconfig in AppConfigs.GetMapAppList())
			{
				list.Add(SetupBll.GetSetupInfo(appconfig));
			}
			foreach (PluginConfig pluginconfig in PluginConfigs.GetMapPluList())
			{
				list.Add(SetupBll.GetSetupInfo(pluginconfig));
			}
			return list;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00007F28 File Offset: 0x00006128
		public static void DownloadSetup(AppConfig appconfig)
		{
			SetupBll.DownloadSetup(SetupBll.GetSetupInfo(appconfig));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00007F35 File Offset: 0x00006135
		public static void DownloadSetup(PluginConfig pluconfig)
		{
			SetupBll.DownloadSetup(SetupBll.GetSetupInfo(pluconfig));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00007F42 File Offset: 0x00006142
		public static void DownloadSetup(SiteConfig siteconfig)
		{
			SetupBll.DownloadSetup(SetupBll.GetSetupInfo(siteconfig));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00007F50 File Offset: 0x00006150
		public static void DownloadSetup(SetupInfo setupinfo)
		{
			if (setupinfo.type == "")
			{
				return;
			}
			string str = "";
			string path = "";
			if (setupinfo.type == "app")
			{
				str = setupinfo.name + ".fpk";
				path = FPFile.GetMapPath(WebConfig.WebPath + "app/" + setupinfo.installpath);
			}
			else if (setupinfo.type == "plugins")
			{
				str = setupinfo.name + ".plu";
				path = FPFile.GetMapPath(WebConfig.WebPath + "plugins/" + setupinfo.installpath);
			}
			else if (setupinfo.type == "sites")
			{
				str = setupinfo.name + ".site";
				path = FPFile.GetMapPath(WebConfig.WebPath + "sites" + setupinfo.installpath);
			}
			using (FPZip fpzip = new FPZip())
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					fpzip.AddDir(directoryInfo2.FullName);
				}
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					fpzip.AddFile(fileInfo.FullName, "");
				}
				if (setupinfo.dll != "")
				{
					string[] array = FPArray.SplitString(setupinfo.dll, 2);
					if (array[1] == "")
					{
						array[1] = array[0].Replace(".Controller", "");
					}
					if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + array[1] + ".dll")))
					{
						fpzip.AddFile(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + array[1] + ".dll"), "bin/" + array[1] + ".dll");
					}
				}
				fpzip.ZipDown(FPUtils.UrlEncode(str));
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00008184 File Offset: 0x00006384
		public static void DeleteSetup(AppConfig appconfig)
		{
			SetupBll.DeleteSetup(SetupBll.GetSetupInfo(appconfig));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008191 File Offset: 0x00006391
		public static void DeleteSetup(PluginConfig pluconfig)
		{
			SetupBll.DeleteSetup(SetupBll.GetSetupInfo(pluconfig));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000081A0 File Offset: 0x000063A0
		public static void DeleteSetup(SiteConfig siteconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "sites/" + siteconfig.sitepath);
			if (Directory.Exists(mapPath))
			{
				Directory.Delete(mapPath, true);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000081D8 File Offset: 0x000063D8
		public static void DeleteSetup(SetupInfo setupinfo)
		{
			if (setupinfo.type == "")
			{
				return;
			}
			if (setupinfo.dll != "")
			{
				string[] array = FPArray.SplitString(setupinfo.dll, 2);
				if (array[1] == "")
				{
					array[1] = array[0].Replace(".Controller", "");
				}
				if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + array[1] + ".dll")))
				{
					File.Delete(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + array[1] + ".dll"));
				}
			}
			string text = "";
			if (setupinfo.type == "app")
			{
				text = FPFile.GetMapPath(WebConfig.WebPath + "app/" + setupinfo.installpath);
			}
			else if (setupinfo.type == "plugins")
			{
				text = FPFile.GetMapPath(WebConfig.WebPath + "plugins/" + setupinfo.installpath);
			}
			else if (setupinfo.type == "sites")
			{
				text = FPFile.GetMapPath(WebConfig.WebPath + setupinfo.installpath);
			}
			if (DbConfigs.DbType == DbType.Access)
			{
				if (File.Exists(text + "\\datas\\access_uninst.sql"))
				{
					DbHelper.ExecuteSql(FPFile.ReadFile(text + "\\datas\\access_uninst.sql"));
				}
			}
			else if (File.Exists(text + "\\datas\\sqlserver_uninst.sql"))
			{
				DbHelper.ExecuteSql(FPFile.ReadFile(text + "\\datas\\sqlserver_uninst.sql"));
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("guid", setupinfo.guid);
			DbHelper.ExecuteDelete<SortAppInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (Directory.Exists(text))
			{
				Directory.Delete(text, true);
			}
			if (setupinfo.type == "sites")
			{
				string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "sites/" + setupinfo.installpath);
				if (Directory.Exists(mapPath))
				{
					Directory.Delete(mapPath, true);
				}
			}
		}
	}
}
