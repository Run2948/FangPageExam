using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001F RID: 31
	[ModelPrefix("WMS")]
	public class MsgTempInfo
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000662C File Offset: 0x0000482C
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00006644 File Offset: 0x00004844
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006650 File Offset: 0x00004850
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00006668 File Offset: 0x00004868
		public int type
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006674 File Offset: 0x00004874
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000668C File Offset: 0x0000488C
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006698 File Offset: 0x00004898
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000066B0 File Offset: 0x000048B0
		public string markup
		{
			get
			{
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000066BC File Offset: 0x000048BC
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000066D4 File Offset: 0x000048D4
		public string content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x040000A0 RID: 160
		private int m_id;

		// Token: 0x040000A1 RID: 161
		private int m_type;

		// Token: 0x040000A2 RID: 162
		private string m_name = string.Empty;

		// Token: 0x040000A3 RID: 163
		private string m_markup = string.Empty;

		// Token: 0x040000A4 RID: 164
		private string m_content = string.Empty;
	}
}
