using System;

namespace FangPage.Data
{
	// Token: 0x02000015 RID: 21
	public sealed class PrimaryKey : Attribute
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00002249 File Offset: 0x00000449
		public PrimaryKey()
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000BFF1 File Offset: 0x0000A1F1
		public PrimaryKey(bool isPrimaryKey)
		{
			this.m_isprimarykey = isPrimaryKey;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000C000 File Offset: 0x0000A200
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000C008 File Offset: 0x0000A208
		public bool IsPrimaryKey
		{
			get
			{
				return this.m_isprimarykey;
			}
			set
			{
				this.m_isprimarykey = value;
			}
		}

		// Token: 0x04000034 RID: 52
		private bool m_isprimarykey;
	}
}
