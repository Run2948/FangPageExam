using System;
using System.Collections.Generic;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000027 RID: 39
	public class index : AdminController
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00007678 File Offset: 0x00005878
		protected override void Controller()
		{
			if (this.sysconfig.adminpath.ToLower() != this.sitepath.ToLower())
			{
				this.sysconfig.adminpath = this.sitepath;
				SysConfigs.SaveConfig(this.sysconfig);
				SysConfigs.ResetConfig();
			}
			List<MenuInfo> list = new List<MenuInfo>();
			if (this.roleid == 1)
			{
				list = MenuBll.GetMenuList(0);
			}
			else
			{
				list = MenuBll.GetMenuList(0, this.role.menus);
			}
			if (list.Count > 0)
			{
				this.lefturl = list[0].lefturl;
				this.righturl = list[0].righturl;
			}
			if (this.lefturl == "")
			{
				this.lefturl = "sysmenu.aspx?parentid=1";
			}
			if (string.IsNullOrEmpty(this.righturl))
			{
				this.righturl = "desktop.aspx";
			}
		}

		// Token: 0x04000053 RID: 83
		protected string lefturl = "";

		// Token: 0x04000054 RID: 84
		protected string righturl = "";

		// Token: 0x04000055 RID: 85
		protected new string version = "7.4.1";
	}
}
