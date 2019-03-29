using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000051 RID: 81
	public class userlog : LoginController
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		protected override void View()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("uid", this.userid);
			this.userloglist = DbHelper.ExecuteList<SysLogInfo>(this.pager, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x04000154 RID: 340
		protected List<SysLogInfo> userloglist = new List<SysLogInfo>();

		// Token: 0x04000155 RID: 341
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
