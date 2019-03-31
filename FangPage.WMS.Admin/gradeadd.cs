using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000056 RID: 86
	public class gradeadd : SuperController
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00010020 File Offset: 0x0000E220
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.grade = DbHelper.ExecuteModel<GradeInfo>(this.id);
			}
			if (this.ispost)
			{
				this.grade = FPRequest.GetModel<GradeInfo>(this.grade);
				if (this.id > 0)
				{
					DbHelper.ExecuteUpdate<GradeInfo>(this.grade);
				}
				else
				{
					int num = FPUtils.StrToInt(DbHelper.ExecuteMax<GradeInfo>("display"));
					this.grade.display = num + 1;
					DbHelper.ExecuteInsert<GradeInfo>(this.grade);
				}
				FPCache.Remove("FP_GRADELIST");
				base.Response.Redirect("grademanage.aspx");
			}
		}

		// Token: 0x040000F3 RID: 243
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000F4 RID: 244
		protected GradeInfo grade = new GradeInfo();
	}
}
