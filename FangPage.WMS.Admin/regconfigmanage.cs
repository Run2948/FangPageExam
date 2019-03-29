using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000045 RID: 69
	public class regconfigmanage : SuperController
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000DA00 File Offset: 0x0000BC00
		protected override void View()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.ispost)
			{
				this.regconfig.regstatus = 0;
				this.regconfig.email = 0;
				this.regconfig.mobile = 0;
				this.regconfig.realname = 0;
				this.regconfig.rules = 0;
				this.regconfig = FPRequest.GetModel<RegConfig>(this.regconfig);
				RegConfigs.SaveConfig(this.regconfig);
			}
			base.SaveRightURL();
		}

		// Token: 0x040000A8 RID: 168
		protected RegConfig regconfig;
	}
}
