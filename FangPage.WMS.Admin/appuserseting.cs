using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000007 RID: 7
	public class appuserseting : SuperController
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002ED4 File Offset: 0x000010D4
		protected override void View()
		{
			if (this.id > 0)
			{
				this.appinfo = DbHelper.ExecuteModel<AppInfo>(this.id);
			}
			base.SaveRightURL();
		}

		// Token: 0x04000009 RID: 9
		protected int id = FPRequest.GetInt("id");

		// Token: 0x0400000A RID: 10
		protected int sortappid = FPRequest.GetInt("sortappid");

		// Token: 0x0400000B RID: 11
		protected AppInfo appinfo = new AppInfo();
	}
}
