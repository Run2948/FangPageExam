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
	// Token: 0x02000053 RID: 83
	public class useradd : SuperController
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000F790 File Offset: 0x0000D990
		protected override void Controller()
		{
			if (this.backurl == "")
			{
				this.backurl = "usermanage.aspx?s_departid=" + this.s_departid;
			}
			if (this.id > 0)
			{
				this.fulluserinfo = DbHelper.ExecuteModel<UserInfo>(this.id);
				if (this.fulluserinfo.id == 0)
				{
					this.ShowErr("对不起，该用户不存在或已被删除。");
					return;
				}
			}
			else
			{
				this.fulluserinfo.departid = this.s_departid;
			}
			this.extendlist = FPXml.LoadList<UserExtend>(FPFile.GetMapPath(this.webpath + "config/user_extend.config"));
			foreach (UserExtend userExtend in this.extendlist)
			{
				if (!this.fulluserinfo.extend.ContainsKey(userExtend.markup) && userExtend.defvalue != "")
				{
					this.fulluserinfo.extend[userExtend.markup] = userExtend.defvalue;
				}
			}
			if (this.ispost)
			{
				this.fulluserinfo.isreal = 0;
				this.fulluserinfo.isemail = 0;
				this.fulluserinfo.ismobile = 0;
				string username = this.fulluserinfo.username;
				int departid = this.fulluserinfo.departid;
				this.fulluserinfo = FPRequest.GetModel<UserInfo>(this.fulluserinfo);
				if (this.fulluserinfo.id == 1)
				{
					this.fulluserinfo.roleid = 1;
				}
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
				string string2 = FPRequest.GetString("admin_password");
				if (this.fulluserinfo.id > 0)
				{
					if (this.fulluserinfo.username != username && UserBll.CheckUserName(this.fulluserinfo.username) > 0)
					{
						this.ShowErr("该用户名已经存在，请使用别的用户名。");
						return;
					}
					if (@string.Trim() != "")
					{
						this.fulluserinfo.password = FPUtils.MD5(@string);
					}
					if (string2.Trim() != "")
					{
						this.fulluserinfo.password2 = FPUtils.MD5(string2);
					}
					Department departInfo = DepartmentBll.GetDepartInfo(this.fulluserinfo.departid);
					if (departInfo.id > 0 && departid > 0)
					{
						if (departInfo.longname != "")
						{
							this.fulluserinfo.departname = departInfo.longname;
						}
						else
						{
							this.fulluserinfo.departname = departInfo.name;
						}
					}
					DbHelper.ExecuteUpdate<UserInfo>(this.fulluserinfo);
				}
				else
				{
					if (@string.Trim() == "")
					{
						this.ShowErr("登录密码不能为空！");
						return;
					}
					if (UserBll.CheckUserName(this.fulluserinfo.username) > 0)
					{
						this.ShowErr("该用户名已经存在，请使用别的用户名。");
						return;
					}
					this.fulluserinfo.password = FPUtils.MD5(@string);
					if (string2 == "")
					{
						this.fulluserinfo.password2 = FPUtils.MD5(@string);
					}
					this.fulluserinfo.regip = FPRequest.GetIP();
					this.fulluserinfo.lastip = FPRequest.GetIP();
					Department departInfo2 = DepartmentBll.GetDepartInfo(this.fulluserinfo.departid);
					this.fulluserinfo.departname = departInfo2.longname;
					DbHelper.ExecuteInsert<UserInfo>(this.fulluserinfo);
				}
				if (this.isfile)
				{
					UserBll.UploadAvatar(FPRequest.Files["user_avatar"], 150, 150, this.fulluserinfo.id);
					if (this.msg != "")
					{
						this.ShowErr(this.msg);
						return;
					}
				}
				if (this.s_departid > 0)
				{
					this.s_departid = this.fulluserinfo.departid;
					this.backurl = "usermanage.aspx?s_departid=" + this.s_departid;
				}
				FPCache.Remove("FP_USERLIST");
				FPCache.Remove("FP_MAXUID");
				base.Response.Redirect(this.backurl);
			}
			this.departlist = DepartmentBll.GetDepartList(0);
			this.rolelist = RoleBll.GetRoleList().FindAll((RoleInfo item) => item.id != 2);
			this.gradelist = GradeBll.GetGradeList();
			this.typelist = TypeBll.GetTypeListByMarkup("user_type");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000FC34 File Offset: 0x0000DE34
		protected string GetChildDepartment(int parentid, string tree)
		{
			List<Department> departList = DepartmentBll.GetDepartList(parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in departList)
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

		// Token: 0x040000E6 RID: 230
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000E7 RID: 231
		protected int s_departid = FPRequest.GetInt("s_departid");

		// Token: 0x040000E8 RID: 232
		protected UserInfo fulluserinfo = new UserInfo();

		// Token: 0x040000E9 RID: 233
		protected List<Department> departlist = new List<Department>();

		// Token: 0x040000EA RID: 234
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x040000EB RID: 235
		protected List<GradeInfo> gradelist = new List<GradeInfo>();

		// Token: 0x040000EC RID: 236
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x040000ED RID: 237
		protected List<UserExtend> extendlist = new List<UserExtend>();

		// Token: 0x040000EE RID: 238
		protected FPData typedata_list = new FPData();
	}
}
