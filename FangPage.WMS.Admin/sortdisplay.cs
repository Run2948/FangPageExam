using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003E RID: 62
	public class sortdisplay : SuperController
	{
		// Token: 0x06000093 RID: 147 RVA: 0x0000C520 File Offset: 0x0000A720
		protected override void Controller()
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("parentid", this.parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.sortlist = DbHelper.ExecuteList<SortInfo>(sqlparams);
			if (this.ispost)
			{
				int num = 0;
				foreach (SortInfo sortInfo in this.sortlist)
				{
					this.sortlist[num].display = FPRequest.GetInt("display_" + sortInfo.id);
					DbHelper.ExecuteUpdate<SortInfo>(this.sortlist[num]);
					FPCache.Remove("FP_SORTTREE" + sortInfo.channelid);
					num++;
				}
				base.Response.Redirect(string.Concat(new object[]
				{
					"sortdisplay.aspx?channelid=",
					this.channelid,
					"&parentid=",
					this.parentid
				}));
			}
		}

		// Token: 0x040000A8 RID: 168
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000A9 RID: 169
		public List<SortInfo> sortlist = new List<SortInfo>();

		// Token: 0x040000AA RID: 170
		public int parentid = FPRequest.GetInt("parentid");
	}
}
