using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000049 RID: 73
	public class usercheckinfo : SuperController
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		protected override void View()
		{
			this.iuser = DbHelper.ExecuteModel<UserInfo>(this.uid);
			if (this.iuser.id == 0)
			{
				this.ShowErr("该用户不存在或已被删除。");
			}
			else
			{
				this.userinfo = DbHelper.ExecuteModel<FullUserInfo>(this.uid);
				if (this.ispost)
				{
					this.iuser = FPRequest.GetModel<UserInfo>(this.iuser);
					this.userinfo = FPRequest.GetModel<FullUserInfo>(this.userinfo);
					if (this.userinfo.isidcard == 1)
					{
						this.iuser.isreal = 1;
					}
					else
					{
						this.iuser.isreal = 0;
					}
					DbHelper.ExecuteUpdate<UserInfo>(this.iuser);
					DbHelper.ExecuteUpdate<FullUserInfo>(this.userinfo);
					base.Response.Redirect("usercheckmanage.aspx");
				}
			}
		}

		// Token: 0x040000AF RID: 175
		protected int uid = FPRequest.GetInt("uid");

		// Token: 0x040000B0 RID: 176
		protected FullUserInfo userinfo = new FullUserInfo();

		// Token: 0x040000B1 RID: 177
		protected UserInfo iuser = new UserInfo();
	}
}
