using System;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x02000008 RID: 8
	public class forget : WebController
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002C4C File Offset: 0x00000E4C
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("mobile");
				string string2 = FPRequest.GetString("smsverify");
				if (@string == "")
				{
					this.ShowErr("请输入您注册的手机号码。");
					return;
				}
				if (@string.Trim().Length > 11)
				{
					this.ShowErr("手机号码不能大于11个字符");
					return;
				}
				if (!Regex.IsMatch(@string.Trim(), "^[\\d|-]+$"))
				{
					this.ShowErr("手机号码中含有非法字符");
					return;
				}
				if (this.isseccode)
				{
					if (FPRequest.GetString("verify").Equals(""))
					{
						this.ShowErr("图片验证码不能为空");
						return;
					}
					if (!this.isvalid)
					{
						this.ShowErr("图片验证码错误");
						return;
					}
				}
				if (string2.Equals(""))
				{
					this.ShowErr("短信验证码不能为空");
					return;
				}
				string clientsms = string.Concat(new object[]
				{
					@string,
					"|",
					string2,
					"|",
					DbUtils.GetDateTime()
				});
				if (this.Session["FP_SMSVERIFY"] == null)
				{
					this.ShowErr("无效验证码");
					return;
				}
				string serversms = this.Session["FP_SMSVERIFY"].ToString();
				int num = SMS.CheckSMS(clientsms, serversms);
				if (num == 0)
				{
					this.ShowErr("验证手机号码不正确");
					return;
				}
				if (num == -1)
				{
					this.ShowErr("验证码不正确");
					return;
				}
				if (num == 2)
				{
					this.ShowErr("验证码已过期");
					return;
				}
				SqlParam sqlParam = DbHelper.MakeAndWhere("mobile", @string);
				if (DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
				{
					sqlParam
				}).id == 0)
				{
					this.ShowErr("输入的手机号码不存在。");
					return;
				}
				string text = FPRandom.CreateCode(20);
				DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("authflag", 2),
					DbHelper.MakeUpdate("authstr", text),
					DbHelper.MakeUpdate("authtime", DbUtils.GetDateTime()),
					DbHelper.MakeAndWhere("mobile", @string)
				});
				FPResponse.Redirect("getpwd.aspx?authstr=" + text);
			}
		}
	}
}
