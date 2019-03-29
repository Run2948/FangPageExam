using System;
using System.Web;

namespace FangPage.MVC
{
	// Token: 0x02000009 RID: 9
	public class FPCookie
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00003328 File Offset: 0x00001528
		public static void WriteCookie(string strName, string strValue)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(strName);
			}
			httpCookie.Value = strValue;
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000337C File Offset: 0x0000157C
		public static void WriteCookie(string strName, string key, string strValue)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(strName);
			}
			httpCookie[key] = strValue;
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000033D0 File Offset: 0x000015D0
		public static void WriteCookie(string strName, string strValue, int expires)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[strName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(strName);
			}
			httpCookie.Value = strValue;
			httpCookie.Expires = DateTime.Now.AddMinutes((double)expires);
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003438 File Offset: 0x00001638
		public static string GetCookie(string strName)
		{
			string result;
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
			{
				result = HttpContext.Current.Request.Cookies[strName].Value.ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000034A4 File Offset: 0x000016A4
		public static string GetCookie(string strName, string key)
		{
			string result;
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
			{
				result = HttpContext.Current.Request.Cookies[strName][key].ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
