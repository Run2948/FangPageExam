using System;
using System.Collections;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS.Tools
{
	// Token: 0x0200003E RID: 62
	public class smsverify : WMSController
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000A578 File Offset: 0x00008778
		protected override void View()
		{
			if (this.userid == 0)
			{
				this.ShowErrMsg("对不起，您尚未登录或超时。");
			}
			else if (this.mobile == "")
			{
				this.ShowErrMsg("手机号码不能为空。");
			}
			else if (this.user.ismobile == 1 && this.user.mobile == this.mobile)
			{
				this.ShowErrMsg("您已绑定了该手机号码。");
			}
			else
			{
				if (this.type == "")
				{
					this.type = "sms_cert";
				}
				MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate(this.type);
				if (msgTemplate.id == 0)
				{
					this.ShowErrMsg("短信模板不存在。");
				}
				else
				{
					string text = WMSUtils.CreateAuthStr(4, true);
					msgTemplate.content = msgTemplate.content.Replace("【验证码】", text);
					string text2 = SMS.Send(this.mobile, msgTemplate.content);
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
						Hashtable hashtable = new Hashtable();
						hashtable["error"] = 0;
						hashtable["message"] = "";
						base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
						base.Response.Write(JsonMapper.ToJson(hashtable));
						base.Response.End();
					}
					else
					{
						this.ShowErrMsg(text2);
					}
				}
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A774 File Offset: 0x00008974
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x04000148 RID: 328
		protected string mobile = FPRequest.GetString("mobile");

		// Token: 0x04000149 RID: 329
		protected string type = FPRequest.GetString("type");
	}
}
