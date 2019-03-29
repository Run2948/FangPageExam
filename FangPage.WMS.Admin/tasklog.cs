using System;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001D RID: 29
	public class tasklog : SuperController
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00006508 File Offset: 0x00004708
		protected override void View()
		{
			string mapPath = FPUtils.GetMapPath(this.webpath + "cache/tasklog.config");
			this.logcontent = FPFile.ReadFile(mapPath);
			if (this.ispost)
			{
				this.logcontent = FPRequest.GetString("logcontent");
				FPFile.WriteFile(mapPath, this.logcontent);
			}
			base.SaveRightURL();
		}

		// Token: 0x04000038 RID: 56
		protected string logcontent = "";
	}
}
