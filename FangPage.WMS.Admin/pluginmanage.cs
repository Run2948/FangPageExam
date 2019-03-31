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
	// Token: 0x02000019 RID: 25
	public class pluginmanage : SuperController
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00004F64 File Offset: 0x00003164
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("plupath");
				if (!File.Exists(FPFile.GetMapPath(this.plupath + @string + "/plugin.config")))
				{
					this.ShowErr("对不起，该插件已被删除或者不存在。");
					return;
				}
				PluginConfig pluginConfig = FPSerializer.Load<PluginConfig>(FPFile.GetMapPath(this.plupath + @string + "/plugin.config"));
				FPFile.GetMapPath(this.plupath + pluginConfig.installpath);
				if (this.action == "delete")
				{
					SetupBll.DeleteSetup(pluginConfig);
					FPCache.Remove("FP_PLUGINLIST");
					FPCache.Remove("FP_SORTAPPLIST");
					base.Response.Redirect("pluginmanage.aspx");
				}
				else if (this.action == "download")
				{
					SetupBll.DownloadSetup(pluginConfig);
				}
				else if (this.action == "build")
				{
					Version v = FPUtils.StrToVersion(pluginConfig.version);
					Version version = FPUtils.StrToVersion("0.0.0");
					if (pluginConfig.dll != "")
					{
						string[] array = FPArray.SplitString(pluginConfig.dll, 2);
						if (array[1] == "")
						{
							array[1] = array[0].Replace(".Controller", "");
						}
						version = FPUtils.GetVersion(FPFile.GetMapPath(this.webpath + "bin/" + array[1] + ".dll"));
					}
					if (version > v)
					{
						pluginConfig.version = FPUtils.FormatVersion(version.ToString());
					}
					string text = "plugins/" + @string;
					if (Directory.Exists(FPFile.GetMapPath(this.webpath + "sites/" + text)))
					{
						SiteConfig siteConfig = SiteConfigs.LoadSiteConfig("plugins");
						siteConfig.dll = pluginConfig.dll;
						FPViews.CreateSite(siteConfig, text);
					}
					if (pluginConfig.createdate == "")
					{
						pluginConfig.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					}
					if (pluginConfig.updatedate == "")
					{
						pluginConfig.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					}
					FPSerializer.Save<PluginConfig>(pluginConfig, FPFile.GetMapPath(this.webpath + "plugins/" + pluginConfig.installpath + "/plugin.config"));
					FPCache.Remove("FP_PLUGINLIST");
					base.AddMsg("插件编译成功！");
				}
			}
			this.pluginlist = PluginConfigs.GetMapPluList();
			bool flag = false;
			for (int i = 0; i < this.pluginlist.Count; i++)
			{
				Version v2 = FPUtils.StrToVersion(this.pluginlist[i].version);
				Version version2 = FPUtils.StrToVersion("0.0.0");
				if (this.pluginlist[i].dll != "")
				{
					string[] array2 = FPArray.SplitString(this.pluginlist[i].dll, 2);
					if (array2[1] == "")
					{
						array2[1] = array2[0].Replace(".Controller", "");
					}
					version2 = FPUtils.GetVersion(FPFile.GetMapPath(this.webpath + "bin/" + array2[1] + ".dll"));
				}
				if (version2 > v2)
				{
					this.pluginlist[i].version = FPUtils.FormatVersion(version2.ToString());
					this.pluginlist[i].updatedate = DbUtils.GetDateTime().ToString();
					FPSerializer.Save<PluginConfig>(this.pluginlist[i], FPFile.GetMapPath(this.plupath + this.pluginlist[i].installpath + "/plugin.config"));
					flag = true;
				}
			}
			if (flag)
			{
				FPCache.Remove("FP_PLUGINLIST");
			}
		}

		// Token: 0x04000037 RID: 55
		protected List<PluginConfig> pluginlist = new List<PluginConfig>();
	}
}
