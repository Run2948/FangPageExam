using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000053 RID: 83
	public class usersearch : SuperController
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		protected override void View()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
			this.deparlist = DbHelper.ExecuteList<Department>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
			sqlParam = DbHelper.MakeAndWhere("id", WhereType.NotEqual, 2);
			this.rolelist = DbHelper.ExecuteList<RoleInfo>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000F158 File Offset: 0x0000D358
		protected string GetChildDepartment(int parentid, string tree)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			List<Department> list = DbHelper.ExecuteList<Department>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in list)
			{
				stringBuilder.Append("  <option value=\"" + department.id + "\"");
				stringBuilder.AppendFormat(">{0}</option>", tree + department.name);
				stringBuilder.AppendLine(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000CE RID: 206
		protected List<Department> deparlist = new List<Department>();

		// Token: 0x040000CF RID: 207
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x040000D0 RID: 208
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x040000D1 RID: 209
		protected int s_roleid = FPRequest.GetInt("s_roleid");

		// Token: 0x040000D2 RID: 210
		protected int s_departid = FPRequest.GetInt("s_departid");

		// Token: 0x040000D3 RID: 211
		protected int s_username = FPRequest.GetInt("s_username");

		// Token: 0x040000D4 RID: 212
		protected int s_realname = FPRequest.GetInt("s_realname");

		// Token: 0x040000D5 RID: 213
		protected int s_mobile = FPRequest.GetInt("s_mobile");

		// Token: 0x040000D6 RID: 214
		protected int s_email = FPRequest.GetInt("s_email");
	}
}
