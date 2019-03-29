using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.MVC;

namespace FangPage.WMS
{
	// Token: 0x02000030 RID: 48
	public class SiteBll
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00007DAC File Offset: 0x00005FAC
		public static List<SiteConfig> GetSiteList()
		{
			List<SiteConfig> list = new List<SiteConfig>();
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "sites");
			if (Directory.Exists(mapPath))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					SiteConfig siteConfig = SiteConfigs.LoadConfig(directoryInfo2.FullName + "\\site.config");
					siteConfig.sitepath = directoryInfo2.Name;
					list.Add(siteConfig);
				}
			}
			return list;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007E4C File Offset: 0x0000604C
		public static SiteConfig GetSiteConfig(string sitepath)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "sites/" + sitepath + "/site.config");
			SiteConfig siteConfig = SiteConfigs.LoadConfig(mapPath);
			siteConfig.sitepath = sitepath;
			return siteConfig;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007E8C File Offset: 0x0000608C
		public static bool SaveSiteConfig(SiteConfig siteconfig)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "sites/" + siteconfig.sitepath);
			if (!Directory.Exists(mapPath))
			{
				Directory.CreateDirectory(mapPath);
			}
			return SiteConfigs.SaveConfig(siteconfig, mapPath + "\\site.config");
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00007EE0 File Offset: 0x000060E0
		public static bool CreateSite(string sitepath, out string errormsg)
		{
			errormsg = "";
			string pattern = "^[a-zA-Z0-9_\\w]+$";
			bool result;
			if (!Regex.IsMatch(sitepath, pattern, RegexOptions.IgnoreCase))
			{
				errormsg = "模板路径只能由数字、字母、下划线组成，并且首字不能为数字。";
				result = false;
			}
			else
			{
				string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "sites/" + sitepath);
				if (!Directory.Exists(mapPath))
				{
					errormsg = "错误：该站点“" + sitepath + "”已被删除或不存在。";
					result = false;
				}
				else
				{
					string mapPath2 = FPUtils.GetMapPath(WebConfig.WebPath + sitepath);
					SiteConfig siteInfo = SiteConfigs.GetSiteInfo(sitepath);
					SiteBll.CreateViewFile(siteInfo, sitepath);
					siteInfo.iscompile = 1;
					SiteConfigs.SaveConfig(siteInfo, mapPath2 + "\\site.config");
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00007F94 File Offset: 0x00006194
		public static void CreateViewFile(SiteConfig siteconfig, string sitepath)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "sites/" + sitepath);
			DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
			Hashtable viewInclude = FPViews.GetViewInclude();
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (fileInfo.Extension.ToLower() == ".aspx" && !fileInfo.Name.StartsWith("_"))
				{
					string text = "sites/" + sitepath + "/" + fileInfo.Name;
					string aspxpath = sitepath + "/" + fileInfo.Name;
					string text2 = "";
					string text3 = "";
					FPViews.CreateView(siteconfig, WebConfig.WebPath, text, aspxpath, 1, "", out text2, out text3);
					if (viewInclude[text] == null)
					{
						viewInclude.Add(text, text2);
						FPViews.AddViewInclude(text, text2);
					}
					else if (text2 != viewInclude[text].ToString())
					{
						viewInclude[text] = text2;
						FPViews.UpdateViewInclude(text, text2);
					}
				}
				else if (!fileInfo.Name.StartsWith("_") && fileInfo.Name != "site.config" && fileInfo.Extension.ToLower() != ".psd")
				{
					if (siteconfig.urltype == 0)
					{
						string mapPath2 = FPUtils.GetMapPath(WebConfig.WebPath + sitepath);
						if (!Directory.Exists(mapPath2))
						{
							Directory.CreateDirectory(mapPath2);
						}
						if (File.Exists(mapPath2 + "\\" + fileInfo.Name) && File.GetAttributes(mapPath2 + "\\" + fileInfo.Name).ToString().IndexOf("ReadOnly") != -1)
						{
							File.SetAttributes(mapPath2 + "\\" + fileInfo.Name, FileAttributes.Normal);
						}
						fileInfo.CopyTo(mapPath2 + "\\" + fileInfo.Name, true);
					}
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				SiteBll.CreateViewFile(siteconfig, sitepath + "/" + directoryInfo2.Name);
			}
		}
	}
}
