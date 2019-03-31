using System;
using System.Collections;
using System.Web;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x02000006 RID: 6
	public class FPSession
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002C14 File Offset: 0x00000E14
		public static void Insert(string key, object obj)
		{
			HttpContext.Current.Session.Add(key, obj);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C28 File Offset: 0x00000E28
		public static void Insert(string key, string u_key, object obj)
		{
			object obj2 = FPSession.Get(key);
			Hashtable hashtable;
			if (obj2 != null)
			{
				hashtable = (obj2 as Hashtable);
			}
			else
			{
				hashtable = new Hashtable();
			}
			if (hashtable[u_key] == null)
			{
				hashtable.Add(u_key, obj);
			}
			else
			{
				hashtable[u_key] = obj;
			}
			FPSession.Insert(key, hashtable);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C70 File Offset: 0x00000E70
		public static void Insert(string key, int u_key, object obj)
		{
			FPSession.Insert(key, u_key.ToString(), obj);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C80 File Offset: 0x00000E80
		public static object Get(string key)
		{
			if (HttpContext.Current.Session[key] != null)
			{
				return HttpContext.Current.Session[key];
			}
			return null;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static object Get(string key, string u_key)
		{
			object obj = FPSession.Get(key);
			object result = null;
			if (obj != null)
			{
				Hashtable hashtable = obj as Hashtable;
				if (hashtable[u_key] != null)
				{
					result = hashtable[u_key];
				}
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002CDA File Offset: 0x00000EDA
		public static object Get(string key, int u_key)
		{
			return FPSession.Get(key, u_key.ToString());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CE9 File Offset: 0x00000EE9
		public static void Remove(string key)
		{
			if (!string.IsNullOrEmpty(key) && HttpContext.Current.Session[key] != null)
			{
				HttpContext.Current.Session.Remove(key);
				HttpContext.Current.Session[key] = null;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D28 File Offset: 0x00000F28
		public static void Remove(string key, string u_keys)
		{
			if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(u_keys))
			{
				object obj = FPSession.Get(key);
				if (obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					foreach (string key2 in FPArray.SplitString(u_keys))
					{
						if (hashtable[key2] != null)
						{
							hashtable.Remove(key2);
						}
					}
					FPSession.Insert(key, hashtable);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static void Remove(string key, int u_key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				object obj = FPSession.Get(key);
				if (obj != null)
				{
					Hashtable hashtable = obj as Hashtable;
					if (hashtable[u_key] != null)
					{
						hashtable.Remove(u_key);
					}
					FPSession.Insert(key, hashtable);
				}
			}
		}
	}
}
