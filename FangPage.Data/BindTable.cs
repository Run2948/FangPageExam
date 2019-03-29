using System;

namespace FangPage.Data
{
	// Token: 0x02000002 RID: 2
	public class BindTable : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public BindTable()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		public BindTable(string tablename)
		{
			this._tablename = tablename;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000207D File Offset: 0x0000027D
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002085 File Offset: 0x00000285
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

		// Token: 0x04000001 RID: 1
		private string _tablename = "";
	}
}
