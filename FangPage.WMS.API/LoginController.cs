using System;

namespace FangPage.WMS.API
{
	// Token: 0x02000005 RID: 5
	public class LoginController : APIController
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000024F0 File Offset: 0x000006F0
		protected override void Init()
		{
			base.Init();
			if (this.userid <= 0)
			{
				base.WriteErr(1, "对不起，您尚未登录或超时!");
			}
			if (this.session.errcode > 0)
			{
				base.WriteErr(this.session.errmsg);
			}
		}
	}
}
