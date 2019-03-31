using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004D RID: 77
	public class permissionadd : SuperController
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000F300 File Offset: 0x0000D500
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.permissioninfo = DbHelper.ExecuteModel<Permission>(this.id);
			}
			if (this.ispost)
			{
				this.permissioninfo.isadd = 0;
				this.permissioninfo.isupdate = 0;
				this.permissioninfo.isdelete = 0;
				this.permissioninfo.isaudit = 0;
				this.permissioninfo = FPRequest.GetModel<Permission>(this.permissioninfo);
				if (this.permissioninfo.id > 0)
				{
					DbHelper.ExecuteUpdate<Permission>(this.permissioninfo);
				}
				else
				{
					DbHelper.ExecuteInsert<Permission>(this.permissioninfo);
				}
				base.Response.Redirect("permissionmanage.aspx");
			}
		}

		// Token: 0x040000DC RID: 220
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000DD RID: 221
		protected Permission permissioninfo = new Permission();
	}
}
