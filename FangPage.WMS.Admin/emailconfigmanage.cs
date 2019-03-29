using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000010 RID: 16
	public class emailconfigmanage : SuperController
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00003F54 File Offset: 0x00002154
		protected override void View()
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
				}
				else if (this.action == "send")
				{
					this.testmail = FPRequest.GetString("testmail");
					if (this.testmail == "")
					{
						this.ShowErr("请输入测试发送EMAIL地址!");
						return;
					}
					string text = Email.Send(this.testmail, "方配网站管理系统(WMS)发送系统测试邮件", "您好，这是一封方配网站管理系统(WMS)邮箱设置页面发送的测试邮件!，如果您收到这款邮件说明您的邮箱配置是正确的。");
					if (!(text == ""))
					{
						this.ShowErr(text);
						return;
					}
					base.AddMsg("发布测试邮件成功，请检查是否收到。");
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x04000018 RID: 24
		protected EmailConfig emailconfig;

		// Token: 0x04000019 RID: 25
		protected string testmail = "";
	}
}
