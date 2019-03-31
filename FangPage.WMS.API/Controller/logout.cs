using System;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x02000010 RID: 16
	public class logout : APIController
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003E34 File Offset: 0x00002034
		protected override void Controller()
		{
			SessionBll.DeleteSession(this.platform, this.token);
			WMSUtils.ClearUserCookie(this.platform, this.port);
			FPSession.Remove("FP_OLUSERINFO");
			FPSession.Remove("FP_PERMISSION");
			if (this.sysconfig.ssocheck == 1)
			{
				SSO.Logout(this.platform, this.token);
			}
			base.WriteSuccess("已成功安全退出系统。");
		}
	}
}
