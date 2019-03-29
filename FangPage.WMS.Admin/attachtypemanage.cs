using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000009 RID: 9
	public class attachtypemanage : SuperController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000307C File Offset: 0x0000127C
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<AttachType>(FPRequest.GetString("chkdel"));
					FPCache.Remove("FP_ATTACHTYPE", "image,flash,media,file");
				}
				else if (this.action == "add")
				{
					AttachType attachType = FPRequest.GetModel<AttachType>();
					DbHelper.ExecuteInsert<AttachType>(attachType);
					FPCache.Remove("FP_ATTACHTYPE" + attachType.type);
				}
				else if (this.action == "edit")
				{
					AttachType attachType = DbHelper.ExecuteModel<AttachType>(this.id);
					attachType = FPRequest.GetModel<AttachType>(attachType, "edit_");
					DbHelper.ExecuteUpdate<AttachType>(attachType);
					FPCache.Remove("FP_ATTACHTYPE" + attachType.type);
				}
				base.Response.Redirect(this.pagename);
			}
			OrderByParam orderby = DbHelper.MakeOrderBy("type", OrderBy.ASC);
			this.attachlist = DbHelper.ExecuteList<AttachType>(orderby, new SqlParam[0]);
			base.SaveRightURL();
		}

		// Token: 0x0400000F RID: 15
		protected List<AttachType> attachlist = new List<AttachType>();

		// Token: 0x04000010 RID: 16
		protected int id = FPRequest.GetInt("id");
	}
}
