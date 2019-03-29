using System;

namespace FangPage.Data
{
	// Token: 0x0200000F RID: 15
	public sealed class NText : Attribute
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000058A6 File Offset: 0x00003AA6
		public NText()
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000058B5 File Offset: 0x00003AB5
		public NText(bool isntext)
		{
			this._isntext = isntext;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000058CB File Offset: 0x00003ACB
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000058D3 File Offset: 0x00003AD3
		public bool IsNText
		{
			get
			{
				return this._isntext;
			}
			set
			{
				this._isntext = value;
			}
		}

		// Token: 0x04000017 RID: 23
		private bool _isntext = true;
	}
}
