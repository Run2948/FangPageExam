using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x0200004D RID: 77
	public class changeemail : LoginController
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000C404 File Offset: 0x0000A604
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("password");
				UserInfo userInfo = UserBll.CheckPassword(this.userid, @string);
				if (userInfo.id > 0)
				{
					string string2 = FPRequest.GetString("email");
					if (string2 == "")
					{
						this.ShowErr("请输入新邮箱。");
					}
					else if (string2 == this.user.email)
					{
						this.ShowErr("输入的新邮箱跟原来的一样，无需更改。");
					}
					else if (!FPUtils.IsEmail(string2))
					{
						this.ShowErr("Email格式不正确");
					}
					else if (DbHelper.ExecuteCount<UserInfo>("[email]='" + string2 + "'") > 0)
					{
						this.ShowErr("邮箱: \"" + string2 + "\" 已经被其他用户使用。");
					}
					else
					{
						userInfo.authstr = WMSUtils.CreateAuthStr(20);
						userInfo.email = string2;
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("isemail", 0),
							DbHelper.MakeSet("email", userInfo.email),
							DbHelper.MakeSet("authflag", 1),
							DbHelper.MakeSet("authstr", userInfo.authstr),
							DbHelper.MakeSet("authtime", DbUtils.GetDateTime()),
							DbHelper.MakeAndWhere("id", this.userid)
						};
						DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
						string newValue = string.Concat(new string[]
						{
							"<pre style=\"width:100%;word-wrap:break-word\"><a href=\"http://",
							this.domain,
							this.rawpath,
							"activationuser.aspx?authstr=",
							userInfo.authstr,
							"\"  target=\"_blank\">http://",
							this.domain,
							this.rawpath,
							"activationuser.aspx?authstr=",
							userInfo.authstr,
							"</a></pre>"
						});
						MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_register");
						msgTemplate.content = msgTemplate.content.Replace("【用户名】", userInfo.username).Replace("【邮箱帐号】", userInfo.email).Replace("【激活链接】", newValue);
						Email.Send(userInfo.email, msgTemplate.name, msgTemplate.content);
						base.ResetUser();
						base.AddMsg("您的注册邮箱[" + userInfo.email + "]将收到一封认证邮件，请登录您的邮箱查收，并点击邮件中的链接完成激活。");
					}
				}
				else
				{
					this.ShowErr("对不起，用户密码不正确。");
				}
			}
		}
	}
}
