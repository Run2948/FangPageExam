using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Config;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000034 RID: 52
	public class Email
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00009E9F File Offset: 0x0000809F
		public static void ReSetConfig()
		{
			FPCache.Remove("FP_EMAILCONFIG");
			Email.emailconfig = EmailConfigs.GetEmailConfig();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00009EB8 File Offset: 0x000080B8
		public static string Send(string msgtos, string subject, string message)
		{
			if (Email.emailconfig.smtp == "")
			{
				return "邮箱服务器尚未配置。";
			}
			if (msgtos == "")
			{
				return "发送地址不能为空。";
			}
			if (subject == "")
			{
				subject = "无标题";
			}
			string result;
			try
			{
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(Email.emailconfig.sysemail, Email.emailconfig.fromname, Encoding.UTF8);
				foreach (string addresses in FPArray.SplitString(msgtos, ";"))
				{
					mailMessage.To.Add(addresses);
				}
				mailMessage.Subject = subject;
				mailMessage.Body = message;
				mailMessage.Priority = MailPriority.Normal;
				mailMessage.BodyEncoding = Encoding.UTF8;
				mailMessage.IsBodyHtml = true;
				new SmtpClient
				{
					Host = Email.emailconfig.smtp,
					Port = Email.emailconfig.port,
					EnableSsl = (Email.emailconfig.ssl == 1),
					Credentials = new NetworkCredential(Email.emailconfig.username, Email.emailconfig.password)
				}.Send(mailMessage);
				result = "";
			}
			catch (SmtpException ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000A008 File Offset: 0x00008208
		public static int CheckEmailVerify(string clientcode, string servercode)
		{
			string[] array = FPArray.SplitString(servercode, "|", 3);
			string[] array2 = FPArray.SplitString(servercode, "|", 3);
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

		// Token: 0x040001AB RID: 427
		private static EmailConfig emailconfig = EmailConfigs.GetEmailConfig();
	}
}
