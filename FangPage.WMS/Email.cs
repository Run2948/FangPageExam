using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000008 RID: 8
	public class Email
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002FE6 File Offset: 0x000011E6
		public static void ReSetConfig()
		{
			FPCache.Remove("FP_EMAILCONFIG");
			Email.emailconfig = EmailConfigs.GetEmailConfig();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003000 File Offset: 0x00001200
		public static string Send(string msgtos, string subject, string message)
		{
			string result;
			if (msgtos == "")
			{
				result = "发送地址不能为空。";
			}
			else
			{
				if (subject == "")
				{
					subject = "无标题";
				}
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(Email.emailconfig.sysemail, Email.emailconfig.fromname, Encoding.UTF8);
				foreach (string addresses in FPUtils.SplitString(msgtos, ";"))
				{
					mailMessage.To.Add(addresses);
				}
				mailMessage.Subject = subject;
				mailMessage.Body = message;
				mailMessage.Priority = MailPriority.Normal;
				mailMessage.BodyEncoding = Encoding.UTF8;
				mailMessage.IsBodyHtml = true;
				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Host = Email.emailconfig.smtp;
				smtpClient.Port = Email.emailconfig.port;
				smtpClient.EnableSsl = (Email.emailconfig.ssl == 1);
				smtpClient.Credentials = new NetworkCredential(Email.emailconfig.username, Email.emailconfig.password);
				try
				{
					smtpClient.Send(mailMessage);
					result = "";
				}
				catch (SmtpException ex)
				{
					result = ex.Message;
				}
			}
			return result;
		}

		// Token: 0x04000017 RID: 23
		private static EmailConfig emailconfig = EmailConfigs.GetEmailConfig();
	}
}
