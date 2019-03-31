using System;
using System.Collections.Generic;
using System.Text;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200005B RID: 91
	public class usersearch : SuperController
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000116FC File Offset: 0x0000F8FC
		protected override void Controller()
		{
			this.deparlist = DepartmentBll.GetDepartList(0);
			this.rolelist = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.id != 2);
			this.gradelist = GradeBll.GetGradeList();
			this.typelist = TypeBll.GetTypeListByMarkup("user_type");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00011760 File Offset: 0x0000F960
		protected string GetChildDepartment(int parentid, string tree)
		{
			List<Department> departList = DepartmentBll.GetDepartList(parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in departList)
			{
				stringBuilder.Append("  <option value=\"" + department.id + "\"");
				if (department.id == this.s_departid)
				{
					stringBuilder.Append(" selected=\"selected\"");
				}
				stringBuilder.AppendFormat(">{0}</option>", tree + department.name);
				stringBuilder.AppendLine(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400010A RID: 266
		protected List<Department> deparlist = new List<Department>();

		// Token: 0x0400010B RID: 267
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x0400010C RID: 268
		protected List<GradeInfo> gradelist = new List<GradeInfo>();

		// Token: 0x0400010D RID: 269
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x0400010E RID: 270
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x0400010F RID: 271
		protected int s_roleid = FPRequest.GetInt("s_roleid");

		// Token: 0x04000110 RID: 272
		protected int s_departid = FPRequest.GetInt("s_departid");

		// Token: 0x04000111 RID: 273
		protected int s_depts = FPRequest.GetInt("s_depts");

		// Token: 0x04000112 RID: 274
		protected int s_gradeid = FPRequest.GetInt("s_gradeid");

		// Token: 0x04000113 RID: 275
		protected string s_types = FPRequest.GetString("s_types");

		// Token: 0x04000114 RID: 276
		protected int s_username = FPRequest.GetInt("s_username", 1);

		// Token: 0x04000115 RID: 277
		protected int s_realname = FPRequest.GetInt("s_realname", 1);

		// Token: 0x04000116 RID: 278
		protected int s_mobile = FPRequest.GetInt("s_mobile");

		// Token: 0x04000117 RID: 279
		protected int s_email = FPRequest.GetInt("s_email");

		// Token: 0x04000118 RID: 280
		protected int s_idcard = FPRequest.GetInt("s_idcard");

		// Token: 0x04000119 RID: 281
		protected int s_sso = FPRequest.GetInt("s_sso");

		// Token: 0x0400011A RID: 282
		protected int s_state = FPRequest.GetInt("s_state");
	}
}
