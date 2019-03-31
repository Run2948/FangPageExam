using System;

namespace FangPage.Data
{
	// Token: 0x02000012 RID: 18
	public sealed class NText : Attribute
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00002249 File Offset: 0x00000449
		public NText()
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000BADC File Offset: 0x00009CDC
		public NText(bool isntext)
		{
			this.m_isntext = isntext;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000BAEB File Offset: 0x00009CEB
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000BAF3 File Offset: 0x00009CF3
		public bool IsNText
		{
			get
			{
				return this.m_isntext;
			}
			set
			{
				this.m_isntext = value;
			}
		}

		// Token: 0x04000025 RID: 37
		private bool m_isntext;
	}
}
