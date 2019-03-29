using System;

namespace FangPage.MVC
{
	// Token: 0x0200000A RID: 10
	public class SiteConfig
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003534 File Offset: 0x00001734
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000354C File Offset: 0x0000174C
		public string name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003558 File Offset: 0x00001758
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003570 File Offset: 0x00001770
		public string sitepath
		{
			get
			{
				return this.m_sitepath;
			}
			set
			{
				this.m_sitepath = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000357C File Offset: 0x0000177C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00003594 File Offset: 0x00001794
		public string inherits
		{
			get
			{
				return this.m_inherits;
			}
			set
			{
				this.m_inherits = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000035A0 File Offset: 0x000017A0
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000035B8 File Offset: 0x000017B8
		public string import
		{
			get
			{
				return this.m_import;
			}
			set
			{
				this.m_import = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000035C4 File Offset: 0x000017C4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000035DC File Offset: 0x000017DC
		public string author
		{
			get
			{
				return this.m_author;
			}
			set
			{
				this.m_author = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000035E8 File Offset: 0x000017E8
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003600 File Offset: 0x00001800
		public int urltype
		{
			get
			{
				return this.m_urltype;
			}
			set
			{
				this.m_urltype = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000360C File Offset: 0x0000180C
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003624 File Offset: 0x00001824
		public string version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003630 File Offset: 0x00001830
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000366D File Offset: 0x0000186D
		public string sitetitle
		{
			get
			{
				if (this.m_sitetitle == "")
				{
					this.m_sitetitle = this.name;
				}
				return this.m_sitetitle;
			}
			set
			{
				this.m_sitetitle = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003678 File Offset: 0x00001878
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003690 File Offset: 0x00001890
		public string keywords
		{
			get
			{
				return this.m_keywords;
			}
			set
			{
				this.m_keywords = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000369C File Offset: 0x0000189C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000036B4 File Offset: 0x000018B4
		public string description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000036C0 File Offset: 0x000018C0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000036D8 File Offset: 0x000018D8
		public string otherhead
		{
			get
			{
				return this.m_otherhead;
			}
			set
			{
				this.m_otherhead = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000036E4 File Offset: 0x000018E4
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000036FC File Offset: 0x000018FC
		public int autocreate
		{
			get
			{
				return this.m_autocreate;
			}
			set
			{
				this.m_autocreate = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003708 File Offset: 0x00001908
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003720 File Offset: 0x00001920
		public int closed
		{
			get
			{
				return this.m_closed;
			}
			set
			{
				this.m_closed = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000372C File Offset: 0x0000192C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003744 File Offset: 0x00001944
		public string closedreason
		{
			get
			{
				return this.m_closedreason;
			}
			set
			{
				this.m_closedreason = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003750 File Offset: 0x00001950
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003768 File Offset: 0x00001968
		public string ipdenyaccess
		{
			get
			{
				return this.m_ipdenyaccess;
			}
			set
			{
				this.m_ipdenyaccess = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003774 File Offset: 0x00001974
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000378C File Offset: 0x0000198C
		public string ipaccess
		{
			get
			{
				return this.m_ipaccess;
			}
			set
			{
				this.m_ipaccess = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003798 File Offset: 0x00001998
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000037B0 File Offset: 0x000019B0
		public string createdate
		{
			get
			{
				return this.m_createdate;
			}
			set
			{
				this.m_createdate = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000037BC File Offset: 0x000019BC
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000037D4 File Offset: 0x000019D4
		public int iscompile
		{
			get
			{
				return this.m_iscompile;
			}
			set
			{
				this.m_iscompile = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000037E0 File Offset: 0x000019E0
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000037F8 File Offset: 0x000019F8
		public string roles
		{
			get
			{
				return this.m_roles;
			}
			set
			{
				this.m_roles = value;
			}
		}

		// Token: 0x04000004 RID: 4
		private string m_name = "方配站点";

		// Token: 0x04000005 RID: 5
		private string m_sitepath = "";

		// Token: 0x04000006 RID: 6
		private string m_inherits = "";

		// Token: 0x04000007 RID: 7
		private string m_import = "";

		// Token: 0x04000008 RID: 8
		private string m_author = "方配";

		// Token: 0x04000009 RID: 9
		private int m_urltype = 2;

		// Token: 0x0400000A RID: 10
		private string m_version = "";

		// Token: 0x0400000B RID: 11
		private string m_sitetitle = "";

		// Token: 0x0400000C RID: 12
		private string m_keywords = "";

		// Token: 0x0400000D RID: 13
		private string m_description = "";

		// Token: 0x0400000E RID: 14
		private string m_otherhead = "";

		// Token: 0x0400000F RID: 15
		private int m_autocreate = 1;

		// Token: 0x04000010 RID: 16
		private int m_closed = 0;

		// Token: 0x04000011 RID: 17
		private string m_closedreason = "站点正在升级，请稍后再访问！";

		// Token: 0x04000012 RID: 18
		private string m_ipdenyaccess = "";

		// Token: 0x04000013 RID: 19
		private string m_ipaccess = "";

		// Token: 0x04000014 RID: 20
		private string m_createdate = DateTime.Now.ToString("yyyy-MM-dd");

		// Token: 0x04000015 RID: 21
		private int m_iscompile = 0;

		// Token: 0x04000016 RID: 22
		private string m_roles = "";
	}
}
