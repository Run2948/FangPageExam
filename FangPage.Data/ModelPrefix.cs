using System;

namespace FangPage.Data
{
	// Token: 0x02000003 RID: 3
	public class ModelPrefix : Attribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000208E File Offset: 0x0000028E
		public ModelPrefix()
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A1 File Offset: 0x000002A1
		public ModelPrefix(string prefix)
		{
			this._prefix = prefix;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020BB File Offset: 0x000002BB
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020C3 File Offset: 0x000002C3
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

		// Token: 0x04000002 RID: 2
		private string _prefix = "";
	}
}
