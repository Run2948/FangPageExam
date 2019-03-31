using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Config
{
	// Token: 0x02000029 RID: 41
	public class RegConfigs
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00007228 File Offset: 0x00005428
		public static RegConfig GetRegConfig()
		{
			object obj = FPCache.Get("FP_REGCONFIG");
			object obj2 = RegConfigs.lockHelper;
			RegConfig regConfig;
			lock (obj2)
			{
				if (obj != null)
				{
					regConfig = (obj as RegConfig);
				}
				else
				{
					string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/reg.config");
					regConfig = FPSerializer.Load<RegConfig>(mapPath);
					CacheBll.Insert("用户注册配置缓存", "FP_REGCONFIG", regConfig, mapPath);
				}
			}
			return regConfig;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000072A0 File Offset: 0x000054A0
		public static bool SaveConfig(RegConfig regconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/reg.config");
			FPCache.Remove("FP_REGCONFIG");
			return FPSerializer.Save<RegConfig>(regconfig, mapPath);
		}

		// Token: 0x040001A5 RID: 421
		private static object lockHelper = new object();
	}
}
