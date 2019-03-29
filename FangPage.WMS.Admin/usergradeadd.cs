using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004D RID: 77
	public class usergradeadd : SuperController
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000E544 File Offset: 0x0000C744
		protected override void View()
		{
			if (this.id > 0)
			{
				this.grade = DbHelper.ExecuteModel<UserGrade>(this.id);
			}
			if (this.ispost)
			{
				this.grade = FPRequest.GetModel<UserGrade>(this.grade);
				if (this.id > 0)
				{
					DbHelper.ExecuteUpdate<UserGrade>(this.grade);
				}
				else
				{
					DbHelper.ExecuteInsert<UserGrade>(this.grade);
				}
				base.Response.Redirect("usergrademanage.aspx");
			}
			base.SaveRightURL();
		}

		// Token: 0x040000BD RID: 189
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000BE RID: 190
		protected UserGrade grade = new UserGrade();
	}
}
