using System;
using System.Collections.Generic;
using System.IO;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000012 RID: 18
	public class pluginmanage : SuperController
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000045F0 File Offset: 0x000027F0
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("pluname");
				if (this.action == "delete")
				{
					if (@string != "" && Directory.Exists(FPUtils.GetMapPath(this.webpath + "plugins/" + @string)))
					{
						Directory.Delete(FPUtils.GetMapPath(this.webpath + "plugins/" + @string), true);
					}
				}
				else if (this.action == "download")
				{
					using (FPZip fpzip = new FPZip())
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(FPUtils.GetMapPath(this.webpath + "plugins/" + @string));
						foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
						{
							fpzip.AddDirectory(directoryInfo2.FullName);
						}
						foreach (FileInfo fileInfo in directoryInfo.GetFiles())
						{
							fpzip.AddFile(fileInfo.FullName, "");
						}
						fpzip.ZipDown(FPUtils.UrlEncode(@string + ".plu"));
					}
				}
			}
			this.pluginlist = PluginBll.GetPluginList();
			base.SaveRightURL();
		}

		// Token: 0x0400001D RID: 29
		protected List<PluginConfig> pluginlist = new List<PluginConfig>();
	}
}
