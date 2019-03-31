using System;
using System.Collections.Generic;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Attach.Controller
{
	// Token: 0x02000006 RID: 6
	public class copy : APIController
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002854 File Offset: 0x00000A54
		protected override void Controller()
		{
			if (this.uid == 0 && this.username != "")
			{
				UserInfo userInfo = UserBll.GetUserInfo(this.username);
				this.uid = userInfo.id;
			}
			if (this.uid == 0)
			{
				base.WriteErr("附件用户不存在。");
			}
			if (this.app == "")
			{
				base.WriteErr("附件应用不能为空。");
			}
			if (this.attachid1 == "" || this.attachid2 == "")
			{
				base.WriteErr("附件ID不能为空。");
			}
            // Token: 0x04000010 RID: 16
            this.username = FPRequest.GetString("username");
			List<AttachInfo> attachList = AttachBll.GetAttachList(this.attachid1);
			AttachBll.Create(attachList, this.attachid2, this.userid, this.app);
			FPResponse.WriteJson(new
			{
				errcode = 0,
				errmsg = "OK",
				attachlist = attachList
			});
		}

		// Token: 0x0400000F RID: 15
		protected string app = FPRequest.GetString("app");

		// Token: 0x04000010 RID: 16
		//protected string username = FPRequest.GetString("username");

		// Token: 0x04000011 RID: 17
		protected int uid = FPRequest.GetInt("uid");

		// Token: 0x04000012 RID: 18
		protected string attachid1 = FPRequest.GetString("attachid1");

		// Token: 0x04000013 RID: 19
		protected string attachid2 = FPRequest.GetString("attachid2");
	}
}
