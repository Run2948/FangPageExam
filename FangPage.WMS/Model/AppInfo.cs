using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000013 RID: 19
	[ModelPrefix("WMS")]
	public class AppInfo
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004FC4 File Offset: 0x000031C4
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00004FDC File Offset: 0x000031DC
		[Identity(true)]
		[PrimaryKey(true)]
		public int id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00004FE8 File Offset: 0x000031E8
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00005000 File Offset: 0x00003200
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
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000500C File Offset: 0x0000320C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00005024 File Offset: 0x00003224
		public string installpath
		{
			get
			{
				return this.m_installpath;
			}
			set
			{
				this.m_installpath = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00005030 File Offset: 0x00003230
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00005048 File Offset: 0x00003248
		public string setpath
		{
			get
			{
				return this.m_setpath;
			}
			set
			{
				this.m_setpath = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00005054 File Offset: 0x00003254
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000506C File Offset: 0x0000326C
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00005078 File Offset: 0x00003278
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00005090 File Offset: 0x00003290
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000509C File Offset: 0x0000329C
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000050B4 File Offset: 0x000032B4
		public string frontpage
		{
			get
			{
				return this.m_frontpage;
			}
			set
			{
				this.m_frontpage = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000050C0 File Offset: 0x000032C0
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000050D8 File Offset: 0x000032D8
		public string target
		{
			get
			{
				return this.m_target;
			}
			set
			{
				this.m_target = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000050E4 File Offset: 0x000032E4
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000050FC File Offset: 0x000032FC
		public string notes
		{
			get
			{
				return this.m_notes;
			}
			set
			{
				this.m_notes = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00005108 File Offset: 0x00003308
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00005120 File Offset: 0x00003320
		public string files
		{
			get
			{
				return this.m_files;
			}
			set
			{
				this.m_files = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000512C File Offset: 0x0000332C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00005144 File Offset: 0x00003344
		public string guid
		{
			get
			{
				return this.m_guid;
			}
			set
			{
				this.m_guid = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00005150 File Offset: 0x00003350
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00005168 File Offset: 0x00003368
		public string sortapps
		{
			get
			{
				return this.m_sortapps;
			}
			set
			{
				this.m_sortapps = value;
			}
		}

		// Token: 0x04000023 RID: 35
		private int m_id;

		// Token: 0x04000024 RID: 36
		private string m_name = string.Empty;

		// Token: 0x04000025 RID: 37
		private string m_installpath = string.Empty;

		// Token: 0x04000026 RID: 38
		private string m_setpath = string.Empty;

		// Token: 0x04000027 RID: 39
		private string m_author = string.Empty;

		// Token: 0x04000028 RID: 40
		private string m_version = string.Empty;

		// Token: 0x04000029 RID: 41
		private string m_frontpage = string.Empty;

		// Token: 0x0400002A RID: 42
		private string m_target = "_blank";

		// Token: 0x0400002B RID: 43
		private string m_notes = string.Empty;

		// Token: 0x0400002C RID: 44
		private string m_files = string.Empty;

		// Token: 0x0400002D RID: 45
		private string m_guid = string.Empty;

		// Token: 0x0400002E RID: 46
		private string m_sortapps = string.Empty;
	}
}
