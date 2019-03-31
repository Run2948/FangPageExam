using System;
using System.Text.RegularExpressions;
using System.Web;
using FangPage.Common;
using FangPage.WMS.Config;

namespace FangPage.WMS
{
	// Token: 0x02000006 RID: 6
	public class WMSUtils
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00003B18 File Offset: 0x00001D18
		public static void WriteCookie(string cookieName, int Port, string strName, string strValue)
		{
			if (string.IsNullOrEmpty(cookieName))
			{
				cookieName = "FP_WMS";
			}
			if (Port > 0)
			{
				cookieName = cookieName + "_" + Port;
			}
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookieName];
			if (httpCookie == null)
			{
				httpCookie = new HttpCookie(cookieName);
				httpCookie.Values[strName] = FPUtils.UrlEncode(strValue);
			}
			else
			{
				httpCookie.Values[strName] = FPUtils.UrlEncode(strValue);
				if (HttpContext.Current.Request.Cookies[cookieName]["expires"] != null && FPUtils.StrToInt(HttpContext.Current.Request.Cookies[cookieName]["expires"].ToString(), 0) > 0)
				{
					httpCookie.Expires = DateTime.Now.AddMinutes((double)FPUtils.StrToInt(HttpContext.Current.Request.Cookies[cookieName]["expires"].ToString(), 0));
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

		// Token: 0x06000017 RID: 23 RVA: 0x00003C9C File Offset: 0x00001E9C
		public static void WriteUserCookie(string cookieName, int Port, string token, int expires)
		{
			if (token == "")
			{
				return;
			}
			if (string.IsNullOrEmpty(cookieName))
			{
				cookieName = "FP_WMS";
			}
			if (Port > 0)
			{
				cookieName = cookieName + "_" + Port;
			}
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.Values["token"] = token;
			if (expires > 0)
			{
				httpCookie.Expires = DateTime.Now.AddMinutes((double)expires);
			}
			string cookiedomain = SysConfigs.GetConfig().cookiedomain;
			if (cookiedomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookiedomain.TrimStart(new char[]
			{
				'.'
			})) > -1 && WMSUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
			{
				httpCookie.Domain = cookiedomain;
			}
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003D88 File Offset: 0x00001F88
		public static string GetCookie(string cookieName, int Port, string strName)
		{
			if (string.IsNullOrEmpty(cookieName))
			{
				cookieName = "FP_WMS";
			}
			if (Port > 0)
			{
				cookieName = cookieName + "_" + Port;
			}
			if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookieName] != null && HttpContext.Current.Request.Cookies[cookieName][strName] != null)
			{
				return FPUtils.UrlDecode(HttpContext.Current.Request.Cookies[cookieName][strName].ToString());
			}
			return "";
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003E2B File Offset: 0x0000202B
		public static void ClearUserCookie(string cookieName, int Port)
		{
			if (string.IsNullOrEmpty(cookieName))
			{
				cookieName = "FP_WMS";
			}
			if (Port > 0)
			{
				cookieName = cookieName + "_" + Port;
			}
			WMSUtils.ClearUserCookie(cookieName);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003E5C File Offset: 0x0000205C
		public static void ClearUserCookie(string cookieName)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.Values.Clear();
			httpCookie.Expires = DateTime.Now.AddYears(-1);
			string text = SysConfigs.GetConfig().cookiedomain.Trim();
			if (text != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(text.TrimStart(new char[]
			{
				'.'
			})) > -1 && FPUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
			{
				httpCookie.Domain = text;
			}
			HttpContext.Current.Response.AppendCookie(httpCookie);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003F0B File Offset: 0x0000210B
		public static bool IsValidDomain(string host)
		{
			return host.IndexOf(".") != -1 && !new Regex("^\\d+$").IsMatch(host.Replace(".", string.Empty));
		}
	}
}
