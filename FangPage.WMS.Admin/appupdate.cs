using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000032 RID: 50
	public class appupdate : SuperController
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000095B8 File Offset: 0x000077B8
		protected override void Controller()
		{
			if (this.app_path != "")
			{
				this.appinfo = AppConfigs.GetMapAppConfig(this.app_path);
				this.appinfo.installpath = this.app_path;
			}
			if (this.appinfo.guid == "")
			{
				this.ShowErr("对不起，该应用已被删除或不存在。");
				return;
			}
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".fpk" && a != ".zip")
				{
					this.ShowErr("对不起，该文件不是方配系统应用更新文件类型。");
					return;
				}
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				if (File.Exists(mapPath + "\\" + fileName))
				{
					File.Delete(mapPath + "\\" + fileName);
				}
				FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
				if (Directory.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName)))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
				}
				FPZip.UnZip(mapPath + "\\" + fileName, "");
				File.Delete(mapPath + "\\" + fileName);
				if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\app.config"))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					this.ShowErr("更新失败，应用配置文件不存在或有错误。");
					return;
				}
				AppConfig appConfig = FPSerializer.Load<AppConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\app.config");
				if (appConfig.guid != this.appinfo.guid)
				{
					this.ShowErr("更新失败，应用标识码错误。");
					return;
				}
				Version v = FPUtils.StrToVersion(appConfig.version);
				Version v2 = FPUtils.StrToVersion(this.appinfo.version);
				if (v < v2)
				{
					this.ShowErr("对不起，您更新的版本比安装版本还低，没必要更新。");
					return;
				}
				this.appinfo.version = appConfig.version;
				this.appinfo.notes = appConfig.notes;
				this.appinfo.author = appConfig.author;
				this.appinfo.dll = appConfig.dll;
				this.appinfo.adminurl = appConfig.adminurl;
				this.appinfo.indexurl = appConfig.indexurl;
				this.appinfo.homepage = appConfig.homepage;
				this.appinfo.icon = appConfig.icon;
				this.appinfo.notes = appConfig.notes;
				this.appinfo.sortapps = appConfig.sortapps;
				if (this.appinfo.createdate == "")
				{
					this.appinfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				}
				this.appinfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				string mapPath2 = FPFile.GetMapPath(this.apppath + this.appinfo.installpath);
				FPFile.CopyDir(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), mapPath2);
				if (Directory.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName)))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
				}
				FPSerializer.Save<AppConfig>(this.appinfo, FPFile.GetMapPath(this.apppath + this.appinfo.installpath + "/app.config"));
				string[] array = appConfig.sortapps.Split(new char[]
				{
					'§'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = FPArray.SplitString(array[i], "|", 3);
					if (!(array2[0] == "") && !(array2[2] == ""))
					{
						SortAppInfo sortAppInfo = DbHelper.ExecuteModel<SortAppInfo>(new SqlParam[]
						{
							DbHelper.MakeAndWhere("guid", this.appinfo.guid),
							DbHelper.MakeAndWhere("name", array2[0])
						});
						if (sortAppInfo.id > 0)
						{
							sortAppInfo.markup = array2[1];
							sortAppInfo.indexpage = array2[2];
							sortAppInfo.type = "app";
							sortAppInfo.installpath = this.appinfo.installpath;
							DbHelper.ExecuteUpdate<SortAppInfo>(sortAppInfo);
						}
						else
						{
							sortAppInfo.guid = this.appinfo.guid;
							sortAppInfo.name = array2[0];
							sortAppInfo.markup = array2[1];
							sortAppInfo.indexpage = array2[2];
							sortAppInfo.type = "app";
							sortAppInfo.installpath = this.appinfo.installpath;
							DbHelper.ExecuteInsert<SortAppInfo>(sortAppInfo);
						}
					}
				}
				if (Directory.Exists(mapPath2 + "\\bin"))
				{
					FPFile.CopyDir(mapPath2 + "\\bin", FPFile.GetMapPath(this.webpath + "/bin"));
					Directory.Delete(mapPath2 + "\\bin", true);
				}
				if (DbConfigs.DbType == DbType.Access && File.Exists(mapPath2 + "\\datas\\access_update.sql"))
				{
					DbHelper.ExecuteSql(FPFile.ReadFile(mapPath2 + "\\datas\\access_update.sql"));
				}
				else if (DbConfigs.DbType == DbType.SqlServer && File.Exists(mapPath2 + "\\datas\\sqlserver_update.sql"))
				{
					DbHelper.ExecuteSql(FPFile.ReadFile(mapPath2 + "\\datas\\sqlserver_update.sql"));
				}
				FPCache.Remove("FP_APPLIST");
				FPCache.Remove("FP_SORTAPPLIST");
				base.Response.Redirect("appmanage.aspx");
			}
		}

		// Token: 0x0400007F RID: 127
		protected string app_path = FPRequest.GetString("apppath");

		// Token: 0x04000080 RID: 128
		protected AppConfig appinfo = new AppConfig();
	}
}
