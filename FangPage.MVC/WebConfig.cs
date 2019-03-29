using System;
using System.Configuration;

namespace FangPage.MVC
{
	// Token: 0x02000007 RID: 7
	public class WebConfig
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000030C3 File Offset: 0x000012C3
		static WebConfig()
		{
			WebConfig.ReSet();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000030E4 File Offset: 0x000012E4
		public static void ReSet()
		{
			WebConfig.webpath = ConfigurationManager.AppSettings["webpath"];
			if (string.IsNullOrEmpty(WebConfig.webpath))
			{
				WebConfig.webpath = "/";
			}
			if (!WebConfig.webpath.StartsWith("/"))
			{
				WebConfig.webpath = "/" + WebConfig.webpath;
			}
			if (!WebConfig.webpath.EndsWith("/"))
			{
				WebConfig.webpath += "/";
			}
			WebConfig.sitepath = ConfigurationManager.AppSettings["sitepath"];
			if (string.IsNullOrEmpty(WebConfig.sitepath))
			{
				WebConfig.sitepath = "";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000031A8 File Offset: 0x000013A8
		public static string WebPath
		{
			get
			{
				return WebConfig.webpath;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000031C0 File Offset: 0x000013C0
		public static string SitePath
		{
			get
			{
				return WebConfig.sitepath;
			}
		}

		// Token: 0x04000002 RID: 2
		private static string webpath = "";

		// Token: 0x04000003 RID: 3
		private static string sitepath = "";
	}
}
