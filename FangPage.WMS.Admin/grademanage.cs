using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000057 RID: 87
	public class grademanage : SuperController
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x000100E0 File Offset: 0x0000E2E0
		protected override void Controller()
		{
			if (this.ispost && this.action == "delete")
			{
				DbHelper.ExecuteDelete<GradeInfo>(FPRequest.GetString("chkdel"));
				FPCache.Remove("FP_GRADELIST");
			}
			SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.gradelist = DbHelper.ExecuteList<GradeInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (this.ispost && this.action == "display")
			{
				int num = 0;
				foreach (GradeInfo gradeInfo in this.gradelist)
				{
					this.gradelist[num].display = FPRequest.GetInt("display_" + gradeInfo.id);
					DbHelper.ExecuteUpdate<GradeInfo>(this.gradelist[num]);
					num++;
				}
				FPCache.Remove("FP_GRADELIST");
				base.Response.Redirect("grademanage.aspx");
			}
		}

		// Token: 0x040000F5 RID: 245
		protected List<GradeInfo> gradelist = new List<GradeInfo>();
	}
}
