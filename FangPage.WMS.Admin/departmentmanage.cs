using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003D RID: 61
	public class departmentmanage : SuperController
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					int @int = FPRequest.GetInt("id");
					Department department = DbHelper.ExecuteModel<Department>(@int);
					if (DbHelper.ExecuteDelete<Department>(@int) > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("DELETE FROM [{0}WMS_Department] WHERE [id] IN (SELECT [id] FROM [{0}WMS_Department] WHERE [parentlist] LIKE '{1},%')", DbConfigs.Prefix, department.parentlist);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts] = [subcounts]-1 WHERE [id]={1}", DbConfigs.Prefix, department.parentid);
						DbHelper.ExecuteSql(stringBuilder.ToString());
					}
				}
				base.Response.Redirect("departmentmanage.aspx");
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.departmentlist = DbHelper.ExecuteList<Department>(orderby, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		protected string ShowChildDepartment(int parentid, string tree)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<Department> list = DbHelper.ExecuteList<Department>(orderby, new SqlParam[]
			{
				sqlParam
			});
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in list)
			{
				stringBuilder.Append("<tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">");
				stringBuilder.AppendLine("<td align=\"center\">" + department.id + "</td>");
				stringBuilder.AppendLine("<td align=\"left\">" + tree);
				if (department.subcounts > 0)
				{
					stringBuilder.AppendLine("<img src=\"../images/usergroups.gif\" width=\"16\" height=\"16\"  />");
				}
				else
				{
					stringBuilder.AppendLine("<img src=\"../images/users.gif\" width=\"16\" height=\"16\"  />");
				}
				stringBuilder.AppendLine(department.name + "</td>");
				stringBuilder.AppendLine("<td>" + department.description + "</td>");
				stringBuilder.AppendLine("<td><a style=\"color: #1317fc\"  href=\"departmentadd.aspx?parentid=" + department.id + "\">添加子部门</a></td>");
				stringBuilder.AppendLine("<td><a style=\"color: #1317fc\"  href=\"departmentadd.aspx?id=" + department.id + "\">编辑</a></td>");
				stringBuilder.AppendLine("<td><a style=\"color: #1317fc\"  onclick=\"DeleteDepertment(" + department.id + ")\" href=\"#\">删除</a></td>");
				stringBuilder.AppendLine("<td><a style=\"color: #1317fc\"  href=\"departmentdisplay.aspx?parentid=" + department.parentid + "\">排序</a></td></tr>");
				stringBuilder.Append(this.ShowChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000096 RID: 150
		protected List<Department> departmentlist = new List<Department>();
	}
}
