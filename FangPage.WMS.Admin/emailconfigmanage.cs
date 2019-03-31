using System;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000017 RID: 23
	public class emailconfigmanage : SuperController
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00004978 File Offset: 0x00002B78
		protected override void Controller()
		{
			this.emailconfig = EmailConfigs.GetEmailConfig();
			if (this.ispost)
			{
				if (this.action == "save")
				{
					this.emailconfig.ssl = 0;
					this.emailconfig = FPRequest.GetModel<EmailConfig>(this.emailconfig);
					EmailConfigs.SaveConfig(this.emailconfig);
					Email.ReSetConfig();
					base.AddMsg("保存配置成功!");
					return;
				}
				if (this.action == "send")
				{
					this.testmail = FPRequest.GetString("testmail");
					if (this.testmail == "")
					{
						this.ShowErr("请输入测试发送EMAIL地址!");
						return;
					}
					MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_test");
					if (msgTemplate.id == 0 || msgTemplate.status == 0)
					{
						msgTemplate.subject = "方配网站管理系统(WMS)发送系统测试邮件";
						msgTemplate.content = "您好，这是一封方配网站管理系统(WMS)邮箱设置页面发送的测试邮件!，如果您收到这款邮件说明您的邮箱配置是正确的。";
					}
					string text = Email.Send(this.testmail, msgTemplate.subject, msgTemplate.content);
					if (text == "")
					{
						base.AddMsg("发布测试邮件成功，请检查是否收到。");
						return;
					}
					this.ShowErr(text);
					return;
				}
			}
		}

		// Token: 0x04000031 RID: 49
		protected EmailConfig emailconfig;

		// Token: 0x04000032 RID: 50
		protected string testmail = "";
	}
}
