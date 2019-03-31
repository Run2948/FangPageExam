using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002B RID: 43
	public class deptbatch : SuperController
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00007C3C File Offset: 0x00005E3C
		protected override void Controller()
		{
			if (this.ispost)
			{
				List<SqlParam> list = new List<SqlParam>();
				if (this.types != "")
				{
					list.Add(DbHelper.MakeUpdate("types", this.types));
				}
				if (this.manager != "")
				{
					if (this.manager == "clear")
					{
						list.Add(DbHelper.MakeUpdate("manager", ""));
					}
					else
					{
						list.Add(DbHelper.MakeUpdate("manager", this.manager));
					}
				}
				if (this.departer != "")
				{
					if (this.departer == "clear")
					{
						list.Add(DbHelper.MakeUpdate("departer", ""));
					}
					else
					{
						list.Add(DbHelper.MakeUpdate("departer", this.departer));
					}
				}
				if (list.Count > 0)
				{
					if (this.idlist != "")
					{
						list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.idlist));
					}
					else
					{
						list.Add(DbHelper.MakeAndWhere("id", WhereType.GreaterThan, 0));
					}
					DbHelper.ExecuteUpdate<Department>(list.ToArray());
					FPCache.Remove("FP_DEPARTLIST");
					FPCache.Remove("FP_DEPARTMANAGER");
				}
				base.Response.Redirect("departmentmanage.aspx");
			}
			this.typelist = TypeBll.GetTypeListByMarkup("depart_type");
		}

		// Token: 0x0400005D RID: 93
		protected string idlist = FPRequest.GetString("idlist");

		// Token: 0x0400005E RID: 94
		protected string types = FPRequest.GetString("types");

		// Token: 0x0400005F RID: 95
		protected string manager = FPRequest.GetString("manager");

		// Token: 0x04000060 RID: 96
		protected new string departer = FPRequest.GetString("departer");

		// Token: 0x04000061 RID: 97
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
