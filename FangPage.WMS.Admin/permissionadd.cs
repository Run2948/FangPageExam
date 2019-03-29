using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000043 RID: 67
	public class permissionadd : SuperController
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x0000D880 File Offset: 0x0000BA80
		protected override void View()
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
			base.SaveRightURL();
		}

		// Token: 0x040000A5 RID: 165
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000A6 RID: 166
		protected Permission permissioninfo = new Permission();
	}
}
