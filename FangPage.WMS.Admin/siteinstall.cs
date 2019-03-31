using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000038 RID: 56
	public class siteinstall : SuperController
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000AEB8 File Offset: 0x000090B8
		protected override void Controller()
		{
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				if (this.step == "step1")
				{
					this.filename = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(this.filename).ToLower();
					if (a != ".zip" && a != ".site")
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
					FPZip.UnZip(mapPath + "\\" + this.filename, "");
					File.Delete(mapPath + "\\" + this.filename);
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\site.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename), true);
						this.ShowErr("对不起，安装失败，该插件安装配置文件不存在。");
						return;
					}
					this.siteinfo = FPSerializer.Load<SiteConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\site.config");
					if (this.siteinfo.guid == "")
					{
						this.ShowErr("对不起，安装失败，该站点安装标识码不能为空。。");
						return;
					}
					SiteConfig siteConfig = SiteConfigs.GetSiteConfig(this.siteinfo.guid);
					if (siteConfig.guid != "")
					{
						this.isinstall = 1;
					}
					if (this.isinstall == 1)
					{
						Version v = FPUtils.StrToVersion(this.siteinfo.version);
						Version v2 = FPUtils.StrToVersion(siteConfig.version);
						if (v < v2)
						{
							this.ShowErr("对不起，您安装的站点版本比已安装的版本还低，没必要安装。");
							return;
						}
						if (v == v2)
						{
							this.ShowErr("对不起，该站点在系统中已安装，没必要重复安装。");
							return;
						}
						this.siteinfo.sitepath = siteConfig.sitepath;
						return;
					}
				}
				else if (this.step == "step2")
				{
					string text = FPRequest.GetString("installpath").ToLower();
					if (text == "")
					{
						this.ShowErr("对不起，安装目录不能为空。");
						return;
					}
					if (this.isinstall != 1 && Directory.Exists(FPFile.GetMapPath(this.webpath + "sites/" + text)))
					{
						this.ShowErr("对不起，该安装目录[" + text + "]已被其他应用使用，请另选其他目录。");
						return;
					}
					this.filename = FPRequest.GetString("filename");
					this.siteinfo = FPSerializer.Load<SiteConfig>(mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename) + "\\site.config");
					this.siteinfo.sitepath = text;
					string mapPath2 = FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath);
					string text2 = mapPath + "\\" + Path.GetFileNameWithoutExtension(this.filename);
					if (this.isinstall == 1)
					{
						FPFile.ClearDir(mapPath2);
					}
					FPFile.CopyDir(text2, mapPath2);
					Directory.Delete(text2, true);
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
					FPViews.CreateSite(this.siteinfo);
					if (this.siteinfo.createdate == "")
					{
						this.siteinfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					}
					this.siteinfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					SiteConfigs.SaveSiteConfig(this.siteinfo);
					FPCache.Remove("FP_SITELIST");
					base.Response.Redirect("sitemanage.aspx");
				}
			}
		}

		// Token: 0x04000092 RID: 146
		protected string step = FPRequest.GetString("step").ToLower();

		// Token: 0x04000093 RID: 147
		protected new SiteConfig siteinfo = new SiteConfig();

		// Token: 0x04000094 RID: 148
		protected string filename = "";

		// Token: 0x04000095 RID: 149
		protected int isinstall = FPRequest.GetInt("isinstall");
	}
}
