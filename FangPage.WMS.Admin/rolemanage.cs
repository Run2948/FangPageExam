using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000051 RID: 81
	public class rolemanage : SuperController
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000F614 File Offset: 0x0000D814
		protected override void Controller()
		{
			if (this.ispost && this.action == "delete")
			{
				foreach (int num in FPArray.SplitInt(FPRequest.GetString("chkdel")))
				{
					if (num > 5)
					{
						DbHelper.ExecuteDelete<RoleInfo>(num);
					}
				}
				FPCache.Remove("FP_ROLELIST");
			}
			this.rolelist = DbHelper.ExecuteList<RoleInfo>(OrderBy.ASC);
		}

		// Token: 0x040000E2 RID: 226
		protected List<RoleInfo> rolelist = new List<RoleInfo>();
	}
}
