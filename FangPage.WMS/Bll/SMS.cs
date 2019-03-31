using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000033 RID: 51
	public class SMS
	{
		// Token: 0x0600039F RID: 927 RVA: 0x00009A60 File Offset: 0x00007C60
		static SMS()
		{
			SMS.smsconfig = SMSConfigs.GetSMSConfig(out SMS.sendRet);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00009A7B File Offset: 0x00007C7B
		public static void ReSetConfig()
		{
			FPCache.Remove("SMSCONFIG");
			SMS.smsconfig = SMSConfigs.GetSMSConfig(out SMS.sendRet);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009A98 File Offset: 0x00007C98
		public static string Send(string name, string mobile, string content)
		{
			if (SMS.smsconfig.server == "")
			{
				return "短信服务器尚未配置。";
			}
			bool flag = true;
			if (content.StartsWith("【") && content.EndsWith("】"))
			{
				flag = false;
				content = content.TrimStart(new char[]
				{
					'【'
				}).TrimEnd(new char[]
				{
					'】'
				});
			}
			MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("sms_prefix");
			if (flag && msgTemplate.id > 0 && msgTemplate.content != "" && msgTemplate.status == 1)
			{
				msgTemplate.content = msgTemplate.content.Replace("${name}", name).Replace("${time}", FPUtils.GetDateTime()).Replace("[]", "").Replace("【】", "").Replace("()", "").Replace("（）", "");
				content = msgTemplate.content + content;
			}
			MsgTempInfo msgTemplate2 = MsgTempBll.GetMsgTemplate("sms_suffix");
			if (flag && msgTemplate2.id > 0 && msgTemplate2.content != "" && msgTemplate2.status == 1)
			{
				msgTemplate2.content = msgTemplate2.content.Replace("${name}", name).Replace("${time}", FPUtils.GetDateTime()).Replace("[]", "").Replace("【】", "").Replace("()", "").Replace("（）", "");
				content += msgTemplate2.content;
			}
			if (SMS.smsconfig.port == 0)
			{
				string[] array = FPArray.SplitString(SMS.smsconfig.server, "?", 2);
				string s = array[1].Replace("${账号}", SMS.smsconfig.account).Replace("${密码}", SMS.smsconfig.password).Replace("${手机号码}", mobile).Replace("${短信内容}", content);
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				try
				{
					HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(array[0]);
					httpWebRequest.Method = "POST";
					httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
					httpWebRequest.ContentLength = (long)bytes.Length;
					httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
					Stream requestStream = httpWebRequest.GetRequestStream();
					requestStream.Write(bytes, 0, bytes.Length);
					requestStream.Flush();
					requestStream.Close();
					HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
					if (httpWebResponse.StatusCode != HttpStatusCode.OK)
					{
						return "短信服务器无响应。";
					}
					string key = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
					if (SMS.sendRet.Contains(key))
					{
						return SMS.sendRet[key].ToString();
					}
					return "";
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}
			string result;
			try
			{
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(SMS.smsconfig.server), SMS.smsconfig.port);
				socket.Connect(remoteEP);
				byte[] bytes2 = Encoding.UTF8.GetBytes(mobile + "|" + content);
				socket.Send(bytes2);
				socket.Close();
				result = "";
			}
			catch (Exception ex2)
			{
				result = ex2.Message;
			}
			return result;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00009E20 File Offset: 0x00008020
		public static int CheckSMS(string clientsms, string serversms)
		{
			string[] array = FPArray.SplitString(serversms, "|", 3);
			string[] array2 = FPArray.SplitString(serversms, "|", 3);
			if (array2[0] != array[0])
			{
				return 0;
			}
			if (array2[1] != array[1])
			{
				return -1;
			}
			DateTime dateTime = Convert.ToDateTime(array2[2]);
			DateTime t = Convert.ToDateTime(array[2]);
			if (dateTime.AddMinutes(5.0) < t)
			{
				return -2;
			}
			return 1;
		}

		// Token: 0x040001A9 RID: 425
		private static SMSConfig smsconfig;

		// Token: 0x040001AA RID: 426
		private static Hashtable sendRet = new Hashtable();
	}
}
