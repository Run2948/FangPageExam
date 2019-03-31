using System;

namespace FangPage.Data
{
	// Token: 0x02000003 RID: 3
	public class LeftJoin : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000211F File Offset: 0x0000031F
		public LeftJoin()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000213D File Offset: 0x0000033D
		public LeftJoin(string tablename, string field)
		{
			this._tablename = tablename;
			this._field = field;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002169 File Offset: 0x00000369
		public LeftJoin(string tablename, int number, string field)
		{
			this._tablename = tablename;
			this._number = number;
			this._field = field;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000219C File Offset: 0x0000039C
		public string TableName
		{
			get
			{
				return this._tablename;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021A4 File Offset: 0x000003A4
		public int Number
		{
			get
			{
				return this._number;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021AC File Offset: 0x000003AC
		public string Field
		{
			get
			{
				return this._field;
			}
		}

		// Token: 0x0400000B RID: 11
		private string _tablename = "";

		// Token: 0x0400000C RID: 12
		private int _number;

		// Token: 0x0400000D RID: 13
		private string _field = "";
	}
}
