using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000032 RID: 50
	public class CacheBll
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00009750 File Offset: 0x00007950
		public static void Insert(string name, string key, object obj)
		{
			FPCache.Insert(key, obj);
			SqlParam sqlParam = DbHelper.MakeAndWhere("key", key);
			CacheInfo cacheInfo = DbHelper.ExecuteModel<CacheInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (cacheInfo.id > 0)
			{
				cacheInfo.name = name;
				cacheInfo.expires = -1;
				cacheInfo.cachedatetime = DbUtils.GetDateTime();
				DbHelper.ExecuteUpdate<CacheInfo>(cacheInfo);
				return;
			}
			cacheInfo.name = name;
			cacheInfo.key = key;
			cacheInfo.expires = -1;
			cacheInfo.cachedatetime = DbUtils.GetDateTime();
			DbHelper.ExecuteInsert<CacheInfo>(cacheInfo);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000097D4 File Offset: 0x000079D4
		public static void Insert(string name, string key, object obj, int expires)
		{
			FPCache.Insert(key, obj, expires);
			SqlParam sqlParam = DbHelper.MakeAndWhere("key", key);
			CacheInfo cacheInfo = DbHelper.ExecuteModel<CacheInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (cacheInfo.id > 0)
			{
				cacheInfo.name = name;
				cacheInfo.expires = expires;
				cacheInfo.cachedatetime = DbUtils.GetDateTime();
				DbHelper.ExecuteUpdate<CacheInfo>(cacheInfo);
				return;
			}
			cacheInfo.name = name;
			cacheInfo.key = key;
			cacheInfo.expires = expires;
			cacheInfo.cachedatetime = DbUtils.GetDateTime();
			DbHelper.ExecuteInsert<CacheInfo>(cacheInfo);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00009858 File Offset: 0x00007A58
		public static void Insert(string name, string key, int uid, object obj)
		{
			FPCache.InsertAt(key, uid, obj);
			SqlParam sqlParam = DbHelper.MakeAndWhere("key", key);
			CacheInfo cacheInfo = DbHelper.ExecuteModel<CacheInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (cacheInfo.id > 0)
			{
				cacheInfo.name = name;
				cacheInfo.expires = -1;
				cacheInfo.cachedatetime = DbUtils.GetDateTime();
				DbHelper.ExecuteUpdate<CacheInfo>(cacheInfo);
				return;
			}
			cacheInfo.name = name;
			cacheInfo.key = key;
			cacheInfo.expires = -1;
			cacheInfo.cachedatetime = DbUtils.GetDateTime();
			DbHelper.ExecuteInsert<CacheInfo>(cacheInfo);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000098DC File Offset: 0x00007ADC
		public static void Insert(string name, string key, object obj, string fileName)
		{
			FPCache.Insert(key, obj, fileName);
			SqlParam sqlParam = DbHelper.MakeAndWhere("key", key);
			CacheInfo cacheInfo = DbHelper.ExecuteModel<CacheInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (cacheInfo.id > 0)
			{
				cacheInfo.name = name;
				cacheInfo.expires = 0;
				cacheInfo.cachedatetime = DbUtils.GetDateTime();
				DbHelper.ExecuteUpdate<CacheInfo>(cacheInfo);
				return;
			}
			cacheInfo.name = name;
			cacheInfo.key = key;
			cacheInfo.expires = 0;
			cacheInfo.cachedatetime = DbUtils.GetDateTime();
			DbHelper.ExecuteInsert<CacheInfo>(cacheInfo);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000995E File Offset: 0x00007B5E
		public static void RemoveSortCache()
		{
			FPCache.RemoveStart("FP_SORTTREE");
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000996C File Offset: 0x00007B6C
		public static void RemoveUserCache(string key, int departid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("departid", departid);
			foreach (UserInfo userInfo in DbHelper.ExecuteList<UserInfo>(new SqlParam[]
			{
				sqlParam
			}))
			{
				FPCache.Remove(key, userInfo.id.ToString());
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000099E8 File Offset: 0x00007BE8
		public static void RemoveUserCache(string key, string departid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("departid", WhereType.In, departid);
			foreach (UserInfo userInfo in DbHelper.ExecuteList<UserInfo>(new SqlParam[]
			{
				sqlParam
			}))
			{
				FPCache.Remove(key, userInfo.id.ToString());
			}
		}
	}
}
