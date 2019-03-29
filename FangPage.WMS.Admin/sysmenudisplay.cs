using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000019 RID: 25
	public class sysmenudisplay : SuperController
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00005D0C File Offset: 0x00003F0C
		protected override void View()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.menulist = DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
			if (this.ispost)
			{
				int num = 0;
				foreach (MenuInfo menuInfo in this.menulist)
				{
					this.menulist[num].display = FPRequest.GetInt("display_" + menuInfo.id);
					DbHelper.ExecuteUpdate<MenuInfo>(this.menulist[num]);
					num++;
				}
				base.Response.Redirect(this.pagename + "?parentid=" + this.parentid);
			}
			base.SaveRightURL();
		}

		// Token: 0x04000032 RID: 50
		public List<MenuInfo> menulist = new List<MenuInfo>();

		// Token: 0x04000033 RID: 51
		public int parentid = FPRequest.GetInt("parentid");
	}
}
