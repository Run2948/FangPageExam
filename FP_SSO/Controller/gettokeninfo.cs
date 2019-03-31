using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_SSO.Controller
{
	// Token: 0x02000006 RID: 6
	public class gettokeninfo : SSOController
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000021E8 File Offset: 0x000003E8
		protected override void Controller()
		{
			if (this.ispost)
			{
				UserInfo userInfo = UserBll.GetUserInfo(SessionBll.GetSessionInfo(this.token).username);
				FPData fpdata = new FPData();
				if (userInfo.id > 0)
				{
					fpdata["errcode"] = "0";
					fpdata["errmsg"] = "";
					fpdata["userid"] = userInfo.id.ToString();
					fpdata["username"] = userInfo.username;
					fpdata["realname"] = userInfo.realname;
					fpdata["password"] = userInfo.extend["password"];
				}
				else
				{
					fpdata["errcode"] = "1";
					fpdata["errmsg"] = "您尚未登录OA或超时，请重新登录。";
					fpdata["userid"] = userInfo.id.ToString();
					fpdata["username"] = userInfo.username;
					fpdata["realname"] = userInfo.realname;
					fpdata["realname"] = userInfo.realname;
					fpdata["password"] = "";
				}
				FPResponse.WriteJson(fpdata);
			}
		}
	}
}
