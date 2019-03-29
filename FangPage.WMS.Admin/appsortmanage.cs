using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002E RID: 46
	public class appsortmanage : SuperController
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000A4AC File Offset: 0x000086AC
		protected override void View()
		{
			if (this.id > 0)
			{
				this.appinfo = DbHelper.ExecuteModel<AppInfo>(this.id);
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("appid", this.id);
			if (this.ispost)
			{
				if (this.action == "appupdate")
				{
					SortAppInfo sortAppInfo = DbHelper.ExecuteModel<SortAppInfo>(this.sortappid);
					sortAppInfo = FPRequest.GetModel<SortAppInfo>(sortAppInfo, "sort_");
					if (sortAppInfo.name == "")
					{
						this.ShowErr("栏目应用名称不能为空。");
						return;
					}
					DbHelper.ExecuteUpdate<SortAppInfo>(sortAppInfo);
				}
				else if (this.action == "appadd")
				{
					SortAppInfo sortAppInfo = FPRequest.GetModel<SortAppInfo>(new SortAppInfo(), "sortadd_");
					sortAppInfo.appid = this.id;
					sortAppInfo.installpath = this.appinfo.installpath;
					if (sortAppInfo.name == "")
					{
						this.ShowErr("栏目应用名称不能为空。");
						return;
					}
					DbHelper.ExecuteInsert<SortAppInfo>(sortAppInfo);
				}
				else if (this.action == "appdelete")
				{
					DbHelper.ExecuteDelete<SortAppInfo>(FPRequest.GetInt("appid"));
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
						text += "|";
					}
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						sortAppInfo2.name,
						",",
						sortAppInfo2.markup,
						",",
						sortAppInfo2.indexpage,
						",",
						sortAppInfo2.viewpage
					});
				}
				this.appinfo.sortapps = text;
				FPSerializer.Save<AppInfo>(this.appinfo, FPUtils.GetMapPath(this.webpath + this.appinfo.installpath + "/app.config"));
				CacheBll.RemoveSortCache();
				base.Response.Redirect("appsortmanage.aspx?id=" + this.id);
			}
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x04000068 RID: 104
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000069 RID: 105
		protected int sortappid = FPRequest.GetInt("sortappid");

		// Token: 0x0400006A RID: 106
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();

		// Token: 0x0400006B RID: 107
		protected AppInfo appinfo = new AppInfo();
	}
}
