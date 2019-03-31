using System;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000015 RID: 21
	public class ssoconfigs : AdminController
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000046A4 File Offset: 0x000028A4
		protected override void Controller()
		{
			this.ssoconfig = SSOConfigs.GetSSOConfig();
			if (this.ispost)
			{
				this.ssoconfig = FPRequest.GetModel<SSOConfig>(this.ssoconfig);
				SSOConfigs.SaveConfig(this.ssoconfig);
				SSO.ReSetConfig();
				base.AddMsg("保存配置成功!");
			}
		}

		// Token: 0x04000030 RID: 48
		protected SSOConfig ssoconfig;
	}
}
