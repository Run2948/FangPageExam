using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001F RID: 31
	public class sysmenuadd : SuperController
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00006B60 File Offset: 0x00004D60
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.menuinfo = DbHelper.ExecuteModel<MenuInfo>(this.id);
				this.parentid = this.menuinfo.parentid;
			}
			if (this.ispost)
			{
				this.menuinfo.hidden = 0;
				this.menuinfo = FPRequest.GetModel<MenuInfo>(this.menuinfo);
				if (this.menuinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<MenuInfo>(this.menuinfo);
				}
				else
				{
					this.menuinfo.platform = this.sysconfig.platform;
					SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.menuinfo.parentid);
					this.menuinfo.display = DbHelper.ExecuteCount<MenuInfo>(new SqlParam[]
					{
						sqlParam
					}) + 1;
					this.menuinfo.id = DbHelper.ExecuteInsert<MenuInfo>(this.menuinfo);
					RoleInfo mapRoleInfo = RoleBll.GetMapRoleInfo(1);
					RoleInfo roleInfo = mapRoleInfo;
					roleInfo.menus += ((mapRoleInfo.menus == "") ? this.menuinfo.id.ToString() : ("," + this.menuinfo.id));
					DbHelper.ExecuteUpdate<RoleInfo>(mapRoleInfo);
					base.ResetUser();
				}
				FPCache.Remove("FP_MENULIST");
				base.Response.Redirect("sysmenumanage.aspx");
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeOrWhere("(platform", "SYSTEM"),
				DbHelper.MakeOrWhere("platform)", this.sysconfig.platform),
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.menulist = DbHelper.ExecuteList<MenuInfo>(sqlparams);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006D20 File Offset: 0x00004F20
		protected List<MenuInfo> GetChildMenu(int parentid)
		{
			return DbHelper.ExecuteList<MenuInfo>(new SqlParam[]
			{
				DbHelper.MakeOrWhere("(platform", "SYSTEM"),
				DbHelper.MakeOrWhere("platform)", this.sysconfig.platform),
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x04000047 RID: 71
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000048 RID: 72
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000049 RID: 73
		protected MenuInfo parentmenuinfo = new MenuInfo();

		// Token: 0x0400004A RID: 74
		protected MenuInfo menuinfo = new MenuInfo();

		// Token: 0x0400004B RID: 75
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
