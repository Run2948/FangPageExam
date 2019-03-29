using System;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000004 RID: 4
	public class dbshrink : SuperController
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000028D0 File Offset: 0x00000AD0
		protected override void View()
		{
			this.dbconfig = DbConfigs.GetDbConfig();
			this.dbsize = FPUtils.FormatBytesStr(DbBll.GetDbSize());
			if (this.ispost)
			{
				DbBll.ShrinkDatabase();
			}
			base.SaveRightURL();
		}

		// Token: 0x04000004 RID: 4
		protected DbConfigInfo dbconfig = new DbConfigInfo();

		// Token: 0x04000005 RID: 5
		protected string dbsize = "";
	}
}
