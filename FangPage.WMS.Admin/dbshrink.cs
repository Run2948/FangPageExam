using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Bll;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000004 RID: 4
	public class dbshrink : SuperController
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002708 File Offset: 0x00000908
		protected override void Controller()
		{
			this.dbconfig = DbConfigs.GetDbConfig();
			this.dbsize = FPFile.FormatBytesStr(DbBll.GetDbSize());
			if (this.ispost)
			{
				DbBll.ShrinkDatabase();
			}
		}

		// Token: 0x04000005 RID: 5
		protected DbConfigInfo dbconfig = new DbConfigInfo();

		// Token: 0x04000006 RID: 6
		protected string dbsize = "";
	}
}
