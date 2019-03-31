using System;

namespace FangPage.WMS.API
{
	// Token: 0x02000003 RID: 3
	public class AdminController : APIController
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		protected override void Init()
		{
			base.Init();
			if (this.userid <= 0)
			{
				base.WriteErr(1, "对不起，您尚未登录或超时!");
			}
			if (!this.isadmin)
			{
				base.WriteErr(2, "对不起，您没有权限访问后台!");
			}
		}
	}
}
