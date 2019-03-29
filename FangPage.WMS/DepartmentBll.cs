using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000023 RID: 35
	public class DepartmentBll
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00006F28 File Offset: 0x00005128
		public static List<Department> GetDepartList(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<Department>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
