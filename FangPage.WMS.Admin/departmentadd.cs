using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003B RID: 59
	public class departmentadd : SuperController
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000C21C File Offset: 0x0000A41C
		protected override void View()
		{
			if (this.id > 0)
			{
				this.department = DbHelper.ExecuteModel<Department>(this.id);
				this.parentid = this.department.parentid;
			}
			if (this.ispost)
			{
				this.department = FPRequest.GetModel<Department>(this.department);
				string text = "0";
				Department department = new Department();
				if (this.department.id > 0)
				{
					if (DbHelper.ExecuteUpdate<Department>(this.department) > 0)
					{
						if (this.department.parentid != this.parentid)
						{
							text = this.department.parentlist;
							if (this.department.parentid > 0)
							{
								department = DbHelper.ExecuteModel<Department>(this.department.parentid);
								this.department.parentlist = department.parentlist + "," + department.id;
							}
							else
							{
								this.department.parentlist = "0," + this.department.id.ToString();
							}
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist]='{1}' WHERE [id]={2};", DbConfigs.Prefix, this.department.parentlist, this.id);
							if (DbConfigs.DbType == DbType.Access)
							{
								stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist]=REPLACE([parentlist], '{1}', '{2}', 1, 1) WHERE [id] in (select [id] FROM [{0}WMS_Department] WHERE [parentlis] LIKE '{3},%');", new object[]
								{
									DbConfigs.Prefix,
									text,
									this.department.parentlist,
									text
								});
							}
							else
							{
								stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist]=STUFF([parentlist],1,{1},'{2}') WHERE [id] in (SELECT [id] FROM [{0}WMS_Department] WHERE [parentlis] LIKE '{3},%');", new object[]
								{
									DbConfigs.Prefix,
									text.Length,
									this.department.parentlist,
									text
								});
							}
							stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts]=[subcounts]-1 WHERE [id]={1};", DbConfigs.Prefix, this.parentid);
							stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts]=[subcounts]+1 WHERE [id]={1};", DbConfigs.Prefix, this.department.parentid);
							DbHelper.ExecuteSql(stringBuilder.ToString());
						}
					}
				}
				else
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
					this.department.display = FPUtils.StrToInt(DbHelper.ExecuteMax<Department>("display", new SqlParam[]
					{
						sqlParam
					}).ToString()) + 1;
					this.id = DbHelper.ExecuteInsert<Department>(this.department);
					if (this.id > 0)
					{
						if (this.department.parentid > 0)
						{
							department = DbHelper.ExecuteModel<Department>(this.department.parentid);
							text = department.parentlist + "," + this.id;
						}
						else
						{
							text = text + "," + this.id;
						}
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist] = '{1}' WHERE [id]={2};", DbConfigs.Prefix, text, this.id);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts] = [subcounts]+1 WHERE [id]={1};", DbConfigs.Prefix, this.department.parentid);
						DbHelper.ExecuteSql(stringBuilder.ToString());
					}
				}
				base.Response.Redirect("departmentmanage.aspx");
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.departmentlist = DbHelper.ExecuteList<Department>(orderby, sqlparams);
			base.SaveRightURL();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000C610 File Offset: 0x0000A810
		protected string GetChildDepartment(int parentid, string tree)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<Department> list = DbHelper.ExecuteList<Department>(orderby, sqlparams);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in list)
			{
				string arg = "";
				if (department.id == this.parentid)
				{
					arg = "selected=\"selected\"";
				}
				stringBuilder.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", department.id, arg, tree + department.name);
				stringBuilder.Append(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000090 RID: 144
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000091 RID: 145
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000092 RID: 146
		protected Department department = new Department();

		// Token: 0x04000093 RID: 147
		protected List<Department> departmentlist = new List<Department>();
	}
}
