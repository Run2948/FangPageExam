using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000044 RID: 68
	public class permissionmanage : AdminController
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000D978 File Offset: 0x0000BB78
		protected override void View()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<Permission>(FPRequest.GetString("chkdel"));
				}
			}
			this.permissionlist = DbHelper.ExecuteList<Permission>(OrderBy.ASC);
			base.SaveRightURL();
		}

		// Token: 0x040000A7 RID: 167
		protected List<Permission> permissionlist = new List<Permission>();
	}
}
