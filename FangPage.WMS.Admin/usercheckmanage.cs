using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004A RID: 74
	public class usercheckmanage : SuperController
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000DE84 File Offset: 0x0000C084
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("chkdel");
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeSet("isidcard", 0),
					DbHelper.MakeSet("idcardface", ""),
					DbHelper.MakeAndWhere("uid", WhereType.In, @string)
				};
				DbHelper.ExecuteUpdate<FullUserInfo>(sqlparams);
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeSet("isreal", 0),
					DbHelper.MakeAndWhere("id", WhereType.In, @string)
				};
				DbHelper.ExecuteUpdate<UserInfo>(sqlparams2);
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("idcardface", WhereType.NotEqual, "");
			this.userinfolist = DbHelper.ExecuteList<FullUserInfo>(this.pager, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x040000B2 RID: 178
		protected List<FullUserInfo> userinfolist = new List<FullUserInfo>();

		// Token: 0x040000B3 RID: 179
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
