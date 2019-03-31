using System;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FP_SSO.Controller
{
	// Token: 0x02000009 RID: 9
	public class updatepwd : SSOController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002328 File Offset: 0x00000528
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("newpwd");
				UserBll.UpdatePassword(FPRequest.GetString("uname"), @string);
			}
		}
	}
}
