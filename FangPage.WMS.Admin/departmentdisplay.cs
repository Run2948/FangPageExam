using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003C RID: 60
	public class departmentdisplay : SuperController
	{
		// Token: 0x06000093 RID: 147 RVA: 0x0000C774 File Offset: 0x0000A974
		protected override void View()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.departmentlist = DbHelper.ExecuteList<Department>(orderby, new SqlParam[]
			{
				sqlParam
			});
			if (this.ispost)
			{
				int num = 0;
				foreach (Department department in this.departmentlist)
				{
					this.departmentlist[num].display = FPRequest.GetInt("display_" + department.id);
					DbHelper.ExecuteUpdate<Department>(this.departmentlist[num]);
					num++;
				}
				base.Response.Redirect(this.cururl + "?parentid=" + this.parentid);
			}
			base.SaveRightURL();
		}

		// Token: 0x04000094 RID: 148
		public List<Department> departmentlist = new List<Department>();

		// Token: 0x04000095 RID: 149
		public int parentid = FPRequest.GetInt("parentid");
	}
}
