using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000020 RID: 32
	public class index : AdminController
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000689C File Offset: 0x00004A9C
		protected override void View()
		{
			if (this.sysconfig.admintitle == "")
			{
				this.pagetitle = this.siteconfig.sitetitle;
			}
			else
			{
				this.pagetitle = this.sysconfig.admintitle;
			}
			if (this.Session["FP_ADMIN_LEFTMENU"] != null)
			{
				this.lefturl = this.Session["FP_ADMIN_LEFTMENU"].ToString();
			}
			if (string.IsNullOrEmpty(this.lefturl))
			{
				if (this.role.menus != "" && this.roleid != 1)
				{
					MenuInfo menuInfo = DbHelper.ExecuteModel<MenuInfo>(FPUtils.SplitInt(this.role.menus)[0]);
					this.lefturl = menuInfo.lefturl;
					if (this.lefturl != "")
					{
						if (this.lefturl.IndexOf("?") > 0)
						{
							this.lefturl = this.lefturl + "&topmenuid=" + menuInfo.id;
						}
						else
						{
							this.lefturl = this.lefturl + "?topmenuid=" + menuInfo.id;
						}
					}
				}
				if (this.lefturl == "")
				{
					this.lefturl = "sysmenu.aspx?parentid=1";
				}
			}
			if (this.Session["FP_ADMIN_RIGHTMENU"] != null)
			{
				this.righturl = this.Session["FP_ADMIN_RIGHTMENU"].ToString();
			}
			if (string.IsNullOrEmpty(this.righturl))
			{
				this.righturl = this.role.desktopurl;
			}
		}

		// Token: 0x0400003B RID: 59
		protected string lefturl = "";

		// Token: 0x0400003C RID: 60
		protected string righturl = "";
	}
}
