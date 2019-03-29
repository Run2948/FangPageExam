using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000C RID: 12
	public class desktopmanage : SuperController
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00003590 File Offset: 0x00001790
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<DesktopInfo>(new List<SqlParam>
					{
						DbHelper.MakeAndWhere("id", WhereType.In, FPRequest.GetString("chkdel"))
					}.ToArray());
				}
				base.Response.Redirect("desktopmanage.aspx");
			}
			this.desktoplist = DbHelper.ExecuteList<DesktopInfo>(OrderBy.ASC);
			base.SaveRightURL();
		}

		// Token: 0x04000013 RID: 19
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
