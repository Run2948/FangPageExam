using System;
using System.Collections.Generic;
using System.IO;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002C RID: 44
	public class sitemanage : SuperController
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00009964 File Offset: 0x00007B64
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					string @string = FPRequest.GetString("chkdel");
					foreach (string text in @string.Split(new char[]
					{
						','
					}))
					{
						if (text != "" && Directory.Exists(FPUtils.GetMapPath(this.webpath + "sites/" + text)))
						{
							Directory.Delete(FPUtils.GetMapPath(this.webpath + "sites/" + text), true);
						}
					}
				}
				else if (this.action == "create")
				{
					string string2 = FPRequest.GetString("sitepath");
					string message = "";
					if (!SiteBll.CreateSite(string2, out message))
					{
						this.ShowErr(message);
						return;
					}
					base.AddMsg("站点编译成功！");
				}
				else if (this.action == "download")
				{
					string string3 = FPRequest.GetString("sitepath");
					SiteConfig siteInfo = SiteConfigs.GetSiteInfo(string3);
					string str = ((siteInfo.name != "") ? siteInfo.name : siteInfo.sitepath) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".fpsite";
					using (FPZip fpzip = new FPZip())
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(FPUtils.GetMapPath(this.webpath + "sites/" + string3));
						foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
						{
							fpzip.AddDirectory(directoryInfo2.FullName);
						}
						foreach (FileInfo fileInfo in directoryInfo.GetFiles())
						{
							fpzip.AddFile(fileInfo.FullName, "");
						}
						fpzip.ZipDown(FPUtils.UrlEncode(str));
					}
				}
			}
			this.sitelist = SiteBll.GetSiteList();
			base.SaveRightURL();
		}

		// Token: 0x0400005E RID: 94
		protected List<SiteConfig> sitelist = new List<SiteConfig>();
	}
}
