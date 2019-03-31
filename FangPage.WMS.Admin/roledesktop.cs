using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004B RID: 75
	public class roledesktop : SuperController
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		protected override void Controller()
		{
			this.roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.rid);
			if (this.roleinfo.id == 0)
			{
				this.ShowErr("对不起，该角色不存在或已被删除。");
				return;
			}
			if (this.ispost)
			{
				this.roleinfo.desktop = FPRequest.GetString("desktop");
				DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
				if (this.roleinfo.id == this.roleid)
				{
					base.ResetUser();
				}
				FPCache.Remove("FP_ROLELIST");
				base.Response.Redirect(this.pagename + "?rid=" + this.rid);
			}
			this.zNodes = this.GetDeskTree(0);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		private string GetDeskTree(int parentid)
		{
			List<DesktopInfo> list = DbHelper.ExecuteList<DesktopInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("desk", OrderBy.ASC),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
			string text = "";
			foreach (DesktopInfo desktopInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				string text2 = "";
				if (FPArray.Contain(this.roleinfo.desktop, desktopInfo.id))
				{
					text2 = "checked:true,";
				}
				text = string.Concat(new object[]
				{
					text,
					"{ id: ",
					desktopInfo.id,
					", pId: ",
					parentid,
					", name: \"",
					desktopInfo.name,
					"\",",
					text2,
					"open:true, icon: \"",
					this.webpath,
					this.sitepath,
					"/statics/images/bdesk.gif\" }"
				});
				string deskTree = this.GetDeskTree(desktopInfo.id);
				if (deskTree != "")
				{
					text = text + "," + deskTree;
				}
			}
			return text;
		}

		// Token: 0x040000D6 RID: 214
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x040000D7 RID: 215
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x040000D8 RID: 216
		protected string zNodes = "";
	}
}
