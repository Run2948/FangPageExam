using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x02000005 RID: 5
	public class FPResponse
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000026FC File Offset: 0x000008FC
		public static void End()
		{
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000270D File Offset: 0x0000090D
		public static void Redirect(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002720 File Offset: 0x00000920
		public static void Redirect(string url, string target)
		{
			if (target == "" || target == "_self" || target == "self")
			{
				HttpContext.Current.Response.Redirect(url);
				return;
			}
			target = target.TrimStart(new char[]
			{
				'_'
			});
			HttpContext.Current.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			HttpContext.Current.Response.Write("<script type=\"text/javascript\">");
			HttpContext.Current.Response.Write(string.Concat(new string[]
			{
				"window.",
				target,
				".location='",
				url,
				"';"
			}));
			HttpContext.Current.Response.Write("</script>");
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002802 File Offset: 0x00000A02
		public static void Write(string s)
		{
			HttpContext.Current.Response.Write(s);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002814 File Offset: 0x00000A14
		public static void Write(string s, bool endResponse)
		{
			HttpContext.Current.Response.Write(s);
			if (endResponse)
			{
				HttpContext.Current.Response.End();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002838 File Offset: 0x00000A38
		public static void Write(object obj)
		{
			HttpContext.Current.Response.Write(obj);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000284A File Offset: 0x00000A4A
		public static void Write(object obj, bool endResponse)
		{
			HttpContext.Current.Response.Write(obj);
			if (endResponse)
			{
				HttpContext.Current.Response.End();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000286E File Offset: 0x00000A6E
		public static void WriteEnd(object obj)
		{
			HttpContext.Current.Response.Write(obj);
			HttpContext.Current.Response.End();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002890 File Offset: 0x00000A90
		public static void WriteDown(string filePath)
		{
			string fileName = Path.GetFileName(filePath);
			HttpContext.Current.Response.Buffer = true;
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ContentType = FPResponse.GetResponseContentType(Path.GetExtension(filePath));
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FPUtils.UrlEncode(fileName));
			HttpContext.Current.Response.WriteFile(filePath);
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002930 File Offset: 0x00000B30
		public static void WriteDown(string filePath, string filename)
		{
			HttpContext.Current.Response.Buffer = true;
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.ContentType = FPResponse.GetResponseContentType(Path.GetExtension(filePath));
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FPUtils.UrlEncode(filename));
			HttpContext.Current.Response.WriteFile(filePath);
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000029C8 File Offset: 0x00000BC8
		public static void WriteJson(object obj)
		{
			FPResponse.SendData(FPJson.ToJson(obj));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029D5 File Offset: 0x00000BD5
		public static void WriteXml<T>(T model) where T : new()
		{
			FPResponse.SendData(FPXml.ToXml<T>(model));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029E2 File Offset: 0x00000BE2
		public static void WriteXml<T>(List<T> list) where T : new()
		{
			FPResponse.SendData(FPXml.ToXml<T>(list));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029EF File Offset: 0x00000BEF
		public static void SendData(string data)
		{
			HttpContext.Current.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			HttpContext.Current.Response.Write(data);
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A29 File Offset: 0x00000C29
		public static void SendData(object obj)
		{
			HttpContext.Current.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			HttpContext.Current.Response.Write(obj);
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A63 File Offset: 0x00000C63
		public static void Log(object obj)
		{
			if (obj != null)
			{
				FPFile.AppendFile(FPFile.GetMapPath(WebConfig.WebPath + "log/sys.log"), obj.ToString());
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A87 File Offset: 0x00000C87
		public static void Log(string logfile, object obj)
		{
			if (obj != null)
			{
				FPFile.AppendFile(FPFile.GetMapPath(WebConfig.WebPath + "log/" + logfile), obj.ToString());
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AAC File Offset: 0x00000CAC
		private static string GetResponseContentType(string type)
		{
			if (type == ".htm")
			{
				return "text/html";
			}
			if (type == ".html")
			{
				return "text/html";
			}
			if (type == ".txt")
			{
				return "text/plain";
			}
			if (type == ".xml")
			{
				return "text/plain";
			}
			if (type == ".js")
			{
				return "application/x-javascript";
			}
			if (type == ".css")
			{
				return "text/css";
			}
			if (type == ".jpg")
			{
				return "image/jpeg";
			}
			if (type == "gif")
			{
				return "image/gif";
			}
			if (type == "png")
			{
				return "image/png";
			}
			if (type == ".swf")
			{
				return "application/x-shockwave-flash";
			}
			if (type == ".flv")
			{
				return "application/x-shockwave-flash";
			}
			if (type == "doc")
			{
				return "application/msword";
			}
			if (type == ".xls")
			{
				return "application/vnd.ms-excel";
			}
			if (type == ".ppt")
			{
				return "application/vnd.ms-powerpoint";
			}
			if (type == ".mp3")
			{
				return "audio/mpeg";
			}
			if (type == ".mpg")
			{
				return "video/mpeg";
			}
			if (type == ".rar")
			{
				return "application/zip";
			}
			if (type == ".zip")
			{
				return "application/zip";
			}
			return "application/octet-stream";
		}
	}
}
