using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Config
{
	// Token: 0x02000022 RID: 34
	public class EmailConfigs
	{
		// Token: 0x0600027C RID: 636 RVA: 0x000065D0 File Offset: 0x000047D0
		public static EmailConfig GetEmailConfig()
		{
			object obj = FPCache.Get("FP_EMAILCONFIG");
			object obj2 = EmailConfigs.lockHelper;
			EmailConfig emailConfig;
			lock (obj2)
			{
				if (obj != null)
				{
					emailConfig = (obj as EmailConfig);
				}
				else
				{
					string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/email.config");
					emailConfig = FPSerializer.Load<EmailConfig>(mapPath);
					CacheBll.Insert("系统邮箱配置缓存", "FP_EMAILCONFIG", emailConfig, mapPath);
				}
			}
			return emailConfig;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006648 File Offset: 0x00004848
		public static bool SaveConfig(EmailConfig emailconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/email.config");
			FPCache.Remove("FP_EMAILCONFIG");
			return FPSerializer.Save<EmailConfig>(emailconfig, mapPath);
		}

		// Token: 0x0400014C RID: 332
		private static object lockHelper = new object();
	}
}
