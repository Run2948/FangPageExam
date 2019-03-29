using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000047 RID: 71
	public class rolemanage : SuperController
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					foreach (int num in FPUtils.SplitInt(FPRequest.GetString("chkdel")))
					{
						if (num > 5)
						{
							DbHelper.ExecuteDelete<RoleInfo>(num);
						}
					}
				}
			}
			this.rolelist = DbHelper.ExecuteList<RoleInfo>(OrderBy.ASC);
			base.SaveRightURL();
		}

		// Token: 0x040000AB RID: 171
		protected List<RoleInfo> rolelist = new List<RoleInfo>();
	}
}
