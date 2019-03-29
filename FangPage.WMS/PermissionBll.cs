using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200002C RID: 44
	public class PermissionBll
	{
		// Token: 0x06000256 RID: 598 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public Permission GetPermission(int id)
		{
			return DbHelper.ExecuteModel<Permission>(id);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public List<Permission> GetPermissionList(string idlist)
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
