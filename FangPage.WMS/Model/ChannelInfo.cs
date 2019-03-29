using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000016 RID: 22
	[ModelPrefix("WMS")]
	public class ChannelInfo
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00005454 File Offset: 0x00003654
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000546B File Offset: 0x0000366B
		[PrimaryKey(true)]
		[Identity(true)]
		public int id { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00005474 File Offset: 0x00003674
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000548C File Offset: 0x0000368C
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005498 File Offset: 0x00003698
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000054B0 File Offset: 0x000036B0
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000054D4 File Offset: 0x000036D4
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

		// Token: 0x0400003F RID: 63
		private string m_name = "";

		// Token: 0x04000040 RID: 64
		private string m_markup = string.Empty;

		// Token: 0x04000041 RID: 65
		private int m_display;
	}
}
