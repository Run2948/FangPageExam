using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000027 RID: 39
	public class channelmanage : AdminController
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00008414 File Offset: 0x00006614
		protected override void View()
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
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.channellist = DbHelper.ExecuteList<ChannelInfo>(orderby, new SqlParam[0]);
			base.SaveRightURL();
		}

		// Token: 0x0400004D RID: 77
		protected List<ChannelInfo> channellist = new List<ChannelInfo>();
	}
}
