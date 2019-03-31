using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000030 RID: 48
	public class appmanage : SuperController
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00008D14 File Offset: 0x00006F14
		protected override void Controller()
		{
			if (this.ispost)
			{
				AppConfig mapAppConfig = AppConfigs.GetMapAppConfig(FPRequest.GetString("apppath"));
				if (mapAppConfig.guid == "")
				{
					this.ShowErr("对不起，该插件已被删除或者不存在。");
					return;
				}
				FPFile.GetMapPath(this.apppath + mapAppConfig.installpath);
				if (this.action == "delete")
				{
					SetupBll.DeleteSetup(mapAppConfig);
					FPCache.Remove("FP_APPLIST");
					FPCache.Remove("FP_SORTAPPLIST");
					base.Response.Redirect("appmanage.aspx");
				}
				else if (this.action == "download")
				{
					SetupBll.DownloadSetup(mapAppConfig);
				}
				else if (this.action == "build")
				{
					Version v = FPUtils.StrToVersion(mapAppConfig.version);
					Version version = FPUtils.StrToVersion("0.0.0");
					if (mapAppConfig.dll != "")
					{
						string[] array = FPArray.SplitString(mapAppConfig.dll, 2);
						if (array[1] == "")
						{
							array[1] = array[0].Replace(".Controller", "");
						}
						version = FPUtils.GetVersion(FPFile.GetMapPath(this.webpath + "bin/" + array[1] + ".dll"));
					}
					if (version > v)
					{
						mapAppConfig.version = FPUtils.FormatVersion(version.ToString());
					}
					string text = "app/" + mapAppConfig.installpath;
					if (Directory.Exists(FPFile.GetMapPath(this.webpath + "sites/" + text)))
					{
						SiteConfig siteConfig = SiteConfigs.LoadSiteConfig("app");
						siteConfig.dll = mapAppConfig.dll;
						FPViews.CreateSite(siteConfig, text);
					}
					if (mapAppConfig.createdate == "")
					{
						mapAppConfig.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					}
					if (mapAppConfig.updatedate == "")
					{
						mapAppConfig.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					}
					FPSerializer.Save<AppConfig>(mapAppConfig, FPFile.GetMapPath(this.webpath + "app/" + mapAppConfig.installpath + "/app.config"));
					FPCache.Remove("FP_APPLIST");
					base.AddMsg("应用编译成功！");
				}
			}
			this.applist = AppConfigs.GetMapAppList();
			bool flag = false;
			for (int i = 0; i < this.applist.Count; i++)
			{
				if (File.Exists(FPFile.GetMapPath(this.apppath + this.applist[i].installpath + "/app.config")))
				{
					Version v2 = FPUtils.StrToVersion(this.applist[i].version);
					Version version2 = FPUtils.StrToVersion("0.0.0");
					if (this.applist[i].dll != "")
					{
						string[] array2 = FPArray.SplitString(this.applist[i].dll, 2);
						if (array2[1] == "")
						{
							array2[1] = array2[0].Replace(".Controller", "");
						}
						version2 = FPUtils.GetVersion(FPFile.GetMapPath(this.webpath + "bin/" + array2[1] + ".dll"));
					}
					if (version2 > v2)
					{
						this.applist[i].version = FPUtils.FormatVersion(version2.ToString());
						this.applist[i].updatedate = DbUtils.GetDateTime().ToString();
						FPSerializer.Save<AppConfig>(this.applist[i], FPFile.GetMapPath(this.apppath + this.applist[i].installpath + "/app.config"));
						flag = true;
					}
				}
			}
			if (flag)
			{
				FPCache.Remove("FP_APPLIST");
			}
		}

		// Token: 0x0400007B RID: 123
		protected List<AppConfig> applist = new List<AppConfig>();
	}
}
