using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000046 RID: 70
	public class TypeBll
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000B2FC File Offset: 0x000094FC
		public static List<TypeInfo> GetTypeList(string types)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, types);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000B33C File Offset: 0x0000953C
		public static List<TypeInfo> GetTypeList(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B380 File Offset: 0x00009580
		public static List<TypeInfo> GetTypeListByMarkup(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", markup);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B3BC File Offset: 0x000095BC
		public static void UpdateTypePosts(int typeid, int posts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [posts]=[posts]+{1} WHERE [id]={2}|", DbConfigs.Prefix, posts, typeid);
			stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [posts]=0 WHERE [posts]<0", DbConfigs.Prefix, posts, typeid);
			DbHelper.ExecuteSql(stringBuilder.ToString());
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B418 File Offset: 0x00009618
		public static void UpdateTypePosts(string types, int posts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [posts]=[posts]+{1} WHERE [id] IN({2})|", DbConfigs.Prefix, posts, types);
			stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [posts]=0 WHERE [posts]<0", DbConfigs.Prefix, posts, types);
			DbHelper.ExecuteSql(stringBuilder.ToString());
		}
	}
}
