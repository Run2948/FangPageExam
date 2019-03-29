using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000048 RID: 72
	public class rolepermission : SuperController
	{
		// Token: 0x060000AE RID: 174 RVA: 0x0000DC44 File Offset: 0x0000BE44
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
					this.roleinfo.permission = FPRequest.GetString("chkperm");
					DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
					if (this.roleinfo.id == this.roleid)
					{
						base.ResetUser();
					}
					base.Response.Redirect(this.pagename + "?rid=" + this.rid);
				}
				this.permisionlist = DbHelper.ExecuteList<Permission>(OrderBy.ASC);
				base.SaveRightURL();
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000DD18 File Offset: 0x0000BF18
		protected bool isPermission(int id)
		{
			return FPUtils.InArray(id, this.roleinfo.permission);
		}

		// Token: 0x040000AC RID: 172
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x040000AD RID: 173
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x040000AE RID: 174
		protected List<Permission> permisionlist = new List<Permission>();
	}
}
