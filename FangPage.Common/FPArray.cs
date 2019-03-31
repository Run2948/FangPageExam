using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace FangPage.Common
{
	// Token: 0x02000009 RID: 9
	public class FPArray
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002434 File Offset: 0x00000634
		public static string[] SplitString(string strContent)
		{
			return FPArray.SplitString(strContent, ",");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002454 File Offset: 0x00000654
		public static string[] SplitString(string strContent, string separator)
		{
			bool flag = string.IsNullOrEmpty(strContent);
			if (flag)
			{
				strContent = "";
			}
			bool flag2 = strContent.IndexOf(separator) < 0;
			string[] result;
			if (flag2)
			{
				string[] array = new string[]
				{
					strContent
				};
				result = array;
			}
			else
			{
				result = Regex.Split(strContent, Regex.Escape(separator), RegexOptions.IgnoreCase);
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024A4 File Offset: 0x000006A4
		public static string[] SplitString(string strContent, int length)
		{
			return FPArray.SplitString(strContent, ",", length);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024C4 File Offset: 0x000006C4
		public static string[] SplitString(string strContent, string separator, int length)
		{
			bool flag = string.IsNullOrEmpty(strContent);
			if (flag)
			{
				strContent = "";
			}
			string[] array = new string[length];
			string[] array2 = FPArray.SplitString(strContent, separator);
			for (int i = 0; i < length; i++)
			{
				bool flag2 = i < array2.Length;
				if (flag2)
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

		// Token: 0x06000029 RID: 41 RVA: 0x0000252C File Offset: 0x0000072C
		public static string[] SplitString(string strContent, string[] separator)
		{
			bool flag = string.IsNullOrEmpty(strContent);
			if (flag)
			{
				strContent = "";
			}
			bool flag2 = separator.Length == 0;
			string[] result;
			if (flag2)
			{
				string[] array = new string[]
				{
					strContent
				};
				result = array;
			}
			else
			{
				result = strContent.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002574 File Offset: 0x00000774
		public static string[] SplitString(string strContent, string[] separator, int length)
		{
			string[] array = new string[length];
			string[] array2 = FPArray.SplitString(strContent, separator);
			for (int i = 0; i < length; i++)
			{
				bool flag = i < array2.Length;
				if (flag)
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

		// Token: 0x0600002B RID: 43 RVA: 0x000025C8 File Offset: 0x000007C8
		public static int[] SplitInt(string strContent)
		{
			return FPArray.SplitInt(strContent, ",");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025E8 File Offset: 0x000007E8
		public static int[] SplitInt(string strContent, int length)
		{
			return FPArray.SplitInt(strContent, ",", length);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002608 File Offset: 0x00000808
		public static int[] SplitInt(string strContent, string separator)
		{
			string[] array = FPArray.SplitString(strContent, separator);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = FPUtils.StrToInt(array[i]);
			}
			return array2;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002650 File Offset: 0x00000850
		public static int[] SplitInt(string strContent, string separator, int length)
		{
			int[] array = new int[length];
			string[] array2 = FPArray.SplitString(strContent, separator);
			for (int i = 0; i < length; i++)
			{
				bool flag = i < array2.Length;
				if (flag)
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

		// Token: 0x0600002F RID: 47 RVA: 0x000026A4 File Offset: 0x000008A4
		public static int[] SplitInt(string strContent, string[] separator)
		{
			string[] array = FPArray.SplitString(strContent, separator);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = FPUtils.StrToInt(array[i]);
			}
			return array2;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026EC File Offset: 0x000008EC
		public static int[] SplitInt(string strContent, string[] separator, int length)
		{
			int[] array = new int[length];
			string[] array2 = FPArray.SplitString(strContent, separator);
			for (int i = 0; i < length; i++)
			{
				bool flag = i < array2.Length;
				if (flag)
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

		// Token: 0x06000031 RID: 49 RVA: 0x00002740 File Offset: 0x00000940
		public static string Join(string[] strContent)
		{
			return FPArray.Join(strContent, "");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002760 File Offset: 0x00000960
		public static string Join(string[] strContent, string separator)
		{
			bool flag = string.IsNullOrEmpty(separator);
			if (flag)
			{
				separator = ",";
			}
			bool flag2 = strContent.Length == 0;
			string result;
			if (flag2)
			{
				result = "";
			}
			else
			{
				result = string.Join(separator, strContent);
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000027A0 File Offset: 0x000009A0
		public static string Join(int[] strContent)
		{
			return FPArray.Join(strContent, "");
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027C0 File Offset: 0x000009C0
		public static string Join(int[] strContent, string separator)
		{
			bool flag = string.IsNullOrEmpty(separator);
			if (flag)
			{
				separator = ",";
			}
			bool flag2 = strContent.Length == 0;
			string result;
			if (flag2)
			{
				result = "";
			}
			else
			{
				string text = "";
				foreach (int item in strContent)
				{
					text = FPArray.Append(text, item, separator);
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002828 File Offset: 0x00000A28
		public static string Append(string strContent, string item)
		{
			return FPArray.Append(strContent, item, ",");
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002848 File Offset: 0x00000A48
		public static string Append(string strContent, int item)
		{
			return FPArray.Append(strContent, item, ",");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002868 File Offset: 0x00000A68
		public static string Append(string strContent, int item, string separator)
		{
			return FPArray.Append(strContent, item.ToString(), separator);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002888 File Offset: 0x00000A88
		public static string Append(string strContent, string item, string separator)
		{
			bool flag = string.IsNullOrEmpty(strContent);
			if (flag)
			{
				strContent = "";
			}
			bool flag2 = separator.Length == 1;
			if (flag2)
			{
				char c = Convert.ToChar(separator);
				strContent = strContent.TrimStart(new char[]
				{
					c
				}).TrimEnd(new char[]
				{
					c
				});
				item = item.TrimStart(new char[]
				{
					c
				}).TrimEnd(new char[]
				{
					c
				});
			}
			bool flag3 = !string.IsNullOrEmpty(item);
			if (flag3)
			{
				bool flag4 = !string.IsNullOrEmpty(strContent);
				if (flag4)
				{
					strContent += separator;
				}
				strContent += item;
			}
			return strContent;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000293C File Offset: 0x00000B3C
		public static string Push(string strContent, string item)
		{
			return FPArray.Push(strContent, item, ",");
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000295C File Offset: 0x00000B5C
		public static string Push(string strContent, int item)
		{
			return FPArray.Push(strContent, item, ",");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000297C File Offset: 0x00000B7C
		public static string Push(string strContent, int item, string separator)
		{
			return FPArray.Push(strContent, item.ToString(), separator);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000299C File Offset: 0x00000B9C
		public static string Push(string strContent, string item, string separator)
		{
			bool flag = string.IsNullOrEmpty(strContent);
			if (flag)
			{
				strContent = "";
			}
			bool flag2 = separator.Length == 1;
			if (flag2)
			{
				char c = Convert.ToChar(separator);
				strContent = strContent.TrimStart(new char[]
				{
					c
				}).TrimEnd(new char[]
				{
					c
				});
				item = item.TrimStart(new char[]
				{
					c
				}).TrimEnd(new char[]
				{
					c
				});
			}
			bool flag3 = !string.IsNullOrEmpty(item);
			if (flag3)
			{
				foreach (string item2 in FPArray.SplitString(item, separator))
				{
					bool flag4 = FPArray.InArray(item2, strContent, separator.ToString()) == -1;
					if (flag4)
					{
						strContent = FPArray.Append(strContent, item2, separator);
					}
				}
			}
			return strContent;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A78 File Offset: 0x00000C78
		public static int InArray(string item, string[] stringArray)
		{
			for (int i = 0; i < stringArray.Length; i++)
			{
				bool flag = stringArray[i] == item;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public static int InArray(int item, string[] stringArray)
		{
			for (int i = 0; i < stringArray.Length; i++)
			{
				bool flag = FPUtils.StrToInt(stringArray[i]) == item;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static int InArray(int item, string stringArray)
		{
			return FPArray.InArray(item, stringArray, ",");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B10 File Offset: 0x00000D10
		public static int InArray(string item, string stringArray)
		{
			return FPArray.InArray(item, stringArray, ",");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B30 File Offset: 0x00000D30
		public static int InArray(int item, string stringArray, string separator)
		{
			return FPArray.InArray(item.ToString(), stringArray, separator);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B50 File Offset: 0x00000D50
		public static int InArray(string item, string stringArray, string separator)
		{
			bool flag = stringArray != null && stringArray != "";
			if (flag)
			{
				string[] array = FPArray.SplitString(stringArray, separator);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = array[i] == item;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public static int InArray(string item, string stringArray, string[] separator)
		{
			bool flag = stringArray != null && stringArray != "";
			if (flag)
			{
				string[] array = FPArray.SplitString(stringArray, separator);
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = array[i] == item;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C10 File Offset: 0x00000E10
		public static bool InIPArray(string ip, string[] iparray)
		{
			string[] array = FPArray.SplitString(ip, ".");
			int i = 0;
			while (i < iparray.Length)
			{
				string[] array2 = FPArray.SplitString(iparray[i], ".");
				int num = 0;
				for (int j = 0; j < array2.Length; j++)
				{
					bool flag = array2[j] == "*";
					if (flag)
					{
						return true;
					}
					bool flag2 = array.Length > j;
					if (!flag2)
					{
						break;
					}
					bool flag3 = array2[j] == array[j];
					if (!flag3)
					{
						break;
					}
					num++;
				}
				bool flag4 = num == 4;
				if (!flag4)
				{
					i++;
					continue;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public static bool InMobileArray(string mobile, string[] mobilearray)
		{
			for (int i = 0; i < mobilearray.Length; i++)
			{
				bool flag = mobilearray[i].IndexOf('*') > 0;
				if (flag)
				{
					string value = mobile.Substring(0, mobilearray[i].IndexOf('*'));
					bool flag2 = mobile.StartsWith(value);
					if (flag2)
					{
						return true;
					}
				}
				else
				{
					bool flag3 = mobilearray[i] == mobile;
					if (flag3)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D50 File Offset: 0x00000F50
		public static bool Contain(string[] stringArray, string item)
		{
			return FPArray.InArray(item, stringArray) >= 0;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D70 File Offset: 0x00000F70
		public static bool Contain(string[] stringArray, int item)
		{
			return FPArray.InArray(item, stringArray) >= 0;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D90 File Offset: 0x00000F90
		public static bool Contain(string stringArray, string item)
		{
			return FPArray.InArray(item, stringArray) >= 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public static bool Contain(string stringArray, int item)
		{
			return FPArray.InArray(item, stringArray) >= 0;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public static bool Contain(string stringArray, string item, string separator)
		{
			return FPArray.InArray(item, stringArray, separator) >= 0;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public static bool Contain(string stringArray, int item, string separator)
		{
			return FPArray.InArray(item, stringArray, separator) >= 0;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E10 File Offset: 0x00001010
		public static bool Contain(string stringArray, string item, string[] separator)
		{
			return FPArray.InArray(item, stringArray, separator) >= 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E30 File Offset: 0x00001030
		public static string Replace(string strContent, string item, string value)
		{
			string text = "";
			foreach (string a in FPArray.SplitString(strContent))
			{
				bool flag = a == item;
				if (flag)
				{
					text = FPArray.Push(text, value);
				}
				else
				{
					text = FPArray.Push(text, item);
				}
			}
			return text;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8C File Offset: 0x0000108C
		public static string Replace(string strContent, string value, int index)
		{
			return FPArray.Replace(strContent, value, index, ",");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EAC File Offset: 0x000010AC
		public static string Replace(string strContent, int value, int index)
		{
			return FPArray.Replace(strContent, value, index, ",");
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002ECC File Offset: 0x000010CC
		public static string Replace(string strContent, int value, int index, string separator)
		{
			return FPArray.Replace(strContent, value.ToString(), index, separator);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static string Replace(string strContent, string value, int index, string separator)
		{
			string text = "";
			int num = 0;
			foreach (string str in FPArray.SplitString(strContent, separator))
			{
				bool flag = num > 0;
				if (flag)
				{
					text += separator;
				}
				bool flag2 = num == index;
				if (flag2)
				{
					text += value;
				}
				else
				{
					text += str;
				}
				num++;
			}
			return text;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F64 File Offset: 0x00001164
		public static string Update(string strContent, int value, int index)
		{
			return FPArray.Update(strContent, value, index, ",");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F84 File Offset: 0x00001184
		public static string Update(string strContent, int value, int index, string separator)
		{
			string text = "";
			int num = 0;
			foreach (int num2 in FPArray.SplitInt(strContent, separator))
			{
				bool flag = num > 0;
				if (flag)
				{
					text += separator;
				}
				bool flag2 = num == index;
				if (flag2)
				{
					text += (num2 + value).ToString();
				}
				else
				{
					text += num2.ToString();
				}
				num++;
			}
			return text;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000300C File Offset: 0x0000120C
		public static string GetString(string strContent, int index)
		{
			return FPArray.GetString(strContent, index, ",");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000302C File Offset: 0x0000122C
		public static string GetString(string strContent, int index, string separator)
		{
			string[] array = FPArray.SplitString(strContent, separator);
			bool flag = index > -1 && index < array.Length;
			string result;
			if (flag)
			{
				result = array[index];
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003064 File Offset: 0x00001264
		public static string GetString(string strContent, int index, string[] separator)
		{
			string[] array = FPArray.SplitString(strContent, separator);
			bool flag = index > -1 && index < array.Length;
			string result;
			if (flag)
			{
				result = array[index];
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000309C File Offset: 0x0000129C
		public static string GetString(string strContent, string strValue, string item)
		{
			int num = FPArray.InArray(item, strContent);
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				result = FPArray.GetString(strValue, num);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000030D4 File Offset: 0x000012D4
		public static string GetString(string strContent, string strValue, string item, string separator)
		{
			int num = FPArray.InArray(item, strContent, separator);
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				result = FPArray.GetString(strValue, num, separator);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000310C File Offset: 0x0000130C
		public static string GetString(string strContent, string strValue, string item, string[] separator)
		{
			int num = FPArray.InArray(item, strContent, separator);
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				result = FPArray.GetString(strValue, num, separator);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003144 File Offset: 0x00001344
		public static int GetInt(string strContent, int index)
		{
			return FPArray.GetInt(strContent, index, ",");
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003164 File Offset: 0x00001364
		public static int GetInt(string strContent, int index, string separator)
		{
			int[] array = FPArray.SplitInt(strContent, separator);
			bool flag = index > -1 && index < array.Length;
			int result;
			if (flag)
			{
				result = array[index];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003198 File Offset: 0x00001398
		public static int GetInt(string strContent, int index, string[] separator)
		{
			int[] array = FPArray.SplitInt(strContent, separator);
			bool flag = index > -1 && index < array.Length;
			int result;
			if (flag)
			{
				result = array[index];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031CC File Offset: 0x000013CC
		public static int GetInt(string strContent, string strValue, string item)
		{
			int num = FPArray.InArray(item, strContent);
			bool flag = num >= 0;
			int result;
			if (flag)
			{
				result = FPArray.GetInt(strValue, num);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003200 File Offset: 0x00001400
		public static int GetInt(string strContent, string strValue, string item, string separator)
		{
			int num = FPArray.InArray(item, strContent, separator);
			bool flag = num >= 0;
			int result;
			if (flag)
			{
				result = FPArray.GetInt(strValue, num, separator);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003234 File Offset: 0x00001434
		public static int GetInt(string strContent, string strValue, string item, string[] separator)
		{
			int num = FPArray.InArray(item, strContent, separator);
			bool flag = num >= 0;
			int result;
			if (flag)
			{
				result = FPArray.GetInt(strValue, num, separator);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003268 File Offset: 0x00001468
		public static string FmatInt(string strContent)
		{
			string text = "";
			foreach (string text2 in FPArray.SplitString(strContent))
			{
				bool flag = FPUtils.IsNumeric(text2);
				if (flag)
				{
					text = FPArray.Push(text, text2);
				}
			}
			return text;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032B8 File Offset: 0x000014B8
		public static string Remove(string strContent, int index)
		{
			return FPArray.Remove(strContent, index, ",");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032D8 File Offset: 0x000014D8
		public static string Remove(string strContent, string remove)
		{
			return FPArray.Remove(strContent, remove, ",");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032F8 File Offset: 0x000014F8
		public static string Remove(string strContent, string remove, string separator)
		{
			string text = "";
			foreach (string item in FPArray.SplitString(strContent, separator))
			{
				bool flag = FPArray.InArray(item, remove, separator) == -1;
				if (flag)
				{
					text = FPArray.Append(text, item, separator);
				}
			}
			return text;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000334C File Offset: 0x0000154C
		public static string Remove(string strContent, int index, string separator)
		{
			string text = "";
			int num = 0;
			foreach (string item in FPArray.SplitString(strContent, separator))
			{
				bool flag = num != index;
				if (flag)
				{
					text = FPArray.Append(text, item, separator);
				}
				num++;
			}
			return text;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033A4 File Offset: 0x000015A4
		public static string RemoveSame(string TempArray)
		{
			string[] array = FPArray.RemoveSame(FPArray.SplitString(TempArray));
			bool flag = array.Length != 0;
			string result;
			if (flag)
			{
				result = string.Join(",", array);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000033E0 File Offset: 0x000015E0
		public static string RemoveSame(string TempArray, string separator)
		{
			string[] array = FPArray.RemoveSame(FPArray.SplitString(TempArray, separator));
			bool flag = array.Length != 0;
			string result;
			if (flag)
			{
				result = string.Join(separator, array);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000341C File Offset: 0x0000161C
		public static string[] RemoveSame(string[] TempArray)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < TempArray.Length; i++)
			{
				bool flag = !arrayList.Contains(TempArray[i]);
				if (flag)
				{
					arrayList.Add(TempArray[i]);
				}
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
	}
}
