using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000042 RID: 66
	public class smsconfigmanage : SuperController
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000D74C File Offset: 0x0000B94C
		protected override void View()
		{
			this.smsconfig = SMSConfigs.GetSMSConfig();
			if (this.ispost)
			{
				if (this.action == "save")
				{
					this.smsconfig = FPRequest.GetModel<SMSConfig>(this.smsconfig);
					SMSConfigs.SaveConfig(this.smsconfig);
					SMS.ReSetConfig();
					base.AddMsg("保存配置成功!");
				}
				else if (this.action == "send")
				{
					this.phone = FPRequest.GetString("phone");
					if (this.phone == "")
					{
						this.ShowErr("请输入接收测试短信的手机号码!");
						return;
					}
					string content = string.Format("您的验证码是：{0}。请不要把验证码泄露给其他人。如非本人操作，可不用理会！", WMSUtils.CreateAuthStr(4, true));
					string text = SMS.Send(this.phone, content);
					if (!(text == ""))
					{
						this.ShowErr(text);
						return;
					}
					base.AddMsg("发布测试短信成功，请检查手机是否收到。");
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x040000A3 RID: 163
		protected string phone = "";

		// Token: 0x040000A4 RID: 164
		protected SMSConfig smsconfig;
	}
}
