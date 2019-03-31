using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000F RID: 15
	public class pluginupdate : SuperController
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00003D48 File Offset: 0x00001F48
		protected override void Controller()
		{
			this.plugininfo = PluginConfigs.GetMapPluConfig(this.plu_path);
			if (this.plugininfo.guid == "")
			{
				this.ShowErr("对不起，该插件已被删除或不存在。");
				return;
			}
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".plu" && a != ".zip")
				{
					this.ShowErr("对不起，该文件不是方配系统插件更新文件类型。");
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
				if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\plugin.config"))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					this.ShowErr("插件配置文件不存在或有错误。");
					return;
				}
				PluginConfig pluginConfig = FPSerializer.Load<PluginConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\plugin.config");
				if (pluginConfig.guid != this.plugininfo.guid)
				{
					this.ShowErr("更新失败，插件标识码错误。");
					return;
				}
				Version v = FPUtils.StrToVersion(pluginConfig.version);
				Version v2 = FPUtils.StrToVersion(this.plugininfo.version);
				if (v < v2)
				{
					this.ShowErr("对不起，您更新的版本比安装版本还低，没有必要更新。");
					return;
				}
				this.plugininfo.author = pluginConfig.author;
				this.plugininfo.adminurl = pluginConfig.adminurl;
				this.plugininfo.indexurl = pluginConfig.indexurl;
				this.plugininfo.homepage = pluginConfig.homepage;
				this.plugininfo.icon = pluginConfig.icon;
				this.plugininfo.notes = pluginConfig.notes;
				this.plugininfo.version = pluginConfig.version;
				this.plugininfo.dll = pluginConfig.dll;
				if (this.plugininfo.createdate == "")
				{
					this.plugininfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				}
				this.plugininfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
				string mapPath2 = FPFile.GetMapPath(this.plupath + this.plugininfo.installpath);
				string text = mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
				FPFile.CopyDir(text, mapPath2);
				if (Directory.Exists(text))
				{
					Directory.Delete(text, true);
				}
				FPSerializer.Save<PluginConfig>(this.plugininfo, FPFile.GetMapPath(this.plupath + this.plu_path + "/plugin.config"));
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
				FPCache.Remove("FP_PLUGINLIST");
				base.Response.Redirect("pluginmanage.aspx");
			}
		}

		// Token: 0x04000024 RID: 36
		protected string plu_path = FPRequest.GetString("plupath");

		// Token: 0x04000025 RID: 37
		protected PluginConfig plugininfo = new PluginConfig();
	}
}
