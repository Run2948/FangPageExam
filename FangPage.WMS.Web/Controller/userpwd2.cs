using System;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x02000011 RID: 17
	public class userpwd2 : LoginController
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003944 File Offset: 0x00001B44
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("oldpwd");
				if (UserBll.CheckPassword2(this.userid, @string).id == 0)
				{
					this.ShowErr("输入的旧密码不正确！");
					return;
				}
				string string2 = FPRequest.GetString("newpwd");
				string string3 = FPRequest.GetString("renewpwd");
				if (string2 != string3)
				{
					this.ShowErr("两次输入密码不相同！");
					return;
				}
				UserBll.UpdatePassword2(this.userid, string3);
				base.AddMsg("交易密码更新成功。");
			}
		}
	}
}
