using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000059 RID: 89
	public class userprofile : LoginController
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		protected override void View()
		{
			this.fulluserinfo = DbHelper.ExecuteModel<FullUserInfo>(this.userid);
			if (this.fulluserinfo.id == 0)
			{
				this.ShowErr("对不起，该用户不存在或已被删除。");
			}
			else
			{
				this.bday = FPUtils.SplitString(this.fulluserinfo.bday, ",", 3);
				if (this.ispost)
				{
					this.fulluserinfo = FPRequest.GetModel<FullUserInfo>(this.fulluserinfo);
					DbHelper.ExecuteUpdate<FullUserInfo>(this.fulluserinfo);
					this.user = this.fulluserinfo;
					base.ResetUser();
					base.AddMsg("信息更新成功！");
				}
			}
		}

		// Token: 0x0400015B RID: 347
		protected FullUserInfo fulluserinfo = new FullUserInfo();

		// Token: 0x0400015C RID: 348
		protected string[] bday;
	}
}
