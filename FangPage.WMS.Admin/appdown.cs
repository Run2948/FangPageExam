using System;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000006 RID: 6
	public class appdown : SuperController
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000029C8 File Offset: 0x00000BC8
		protected override void View()
		{
			this.appinfo = DbHelper.ExecuteModel<AppInfo>(this.id);
			if (this.appinfo.id == 0)
			{
				this.ShowErr("对不起，该应用已被删除或不存在。");
			}
			else
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + this.appinfo.installpath);
				if (this.ispost)
				{
					string @string = FPRequest.GetString("setpath");
					if (@string == "")
					{
						this.ShowErr("对不起，应用默认安装目录名称不能为空。");
					}
					else
					{
						string pattern = "^[a-zA-Z0-9_\\w]+$";
						if (this.err == 0 && !Regex.IsMatch(@string.Trim(), pattern, RegexOptions.IgnoreCase))
						{
							base.AddErr("默认安装目录名称只能由数字、字母或下划线组成。");
						}
						this.appinfo.setpath = @string;
						DbHelper.ExecuteUpdate<AppInfo>(this.appinfo);
						FPSerializer.Save<AppInfo>(this.appinfo, FPUtils.GetMapPath(this.webpath + this.appinfo.installpath + "/app.config"));
						string str = this.appinfo.name + ".fpk";
						if (this.appinfo.files != "")
						{
							using (FPZip fpzip = new FPZip())
							{
								foreach (string text in FPUtils.SplitString(this.appinfo.files))
								{
									if (text.StartsWith("bin/"))
									{
										fpzip.AddFile(FPUtils.GetMapPath(WebConfig.WebPath + text), text);
									}
									else
									{
										fpzip.AddFile(mapPath + "/" + text, text);
									}
								}
								fpzip.ZipDown(FPUtils.UrlEncode(str));
							}
						}
						else
						{
							using (FPZip fpzip = new FPZip())
							{
								DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
								foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
								{
									fpzip.AddDirectory(directoryInfo2.FullName);
								}
								foreach (FileInfo fileInfo in directoryInfo.GetFiles())
								{
									if (fileInfo.Name != "site.config")
									{
										fpzip.AddFile(fileInfo.FullName, "");
									}
								}
								string mapPath2 = FPUtils.GetMapPath(this.webpath + "sites/" + this.appinfo.installpath + "/site.config");
								if (File.Exists(mapPath2))
								{
									SiteConfig siteConfig = SiteConfigs.LoadConfig(mapPath2);
									if (siteConfig.inherits != "")
									{
										if (siteConfig.inherits.EndsWith(".Controller"))
										{
											fpzip.AddFile(FPUtils.GetMapPath(this.webpath + "bin/" + siteConfig.inherits.Replace(".Controller", "") + ".dll"), "bin/" + siteConfig.inherits.Replace(".Controller", "") + ".dll");
										}
										else
										{
											fpzip.AddFile(FPUtils.GetMapPath(this.webpath + "bin/" + siteConfig.inherits + ".dll"), "bin/" + siteConfig.inherits + ".dll");
										}
									}
									if (siteConfig.import != "")
									{
										foreach (string text in siteConfig.import.Split(new string[]
										{
											"\r\n",
											";",
											",",
											"|"
										}, StringSplitOptions.RemoveEmptyEntries))
										{
											fpzip.AddFile(FPUtils.GetMapPath(this.webpath + "bin/" + text + ".dll"), "bin/" + text + ".dll");
										}
									}
								}
								fpzip.ZipDown(FPUtils.UrlEncode(str));
							}
						}
					}
				}
			}
		}

		// Token: 0x04000007 RID: 7
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000008 RID: 8
		protected AppInfo appinfo = new AppInfo();
	}
}
