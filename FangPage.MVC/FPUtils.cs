using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FangPage.MVC
{
	// Token: 0x02000010 RID: 16
	public class FPUtils
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000A264 File Offset: 0x00008464
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

		// Token: 0x060000BD RID: 189 RVA: 0x0000A2CC File Offset: 0x000084CC
		public static string GetMapPath(string strPath)
		{
			string result;
			if (HttpContext.Current != null)
			{
				result = HttpContext.Current.Server.MapPath(strPath);
			}
			else
			{
				if (strPath.StartsWith("/"))
				{
					strPath = strPath.Substring(1, strPath.Length - 1);
				}
				result = AppDomain.CurrentDomain.BaseDirectory + strPath.Replace("/", "\\");
			}
			return result;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000A344 File Offset: 0x00008544
		public static string[] SplitString(string strContent)
		{
			return FPUtils.SplitString(strContent, ",");
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000A364 File Offset: 0x00008564
		public static string[] SplitString(string strContent, string strSplit)
		{
			string[] result;
			if (strContent.IndexOf(strSplit) < 0)
			{
				string[] array = new string[]
				{
					strContent
				};
				result = array;
			}
			else
			{
				result = Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A3A8 File Offset: 0x000085A8
		public static string[] SplitString(string strContent, string strSplit, int p_3)
		{
			string[] array = new string[p_3];
			string[] array2 = FPUtils.SplitString(strContent, strSplit);
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

		// Token: 0x060000C1 RID: 193 RVA: 0x0000A400 File Offset: 0x00008600
		public static int[] SplitInt(string strContent)
		{
			return FPUtils.SplitInt(strContent, ",");
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000A420 File Offset: 0x00008620
		public static int[] SplitInt(string strContent, string strSplit)
		{
			string[] array = FPUtils.SplitString(strContent, strSplit);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = FPUtils.StrToInt(array[i]);
			}
			return array2;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A468 File Offset: 0x00008668
		public static int[] SplitInt(string strContent, string strSplit, int p_3)
		{
			int[] array = new int[p_3];
			string[] array2 = FPUtils.SplitString(strContent, strSplit);
			for (int i = 0; i < p_3; i++)
			{
				if (i < array2.Length)
				{
					array[i] = FPUtils.StrToInt(array2[i]);
				}
				else
				{
					array[i] = 0;
				}
			}
			return array;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public static string CutString(string str, int len)
		{
			Regex regex = new Regex("^[\\u4e00-\\u9fa5]+$", RegexOptions.Compiled);
			char[] array = str.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				if (regex.IsMatch(array[i].ToString()))
				{
					num += 2;
				}
				else
				{
					num++;
				}
				if (num > len)
				{
					break;
				}
				stringBuilder.Append(array[i]);
			}
			if (stringBuilder.ToString() != str)
			{
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A578 File Offset: 0x00008778
		public static bool InArray(int id, string stringArray)
		{
			return FPUtils.InArray(id, stringArray, ",");
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000A598 File Offset: 0x00008798
		public static bool InArray(string str, string stringArray)
		{
			return FPUtils.InArray(str, stringArray, ",");
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public static bool InArray(int id, string stringArray, string strsplit)
		{
			return FPUtils.InArray(id.ToString(), stringArray, strsplit);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public static bool InArray(string str, string stringArray, string strsplit)
		{
			if (stringArray != null && stringArray != "")
			{
				string[] array = FPUtils.SplitString(stringArray, strsplit);
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

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A638 File Offset: 0x00008838
		public static bool InIPArray(string ip, string[] iparray)
		{
			string[] array = FPUtils.SplitString(ip, ".");
			int i = 0;
			while (i < iparray.Length)
			{
				string[] array2 = FPUtils.SplitString(iparray[i], ".");
				int num = 0;
				for (int j = 0; j < array2.Length; j++)
				{
					if (array2[j] == "*")
					{
						return true;
					}
					if (array.Length <= j)
					{
						break;
					}
					if (!(array2[j] == array[j]))
					{
						break;
					}
					num++;
				}
				if (num != 4)
				{
					i++;
					continue;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A708 File Offset: 0x00008908
		public static string[] DelArraySame(string[] TempArray)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < TempArray.Length; i++)
			{
				if (!arrayList.Contains(TempArray[i]))
				{
					arrayList.Add(TempArray[i]);
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A768 File Offset: 0x00008968
		public static string UrlEncode(string str)
		{
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A780 File Offset: 0x00008980
		public static string UrlDecode(string str)
		{
			return HttpUtility.UrlDecode(str);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A798 File Offset: 0x00008998
		public static string GetDate()
		{
			return DateTime.Now.ToString("yyyy-MM-dd");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000A7BC File Offset: 0x000089BC
		public static string GetDate(string datetimestr, string replacestr)
		{
			string result;
			if (string.IsNullOrEmpty(datetimestr))
			{
				result = "";
			}
			else
			{
				try
				{
					datetimestr = Convert.ToDateTime(datetimestr).ToString(replacestr).Replace("1900-01-01", "");
				}
				catch
				{
				}
				result = datetimestr;
			}
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000A820 File Offset: 0x00008A20
		public static string GetDate(DateTime datetime, string replacestr)
		{
			string datetimestr = datetime.ToString();
			return FPUtils.GetDate(datetimestr, replacestr);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A848 File Offset: 0x00008A48
		public static string GetDateTime()
		{
			return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000A86C File Offset: 0x00008A6C
		public static string GetDateTime(string fDateTime, string formatStr)
		{
			string result;
			if (fDateTime == "0000-0-0 0:00:00")
			{
				result = fDateTime;
			}
			else
			{
				result = Convert.ToDateTime(fDateTime).ToString(formatStr);
			}
			return result;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		public static string GetDateTime(string fDateTime)
		{
			string result = "";
			try
			{
				result = FPUtils.GetDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
			}
			catch
			{
				result = FPUtils.GetDateTime();
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public static string RemoveHtml(string content)
		{
			string pattern = "<[^>]*>";
			return Regex.Replace(content, pattern, string.Empty, RegexOptions.IgnoreCase);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A914 File Offset: 0x00008B14
		public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
		{
			for (int i = 0; i < stringArray.Length; i++)
			{
				if (caseInsensetive)
				{
					if (strSearch.ToLower() == stringArray[i].ToLower())
					{
						return i;
					}
				}
				else if (strSearch == stringArray[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A980 File Offset: 0x00008B80
		public static bool StrToBool(object Expression, bool defValue)
		{
			if (Expression != null)
			{
				if (string.Compare(Expression.ToString(), "true", true) == 0)
				{
					return true;
				}
				if (string.Compare(Expression.ToString(), "false", true) == 0)
				{
					return false;
				}
			}
			return defValue;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public static int StrToInt(object Expression)
		{
			return FPUtils.StrToInt(Expression, 0);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000A9FC File Offset: 0x00008BFC
		public static int StrToInt(object Expression, int defValue)
		{
			if (Expression != null)
			{
				string text = Expression.ToString();
				if (text.Length > 0 && text.Length <= 11 && Regex.IsMatch(text, "^[-]?[0-9]*$"))
				{
					if (text.Length < 10 || (text.Length == 10 && text[0] == '1') || (text.Length == 11 && text[0] == '-' && text[1] == '1'))
					{
						return Convert.ToInt32(text);
					}
				}
			}
			return defValue;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public static float StrToFloat(object strValue)
		{
			return FPUtils.StrToFloat(strValue, 0f);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public static float StrToFloat(object strValue, float defValue)
		{
			float result;
			if (strValue == null || strValue.ToString().Length > 10)
			{
				result = defValue;
			}
			else
			{
				float num = defValue;
				if (strValue != null)
				{
					bool flag = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
					if (flag)
					{
						num = Convert.ToSingle(strValue);
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public static decimal StrToDecimal(object strValue)
		{
			return FPUtils.StrToDecimal(strValue, 0.00m);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000AB50 File Offset: 0x00008D50
		public static decimal StrToDecimal(object strValue, decimal defValue)
		{
			decimal result;
			if (strValue == null || strValue.ToString().Length > 10)
			{
				result = defValue;
			}
			else
			{
				decimal num = defValue;
				if (strValue != null)
				{
					bool flag = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
					if (flag)
					{
						num = Convert.ToDecimal(strValue);
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public static double StrToDouble(object strValue)
		{
			return FPUtils.StrToDouble(strValue, 0.0);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public static double StrToDouble(object strValue, double defValue)
		{
			double result;
			if (strValue == null || strValue.ToString().Length > 10)
			{
				result = defValue;
			}
			else
			{
				double num = defValue;
				if (strValue != null)
				{
					bool flag = Regex.IsMatch(strValue.ToString(), "^([-]|[0-9])[0-9]*(\\.\\w*)?$");
					if (flag)
					{
						num = Convert.ToDouble(strValue);
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public static string FormatBytesStr(long bytes)
		{
			string result;
			if (bytes >= 1073741824L)
			{
				result = string.Format("{0:F}", (double)bytes / 1073741824.0) + " G";
			}
			else if (bytes >= 1048576L)
			{
				result = string.Format("{0:F}", (double)bytes / 1048576.0) + " M";
			}
			else if (bytes >= 1024L)
			{
				result = string.Format("{0:F}", (double)bytes / 1024.0) + " K";
			}
			else
			{
				result = bytes.ToString() + " Bytes";
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000AD04 File Offset: 0x00008F04
		public static void CopyDirectory(string sourcePath, string targetPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				Directory.CreateDirectory(targetPath + "\\" + directoryInfo2.Name);
				FPUtils.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public static bool IsNumeric(object Expression)
		{
			if (Expression != null)
			{
				string text = Expression.ToString();
				if (text.Length > 0 && text.Length <= 11 && Regex.IsMatch(text, "^[-]?[0-9]*[.]?[0-9]*$"))
				{
					if (text.Length < 10 || (text.Length == 10 && text[0] == '1') || (text.Length == 11 && text[0] == '-' && text[1] == '1'))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000AE60 File Offset: 0x00009060
		public static bool IsNumericArray(string[] strNumber)
		{
			bool result;
			if (strNumber == null)
			{
				result = false;
			}
			else if (strNumber.Length < 1)
			{
				result = false;
			}
			else
			{
				foreach (string expression in strNumber)
				{
					if (!FPUtils.IsNumeric(expression))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000AEC8 File Offset: 0x000090C8
		public static bool IsNumericArray(string strNumber)
		{
			bool result;
			if (strNumber == null)
			{
				result = false;
			}
			else if (strNumber.Length < 1)
			{
				result = false;
			}
			else
			{
				foreach (string expression in strNumber.Split(new char[]
				{
					','
				}))
				{
					if (!FPUtils.IsNumeric(expression))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000AF48 File Offset: 0x00009148
		public static bool IsIP(string ip)
		{
			return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000AF68 File Offset: 0x00009168
		public static bool IsSafeSqlString(string str)
		{
			return !Regex.IsMatch(str, "[-|;|,|\\/|\\(|\\)|\\[|\\]|\\}|\\{|%|@|\\*|!|\\']");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000AF88 File Offset: 0x00009188
		public static bool IsEmail(string strEmail)
		{
			return Regex.IsMatch(strEmail, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
		}
	}
}
