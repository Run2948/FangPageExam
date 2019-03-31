using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x0200000F RID: 15
	public class FPRequest
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000A178 File Offset: 0x00008378
		public static HttpFileCollection Files
		{
			get
			{
				return HttpContext.Current.Request.Files;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A189 File Offset: 0x00008389
		public static bool IsPost()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("POST");
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A1A4 File Offset: 0x000083A4
		public static bool IsGet()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("GET");
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A1C0 File Offset: 0x000083C0
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

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A215 File Offset: 0x00008415
		public static string GetServerString(string strName)
		{
			if (HttpContext.Current.Request.ServerVariables[strName] == null)
			{
				return "";
			}
			return HttpContext.Current.Request.ServerVariables[strName].ToString();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A250 File Offset: 0x00008450
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
			if (text == null)
			{
				return "";
			}
			return text;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A294 File Offset: 0x00008494
		public static string GetDomain()
		{
			HttpRequest request = HttpContext.Current.Request;
			string text = request.Url.Host;
			if (text == "127.0.0.1")
			{
				text = "localhost";
			}
			if (!request.Url.IsDefaultPort)
			{
				return string.Format("http://{0}:{1}", text, request.Url.Port.ToString());
			}
			return "http://" + text;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000A302 File Offset: 0x00008502
		public static int GetPort()
		{
			return HttpContext.Current.Request.Url.Port;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000A318 File Offset: 0x00008518
		public static string GetHost()
		{
			return HttpContext.Current.Request.Url.Host;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000A32E File Offset: 0x0000852E
		public static string GetRawUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000A340 File Offset: 0x00008540
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

		// Token: 0x060000DC RID: 220 RVA: 0x0000A3BB File Offset: 0x000085BB
		public static string GetUrl()
		{
			return HttpContext.Current.Request.Url.ToString();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000A3D1 File Offset: 0x000085D1
		public static string GetQueryString(string strName)
		{
			if (HttpContext.Current.Request.QueryString[strName] == null)
			{
				return "";
			}
			return HttpContext.Current.Request.QueryString[strName];
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000A405 File Offset: 0x00008605
		public static string GetPageName()
		{
			string[] array = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[]
			{
				'/'
			});
			return array[array.Length - 1].ToLower();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000A436 File Offset: 0x00008636
		public static int GetParamCount()
		{
			return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000A461 File Offset: 0x00008661
		public static string GetFormString(string strName)
		{
			if (HttpContext.Current.Request.Form[strName] == null)
			{
				return "";
			}
			return HttpContext.Current.Request.Form[strName];
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000A495 File Offset: 0x00008695
		public static string GetString(string strName)
		{
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				return FPRequest.GetQueryString(strName);
			}
			return FPRequest.GetFormString(strName);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public static string GetString(string strName, string defValue)
		{
			string @string = FPRequest.GetString(strName);
			if (@string == "")
			{
				return defValue;
			}
			return @string;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000A4DC File Offset: 0x000086DC
		public static int GetQueryInt(string strName, int defValue)
		{
			string queryString = FPRequest.GetQueryString(strName);
			if (FPUtils.IsNumericArray(queryString))
			{
				return FPArray.SplitInt(queryString, 1)[0];
			}
			return FPUtils.StrToInt(queryString, defValue);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000A50C File Offset: 0x0000870C
		public static int GetFormInt(string strName, int defValue)
		{
			string formString = FPRequest.GetFormString(strName);
			if (FPUtils.IsNumericArray(formString))
			{
				return FPArray.SplitInt(formString, 1)[0];
			}
			return FPUtils.StrToInt(formString, defValue);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000A539 File Offset: 0x00008739
		public static int GetInt(string strName, int defValue)
		{
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				return FPRequest.GetQueryInt(strName, defValue);
			}
			return FPRequest.GetFormInt(strName, defValue);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000A55C File Offset: 0x0000875C
		public static int GetInt(string strName)
		{
			return FPRequest.GetInt(strName, 0);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000A565 File Offset: 0x00008765
		public static string GetIntString(string strName)
		{
			return FPArray.FmatInt(FPRequest.GetString(strName));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000A574 File Offset: 0x00008774
		public static DateTime GetDateTime(string strName)
		{
			string text = FPRequest.GetString(strName);
			if (strName.ToLower() == "startdate")
			{
				text = FPUtils.FormatDateTime(text, "yyyy-MM-dd 00:00:00");
			}
			else if (strName.ToLower() == "enddate")
			{
				text = FPUtils.FormatDateTime(text, "yyyy-MM-dd 23:59:59");
			}
			else
			{
				text = FPUtils.FormatDateTime(text);
			}
			return FPUtils.StrToDateTime(text);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000A5D5 File Offset: 0x000087D5
		public static DateTime GetDateTime(string strName, string format)
		{
			return FPUtils.StrToDateTime(FPUtils.FormatDateTime(FPRequest.GetString(strName), format));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public static DateTime? GetDateTime2(string strName)
		{
			string text = FPRequest.GetString(strName);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			if (strName.ToLower() == "startdate")
			{
				text = FPUtils.FormatDateTime(text, "yyyy-MM-dd 00:00:00");
			}
			else if (strName.ToLower() == "enddate")
			{
				text = FPUtils.FormatDateTime(text, "yyyy-MM-dd 23:59:59");
			}
			else
			{
				text = FPUtils.FormatDateTime(text);
			}
			return FPUtils.StrToDateTime2(text);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A65B File Offset: 0x0000885B
		public static DateTime? GetDateTime2(string strName, string format)
		{
			return FPUtils.StrToDateTime2(FPUtils.FormatDateTime(FPRequest.GetString(strName), format));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000A66E File Offset: 0x0000886E
		public static float GetQueryFloat(string strName, float defValue)
		{
			return FPUtils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A68B File Offset: 0x0000888B
		public static float GetFormFloat(string strName, float defValue)
		{
			return FPUtils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000A6A8 File Offset: 0x000088A8
		public static float GetFloat(string strName, float defValue)
		{
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				return FPRequest.GetQueryFloat(strName, defValue);
			}
			return FPRequest.GetFormFloat(strName, defValue);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A6CB File Offset: 0x000088CB
		public static float GetFloat(string strName)
		{
			return FPRequest.GetFloat(strName, 0f);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public static decimal GetQueryDecimal(string strName, decimal defValue)
		{
			return FPUtils.StrToDecimal(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A6F5 File Offset: 0x000088F5
		public static decimal GetFormDecimal(string strName, decimal defValue)
		{
			return FPUtils.StrToDecimal(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A712 File Offset: 0x00008912
		public static decimal GetDecimal(string strName, decimal defValue)
		{
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				return FPRequest.GetQueryDecimal(strName, defValue);
			}
			return FPRequest.GetFormDecimal(strName, defValue);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A735 File Offset: 0x00008935
		public static decimal GetDecimal(string strName)
		{
			return FPRequest.GetDecimal(strName, 0m);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A742 File Offset: 0x00008942
		public static double GetQueryDouble(string strName, double defValue)
		{
			return FPUtils.StrToDouble(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A75F File Offset: 0x0000895F
		public static double GetFormDouble(string strName, double defValue)
		{
			return FPUtils.StrToDouble(HttpContext.Current.Request.Form[strName], defValue);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A77C File Offset: 0x0000897C
		public static double GetDouble(string strName, float defValue)
		{
			if ("".Equals(FPRequest.GetFormString(strName)))
			{
				return FPRequest.GetQueryDouble(strName, (double)defValue);
			}
			return FPRequest.GetFormDouble(strName, (double)defValue);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A7A1 File Offset: 0x000089A1
		public static double GetDouble(string strName)
		{
			return FPRequest.GetDouble(strName, 0f);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A7AE File Offset: 0x000089AE
		public static T GetModel<T>() where T : new()
		{
			return FPRequest.GetModel<T>("");
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A7BA File Offset: 0x000089BA
		public static T GetModel<T>(T model)
		{
			return FPRequest.GetModel<T>(model, "");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public static T GetModel<T>(string prefix) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
			{
				if (propertyInfo != null && propertyInfo.CanWrite)
				{
					string text = prefix + propertyInfo.Name;
					if (text.ToLower() == prefix + "pageurl")
					{
						propertyInfo.SetValue(t, FPRequest.GetRawUrl(), null);
					}
					else if (propertyInfo.PropertyType == typeof(FPData))
					{
						FPData fpdata = FPRequest.GetFPData(text, propertyInfo.GetValue(t, null) as FPData);
						propertyInfo.SetValue(t, fpdata, null);
					}
					else if (propertyInfo.PropertyType == typeof(List<FPData>))
					{
						List<FPData> fplist = FPRequest.GetFPList(text);
						propertyInfo.SetValue(t, fplist, null);
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
						else if (propertyInfo.PropertyType == typeof(DateTime?))
						{
							propertyInfo.SetValue(t, FPRequest.GetDateTime2(text), null);
						}
					}
				}
			}
			return t;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000AA74 File Offset: 0x00008C74
		public static T GetModel<T>(T model, string prefix)
		{
			foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
			{
				if (propertyInfo != null && propertyInfo.CanWrite)
				{
					string text = prefix + propertyInfo.Name;
					if (text.ToLower() == prefix + "pageurl")
					{
						propertyInfo.SetValue(model, FPRequest.GetRawUrl(), null);
					}
					else if (propertyInfo.PropertyType == typeof(FPData))
					{
						FPData fpdata = propertyInfo.GetValue(model, null) as FPData;
						foreach (object obj in propertyInfo.GetCustomAttributes(true))
						{
							if (obj is CheckBox)
							{
								CheckBox checkBox = obj as CheckBox;
								if (checkBox.IsCheckBox)
								{
									if (checkBox.CheckName != "")
									{
										foreach (string key in FPArray.SplitString(checkBox.CheckName))
										{
											fpdata[key] = "";
										}
									}
									else
									{
										foreach (string key2 in fpdata.Keys)
										{
											fpdata[key2] = "";
										}
									}
								}
							}
						}
						fpdata = FPRequest.GetFPData(text, fpdata);
						propertyInfo.SetValue(model, fpdata, null);
					}
					else if (propertyInfo.PropertyType == typeof(List<FPData>))
					{
						List<FPData> fplist = FPRequest.GetFPList(text);
						propertyInfo.SetValue(model, fplist, null);
					}
					else if (HttpContext.Current.Request.QueryString[text] == null && HttpContext.Current.Request.Form[text] == null)
					{
						foreach (object obj2 in propertyInfo.GetCustomAttributes(true))
						{
							if (obj2 is CheckBox && (obj2 as CheckBox).IsCheckBox)
							{
								if (propertyInfo.PropertyType == typeof(int))
								{
									propertyInfo.SetValue(model, 0, null);
								}
								else
								{
									propertyInfo.SetValue(model, "", null);
								}
							}
						}
					}
					else if (propertyInfo.PropertyType == typeof(string))
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
					else if (propertyInfo.PropertyType == typeof(DateTime?))
					{
						propertyInfo.SetValue(model, FPRequest.GetDateTime2(text), null);
					}
				}
			}
			return model;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000AE14 File Offset: 0x00009014
		public static List<T> GetList<T>() where T : new()
		{
			return FPRequest.GetList<T>("");
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000AE20 File Offset: 0x00009020
		public static List<T> GetList<T>(string prefix) where T : new()
		{
			List<T> list = new List<T>();
			Type typeFromHandle = typeof(T);
			int num = 0;
			bool flag = true;
			while (flag)
			{
				T t = Activator.CreateInstance<T>();
				bool flag2 = false;
				foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
				{
					string text = string.Concat(new string[]
					{
						prefix,
						propertyInfo.Name,
						"[",
						num.ToString(),
						"]"
					});
					if (propertyInfo.PropertyType == typeof(FPData))
					{
						FPData fpdata = FPRequest.GetFPData(text, propertyInfo.GetValue(t, null) as FPData);
						if (fpdata.Count > 0)
						{
							propertyInfo.SetValue(t, fpdata, null);
							flag2 = true;
						}
					}
					else if (propertyInfo.PropertyType == typeof(List<FPData>))
					{
						List<FPData> fplist = FPRequest.GetFPList(text);
						if (fplist.Count > 0)
						{
							propertyInfo.SetValue(t, fplist, null);
							flag2 = true;
						}
					}
					else if (HttpContext.Current.Request.QueryString[text] != null || HttpContext.Current.Request.Form[text] != null)
					{
						flag2 = true;
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
						else if (propertyInfo.PropertyType == typeof(DateTime?))
						{
							propertyInfo.SetValue(t, FPRequest.GetDateTime2(text), null);
						}
					}
				}
				if (!flag2)
				{
					break;
				}
				list.Add(t);
				num++;
			}
			return list;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000B130 File Offset: 0x00009330
		public static FPData GetFPData(string name)
		{
			FPData fpdata = new FPData();
			Regex regex = new Regex("((((?:\\s*)\\[([^\\[\\]\\{\\}\\s]+)\\])(?:\\s*)))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			string a = "";
			MatchCollection matchCollection = regex.Matches(name);
			if (matchCollection.Count > 0)
			{
				a = matchCollection[matchCollection.Count - 1].Groups[4].ToString();
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					name = name.Replace(match.Groups[0].ToString(), "");
				}
			}
			Regex regex2 = new Regex("((((?:\\s*)" + name + "\\[([^\\[\\]\\{\\}\\s]+)\\])(?:\\s*)))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			foreach (object obj2 in HttpContext.Current.Request.Params.Keys)
			{
				string text = (string)obj2;
				MatchCollection matchCollection2 = regex2.Matches(text);
				if (matchCollection2.Count > 0)
				{
					if (a != "")
					{
						if (matchCollection2.Count > 1)
						{
							string b = matchCollection2[matchCollection2.Count - 2].Groups[4].ToString();
							if (a == b)
							{
								fpdata[matchCollection2[matchCollection2.Count - 1].Groups[4].ToString()] = FPRequest.GetString(text);
							}
						}
					}
					else
					{
						fpdata[matchCollection2[matchCollection2.Count - 1].Groups[4].ToString()] = FPRequest.GetString(text);
					}
				}
			}
			return fpdata;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000B31C File Offset: 0x0000951C
		public static FPData GetFPData(string name, FPData fpdata)
		{
			Regex regex = new Regex("((((?:\\s*)\\[([^\\[\\]\\{\\}\\s]+)\\])(?:\\s*)))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			string a = "";
			MatchCollection matchCollection = regex.Matches(name);
			if (matchCollection.Count > 0)
			{
				a = matchCollection[matchCollection.Count - 1].Groups[4].ToString();
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					name = name.Replace(match.Groups[0].ToString(), "");
				}
			}
			Regex regex2 = new Regex("((((?:\\s*)" + name + "\\[([^\\[\\]\\{\\}\\s]+)\\])(?:\\s*)))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			foreach (object obj2 in HttpContext.Current.Request.Params.Keys)
			{
				string text = (string)obj2;
				MatchCollection matchCollection2 = regex2.Matches(text);
				if (matchCollection2.Count > 0)
				{
					if (a != "")
					{
						if (matchCollection2.Count > 1)
						{
							string b = matchCollection2[matchCollection2.Count - 2].Groups[4].ToString();
							if (a == b)
							{
								fpdata[matchCollection2[matchCollection2.Count - 1].Groups[4].ToString()] = FPRequest.GetString(text);
							}
						}
					}
					else
					{
						fpdata[matchCollection2[matchCollection2.Count - 1].Groups[4].ToString()] = FPRequest.GetString(text);
					}
				}
			}
			return fpdata;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public static FPData GetFPData(FPData fpdata)
		{
			foreach (KeyValuePair<string, string> keyValuePair in fpdata.Data)
			{
				string key = keyValuePair.Key;
				if (HttpContext.Current.Request.QueryString[key] != null || HttpContext.Current.Request.Form[key] != null)
				{
					fpdata[keyValuePair.Key] = FPRequest.GetString(key);
				}
			}
			return fpdata;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000B590 File Offset: 0x00009790
		public static List<FPData> GetFPList(string name)
		{
			List<FPData> list = new List<FPData>();
			int num = 0;
			bool flag = true;
			while (flag)
			{
				FPData fpdata = FPRequest.GetFPData(string.Concat(new object[]
				{
					name,
					"[",
					num,
					"]"
				}));
				if (fpdata.Count == 0)
				{
					break;
				}
				list.Add(fpdata);
				num++;
			}
			return list;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public static List<FPData> GetFPList(FPData fpdata)
		{
			List<FPData> list = new List<FPData>();
			int num = 0;
			bool flag = true;
			while (flag)
			{
				FPData fpdata2 = new FPData();
				foreach (KeyValuePair<string, string> keyValuePair in fpdata.Data)
				{
					string text = string.Concat(new object[]
					{
						keyValuePair.Key,
						"[",
						num,
						"]"
					});
					if (HttpContext.Current.Request.QueryString[text] != null || HttpContext.Current.Request.Form[text] != null)
					{
						fpdata2[keyValuePair.Key] = FPRequest.GetString(text);
					}
				}
				if (fpdata.Count == 0)
				{
					flag = false;
					break;
				}
				list.Add(fpdata);
				num++;
			}
			return list;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000B6EC File Offset: 0x000098EC
		public static string GetContent()
		{
			string result = "";
			HttpContext.Current.Response.ContentType = "application/json";
			HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			using (StreamReader streamReader = new StreamReader(HttpContext.Current.Request.InputStream))
			{
				result = HttpUtility.UrlDecode(streamReader.ReadToEnd());
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000B768 File Offset: 0x00009968
		public static List<T> GetToList<T>() where T : new()
		{
			return FPJson.ToList<T>(FPRequest.GetContent());
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000B774 File Offset: 0x00009974
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

		// Token: 0x06000106 RID: 262 RVA: 0x0000B82C File Offset: 0x00009A2C
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
					if (text == "127.0.0.1" || text == "0.0.0.0")
					{
						text = FPUtils.GetIntranetIp();
					}
					result = text;
				}
			}
			catch
			{
				result = "0.0.0.0";
			}
			return result;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public static string GetServerIIS()
		{
			string result;
			try
			{
				string text = string.Empty;
				text = HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"];
				if (string.IsNullOrEmpty(text))
				{
					result = "";
				}
				else
				{
					result = text;
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
