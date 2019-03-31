using System;
using System.Security.Cryptography;
using System.Text;

namespace FangPage.Common
{
	// Token: 0x02000011 RID: 17
	public class FPRandom
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000551C File Offset: 0x0000371C
		public static string CreateCode(int len)
		{
			string text = string.Empty;
			long num = (long)FPRandom.GetRandomSeed();
			Random random = new Random((int)(num & -1) | (int)(num >> 32));
			for (int i = 0; i < len; i++)
			{
				int num2 = random.Next();
				bool flag = num2 % 2 == 0;
				char c;
				if (flag)
				{
					c = (char)(48 + (ushort)(num2 % 10));
				}
				else
				{
					c = (char)(65 + (ushort)(num2 % 26));
				}
				text += c.ToString();
			}
			return text;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000055A4 File Offset: 0x000037A4
		public static string CreateCode(string prefix, int len)
		{
			string empty = string.Empty;
			bool flag = string.IsNullOrEmpty(prefix);
			if (flag)
			{
				prefix = "";
			}
			return prefix + FPRandom.CreateCode(len);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000055E0 File Offset: 0x000037E0
		public static string CreateCodeNum(int len)
		{
			string text = string.Empty;
			long num = (long)FPRandom.GetRandomSeed();
			Random random = new Random((int)(num & -1) | (int)(num >> 32));
			for (int i = 0; i < len; i++)
			{
				int num2 = random.Next();
				text += ((char)(48 + (ushort)(num2 % 10))).ToString();
			}
			return text;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000564C File Offset: 0x0000384C
		public static string CreateCodeNum(string prefix, int len)
		{
			string empty = string.Empty;
			bool flag = string.IsNullOrEmpty(prefix);
			if (flag)
			{
				prefix = "";
			}
			return prefix + FPRandom.CreateCodeNum(len);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005688 File Offset: 0x00003888
		public static string CreateAuth(int len)
		{
			StringBuilder stringBuilder = new StringBuilder();
			long num = (long)FPRandom.GetRandomSeed();
			Random random = new Random((int)(num & -1) | (int)(num >> 32));
			for (int i = 0; i < len; i++)
			{
				int num2 = random.Next();
				bool flag = num2 % 2 == 0;
				if (flag)
				{
					stringBuilder.Append((char)(48 + (ushort)(num2 % 10)));
				}
				else
				{
					stringBuilder.Append((char)(65 + (ushort)(num2 % 26)));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005714 File Offset: 0x00003914
		public static string CreateGuid()
		{
			return Guid.NewGuid().ToString();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000573C File Offset: 0x0000393C
		private static int GetRandomSeed()
		{
			byte[] array = new byte[4];
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(array);
			int num = BitConverter.ToInt32(array, 0);
			bool flag = num < 0;
			if (flag)
			{
				num *= -1;
			}
			return num;
		}

		// Token: 0x04000027 RID: 39
		private static readonly string[] verifycodeRange = new string[]
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
	}
}
