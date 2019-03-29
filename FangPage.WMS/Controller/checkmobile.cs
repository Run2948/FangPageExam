using System;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x0200004E RID: 78
	public class checkmobile : LoginController
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		protected override void View()
		{
			if (this.ispost)
			{
				UserInfo userInfo = UserBll.GetUserInfo(this.userid);
				if (userInfo.ismobile == 1)
				{
					this.ShowErr("您的手机已通过了验证。");
				}
				else
				{
					string @string = FPRequest.GetString("mobile");
					if (@string.Trim().Length > 20)
					{
						this.ShowErr("手机号码不能大于20个字符");
					}
					else if (@string.Trim() != "" && !Regex.IsMatch(@string.Trim(), "^[\\d|-]+$"))
					{
						this.ShowErr("手机号码中含有非法字符");
					}
					else
					{
						string string2 = FPRequest.GetString("code");
						string clientsms = string.Concat(new object[]
						{
							@string,
							"|",
							string2,
							"|",
							DbUtils.GetDateTime()
						});
						if (this.Session["FP_SMSVERIFY"] != null)
						{
							string serversms = this.Session["FP_SMSVERIFY"].ToString();
							int num = SMS.CheckSMS(clientsms, serversms);
							if (num == 1)
							{
								SqlParam[] sqlparams = new SqlParam[]
								{
									DbHelper.MakeSet("ismobile", 1),
									DbHelper.MakeSet("mobile", @string),
									DbHelper.MakeAndWhere("id", this.userid)
								};
								DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
								base.ResetUser();
								base.AddMsg("手机号码已绑定成功！");
							}
							else if (num == 0)
							{
								this.ShowErr("验证手机号码不正确。");
							}
							else if (num == -1)
							{
								this.ShowErr("验证码不正确。");
							}
							else if (num == -2)
							{
								this.ShowErr("验证码已过期。");
							}
						}
						else
						{
							this.ShowErr("无效验证码。");
						}
					}
				}
			}
		}
	}
}
