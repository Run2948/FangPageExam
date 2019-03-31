using System;

namespace FangPage.Data
{
	// Token: 0x02000007 RID: 7
	public class ModelPrefix : Attribute
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000022AF File Offset: 0x000004AF
		public ModelPrefix()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000022C2 File Offset: 0x000004C2
		public ModelPrefix(string prefix)
		{
			this._prefix = prefix;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022DC File Offset: 0x000004DC
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000022E4 File Offset: 0x000004E4
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
			set
			{
				this._prefix = value;
			}
		}

		// Token: 0x04000013 RID: 19
		private string _prefix = "";
	}
}
