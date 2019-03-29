using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x0200004C RID: 76
	public class checkemail : LoginController
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000C054 File Offset: 0x0000A254
		protected override void View()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.ispost)
			{
				UserInfo userInfo = UserBll.GetUserInfo(this.userid);
				if (userInfo.isemail == 1)
				{
					this.ShowErr("您的邮箱已通过了验证。");
				}
				else
				{
					string @string = FPRequest.GetString("email");
					if (@string == "")
					{
						this.ShowErr("您还没有设定邮箱。");
					}
					else if (!FPUtils.IsEmail(@string))
					{
						this.ShowErr("Email格式不正确");
					}
					else if (DbHelper.ExecuteCount<UserInfo>(string.Format("[email]='{0}' AND [id]<>{1}", @string, this.userid)) > 0)
					{
						this.ShowErr("邮箱: \"" + @string + "\" 已经被其他用户使用");
					}
					else
					{
						string emailHostName = this.GetEmailHostName(@string);
						if (this.regconfig.accessemail.Trim() != "")
						{
							if (!FPUtils.InArray(emailHostName, this.regconfig.accessemail, "|"))
							{
								this.ShowErr("本站点只允许使用以下域名的Email地址：" + this.regconfig.accessemail);
								return;
							}
						}
						else if (this.regconfig.censoremail.Trim() != "")
						{
							if (FPUtils.InArray(@string, this.regconfig.censoremail, "|"))
							{
								this.ShowErr("本站点不允许使用以下域名的Email地址: " + this.regconfig.censoremail);
								return;
							}
						}
						userInfo.authstr = WMSUtils.CreateAuthStr(20);
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("isemail", 0),
							DbHelper.MakeSet("email", @string),
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
						msgTemplate.content = msgTemplate.content.Replace("【用户名】", userInfo.username).Replace("【邮箱帐号】", @string).Replace("【激活链接】", newValue);
						Email.Send(@string, msgTemplate.name, msgTemplate.content);
						base.ResetUser();
						base.AddMsg("您的注册邮箱[" + @string + "]将收到一封认证邮件，请登录您的邮箱查收，并点击邮件中的链接完成激活。");
					}
				}
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		private string GetEmailHostName(string strEmail)
		{
			string result;
			if (strEmail.IndexOf("@") < 0)
			{
				result = "";
			}
			else
			{
				result = strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
			}
			return result;
		}

		// Token: 0x04000151 RID: 337
		protected RegConfig regconfig = new RegConfig();
	}
}
