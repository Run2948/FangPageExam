using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Configuration;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001B RID: 27
	public class sysconfigmanage : SuperController
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000059C4 File Offset: 0x00003BC4
		protected override void Controller()
		{
			this.sysconfiginfo = SysConfigs.GetConfig();
			this.sitelist = SiteConfigs.GetSysSiteList();
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
			if (configuration.AppSettings.Settings["sitepath"] != null)
			{
				this.mainsite = configuration.AppSettings.Settings["sitepath"].Value;
			}
			CustomErrorsSection customErrorsSection = (CustomErrorsSection)configuration.GetSection("system.web/customErrors");
			if (customErrorsSection.Mode == CustomErrorsMode.Off)
			{
				this.customerror = 1;
			}
			if (this.sysconfiginfo.admintitle == "")
			{
				this.sysconfiginfo.admintitle = this.siteinfo.sitetitle;
			}
			if (this.ispost)
			{
				this.sysconfiginfo.disableie = "";
				this.sysconfiginfo = FPRequest.GetModel<SysConfig>(this.sysconfiginfo);
				this.mainsite = FPRequest.GetString("mainsite");
				if (this.sysconfiginfo.adminpath.ToLower() != this.sitepath.ToLower())
				{
					this.sysconfiginfo.adminpath = this.sitepath;
				}
				SiteConfig siteInfo = SiteConfigs.GetSiteInfo(this.mainsite);
				this.sysconfiginfo.platname = siteInfo.sitetitle;
				this.siteinfo.sitetitle = this.sysconfiginfo.admintitle;
				this.siteinfo.name = this.sysconfiginfo.admintitle;
				string @string = FPRequest.GetString("m_platform");
				if (@string != this.sysconfiginfo.platform)
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("platform", WhereType.Contain, this.sysconfiginfo.platform);
					foreach (MenuInfo menuInfo in DbHelper.ExecuteList<MenuInfo>(new SqlParam[]
					{
						sqlParam
					}))
					{
						DbHelper.ExecuteUpdate<MenuInfo>(new SqlParam[]
						{
							DbHelper.MakeUpdate("platform", FPArray.Replace(menuInfo.platform, this.sysconfiginfo.platform, @string)),
							DbHelper.MakeAndWhere("id", menuInfo.id)
						});
					}
					foreach (DesktopInfo desktopInfo in DbHelper.ExecuteList<DesktopInfo>(new SqlParam[]
					{
						sqlParam
					}))
					{
						DbHelper.ExecuteUpdate<DesktopInfo>(new SqlParam[]
						{
							DbHelper.MakeUpdate("platform", FPArray.Replace(desktopInfo.platform, this.sysconfiginfo.platform, @string)),
							DbHelper.MakeAndWhere("id", desktopInfo.id)
						});
					}
					this.sysconfiginfo.platform = @string;
				}
				FPSerializer.Save<SiteConfig>(this.siteinfo, FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath + "/site.config"));
				if (File.Exists(FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath + "/site.config")))
				{
					FPSerializer.Save<SiteConfig>(this.siteinfo, FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath + "/site.config"));
				}
				FPCache.Remove("FP_SITELIST");
				SysConfigs.SaveConfig(this.sysconfiginfo);
				SysConfigs.ResetConfig();
				if (FPRequest.GetInt("customerror") != this.customerror)
				{
					this.customerror = FPRequest.GetInt("customerror");
					if (this.customerror == 1)
					{
						customErrorsSection.Mode = CustomErrorsMode.Off;
					}
					else
					{
						customErrorsSection.Mode = CustomErrorsMode.On;
					}
				}
				if (configuration.AppSettings.Settings["sitepath"] != null)
				{
					configuration.AppSettings.Settings["sitepath"].Value = this.mainsite;
				}
				else
				{
					configuration.AppSettings.Settings.Add("sitepath", this.mainsite);
				}
				configuration.Save(ConfigurationSaveMode.Modified);
				WebConfig.ReSet();
				base.AddMsg("更新配置成功！");
			}
		}

		// Token: 0x0400003A RID: 58
		protected SysConfig sysconfiginfo = new SysConfig();

		// Token: 0x0400003B RID: 59
		protected List<SiteConfig> sitelist = new List<SiteConfig>();

		// Token: 0x0400003C RID: 60
		protected int customerror;

		// Token: 0x0400003D RID: 61
		protected string mainsite = "";
	}
}
