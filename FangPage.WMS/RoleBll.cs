using System;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200002F RID: 47
	public class RoleBll
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00007D8C File Offset: 0x00005F8C
		public static RoleInfo GetRoleInfo(int roleid)
		{
			return DbHelper.ExecuteModel<RoleInfo>(roleid);
		}
	}
}
