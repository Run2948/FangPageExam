using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x0200000D RID: 13
	public class DES
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000056F0 File Offset: 0x000038F0
		public static string Encode(string encryptString)
		{
			return DES.Encode(encryptString, "FANGPAGE");
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005700 File Offset: 0x00003900
		public static string Encode(string encryptString, string encryptKey)
		{
			if (encryptKey.Length > 8)
			{
				encryptKey = encryptKey.Substring(0, 8);
			}
			encryptKey = encryptKey.PadRight(8, ' ');
			byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
			byte[] keys = DES.Keys;
			byte[] bytes2 = Encoding.UTF8.GetBytes(encryptString);
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
			cryptoStream.Write(bytes2, 0, bytes2.Length);
			cryptoStream.FlushFinalBlock();
			return Convert.ToBase64String(memoryStream.ToArray());
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000578E File Offset: 0x0000398E
		public static string Decode(string decryptString)
		{
			return DES.Decode(decryptString, "FANGPAGE");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000579C File Offset: 0x0000399C
		public static string Decode(string decryptString, string decryptKey)
		{
			string result;
			try
			{
				if (decryptKey.Length > 8)
				{
					decryptKey = decryptKey.Substring(0, 8);
				}
				decryptKey = decryptKey.PadRight(8, ' ');
				byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
				byte[] keys = DES.Keys;
				byte[] array = Convert.FromBase64String(decryptString);
				DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				result = Encoding.UTF8.GetString(memoryStream.ToArray());
			}
			catch
			{
				result = "";
			}
			return result;
		}

		// Token: 0x04000015 RID: 21
		private static byte[] Keys = new byte[]
		{
			18,
			52,
			86,
			120,
			144,
			171,
			205,
			239
		};
	}
}
