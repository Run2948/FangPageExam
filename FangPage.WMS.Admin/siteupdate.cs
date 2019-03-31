using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001A RID: 26
	public class siteupdate : SuperController
	{
		// Token: 0x06000039 RID: 57 RVA: 0x0000534C File Offset: 0x0000354C
		protected override void Controller()
		{
			this.siteinfo = SiteConfigs.GetMapSiteConfig(this.site_path);
			if (this.siteinfo.guid == "")
			{
				this.ShowErr("对不起，该站点不存在或已被删除。");
				return;
			}
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".site" && a != ".zip")
				{
					this.ShowErr("对不起，该文件不是方配站点更新文件类型。");
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
				if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\site.config"))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					this.ShowErr("站点配置文件不存在或有错误。");
					return;
				}
				SiteConfig siteConfig = FPSerializer.Load<SiteConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\site.config");
				if (siteConfig.guid != this.siteinfo.guid)
				{
					this.ShowErr("更新失败，站点标识码错误。");
					return;
				}
				Version v = FPUtils.StrToVersion(this.siteinfo.version);
				Version v2 = FPUtils.StrToVersion(siteConfig.version);
				if (v > v2)
				{
					this.ShowErr("对不起，您更新的版本比安装版本还低，没有必要更新。");
					return;
				}
				this.siteinfo.version = siteConfig.version;
				this.siteinfo.notes = siteConfig.notes;
				this.siteinfo.author = siteConfig.author;
				this.siteinfo.import = siteConfig.import;
				this.siteinfo.dll = siteConfig.dll;
				this.siteinfo.urltype = siteConfig.urltype;
				this.siteinfo.createdate = siteConfig.createdate;
				if (this.siteinfo.createdate == "")
				{
					this.siteinfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				}
				this.siteinfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				string mapPath2 = FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath);
				string text = mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
				FPFile.CopyDir(text, mapPath2);
				Directory.Delete(text, true);
				SiteConfigs.SaveSiteConfig(this.siteinfo);
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
				FPCache.Remove("FP_SETUPCONFIG");
				FPCache.Remove("FP_SITELIST");
				FPViews.CreateSite(this.siteinfo);
				base.Response.Redirect("sitemanage.aspx");
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000574C File Offset: 0x0000394C
		private void CopyDirectory(string sourcePath, string sitePath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (fileInfo.Extension.ToLower() == ".sql")
				{
					if (!Directory.Exists(FPFile.GetMapPath(this.webpath + "sites/" + sitePath)))
					{
						Directory.CreateDirectory(FPFile.GetMapPath(this.webpath + "sites/" + sitePath));
					}
					fileInfo.CopyTo(FPFile.GetMapPath(string.Concat(new string[]
					{
						this.webpath,
						"sites/",
						sitePath,
						"/",
						fileInfo.Name
					})), true);
					if (fileInfo.Name.ToLower().EndsWith("access_install.sql") && DbConfigs.DbType == DbType.Access)
					{
						DbHelper.ExecuteSql(FPFile.ReadFile(fileInfo.FullName));
					}
					else if (fileInfo.Name.ToLower().EndsWith("sqlserver_install.sql") && DbConfigs.DbType == DbType.SqlServer)
					{
						DbHelper.ExecuteSql(FPFile.ReadFile(fileInfo.FullName));
					}
				}
				else if (fileInfo.Extension.ToLower() == ".dll")
				{
					if (!Directory.Exists(FPFile.GetMapPath(this.webpath + sitePath)))
					{
						Directory.CreateDirectory(FPFile.GetMapPath(this.webpath + sitePath));
					}
					fileInfo.CopyTo(FPFile.GetMapPath(this.webpath + sitePath + "/" + fileInfo.Name), true);
				}
				else
				{
					if (!Directory.Exists(FPFile.GetMapPath(this.webpath + "sites/" + sitePath)))
					{
						Directory.CreateDirectory(FPFile.GetMapPath(this.webpath + "sites/" + sitePath));
					}
					fileInfo.CopyTo(FPFile.GetMapPath(string.Concat(new string[]
					{
						this.webpath,
						"sites/",
						sitePath,
						"/",
						fileInfo.Name
					})), true);
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				this.CopyDirectory(directoryInfo2.FullName, sitePath + "/" + directoryInfo2.Name);
			}
		}

		// Token: 0x04000038 RID: 56
		protected string site_path = FPRequest.GetString("sitepath");

		// Token: 0x04000039 RID: 57
		protected new SiteConfig siteinfo = new SiteConfig();
	}
}
