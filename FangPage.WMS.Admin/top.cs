using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000038 RID: 56
	public class top : AdminController
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000BCCC File Offset: 0x00009ECC
		protected override void View()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.menulist = DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
			if (this.Session["FP_ADMIN_TOPMENU"] != null)
			{
				this.topmenuid = FPUtils.StrToInt(this.Session["FP_ADMIN_TOPMENU"].ToString(), 0);
			}
			if (this.topmenuid == 0)
			{
				if (this.roleid == 1)
				{
					this.topmenuid = 1;
				}
				else if (this.role.menus != "")
				{
					this.topmenuid = FPUtils.SplitInt(this.role.menus)[0];
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		protected string FmUrl(MenuInfo menuinfo)
		{
			string text = menuinfo.lefturl;
			if (text.IndexOf("?") > 0)
			{
				text = text + "&topmenuid=" + menuinfo.id;
			}
			else
			{
				text = text + "?topmenuid=" + menuinfo.id;
			}
			return text;
		}

		// Token: 0x04000086 RID: 134
		protected List<MenuInfo> menulist = new List<MenuInfo>();

		// Token: 0x04000087 RID: 135
		protected string version = "4.7";

		// Token: 0x04000088 RID: 136
		protected int topmenuid = 1;
	}
}
