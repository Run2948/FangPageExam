using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000055 RID: 85
	public class userdisplay : SuperController
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000FE64 File Offset: 0x0000E064
		protected override void Controller()
		{
			foreach (Department department in DepartmentBll.GetDepartList().FindAll((Department item) => FPArray.InArray(this.departid, item.parentlist) >= 0))
			{
				List<UserInfo> collection = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
				{
					DbHelper.MakeAndWhere("departid", department.id),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				});
				this.userlist.AddRange(collection);
			}
			if (this.ispost)
			{
				int num = 0;
				if (this.userlist.Count > 0)
				{
					num = this.userlist[0].display - 1;
				}
				int num2 = 0;
				foreach (UserInfo userInfo in this.userlist)
				{
					DepartmentBll.GetDepartInfo(userInfo.departid);
					this.userlist[num2].display = num + FPRequest.GetInt("display_" + userInfo.id);
					DbHelper.ExecuteUpdate<UserInfo>(this.userlist[num2]);
					num2++;
				}
				base.Response.Redirect("userdisplay.aspx?departid=" + this.departid);
			}
		}

		// Token: 0x040000F1 RID: 241
		protected new int departid = FPRequest.GetInt("departid");

		// Token: 0x040000F2 RID: 242
		protected List<UserInfo> userlist = new List<UserInfo>();
	}
}
