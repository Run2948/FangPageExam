using System;

namespace FangPage.Data
{
	// Token: 0x02000011 RID: 17
	public sealed class Identity : Attribute
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00002249 File Offset: 0x00000449
		public Identity()
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000BABC File Offset: 0x00009CBC
		public Identity(bool isIdentity)
		{
			this.IsIdentity = isIdentity;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000BACB File Offset: 0x00009CCB
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000BAD3 File Offset: 0x00009CD3
		public bool IsIdentity
		{
			get
			{
				return this.m_isidentity;
			}
			set
			{
				this.m_isidentity = value;
			}
		}

		// Token: 0x04000024 RID: 36
		private bool m_isidentity;
	}
}
