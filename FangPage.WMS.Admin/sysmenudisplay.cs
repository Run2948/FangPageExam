using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000020 RID: 32
	public class sysmenudisplay : SuperController
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00006DD8 File Offset: 0x00004FD8
		protected override void Controller()
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeOrWhere("(platform", "SYSTEM"),
				DbHelper.MakeOrWhere("platform)", this.sysconfig.platform),
				DbHelper.MakeAndWhere("parentid", this.parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.menulist = DbHelper.ExecuteList<MenuInfo>(sqlparams);
			if (this.ispost)
			{
				int num = 0;
				foreach (MenuInfo menuInfo in this.menulist)
				{
					this.menulist[num].display = FPRequest.GetInt("display_" + menuInfo.id);
					DbHelper.ExecuteUpdate<MenuInfo>(this.menulist[num]);
					num++;
				}
				FPCache.Remove("FP_MENULIST");
				base.Response.Redirect(this.pagename + "?parentid=" + this.parentid);
			}
		}

		// Token: 0x0400004C RID: 76
		public List<MenuInfo> menulist = new List<MenuInfo>();

		// Token: 0x0400004D RID: 77
		public int parentid = FPRequest.GetInt("parentid");
	}
}
