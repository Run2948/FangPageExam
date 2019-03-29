using System;
using FangPage.MVC;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000052 RID: 82
	public class userpwd2 : LoginController
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000CE1C File Offset: 0x0000B01C
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.user.password2 != "")
				{
					string @string = FPRequest.GetString("oldpwd");
					if (UserBll.CheckPassword2(this.userid, @string).id == 0)
					{
						this.ShowErr("输入的旧密码不正确！");
						return;
					}
				}
				string string2 = FPRequest.GetString("newpwd");
				string string3 = FPRequest.GetString("renewpwd");
				if (string2 != string3)
				{
					this.ShowErr("两次输入密码不相同！");
				}
				else
				{
					UserBll.UpdatePassword2(this.userid, string3);
					base.AddMsg("交易密码更新成功。");
				}
			}
		}
	}
}
