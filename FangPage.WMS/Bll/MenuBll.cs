using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200002F RID: 47
	public class MenuBll
	{
		// Token: 0x0600036D RID: 877 RVA: 0x000083D4 File Offset: 0x000065D4
		public static List<MenuInfo> GetMenuList()
		{
			SysConfig config = SysConfigs.GetConfig();
			object obj = FPCache.Get("FP_MENULIST");
			List<MenuInfo> list;
			if (obj != null)
			{
				list = (obj as List<MenuInfo>);
			}
			else
			{
				list = DbHelper.ExecuteList<MenuInfo>(new SqlParam[]
				{
					DbHelper.MakeOrWhere("(platform", "SYSTEM"),
					DbHelper.MakeOrWhere("platform)", config.platform),
					DbHelper.MakeAndWhere("hidden", 0),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				});
				CacheBll.Insert("系统菜单信息缓存", "FP_MENULIST", list, 30);
			}
			return list;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008464 File Offset: 0x00006664
		public static List<MenuInfo> GetMenuList(int parentid)
		{
			return MenuBll.GetMenuList().FindAll((MenuInfo item) => item.parentid == parentid);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00008494 File Offset: 0x00006694
		public static List<MenuInfo> GetMenuList(int parentid, string flagmenus)
		{
			return MenuBll.GetMenuList().FindAll((MenuInfo item) => item.parentid == parentid && FPArray.InArray(item.id, flagmenus) >= 0);
		}
	}
}
