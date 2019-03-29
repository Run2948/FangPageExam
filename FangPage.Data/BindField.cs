using System;

namespace FangPage.Data
{
	// Token: 0x02000014 RID: 20
	public sealed class BindField : Attribute
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00005DC5 File Offset: 0x00003FC5
		public BindField()
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005DD4 File Offset: 0x00003FD4
		public BindField(bool isbind)
		{
			this._isBind = isbind;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00005DEA File Offset: 0x00003FEA
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00005DF2 File Offset: 0x00003FF2
		public bool IsBind
		{
			get
			{
				return this._isBind;
			}
			set
			{
				this._isBind = value;
			}
		}

		// Token: 0x04000027 RID: 39
		private bool _isBind = true;
	}
}
