using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FangPage.Common
{
	// Token: 0x02000012 RID: 18
	public class FPSerializer
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000058AC File Offset: 0x00003AAC
		private FPSerializer()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000058B8 File Offset: 0x00003AB8
		public static T Load<T>(string filename) where T : new()
		{
			T result = Activator.CreateInstance<T>();
			bool flag = File.Exists(filename);
			if (flag)
			{
				Type typeFromHandle = typeof(T);
				object obj = FPSerializer.lockHelper;
				lock (obj)
				{
					using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeFromHandle);
						result = (T)((object)xmlSerializer.Deserialize(fileStream));
						fileStream.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000595C File Offset: 0x00003B5C
		public static bool Save<T>(string filename) where T : new()
		{
			T obj = Activator.CreateInstance<T>();
			return FPSerializer.Save<T>(obj, filename);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000597C File Offset: 0x00003B7C
		public static bool Save<T>(T obj, string filename)
		{
			bool result = false;
			bool flag = !Directory.Exists(Path.GetDirectoryName(filename));
			if (flag)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
			}
			bool flag2 = File.Exists(filename) && File.GetAttributes(filename).ToString().IndexOf("ReadOnly") != -1;
			if (flag2)
			{
				File.SetAttributes(filename, FileAttributes.Normal);
			}
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
				XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
				xmlSerializer.Serialize(fileStream, obj);
				result = true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				bool flag3 = fileStream != null;
				if (flag3)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005A64 File Offset: 0x00003C64
		public static string Serialize<T>(T obj) where T : new()
		{
			bool flag = obj == null;
			if (flag)
			{
				obj = Activator.CreateInstance<T>();
			}
			string result = "";
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			MemoryStream memoryStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = null;
			StreamReader streamReader = null;
			try
			{
				xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlSerializer.Serialize(xmlTextWriter, obj);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				streamReader = new StreamReader(memoryStream);
				result = streamReader.ReadToEnd();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				bool flag2 = xmlTextWriter != null;
				if (flag2)
				{
					xmlTextWriter.Close();
				}
				bool flag3 = streamReader != null;
				if (flag3)
				{
					streamReader.Close();
				}
				memoryStream.Close();
			}
			return result;
		}

		// Token: 0x04000028 RID: 40
		private static object lockHelper = new object();
	}
}
