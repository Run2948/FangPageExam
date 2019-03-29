using System;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001D RID: 29
	public class PluginConfig
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006250 File Offset: 0x00004450
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00006268 File Offset: 0x00004468
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006274 File Offset: 0x00004474
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000628C File Offset: 0x0000448C
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006298 File Offset: 0x00004498
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000062B0 File Offset: 0x000044B0
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000062BC File Offset: 0x000044BC
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000062D4 File Offset: 0x000044D4
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000062E0 File Offset: 0x000044E0
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000062F8 File Offset: 0x000044F8
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006304 File Offset: 0x00004504
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000631C File Offset: 0x0000451C
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006328 File Offset: 0x00004528
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006340 File Offset: 0x00004540
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000634C File Offset: 0x0000454C
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006364 File Offset: 0x00004564
		public int system
		{
			get
			{
				return this.m_system;
			}
			set
			{
				this.m_system = value;
			}
		}

		// Token: 0x0400008C RID: 140
		private string m_name = "";

		// Token: 0x0400008D RID: 141
		private string m_installpath = "";

		// Token: 0x0400008E RID: 142
		private string m_author = "";

		// Token: 0x0400008F RID: 143
		private string m_version = "";

		// Token: 0x04000090 RID: 144
		private string m_frontpage = "";

		// Token: 0x04000091 RID: 145
		private string m_notes = "";

		// Token: 0x04000092 RID: 146
		private string m_guid = "";

		// Token: 0x04000093 RID: 147
		private int m_system = 0;
	}
}
