using System;
using FangPage.MVC;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000053 RID: 83
	public class userpwd : LoginController
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("oldpwd");
				string string2 = FPRequest.GetString("newpwd");
				if (UserBll.CheckPassword(this.userid, @string).id == 0)
				{
					this.ShowErr("输入的旧密码不正确！");
				}
				else
				{
					string string3 = FPRequest.GetString("renewpwd");
					if (string2 != string3)
					{
						this.ShowErr("两次输入密码不相同！");
					}
					else
					{
						UserBll.UpdatePassword(this.userid, string3);
						base.AddMsg("密码更新成功。");
					}
				}
			}
		}
	}
}
