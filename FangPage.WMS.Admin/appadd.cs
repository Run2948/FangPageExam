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
	// Token: 0x02000031 RID: 49
	public class appadd : SuperController
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000910C File Offset: 0x0000730C
		protected override void Controller()
		{
			if (this.app_path != "")
			{
				if (!File.Exists(FPFile.GetMapPath(this.apppath + this.app_path + "/app.config")))
				{
					this.ShowErr("对不起，该应用已被删除或不存在。");
					return;
				}
				this.appinfo = FPSerializer.Load<AppConfig>(FPFile.GetMapPath(this.apppath + this.app_path + "/app.config"));
				this.appinfo.installpath = this.app_path;
				this.appinfo.notes = HttpUtility.HtmlEncode(this.appinfo.notes);
			}
			if (this.ispost)
			{
				string installpath = this.appinfo.installpath;
				this.appinfo = FPRequest.GetModel<AppConfig>(this.appinfo);
				if (this.appinfo.name == "")
				{
					this.ShowErr("应用名称不能为空。");
					return;
				}
				if (this.appinfo.installpath == "")
				{
					this.ShowErr("应用目录不能为空。");
					return;
				}
				string pattern = "^[a-zA-Z0-9_\\w]+$";
				if (!Regex.IsMatch(this.appinfo.installpath.Trim(), pattern, RegexOptions.IgnoreCase))
				{
					this.ShowErr("应用安装目录名称只能由数字、字母或下划线组成。");
					return;
				}
				if (this.app_path == "" && Directory.Exists(FPFile.GetMapPath(this.apppath + this.appinfo.installpath)))
				{
					this.ShowErr("对不起，该目录已经被使用。");
					return;
				}
				if (this.app_path != "" && installpath != this.appinfo.installpath && Directory.Exists(FPFile.GetMapPath(this.apppath + this.appinfo.installpath)))
				{
					this.ShowErr("对不起，该目录已经被使用。");
					return;
				}
				if (this.app_path != "" && installpath != this.appinfo.installpath)
				{
					new DirectoryInfo(FPFile.GetMapPath(this.apppath + installpath)).MoveTo(FPFile.GetMapPath(this.apppath + this.appinfo.installpath));
				}
				if (this.appinfo.guid == "")
				{
					this.appinfo.guid = Guid.NewGuid().ToString();
				}
				if (this.appinfo.version.ToLower().StartsWith("v"))
				{
					this.appinfo.version = this.appinfo.version.Substring(1, this.appinfo.version.Length - 1);
				}
				this.appinfo.version = FPUtils.FormatVersion(this.appinfo.version);
				SqlParam sqlParam = DbHelper.MakeAndWhere("guid", this.appinfo.guid);
				List<SortAppInfo> list = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC, new SqlParam[]
				{
					sqlParam
				});
				string text = "";
				foreach (SortAppInfo sortAppInfo in list)
				{
					if (text != "")
					{
						text += "§";
					}
					text = string.Concat(new string[]
					{
						text,
						sortAppInfo.name,
						"|",
						sortAppInfo.markup,
						"|",
						sortAppInfo.indexpage
					});
				}
				this.appinfo.sortapps = text;
				DbHelper.ExecuteUpdate<SortAppInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("installpath", this.appinfo.installpath),
					DbHelper.MakeAndWhere("guid", this.appinfo.guid)
				});
				if (this.appinfo.createdate == "")
				{
					this.appinfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				}
				this.appinfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				FPSerializer.Save<AppConfig>(this.appinfo, FPFile.GetMapPath(this.apppath + this.appinfo.installpath + "/app.config"));
				FPCache.Remove("FP_SORTAPPLIST");
				FPCache.Remove("FP_APPLIST");
				base.Response.Redirect("appmanage.aspx");
			}
			this.desktoplist = DesktopBll.GetDesktopList(0);
		}

		// Token: 0x0400007C RID: 124
		protected string app_path = FPRequest.GetString("apppath");

		// Token: 0x0400007D RID: 125
		protected AppConfig appinfo = new AppConfig();

		// Token: 0x0400007E RID: 126
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();
	}
}
