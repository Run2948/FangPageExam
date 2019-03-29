using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000018 RID: 24
	public class sysmenuadd : SuperController
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00005AE8 File Offset: 0x00003CE8
		protected override void View()
		{
			if (this.id > 0)
			{
				this.menuinfo = DbHelper.ExecuteModel<MenuInfo>(this.id);
				this.parentid = this.menuinfo.parentid;
			}
			if (this.ispost)
			{
				this.menuinfo = FPRequest.GetModel<MenuInfo>(this.menuinfo);
				if (this.menuinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<MenuInfo>(this.menuinfo);
				}
				else
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.menuinfo.parentid);
					this.menuinfo.display = DbHelper.ExecuteCount<MenuInfo>(new SqlParam[]
					{
						sqlParam
					}) + 1;
					this.menuinfo.id = DbHelper.ExecuteInsert<MenuInfo>(this.menuinfo);
					RoleInfo roleInfo = RoleBll.GetRoleInfo(1);
					RoleInfo roleInfo2 = roleInfo;
					roleInfo2.menus += ((roleInfo.menus == "") ? this.menuinfo.id.ToString() : ("," + this.menuinfo.id));
					DbHelper.ExecuteUpdate<RoleInfo>(roleInfo);
					base.ResetUser();
				}
				base.Response.Redirect("sysmenumanage.aspx");
			}
			SqlParam sqlParam2 = DbHelper.MakeAndWhere("parentid", 0);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.menulist = DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam2
			});
			base.SaveRightURL();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005C8C File Offset: 0x00003E8C
		protected List<MenuInfo> GetChildMenu(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x0400002E RID: 46
		protected int id = FPRequest.GetInt("id");

		// Token: 0x0400002F RID: 47
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000030 RID: 48
		protected MenuInfo menuinfo = new MenuInfo();

		// Token: 0x04000031 RID: 49
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
