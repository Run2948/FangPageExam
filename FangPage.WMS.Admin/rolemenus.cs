using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003F RID: 63
	public class rolemenus : SuperController
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000CCD8 File Offset: 0x0000AED8
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
					this.roleinfo.menus = FPRequest.GetString("menus");
					DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
					if (this.roleinfo.id == this.roleid)
					{
						base.ResetUser();
					}
					base.Response.Redirect(this.pagename + "?rid=" + this.rid);
				}
				this.zNodes = this.GetMenuTree(0);
				base.SaveRightURL();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		private string GetMenuTree(int parentid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<MenuInfo> list = DbHelper.ExecuteList<MenuInfo>(orderby, sqlparams);
			string text = "";
			foreach (MenuInfo menuInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				string text2 = "";
				if (base.ischecked(menuInfo.id, this.roleinfo.menus) || (this.roleinfo.id == 1 && menuInfo.system == 1))
				{
					text2 = "checked:true,";
				}
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"{ id: ",
					menuInfo.id,
					", pId: ",
					parentid,
					", name: \"",
					menuInfo.name,
					"\",",
					text2,
					"open:true, icon: \"",
					this.webpath,
					(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
					"images/sysmenu2.gif\" }"
				});
				string menuTree = this.GetMenuTree(menuInfo.id);
				if (menuTree != "")
				{
					text = text + "," + menuTree;
				}
			}
			return text;
		}

		// Token: 0x0400009A RID: 154
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x0400009B RID: 155
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x0400009C RID: 156
		protected string zNodes = "";
	}
}
