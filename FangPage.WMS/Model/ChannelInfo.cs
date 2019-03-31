using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200000B RID: 11
	[ModelPrefix("WMS")]
	public class ChannelInfo
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004342 File Offset: 0x00002542
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000434A File Offset: 0x0000254A
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004353 File Offset: 0x00002553
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000435B File Offset: 0x0000255B
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004364 File Offset: 0x00002564
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000436C File Offset: 0x0000256C
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004375 File Offset: 0x00002575
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000437D File Offset: 0x0000257D
		public int display
		{
			get
			{
				return this.m_display;
			}
			set
			{
				this.m_display = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000438E File Offset: 0x0000258E
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004397 File Offset: 0x00002597
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000439F File Offset: 0x0000259F
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

		// Token: 0x04000052 RID: 82
		private int m_id;

		// Token: 0x04000053 RID: 83
		private string m_name = string.Empty;

		// Token: 0x04000054 RID: 84
		private string m_markup = string.Empty;

		// Token: 0x04000055 RID: 85
		private int m_display;

		// Token: 0x04000056 RID: 86
		private string m_sortapps = string.Empty;

		// Token: 0x04000057 RID: 87
		private string m_description = string.Empty;
	}
}
