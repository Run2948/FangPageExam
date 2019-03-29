using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001A RID: 26
	public class sysmenumanage : SuperController
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00005E4C File Offset: 0x0000404C
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeAndWhere("system", WhereType.NotEqual, 1),
						DbHelper.MakeAndWhere("id", WhereType.In, FPRequest.GetString("chkdel"))
					};
					DbHelper.ExecuteDelete<MenuInfo>(sqlparams);
				}
				else if (this.action == "desk")
				{
					int[] array = FPUtils.SplitInt(FPRequest.GetString("chkdel"));
					foreach (int id in array)
					{
						MenuInfo menuInfo = DbHelper.ExecuteModel<MenuInfo>(id);
						DbHelper.ExecuteInsert<DesktopInfo>(new DesktopInfo
						{
							name = menuInfo.name,
							lefturl = menuInfo.lefturl,
							righturl = menuInfo.righturl
						});
					}
				}
				base.Response.Redirect("sysmenumanage.aspx");
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.menulist = DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00005FB4 File Offset: 0x000041B4
		protected List<MenuInfo> GetChildMenu(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<MenuInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x04000034 RID: 52
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
