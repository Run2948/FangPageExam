using FangPage.Common;
using FangPage.MVC;
using FP_Exam.Model;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FP_Exam
{
	public class ExamConifgs
	{
		private static object lockHelper = new object();

		public static ExamConfig GetExamConfig()
		{
			object obj = FPCache.Get("ExamConfig");
			object obj2 = ExamConifgs.lockHelper;
			ExamConfig result;
			lock (obj2)
			{
				bool flag = obj == null;
				if (flag)
				{
					string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/exam.config");
					ExamConfig examConfig = FPSerializer.Load<ExamConfig>(mapPath);
					FPCache.Insert("ExamConfig", examConfig, mapPath);
					result = examConfig;
					return result;
				}
			}
			result = (obj as ExamConfig);
			return result;
		}

		public static bool SaveConfig(ExamConfig examconfig)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "config/exam.config");
			FPCache.Remove("ExamConfig");
			return FPSerializer.Save<ExamConfig>(examconfig, mapPath);
		}

		[DllImport("Iphlpapi.dll")]
		private static extern int SendARP(int DestIP, int SrcIP, ref long MacAddr, ref int PhyAddrLen);

		[DllImport("Ws2_32.dll")]
		private static extern int inet_addr(string ipaddr);

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
					bool flag = i == 5;
					if (flag)
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
	}
}
