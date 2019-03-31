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
	// Token: 0x0200002E RID: 46
	public class userbatch : SuperController
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00007FA0 File Offset: 0x000061A0
		protected override void Controller()
		{
			if (this.backurl == "")
			{
				this.backurl = "usermanage.aspx";
			}
			if (this.ispost)
			{
				List<SqlParam> list = new List<SqlParam>();
				if (FPRequest.GetInt("roleid") > 0)
				{
					RoleInfo roleInfo = RoleBll.GetRoleInfo(FPRequest.GetInt("roleid"));
					list.Add(DbHelper.MakeUpdate("roleid", roleInfo.id));
				}
				if (FPRequest.GetInt("departid") > 0)
				{
					Department departInfo = DepartmentBll.GetDepartInfo(FPRequest.GetInt("departid"));
					list.Add(DbHelper.MakeUpdate("departid", departInfo.id));
					list.Add(DbHelper.MakeUpdate("departname", departInfo.name));
				}
				if (FPRequest.GetInt("gradeid") > 0)
				{
					list.Add(DbHelper.MakeUpdate("gradeid", FPRequest.GetInt("gradeid")));
				}
				string text = FPArray.Remove(FPRequest.GetString("types"), "");
				if (text != "")
				{
					list.Add(DbHelper.MakeUpdate("types", text));
				}
				if (FPRequest.GetString("password") != "")
				{
					list.Add(DbHelper.MakeUpdate("password", FPUtils.MD5(FPRequest.GetString("password"))));
				}
				if (FPRequest.GetString("password2") != "")
				{
					list.Add(DbHelper.MakeUpdate("password2", FPUtils.MD5(FPRequest.GetString("password2"))));
				}
				if (FPRequest.GetInt("isreal", -1) != -1)
				{
					list.Add(DbHelper.MakeUpdate("isreal", FPRequest.GetInt("isreal")));
				}
				if (FPRequest.GetInt("ismobile", -1) != -1)
				{
					list.Add(DbHelper.MakeUpdate("ismobile", FPRequest.GetInt("ismobile")));
				}
				if (FPRequest.GetInt("isemail", -1) != -1)
				{
					list.Add(DbHelper.MakeUpdate("isemail", FPRequest.GetInt("isemail")));
				}
				if (FPRequest.GetInt("sex", -2) != -2)
				{
					list.Add(DbHelper.MakeUpdate("sex", FPRequest.GetInt("sex")));
				}
				if (FPRequest.GetInt("exp") > 0)
				{
					list.Add(DbHelper.MakeUpdate("exp", FPRequest.GetInt("exp")));
				}
				if (list.Count > 0)
				{
					list.Add(DbHelper.MakeAndWhere("id", WhereType.NotEqual, 1));
					if (this.idlist != "")
					{
						list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.idlist));
					}
					else
					{
						if (this.s_roleid > 0)
						{
							list.Add(DbHelper.MakeAndWhere("roleid", this.s_roleid));
						}
						if (this.s_departid > 0)
						{
							string departIdList = DepartmentBll.GetDepartIdList(this.s_departid);
							list.Add(DbHelper.MakeAndWhere("departid", WhereType.In, departIdList));
						}
						if (this.s_gradeid > 0)
						{
							list.Add(DbHelper.MakeAndWhere("gradeid", this.s_gradeid));
						}
						if (this.s_types != "")
						{
							foreach (int num in FPArray.SplitInt(this.s_types))
							{
								list.Add(DbHelper.MakeAndWhere("types", WhereType.Contain, num));
							}
						}
						if (this.s_sso == 1)
						{
							list.Add(DbHelper.MakeAndWhere("issso", 1));
						}
						else if (this.s_sso == 2)
						{
							list.Add(DbHelper.MakeAndWhere("issso", 0));
						}
						if (this.s_state == 1)
						{
							list.Add(DbHelper.MakeAndWhere("state", 1));
						}
						else if (this.s_state == 2)
						{
							list.Add(DbHelper.MakeAndWhere("state", 0));
						}
						string text2 = "";
						if (this.keyword != "")
						{
							if (this.s_username == 1)
							{
								text2 = "[username] LIKE '%" + this.keyword + "%'";
							}
							if (this.s_realname == 1)
							{
								if (text2 != "")
								{
									text2 += " OR ";
								}
								text2 = text2 + "[realname] LIKE '%" + this.keyword + "%'";
							}
							if (this.s_mobile == 1)
							{
								if (text2 != "")
								{
									text2 += " OR ";
								}
								text2 = text2 + "[mobile] LIKE '%" + this.keyword + "%'";
							}
							if (this.s_email == 1)
							{
								if (text2 != "")
								{
									text2 += " OR ";
								}
								text2 = text2 + "[email] LIKE '%" + this.keyword + "%'";
							}
							if (this.s_idcard == 1)
							{
								if (text2 != "")
								{
									text2 += " OR ";
								}
								text2 = text2 + "[idcard] LIKE '%" + this.keyword + "%'";
							}
							if (text2 == "")
							{
								text2 = string.Concat(new string[]
								{
									"[username] LIKE '%",
									this.keyword,
									"%' OR [realname] LIKE '%",
									this.keyword,
									"%'"
								});
							}
							list.Add(DbHelper.MakeAndWhere("(" + text2 + ")", WhereType.Custom, ""));
						}
					}
					DbHelper.ExecuteUpdate<UserInfo>(list.ToArray());
					FPCache.Remove("FP_USERLIST");
				}
				base.Response.Redirect(this.backurl);
			}
			this.departlist = DepartmentBll.GetDepartList(0);
			this.rolelist = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.id != 2);
			this.gradelist = GradeBll.GetGradeList();
			this.typelist = TypeBll.GetTypeListByMarkup("usertype");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000085BC File Offset: 0x000067BC
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

		// Token: 0x04000066 RID: 102
		protected string idlist = FPRequest.GetString("idlist");

		// Token: 0x04000067 RID: 103
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x04000068 RID: 104
		protected int s_roleid = FPRequest.GetInt("s_roleid");

		// Token: 0x04000069 RID: 105
		protected int s_departid = FPRequest.GetInt("s_departid");

		// Token: 0x0400006A RID: 106
		protected int s_gradeid = FPRequest.GetInt("s_gradeid");

		// Token: 0x0400006B RID: 107
		protected string s_types = FPArray.Remove(FPRequest.GetString("s_types"), "");

		// Token: 0x0400006C RID: 108
		protected int s_username = FPRequest.GetInt("s_username", 1);

		// Token: 0x0400006D RID: 109
		protected int s_realname = FPRequest.GetInt("s_realname", 1);

		// Token: 0x0400006E RID: 110
		protected int s_mobile = FPRequest.GetInt("s_mobile");

		// Token: 0x0400006F RID: 111
		protected int s_email = FPRequest.GetInt("s_email");

		// Token: 0x04000070 RID: 112
		protected int s_idcard = FPRequest.GetInt("s_idcard");

		// Token: 0x04000071 RID: 113
		protected int s_sso = FPRequest.GetInt("s_sso");

		// Token: 0x04000072 RID: 114
		protected int s_state = FPRequest.GetInt("s_state");

		// Token: 0x04000073 RID: 115
		protected List<Department> departlist = new List<Department>();

		// Token: 0x04000074 RID: 116
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x04000075 RID: 117
		protected List<GradeInfo> gradelist = new List<GradeInfo>();

		// Token: 0x04000076 RID: 118
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
