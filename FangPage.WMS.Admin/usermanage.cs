using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000051 RID: 81
	public class usermanage : SuperController
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000ECDC File Offset: 0x0000CEDC
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<UserInfo>(FPRequest.GetString("chkdel"));
					DbHelper.ExecuteDelete<FullUserInfo>(FPRequest.GetString("chkdel"));
					base.Response.Redirect(this.webpath + this.cururl);
				}
				else if (this.action == "credit")
				{
					base.Response.Redirect("creditadd.aspx?uid=" + FPRequest.GetString("chkdel"));
				}
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("roleid", WhereType.NotEqual, 3));
			if (this.s_roleid > 0)
			{
				list.Add(DbHelper.MakeAndWhere("roleid", this.s_roleid));
			}
			if (this.s_departid > 0)
			{
				list.Add(DbHelper.MakeAndWhere("departid", this.s_departid));
			}
			string text = "";
			if (this.keyword != "")
			{
				if (this.s_username == 1)
				{
					text = "[username] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_realname == 1)
				{
					if (text != "")
					{
						text += " OR ";
					}
					text = text + "[realname] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_mobile == 1)
				{
					if (text != "")
					{
						text += " OR ";
					}
					text = text + "[mobile] LIKE '%" + this.keyword + "%'";
				}
				if (this.s_email == 1)
				{
					if (text != "")
					{
						text += " OR ";
					}
					text = text + "[email] LIKE '%" + this.keyword + "%'";
				}
				if (text == "")
				{
					text = "[username] LIKE '%" + this.keyword + "%'";
				}
				list.Add(DbHelper.MakeAndWhere("(" + text + ")", WhereType.Custom, ""));
			}
			this.userlist = DbHelper.ExecuteList<UserInfo>(this.pager, list.ToArray());
			base.SaveRightURL();
		}

		// Token: 0x040000C3 RID: 195
		protected List<UserInfo> userlist = new List<UserInfo>();

		// Token: 0x040000C4 RID: 196
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x040000C5 RID: 197
		protected int s_roleid = FPRequest.GetInt("s_roleid");

		// Token: 0x040000C6 RID: 198
		protected int s_departid = FPRequest.GetInt("s_departid");

		// Token: 0x040000C7 RID: 199
		protected int s_username = FPRequest.GetInt("s_username");

		// Token: 0x040000C8 RID: 200
		protected int s_realname = FPRequest.GetInt("s_realname");

		// Token: 0x040000C9 RID: 201
		protected int s_mobile = FPRequest.GetInt("s_mobile");

		// Token: 0x040000CA RID: 202
		protected int s_email = FPRequest.GetInt("s_email");

		// Token: 0x040000CB RID: 203
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
