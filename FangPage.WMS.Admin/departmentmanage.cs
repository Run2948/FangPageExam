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
	// Token: 0x02000048 RID: 72
	public class departmentmanage : SuperController
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000E094 File Offset: 0x0000C294
		protected override void Controller()
		{
			if (this.action == "refresh")
			{
				FPCache.Remove("FP_DEPARTLIST");
				FPCache.Remove("FP_DEPARTMANAGER");
			}
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					int @int = FPRequest.GetInt("id");
					Department department = DbHelper.ExecuteModel<Department>(@int);
					if (DbHelper.ExecuteDelete<Department>(@int) > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("DELETE FROM [{0}WMS_Department] WHERE [id] IN (SELECT [id] FROM [{0}WMS_Department] WHERE [parentlist] LIKE '{1},%')|", DbConfigs.Prefix, department.parentlist);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts] = [subcounts]-1 WHERE [id]={1}", DbConfigs.Prefix, department.parentid);
						DbHelper.ExecuteSql(stringBuilder.ToString());
					}
					FPCache.Remove("FP_DEPARTLIST");
					FPCache.Remove("FP_DEPARTMANAGER");
					base.Response.Redirect(string.Concat(new object[]
					{
						"departmentmanage.aspx?departname=",
						this.departname,
						"&layer=",
						this.layer
					}));
				}
				else if (this.action == "reset")
				{
					List<Department> list = DbHelper.ExecuteList<Department>();
					string text = "";
					foreach (Department department2 in list)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", department2.id);
						int num = DbHelper.ExecuteCount<Department>(new SqlParam[]
						{
							sqlParam
						});
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("departid", department2.id);
						int num2 = DbHelper.ExecuteCount<UserInfo>(new SqlParam[]
						{
							sqlParam2
						});
						string userDepartment = DepartmentBll.GetUserDepartment(department2.id);
						string text2 = department2.longname;
						if (text2 == "")
						{
							text2 = department2.name;
						}
						if (text != "")
						{
							text += "|";
						}
						text += string.Format("UPDATE [{0}WMS_Department] SET [subcounts]={1},[usercount]={2},[departlist]='{3}' WHERE [id]={4}|", new object[]
						{
							DbConfigs.Prefix,
							num,
							num2,
							userDepartment,
							department2.id
						});
						text += string.Format("UPDATE [{0}WMS_UserInfo] SET [departname]='{1}',[display]=[display]+{2} WHERE [departid]={3}", new object[]
						{
							DbConfigs.Prefix,
							text2,
							department2.display,
							department2.id
						});
					}
					DbHelper.ExecuteSql(text);
					FPCache.Remove("FP_DEPARTLIST");
					FPCache.Remove("FP_DEPARTMANAGER");
					this.msg = "用户部门重置成功！";
				}
				else if (this.action == "batch")
				{
					string @string = FPRequest.GetString("chkid");
					base.Response.Redirect("deptbatch.aspx?idlist=" + @string);
				}
			}
			if (this.departname != "")
			{
				SqlParam sqlParam3 = DbHelper.MakeAndWhere("name", WhereType.Like, this.departname);
				List<Department> list2 = DbHelper.ExecuteList<Department>(new SqlParam[]
				{
					sqlParam3
				});
				string text3 = "";
				foreach (Department department3 in list2)
				{
					this.departids = FPArray.Push(this.departids, department3.parentlist);
					text3 = FPArray.Push(text3, department3.id);
				}
				SqlParam sqlParam4 = DbHelper.MakeAndWhere("parentlist", WhereType.Contain, text3);
				this.departids = FPArray.Push(this.departids, DbHelper.ExecuteField<Department>(new SqlParam[]
				{
					sqlParam4
				}));
				if (this.departids != "")
				{
					this.departmentlist = DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == 0 && FPArray.Contain(this.departids, item.id));
				}
			}
			else
			{
				this.departmentlist = DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == 0);
			}
			this.typelist = TypeBll.GetTypeListByMarkup("depart_type");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		protected string ShowChildDepartment(int parentid, string ids, int layer, string tree)
		{
			List<Department> list = new List<Department>();
			if (ids != "")
			{
				list = DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == parentid && FPArray.Contain(ids, item.id));
			}
			else
			{
				list = DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == parentid);
			}
			if (layer > 0)
			{
				list = list.FindAll((Department item) => FPArray.SplitInt(item.parentlist).Length <= layer + 1);
			}
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in list)
			{
				stringBuilder.Append("<tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">");
				stringBuilder.AppendLine("<td align=\"center\"><input id=\"chkid\" name=\"chkid\" value=\"" + department.id + "\" type=\"checkbox\"/></td>");
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td align=\"left\"><a href=\"usermanage.aspx?s_departid=",
					department.id,
					"&backurl=",
					FPUtils.UrlEncode(this.pageurl),
					"\">",
					tree
				}));
				if (department.subcounts > 0)
				{
					stringBuilder.AppendLine("<img src=\"" + this.webpath + this.sitepath + "/statics/images/usergroups.gif\" width=\"16\" height=\"16\"  />");
				}
				else
				{
					stringBuilder.AppendLine("<img src=\"" + this.webpath + this.sitepath + "/statics/images/users.gif\" width=\"16\" height=\"16\"  />");
				}
				stringBuilder.AppendLine(FPController.echo(department.name, this.departname, "<span style=\"background-color:#ffd800;\">" + this.departname + "</span>") + "</a></td>");
				stringBuilder.AppendLine("<td>" + department.keyid + "</td>");
				if (this.typelist.Count > 0)
				{
					stringBuilder.AppendLine("<td>" + department.depttype + "</td>");
				}
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"departmentadd.aspx?parentid=",
					department.id,
					"&departname=",
					this.departname,
					"\">添加子部门</a></td>"
				}));
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"departmentadd.aspx?id=",
					department.id,
					"&departname=",
					this.departname,
					"\">编辑部门</a></td>"
				}));
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"departmentdisplay.aspx?parentid=",
					department.parentid,
					"&departname=",
					this.departname,
					"\">部门排序</a></td>"
				}));
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"userdisplay.aspx?departid=",
					department.id,
					"&departname=",
					this.departname,
					"\">用户排序</a></td>"
				}));
				stringBuilder.AppendLine("<td><a style=\"color: #1317fc\"  onclick=\"DeleteDepertment(" + department.id + ")\" href=\"#\">删除部门</a></td></tr>");
				stringBuilder.Append(this.ShowChildDepartment(department.id, ids, layer, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000CB RID: 203
		protected List<Department> departmentlist = new List<Department>();

		// Token: 0x040000CC RID: 204
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x040000CD RID: 205
		protected string departname = FPRequest.GetString("departname");

		// Token: 0x040000CE RID: 206
		protected int layer = FPRequest.GetInt("layer");

		// Token: 0x040000CF RID: 207
		protected string departids = "";
	}
}
