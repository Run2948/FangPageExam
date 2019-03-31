using System;

namespace FangPage.Data
{
	// Token: 0x02000002 RID: 2
	public class AttrInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public bool IsIdentity
		{
			get
			{
				return this.m_isidentity;
			}
			set
			{
				this.m_isidentity = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public bool IsPrimaryKey
		{
			get
			{
				return this.m_isprimarykey;
			}
			set
			{
				this.m_isprimarykey = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public bool IsBindField
		{
			get
			{
				return this.m_isbindfield;
			}
			set
			{
				this.m_isbindfield = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public bool IsLeftJoin
		{
			get
			{
				return this.m_isleftjoin;
			}
			set
			{
				this.m_isleftjoin = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		public bool IsMap
		{
			get
			{
				return this.m_ismap;
			}
			set
			{
				this.m_ismap = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020AD File Offset: 0x000002AD
		public bool IsTempField
		{
			get
			{
				return this.m_istempfield;
			}
			set
			{
				this.m_istempfield = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B6 File Offset: 0x000002B6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020BE File Offset: 0x000002BE
		public bool IsNtext
		{
			get
			{
				return this.m_ntext;
			}
			set
			{
				this.m_ntext = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020CF File Offset: 0x000002CF
		public string TableName
		{
			get
			{
				return this.m_tablename;
			}
			set
			{
				this.m_tablename = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020E0 File Offset: 0x000002E0
		public int Number
		{
			get
			{
				return this.m_number;
			}
			set
			{
				this.m_number = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000020F1 File Offset: 0x000002F1
		public string ColName
		{
			get
			{
				return this.m_colname;
			}
			set
			{
				this.m_colname = value;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool m_isidentity;

		// Token: 0x04000002 RID: 2
		private bool m_isprimarykey;

		// Token: 0x04000003 RID: 3
		private bool m_isbindfield = true;

		// Token: 0x04000004 RID: 4
		private bool m_isleftjoin;

		// Token: 0x04000005 RID: 5
		private bool m_ismap;

		// Token: 0x04000006 RID: 6
		private bool m_istempfield;

		// Token: 0x04000007 RID: 7
		private bool m_ntext;

		// Token: 0x04000008 RID: 8
		private string m_tablename = string.Empty;

		// Token: 0x04000009 RID: 9
		private int m_number;

		// Token: 0x0400000A RID: 10
		private string m_colname = string.Empty;
	}
}
