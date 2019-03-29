using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004B RID: 75
	public class useradd : SuperController
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000DF80 File Offset: 0x0000C180
		protected override void View()
		{
			if (this.id > 0)
			{
				this.fulluserinfo = DbHelper.ExecuteModel<FullUserInfo>(this.id);
			}
			this.bday = FPUtils.SplitString(this.fulluserinfo.bday, ",", 3);
			if (this.ispost)
			{
				this.fulluserinfo.isreal = 0;
				this.fulluserinfo.isemail = 0;
				this.fulluserinfo.ismobile = 0;
				string username = this.fulluserinfo.username;
				this.fulluserinfo = FPRequest.GetModel<FullUserInfo>(this.fulluserinfo);
				if (this.fulluserinfo.roleid == 0)
				{
					this.ShowErr("请选择用户角色。");
					return;
				}
				if (this.fulluserinfo.username == "")
				{
					this.ShowErr("请输入用户名。");
					return;
				}
				string @string = FPRequest.GetString("password1");
				if (this.fulluserinfo.isidcard != 0)
				{
					this.fulluserinfo.isidcard = ((this.fulluserinfo.isreal == 1) ? 1 : -1);
				}
				if (FPRequest.GetInt("isgrade") == 1)
				{
					UserGrade userGradeByExpHigher = UserBll.GetUserGradeByExpHigher(this.fulluserinfo.exp);
					this.fulluserinfo.gradeid = userGradeByExpHigher.id;
				}
				if (this.fulluserinfo.id > 0)
				{
					if (this.fulluserinfo.username != username)
					{
						if (UserBll.CheckUserName(this.fulluserinfo.username))
						{
							this.ShowErr("该用户名已经存在，请使用别的用户名。");
							return;
						}
					}
					if (@string.Trim() != "")
					{
						this.fulluserinfo.password = FPUtils.MD5(@string);
					}
					DbHelper.ExecuteUpdate<FullUserInfo>(this.fulluserinfo);
				}
				else
				{
					if (@string.Trim() == "")
					{
						this.ShowErr("登录密码不能为空！");
						return;
					}
					if (UserBll.CheckUserName(this.fulluserinfo.username))
					{
						this.ShowErr("该用户名已经存在，请使用别的用户名。");
						return;
					}
					this.fulluserinfo.password = FPUtils.MD5(@string);
					this.fulluserinfo.regip = FPRequest.GetIP();
					this.fulluserinfo.lastip = FPRequest.GetIP();
					this.fulluserinfo.id = DbHelper.ExecuteInsert<FullUserInfo>(this.fulluserinfo);
				}
				base.Response.Redirect("usermanage.aspx");
			}
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
			this.usergradelist = DbHelper.ExecuteList<UserGrade>(OrderBy.ASC);
			this.typelist = TypeBll.GetTypeListByMarkup("usertype");
			base.SaveRightURL();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000E2CC File Offset: 0x0000C4CC
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
				if (department.id == this.fulluserinfo.departid)
				{
					stringBuilder.Append("   selected=\"selected\"   ");
				}
				stringBuilder.AppendFormat(">{0}</option>", tree + department.name);
				stringBuilder.AppendLine(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000B4 RID: 180
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000B5 RID: 181
		protected FullUserInfo fulluserinfo = new FullUserInfo();

		// Token: 0x040000B6 RID: 182
		protected List<Department> deparlist = new List<Department>();

		// Token: 0x040000B7 RID: 183
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x040000B8 RID: 184
		protected List<UserGrade> usergradelist = new List<UserGrade>();

		// Token: 0x040000B9 RID: 185
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x040000BA RID: 186
		protected string[] bday;
	}
}
