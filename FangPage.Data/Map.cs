using System;

namespace FangPage.Data
{
	// Token: 0x02000004 RID: 4
	public class Map : Attribute
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000021B4 File Offset: 0x000003B4
		public Map()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021D2 File Offset: 0x000003D2
		public Map(string tablename, string field)
		{
			this._tablename = tablename;
			this._field = field;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021FE File Offset: 0x000003FE
		public Map(string tablename, int number, string field)
		{
			this._tablename = tablename;
			this._number = number;
			this._field = field;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002231 File Offset: 0x00000431
		public string TableName
		{
			get
			{
				return this._tablename;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002239 File Offset: 0x00000439
		public int Number
		{
			get
			{
				return this._number;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002241 File Offset: 0x00000441
		public string Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x0400000E RID: 14
		private string _tablename = "";

		// Token: 0x0400000F RID: 15
		private int _number;

		// Token: 0x04000010 RID: 16
		private string _field = "";
	}
}
