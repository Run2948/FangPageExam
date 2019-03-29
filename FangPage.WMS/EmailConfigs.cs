using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000010 RID: 16
	public class EmailConfigs
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00004A04 File Offset: 0x00002C04
		public static EmailConfig GetEmailConfig()
		{
			EmailConfig emailConfig = FPCache.Get<EmailConfig>("FP_EMAILCONFIG");
			lock (EmailConfigs.lockHelper)
			{
				if (emailConfig == null)
				{
					string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/email.config");
					emailConfig = FPSerializer.Load<EmailConfig>(mapPath);
					FPCache.Insert("FP_EMAILCONFIG", emailConfig, mapPath);
				}
			}
			return emailConfig;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004A84 File Offset: 0x00002C84
		public static bool SaveConfig(EmailConfig emailconfig)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/email.config");
			FPCache.Remove("FP_EMAILCONFIG");
			return FPSerializer.Save<EmailConfig>(emailconfig, mapPath);
		}

		// Token: 0x04000020 RID: 32
		private static object lockHelper = new object();
	}
}
