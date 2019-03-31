using System;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;

namespace FP_Attach.Controller
{
	// Token: 0x02000008 RID: 8
	public class delete : LoginController
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002A00 File Offset: 0x00000C00
		protected override void Controller()
		{
			AttachBll.DeleteById(this.aid);
			SysBll.InsertLog(this.userid, "删除附件", "用户：" + this.realname + "，删除附件：" + this.aid, true);
			FPResponse.WriteJson(new
			{
				errcode = 0,
				errmsg = "OK",
				aid = this.aid
			});
		}

		// Token: 0x04000015 RID: 21
		protected string aid = FPRequest.GetString("aid");
	}
}
