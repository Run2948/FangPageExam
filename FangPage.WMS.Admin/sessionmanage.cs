using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000012 RID: 18
	public class sessionmanage : AdminController
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00004378 File Offset: 0x00002578
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				if (this.action == "delete")
				{
					string @string = FPRequest.GetString("chkdel");
					if (@string != "")
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, @string);
						this.sessionlist = DbHelper.ExecuteList<SessionInfo>(new SqlParam[]
						{
							sqlParam
						});
						foreach (SessionInfo sessionInfo in this.sessionlist)
						{
							DbHelper.ExecuteDelete<SessionInfo>(sessionInfo.id);
							SessionBll.RemoveCacheSession(sessionInfo.platform, sessionInfo.token);
						}
					}
					base.Response.Redirect("sessionmanage.aspx");
				}
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("state", 1),
				DbHelper.MakeAndWhere("platform", this.platform)
			};
			this.sessionlist = DbHelper.ExecuteList<SessionInfo>(this.pager, sqlparams);
		}

		// Token: 0x0400002A RID: 42
		protected List<SessionInfo> sessionlist = new List<SessionInfo>();

		// Token: 0x0400002B RID: 43
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
