using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200003C RID: 60
	public class SysBll
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0000B1F0 File Offset: 0x000093F0
		public static int InsertLog(int uid, string name, string content, bool status)
		{
			int result = 0;
			if (SysConfigs.GetConfig().allowlog == 1)
			{
				result = DbHelper.ExecuteInsert<SysLogInfo>(new SysLogInfo
				{
					uid = uid,
					name = name,
					content = content,
					ip = FPRequest.GetIP(),
					postdatetime = DbUtils.GetDateTime(),
					status = (status ? 1 : 0)
				});
			}
			return result;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B250 File Offset: 0x00009450
		public static long GetSysSize()
		{
			object obj = FPCache.Get("WMS_SYSSIZE");
			long num;
			if (obj == null)
			{
				num = FPFile.GetDirSize(FPFile.GetMapPath(WebConfig.WebPath));
				int expires = 10;
				int num2 = (int)(num / 1073741824L);
				if (num2 > 1 && num2 <= 10)
				{
					expires = 30;
				}
				else if (num2 > 10 && num2 <= 20)
				{
					expires = 60;
				}
				else if (num2 > 20 && num2 <= 30)
				{
					expires = 120;
				}
				else if (num2 > 30 && num2 <= 40)
				{
					expires = 180;
				}
				else if (num2 > 40 && num2 <= 50)
				{
					expires = 250;
				}
				else if (num2 > 50 && num2 <= 100)
				{
					expires = 600;
				}
				else if (num2 > 100)
				{
					expires = 1200;
				}
				CacheBll.Insert("系统文件大小缓存", "WMS_SYSSIZE", num, expires);
			}
			else
			{
				num = (long)obj;
			}
			return num;
		}
	}
}
