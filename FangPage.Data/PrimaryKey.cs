using System;

namespace FangPage.Data
{
	// Token: 0x02000013 RID: 19
	public sealed class PrimaryKey : Attribute
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00005D8F File Offset: 0x00003F8F
		public PrimaryKey()
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005D9E File Offset: 0x00003F9E
		public PrimaryKey(bool isPrimaryKey)
		{
			this._isPrimaryKey = isPrimaryKey;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005DB4 File Offset: 0x00003FB4
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00005DBC File Offset: 0x00003FBC
		public bool IsPrimaryKey
		{
			get
			{
				return this._isPrimaryKey;
			}
			set
			{
				this._isPrimaryKey = value;
			}
		}

		// Token: 0x04000026 RID: 38
		private bool _isPrimaryKey = true;
	}
}
