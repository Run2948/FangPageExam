using System;
using System.IO;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000034 RID: 52
	public class SysConfigs
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00009600 File Offset: 0x00007800
		static SysConfigs()
		{
			SysConfigs.ResetConfig();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009610 File Offset: 0x00007810
		public static void ResetConfig()
		{
			SysConfigs.filename = FPUtils.GetMapPath(WebConfig.WebPath + "config/sys.config");
			if (!File.Exists(SysConfigs.filename))
			{
				if (!Directory.Exists(Path.GetDirectoryName(SysConfigs.filename)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(SysConfigs.filename));
				}
				SysConfigs.m_configinfo = new SysConfig();
				if (SysConfigs.m_configinfo.passwordkey == "")
				{
					SysConfigs.m_configinfo.passwordkey = WMSUtils.CreateAuthStr(10);
				}
				SysConfigs.SaveConfig(SysConfigs.m_configinfo);
			}
			SysConfigs.m_configinfo = FPSerializer.Load<SysConfig>(SysConfigs.filename);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000096C0 File Offset: 0x000078C0
		public static SysConfig GetConfig()
		{
			if (SysConfigs.m_configinfo == null)
			{
				SysConfigs.ResetConfig();
			}
			return SysConfigs.m_configinfo;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000096F0 File Offset: 0x000078F0
		public static string AdminPath
		{
			get
			{
				return SysConfigs.m_configinfo.adminpath;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000970C File Offset: 0x0000790C
		public static int TaskInterval
		{
			get
			{
				return SysConfigs.m_configinfo.taskinterval;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009728 File Offset: 0x00007928
		public static bool SaveConfig(SysConfig sysconfiginfo)
		{
			return FPSerializer.Save<SysConfig>(sysconfiginfo, SysConfigs.filename);
		}

		// Token: 0x0400012A RID: 298
		private static SysConfig m_configinfo;

		// Token: 0x0400012B RID: 299
		public static string filename = null;
	}
}
