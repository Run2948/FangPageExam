using System;
using System.IO;
using System.Text;
using FangPage.MVC;

namespace FangPage.WMS.Task
{
	// Token: 0x02000036 RID: 54
	public class TaskLog
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00009834 File Offset: 0x00007A34
		public static void WriteFailedLog(string logContent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			stringBuilder.Append("  ");
			stringBuilder.Append(logContent);
			stringBuilder.Append("\r\n");
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "cache");
			if (!Directory.Exists(mapPath))
			{
				Directory.CreateDirectory(mapPath);
			}
			using (FileStream fileStream = new FileStream(mapPath + "\\tasklog.config", FileMode.Append, FileAccess.Write, FileShare.Write))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Close();
			}
		}
	}
}
