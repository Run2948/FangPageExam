using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000035 RID: 53
	public class SysBll
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00009750 File Offset: 0x00007950
		public static int InsertLog(int uid, string name, string title, string description, bool status)
		{
			int result = 0;
			SysConfig config = SysConfigs.GetConfig();
			if (config.allowlog == 1)
			{
				result = DbHelper.ExecuteInsert<SysLogInfo>(new SysLogInfo
				{
					uid = uid,
					name = name,
					title = title,
					description = description,
					ip = FPRequest.GetIP(),
					postdatetime = DbUtils.GetDateTime(),
					status = (status ? 1 : 0)
				});
			}
			return result;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000097D4 File Offset: 0x000079D4
		public static long GetSysSize()
		{
			object obj = FPCache.Get("CACHE_SYSSIZE");
			long num;
			if (obj == null)
			{
				num = WMSUtils.DirSize(WebConfig.WebPath);
				FPCache.Insert("CACHE_SYSSIZE", num, 10);
			}
			else
			{
				num = (long)obj;
			}
			return num;
		}
	}
}
