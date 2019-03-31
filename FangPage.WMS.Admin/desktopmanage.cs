using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000D RID: 13
	public class desktopmanage : SuperController
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000038D4 File Offset: 0x00001AD4
		protected override void Controller()
		{
			if (this.ispost)
			{
				int @int = FPRequest.GetInt("id");
				if (this.action == "delete")
				{
					DbHelper.ExecuteModel<DesktopInfo>(@int);
					if (DbHelper.ExecuteDelete<DesktopInfo>(@int) > 0)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", @int);
						DbHelper.ExecuteDelete<DesktopInfo>(new SqlParam[]
						{
							sqlParam
						});
					}
					FPCache.Remove("FP_DESKTOPLIST");
				}
			}
			this.desktoplist = DesktopBll.GetDesktopList().FindAll((DesktopInfo item) => item.parentid == 0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003970 File Offset: 0x00001B70
		protected List<DesktopInfo> GetChildDesktop(int parentid)
		{
			return DesktopBll.GetDesktopList().FindAll((DesktopInfo item) => item.parentid == parentid);
		}

		// Token: 0x04000020 RID: 32
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
