using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000A RID: 10
	public class desktopdisplay : SuperController
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000030D8 File Offset: 0x000012D8
		protected override void Controller()
		{
			this.desktoplist = DesktopBll.GetDesktopList().FindAll((DesktopInfo item) => item.parentid == this.parentid && item.desk == this.desk);
			if (this.ispost && this.action == "display")
			{
				int num = 0;
				foreach (DesktopInfo desktopInfo in this.desktoplist)
				{
					this.desktoplist[num].display = FPRequest.GetInt("display_" + desktopInfo.id);
					DbHelper.ExecuteUpdate<DesktopInfo>(this.desktoplist[num]);
					num++;
				}
				FPCache.Remove("FP_DESKTOPLIST");
				base.Response.Redirect(this.pageurl);
			}
		}

		// Token: 0x04000014 RID: 20
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000015 RID: 21
		protected int desk = FPRequest.GetInt("desk");

		// Token: 0x04000016 RID: 22
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
