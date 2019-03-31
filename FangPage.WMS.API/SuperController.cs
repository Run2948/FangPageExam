using System;

namespace FangPage.WMS.API
{
	// Token: 0x02000006 RID: 6
	public class SuperController : AdminController
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000252C File Offset: 0x0000072C
		protected override void Init()
		{
			base.Init();
			if (!this.isperm)
			{
				base.WriteErr("对不起，您没有权限访问!");
				return;
			}
		}
	}
}
