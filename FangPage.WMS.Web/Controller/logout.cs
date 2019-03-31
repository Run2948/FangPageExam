using System;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x0200000D RID: 13
	public class logout : WebController
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000354C File Offset: 0x0000174C
		protected override void Controller()
		{
			SessionBll.SessionLogout(this.session);
			WMSUtils.ClearUserCookie(this.platform, this.session.port);
			FPSession.Remove("FP_OLUSERINFO");
			FPSession.Remove("FP_PERMISSION");
			this.Session.Abandon();
			base.AddMsg("已成功安全退出系统");
			base.SetMetaRefresh(2);
			this.action = "1";
		}
	}
}
