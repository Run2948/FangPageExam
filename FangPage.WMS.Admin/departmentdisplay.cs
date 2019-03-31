using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000047 RID: 71
	public class departmentdisplay : SuperController
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000DF30 File Offset: 0x0000C130
		protected override void Controller()
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", this.parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.departmentlist = DbHelper.ExecuteList<Department>(sqlparams);
			if (this.ispost)
			{
				Department departInfo = DepartmentBll.GetDepartInfo(this.parentid);
				int num = 0;
				foreach (Department department in this.departmentlist)
				{
					this.departmentlist[num].display = departInfo.display + FPRequest.GetInt("display_" + department.id);
					DbHelper.ExecuteUpdate<Department>(this.departmentlist[num]);
					num++;
				}
				FPCache.Remove("FP_DEPARTLIST");
				base.Response.Redirect(string.Concat(new object[]
				{
					"departmentdisplay.aspx?parentid=",
					this.parentid,
					"&departname=",
					this.departname
				}));
			}
		}

		// Token: 0x040000C8 RID: 200
		public List<Department> departmentlist = new List<Department>();

		// Token: 0x040000C9 RID: 201
		public int parentid = FPRequest.GetInt("parentid");

		// Token: 0x040000CA RID: 202
		protected string departname = FPRequest.GetString("departname");
	}
}
