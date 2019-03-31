using System;

namespace FangPage.Data
{
	// Token: 0x02000005 RID: 5
	public sealed class TempField : Attribute
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002249 File Offset: 0x00000449
		public TempField()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002251 File Offset: 0x00000451
		public TempField(bool isbind)
		{
			this.m_istemp = isbind;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002260 File Offset: 0x00000460
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002268 File Offset: 0x00000468
		public bool IsTemp
		{
			get
			{
				return this.m_istemp;
			}
			set
			{
				this.m_istemp = value;
			}
		}

		// Token: 0x04000011 RID: 17
		private bool m_istemp;
	}
}
