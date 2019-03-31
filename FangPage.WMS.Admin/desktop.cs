using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000028 RID: 40
	public class desktop : AdminController
	{
		// Token: 0x0600005E RID: 94 RVA: 0x0000777E File Offset: 0x0000597E
		protected override void Controller()
		{
			this.desks = DesktopBll.GetMaxDesk("wms_desktop", this.role.desktop);
			this.syspath = FPFile.GetMapPath(this.webpath);
			this.dbconfig = DbConfigs.GetDbConfig();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000077B7 File Offset: 0x000059B7
		protected List<DesktopInfo> GetDesktopList(int desk)
		{
			return DesktopBll.GetDesktopList("wms_desktop", this.role.desktop, desk);
		}

		// Token: 0x04000056 RID: 86
		protected DbConfigInfo dbconfig = new DbConfigInfo();

		// Token: 0x04000057 RID: 87
		protected string syspath = "";

		// Token: 0x04000058 RID: 88
		protected int desks;
	}
}
