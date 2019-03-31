using System;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000025 RID: 37
	public class watermarkset : SuperController
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000075DC File Offset: 0x000057DC
		protected override void Controller()
		{
			this.sysconfiginfo = SysConfigs.GetConfig();
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				this.sysconfiginfo = FPRequest.GetModel<SysConfig>(this.sysconfiginfo);
				SysConfigs.SaveConfig(this.sysconfiginfo);
				SysConfigs.ResetConfig();
			}
		}

		// Token: 0x04000052 RID: 82
		protected SysConfig sysconfiginfo = new SysConfig();
	}
}
