using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FangPage.Data
{
	// Token: 0x0200000E RID: 14
	public class DbUtils
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000B274 File Offset: 0x00009474
		public static string MD5(string str)
		{
			byte[] array = Encoding.Default.GetBytes(str);
			array = new MD5CryptoServiceProvider().ComputeHash(array);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x").PadLeft(2, '0');
			}
			return text;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public static DateTime GetDateTime()
		{
			return Convert.ToDateTime(DateTime.Now.ToString());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000B2EF File Offset: 0x000094EF
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

		// Token: 0x060000EF RID: 239 RVA: 0x0000B313 File Offset: 0x00009513
		public static string[] SplitString(string strContent, string[] strSplit)
		{
			if (strSplit.Length == 0)
			{
				return new string[]
				{
					strContent
				};
			}
			return strContent.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000B32C File Offset: 0x0000952C
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

		// Token: 0x060000F1 RID: 241 RVA: 0x0000B36C File Offset: 0x0000956C
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

		// Token: 0x060000F2 RID: 242 RVA: 0x0000B3D0 File Offset: 0x000095D0
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

		// Token: 0x060000F3 RID: 243 RVA: 0x0000B44E File Offset: 0x0000964E
		public static int StrToInt(object Expression)
		{
			return DbUtils.StrToInt(Expression, 0);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000B458 File Offset: 0x00009658
		public static decimal StrToDecimal(object strValue, decimal defValue)
		{
			if (strValue == null || strValue.ToString().Length > 10)
			{
				return defValue;
			}
			decimal result = defValue;
			if (strValue != null && Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$"))
			{
				result = Convert.ToDecimal(strValue);
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000B498 File Offset: 0x00009698
		public static decimal StrToDecimal(object strValue)
		{
			return DbUtils.StrToDecimal(strValue, 0.00m);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000B4AA File Offset: 0x000096AA
		public static bool InArray(int id, string stringArray)
		{
			return DbUtils.InArray(id, stringArray, ",");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public static bool InArray(string str, string stringArray)
		{
			return DbUtils.InArray(str, stringArray, ",");
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000B4C6 File Offset: 0x000096C6
		public static bool InArray(int id, string stringArray, string strsplit)
		{
			return DbUtils.InArray(id.ToString(), stringArray, strsplit);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public static bool InArray(string str, string stringArray, string strsplit)
		{
			if (stringArray != null && stringArray != "")
			{
				string[] array = DbUtils.SplitString(stringArray, strsplit);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == str)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000B519 File Offset: 0x00009719
		public static string PushString(string strContent, int push)
		{
			return DbUtils.PushString(strContent, push.ToString(), ',');
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000B52A File Offset: 0x0000972A
		public static string PushString(string strContent, string push)
		{
			return DbUtils.PushString(strContent, push, ',');
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000B535 File Offset: 0x00009735
		public static string PushString(string strContent, int push, char separator)
		{
			return DbUtils.PushString(strContent, push.ToString(), separator);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000B548 File Offset: 0x00009748
		public static string PushString(string strContent, string push, char separator)
		{
			strContent = strContent.TrimStart(new char[]
			{
				separator
			}).TrimEnd(new char[]
			{
				separator
			});
			if (!string.IsNullOrEmpty(push))
			{
				string text = "";
				foreach (string text2 in DbUtils.SplitString(push, separator.ToString()))
				{
					if (!DbUtils.InArray(text2, strContent, separator.ToString()))
					{
						if (text != "")
						{
							text += separator.ToString();
						}
						text += text2;
					}
				}
				if (text != "")
				{
					if (strContent != "")
					{
						strContent += separator.ToString();
					}
					strContent += text;
				}
			}
			return strContent;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000B610 File Offset: 0x00009810
		public static string FormatInString(string strContent)
		{
			string text = "";
			if (string.IsNullOrEmpty(strContent))
			{
				return "";
			}
			bool flag = true;
			if ((strContent.StartsWith("'") && strContent.EndsWith("'")) || (strContent.StartsWith("\"") && strContent.EndsWith("\"")))
			{
				strContent = strContent.TrimStart(new char[]
				{
					'\''
				}).TrimEnd(new char[]
				{
					'\''
				}).TrimStart(new char[]
				{
					'"'
				}).TrimEnd(new char[]
				{
					'"'
				});
				flag = false;
			}
			strContent = strContent.TrimStart(new char[]
			{
				','
			}).TrimEnd(new char[]
			{
				','
			});
			if (!DbUtils.IsNumericArray(strContent))
			{
				flag = false;
			}
			if (flag)
			{
				foreach (string text2 in DbUtils.SplitString(strContent, ","))
				{
					if (text2 != "")
					{
						text = DbUtils.PushString(text, text2);
					}
				}
				return text;
			}
			foreach (string text3 in DbUtils.SplitString(strContent, ","))
			{
				if (text3 != "")
				{
					text = DbUtils.PushString(text, "'" + text3 + "'");
				}
			}
			return text;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000B75C File Offset: 0x0000995C
		public static bool IsNumeric(object Expression)
		{
			if (Expression != null)
			{
				string text = Expression.ToString();
				if (text.Length > 0 && text.Length <= 11 && Regex.IsMatch(text, "^[-]?[0-9]*[.]?[0-9]*$") && (text.Length < 10 || (text.Length == 10 && text[0] == '1') || (text.Length == 11 && text[0] == '-' && text[1] == '1')))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public static bool IsNumericArray(string strNumber)
		{
			if (strNumber == null)
			{
				return false;
			}
			if (strNumber.Length < 1)
			{
				return false;
			}
			foreach (string text in strNumber.Split(new char[]
			{
				','
			}))
			{
				if (!DbUtils.IsNumeric(text) && text != "")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000B834 File Offset: 0x00009A34
		public static string RegEsc(string str)
		{
			foreach (string text in new string[]
			{
				"%",
				"_",
				"'"
			})
			{
				if (!(text == "%"))
				{
					if (!(text == "_"))
					{
						if (text == "'")
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
			return str;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000B8CC File Offset: 0x00009ACC
		public static string GetWhereTypeString(WhereType wheretype)
		{
			string result = "";
			switch (wheretype)
			{
			case WhereType.Equal:
				result = "=";
				break;
			case WhereType.NotEqual:
				result = "<>";
				break;
			case WhereType.GreaterThan:
				result = ">";
				break;
			case WhereType.GreaterThanEqual:
				result = ">=";
				break;
			case WhereType.LessThan:
				result = "<";
				break;
			case WhereType.LessThanEqual:
				result = "<=";
				break;
			case WhereType.Like:
				result = "LIKE";
				break;
			case WhereType.NotLike:
				result = "NOT LIKE";
				break;
			case WhereType.In:
				result = "IN";
				break;
			case WhereType.NotIn:
				result = "NOT IN";
				break;
			}
			return result;
		}
	}
}
