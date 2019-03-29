using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000004 RID: 4
	public class SMSConfigs
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021E4 File Offset: 0x000003E4
		public static SMSConfig GetSMSConfig()
		{
			SMSConfig smsconfig = FPCache.Get<SMSConfig>("FP_SMSCONFIG");
			lock (SMSConfigs.lockHelper)
			{
				if (smsconfig == null)
				{
					string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/sms.config");
					smsconfig = FPSerializer.Load<SMSConfig>(mapPath);
					if (smsconfig.posturl == "")
					{
						smsconfig.posturl = "http://sms.106jiekou.com/utf8/sms.aspx";
					}
					FPCache.Insert("FP_SMSCONFIG", smsconfig, mapPath);
				}
			}
			return smsconfig;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000228C File Offset: 0x0000048C
		public static bool SaveConfig(SMSConfig smsconfig)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/sms.config");
			FPCache.Remove("FP_SMSCONFIG");
			return FPSerializer.Save<SMSConfig>(smsconfig, mapPath);
		}

		// Token: 0x04000001 RID: 1
		private static object lockHelper = new object();
	}
}
