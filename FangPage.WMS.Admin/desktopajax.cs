using System;
using System.Collections;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000C RID: 12
	public class desktopajax : AdminController
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000354C File Offset: 0x0000174C
		protected override void Controller()
		{
			if (this.ispost)
			{
				DesktopInfo desktopInfo = DesktopBll.GetDesktopInfo(this.parentid);
				DesktopInfo desktopInfo2 = new DesktopInfo();
				desktopInfo2.name = this.name;
				if (this.type == "sites")
				{
					SiteConfig mapSiteConfig = SiteConfigs.GetMapSiteConfig(this.path);
					if (desktopInfo2.name == "")
					{
						desktopInfo2.name = mapSiteConfig.name;
					}
					if (desktopInfo.markup == "wms_desktop")
					{
						desktopInfo2.url = mapSiteConfig.adminurl;
					}
					else
					{
						desktopInfo2.url = mapSiteConfig.indexurl;
					}
					if (mapSiteConfig.icon != "")
					{
						desktopInfo2.icon = mapSiteConfig.icon;
					}
					else
					{
						desktopInfo2.icon = this.webpath + "common/images/site.png";
					}
				}
				else if (this.type == "app")
				{
					AppConfig mapAppConfig = AppConfigs.GetMapAppConfig(this.path);
					if (desktopInfo2.name == "")
					{
						desktopInfo2.name = mapAppConfig.name;
					}
					if (desktopInfo.markup == "wms_desktop")
					{
						desktopInfo2.url = mapAppConfig.adminurl;
					}
					else
					{
						desktopInfo2.url = mapAppConfig.indexurl;
					}
					if (mapAppConfig.icon != "")
					{
						desktopInfo2.icon = mapAppConfig.icon;
					}
					else
					{
						desktopInfo2.icon = this.webpath + "common/images/app.png";
					}
				}
				else if (this.type == "plugins")
				{
					PluginConfig mapPluConfig = PluginConfigs.GetMapPluConfig(this.path);
					if (desktopInfo2.name == "")
					{
						desktopInfo2.name = mapPluConfig.name;
					}
					if (desktopInfo.markup == "wms_desktop")
					{
						desktopInfo2.url = mapPluConfig.adminurl;
					}
					else
					{
						desktopInfo2.url = mapPluConfig.indexurl;
					}
					if (mapPluConfig.icon != "")
					{
						desktopInfo2.icon = mapPluConfig.icon;
					}
					else
					{
						desktopInfo2.icon = this.webpath + "common/images/plugin.png";
					}
				}
				desktopInfo2.app = this.type + "_" + this.path;
				desktopInfo2.uid = this.userid;
				desktopInfo2.parentid = this.parentid;
				desktopInfo2.desk = DesktopBll.GetMaxDesk(desktopInfo.markup);
				desktopInfo2.target = "_self";
				desktopInfo2.platform = this.sysconfig.platform;
				SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
				int num = DbHelper.ExecuteCount<DesktopInfo>(new SqlParam[]
				{
					sqlParam
				});
				desktopInfo2.display = num + 1;
				DbHelper.ExecuteInsert<DesktopInfo>(desktopInfo2);
				FPCache.Remove("FP_DESKTOPLIST");
				Hashtable hashtable = new Hashtable();
				hashtable["msg"] = "OK";
				base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
				base.Response.Write(FPJson.ToJson(hashtable));
				base.Response.End();
			}
		}

		// Token: 0x0400001C RID: 28
		protected string type = FPRequest.GetString("type").ToLower();

		// Token: 0x0400001D RID: 29
		protected string path = FPRequest.GetString("path").ToLower();

		// Token: 0x0400001E RID: 30
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x0400001F RID: 31
		protected string name = FPRequest.GetString("name");
	}
}
