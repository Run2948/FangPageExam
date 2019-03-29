using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000D RID: 13
	public class pluginadd : SuperController
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000362C File Offset: 0x0000182C
		protected override void View()
		{
			if (this.pluname != "")
			{
				this.pluginconfig = FPSerializer.Load<PluginConfig>(FPUtils.GetMapPath(this.webpath + "plugins/" + this.pluname + "/plugin.config"));
				this.pluginconfig.installpath = this.pluname;
			}
			if (this.ispost)
			{
				this.pluginconfig = FPRequest.GetModel<PluginConfig>(this.pluginconfig);
				if (this.pluginconfig.guid == "")
				{
					this.pluginconfig.guid = Guid.NewGuid().ToString();
				}
				FPSerializer.Save<PluginConfig>(this.pluginconfig, FPUtils.GetMapPath(this.webpath + "plugins/" + this.pluginconfig.installpath + "/plugin.config"));
				base.Response.Redirect("pluginmanage.aspx");
			}
		}

		// Token: 0x04000014 RID: 20
		protected PluginConfig pluginconfig = new PluginConfig();

		// Token: 0x04000015 RID: 21
		protected string pluname = FPRequest.GetString("pluname");
	}
}
