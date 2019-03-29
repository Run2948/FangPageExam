using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000024 RID: 36
	public class appset : SuperController
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000760C File Offset: 0x0000580C
		protected override void View()
		{
			if (this.id > 0)
			{
				this.appinfo = DbHelper.ExecuteModel<AppInfo>(this.id);
			}
			if (this.ispost)
			{
				this.appinfo.target = "";
				this.appinfo = FPRequest.GetModel<AppInfo>(this.appinfo);
				if (this.appinfo.name == "")
				{
					this.ShowErr("应用名称不能为空");
					return;
				}
				if (this.appinfo.installpath == "")
				{
					this.ShowErr("安装目录不能为空");
					return;
				}
				string pattern = "^[a-zA-Z0-9_\\w]+$";
				if (this.err == 0 && !Regex.IsMatch(this.appinfo.installpath.Trim(), pattern, RegexOptions.IgnoreCase))
				{
					base.AddErr("应用安装目录名称只能由数字、字母或下划线组成。");
				}
				if (this.appinfo.guid == "")
				{
					this.appinfo.guid = Guid.NewGuid().ToString();
				}
				if (this.appinfo.target == "")
				{
					this.appinfo.target = "_self";
				}
				if (this.appinfo.setpath == "")
				{
					this.appinfo.setpath = this.appinfo.installpath;
				}
				this.appinfo.version = FPUtils.StrToDecimal(this.appinfo.version).ToString("0.0");
				SqlParam sqlParam = DbHelper.MakeAndWhere("appid", this.id);
				List<SortAppInfo> list = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC, new SqlParam[]
				{
					sqlParam
				});
				string text = "";
				foreach (SortAppInfo sortAppInfo in list)
				{
					if (text != "")
					{
						text += "|";
					}
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						sortAppInfo.name,
						",",
						sortAppInfo.markup,
						",",
						sortAppInfo.indexpage,
						",",
						sortAppInfo.viewpage
					});
				}
				this.appinfo.sortapps = text;
				if (this.appinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<AppInfo>(this.appinfo);
				}
				else
				{
					this.id = DbHelper.ExecuteInsert<AppInfo>(this.appinfo);
				}
				FPSerializer.Save<AppInfo>(this.appinfo, FPUtils.GetMapPath(this.webpath + this.appinfo.installpath + "/app.config"));
				base.Response.Redirect("appmanage.aspx");
			}
			base.SaveRightURL();
		}

		// Token: 0x04000047 RID: 71
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000048 RID: 72
		protected AppInfo appinfo = new AppInfo();
	}
}
