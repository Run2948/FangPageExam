using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Verify.Controller
{
	// Token: 0x02000003 RID: 3
	public class emailverify : APIController
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002344 File Offset: 0x00000544
		protected override void Controller()
		{
			if (this.email == "")
			{
				base.WriteErr("邮箱不能为空");
				return;
			}
			if (!FPUtils.IsEmail(this.email))
			{
				base.WriteErr("邮箱格式不正确");
				return;
			}
			if (this.isseccode && !this.isvalid)
			{
				base.WriteErr("验证码错误。");
				return;
			}
			MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_verify");
			if (msgTemplate.id == 0)
			{
				msgTemplate.subject = "验证码邮件";
				msgTemplate.content = "您的验证码是：【验证码】。请不要把验证码泄露给其他人。如非本人操作，可不用理会！";
			}
			string text = FPRandom.CreateCodeNum(4);
			msgTemplate.content = msgTemplate.content.Replace("【邮箱帐号】", this.email).Replace("【验证码】", text);
			string text2 = Email.Send(this.email, msgTemplate.subject, msgTemplate.content);
			if (text2 == "")
			{
				this.Session["FP_EMAILVERIFY"] = string.Concat(new object[]
				{
					this.email,
					"|",
					text,
					"|",
					DbUtils.GetDateTime()
				});
				base.WriteSuccess("邮件发送成功");
				return;
			}
			base.WriteErr(text2);
		}

		// Token: 0x04000001 RID: 1
		protected string email = FPRequest.GetString("email");
	}
}
