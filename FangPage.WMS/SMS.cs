using System;
using System.IO;
using System.Net;
using System.Text;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000031 RID: 49
	public class SMS
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00008252 File Offset: 0x00006452
		public static void ReSetConfig()
		{
			FPCache.Remove("FP_SMSCONFIG");
			SMS.smsconfig = SMSConfigs.GetSMSConfig();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000826C File Offset: 0x0000646C
		public static string Send(string mobile, string content)
		{
			string result;
			try
			{
				string format = "account={0}&password={1}&mobile={2}&content={3}";
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				byte[] bytes = utf8Encoding.GetBytes(string.Format(format, new object[]
				{
					SMS.smsconfig.account,
					SMS.smsconfig.password,
					mobile,
					content
				}));
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(SMS.smsconfig.posturl);
				httpWebRequest.Method = "POST";
				httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
				httpWebRequest.ContentLength = (long)bytes.Length;
				Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Flush();
				requestStream.Close();
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				if (httpWebResponse.StatusCode == HttpStatusCode.OK)
				{
					StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8);
					string text = streamReader.ReadToEnd();
					string text2 = text;
					switch (text2)
					{
					case "100":
						text = "";
						break;
					case "101":
						text = "验证失败";
						break;
					case "102":
						text = "手机号码格式不正确";
						break;
					case "103":
						text = "会员级别不够";
						break;
					case "104":
						text = "内容未审核";
						break;
					case "105":
						text = "内容过多";
						break;
					case "106":
						text = "账户余额不足";
						break;
					case "107":
						text = "Ip受限";
						break;
					case "108":
						text = "手机号码发送太频繁";
						break;
					case "120":
						text = "系统升级";
						break;
					}
					result = text;
				}
				else
				{
					result = "短信服务器无响应。";
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000084E4 File Offset: 0x000066E4
		public static int CheckSMS(string clientsms, string serversms)
		{
			string[] array = FPUtils.SplitString(serversms, "|", 3);
			string[] array2 = FPUtils.SplitString(serversms, "|", 3);
			int result;
			if (array2[0] != array[0])
			{
				result = 0;
			}
			else if (array2[1] != array[1])
			{
				result = -1;
			}
			else
			{
				DateTime dateTime = Convert.ToDateTime(array2[2]);
				DateTime t = Convert.ToDateTime(array[2]);
				if (dateTime.AddMinutes(5.0) < t)
				{
					result = -2;
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		// Token: 0x04000129 RID: 297
		private static SMSConfig smsconfig = SMSConfigs.GetSMSConfig();
	}
}
