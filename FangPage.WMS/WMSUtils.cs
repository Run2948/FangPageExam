using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using FangPage.MVC;

namespace FangPage.WMS
{
	// Token: 0x02000011 RID: 17
	public class WMSUtils
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static string CreateAuthStr(int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < len; i++)
			{
				int num = random.Next();
				if (num % 2 == 0)
				{
					stringBuilder.Append((char)(48 + (ushort)(num % 10)));
				}
				else
				{
					stringBuilder.Append((char)(65 + (ushort)(num % 26)));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004B4C File Offset: 0x00002D4C
		public static string CreateAuthStr(int len, bool OnlyNum)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < len; i++)
			{
				int num;
				if (!OnlyNum)
				{
					num = WMSUtils.verifycodeRandom.Next(0, WMSUtils.verifycodeRange.Length);
				}
				else
				{
					num = WMSUtils.verifycodeRandom.Next(0, 10);
				}
				stringBuilder.Append(WMSUtils.verifycodeRange[num]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004BBC File Offset: 0x00002DBC
		public static bool IsValidDomain(string host)
		{
			return host.IndexOf(".") != -1 && !new Regex("^\\d+$").IsMatch(host.Replace(".", string.Empty));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004C10 File Offset: 0x00002E10
		public static long DirSize(string strPath)
		{
			long result;
			try
			{
				DirectoryInfo d = new DirectoryInfo(FPUtils.GetMapPath(strPath));
				result = WMSUtils.DirSize(d);
			}
			catch
			{
				result = 0L;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004C50 File Offset: 0x00002E50
		public static long DirSize(DirectoryInfo d)
		{
			long num = 0L;
			FileInfo[] files = d.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				num += fileInfo.Length;
			}
			DirectoryInfo[] directories = d.GetDirectories();
			foreach (DirectoryInfo d2 in directories)
			{
				num += WMSUtils.DirSize(d2);
			}
			return num;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public static string GetTextFromHTML(string HTML)
		{
			Regex regex = new Regex("</?(?!br|img)[^>]*>", RegexOptions.IgnoreCase);
			return regex.Replace(HTML, "");
		}

		// Token: 0x04000021 RID: 33
		private static string[] verifycodeRange = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
			"a",
			"b",
			"c",
			"d",
			"e",
			"f",
			"g",
			"h",
			"j",
			"k",
			"m",
			"n",
			"p",
			"q",
			"r",
			"s",
			"t",
			"u",
			"v",
			"w",
			"x",
			"y"
		};

		// Token: 0x04000022 RID: 34
		private static Random verifycodeRandom = new Random();
	}
}
