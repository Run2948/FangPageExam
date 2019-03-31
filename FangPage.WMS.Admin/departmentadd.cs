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
	// Token: 0x02000046 RID: 70
	public class departmentadd : SuperController
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000D808 File Offset: 0x0000BA08
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.department = DbHelper.ExecuteModel<Department>(this.id);
				this.parentid = this.department.parentid;
				if (this.department.id == 0)
				{
					this.ShowErr("对不起，该部门已被删除或不存。");
					return;
				}
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
						department = DbHelper.ExecuteModel<Department>(this.department.parentid);
						if (this.department.parentid != this.parentid)
						{
							text = this.department.parentlist;
							if (this.department.parentid > 0)
							{
								this.department.parentlist = department.parentlist + "," + this.department.id;
								this.department.departlist = department.departlist + ">" + this.department.name;
							}
							else
							{
								this.department.parentlist = "0," + this.department.id.ToString();
								this.department.departlist = this.department.name;
							}
							SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.department.parentid);
							this.department.display = FPUtils.StrToInt(DbHelper.ExecuteMax<Department>("display", new SqlParam[]
							{
								sqlParam
							}).ToString()) + 1;
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist]='{1}',[departlist]='{2}',[display]={3} WHERE [id]={4};", new object[]
							{
								DbConfigs.Prefix,
								this.department.parentlist,
								this.department.departlist,
								this.department.display,
								this.id
							});
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
								stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist]=STUFF([parentlist],1,{1},'{2}') WHERE [id] in (SELECT [id] FROM [{0}WMS_Department] WHERE [parentlist] LIKE '{3},%');", new object[]
								{
									DbConfigs.Prefix,
									text.Length,
									this.department.parentlist,
									text
								});
							}
							stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts]=[subcounts]-1 WHERE [id]={1};", DbConfigs.Prefix, this.parentid);
							stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts]=[subcounts]+1 WHERE [id]={1};", DbConfigs.Prefix, this.department.parentid);
							string text2 = DbHelper.ExecuteSql(stringBuilder.ToString());
							if (text2 != "")
							{
								this.ShowErr(text2);
							}
						}
						else
						{
							if (this.department.parentid > 0)
							{
								this.department.departlist = department.departlist + ">" + this.department.name;
							}
							else
							{
								this.department.departlist = this.department.name;
							}
							DbHelper.ExecuteUpdate<Department>(new SqlParam[]
							{
								DbHelper.MakeUpdate("departlist", this.department.departlist),
								DbHelper.MakeAndWhere("id", this.department.id)
							});
						}
					}
				}
				else
				{
					SqlParam sqlParam2 = DbHelper.MakeAndWhere("parentid", this.parentid);
					this.department.display = FPUtils.StrToInt(DbHelper.ExecuteMax<Department>("display", new SqlParam[]
					{
						sqlParam2
					}).ToString()) + 1;
					this.id = DbHelper.ExecuteInsert<Department>(this.department);
					if (this.id > 0)
					{
						if (this.department.parentid > 0)
						{
							department = DbHelper.ExecuteModel<Department>(this.department.parentid);
							text = department.parentlist + "," + this.id;
							this.department.departlist = department.departlist + ">" + this.department.name;
						}
						else
						{
							text = text + "," + this.id;
							this.department.departlist = this.department.name;
						}
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist] = '{1}',[departlist]='{2}' WHERE [id]={3};", new object[]
						{
							DbConfigs.Prefix,
							text,
							this.department.departlist,
							this.id
						});
						stringBuilder2.AppendFormat("UPDATE [{0}WMS_Department] SET [subcounts] = [subcounts]+1 WHERE [id]={1};", DbConfigs.Prefix, this.department.parentid);
						DbHelper.ExecuteSql(stringBuilder2.ToString());
					}
				}
				DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("departname", this.department.longname),
					DbHelper.MakeAndWhere("departid", this.id)
				});
				FPCache.Remove("FP_DEPARTLIST");
				FPCache.Remove("FP_DEPARTMANAGER");
				if (this.parentid > 0)
				{
					base.Response.Redirect("departmentmanage.aspx?departname=" + this.departname);
				}
				else
				{
					base.Response.Redirect("departmentmanage.aspx");
				}
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.departmentlist = DbHelper.ExecuteList<Department>(sqlparams);
			this.typelist = TypeBll.GetTypeListByMarkup("depart_type");
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		protected string GetChildDepartment(int parentid, string tree)
		{
			List<Department> list = DbHelper.ExecuteList<Department>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
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

		// Token: 0x040000C2 RID: 194
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000C3 RID: 195
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x040000C4 RID: 196
		protected Department department = new Department();

		// Token: 0x040000C5 RID: 197
		protected List<Department> departmentlist = new List<Department>();

		// Token: 0x040000C6 RID: 198
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x040000C7 RID: 199
		protected string departname = FPRequest.GetString("departname");
	}
}
