using System;
using System.IO;
using FangPage.Common;
using FangPage.MVC;

namespace FangPage.WMS.Config
{
	// Token: 0x0200002A RID: 42
	public class SysConfigs
	{
		// Token: 0x06000340 RID: 832 RVA: 0x000072DF File Offset: 0x000054DF
		static SysConfigs()
		{
			SysConfigs.ResetConfig();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000072E8 File Offset: 0x000054E8
		public static void ResetConfig()
		{
			SysConfigs.filename = FPFile.GetMapPath(WebConfig.WebPath + "config/sys.config");
			if (!File.Exists(SysConfigs.filename))
			{
				if (!Directory.Exists(Path.GetDirectoryName(SysConfigs.filename)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(SysConfigs.filename));
				}
				SysConfigs.m_configinfo = new SysConfig();
				if (SysConfigs.m_configinfo.passwordkey == "")
				{
					SysConfigs.m_configinfo.passwordkey = FPRandom.CreateCode(20);
				}
				SysConfigs.SaveConfig(SysConfigs.m_configinfo);
			}
			SysConfigs.m_configinfo = FPSerializer.Load<SysConfig>(SysConfigs.filename);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00007386 File Offset: 0x00005586
		public static SysConfig GetConfig()
		{
			if (SysConfigs.m_configinfo == null)
			{
				SysConfigs.ResetConfig();
			}
			return SysConfigs.m_configinfo;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00007399 File Offset: 0x00005599
		public static string AdminPath
		{
			get
			{
				return SysConfigs.m_configinfo.adminpath;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000073A5 File Offset: 0x000055A5
		public static string PlatForm
		{
			get
			{
				return SysConfigs.m_configinfo.platform;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000073B1 File Offset: 0x000055B1
		public static bool SaveConfig(SysConfig sysconfiginfo)
		{
			return FPSerializer.Save<SysConfig>(sysconfiginfo, SysConfigs.filename);
		}

		// Token: 0x040001A6 RID: 422
		private static SysConfig m_configinfo;

		// Token: 0x040001A7 RID: 423
		public static string filename;
	}
}
