using System;
using System.Web;
using System.Web.Caching;

namespace FangPage.MVC
{
	// Token: 0x02000008 RID: 8
	public class FPCache
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000031E0 File Offset: 0x000013E0
		public static void Insert(string key, object obj, string fileName)
		{
			CacheDependency dependencies = new CacheDependency(fileName);
			HttpContext.Current.Cache.Insert(key, obj, dependencies);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003208 File Offset: 0x00001408
		public static void Insert(string key, object obj, int expires)
		{
			HttpContext.Current.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000322C File Offset: 0x0000142C
		public static object Get(string key)
		{
			object result;
			if (string.IsNullOrEmpty(key))
			{
				result = null;
			}
			else
			{
				result = HttpContext.Current.Cache.Get(key);
			}
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003260 File Offset: 0x00001460
		public static T Get<T>(string key)
		{
			object obj = FPCache.Get(key);
			return (obj == null) ? default(T) : ((T)((object)obj));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003290 File Offset: 0x00001490
		public static void Remove(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				HttpContext.Current.Cache.Remove(key);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000032BC File Offset: 0x000014BC
		public static void Remove(string key, string keystr)
		{
			if (!string.IsNullOrEmpty(key))
			{
				foreach (string str in keystr.Split(new char[]
				{
					','
				}))
				{
					HttpContext.Current.Cache.Remove(key + str);
				}
			}
		}
	}
}
