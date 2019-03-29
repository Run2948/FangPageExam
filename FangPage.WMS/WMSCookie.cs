using System;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200000E RID: 14
	public class WMSCookie
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003DA0 File Offset: 0x00001FA0
		public static void WriteCookie(string strName, string strValue)
		{
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies["wms"];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie("wms");
				httpCookie.Values[strName] = FPUtils.UrlEncode(strValue);
			}
			else
			{
				httpCookie.Values[strName] = FPUtils.UrlEncode(strValue);
				if (HttpContext.Current.Request.Cookies["wms"]["expires"] != null)
				{
					int num = FPUtils.StrToInt(HttpContext.Current.Request.Cookies["wms"]["expires"].ToString(), 0);
					if (num > 0)
					{
						httpCookie.Expires = DateTime.Now.AddMinutes((double)FPUtils.StrToInt(HttpContext.Current.Request.Cookies["wms"]["expires"].ToString(), 0));
					}
				}
			}
			string text = SysConfigs.GetConfig().cookiedomain.Trim();
			if (text != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(text.TrimStart(new char[]
			{
				'.'
			})) > -1 && WMSUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
			{
				httpCookie.Domain = text;
			}
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003F4C File Offset: 0x0000214C
		public static void WriteUserCookie(UserInfo userinfo, int expires, string passwordkey)
		{
			if (userinfo != null && userinfo.id > 0)
			{
				HttpCookie httpCookie = new HttpCookie("wms");
				httpCookie.Values["userid"] = userinfo.id.ToString();
				httpCookie.Values["password"] = FPUtils.UrlEncode(DES.Encode(userinfo.password, passwordkey));
				httpCookie.Values["expires"] = expires.ToString();
				if (expires > 0)
				{
					httpCookie.Expires = DateTime.Now.AddMinutes((double)expires);
				}
				string text = SysConfigs.GetConfig().cookiedomain.Trim();
				if (text != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(text.TrimStart(new char[]
				{
					'.'
				})) > -1 && WMSUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
				{
					httpCookie.Domain = text;
				}
				HttpContext.Current.Response.AppendCookie(httpCookie);
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000408C File Offset: 0x0000228C
		public static string GetCookie(string strName)
		{
			string result;
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["wms"] != null && HttpContext.Current.Request.Cookies["wms"][strName] != null)
			{
				result = FPUtils.UrlDecode(HttpContext.Current.Request.Cookies["wms"][strName].ToString());
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004128 File Offset: 0x00002328
		public static DateTime GetLastCookieTime()
		{
			string dateTime = FPUtils.GetDateTime(WMSCookie.GetCookie("lastactivity"));
			return Convert.ToDateTime(dateTime);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004150 File Offset: 0x00002350
		public static void ClearUserCookie()
		{
			WMSCookie.ClearUserCookie("wms");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004160 File Offset: 0x00002360
		public static void ClearUserCookie(string cookieName)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.Values.Clear();
			httpCookie.Expires = DateTime.Now.AddYears(-1);
			string text = SysConfigs.GetConfig().cookiedomain.Trim();
			if (text != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(text.TrimStart(new char[]
			{
				'.'
			})) > -1 && WMSUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
			{
				httpCookie.Domain = text;
			}
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}
	}
}
