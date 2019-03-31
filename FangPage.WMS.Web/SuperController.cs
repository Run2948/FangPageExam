using System;

namespace FangPage.WMS.Web
{
	// Token: 0x02000004 RID: 4
	public class SuperController : AdminController
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002592 File Offset: 0x00000792
		protected override void Init()
		{
			base.Init();
			if (!this.isperm)
			{
				this.ShowErr("对不起，您没有权限访问!", "返回");
				return;
			}
		}
	}
}
