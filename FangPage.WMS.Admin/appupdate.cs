using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000025 RID: 37
	public class appupdate : SuperController
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00007994 File Offset: 0x00005B94
		protected override void View()
		{
			this.appinfo = DbHelper.ExecuteModel<AppInfo>(this.appid);
			if (this.appinfo.id == 0)
			{
				this.ShowErr("该应用不存在或已被删除。");
			}
			else
			{
				if (this.ispost)
				{
					string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
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
					FPZip.UnZipFile(mapPath + "\\" + fileName, "");
					File.Delete(mapPath + "\\" + fileName);
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\app.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
						this.ShowErr("应用配置文件不存在或有错误。");
						return;
					}
					AppInfo appInfo = FPSerializer.Load<AppInfo>(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\app.config");
					if (this.appinfo.guid == "")
					{
						this.ShowErr("对不起，该应用标识码错误，更新失败。");
						return;
					}
					SqlParam sqlParam = DbHelper.MakeAndWhere("guid", this.appinfo.guid);
					this.appinfo = DbHelper.ExecuteModel<AppInfo>(new SqlParam[]
					{
						sqlParam
					});
					if (this.appinfo.id == 0)
					{
						this.ShowErr("对不起，该应用不存在或已被删除。");
						return;
					}
					Version v = new Version(FPUtils.StrToDecimal(appInfo.version).ToString("0.0"));
					Version v2 = new Version(FPUtils.StrToDecimal(this.appinfo.version).ToString("0.0"));
					if (v < v2)
					{
						this.ShowErr("对不起，您更新的版本比安装版本还低，不能更新");
						return;
					}
					this.appinfo.name = appInfo.name;
					this.appinfo.version = appInfo.version;
					this.appinfo.notes = appInfo.notes;
					this.appinfo.author = appInfo.author;
					string mapPath2 = FPUtils.GetMapPath(this.webpath + this.appinfo.installpath);
					string sourcePath = mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
					this.appinfo.files = appupdate.CopyDirectory(sourcePath, mapPath2, "", this.appinfo.files);
					if (DbHelper.ExecuteUpdate<AppInfo>(this.appinfo) > 0)
					{
						foreach (string strContent in appInfo.sortapps.Split(new char[]
						{
							'|'
						}))
						{
							string[] array2 = FPUtils.SplitString(strContent, ",", 4);
							if (!(array2[0] == ""))
							{
								SqlParam[] sqlparams = new SqlParam[]
								{
									DbHelper.MakeAndWhere("appid", this.appinfo.id),
									DbHelper.MakeAndWhere("name", array2[0])
								};
								SortAppInfo sortAppInfo = DbHelper.ExecuteModel<SortAppInfo>(sqlparams);
								if (sortAppInfo.id > 0)
								{
									sortAppInfo.name = array2[0];
									sortAppInfo.markup = array2[1];
									sortAppInfo.indexpage = array2[2];
									sortAppInfo.viewpage = array2[3];
									DbHelper.ExecuteUpdate<SortAppInfo>(sortAppInfo);
								}
								else
								{
									sortAppInfo.appid = this.appinfo.id;
									sortAppInfo.name = array2[0];
									sortAppInfo.markup = array2[1];
									sortAppInfo.indexpage = array2[2];
									sortAppInfo.viewpage = array2[3];
									DbHelper.ExecuteInsert<SortAppInfo>(sortAppInfo);
								}
							}
						}
					}
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					CacheBll.RemoveSortCache();
					base.Response.Redirect("appmanage.aspx");
				}
				base.SaveRightURL();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00007EC8 File Offset: 0x000060C8
		public static string CopyDirectory(string sourcePath, string targetPath, string vpath, string appfiles)
		{
			string text = "";
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (fileInfo.Extension == ".dll")
				{
					fileInfo.CopyTo(FPUtils.GetMapPath(WebConfig.WebPath + "bin/" + fileInfo.Name), true);
					if (!FPUtils.InArray("bin/" + fileInfo.Name, appfiles))
					{
						if (text != "")
						{
							text += ",";
						}
						text = text + "bin/" + fileInfo.Name;
					}
				}
				else if (fileInfo.Extension == ".sql")
				{
					fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
					if (fileInfo.Name.ToLower().EndsWith("access.sql") && DbConfigs.DbType == DbType.Access)
					{
						string sqlstring = FPFile.ReadFile(fileInfo.FullName);
						DbHelper.ExecuteSql(sqlstring);
					}
					else if (fileInfo.Name.ToLower().EndsWith("sqlserver.sql") && DbConfigs.DbType == DbType.SqlServer)
					{
						string sqlstring = FPFile.ReadFile(fileInfo.FullName);
						DbHelper.ExecuteSql(sqlstring);
					}
					if (!FPUtils.InArray("bin/" + fileInfo.Name, appfiles))
					{
						if (text != "")
						{
							text += ",";
						}
						text = text + "bin/" + fileInfo.Name;
					}
				}
				else
				{
					if (!FPUtils.InArray("bin/" + fileInfo.Name, appfiles))
					{
						if (text != "")
						{
							text += ",";
						}
						text = text + "bin/" + fileInfo.Name;
					}
					fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string text2 = appupdate.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name, vpath + directoryInfo2.Name + "/", appfiles);
				if (text2 != "")
				{
					if (text != "")
					{
						text += ",";
					}
					text += text2;
				}
			}
			return text;
		}

		// Token: 0x04000049 RID: 73
		protected int appid = FPRequest.GetInt("appid");

		// Token: 0x0400004A RID: 74
		protected AppInfo appinfo = new AppInfo();
	}
}
