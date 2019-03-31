using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000035 RID: 53
	public class ChannelBll
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000A07C File Offset: 0x0000827C
		public static List<ChannelInfo> GetChannelList()
		{
			SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<ChannelInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public static List<ChannelInfo> GetChannelList(string idmarkup)
		{
			SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			if (FPUtils.IsNumericArray(idmarkup))
			{
				SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, idmarkup);
				return DbHelper.ExecuteList<ChannelInfo>(new SqlParam[]
				{
					sqlParam,
					sqlParam2
				});
			}
			SqlParam sqlParam3 = DbHelper.MakeAndWhere("markup", WhereType.Like, idmarkup);
			return DbHelper.ExecuteList<ChannelInfo>(new SqlParam[]
			{
				sqlParam,
				sqlParam3
			});
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A106 File Offset: 0x00008306
		public static ChannelInfo GetChannelInfo(int id)
		{
			return DbHelper.ExecuteModel<ChannelInfo>(id);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A110 File Offset: 0x00008310
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
