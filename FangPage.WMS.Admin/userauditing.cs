using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004C RID: 76
	public class userauditing : SuperController
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000E440 File Offset: 0x0000C640
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<UserInfo>(FPRequest.GetString("chkdel"));
				}
				else if (this.action == "auditing")
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeSet("roleid", 5),
						DbHelper.MakeAndWhere("id", WhereType.In, FPRequest.GetString("chkdel"))
					};
					DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
				}
				base.Response.Redirect("userauditing.aspx");
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("roleid", 3);
			this.userlist = DbHelper.ExecuteList<UserInfo>(this.pager, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x040000BB RID: 187
		protected List<UserInfo> userlist = new List<UserInfo>();

		// Token: 0x040000BC RID: 188
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
