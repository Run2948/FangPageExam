using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000054 RID: 84
	public class forget : WMSController
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000CF90 File Offset: 0x0000B190
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("email");
				if (@string == "")
				{
					this.ShowErr("请输入邮箱。");
				}
				else
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("email", @string);
					UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
					{
						sqlParam
					});
					if (userInfo.id == 0)
					{
						this.ShowErr("输入的邮件地址不存在。");
					}
					else
					{
						string text = WMSUtils.CreateAuthStr(20);
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("authflag", 2),
							DbHelper.MakeSet("authstr", text),
							DbHelper.MakeSet("authtime", DbUtils.GetDateTime()),
							DbHelper.MakeAndWhere("email", @string)
						};
						DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
						string newValue = string.Concat(new string[]
						{
							"<pre style=\"width:100%;word-wrap:break-word\"><a href=\"http://",
							this.domain,
							this.rawpath,
							"getpass.aspx?authstr=",
							text,
							"\"  target=\"_blank\">http://",
							this.domain,
							this.rawpath,
							"getpass.aspx?authstr=",
							text,
							"</a></pre>"
						});
						MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_resetpassword");
						msgTemplate.content = msgTemplate.content.Replace("【用户名】", userInfo.username).Replace("【邮箱帐号】", @string).Replace("【激活链接】", newValue);
						Email.Send(@string, msgTemplate.name, msgTemplate.content);
						base.AddMsg("密码重置邮件已发送到您的邮箱");
					}
				}
			}
		}
	}
}
