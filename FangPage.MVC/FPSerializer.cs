using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FangPage.MVC
{
	// Token: 0x02000004 RID: 4
	public class FPSerializer
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002600 File Offset: 0x00000800
		private FPSerializer()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000260C File Offset: 0x0000080C
		public static T Load<T>(string filename) where T : new()
		{
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			if (File.Exists(filename))
			{
				Type typeFromHandle = typeof(T);
				lock (FPSerializer.lockHelper)
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

		// Token: 0x0600000B RID: 11 RVA: 0x000026D8 File Offset: 0x000008D8
		public static bool Save<T>(string filename) where T : new()
		{
			T obj = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			return FPSerializer.Save<T>(obj, filename);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002714 File Offset: 0x00000914
		public static bool Save<T>(T obj, string filename)
		{
			bool result = false;
			if (!Directory.Exists(Path.GetDirectoryName(filename)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
			}
			if (File.Exists(filename) && File.GetAttributes(filename).ToString().IndexOf("ReadOnly") != -1)
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
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000027F4 File Offset: 0x000009F4
		public static string Serialize<T>() where T : new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			string result = "";
			XmlSerializer xmlSerializer = new XmlSerializer(t.GetType());
			MemoryStream memoryStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = null;
			StreamReader streamReader = null;
			try
			{
				xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlSerializer.Serialize(xmlTextWriter, t);
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
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
				if (streamReader != null)
				{
					streamReader.Close();
				}
				memoryStream.Close();
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private static object lockHelper = new object();
	}
}
