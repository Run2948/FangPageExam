using System;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;

namespace FP_Attach.Controller
{
	// Token: 0x02000007 RID: 7
	public class delete2 : APIController
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000298C File Offset: 0x00000B8C
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

		// Token: 0x04000014 RID: 20
		protected string aid = FPRequest.GetString("aid");
	}
}
