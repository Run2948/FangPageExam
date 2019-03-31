using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000035 RID: 53
	public class siteadd : SuperController
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00009E2C File Offset: 0x0000802C
		protected override void Controller()
		{
			if (this.site_path != "")
			{
				if (!File.Exists(FPFile.GetMapPath(this.webpath + "sites/" + this.site_path + "/site.config")))
				{
					this.ShowErr("对不起，该站点已被删除或不存在。");
					return;
				}
				this.siteinfo = SiteConfigs.GetMapSiteConfig(this.site_path);
			}
			if (this.ispost)
			{
				string text = FPRequest.GetString("dirpath").ToLower();
				if (text.Trim() == "")
				{
					base.AddErr("站点目录不能为空。");
					return;
				}
				string pattern = "^[a-zA-Z0-9_\\w]+$";
				if (this.err == 0 && !Regex.IsMatch(text.Trim(), pattern, RegexOptions.IgnoreCase))
				{
					base.AddErr("站点目录只能由数字、字母或下划线组成，并且首字不能为数字。");
					return;
				}
				if (FPArray.InArray(text, "bin,app,cache,config,datas,plugins,sites,upload,common") >= 0)
				{
					base.AddErr("对不起，目录[" + text + "]是系统固定目录，不能做为站点目录使用。");
					return;
				}
				this.siteinfo.roles = "";
				this.siteinfo = FPRequest.GetModel<SiteConfig>(this.siteinfo);
				if (this.siteinfo.sitepath != text)
				{
					if (Directory.Exists(FPFile.GetMapPath(this.webpath + "sites/" + text)))
					{
						this.ShowErr("该站点路径已存在，请使用其他的目录名称");
						return;
					}
					this.siteinfo.sitepath = text;
				}
				if (this.site_path == "")
				{
					if (File.Exists(FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath + "/site.config")))
					{
						SiteConfig siteConfig = FPSerializer.Load<SiteConfig>(FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath + "/site.config"));
						this.siteinfo = siteConfig;
					}
					if (FPRequest.GetString("name") != "")
					{
						this.siteinfo.name = FPRequest.GetString("name");
					}
				}
				if (this.siteinfo.name == "")
				{
					this.ShowErr("站点名称不能为空。");
					return;
				}
				if (this.siteinfo.guid == "")
				{
					this.siteinfo.guid = Guid.NewGuid().ToString();
				}
				if (this.siteinfo.sitetitle == "")
				{
					this.siteinfo.sitetitle = this.siteinfo.name;
				}
				if (this.siteinfo.version.ToLower().StartsWith("v"))
				{
					this.siteinfo.version = this.siteinfo.version.Substring(1, this.siteinfo.version.Length - 1);
				}
				this.siteinfo.version = FPUtils.FormatVersion(this.siteinfo.version);
				if (this.site_path != "" && this.siteinfo.markup == "sites_" + this.site_path)
				{
					this.siteinfo.markup = "sites_" + this.siteinfo.sitepath;
				}
				if (this.siteinfo.createdate == "")
				{
					this.siteinfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				}
				this.siteinfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				if (this.site_path != "" && this.siteinfo.sitepath != this.site_path)
				{
					Directory.Move(FPFile.GetMapPath(this.webpath + "sites/" + this.site_path), FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath));
				}
				SiteConfigs.SaveSiteConfig(this.siteinfo);
				if (!Directory.Exists(FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath)))
				{
					Directory.CreateDirectory(FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath));
				}
				FPSerializer.Save<SiteConfig>(this.siteinfo, FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath + "/site.config"));
				FPCache.Remove("FP_SITELIST");
				if (this.siteinfo.sitepath == WebConfig.SitePath)
				{
					SysConfig config = SysConfigs.GetConfig();
					config.platname = this.siteinfo.sitetitle;
					SysConfigs.SaveConfig(config);
					SysConfigs.ResetConfig();
				}
				if (this.tab == 0)
				{
					base.Response.Redirect("sitemanage.aspx");
				}
				else
				{
					base.Response.Redirect(string.Concat(new object[]
					{
						"siteadd.aspx?sitepath=",
						this.siteinfo.sitepath,
						"&tab=",
						this.tab
					}));
				}
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.NotIn, "2,3,4");
			this.rolelist = DbHelper.ExecuteList<RoleInfo>(new SqlParam[]
			{
				sqlParam
			});
			this.desktoplist = DesktopBll.GetDesktopList(0);
		}

		// Token: 0x04000085 RID: 133
		protected string site_path = FPRequest.GetString("sitepath");

		// Token: 0x04000086 RID: 134
		protected new SiteConfig siteinfo = new SiteConfig();

		// Token: 0x04000087 RID: 135
		protected int tab = FPRequest.GetInt("tab");

		// Token: 0x04000088 RID: 136
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x04000089 RID: 137
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
