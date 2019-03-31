using System;

namespace FangPage.MVC
{
	// Token: 0x0200000B RID: 11
	public class SiteConfig
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003591 File Offset: 0x00001791
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003599 File Offset: 0x00001799
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000035A2 File Offset: 0x000017A2
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000035AA File Offset: 0x000017AA
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000035B4 File Offset: 0x000017B4
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003601 File Offset: 0x00001801
		public string markup
		{
			get
			{
				if (this.m_markup == "" && this.sitepath != "")
				{
					this.m_markup = "sites_" + this.sitepath;
				}
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000360A File Offset: 0x0000180A
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003612 File Offset: 0x00001812
		public string platform
		{
			get
			{
				return this.m_platform;
			}
			set
			{
				this.m_platform = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000361B File Offset: 0x0000181B
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003623 File Offset: 0x00001823
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000362C File Offset: 0x0000182C
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003634 File Offset: 0x00001834
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000363D File Offset: 0x0000183D
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003645 File Offset: 0x00001845
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000364E File Offset: 0x0000184E
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003656 File Offset: 0x00001856
		public string dll
		{
			get
			{
				return this.m_dll;
			}
			set
			{
				this.m_dll = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000365F File Offset: 0x0000185F
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003667 File Offset: 0x00001867
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003670 File Offset: 0x00001870
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003678 File Offset: 0x00001878
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003681 File Offset: 0x00001881
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003689 File Offset: 0x00001889
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003692 File Offset: 0x00001892
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000036B8 File Offset: 0x000018B8
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000036C1 File Offset: 0x000018C1
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000036C9 File Offset: 0x000018C9
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000036D2 File Offset: 0x000018D2
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000036DA File Offset: 0x000018DA
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000036E3 File Offset: 0x000018E3
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000036EB File Offset: 0x000018EB
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000036F4 File Offset: 0x000018F4
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000036FC File Offset: 0x000018FC
		public string copyright
		{
			get
			{
				return this.m_copyright;
			}
			set
			{
				this.m_copyright = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003705 File Offset: 0x00001905
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000370D File Offset: 0x0000190D
		public string homepage
		{
			get
			{
				return this.m_homepage;
			}
			set
			{
				this.m_homepage = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003716 File Offset: 0x00001916
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000371E File Offset: 0x0000191E
		public string adminurl
		{
			get
			{
				return this.m_adminurl;
			}
			set
			{
				this.m_adminurl = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003727 File Offset: 0x00001927
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000372F File Offset: 0x0000192F
		public string indexurl
		{
			get
			{
				return this.m_indexurl;
			}
			set
			{
				this.m_indexurl = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003738 File Offset: 0x00001938
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003740 File Offset: 0x00001940
		public string icon
		{
			get
			{
				return this.m_icon;
			}
			set
			{
				this.m_icon = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003749 File Offset: 0x00001949
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003751 File Offset: 0x00001951
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000375A File Offset: 0x0000195A
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003762 File Offset: 0x00001962
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000376B File Offset: 0x0000196B
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003773 File Offset: 0x00001973
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000377C File Offset: 0x0000197C
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003784 File Offset: 0x00001984
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000378D File Offset: 0x0000198D
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003795 File Offset: 0x00001995
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000379E File Offset: 0x0000199E
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000037A6 File Offset: 0x000019A6
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000037AF File Offset: 0x000019AF
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000037B7 File Offset: 0x000019B7
		public string updatedate
		{
			get
			{
				return this.m_updatedate;
			}
			set
			{
				this.m_updatedate = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000037C0 File Offset: 0x000019C0
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000037C8 File Offset: 0x000019C8
		public string size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000037D1 File Offset: 0x000019D1
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000037D9 File Offset: 0x000019D9
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

		// Token: 0x04000007 RID: 7
		private string m_name = "";

		// Token: 0x04000008 RID: 8
		private string m_guid = string.Empty;

		// Token: 0x04000009 RID: 9
		private string m_markup = string.Empty;

		// Token: 0x0400000A RID: 10
		private string m_platform = string.Empty;

		// Token: 0x0400000B RID: 11
		private string m_sitepath = "";

		// Token: 0x0400000C RID: 12
		private string m_author = "方配";

		// Token: 0x0400000D RID: 13
		private string m_import = "";

		// Token: 0x0400000E RID: 14
		private string m_dll = "";

		// Token: 0x0400000F RID: 15
		private int m_urltype = 1;

		// Token: 0x04000010 RID: 16
		private string m_notes = "";

		// Token: 0x04000011 RID: 17
		private string m_version = "1.0.0";

		// Token: 0x04000012 RID: 18
		private string m_sitetitle = "";

		// Token: 0x04000013 RID: 19
		private string m_keywords = "";

		// Token: 0x04000014 RID: 20
		private string m_description = "";

		// Token: 0x04000015 RID: 21
		private string m_otherhead = "";

		// Token: 0x04000016 RID: 22
		private string m_copyright = "";

		// Token: 0x04000017 RID: 23
		private string m_homepage = "";

		// Token: 0x04000018 RID: 24
		private string m_adminurl = "";

		// Token: 0x04000019 RID: 25
		private string m_indexurl = "";

		// Token: 0x0400001A RID: 26
		private string m_icon = "";

		// Token: 0x0400001B RID: 27
		private int m_autocreate = 1;

		// Token: 0x0400001C RID: 28
		private int m_closed;

		// Token: 0x0400001D RID: 29
		private string m_closedreason = "站点正在升级，请稍后再访问！";

		// Token: 0x0400001E RID: 30
		private string m_ipdenyaccess = "";

		// Token: 0x0400001F RID: 31
		private string m_ipaccess = "";

		// Token: 0x04000020 RID: 32
		private string m_createdate = string.Empty;

		// Token: 0x04000021 RID: 33
		private string m_updatedate = string.Empty;

		// Token: 0x04000022 RID: 34
		private string m_size = string.Empty;

		// Token: 0x04000023 RID: 35
		private string m_roles = "";
	}
}
