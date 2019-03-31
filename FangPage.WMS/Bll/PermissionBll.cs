using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000038 RID: 56
	public class PermissionBll
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000A414 File Offset: 0x00008614
		public static Permission GetPermission(int id)
		{
			return DbHelper.ExecuteModel<Permission>(id);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A41C File Offset: 0x0000861C
		public static List<Permission> GetPermissionList(string idlist)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("status", 1),
				DbHelper.MakeAndWhere("id", WhereType.In, idlist)
			};
			return DbHelper.ExecuteList<Permission>(OrderBy.ASC, sqlparams);
		}
	}
}
