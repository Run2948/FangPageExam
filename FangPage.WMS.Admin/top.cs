using System;
using System.Collections.Generic;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000045 RID: 69
	public class top : AdminController
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		protected override void Controller()
		{
			if (this.roleid == 1)
			{
				this.menulist = MenuBll.GetMenuList(0);
				return;
			}
			this.menulist = MenuBll.GetMenuList(0, this.role.menus);
		}

		// Token: 0x040000C0 RID: 192
		protected List<MenuInfo> menulist = new List<MenuInfo>();

		// Token: 0x040000C1 RID: 193
		protected new string version = "7.4.1";
	}
}
