using System;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Verify.Controller
{
	// Token: 0x02000004 RID: 4
	public class smsverify : APIController
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002490 File Offset: 0x00000690
		protected override void Controller()
		{
			if (this.mobile == "")
			{
				base.WriteErr("手机号码不能为空");
				return;
			}
			if (this.mobile.Trim().Length > 11)
			{
				base.WriteErr("手机号码不能大于11个字符");
				return;
			}
			if (!Regex.IsMatch(this.mobile.Trim(), "^[\\d|-]+$"))
			{
				base.WriteErr("手机号码中含有非法字符");
				return;
			}
			if (this.isseccode && !this.isvalid)
			{
				base.WriteErr("验证码错误。");
				return;
			}
			MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("sms_verify");
			if (msgTemplate.id == 0)
			{
				msgTemplate.content = "您的验证码是：【验证码】。请不要把验证码泄露给其他人。如非本人操作，可不用理会！";
			}
			string text = FPRandom.CreateCodeNum(4);
			msgTemplate.content = msgTemplate.content.Replace("【验证码】", text);
			string text2 = SMS.Send("", this.mobile, msgTemplate.content);
			if (text2 == "")
			{
				this.Session["FP_SMSVERIFY"] = string.Concat(new object[]
				{
					this.mobile,
					"|",
					text,
					"|",
					DbUtils.GetDateTime()
				});
				base.WriteSuccess("短信发送成功");
				return;
			}
			base.WriteErr(text2);
		}

		// Token: 0x04000002 RID: 2
		protected string mobile = FPRequest.GetString("mobile");
	}
}
