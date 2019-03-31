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
	// Token: 0x0200002F RID: 47
	public class appinstall : SuperController
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000878C File Offset: 0x0000698C
		protected override void Controller()
		{
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				if (this.step == "step1")
				{
					this.filename = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(this.filename).ToLower();
					if (a != ".fpk" && a != ".zip")
					{
						this.ShowErr("对不起，该文件不是方配WMS系统应用安装文件类型。");
						return;
					}
					if (!Directory.Exists(mapPath))
					{
						Directory.CreateDirectory(mapPath);
					}
					if (File.Exists(mapPath + "\\" + this.filename))
					{
						File.Delete(mapPath + "\\" + this.filename);
					}
					FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + this.filename);
					if (Directory.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename)))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
					}
					FPZip.UnZip(mapPath + "\\" + this.filename, "");
					File.Delete(mapPath + "\\" + this.filename);
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\app.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
						this.ShowErr("对不起，安装失败，应用安装配置文件不存在。");
						return;
					}
					this.appinfo = FPSerializer.Load<AppConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\app.config");
					if (this.appinfo.guid == "")
					{
						this.ShowErr("对不起，安装失败，应用安装标识码不能为空。");
						return;
					}
					AppConfig appConfig = AppConfigs.GetAppConfig(this.appinfo.guid);
					if (appConfig.installpath != "")
					{
						this.isinstall = 1;
					}
					if (this.isinstall == 1)
					{
						Version v = FPUtils.StrToVersion(this.appinfo.version);
						Version v2 = FPUtils.StrToVersion(appConfig.version);
						if (v < v2)
						{
							this.ShowErr("对不起，您安装的应用版本比已安装的版本还低，没必要安装。");
							return;
						}
						if (v == v2)
						{
							this.ShowErr("对不起，该应用在系统中已安装，没必要重复安装。");
							return;
						}
						this.appinfo.installpath = appConfig.installpath;
						return;
					}
				}
				else if (this.step == "step2")
				{
					string @string = FPRequest.GetString("installpath");
					if (@string == "")
					{
						this.ShowErr("对不起，安装目录不能为空。");
						return;
					}
					if (this.isinstall != 1 && Directory.Exists(FPFile.GetMapPath(this.apppath + @string)))
					{
						this.ShowErr("对不起，该安装目录[" + @string + "]已被其他应用使用，请另选其他目录。");
						return;
					}
					this.filename = FPRequest.GetString("filename");
					this.appinfo = FPSerializer.Load<AppConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\app.config");
					this.appinfo.installpath = @string;
					string mapPath2 = FPFile.GetMapPath(this.apppath + this.appinfo.installpath);
					string text = mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename);
					if (this.isinstall == 1)
					{
						FPFile.ClearDir(mapPath2);
					}
					FPFile.CopyDir(text, mapPath2);
					if (Directory.Exists(text))
					{
						Directory.Delete(text, true);
					}
					string[] array = this.appinfo.sortapps.Split(new char[]
					{
						'§'
					});
					for (int i = 0; i < array.Length; i++)
					{
						string[] array2 = FPArray.SplitString(array[i], "|", 3);
						if (!(array2[0] == "") && !(array2[2] == ""))
						{
							DbHelper.ExecuteInsert<SortAppInfo>(new SortAppInfo
							{
								guid = this.appinfo.guid,
								name = array2[0],
								markup = array2[1],
								indexpage = array2[2],
								type = "app",
								installpath = this.appinfo.installpath
							});
						}
					}
					if (Directory.Exists(mapPath2 + "\\bin"))
					{
						FPFile.CopyDir(mapPath2 + "\\bin", FPFile.GetMapPath(this.webpath + "/bin"));
						Directory.Delete(mapPath2 + "\\bin", true);
					}
					if (DbConfigs.DbType == DbType.Access && File.Exists(mapPath2 + "\\datas\\access_install.sql"))
					{
						DbHelper.ExecuteSql(FPFile.ReadFile(mapPath2 + "\\datas\\access_install.sql"));
					}
					else if (DbConfigs.DbType == DbType.SqlServer && File.Exists(mapPath2 + "\\datas\\sqlserver_install.sql"))
					{
						DbHelper.ExecuteSql(FPFile.ReadFile(mapPath2 + "\\datas\\sqlserver_install.sql"));
					}
					FPCache.Remove("FP_APPLIST");
					FPCache.Remove("FP_SORTAPPLIST");
					base.Response.Redirect("appmanage.aspx");
				}
			}
		}

		// Token: 0x04000077 RID: 119
		protected string step = FPRequest.GetString("step").ToLower();

		// Token: 0x04000078 RID: 120
		protected AppConfig appinfo = new AppConfig();

		// Token: 0x04000079 RID: 121
		protected string filename = "";

		// Token: 0x0400007A RID: 122
		protected int isinstall = FPRequest.GetInt("isinstall");
	}
}
