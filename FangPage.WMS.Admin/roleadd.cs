using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000050 RID: 80
	public class roleadd : SuperController
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		protected override void Controller()
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
					this.roleinfo.id = DbHelper.ExecuteInsert<RoleInfo>(this.roleinfo);
				}
				DbHelper.ExecuteUpdate<SessionInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("rolename", this.roleinfo.name),
					DbHelper.MakeAndWhere("roleid", this.roleinfo.id)
				});
				FPCache.Remove("FP_ROLELIST");
				base.ResetUser();
				base.Response.Redirect("rolemanage.aspx");
			}
		}

		// Token: 0x040000E0 RID: 224
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000E1 RID: 225
		protected RoleInfo roleinfo = new RoleInfo();
	}
}
