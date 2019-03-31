using System;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004F RID: 79
	public class regconfigmanage : SuperController
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000F438 File Offset: 0x0000D638
		protected override void Controller()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.ispost)
			{
				this.regconfig.regstatus = 0;
				this.regconfig.email = 0;
				this.regconfig.mobile = 0;
				this.regconfig.realname = 0;
				this.regconfig.depart = 0;
				this.regconfig.idcard = 0;
				this.regconfig.regacount = 0;
				this.regconfig.regmobile = 0;
				this.regconfig = FPRequest.GetModel<RegConfig>(this.regconfig);
				if (this.regconfig.regverify == 2)
				{
					this.regconfig.email = 1;
				}
				RegConfigs.SaveConfig(this.regconfig);
			}
		}

		// Token: 0x040000DF RID: 223
		protected RegConfig regconfig;
	}
}
