using System;
using System.IO;
using System.Text;
using System.Web;

namespace FangPage.MVC
{
	// Token: 0x02000003 RID: 3
	public class FPFile
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002158 File Offset: 0x00000358
		public static void WriteFile(string filename, string content)
		{
			if (!Directory.Exists(Path.GetDirectoryName(filename)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
			}
			if (File.Exists(filename) && File.GetAttributes(filename).ToString().IndexOf("ReadOnly") != -1)
			{
				File.SetAttributes(filename, FileAttributes.Normal);
			}
			using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(content);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Close();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000220C File Offset: 0x0000040C
		public static string ReadFile(string filename)
		{
			string result;
			if (!File.Exists(filename))
			{
				result = "";
			}
			else
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000228C File Offset: 0x0000048C
		public static void Download(string filePath)
		{
			string fileName = Path.GetFileName(filePath);
			HttpContext.Current.Response.Buffer = true;
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ContentType = FPFile.GetResponseContentType(Path.GetExtension(filePath));
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FPUtils.UrlEncode(fileName));
			HttpContext.Current.Response.WriteFile(filePath);
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002334 File Offset: 0x00000534
		public static void Download(string filePath, string filename)
		{
			HttpContext.Current.Response.Buffer = true;
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ContentType = FPFile.GetResponseContentType(Path.GetExtension(filePath));
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FPUtils.UrlEncode(filename));
			HttpContext.Current.Response.WriteFile(filePath);
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023D4 File Offset: 0x000005D4
		private static string GetResponseContentType(string type)
		{
			string result;
			if (type == ".htm")
			{
				result = "text/html";
			}
			else if (type == ".html")
			{
				result = "text/html";
			}
			else if (type == ".txt")
			{
				result = "text/plain";
			}
			else if (type == ".xml")
			{
				result = "text/plain";
			}
			else if (type == ".js")
			{
				result = "application/x-javascript";
			}
			else if (type == ".css")
			{
				result = "text/css";
			}
			else if (type == ".jpg")
			{
				result = "image/jpeg";
			}
			else if (type == "gif")
			{
				result = "image/gif";
			}
			else if (type == "png")
			{
				result = "image/png";
			}
			else if (type == ".swf")
			{
				result = "application/x-shockwave-flash";
			}
			else if (type == ".flv")
			{
				result = "application/x-shockwave-flash";
			}
			else if (type == "doc")
			{
				result = "application/msword";
			}
			else if (type == ".xls")
			{
				result = "application/vnd.ms-excel";
			}
			else if (type == ".ppt")
			{
				result = "application/vnd.ms-powerpoint";
			}
			else if (type == ".mp3")
			{
				result = "audio/mpeg";
			}
			else if (type == ".mpg")
			{
				result = "video/mpeg";
			}
			else if (type == ".rar")
			{
				result = "application/zip";
			}
			else if (type == ".zip")
			{
				result = "application/zip";
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
