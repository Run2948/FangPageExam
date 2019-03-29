using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003A RID: 58
	public class creditmanage : SuperController
	{
		// Token: 0x0600008E RID: 142 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("chkid");
				foreach (int id in FPUtils.SplitInt(@string))
				{
					CreditInfo creditInfo = DbHelper.ExecuteModel<CreditInfo>(id);
					if (DbHelper.ExecuteDelete<CreditInfo>(id) > 0)
					{
						string sqlstring = string.Format("UPDATE [{0}WMS_UserInfo] SET [credits]=[credits]+{1} WHERE [id]={2}", DbConfigs.Prefix, creditInfo.credits * -1, creditInfo.uid);
						DbHelper.ExecuteSql(sqlstring);
					}
				}
			}
			if (this.uid > 0)
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("uid", this.uid);
				this.creditlist = DbHelper.ExecuteList<CreditInfo>(this.pager, new SqlParam[]
				{
					sqlParam
				});
			}
			else
			{
				this.creditlist = DbHelper.ExecuteList<CreditInfo>(this.pager);
			}
			this.iuser = UserBll.GetUserInfo(this.uid);
			base.SaveRightURL();
		}

		// Token: 0x0400008C RID: 140
		protected int uid = FPRequest.GetInt("uid");

		// Token: 0x0400008D RID: 141
		protected UserInfo iuser = new UserInfo();

		// Token: 0x0400008E RID: 142
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x0400008F RID: 143
		protected List<CreditInfo> creditlist = new List<CreditInfo>();
	}
}
