using System;
using System.Data;
using System.Data.Common;

namespace FangPage.Data
{
	// Token: 0x02000008 RID: 8
	public interface IDbProvider
	{
		// Token: 0x06000093 RID: 147
		DbProviderFactory Instance();

		// Token: 0x06000094 RID: 148
		void DeriveParameters(IDbCommand cmd);

		// Token: 0x06000095 RID: 149
		DbParameter MakeParam(string ParamName, object Value);

		// Token: 0x06000096 RID: 150
		DbParameter MakeParam(string ParamName, DbType DbType);

		// Token: 0x06000097 RID: 151
		DbParameter MakeParam(string ParamName, DbType DbType, int Size);

		// Token: 0x06000098 RID: 152
		string GetLastIdSql();

		// Token: 0x06000099 RID: 153
		string RunSql(string sqlstring);
	}
}
