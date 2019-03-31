using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002A RID: 42
	public class usercreate : SuperController
	{
		// Token: 0x06000063 RID: 99 RVA: 0x0000798C File Offset: 0x00005B8C
		protected override void Controller()
		{
			if (this.ispost)
			{
				int @int = FPRequest.GetInt("startacount");
				int int2 = FPRequest.GetInt("endacount");
				if (int2 < @int)
				{
					this.ShowErr("终止帐号不能小于起始帐号。");
					return;
				}
				string text = FPRequest.GetString("password");
				if (text == "")
				{
					text = "123456";
				}
				int int3 = FPRequest.GetInt("departid");
				Department departInfo = DepartmentBll.GetDepartInfo(int3);
				int num = FPRequest.GetInt("roleid");
				if (num == 0)
				{
					num = 5;
				}
				string @string = FPRequest.GetString("types");
				int int4 = FPRequest.GetInt("gradeid");
				for (int i = @int; i <= int2; i++)
				{
					UserInfo userInfo = UserBll.GetUserInfo(i.ToString());
					if (userInfo.id > 0)
					{
						userInfo.password = FPUtils.MD5(text);
						userInfo.roleid = num;
						userInfo.departid = int3;
						userInfo.departname = departInfo.longname;
						userInfo.types = @string;
						userInfo.gradeid = int4;
						DbHelper.ExecuteUpdate<UserInfo>(userInfo);
					}
					else
					{
						userInfo.username = i.ToString();
						userInfo.password = FPUtils.MD5(text);
						userInfo.roleid = num;
						userInfo.departid = int3;
						userInfo.departname = departInfo.longname;
						userInfo.types = @string;
						userInfo.gradeid = int4;
						DbHelper.ExecuteInsert<UserInfo>(userInfo);
					}
				}
				FPResponse.Redirect("usermanage.aspx");
			}
			this.departlist = DepartmentBll.GetDepartList(0);
			this.rolelist = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.id != 2);
			this.gradelist = DbHelper.ExecuteList<GradeInfo>(OrderBy.ASC);
			this.typelist = TypeBll.GetTypeListByMarkup("usertype");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00007B54 File Offset: 0x00005D54
		protected string GetChildDepartment(int parentid, string tree)
		{
			List<Department> departList = DepartmentBll.GetDepartList(parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in departList)
			{
				stringBuilder.Append("  <option value=\"" + department.id + "\"");
				stringBuilder.AppendFormat(">{0}</option>", tree + department.name);
				stringBuilder.AppendLine(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000059 RID: 89
		protected List<Department> departlist = new List<Department>();

		// Token: 0x0400005A RID: 90
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x0400005B RID: 91
		protected List<GradeInfo> gradelist = new List<GradeInfo>();

		// Token: 0x0400005C RID: 92
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
