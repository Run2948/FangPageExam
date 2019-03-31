using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000E RID: 14
	public class pluginadd : SuperController
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000039B4 File Offset: 0x00001BB4
		protected override void Controller()
		{
			if (this.plu_path != "")
			{
				if (!File.Exists(FPFile.GetMapPath(this.plupath + this.plu_path + "/plugin.config")))
				{
					this.ShowErr("对不起，该插件已被删除或不存在。");
					return;
				}
				this.pluginconfig = FPSerializer.Load<PluginConfig>(FPFile.GetMapPath(this.plupath + this.plu_path + "/plugin.config"));
				this.pluginconfig.installpath = this.plu_path;
				this.pluginconfig.notes = HttpUtility.HtmlEncode(this.pluginconfig.notes);
			}
			if (this.ispost)
			{
				string installpath = this.pluginconfig.installpath;
				this.pluginconfig = FPRequest.GetModel<PluginConfig>(this.pluginconfig);
				if (this.pluginconfig.name == "")
				{
					this.ShowErr("插件名称不能为空。");
					return;
				}
				if (this.pluginconfig.installpath == "")
				{
					this.ShowErr("插件目录不能为空。");
					return;
				}
				string pattern = "^[a-zA-Z0-9_\\w]+$";
				if (!Regex.IsMatch(this.pluginconfig.installpath.Trim(), pattern, RegexOptions.IgnoreCase))
				{
					this.ShowErr("插件安装目录名称只能由数字、字母或下划线组成。");
					return;
				}
				if (this.plu_path == "" && Directory.Exists(FPFile.GetMapPath(this.plupath + this.pluginconfig.installpath)))
				{
					this.ShowErr("对不起，该目录已经被使用。");
					return;
				}
				if (this.plu_path != "" && installpath != this.pluginconfig.installpath && Directory.Exists(FPFile.GetMapPath(this.plupath + this.pluginconfig.installpath)))
				{
					this.ShowErr("对不起，该目录已经被使用。");
					return;
				}
				if (this.plu_path != "" && installpath != this.pluginconfig.installpath)
				{
					new DirectoryInfo(FPFile.GetMapPath(this.plupath + installpath)).MoveTo(FPFile.GetMapPath(this.plupath + this.pluginconfig.installpath));
				}
				if (this.pluginconfig.guid == "")
				{
					this.pluginconfig.guid = Guid.NewGuid().ToString();
				}
				if (this.pluginconfig.version.ToLower().StartsWith("v"))
				{
					this.pluginconfig.version = this.pluginconfig.version.Substring(1, this.pluginconfig.version.Length - 1);
				}
				this.pluginconfig.version = FPUtils.FormatVersion(this.pluginconfig.version);
				if (this.pluginconfig.createdate == "")
				{
					this.pluginconfig.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				}
				this.pluginconfig.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				FPSerializer.Save<PluginConfig>(this.pluginconfig, FPFile.GetMapPath(this.plupath + this.pluginconfig.installpath + "/plugin.config"));
				FPCache.Remove("FP_PLUGINLIST");
				base.Response.Redirect("pluginmanage.aspx");
			}
			this.desktoplist = DesktopBll.GetDesktopList(0);
		}

		// Token: 0x04000021 RID: 33
		protected string plu_path = FPRequest.GetString("plupath");

		// Token: 0x04000022 RID: 34
		protected PluginConfig pluginconfig = new PluginConfig();

		// Token: 0x04000023 RID: 35
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
