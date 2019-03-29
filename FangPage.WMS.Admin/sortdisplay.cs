using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000031 RID: 49
	public class sortdisplay : SuperController
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000A94C File Offset: 0x00008B4C
		protected override void View()
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("parentid", this.parentid)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.sortlist = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
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
			base.SaveRightURL();
		}

		// Token: 0x04000070 RID: 112
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000071 RID: 113
		public List<SortInfo> sortlist = new List<SortInfo>();

		// Token: 0x04000072 RID: 114
		public int parentid = FPRequest.GetInt("parentid");
	}
}
