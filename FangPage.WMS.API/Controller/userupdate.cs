using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x0200000F RID: 15
	public class userupdate : LoginController
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00003DE4 File Offset: 0x00001FE4
		protected override void Controller()
		{
			if (this.ispost)
			{
				this.user = FPRequest.GetModel<UserInfo>(this.user);
				DbHelper.ExecuteUpdate<UserInfo>(this.user);
				base.ResetUser();
				base.WriteSuccess("更改用户成功");
			}
			base.WriteErr("");
		}
	}
}
