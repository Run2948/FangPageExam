using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200002E RID: 46
	public class RegConfigs
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00007CBC File Offset: 0x00005EBC
		public static RegConfig GetRegConfig()
		{
			RegConfig regConfig = FPCache.Get<RegConfig>("FP_REGCONFIG");
			lock (RegConfigs.lockHelper)
			{
				if (regConfig == null)
				{
					string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/reg.config");
					regConfig = FPSerializer.Load<RegConfig>(mapPath);
					FPCache.Insert("FP_REGCONFIG", regConfig, mapPath);
				}
			}
			return regConfig;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007D3C File Offset: 0x00005F3C
		public static bool SaveConfig(RegConfig regconfig)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/reg.config");
			FPCache.Remove("FP_REGCONFIG");
			return FPSerializer.Save<RegConfig>(regconfig, mapPath);
		}

		// Token: 0x04000128 RID: 296
		private static object lockHelper = new object();
	}
}
