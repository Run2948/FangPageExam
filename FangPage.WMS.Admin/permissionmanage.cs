using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004E RID: 78
	public class permissionmanage : AdminController
	{
		// Token: 0x060000BD RID: 189 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		protected override void Controller()
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
		}

		// Token: 0x040000DE RID: 222
		protected List<Permission> permissionlist = new List<Permission>();
	}
}
