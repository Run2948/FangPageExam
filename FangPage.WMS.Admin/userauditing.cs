using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000054 RID: 84
	public class userauditing : SuperController
	{
		// Token: 0x060000CB RID: 203 RVA: 0x0000FD88 File Offset: 0x0000DF88
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<UserInfo>(FPRequest.GetString("chkdel"));
				}
				else if (this.action == "auditing")
				{
					DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
					{
						DbHelper.MakeUpdate("roleid", 5),
						DbHelper.MakeAndWhere("id", WhereType.In, FPRequest.GetString("chkdel"))
					});
				}
				base.Response.Redirect("userauditing.aspx");
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("roleid", 3);
			this.userlist = DbHelper.ExecuteList<UserInfo>(this.pager, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x040000EF RID: 239
		protected List<UserInfo> userlist = new List<UserInfo>();

		// Token: 0x040000F0 RID: 240
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
