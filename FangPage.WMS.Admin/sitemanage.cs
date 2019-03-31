using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000039 RID: 57
	public class sitemanage : SuperController
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000B3E0 File Offset: 0x000095E0
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (this.action == "delete" || this.action == "create" || this.action == "download")
				{
					string @string = FPRequest.GetString("sitepath");
					if (!File.Exists(FPFile.GetMapPath(this.webpath + "sites/" + @string + "/site.config")))
					{
						this.ShowErr("对不起，该站点已被删除或者不存在。");
						return;
					}
					SiteConfig siteInfo = SiteConfigs.GetSiteInfo(@string);
					FPFile.GetMapPath(this.webpath + "sites/" + siteInfo.sitepath);
					if (this.action == "delete")
					{
						SetupBll.DeleteSetup(siteInfo);
						FPCache.Remove("FP_SITELIST");
					}
					else if (this.action == "download")
					{
						SetupBll.DownloadSetup(siteInfo);
					}
					else if (this.action == "create")
					{
						Version v = FPUtils.StrToVersion(siteInfo.version);
						Version version = FPUtils.StrToVersion("0.0.0");
						if (siteInfo.dll != "")
						{
							string[] array = FPArray.SplitString(siteInfo.dll, 2);
							if (array[1] == "")
							{
								array[1] = array[0].Replace(".Controller", "");
							}
							version = FPUtils.GetVersion(FPFile.GetMapPath(this.webpath + "bin/" + array[1] + ".dll"));
						}
						if (version > v)
						{
							siteInfo.version = FPUtils.FormatVersion(version.ToString());
						}
						FPViews.CreateSite(siteInfo);
						if (siteInfo.createdate == "")
						{
							siteInfo.createdate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
						}
						siteInfo.updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
						SiteConfigs.SaveSiteConfig(siteInfo);
						FPCache.Remove("FP_SITELIST");
						base.AddMsg("站点编译成功！");
					}
				}
				else if (this.action == "open")
				{
					string[] array2 = FPArray.SplitString(FPRequest.GetString("chksite"));
					for (int i = 0; i < array2.Length; i++)
					{
						SiteConfig mapSiteConfig = SiteConfigs.GetMapSiteConfig(array2[i]);
						if (mapSiteConfig.guid != "")
						{
							mapSiteConfig.closed = 0;
							SiteConfigs.SaveSiteConfig(mapSiteConfig);
						}
					}
				}
				else if (this.action == "close")
				{
					string[] array2 = FPArray.SplitString(FPRequest.GetString("chksite"));
					for (int i = 0; i < array2.Length; i++)
					{
						SiteConfig mapSiteConfig2 = SiteConfigs.GetMapSiteConfig(array2[i]);
						if (mapSiteConfig2.guid != "")
						{
							mapSiteConfig2.closed = 1;
							SiteConfigs.SaveSiteConfig(mapSiteConfig2);
						}
					}
				}
			}
			this.sitelist = SiteConfigs.GetMapSiteList();
			bool flag = false;
			for (int j = 0; j < this.sitelist.Count; j++)
			{
				Version v2 = FPUtils.StrToVersion(this.sitelist[j].version);
				Version version2 = FPUtils.StrToVersion("0.0.0");
				if (this.sitelist[j].dll != "")
				{
					string[] array3 = FPArray.SplitString(this.sitelist[j].dll, 2);
					if (array3[1] == "")
					{
						array3[1] = array3[0].Replace(".Controller", "");
					}
					version2 = FPUtils.GetVersion(FPFile.GetMapPath(this.webpath + "bin/" + array3[1] + ".dll"));
				}
				if (version2 > v2)
				{
					this.sitelist[j].version = FPUtils.FormatVersion(version2.ToString());
					this.sitelist[j].updatedate = DbUtils.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");
					SiteConfigs.SaveSiteConfig(this.sitelist[j]);
					flag = true;
				}
			}
			if (flag)
			{
				FPCache.Remove("FP_SITELIST");
			}
		}

		// Token: 0x04000096 RID: 150
		protected List<SiteConfig> sitelist = new List<SiteConfig>();
	}
}
