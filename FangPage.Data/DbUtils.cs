using System;
using System.Text.RegularExpressions;
using System.Web;

namespace FangPage.Data
{
	// Token: 0x0200000C RID: 12
	public class DbUtils
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005464 File Offset: 0x00003664
		public static DateTime GetDateTime()
		{
			return Convert.ToDateTime(DateTime.Now.ToString());
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000548C File Offset: 0x0000368C
		public static string[] SplitString(string strContent, string strSplit)
		{
			if (strContent.IndexOf(strSplit) < 0)
			{
				return new string[]
				{
					strContent
				};
			}
			return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000054C0 File Offset: 0x000036C0
		public static string[] SplitString(string strContent, string strSplit, int p_3)
		{
			string[] array = new string[p_3];
			string[] array2 = DbUtils.SplitString(strContent, strSplit);
			for (int i = 0; i < p_3; i++)
			{
				if (i < array2.Length)
				{
					array[i] = array2[i];
				}
				else
				{
					array[i] = string.Empty;
				}
			}
			return array;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005500 File Offset: 0x00003700
		public static string GetMapPath(string strPath)
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.Server.MapPath(strPath);
			}
			if (strPath.StartsWith("/"))
			{
				strPath = strPath.Substring(1, strPath.Length - 1);
			}
			return AppDomain.CurrentDomain.BaseDirectory + strPath.Replace("/", "\\");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005564 File Offset: 0x00003764
		public static int StrToInt(object Expression, int defValue)
		{
			if (Expression != null)
			{
				string text = Expression.ToString();
				if (text.Length > 0 && text.Length <= 11 && Regex.IsMatch(text, "^[-]?[0-9]*$") && (text.Length < 10 || (text.Length == 10 && text[0] == '1') || (text.Length == 11 && text[0] == '-' && text[1] == '1')))
				{
					return Convert.ToInt32(text);
				}
			}
			return defValue;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000055E2 File Offset: 0x000037E2
		public static int StrToInt(object Expression)
		{
			return DbUtils.StrToInt(Expression, 0);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000055EC File Offset: 0x000037EC
		public static decimal StrToDecimal(object strValue, decimal defValue)
		{
			if (strValue == null || strValue.ToString().Length > 10)
			{
				return defValue;
			}
			decimal result = defValue;
			if (strValue != null)
			{
				bool flag = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
				if (flag)
				{
					result = Convert.ToDecimal(strValue);
				}
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000562E File Offset: 0x0000382E
		public static decimal StrToDecimal(object strValue)
		{
			return DbUtils.StrToDecimal(strValue, 0.00m);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005640 File Offset: 0x00003840
		public static string RegEsc(string str)
		{
			string[] array = new string[]
			{
				"%",
				"_",
				"'"
			};
			foreach (string text in array)
			{
				string a;
				if ((a = text) != null)
				{
					if (!(a == "%"))
					{
						if (!(a == "_"))
						{
							if (a == "'")
							{
								str = str.Replace(text, "['']");
							}
						}
						else
						{
							str = str.Replace(text, "[_]");
						}
					}
					else
					{
						str = str.Replace(text, "[%]");
					}
				}
			}
			return str;
		}
	}
}
