using System;

namespace FangPage.WMS.Model
{
	// Token: 0x02000010 RID: 16
	public class SetupInfo
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004DF4 File Offset: 0x00002FF4
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004DFC File Offset: 0x00002FFC
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
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004E05 File Offset: 0x00003005
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00004E0D File Offset: 0x0000300D
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004E16 File Offset: 0x00003016
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00004E1E File Offset: 0x0000301E
		public string type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004E27 File Offset: 0x00003027
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004E2F File Offset: 0x0000302F
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00004E38 File Offset: 0x00003038
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00004E40 File Offset: 0x00003040
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004E49 File Offset: 0x00003049
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00004E51 File Offset: 0x00003051
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004E5A File Offset: 0x0000305A
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00004E90 File Offset: 0x00003090
		public string markup
		{
			get
			{
				if (this.m_markup == "")
				{
					this.m_markup = this.type + "_" + this.installpath;
				}
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00004E99 File Offset: 0x00003099
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00004EA1 File Offset: 0x000030A1
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00004EAA File Offset: 0x000030AA
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00004EB2 File Offset: 0x000030B2
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

		// Token: 0x0400009F RID: 159
		private string m_guid = string.Empty;

		// Token: 0x040000A0 RID: 160
		private string m_name = string.Empty;

		// Token: 0x040000A1 RID: 161
		private string m_type = string.Empty;

		// Token: 0x040000A2 RID: 162
		private string m_installpath = string.Empty;

		// Token: 0x040000A3 RID: 163
		private string m_icon = "";

		// Token: 0x040000A4 RID: 164
		private string m_version = string.Empty;

		// Token: 0x040000A5 RID: 165
		private string m_markup = string.Empty;

		// Token: 0x040000A6 RID: 166
		private string m_platform = string.Empty;

		// Token: 0x040000A7 RID: 167
		private string m_dll = string.Empty;
	}
}
