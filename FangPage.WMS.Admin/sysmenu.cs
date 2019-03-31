using System;
using System.Collections.Generic;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000044 RID: 68
	public class sysmenu : AdminController
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x0000D755 File Offset: 0x0000B955
		protected override void Controller()
		{
			if (this.parentid > 0)
			{
				this.menulist = this.GetChildMenu(this.parentid);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000D772 File Offset: 0x0000B972
		protected List<MenuInfo> GetChildMenu(int parentid)
		{
			if (this.roleid == 1)
			{
				return MenuBll.GetMenuList(parentid);
			}
			return MenuBll.GetMenuList(parentid, this.role.menus);
		}

		// Token: 0x040000BE RID: 190
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x040000BF RID: 191
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
