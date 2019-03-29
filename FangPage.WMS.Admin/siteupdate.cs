using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000013 RID: 19
	public class siteupdate : SuperController
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000047AC File Offset: 0x000029AC
		protected override void View()
		{
			string mapPath = FPUtils.GetMapPath(this.webpath + "sites/" + this.m_sitepath);
			if (!File.Exists(mapPath + "\\site.config"))
			{
				this.ShowErr("该站点不存在或已被删除。");
			}
			else
			{
				this.m_siteconfig = SiteConfigs.LoadConfig(mapPath + "\\site.config");
				if (this.ispost)
				{
					string mapPath2 = FPUtils.GetMapPath(this.webpath + "cache");
					string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(fileName).ToLower();
					if (a != ".fpsite" && a != ".zip")
					{
						this.ShowErr("对不起，该文件不是方配站点更新文件类型。");
						return;
					}
					if (!Directory.Exists(mapPath2))
					{
						Directory.CreateDirectory(mapPath2);
					}
					if (File.Exists(mapPath2 + "\\" + fileName))
					{
						File.Delete(mapPath2 + "\\" + fileName);
					}
					FPRequest.Files["uploadfile"].SaveAs(mapPath2 + "\\" + fileName);
					if (Directory.Exists(mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName)))
					{
						Directory.Delete(mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					}
					FPZip.UnZipFile(mapPath2 + "\\" + fileName, "");
					File.Delete(mapPath2 + "\\" + fileName);
					if (!File.Exists(mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\site.config"))
					{
						Directory.Delete(mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
						this.ShowErr("站点配置文件不存在或有错误。");
						return;
					}
					SiteConfig siteConfig = SiteConfigs.LoadConfig(mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\site.config");
					Version v = new Version(FPUtils.StrToDecimal(this.m_siteconfig.version).ToString("0.0"));
					Version v2 = new Version(FPUtils.StrToDecimal(siteConfig.version).ToString("0.0"));
					if (v > v2)
					{
						this.ShowErr("对不起，您更新的版本比安装版本还低，不能更新");
						return;
					}
					string mapPath3 = FPUtils.GetMapPath(this.webpath + "sites/" + this.siteconfig.sitepath);
					string sourcePath = mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName);
					siteupdate.CopyDirectory(sourcePath, mapPath3);
					Directory.Delete(mapPath2 + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					base.Response.Redirect("sitemanage.aspx");
				}
				base.SaveRightURL();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public static void CopyDirectory(string sourcePath, string targetPath)
		{
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
					fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
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
				}
				else
				{
					fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				siteupdate.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
			}
		}

		// Token: 0x0400001E RID: 30
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x0400001F RID: 31
		protected SiteConfig m_siteconfig = new SiteConfig();
	}
}
