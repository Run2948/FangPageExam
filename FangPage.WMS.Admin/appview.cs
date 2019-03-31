using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000006 RID: 6
	public class appview : AdminController
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000027C4 File Offset: 0x000009C4
		protected override void Controller()
		{
			this.appinfo = AppConfigs.GetMapAppConfig(this.app_path);
			if (this.appinfo.guid == "")
			{
				this.ShowErr("对不起，该应用已被删除或不存在。");
				return;
			}
			string mapPath = FPFile.GetMapPath(this.apppath + this.appinfo.installpath);
			this.appinfo.size = FPFile.FormatBytesStr(FPFile.GetDirSize(mapPath));
		}

		// Token: 0x04000008 RID: 8
		protected string app_path = FPRequest.GetString("apppath");

		// Token: 0x04000009 RID: 9
		protected AppConfig appinfo = new AppConfig();
	}
}
