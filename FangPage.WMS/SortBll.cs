using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000032 RID: 50
	public class SortBll
	{
		// Token: 0x0600026C RID: 620 RVA: 0x00008588 File Offset: 0x00006788
		public static SortInfo GetSortInfo(int id)
		{
			return DbHelper.ExecuteModel<SortInfo>(id);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000085A0 File Offset: 0x000067A0
		public static SortInfo GetSortInfo(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", markup);
			return DbHelper.ExecuteModel<SortInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000085D0 File Offset: 0x000067D0
		public static SortInfo GetSortInfo(int channelid, string markup)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("markup", markup)
			};
			return DbHelper.ExecuteModel<SortInfo>(sqlparams);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00008614 File Offset: 0x00006814
		public static List<SortInfo> GetSortList(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<SortInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008658 File Offset: 0x00006858
		public static List<SortInfo> GetSortList(int channelid, int parentid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("parentid", parentid)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000086AC File Offset: 0x000068AC
		public static List<SortInfo> GetChannelSortList(int channelid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("channelid", channelid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<SortInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000086F0 File Offset: 0x000068F0
		public static string GetChildSorts(int sortid)
		{
			SortInfo sortInfo = SortBll.GetSortInfo(sortid);
			return SortBll.GetChildSorts(sortInfo);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008710 File Offset: 0x00006910
		public static string GetChildSorts(SortInfo sortinfo)
		{
			string result;
			if (sortinfo.id <= 0)
			{
				result = "";
			}
			else
			{
				string text = sortinfo.id.ToString();
				SqlParam sqlParam = DbHelper.MakeAndWhere("[parentlist] LIKE '" + sortinfo.parentlist + ",%'", WhereType.Custom, "");
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
				{
					sqlParam
				});
				foreach (SortInfo sortInfo in list)
				{
					text = text + "," + sortInfo.id;
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000087E4 File Offset: 0x000069E4
		public static void UpdateSortPosts(int sortid, int posts)
		{
			SortInfo sortInfo = SortBll.GetSortInfo(sortid);
			if (sortInfo.id != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [posts]=[posts]+{1} WHERE [id] IN({2})|", DbConfigs.Prefix, posts, sortInfo.parentlist);
				stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [posts]=0 WHERE [posts]<0", DbConfigs.Prefix);
				DbHelper.ExecuteSql(stringBuilder.ToString());
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000884C File Offset: 0x00006A4C
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

		// Token: 0x06000276 RID: 630 RVA: 0x00008900 File Offset: 0x00006B00
		public static string ResetSortPosts<T>(int sortid)
		{
			string childSorts = SortBll.GetChildSorts(sortid);
			SqlParam sqlParam = DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts);
			int num = DbHelper.ExecuteCount<T>(new SqlParam[]
			{
				sqlParam
			});
			string sqlstring = string.Format("UPDATE [{0}WMS_SortInfo] SET [posts]={1} WHERE [id]={2}", DbConfigs.Prefix, num, sortid);
			return DbHelper.ExecuteSql(sqlstring);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00008960 File Offset: 0x00006B60
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

		// Token: 0x06000278 RID: 632 RVA: 0x00008A3C File Offset: 0x00006C3C
		public static SortAppInfo GetSortAppInfo(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", markup);
			return DbHelper.ExecuteModel<SortAppInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008A6C File Offset: 0x00006C6C
		public static List<SortAppInfo> GetSortAppList(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", WhereType.Like, markup);
			return DbHelper.ExecuteList<SortAppInfo>(new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
