using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000011 RID: 17
	public class plugininstall : SuperController
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00004088 File Offset: 0x00002288
		protected override void View()
		{
			if (this.ispost)
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				if (this.step == "step1")
				{
					this.filename = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(this.filename).ToLower();
					if (a != ".zip" && a != ".plu")
					{
						this.ShowErr("该文件不是方配WMS系统插件安装文件类型");
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
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\plugin.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
						this.ShowErr("插件安装配置文件不存在。");
						return;
					}
					this.plugininfo = FPSerializer.Load<PluginConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\plugin.config");
					if (this.plugininfo.guid == "")
					{
						this.ShowErr("对不起，该插件安装标识码不能为空。");
						return;
					}
				}
				else if (this.step == "step2")
				{
					string @string = FPRequest.GetString("installpath");
					if (@string == "")
					{
						this.ShowErr("安装目录不能为空。");
						return;
					}
					this.filename = FPRequest.GetString("filename");
					this.plugininfo = FPSerializer.Load<PluginConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\plugin.config");
					this.plugininfo.installpath = @string;
					string mapPath2 = FPUtils.GetMapPath(this.webpath + "plugins/" + this.plugininfo.installpath);
					string sourcePath = mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename);
					plugininstall.CopyDirectory(sourcePath, mapPath2);
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
					base.Response.Redirect("pluginmanage.aspx");
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000043E8 File Offset: 0x000025E8
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
				plugininstall.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
			}
		}

		// Token: 0x0400001A RID: 26
		protected string step = FPRequest.GetString("step").ToLower();

		// Token: 0x0400001B RID: 27
		protected PluginConfig plugininfo = new PluginConfig();

		// Token: 0x0400001C RID: 28
		protected string filename = "";
	}
}
