using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002B RID: 43
	public class siteinstall : SuperController
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000093E8 File Offset: 0x000075E8
		protected override void View()
		{
			if (this.ispost)
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				if (this.step == "step1")
				{
					this.filename = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(this.filename).ToLower();
					if (a != ".zip" && a != ".fpsite")
					{
						this.ShowErr("该文件不是方配站点安装文件类型");
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
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\site.config"))
					{
						string mapPath2 = FPUtils.GetMapPath(this.webpath + "sites");
						string sourcePath = mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename);
						siteinstall.CopyDirectory(sourcePath, mapPath2);
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
						base.Response.Redirect("sitemanage.aspx");
					}
					else
					{
						this.siteinfo = SiteConfigs.LoadConfig(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\site.config");
					}
				}
				else if (this.step == "step2")
				{
					string text = FPRequest.GetString("installpath").ToLower();
					if (text == "")
					{
						this.ShowErr("安装目录不能为空。");
						return;
					}
					this.filename = FPRequest.GetString("filename");
					this.siteinfo = SiteConfigs.LoadConfig(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\site.config");
					this.siteinfo.sitepath = text;
					string mapPath2 = FPUtils.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath);
					string sourcePath2 = mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename);
					siteinstall.CopyDirectory(sourcePath2, mapPath2);
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
					base.Response.Redirect("sitemanage.aspx");
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000975C File Offset: 0x0000795C
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
				siteinstall.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
			}
		}

		// Token: 0x0400005B RID: 91
		protected string step = FPRequest.GetString("step").ToLower();

		// Token: 0x0400005C RID: 92
		protected SiteConfig siteinfo = new SiteConfig();

		// Token: 0x0400005D RID: 93
		protected string filename = "";
	}
}
