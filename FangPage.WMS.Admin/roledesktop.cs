using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000040 RID: 64
	public class roledesktop : SuperController
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		protected override void View()
		{
			this.roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.rid);
			if (this.roleinfo.id == 0)
			{
				this.ShowErr("对不起，该角色不存在或已被删除。");
			}
			else
			{
				if (this.ispost)
				{
					this.roleinfo.desktop = FPRequest.GetString("desktop");
					DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
					if (this.roleinfo.id == this.roleid)
					{
						base.ResetUser();
					}
					base.Response.Redirect(this.pagename + "?rid=" + this.rid);
				}
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("hidden", 0),
					DbHelper.MakeOrWhere("system", 1)
				};
				this.desktoplist = DbHelper.ExecuteList<DesktopInfo>(OrderBy.ASC, sqlparams);
				base.SaveRightURL();
			}
		}

		// Token: 0x0400009D RID: 157
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x0400009E RID: 158
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x0400009F RID: 159
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
