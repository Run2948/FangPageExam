using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000021 RID: 33
	public class sysmenumanage : SuperController
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00006F2C File Offset: 0x0000512C
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, FPRequest.GetString("chkdel"));
					DbHelper.ExecuteDelete<MenuInfo>(new SqlParam[]
					{
						sqlParam
					});
					FPCache.Remove("FP_MENULIST");
				}
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

		// Token: 0x0600004F RID: 79 RVA: 0x00006FF4 File Offset: 0x000051F4
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

		// Token: 0x0400004E RID: 78
		protected List<MenuInfo> menulist = new List<MenuInfo>();
	}
}
