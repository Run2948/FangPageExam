using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000034 RID: 52
	public class channelmanage : AdminController
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00009D9C File Offset: 0x00007F9C
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				string @string = FPRequest.GetString("chkdel");
				if (DbHelper.ExecuteDelete<ChannelInfo>(@string) > 0)
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("channelid", WhereType.In, @string);
					DbHelper.ExecuteDelete<SortInfo>(new SqlParam[]
					{
						sqlParam
					});
				}
			}
			SqlParam sqlParam2 = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.channellist = DbHelper.ExecuteList<ChannelInfo>(new SqlParam[]
			{
				sqlParam2
			});
		}

		// Token: 0x04000084 RID: 132
		protected List<ChannelInfo> channellist = new List<ChannelInfo>();
	}
}
