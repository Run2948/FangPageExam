using System;

namespace FangPage.Data
{
	// Token: 0x02000018 RID: 24
	public class SqlParam
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000C047 File Offset: 0x0000A247
		public SqlParam()
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000C05A File Offset: 0x0000A25A
		public SqlParam(SqlType SqlType, string ParamName, object Value)
		{
			this.SqlType = SqlType;
			this.ParamName = ParamName;
			this.Value = Value;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000C082 File Offset: 0x0000A282
		public SqlParam(SqlType SqlType, string ParamName, WhereType WhereType, object Value)
		{
			this.SqlType = SqlType;
			this.ParamName = ParamName;
			this.WhereType = WhereType;
			this.Value = Value;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000C0B2 File Offset: 0x0000A2B2
		public SqlParam(string ParamName, OrderBy orderby)
		{
			this.SqlType = SqlType.OrderBy;
			this.ParamName = ParamName;
			this.Value = orderby.ToString();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		public SqlType SqlType
		{
			get
			{
				return this.m_SqlType;
			}
			set
			{
				this.m_SqlType = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000C0FF File Offset: 0x0000A2FF
		public string ParamName
		{
			get
			{
				return this.m_ParamName;
			}
			set
			{
				this.m_ParamName = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000C108 File Offset: 0x0000A308
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000C110 File Offset: 0x0000A310
		public WhereType WhereType
		{
			get
			{
				return this.m_WhereType;
			}
			set
			{
				this.m_WhereType = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000C11C File Offset: 0x0000A31C
		public string WhereTypeStr
		{
			get
			{
				string result = "";
				switch (this.WhereType)
				{
				case WhereType.Equal:
					result = "=";
					break;
				case WhereType.NotEqual:
					result = "<>";
					break;
				case WhereType.GreaterThan:
					result = ">";
					break;
				case WhereType.GreaterThanEqual:
					result = ">=";
					break;
				case WhereType.LessThan:
					result = "<";
					break;
				case WhereType.LessThanEqual:
					result = "<=";
					break;
				case WhereType.Like:
					result = "LIKE";
					break;
				case WhereType.NotLike:
					result = "NOT LIKE";
					break;
				case WhereType.In:
					result = "IN";
					break;
				case WhereType.NotIn:
					result = "NOT IN";
					break;
				}
				return result;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000C1B5 File Offset: 0x0000A3B5
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000C1BD File Offset: 0x0000A3BD
		public object Value { get; set; }

		// Token: 0x0400003E RID: 62
		private SqlType m_SqlType;

		// Token: 0x0400003F RID: 63
		private string m_ParamName = "";

		// Token: 0x04000040 RID: 64
		private WhereType m_WhereType;
	}
}
