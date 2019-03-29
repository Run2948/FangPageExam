using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000039 RID: 57
	public class creditadd : SuperController
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000BE3C File Offset: 0x0000A03C
		protected override void View()
		{
			string @string = FPRequest.GetString("uid");
			this.uname = this.GetUserName(@string);
			if (@string == "")
			{
				this.reurl = "usermanage.aspx";
			}
			if (this.ispost)
			{
				string string2 = FPRequest.GetString("username");
				if (string2 == "")
				{
					this.ShowErr("充值用户名不能为空");
					return;
				}
				this.credits = FPRequest.GetInt("credits");
				if (this.credits == 0)
				{
					this.ShowErr("充值积分必须大于零或小于零");
					return;
				}
				string text = FPRequest.GetString("note");
				if (text == "")
				{
					text = "用户积分充值";
				}
				foreach (string value in FPUtils.SplitString(string2))
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("username", value);
					UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
					{
						sqlParam
					});
					if (userInfo.id <= 0)
					{
						this.ShowErr("对不起，该充值用户不存在");
						return;
					}
					UserBll.UpdateUserCredit(new CreditInfo
					{
						uid = userInfo.id,
						name = text,
						type = 1,
						credits = this.credits,
						doid = this.userid,
						doname = this.username
					});
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		private string GetUserName(string uidlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, uidlist);
			List<UserInfo> list = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			foreach (UserInfo userInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				text += userInfo.username;
			}
			return text;
		}

		// Token: 0x04000089 RID: 137
		protected string reurl = "creditmanage.aspx";

		// Token: 0x0400008A RID: 138
		protected int credits;

		// Token: 0x0400008B RID: 139
		protected string uname = "";
	}
}
