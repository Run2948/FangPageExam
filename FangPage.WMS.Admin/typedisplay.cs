using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000041 RID: 65
	public class typedisplay : AdminController
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000CD74 File Offset: 0x0000AF74
		protected override void Controller()
		{
			if (this.channelid > 0)
			{
				this.reurl = this.reurl + "?channelid=" + this.channelid;
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", this.parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.typelist = DbHelper.ExecuteList<TypeInfo>(sqlparams);
			if (this.ispost)
			{
				int num = 0;
				foreach (TypeInfo typeInfo in this.typelist)
				{
					this.typelist[num].display = FPRequest.GetInt("display_" + typeInfo.id);
					DbHelper.ExecuteUpdate<TypeInfo>(this.typelist[num]);
					num++;
				}
				CacheBll.RemoveSortCache();
				FPCache.Remove("FP_TYPELIST");
				base.Response.Redirect("typedisplay.aspx?parentid=" + this.parentid);
			}
		}

		// Token: 0x040000B3 RID: 179
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x040000B4 RID: 180
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x040000B5 RID: 181
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000B6 RID: 182
		protected string reurl = FPRequest.GetString("reurl", "typemanage.aspx");
	}
}
