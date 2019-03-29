using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000022 RID: 34
	public class appinstall : SuperController
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00006B80 File Offset: 0x00004D80
		protected override void View()
		{
			if (this.ispost)
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				if (this.step == "step1")
				{
					this.filename = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(this.filename).ToLower();
					if (a != ".fpk" && a != ".zip")
					{
						this.ShowErr("该文件不是方配站点应用安装文件类型");
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
					FPZip.UnZipFile(mapPath + "\\" + this.filename, "");
					File.Delete(mapPath + "\\" + this.filename);
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\app.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
						this.ShowErr("应用安装配置文件不存在。");
						return;
					}
					this.appinfo = FPSerializer.Load<AppInfo>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\app.config");
					if (this.appinfo.guid == "")
					{
						this.ShowErr("对不起，该应用安装标识码不能为空。");
						return;
					}
					SqlParam sqlParam = DbHelper.MakeAndWhere("guid", this.appinfo.guid);
					if (DbHelper.ExecuteCount<AppInfo>(new SqlParam[]
					{
						sqlParam
					}) > 0)
					{
						this.ShowErr("对不起，该应用已安装，不能重复安装。");
						return;
					}
				}
				else if (this.step == "step2")
				{
					this.filename = FPRequest.GetString("filename");
					this.appinfo = FPSerializer.Load<AppInfo>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\app.config");
					if (this.appinfo.name == "")
					{
						this.appinfo.name = "无应用名称";
					}
					if (FPRequest.GetString("installpath") == "")
					{
						this.ShowErr("对不起，安装目录不能为空");
						return;
					}
					this.appinfo.installpath = FPRequest.GetString("installpath");
					if (!Directory.Exists(FPUtils.GetMapPath(this.webpath + this.appinfo.installpath)))
					{
						Directory.CreateDirectory(FPUtils.GetMapPath(this.webpath + this.appinfo.installpath));
					}
					string mapPath2 = FPUtils.GetMapPath(this.webpath + this.appinfo.installpath);
					string sourcePath = mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename);
					this.appinfo.files = this.CopyDirectory(sourcePath, mapPath2, "");
					this.appinfo.id = DbHelper.ExecuteInsert<AppInfo>(this.appinfo);
					if (this.appinfo.id > 0)
					{
						foreach (string strContent in this.appinfo.sortapps.Split(new char[]
						{
							'|'
						}))
						{
							string[] array2 = FPUtils.SplitString(strContent, ",", 4);
							if (!(array2[0] == ""))
							{
								DbHelper.ExecuteInsert<SortAppInfo>(new SortAppInfo
								{
									appid = this.appinfo.id,
									name = array2[0],
									markup = array2[1],
									indexpage = array2[2],
									viewpage = array2[3],
									installpath = this.appinfo.installpath
								});
							}
						}
					}
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
					CacheBll.RemoveSortCache();
					base.Response.Redirect("appmanage.aspx");
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000070C8 File Offset: 0x000052C8
		private string CopyDirectory(string sourcePath, string targetPath, string vpath)
		{
			string text = "";
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (text != "")
				{
					text += ",";
				}
				if (fileInfo.Extension == ".dll")
				{
					fileInfo.CopyTo(FPUtils.GetMapPath(WebConfig.WebPath + "bin/" + fileInfo.Name), true);
					text = text + "bin/" + fileInfo.Name;
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
					text = text + vpath + fileInfo.Name;
				}
				else
				{
					fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
					text = text + vpath + fileInfo.Name;
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string text2 = this.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name, vpath + directoryInfo2.Name + "/");
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

		// Token: 0x04000043 RID: 67
		protected string step = FPRequest.GetString("step").ToLower();

		// Token: 0x04000044 RID: 68
		protected AppInfo appinfo = new AppInfo();

		// Token: 0x04000045 RID: 69
		protected string filename = "";
	}
}
