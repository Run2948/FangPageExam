using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000034 RID: 52
	public class typedisplay : AdminController
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000B2D8 File Offset: 0x000094D8
		protected override void View()
		{
			if (this.channelid > 0)
			{
				this.reurl = this.reurl + "?channelid=" + this.channelid;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.typelist = DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
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
				base.Response.Redirect("typedisplay.aspx?parentid=" + this.parentid);
			}
			base.SaveRightURL();
		}

		// Token: 0x0400007B RID: 123
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x0400007C RID: 124
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x0400007D RID: 125
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x0400007E RID: 126
		protected string reurl = FPRequest.GetString("reurl", "typemanage.aspx");
	}
}
