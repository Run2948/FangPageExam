using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x02000003 RID: 3
	public class FPCache
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020C0 File Offset: 0x000002C0
		public static void Insert(string key, object obj)
		{
			HttpContext.Current.Cache.Insert(key, obj);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D4 File Offset: 0x000002D4
		public static void Insert(string key, object obj, string fileName)
		{
			CacheDependency dependencies = new CacheDependency(fileName);
			HttpContext.Current.Cache.Insert(key, obj, dependencies);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020FA File Offset: 0x000002FA
		public static void Insert(string key, object obj, int expires)
		{
			HttpContext.Current.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000211C File Offset: 0x0000031C
		public static void InsertAt(string name, string key, object obj)
		{
			object obj2 = FPCache.Get(name);
			Hashtable hashtable;
			if (obj2 != null)
			{
				hashtable = (obj2 as Hashtable);
			}
			else
			{
				hashtable = new Hashtable();
			}
			if (hashtable[key] == null)
			{
				hashtable.Add(key, obj);
			}
			else
			{
				hashtable[key] = obj;
			}
			FPCache.Insert(name, hashtable);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002164 File Offset: 0x00000364
		public static void InsertAt(string name, int key, object obj)
		{
			FPCache.InsertAt(name, key.ToString(), obj);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002174 File Offset: 0x00000374
		public static void InsertAt(string name, string key, object obj, int expires)
		{
			object obj2 = FPCache.Get(name);
			Hashtable hashtable;
			if (obj2 != null)
			{
				hashtable = (obj2 as Hashtable);
			}
			else
			{
				hashtable = new Hashtable();
			}
			if (hashtable[key] == null)
			{
				hashtable.Add(key, obj);
			}
			else
			{
				hashtable[key] = obj;
			}
			FPCache.Insert(name, hashtable, expires);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BD File Offset: 0x000003BD
		public static void InsertAt(string name, int key, object obj, int expires)
		{
			FPCache.InsertAt(name, key.ToString(), obj, expires);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CE File Offset: 0x000003CE
		public static object Get(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return null;
			}
			return HttpContext.Current.Cache.Get(key);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EC File Offset: 0x000003EC
		public static object Get(string name, string key)
		{
			object obj = FPCache.Get(name);
			object result = null;
			if (obj != null)
			{
				Hashtable hashtable = obj as Hashtable;
				if (hashtable[key] != null)
				{
					result = hashtable[key];
				}
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000221E File Offset: 0x0000041E
		public static object Get(string name, int key)
		{
			return FPCache.Get(name, key.ToString());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000222D File Offset: 0x0000042D
		public static void Remove(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				HttpContext.Current.Cache.Remove(key);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002248 File Offset: 0x00000448
		public static void Remove(string name, int key)
		{
			if (!string.IsNullOrEmpty(name))
			{
				object obj = FPCache.Get(name);
				if (obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					if (hashtable[key] != null)
					{
						hashtable.Remove(key);
					}
					FPCache.Insert(name, hashtable);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002290 File Offset: 0x00000490
		public static void Remove(string name, string keys)
		{
			if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(keys))
			{
				object obj = FPCache.Get(name);
				if (obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					foreach (string key in FPArray.SplitString(keys))
					{
						if (hashtable[key] != null)
						{
							hashtable.Remove(key);
						}
					}
					FPCache.Insert(name, hashtable);
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022F4 File Offset: 0x000004F4
		public static void Remove(string name, int key, int expires)
		{
			if (!string.IsNullOrEmpty(name))
			{
				object obj = FPCache.Get(name);
				if (obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					if (hashtable[key] != null)
					{
						hashtable.Remove(key);
					}
					FPCache.Insert(name, hashtable, expires);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000233C File Offset: 0x0000053C
		public static void Remove(string name, string keys, int expires)
		{
			if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(keys))
			{
				object obj = FPCache.Get(name);
				if (obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					foreach (string key in FPArray.SplitString(keys))
					{
						if (hashtable[key] != null)
						{
							hashtable.Remove(key);
						}
					}
					FPCache.Insert(name, hashtable, expires);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023A0 File Offset: 0x000005A0
		public static void RemoveStart(string startkey)
		{
			if (!string.IsNullOrEmpty(startkey))
			{
				IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Key.ToString().StartsWith(startkey))
					{
						HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
					}
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002400 File Offset: 0x00000600
		public static void RemoveList(string name, string keys)
		{
			if (!string.IsNullOrEmpty(name))
			{
				foreach (string text in FPArray.SplitString(keys))
				{
					if (text != "")
					{
						FPCache.Remove(name + text);
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002448 File Offset: 0x00000648
		public static void RemovePattern(string pattern)
		{
			IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			while (enumerator.MoveNext())
			{
				if (regex.IsMatch(enumerator.Key.ToString()))
				{
					HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024A8 File Offset: 0x000006A8
		public static void Clear()
		{
			IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
			ArrayList arrayList = new ArrayList();
			while (enumerator.MoveNext())
			{
				arrayList.Add(enumerator.Key);
			}
			foreach (object obj in arrayList)
			{
				string key = (string)obj;
				HttpContext.Current.Cache.Remove(key);
			}
		}
	}
}
