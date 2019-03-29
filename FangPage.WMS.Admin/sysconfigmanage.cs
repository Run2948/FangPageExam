using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000014 RID: 20
	public class sysconfigmanage : SuperController
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00004C98 File Offset: 0x00002E98
		protected override void View()
		{
			this.sysconfiginfo = SysConfigs.GetConfig();
			this.sitelist = SiteBll.GetSiteList();
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
			this.adminsiteconfig = SiteConfigs.LoadConfig(FPUtils.GetMapPath(this.webpath + this.sysconfiginfo.adminpath + "/site.config"));
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有限权进行修改配置。");
					return;
				}
				this.sysconfiginfo = FPRequest.GetModel<SysConfig>(this.sysconfiginfo);
				if (this.sysconfiginfo.admintitle == "")
				{
					this.sysconfiginfo.admintitle = this.adminsiteconfig.sitetitle;
				}
				this.sysconfiginfo.passwordkey = WMSUtils.CreateAuthStr(10);
				WMSCookie.WriteCookie("password", DES.Encode(this.user.password, this.sysconfiginfo.passwordkey));
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
						customErrorsSection.Mode = CustomErrorsMode.RemoteOnly;
					}
				}
				if (configuration.AppSettings.Settings["sitepath"] != null)
				{
					configuration.AppSettings.Settings["sitepath"].Value = FPRequest.GetString("mainsite");
				}
				else
				{
					configuration.AppSettings.Settings.Add("sitepath", FPRequest.GetString("mainsite"));
				}
				configuration.Save(ConfigurationSaveMode.Modified);
				WebConfig.ReSet();
				base.AddMsg("更新配置成功！");
			}
			base.SaveRightURL();
		}

		// Token: 0x04000020 RID: 32
		protected SysConfig sysconfiginfo = new SysConfig();

		// Token: 0x04000021 RID: 33
		protected List<SiteConfig> sitelist = new List<SiteConfig>();

		// Token: 0x04000022 RID: 34
		protected SiteConfig adminsiteconfig = new SiteConfig();

		// Token: 0x04000023 RID: 35
		protected int customerror = 0;

		// Token: 0x04000024 RID: 36
		protected string mainsite = "";
	}
}
