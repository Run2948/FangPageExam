using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x0200004B RID: 75
	public class adminmenu : AdminController
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000BF84 File Offset: 0x0000A184
		protected override void View()
		{
			if (this.parentid > 0)
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
				OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				this.menulist = DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
				{
					sqlParam
				});
			}
			base.SaveLeftURL(this.rawurl);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BFEC File Offset: 0x0000A1EC
		protected List<MenuInfo> GetChildMenu(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x0400014F RID: 335
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000150 RID: 336
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
