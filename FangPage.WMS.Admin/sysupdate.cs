using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000022 RID: 34
	public class sysupdate : SuperController
	{
		// Token: 0x06000051 RID: 81 RVA: 0x0000706C File Offset: 0x0000526C
		protected override void Controller()
		{
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache/upgrade");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				if (Path.GetExtension(fileName).ToLower() != ".zip")
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
				FPZip.UnZip(mapPath + "\\" + fileName, "");
				File.Delete(mapPath + "\\" + fileName);
				string mapPath2 = FPFile.GetMapPath(this.webpath);
				sysupdate.CopyDirectory(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), mapPath2);
				Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
				base.Response.Redirect("sysupdate.aspx");
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000071CC File Offset: 0x000053CC
		public static void CopyDirectory(string sourcePath, string targetPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (fileInfo.Extension == ".sql")
				{
					if (fileInfo.Name.ToLower().EndsWith("_access.sql") && DbConfigs.DbType == DbType.Access)
					{
						DbHelper.ExecuteSql(FPFile.ReadFile(fileInfo.FullName));
					}
					else if (fileInfo.Name.ToLower().EndsWith("_sqlserver.sql") && DbConfigs.DbType == DbType.SqlServer)
					{
						DbHelper.ExecuteSql(FPFile.ReadFile(fileInfo.FullName));
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
	}
}
