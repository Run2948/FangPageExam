using System;

namespace FangPage.WMS
{
	// Token: 0x0200000B RID: 11
	public class SuperController : AdminController
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003700 File Offset: 0x00001900
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!this.isperm)
			{
				this.ShowErr("对不起，您没有权限访问!");
			}
		}
	}
}
