using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001E RID: 30
	public class syslogmanage : SuperController
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00006AE4 File Offset: 0x00004CE4
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<SysLogInfo>(FPRequest.GetString("chkdel"));
				}
			}
			this.sysloglist = DbHelper.ExecuteList<SysLogInfo>(this.pager);
		}

		// Token: 0x04000045 RID: 69
		protected List<SysLogInfo> sysloglist = new List<SysLogInfo>();

		// Token: 0x04000046 RID: 70
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
