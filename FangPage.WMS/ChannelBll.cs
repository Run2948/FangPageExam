using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000012 RID: 18
	public class ChannelBll
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00004E44 File Offset: 0x00003044
		public static List<ChannelInfo> GetChannelList()
		{
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<ChannelInfo>(orderby, new SqlParam[0]);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004E70 File Offset: 0x00003070
		public static List<ChannelInfo> GetChannelList(string idmarkup)
		{
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<ChannelInfo> result;
			if (FPUtils.IsNumericArray(idmarkup))
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, idmarkup);
				result = DbHelper.ExecuteList<ChannelInfo>(orderby, new SqlParam[]
				{
					sqlParam
				});
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("markup", WhereType.Like, idmarkup);
				result = DbHelper.ExecuteList<ChannelInfo>(orderby, new SqlParam[]
				{
					sqlParam
				});
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004EE4 File Offset: 0x000030E4
		public static ChannelInfo GetChannelInfo(int id)
		{
			return DbHelper.ExecuteModel<ChannelInfo>(id);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004EFC File Offset: 0x000030FC
		public static ChannelInfo GetChannelInfo(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", markup);
			return DbHelper.ExecuteModel<ChannelInfo>(new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
