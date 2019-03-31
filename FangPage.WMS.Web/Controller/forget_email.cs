using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x02000009 RID: 9
	public class forget_email : WebController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002E68 File Offset: 0x00001068
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("email");
				if (@string == "")
				{
					this.ShowErr("请输入您注册的邮箱。");
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
				SqlParam sqlParam = DbHelper.MakeAndWhere("email", @string);
				UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
				{
					sqlParam
				});
				if (userInfo.id == 0)
				{
					this.ShowErr("输入的邮件地址不存在。");
					return;
				}
				string text = FPRandom.CreateCode(20);
				DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("authflag", 2),
					DbHelper.MakeUpdate("authstr", text),
					DbHelper.MakeUpdate("authtime", DbUtils.GetDateTime()),
					DbHelper.MakeAndWhere("email", @string)
				});
				string newValue = string.Concat(new string[]
				{
					"<pre style=\"width:100%;word-wrap:break-word\"><a href=\"",
					this.domain,
					this.rawpath,
					"getpwd.aspx?authstr=",
					text,
					"\"  target=\"_blank\">",
					this.domain,
					this.rawpath,
					"getpwd.aspx?authstr=",
					text,
					"</a></pre>"
				});
				MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_resetpassword");
				if (msgTemplate.status > 0)
				{
					msgTemplate.content = msgTemplate.content.Replace("【用户名】", userInfo.username).Replace("【姓名】", userInfo.realname).Replace("【邮箱帐号】", @string).Replace("【激活链接】", newValue);
					Email.Send(@string, msgTemplate.name, msgTemplate.content);
				}
				base.AddMsg("密码重置邮件已发送到您的邮箱");
			}
		}
	}
}
