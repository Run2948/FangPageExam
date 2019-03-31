using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Config
{
	// Token: 0x02000028 RID: 40
	public class PluginConfigs
	{
		// Token: 0x06000335 RID: 821 RVA: 0x0000700C File Offset: 0x0000520C
		public static List<PluginConfig> GetMapPluList()
		{
			List<PluginConfig> list = new List<PluginConfig>();
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "plugins");
			if (Directory.Exists(mapPath))
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(mapPath).GetDirectories())
				{
					if (File.Exists(directoryInfo.FullName + "\\plugin.config"))
					{
						PluginConfig pluginConfig = FPSerializer.Load<PluginConfig>(directoryInfo.FullName + "\\plugin.config");
						pluginConfig.installpath = directoryInfo.Name;
						pluginConfig.size = FPFile.FormatBytesStr(FPFile.GetDirSize(directoryInfo.FullName));
						list.Add(pluginConfig);
					}
				}
			}
			return list;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000070BC File Offset: 0x000052BC
		public static PluginConfig GetMapPluConfig(string installpath)
		{
			PluginConfig result = new PluginConfig();
			if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "plugins/" + installpath + "/plugin.config")))
			{
				result = FPSerializer.Load<PluginConfig>(FPFile.GetMapPath(WebConfig.WebPath + "plugins/" + installpath + "/plugin.config"));
			}
			return result;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00007114 File Offset: 0x00005314
		public static PluginConfig GetPluConfig(string guid)
		{
			List<PluginConfig> list = PluginConfigs.GetMapPluList().FindAll((PluginConfig item) => item.guid.ToLower() == guid.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new PluginConfig();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000715C File Offset: 0x0000535C
		public static List<PluginConfig> GetPluList()
		{
			object obj = FPCache.Get("FP_PLUGINLIST");
			List<PluginConfig> list;
			if (obj != null)
			{
				list = (obj as List<PluginConfig>);
			}
			else
			{
				list = PluginConfigs.GetMapPluList();
				CacheBll.Insert("系统插件信息缓存", "FP_PLUGINLIST", list);
			}
			return list;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00007198 File Offset: 0x00005398
		public static PluginConfig GetPluInfo(string installpath)
		{
			List<PluginConfig> list = PluginConfigs.GetPluList().FindAll((PluginConfig item) => item.installpath.ToLower() == installpath.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new PluginConfig();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000071E0 File Offset: 0x000053E0
		public static PluginConfig GetPluInfoByMarkup(string markup)
		{
			List<PluginConfig> list = PluginConfigs.GetPluList().FindAll((PluginConfig item) => item.markup.ToLower() == markup.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new PluginConfig();
		}
	}
}
