using System;
using System.Collections;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000014 RID: 20
	public class smsconfigmanage : SuperController
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00004574 File Offset: 0x00002774
		protected override void Controller()
		{
			Hashtable hashtable;
			this.smsconfig = SMSConfigs.GetSMSConfig(out hashtable);
			if (this.ispost)
			{
				if (this.action == "save")
				{
					this.smsconfig = FPRequest.GetModel<SMSConfig>(this.smsconfig);
					SMSConfigs.SaveConfig(this.smsconfig);
					SMS.ReSetConfig();
					base.AddMsg("保存配置成功!");
					return;
				}
				if (this.action == "send")
				{
					this.phone = FPRequest.GetString("phone");
					if (this.phone == "")
					{
						this.ShowErr("请输入接收测试短信的手机号码!");
						return;
					}
					MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("sms_test");
					if (msgTemplate.id == 0)
					{
						msgTemplate.content = "您的验证码是：【验证码】。请不要把验证码泄露给其他人。如非本人操作，可不用理会！";
					}
					msgTemplate.content = msgTemplate.content.Replace("【验证码】", FPRandom.CreateCodeNum(4));
					string text = SMS.Send(this.realname, this.phone, msgTemplate.content);
					if (text == "")
					{
						base.AddMsg("发布测试短信成功，请检查手机是否收到。");
						return;
					}
					this.ShowErr(text);
					return;
				}
			}
		}

		// Token: 0x0400002E RID: 46
		protected string phone = "";

		// Token: 0x0400002F RID: 47
		protected SMSConfig smsconfig;
	}
}
