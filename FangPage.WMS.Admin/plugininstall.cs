using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000018 RID: 24
	public class plugininstall : SuperController
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00004AA8 File Offset: 0x00002CA8
		protected override void Controller()
		{
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
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
					FPZip.UnZip(mapPath + "\\" + this.filename, "");
					File.Delete(mapPath + "\\" + this.filename);
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\plugin.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
						this.ShowErr("对不起，安装失败，该插件安装配置文件不存在。");
						return;
					}
					this.plugininfo = FPSerializer.Load<PluginConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\plugin.config");
					if (this.plugininfo.guid == "")
					{
						this.ShowErr("对不起，安装失败，该插件安装标识码不能为空。");
						return;
					}
					PluginConfig pluConfig = PluginConfigs.GetPluConfig(this.plugininfo.guid);
					if (pluConfig.installpath != "")
					{
						this.isinstall = 1;
					}
					if (this.isinstall == 1)
					{
						Version v = FPUtils.StrToVersion(this.plugininfo.version);
						Version v2 = FPUtils.StrToVersion(pluConfig.version);
						if (v < v2)
						{
							this.ShowErr("对不起，您安装的插件版本比已安装的版本还低，没必要安装。");
							return;
						}
						if (v == v2)
						{
							this.ShowErr("对不起，该插件在系统中已安装，没必要重复安装。");
							return;
						}
						this.plugininfo.installpath = pluConfig.installpath;
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
					if (this.isinstall != 1 && Directory.Exists(FPFile.GetMapPath(this.plupath + @string)))
					{
						this.ShowErr("对不起，该安装目录[" + @string + "]已被其他应用使用，请另选其他目录。");
						return;
					}
					this.filename = FPRequest.GetString("filename");
					this.plugininfo = FPSerializer.Load<PluginConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\plugin.config");
					this.plugininfo.installpath = @string;
					string mapPath2 = FPFile.GetMapPath(this.plupath + this.plugininfo.installpath);
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
					FPCache.Remove("FP_PLUGINLIST");
					base.Response.Redirect("pluginmanage.aspx");
				}
			}
		}

		// Token: 0x04000033 RID: 51
		protected string step = FPRequest.GetString("step").ToLower();

		// Token: 0x04000034 RID: 52
		protected PluginConfig plugininfo = new PluginConfig();

		// Token: 0x04000035 RID: 53
		protected string filename = "";

		// Token: 0x04000036 RID: 54
		protected int isinstall = FPRequest.GetInt("isinstall");
	}
}
