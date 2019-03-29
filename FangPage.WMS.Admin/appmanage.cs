using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000023 RID: 35
	public class appmanage : SuperController
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00007374 File Offset: 0x00005574
		protected override void View()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				int @int = FPRequest.GetInt("appid");
				AppInfo appInfo = DbHelper.ExecuteModel<AppInfo>(@int);
				string mapPath = FPUtils.GetMapPath(this.webpath + appInfo.installpath);
				if (this.action == "delete")
				{
					if (DbHelper.ExecuteDelete<AppInfo>(@int) > 0)
					{
						foreach (string text in FPUtils.SplitString(appInfo.files))
						{
							if (text.StartsWith("bin/"))
							{
								if (File.Exists(FPUtils.GetMapPath(WebConfig.WebPath + text)))
								{
									File.Delete(FPUtils.GetMapPath(WebConfig.WebPath + text));
								}
							}
							if (File.Exists(mapPath + "/" + text))
							{
								if (text.EndsWith(".sql"))
								{
									if (text.ToLower().EndsWith("access_un.sql") && DbConfigs.DbType == DbType.Access)
									{
										string sqlstring = FPFile.ReadFile(mapPath + "/" + text);
										DbHelper.ExecuteSql(sqlstring);
									}
									else if (text.ToLower().EndsWith("sqlserver_un.sql") && DbConfigs.DbType == DbType.SqlServer)
									{
										string sqlstring = FPFile.ReadFile(mapPath + "/" + text);
										DbHelper.ExecuteSql(sqlstring);
									}
								}
								File.Delete(mapPath + "/" + text);
							}
						}
						if (Directory.Exists(mapPath))
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
							if (directoryInfo.GetFiles().Length == 0)
							{
								directoryInfo.Delete(true);
							}
						}
						SqlParam sqlParam = DbHelper.MakeAndWhere("appid", @int);
						DbHelper.ExecuteDelete<SortAppInfo>(new SqlParam[]
						{
							sqlParam
						});
					}
					FPCache.Remove("FP_SORTTREE");
					base.Response.Redirect("appmanage.aspx");
				}
			}
			this.applist = DbHelper.ExecuteList<AppInfo>(OrderBy.ASC);
			base.SaveRightURL();
		}

		// Token: 0x04000046 RID: 70
		protected List<AppInfo> applist = new List<AppInfo>();
	}
}
