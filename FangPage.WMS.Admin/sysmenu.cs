using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000037 RID: 55
	public class sysmenu : AdminController
	{
		// Token: 0x06000085 RID: 133 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		protected override void View()
		{
			if (this.parentid > 0)
			{
				this.menulist = this.GetChildMenu(this.parentid);
			}
			base.SaveLeftURL(this.fullname);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000BC34 File Offset: 0x00009E34
		protected List<MenuInfo> GetChildMenu(int parentid)
		{
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("parentid", parentid));
			if (this.roleid != 1)
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.role.menus));
			}
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<MenuInfo>(orderby, list.ToArray());
		}

		// Token: 0x04000084 RID: 132
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000085 RID: 133
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
