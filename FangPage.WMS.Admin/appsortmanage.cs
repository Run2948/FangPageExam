using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003B RID: 59
	public class appsortmanage : SuperController
	{
		// Token: 0x0600008D RID: 141 RVA: 0x0000C040 File Offset: 0x0000A240
		protected override void Controller()
		{
			if (this.app_path != "")
			{
				this.appinfo = AppConfigs.GetMapAppConfig(this.app_path);
			}
			if (this.appinfo.guid == "")
			{
				this.ShowErr("对不起，该应用已被删除或不存在。");
				return;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("guid", this.appinfo.guid);
			if (this.ispost)
			{
				if (this.action == "add")
				{
					SortAppInfo model = FPRequest.GetModel<SortAppInfo>(new SortAppInfo(), "add_");
					model.guid = this.appinfo.guid;
					model.type = "app";
					model.installpath = this.appinfo.installpath;
					if (model.name == "")
					{
						this.ShowErr("栏目应用名称不能为空。");
						return;
					}
					DbHelper.ExecuteInsert<SortAppInfo>(model);
				}
				else if (this.action == "update")
				{
					SortAppInfo sortAppInfo = DbHelper.ExecuteModel<SortAppInfo>(this.id);
					sortAppInfo = FPRequest.GetModel<SortAppInfo>(sortAppInfo, "update_");
					if (sortAppInfo.name == "")
					{
						this.ShowErr("栏目应用名称不能为空。");
						return;
					}
					DbHelper.ExecuteUpdate<SortAppInfo>(sortAppInfo);
				}
				else if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<SortAppInfo>(FPRequest.GetInt("sortappid"));
				}
				this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC, new SqlParam[]
				{
					sqlParam
				});
				string text = "";
				foreach (SortAppInfo sortAppInfo2 in this.sortapplist)
				{
					if (text != "")
					{
						text += "§";
					}
					text = string.Concat(new string[]
					{
						text,
						sortAppInfo2.name,
						"|",
						sortAppInfo2.markup,
						"|",
						sortAppInfo2.indexpage
					});
				}
				this.appinfo.sortapps = text;
				FPSerializer.Save<AppConfig>(this.appinfo, FPFile.GetMapPath(this.webpath + "app/" + this.appinfo.installpath + "/app.config"));
				FPCache.Remove("FP_APPLIST");
				FPCache.Remove("FP_SORTAPPLIST");
				base.Response.Redirect("appsortmanage.aspx?apppath=" + this.app_path);
			}
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x040000A0 RID: 160
		protected string app_path = FPRequest.GetString("apppath");

		// Token: 0x040000A1 RID: 161
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();

		// Token: 0x040000A2 RID: 162
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000A3 RID: 163
		protected AppConfig appinfo = new AppConfig();
	}
}
