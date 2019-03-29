using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200001D RID: 29
	public class usersearch : AdminController
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000DA08 File Offset: 0x0000BC08
		protected override void View()
		{
			List<SqlParam> list = new List<SqlParam>();
			if (this.departid > 0)
			{
				list.Add(DbHelper.MakeAndWhere("departid", WhereType.In, this.GetChildDepartid(this.departid)));
			}
			if (this.keyword != "")
			{
				list.Add(DbHelper.MakeAndWhere("username", WhereType.Like, this.keyword));
				list.Add(DbHelper.MakeOrWhere("realname", WhereType.Like, this.keyword));
			}
			if (list.Count > 0)
			{
				this.userlist = DbHelper.ExecuteList<UserInfo>(list.ToArray());
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		protected string GetChildDepartid(int parentid)
		{
			List<Department> departList = DepartmentBll.GetDepartList(parentid);
			string text = parentid.ToString();
			foreach (Department department in departList)
			{
				text = text + "," + this.GetChildDepartid(department.id);
			}
			return text;
		}

		// Token: 0x0400008A RID: 138
		protected new int departid = FPRequest.GetInt("departid");

		// Token: 0x0400008B RID: 139
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x0400008C RID: 140
		protected List<UserInfo> userlist = new List<UserInfo>();
	}
}
