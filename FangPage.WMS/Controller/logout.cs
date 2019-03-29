using System;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000057 RID: 87
	public class logout : WMSController
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		protected override void View()
		{
			UserBll.UpdateUserState(this.userid, 0);
			WMSCookie.ClearUserCookie();
			this.Session["FP_OLUSERINFO"] = null;
			this.Session["FP_ADMIN_LEFTMENU"] = null;
			this.Session["FP_ADMIN_RIGHTMENU"] = null;
			this.Session["FP_ADMIN_TOPMENU"] = null;
			this.Session.Abandon();
			base.AddMsg("已成功安全退出系统");
			base.SetMetaRefresh(2);
			this.action = "1";
		}
	}
}
