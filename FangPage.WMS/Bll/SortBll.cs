using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200003A RID: 58
	public class SortBll
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000A577 File Offset: 0x00008777
		public static SortInfo GetSortInfo(int id)
		{
			return DbHelper.ExecuteModel<SortInfo>(id);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000A580 File Offset: 0x00008780
		public static SortInfo GetSortInfo(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", markup);
			return DbHelper.ExecuteModel<SortInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public static SortInfo GetSortInfo(int channelid, string markup)
		{
			return DbHelper.ExecuteModel<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("markup", markup)
			});
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public static List<SortInfo> GetSortList(int parentid)
		{
			return DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000A604 File Offset: 0x00008804
		public static List<SortInfo> GetSortList(int channelid, int parentid)
		{
			return DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000A650 File Offset: 0x00008850
		public static List<SortInfo> GetSortList(int channelid, string sorts)
		{
			return DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("id", WhereType.In, sorts),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000A68D File Offset: 0x0000888D
		public static List<SortInfo> GetChannelSortList(int channelid)
		{
			return DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000A6BB File Offset: 0x000088BB
		public static string GetChildSorts(int sortid)
		{
			return SortBll.GetChildSorts(SortBll.GetSortInfo(sortid));
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public static string GetChildSorts(SortInfo sortinfo)
		{
			if (sortinfo.id <= 0)
			{
				return "";
			}
			string text = sortinfo.id.ToString();
			SqlParam sqlParam = DbHelper.MakeAndWhere("[parentlist] LIKE '" + sortinfo.parentlist + ",%'", WhereType.Custom, "");
			foreach (SortInfo sortInfo in DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				sqlParam
			}))
			{
				text = text + "," + sortInfo.id;
			}
			return text;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000A778 File Offset: 0x00008978
		public static void UpdateSortPosts(int sortid, int posts)
		{
			SortInfo sortInfo = SortBll.GetSortInfo(sortid);
			if (sortInfo.id == 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [posts]=[posts]+{1} WHERE [id] IN({2})|", DbConfigs.Prefix, posts, sortInfo.parentlist);
			stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [posts]=0 WHERE [posts]<0", DbConfigs.Prefix);
			DbHelper.ExecuteSql(stringBuilder.ToString());
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public static void UpdateSortPosts(string sorts, int posts)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, sorts);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				sqlParam
			});
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SortInfo sortInfo in list)
			{
				stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [posts]=[posts]+{1} WHERE [id] IN({2})|", DbConfigs.Prefix, posts, sortInfo.parentlist);
			}
			stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [posts]=0 WHERE [posts]<0", DbConfigs.Prefix);
			DbHelper.ExecuteSql(stringBuilder.ToString());
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000A878 File Offset: 0x00008A78
		public static string ResetSortPosts<T>(int sortid)
		{
			string childSorts = SortBll.GetChildSorts(sortid);
			SqlParam sqlParam = DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts);
			int num = DbHelper.ExecuteCount<T>(new SqlParam[]
			{
				sqlParam
			});
			return DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_SortInfo] SET [posts]={1} WHERE [id]={2}", DbConfigs.Prefix, num, sortid));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000A8CC File Offset: 0x00008ACC
		public static string ResetSortPosts<T>(List<SortInfo> sortlist)
		{
			string text = "";
			foreach (SortInfo sortInfo in sortlist)
			{
				string childSorts = SortBll.GetChildSorts(sortInfo);
				SqlParam sqlParam = DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts);
				int num = DbHelper.ExecuteCount<T>(new SqlParam[]
				{
					sqlParam
				});
				if (text != "")
				{
					text += "|";
				}
				text += string.Format("UPDATE [{0}WMS_SortInfo] SET [posts]={1} WHERE [id]={2}", DbConfigs.Prefix, num, sortInfo.id);
			}
			return DbHelper.ExecuteSql(text);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000A988 File Offset: 0x00008B88
		public static SortAppInfo GetSortAppInfo(int id)
		{
			List<SortAppInfo> list = SortBll.GetSortAppList().FindAll((SortAppInfo item) => item.id == id);
			if (list.Count > 0)
			{
				return list[0];
			}
			return new SortAppInfo();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public static List<SortAppInfo> GetSortAppList()
		{
			object obj = FPCache.Get("FP_SORTAPPLIST");
			List<SortAppInfo> list;
			if (obj != null)
			{
				list = (obj as List<SortAppInfo>);
			}
			else
			{
				list = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC);
				CacheBll.Insert("栏目功能信息缓存", "FP_SORTAPPLIST", list);
			}
			return list;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000AA0C File Offset: 0x00008C0C
		public static List<SortAppInfo> GetSortAppList(string markup)
		{
			return SortBll.GetSortAppList().FindAll((SortAppInfo item) => item.markup.Contains(markup));
		}
	}
}
