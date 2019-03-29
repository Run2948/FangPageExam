using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000E RID: 14
	public class pluginupdate : SuperController
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00003758 File Offset: 0x00001958
		protected override void View()
		{
			if (this.pluname != "")
			{
				this.pluginconfig = FPSerializer.Load<PluginConfig>(FPUtils.GetMapPath(this.webpath + "plugins/" + this.pluname + "/plugin.config"));
				this.pluginconfig.installpath = this.pluname;
			}
			if (this.ispost)
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".plu" && a != ".zip")
				{
					this.ShowErr("对不起，该文件不是方配系统插件更新文件类型。");
				}
				else
				{
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
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\plugin.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
						this.ShowErr("插件配置文件不存在或有错误。");
					}
					else
					{
						PluginConfig pluginConfig = FPSerializer.Load<PluginConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\plugin.config");
						if (pluginConfig.guid == "")
						{
							this.ShowErr("对不起，该插件标识码错误，更新失败。");
						}
						else
						{
							Version v = new Version(FPUtils.StrToDecimal(pluginConfig.version).ToString("0.0"));
							Version v2 = new Version(FPUtils.StrToDecimal(this.pluginconfig.version).ToString("0.0"));
							if (v < v2)
							{
								this.ShowErr("对不起，您更新的版本比安装版本还低，不能更新");
							}
							else
							{
								string mapPath2 = FPUtils.GetMapPath(this.webpath + "plugins/" + pluginConfig.installpath);
								string sourcePath = mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
								pluginupdate.CopyDirectory(sourcePath, mapPath2);
								Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
								base.Response.Redirect("pluginmanage.aspx");
							}
						}
					}
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003A68 File Offset: 0x00001C68
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
				pluginupdate.CopyDirectory(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
			}
		}

		// Token: 0x04000016 RID: 22
		protected string pluname = FPRequest.GetString("pluname");

		// Token: 0x04000017 RID: 23
		protected PluginConfig pluginconfig = new PluginConfig();
	}
}
