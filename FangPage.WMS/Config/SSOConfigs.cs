using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Config
{
	// Token: 0x02000021 RID: 33
	public class SSOConfigs
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00006518 File Offset: 0x00004718
		public static SSOConfig GetSSOConfig()
		{
			object obj = FPCache.Get("FP_SSOCONFIG");
			object obj2 = SSOConfigs.lockHelper;
			SSOConfig ssoconfig;
			lock (obj2)
			{
				if (obj != null)
				{
					ssoconfig = (obj as SSOConfig);
				}
				else
				{
					string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/sso.config");
					ssoconfig = FPSerializer.Load<SSOConfig>(mapPath);
					CacheBll.Insert("单点登录配置缓存", "FP_SSOCONFIG", ssoconfig, mapPath);
				}
			}
			return ssoconfig;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006590 File Offset: 0x00004790
		public static bool SaveConfig(SSOConfig ssoconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/sso.config");
			FPCache.Remove("FP_SSOCONFIG");
			return FPSerializer.Save<SSOConfig>(ssoconfig, mapPath);
		}

		// Token: 0x0400014B RID: 331
		private static object lockHelper = new object();
	}
}
