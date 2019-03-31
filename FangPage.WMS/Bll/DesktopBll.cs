using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200002B RID: 43
	public class DesktopBll
	{
		// Token: 0x06000347 RID: 839 RVA: 0x000073C0 File Offset: 0x000055C0
		public static List<DesktopInfo> GetDesktopList(string markup, string deskflag)
		{
			List<DesktopInfo> result = new List<DesktopInfo>();
			DesktopInfo desktopInfo = DesktopBll.GetDesktopInfo(markup);
			if (desktopInfo.id > 0)
			{
				if (!string.IsNullOrEmpty(deskflag))
				{
					result = DesktopBll.GetDesktopList(desktopInfo.id).FindAll((DesktopInfo item) => item.hidden == 0 && FPArray.InArray(item.id, deskflag) >= 0);
				}
				else
				{
					result = DesktopBll.GetDesktopList(desktopInfo.id).FindAll((DesktopInfo item) => item.hidden == 0);
				}
			}
			return result;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000744D File Offset: 0x0000564D
		public static int GetMaxDesk(string markup)
		{
			return DesktopBll.GetMaxDesk(markup, "");
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000745C File Offset: 0x0000565C
		public static int GetMaxDesk(string markup, string desktops)
		{
			DesktopInfo desktopInfo = DesktopBll.GetDesktopInfo(markup);
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("parentid", desktopInfo.id));
			if (!string.IsNullOrEmpty(desktops))
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, desktops));
			}
			return FPUtils.StrToInt(DbHelper.ExecuteMax<DesktopInfo>("desk", list.ToArray()));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000074C0 File Offset: 0x000056C0
		public static List<DesktopInfo> GetDesktopList(string markup, string deskflag, int desk)
		{
			List<DesktopInfo> result = new List<DesktopInfo>();
			DesktopInfo desktopInfo = DesktopBll.GetDesktopInfo(markup);
			if (desktopInfo.id > 0)
			{
				if (!string.IsNullOrEmpty(deskflag))
				{
					result = DesktopBll.GetDesktopList(desktopInfo.id).FindAll((DesktopInfo item) => item.hidden == 0 && item.desk == desk && FPArray.InArray(item.id, deskflag) >= 0);
				}
				else
				{
					result = DesktopBll.GetDesktopList(desktopInfo.id).FindAll((DesktopInfo item) => item.hidden == 0 && item.desk == desk);
				}
			}
			return result;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00007544 File Offset: 0x00005744
		public static List<DesktopInfo> GetDesktopList(int parentid)
		{
			return DesktopBll.GetDesktopList().FindAll((DesktopInfo item) => item.hidden == 0 && item.parentid == parentid);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00007574 File Offset: 0x00005774
		public static List<DesktopInfo> GetDesktopList()
		{
			SysConfig config = SysConfigs.GetConfig();
			object obj = FPCache.Get("FP_DESKTOPLIST");
			List<DesktopInfo> list;
			if (obj != null)
			{
				list = (obj as List<DesktopInfo>);
			}
			else
			{
				list = DbHelper.ExecuteList<DesktopInfo>(new SqlParam[]
				{
					DbHelper.MakeAndWhere('(', "platform", "SYSTEM"),
					DbHelper.MakeOrWhere("platform", ')', WhereType.Contain, config.platform),
					DbHelper.MakeOrderBy("parentid", OrderBy.ASC),
					DbHelper.MakeOrderBy("desk", OrderBy.ASC),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				});
				CacheBll.Insert("系统桌面信息缓存", "FP_DESKTOPLIST", list);
			}
			return list;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00007610 File Offset: 0x00005810
		public static DesktopInfo GetDesktopInfo(string markup)
		{
			List<DesktopInfo> list = DesktopBll.GetDesktopList().FindAll((DesktopInfo item) => item.markup.ToLower() == markup.ToLower());
			if (list.Count > 0)
			{
				return list[0];
			}
			return new DesktopInfo();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00007658 File Offset: 0x00005858
		public static DesktopInfo GetDesktopInfo(int id)
		{
			List<DesktopInfo> list = DesktopBll.GetDesktopList().FindAll((DesktopInfo item) => item.id == id);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new DesktopInfo();
		}
	}
}
