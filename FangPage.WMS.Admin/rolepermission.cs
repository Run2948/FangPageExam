using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000052 RID: 82
	public class rolepermission : SuperController
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000F694 File Offset: 0x0000D894
		protected override void Controller()
		{
			this.roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.rid);
			if (this.roleinfo.id == 0)
			{
				this.ShowErr("对不起，该角色不存在或已被删除。");
				return;
			}
			if (this.ispost)
			{
				this.roleinfo.permission = FPRequest.GetString("chkperm");
				DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
				if (this.roleinfo.id == this.roleid)
				{
					base.ResetUser();
				}
				FPCache.Remove("FP_ROLELIST");
				base.Response.Redirect(this.pagename + "?rid=" + this.rid);
			}
			this.permisionlist = DbHelper.ExecuteList<Permission>(OrderBy.ASC);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000F749 File Offset: 0x0000D949
		protected bool isPermission(int id)
		{
			return FPArray.InArray(id, this.roleinfo.permission) >= 0;
		}

		// Token: 0x040000E3 RID: 227
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x040000E4 RID: 228
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x040000E5 RID: 229
		protected List<Permission> permisionlist = new List<Permission>();
	}
}
