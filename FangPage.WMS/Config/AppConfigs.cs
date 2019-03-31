using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Config
{
	// Token: 0x0200001D RID: 29
	public class AppConfigs
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00006020 File Offset: 0x00004220
		public static List<AppConfig> GetMapAppList()
		{
			List<AppConfig> list = new List<AppConfig>();
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "app");
			if (Directory.Exists(mapPath))
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(mapPath).GetDirectories())
				{
					if (File.Exists(directoryInfo.FullName + "\\app.config"))
					{
						AppConfig appConfig = FPSerializer.Load<AppConfig>(directoryInfo.FullName + "\\app.config");
						appConfig.installpath = directoryInfo.Name;
						appConfig.size = FPFile.FormatBytesStr(FPFile.GetDirSize(directoryInfo.FullName));
						list.Add(appConfig);
					}
				}
			}
			return list;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000060D0 File Offset: 0x000042D0
		public static AppConfig GetMapAppConfig(string installpath)
		{
			AppConfig result = new AppConfig();
			if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "app/" + installpath + "/app.config")))
			{
				result = FPSerializer.Load<AppConfig>(FPFile.GetMapPath(WebConfig.WebPath + "app/" + installpath + "/app.config"));
			}
			return result;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00006128 File Offset: 0x00004328
		public static AppConfig GetAppConfig(string guid)
		{
			List<AppConfig> list = AppConfigs.GetMapAppList().FindAll((AppConfig item) => item.guid.ToLower() == guid.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new AppConfig();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006170 File Offset: 0x00004370
		public static List<AppConfig> GetAppList()
		{
			object obj = FPCache.Get("FP_APPLIST");
			List<AppConfig> list;
			if (obj != null)
			{
				list = (obj as List<AppConfig>);
			}
			else
			{
				list = AppConfigs.GetMapAppList();
				CacheBll.Insert("系统应用信息缓存", "FP_APPLIST", list);
			}
			return list;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000061AC File Offset: 0x000043AC
		public static AppConfig GetAppInfo(string installpath)
		{
			List<AppConfig> list = AppConfigs.GetAppList().FindAll((AppConfig item) => item.installpath.ToLower() == installpath.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new AppConfig();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000061F4 File Offset: 0x000043F4
		public static AppConfig GetAppInfoByMarkup(string markup)
		{
			List<AppConfig> list = AppConfigs.GetAppList().FindAll((AppConfig item) => item.markup.ToLower() == markup.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new AppConfig();
		}
	}
}
