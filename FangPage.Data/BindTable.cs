using System;

namespace FangPage.Data
{
	// Token: 0x02000006 RID: 6
	public class BindTable : Attribute
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002271 File Offset: 0x00000471
		public BindTable()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002284 File Offset: 0x00000484
		public BindTable(string tablename)
		{
			this._tablename = tablename;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000229E File Offset: 0x0000049E
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022A6 File Offset: 0x000004A6
		public string TableName
		{
			get
			{
				return this._tablename;
			}
			set
			{
				this._tablename = value;
			}
		}

		// Token: 0x04000012 RID: 18
		private string _tablename = "";
	}
}
