using System;
using System.Reflection;
using System.Web;

namespace FangPage.MVC
{
	// Token: 0x0200000E RID: 14
	public class FPRequest
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00008934 File Offset: 0x00006B34
		public static HttpFileCollection Files
		{
			get
			{
				return HttpContext.Current.Request.Files;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00008958 File Offset: 0x00006B58
		public static bool IsPost()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("POST");
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00008984 File Offset: 0x00006B84
		public static bool IsGet()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("GET");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000089B0 File Offset: 0x00006BB0
		public static bool IsPostFile()
		{
			for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
			{
				if (HttpContext.Current.Request.Files[i].FileName != "")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00008A18 File Offset: 0x00006C18
		public static string GetServerString(string strName)
		{
			string result;
			if (HttpContext.Current.Request.ServerVariables[strName] == null)
			{
				result = "";
			}
			else
			{
				result = HttpContext.Current.Request.ServerVariables[strName].ToString();
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00008A6C File Offset: 0x00006C6C
		public static string GetUrlReferrer()
		{
			string text = null;
			try
			{
				text = HttpContext.Current.Request.UrlReferrer.ToString();
			}
			catch
			{
			}
			string result;
			if (text == null)
			{
				result = "";
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public static string GetCurrentFullHost()
		{
			HttpRequest request = HttpContext.Current.Request;
			string result;
			if (!request.Url.IsDefaultPort)
			{
				result = string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
			}
			else
			{
				result = request.Url.Host;
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00008B28 File Offset: 0x00006D28
		public static string GetHost()
		{
			return HttpContext.Current.Request.Url.Host;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00008B50 File Offset: 0x00006D50
		public static string GetRawUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00008B74 File Offset: 0x00006D74
		public static bool IsBrowserGet()
		{
			string[] array = new string[]
			{
				"ie",
				"opera",
				"netscape",
				"mozilla",
				"konqueror",
				"firefox"
			};
			string text = HttpContext.Current.Request.Browser.Type.ToLower();
			for (int i = 0; i < array.Length; i++)
			{
				if (text.IndexOf(array[i]) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00008C10 File Offset: 0x00006E10
		public static string GetUrl()
		{
			return HttpContext.Current.Request.Url.ToString();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00008C38 File Offset: 0x00006E38
		public static string GetQueryString(string strName)
		{
			string result;
			if (HttpContext.Current.Request.QueryString[strName] == null)
			{
				result = "";
			}
			else
			{
				result = HttpContext.Current.Request.QueryString[strName];
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00008C88 File Offset: 0x00006E88
		public static string GetPageName()
		{
			string[] array = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[]
			{
				'/'
			});
			return array[array.Length - 1].ToLower();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public static int GetParamCount()
		{
			return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00008D0C File Offset: 0x00006F0C
		public static string GetFormString(string strName)
		{
			string result;
			if (HttpContext.Current.Request.Form[strName] == null)
			{
				result = "";
			}
			else
			{
				result = HttpContext.Current.Request.Form[strName];
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00008D5C File Offset: 0x00006F5C
		public static string GetString(string strName)
		{
			string result;
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				result = FPRequest.GetQueryString(strName);
			}
			else
			{
				result = FPRequest.GetFormString(strName);
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00008D98 File Offset: 0x00006F98
		public static string GetString(string strName, string defValue)
		{
			string @string = FPRequest.GetString(strName);
			string result;
			if (@string == "")
			{
				result = defValue;
			}
			else
			{
				result = @string;
			}
			return result;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00008DCC File Offset: 0x00006FCC
		public static int GetQueryInt(string strName, int defValue)
		{
			return FPUtils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00008DFC File Offset: 0x00006FFC
		public static int GetFormInt(string strName, int defValue)
		{
			return FPUtils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00008E2C File Offset: 0x0000702C
		public static int GetInt(string strName, int defValue)
		{
			int result;
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				result = FPRequest.GetQueryInt(strName, defValue);
			}
			else
			{
				result = FPRequest.GetFormInt(strName, defValue);
			}
			return result;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00008E68 File Offset: 0x00007068
		public static int GetInt(string strName)
		{
			return FPRequest.GetInt(strName, 0);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00008E84 File Offset: 0x00007084
		public static DateTime GetDateTime(string strName)
		{
			string dateTime = FPUtils.GetDateTime(FPRequest.GetString(strName));
			return Convert.ToDateTime(dateTime);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008EA8 File Offset: 0x000070A8
		public static DateTime GetDateTime()
		{
			string dateTime = FPUtils.GetDateTime();
			return Convert.ToDateTime(dateTime);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008EC8 File Offset: 0x000070C8
		public static float GetQueryFloat(string strName, float defValue)
		{
			return FPUtils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00008EF8 File Offset: 0x000070F8
		public static float GetFormFloat(string strName, float defValue)
		{
			return FPUtils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00008F28 File Offset: 0x00007128
		public static float GetFloat(string strName, float defValue)
		{
			float result;
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				result = FPRequest.GetQueryFloat(strName, defValue);
			}
			else
			{
				result = FPRequest.GetFormFloat(strName, defValue);
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00008F64 File Offset: 0x00007164
		public static float GetFloat(string strName)
		{
			return FPRequest.GetFloat(strName, 0f);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00008F84 File Offset: 0x00007184
		public static decimal GetQueryDecimal(string strName, decimal defValue)
		{
			return FPUtils.StrToDecimal(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00008FB4 File Offset: 0x000071B4
		public static decimal GetFormDecimal(string strName, decimal defValue)
		{
			return FPUtils.StrToDecimal(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00008FE4 File Offset: 0x000071E4
		public static decimal GetDecimal(string strName, decimal defValue)
		{
			decimal result;
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				result = FPRequest.GetQueryDecimal(strName, defValue);
			}
			else
			{
				result = FPRequest.GetFormDecimal(strName, defValue);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00009020 File Offset: 0x00007220
		public static decimal GetDecimal(string strName)
		{
			return FPRequest.GetDecimal(strName, 0m);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00009040 File Offset: 0x00007240
		public static double GetQueryDouble(string strName, double defValue)
		{
			return FPUtils.StrToDouble(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00009070 File Offset: 0x00007270
		public static double GetFormDouble(string strName, double defValue)
		{
			return FPUtils.StrToDouble(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000090A0 File Offset: 0x000072A0
		public static double GetDouble(string strName, float defValue)
		{
			double result;
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				result = FPRequest.GetQueryDouble(strName, (double)defValue);
			}
			else
			{
				result = FPRequest.GetFormDouble(strName, (double)defValue);
			}
			return result;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000090E0 File Offset: 0x000072E0
		public static double GetDouble(string strName)
		{
			return FPRequest.GetDouble(strName, 0f);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00009100 File Offset: 0x00007300
		public static T GetModel<T>() where T : new()
		{
			return FPRequest.GetModel<T>("");
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000911C File Offset: 0x0000731C
		public static T GetModel<T>(T model)
		{
			return FPRequest.GetModel<T>(model, "");
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000913C File Offset: 0x0000733C
		public static T GetModel<T>(string prefix) where T : new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			Type typeFromHandle = typeof(T);
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (propertyInfo != null && propertyInfo.CanWrite)
				{
					string text = prefix + propertyInfo.Name;
					if (text.ToLower() == prefix + "pageurl")
					{
						propertyInfo.SetValue(t, FPRequest.GetRawUrl(), null);
					}
					else if (HttpContext.Current.Request.QueryString[text] != null || HttpContext.Current.Request.Form[text] != null)
					{
						if (propertyInfo.PropertyType == typeof(string))
						{
							propertyInfo.SetValue(t, FPRequest.GetString(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(int))
						{
							propertyInfo.SetValue(t, FPRequest.GetInt(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(short))
						{
							propertyInfo.SetValue(t, short.Parse(FPRequest.GetInt(text).ToString()), null);
						}
						else if (propertyInfo.PropertyType == typeof(DateTime))
						{
							propertyInfo.SetValue(t, FPRequest.GetDateTime(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(decimal))
						{
							propertyInfo.SetValue(t, FPRequest.GetDecimal(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(float))
						{
							propertyInfo.SetValue(t, FPRequest.GetFloat(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(double))
						{
							propertyInfo.SetValue(t, FPRequest.GetDouble(text), null);
						}
					}
				}
			}
			return t;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000093EC File Offset: 0x000075EC
		public static T GetModel<T>(T model, string prefix)
		{
			Type type = model.GetType();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (propertyInfo != null && propertyInfo.CanWrite)
				{
					string text = prefix + propertyInfo.Name;
					if (text.ToLower() == prefix + "pageurl")
					{
						propertyInfo.SetValue(model, FPRequest.GetRawUrl(), null);
					}
					else if (HttpContext.Current.Request.QueryString[text] != null || HttpContext.Current.Request.Form[text] != null)
					{
						if (propertyInfo.PropertyType == typeof(string))
						{
							propertyInfo.SetValue(model, FPRequest.GetString(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(int))
						{
							propertyInfo.SetValue(model, FPRequest.GetInt(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(DateTime))
						{
							propertyInfo.SetValue(model, FPRequest.GetDateTime(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(decimal))
						{
							propertyInfo.SetValue(model, FPRequest.GetDecimal(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(float))
						{
							propertyInfo.SetValue(model, FPRequest.GetFloat(text), null);
						}
						else if (propertyInfo.PropertyType == typeof(double))
						{
							propertyInfo.SetValue(model, FPRequest.GetDouble(text), null);
						}
					}
				}
			}
			return model;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00009630 File Offset: 0x00007830
		public static string GetIP()
		{
			string result;
			try
			{
				string text = string.Empty;
				text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
				if (text == null || text == string.Empty)
				{
					text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
				}
				if (text == null || text == string.Empty)
				{
					text = HttpContext.Current.Request.UserHostAddress;
				}
				if (text == null || text == string.Empty || !FPUtils.IsIP(text))
				{
					result = "0.0.0.0";
				}
				else
				{
					result = text;
				}
			}
			catch
			{
				result = "0.0.0.0";
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00009708 File Offset: 0x00007908
		public static string GetServerIP()
		{
			string result;
			try
			{
				string text = string.Empty;
				text = HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
				if (text == null || text == string.Empty || !FPUtils.IsIP(text))
				{
					result = "0.0.0.0";
				}
				else
				{
					result = text;
				}
			}
			catch
			{
				result = "0.0.0.0";
			}
			return result;
		}
	}
}
