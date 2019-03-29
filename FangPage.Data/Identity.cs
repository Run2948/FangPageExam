using System;

namespace FangPage.Data
{
	// Token: 0x0200000E RID: 14
	public sealed class Identity : Attribute
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00005870 File Offset: 0x00003A70
		public Identity()
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000587F File Offset: 0x00003A7F
		public Identity(bool isIdentity)
		{
			this.IsIdentity = isIdentity;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005895 File Offset: 0x00003A95
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000589D File Offset: 0x00003A9D
		public bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
			set
			{
				this._isIdentity = value;
			}
		}

		// Token: 0x04000016 RID: 22
		private bool _isIdentity = true;
	}
}
