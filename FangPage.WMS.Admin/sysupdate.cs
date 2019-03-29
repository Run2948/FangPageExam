using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001B RID: 27
	public class sysupdate : SuperController
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000600C File Offset: 0x0000420C
		protected override void View()
		{
			if (this.ispost)
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".zip")
				{
					this.ShowErr("对不起，该文件不是系统更新文件类型。");
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
				string mapPath2 = FPUtils.GetMapPath(this.webpath);
				string sourcePath = mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
				sysupdate.CopyDirectory(sourcePath, mapPath2);
				Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
				base.Response.Redirect("sysupdate.aspx");
			}
			base.SaveRightURL();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000061B0 File Offset: 0x000043B0
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
				}
				else if (fileInfo.Extension == ".sql")
				{
					if (fileInfo.Name.ToLower().EndsWith("_access.sql") && DbConfigs.DbType == DbType.Access)
					{
						string sqlstring = FPFile.ReadFile(fileInfo.FullName);
						DbHelper.ExecuteSql(sqlstring);
					}
					else if (fileInfo.Name.ToLower().EndsWith("_sqlserver.sql") && DbConfigs.DbType == DbType.SqlServer)
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
				sysupdate.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
			}
		}

		// Token: 0x04000035 RID: 53
		protected string sysversion = "4.7";
	}
}
