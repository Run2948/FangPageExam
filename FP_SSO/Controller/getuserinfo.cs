using System;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_SSO.Controller
{
	// Token: 0x0200000B RID: 11
	public class getuserinfo : SSOController
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000235C File Offset: 0x0000055C
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("username");
				UserInfo userInfo = UserBll.GetUserInfo(@string);
				if (userInfo.issso == 1 && this.sysconfig.ssocheck == 1)
				{
					userInfo = SSO.GetFullUserInfo(@string);
				}
				FPResponse.WriteJson(userInfo);
			}
		}
	}
}
