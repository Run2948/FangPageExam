using System;
using System.Collections.Generic;
using System.IO;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200002D RID: 45
	public class PluginBll
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00007C14 File Offset: 0x00005E14
		public static List<PluginConfig> GetPluginList()
		{
			List<PluginConfig> list = new List<PluginConfig>();
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "plugins");
			if (Directory.Exists(mapPath))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					PluginConfig pluginConfig = FPSerializer.Load<PluginConfig>(directoryInfo2.FullName + "\\plugin.config");
					pluginConfig.installpath = directoryInfo2.Name;
					list.Add(pluginConfig);
				}
			}
			return list;
		}
	}
}
