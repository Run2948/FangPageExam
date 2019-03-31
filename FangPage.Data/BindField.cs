using System;

namespace FangPage.Data
{
	// Token: 0x02000016 RID: 22
	public sealed class BindField : Attribute
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000C011 File Offset: 0x0000A211
		public BindField()
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000C020 File Offset: 0x0000A220
		public BindField(bool isbind)
		{
			this.m_isbind = isbind;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000C036 File Offset: 0x0000A236
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000C03E File Offset: 0x0000A23E
		public bool IsBind
		{
			get
			{
				return this.m_isbind;
			}
			set
			{
				this.m_isbind = value;
			}
		}

		// Token: 0x04000035 RID: 53
		private bool m_isbind = true;
	}
}
