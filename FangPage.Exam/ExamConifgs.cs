using System;
using System.Runtime.InteropServices;
using System.Text;
using FangPage.MVC;
using FangPage.Exam.Model;

namespace FangPage.Exam
{
	// Token: 0x02000005 RID: 5
	public class ExamConifgs
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public static ExamConfig GetExamConfig()
		{
			ExamConfig examConfig = FPCache.Get<ExamConfig>("ExamConfig");
			lock (ExamConifgs.lockHelper)
			{
				if (examConfig == null)
				{
					string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/exam.config");
					examConfig = FPSerializer.Load<ExamConfig>(mapPath);
					FPCache.Insert("ExamConfig", examConfig, mapPath);
				}
			}
			return examConfig;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003C28 File Offset: 0x00001E28
		public static bool SaveConfig(ExamConfig examconfig)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "config/exam.config");
			FPCache.Remove("ExamConfig");
			return FPSerializer.Save<ExamConfig>(examconfig, mapPath);
		}

		// Token: 0x06000029 RID: 41
		[DllImport("Iphlpapi.dll")]
		private static extern int SendARP(int DestIP, int SrcIP, ref long MacAddr, ref int PhyAddrLen);

		// Token: 0x0600002A RID: 42
		[DllImport("Ws2_32.dll")]
		private static extern int inet_addr(string ipaddr);

		// Token: 0x0600002B RID: 43 RVA: 0x00003C64 File Offset: 0x00001E64
		public static string GetMacAddress(string RemoteIP)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string result;
			try
			{
				int destIP = ExamConifgs.inet_addr(RemoteIP);
				long value = 0L;
				int num = 6;
				ExamConifgs.SendARP(destIP, 0, ref value, ref num);
				string text = Convert.ToString(value, 16).PadLeft(12, '0').ToUpper();
				int num2 = 12;
				for (int i = 0; i < 6; i++)
				{
					if (i == 5)
					{
						stringBuilder.Append(text.Substring(num2 - 2, 2));
					}
					else
					{
						stringBuilder.Append(text.Substring(num2 - 2, 2) + "-");
					}
					num2 -= 2;
				}
				result = stringBuilder.ToString();
			}
			catch
			{
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		private static object lockHelper = new object();
	}
}
