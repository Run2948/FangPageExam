using System;
using System.Web;

namespace FangPage.MVC
{
	// Token: 0x02000004 RID: 4
	public class FPCookie
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002540 File Offset: 0x00000740
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

		// Token: 0x0600001D RID: 29 RVA: 0x00002584 File Offset: 0x00000784
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

		// Token: 0x0600001E RID: 30 RVA: 0x000025CC File Offset: 0x000007CC
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002628 File Offset: 0x00000828
		public static string GetCookie(string strName)
		{
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
			{
				return HttpContext.Current.Request.Cookies[strName].Value.ToString();
			}
			return "";
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002684 File Offset: 0x00000884
		public static string GetCookie(string strName, string key)
		{
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
			{
				return HttpContext.Current.Request.Cookies[strName][key].ToString();
			}
			return "";
		}
	}
}
