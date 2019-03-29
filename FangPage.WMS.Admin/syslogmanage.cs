using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000017 RID: 23
	public class syslogmanage : SuperController
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00005A50 File Offset: 0x00003C50
		protected override void View()
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
			base.SaveRightURL();
		}

		// Token: 0x0400002C RID: 44
		protected List<SysLogInfo> sysloglist = new List<SysLogInfo>();

		// Token: 0x0400002D RID: 45
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
