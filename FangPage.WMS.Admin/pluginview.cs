using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000010 RID: 16
	public class pluginview : AdminController
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000418C File Offset: 0x0000238C
		protected override void Controller()
		{
			this.plugininfo = PluginConfigs.GetMapPluConfig(this.plu_path);
			if (this.plugininfo.installpath == "")
			{
				this.ShowErr("对不起，该插件已被删除或不存在。");
				return;
			}
			string mapPath = FPFile.GetMapPath(this.plupath + this.plugininfo.installpath);
			this.plugininfo.size = FPFile.FormatBytesStr(FPFile.GetDirSize(mapPath));
		}

		// Token: 0x04000026 RID: 38
		protected PluginConfig plugininfo = new PluginConfig();

		// Token: 0x04000027 RID: 39
		protected string plu_path = FPRequest.GetString("plupath");
	}
}
