using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000046 RID: 70
	public class roleadd : SuperController
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000DA98 File Offset: 0x0000BC98
		protected override void View()
		{
			if (this.id > 0)
			{
				this.roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.id);
			}
			if (this.ispost)
			{
				this.roleinfo.isadmin = 0;
				this.roleinfo.isdownload = 0;
				this.roleinfo.isupload = 0;
				this.roleinfo = FPRequest.GetModel<RoleInfo>(this.roleinfo);
				if (this.roleinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
				}
				else
				{
					DbHelper.ExecuteInsert<RoleInfo>(this.roleinfo);
				}
				if (this.roleinfo.id == this.roleid)
				{
					base.ResetUser();
				}
				base.Response.Redirect("rolemanage.aspx");
			}
			base.SaveRightURL();
		}

		// Token: 0x040000A9 RID: 169
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000AA RID: 170
		protected RoleInfo roleinfo = new RoleInfo();
	}
}
