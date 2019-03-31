using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004A RID: 74
	public class rolemenus : SuperController
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000E9B0 File Offset: 0x0000CBB0
		protected override void Controller()
		{
			this.roleinfo = RoleBll.GetRoleInfo(this.rid);
			if (this.roleinfo.id == 0)
			{
				this.ShowErr("对不起，该角色不存在或已被删除。");
				return;
			}
			if (this.ispost)
			{
				this.roleinfo.menus = FPRequest.GetString("menus");
				DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
				if (this.roleinfo.id == this.roleid)
				{
					base.ResetUser();
				}
				FPCache.Remove("FP_ROLELIST");
				base.Response.Redirect(this.pagename + "?rid=" + this.rid);
			}
			this.zNodes = this.GetMenuTree(0);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000EA68 File Offset: 0x0000CC68
		private string GetMenuTree(int parentid)
		{
			List<MenuInfo> list = DbHelper.ExecuteList<MenuInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
			string text = "";
			foreach (MenuInfo menuInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				string text2 = "";
				if (FPArray.Contain(this.roleinfo.menus, menuInfo.id) || (this.roleinfo.id == 1 && menuInfo.system == 1))
				{
					text2 = "checked:true,";
				}
				text = string.Concat(new object[]
				{
					text,
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
					this.sitepath,
					"/statics/images/sysmenu2.gif\" }"
				});
				string menuTree = this.GetMenuTree(menuInfo.id);
				if (menuTree != "")
				{
					text = text + "," + menuTree;
				}
			}
			return text;
		}

		// Token: 0x040000D3 RID: 211
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x040000D4 RID: 212
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x040000D5 RID: 213
		protected string zNodes = "";
	}
}
