using System;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x02000010 RID: 16
	public class userpwd : LoginController
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000038A4 File Offset: 0x00001AA4
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("oldpwd");
				string string2 = FPRequest.GetString("newpwd");
				if (UserBll.CheckPassword(this.userid, @string).id == 0)
				{
					this.ShowErr("输入的旧密码不正确！");
					return;
				}
				string string3 = FPRequest.GetString("renewpwd");
				if (string2 != string3)
				{
					this.ShowErr("两次输入密码不相同！");
					return;
				}
				UserBll.UpdatePassword(this.userid, string3);
				if (this.sysconfig.ssocheck == 1)
				{
					SSO.UpdatePassword(this.username, string3);
				}
				base.AddMsg("密码更新成功。");
			}
		}
	}
}
