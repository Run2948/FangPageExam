using System;

namespace FangPage.Data
{
	// Token: 0x02000016 RID: 22
	public class SqlParam
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00005DFB File Offset: 0x00003FFB
		public SqlParam()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005E0E File Offset: 0x0000400E
		public SqlParam(SqlType SqlType, string ParamName, object Value)
		{
			this.SqlType = SqlType;
			this.ParamName = ParamName;
			this.Value = Value;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005E36 File Offset: 0x00004036
		public SqlParam(SqlType SqlType, string ParamName, WhereType WhereType, object Value)
		{
			this.SqlType = SqlType;
			this.ParamName = ParamName;
			this.WhereType = WhereType;
			this.Value = Value;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005E66 File Offset: 0x00004066
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005E6E File Offset: 0x0000406E
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005E77 File Offset: 0x00004077
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005E7F File Offset: 0x0000407F
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005E88 File Offset: 0x00004088
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005E90 File Offset: 0x00004090
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005E9C File Offset: 0x0000409C
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005F35 File Offset: 0x00004135
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00005F3D File Offset: 0x0000413D
		public object Value { get; set; }

		// Token: 0x0400002D RID: 45
		private SqlType m_SqlType;

		// Token: 0x0400002E RID: 46
		private string m_ParamName = "";

		// Token: 0x0400002F RID: 47
		private WhereType m_WhereType;
	}
}
