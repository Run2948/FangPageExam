using System;
using System.Configuration;

namespace FangPage.MVC
{
	// Token: 0x0200000A RID: 10
	public class WebConfig
	{
		// Token: 0x0600004E RID: 78 RVA: 0x000034BD File Offset: 0x000016BD
		static WebConfig()
		{
			WebConfig.ReSet();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000034D8 File Offset: 0x000016D8
		public static void ReSet()
		{
			WebConfig.m_webpath = ConfigurationManager.AppSettings["webpath"];
			if (string.IsNullOrEmpty(WebConfig.m_webpath))
			{
				WebConfig.m_webpath = "/";
			}
			if (!WebConfig.m_webpath.StartsWith("/"))
			{
				WebConfig.m_webpath = "/" + WebConfig.m_webpath;
			}
			if (!WebConfig.m_webpath.EndsWith("/"))
			{
				WebConfig.m_webpath += "/";
			}
			WebConfig.m_sitepath = ConfigurationManager.AppSettings["sitepath"];
			if (string.IsNullOrEmpty(WebConfig.m_sitepath))
			{
				WebConfig.m_sitepath = "";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003583 File Offset: 0x00001783
		public static string WebPath
		{
			get
			{
				return WebConfig.m_webpath;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000358A File Offset: 0x0000178A
		public static string SitePath
		{
			get
			{
				return WebConfig.m_sitepath;
			}
		}

		// Token: 0x04000005 RID: 5
		private static string m_webpath = "";

		// Token: 0x04000006 RID: 6
		private static string m_sitepath = "";
	}
}
