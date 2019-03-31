using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x02000010 RID: 16
	public class DES
	{
		// Token: 0x0600010C RID: 268 RVA: 0x0000B95E File Offset: 0x00009B5E
		public static string Encode(string encryptString)
		{
			return DES.Encode(encryptString, "FANGPAGE");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000B96C File Offset: 0x00009B6C
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

		// Token: 0x0600010E RID: 270 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		public static string Decode(string decryptString)
		{
			return DES.Decode(decryptString, "FANGPAGE");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000BA00 File Offset: 0x00009C00
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

		// Token: 0x04000023 RID: 35
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
