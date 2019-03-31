using System;
using System.Collections;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Config
{
	// Token: 0x0200001F RID: 31
	public class SMSConfigs
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000062C4 File Offset: 0x000044C4
		public static SMSConfig GetSMSConfig(out Hashtable sendRet)
		{
			object obj = FPCache.Get("FP_SMSCONFIG");
			object obj2 = SMSConfigs.lockHelper;
			SMSConfig smsconfig;
			lock (obj2)
			{
				if (obj != null)
				{
					smsconfig = (obj as SMSConfig);
				}
				else
				{
					string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/sms.config");
					smsconfig = FPSerializer.Load<SMSConfig>(mapPath);
					CacheBll.Insert("系统短信配置缓存", "FP_SMSCONFIG", smsconfig, mapPath);
				}
			}
			sendRet = new Hashtable();
			string[] array = FPArray.SplitString(smsconfig.result, "|");
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = FPArray.SplitString(array[i], "=", 2);
				if (array2[0] != "" && !sendRet.Contains(array2[0]))
				{
					sendRet[array2[0]] = array2[1];
				}
			}
			return smsconfig;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000063A8 File Offset: 0x000045A8
		public static bool SaveConfig(SMSConfig smsconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/sms.config");
			FPCache.Remove("FP_SMSCONFIG");
			return FPSerializer.Save<SMSConfig>(smsconfig, mapPath);
		}

		// Token: 0x04000140 RID: 320
		private static object lockHelper = new object();
	}
}
