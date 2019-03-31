using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000039 RID: 57
	public class RoleBll
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000A459 File Offset: 0x00008659
		public static RoleInfo GetMapRoleInfo(int id)
		{
			return DbHelper.ExecuteModel<RoleInfo>(id);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A464 File Offset: 0x00008664
		public static RoleInfo GetRoleInfo(int id)
		{
			List<RoleInfo> list = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.id == id);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new RoleInfo();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000A4AC File Offset: 0x000086AC
		public static RoleInfo GetRoleInfo(string markup)
		{
			List<RoleInfo> list = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.markup == markup || item.name == markup);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new RoleInfo();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public static List<RoleInfo> GetRoleList()
		{
			object obj = FPCache.Get("FP_ROLELIST");
			List<RoleInfo> list;
			if (obj != null)
			{
				list = (obj as List<RoleInfo>);
			}
			else
			{
				list = DbHelper.ExecuteList<RoleInfo>(OrderBy.ASC);
				CacheBll.Insert("系统角色信息缓存", "FP_ROLELIST", list);
			}
			return list;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000A530 File Offset: 0x00008730
		public static RoleInfo CheckRole(string name)
		{
			List<RoleInfo> list = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.name == name);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new RoleInfo();
		}
	}
}
