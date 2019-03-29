using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001F RID: 31
	public class watermarkset : SuperController
	{
		// Token: 0x0600004A RID: 74 RVA: 0x0000681C File Offset: 0x00004A1C
		protected override void View()
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
			base.SaveRightURL();
		}

		// Token: 0x0400003A RID: 58
		protected SysConfig sysconfiginfo = new SysConfig();
	}
}
