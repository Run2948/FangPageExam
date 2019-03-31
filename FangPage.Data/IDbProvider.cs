using System;
using System.Data;
using System.Data.Common;

namespace FangPage.Data
{
	// Token: 0x0200000F RID: 15
	public interface IDbProvider
	{
		// Token: 0x06000104 RID: 260
		DbProviderFactory Instance();

		// Token: 0x06000105 RID: 261
		void DeriveParameters(IDbCommand cmd);

		// Token: 0x06000106 RID: 262
		DbParameter MakeParam(string ParamName, object Value);

		// Token: 0x06000107 RID: 263
		DbParameter MakeParam(string ParamName, DbType DbType);

		// Token: 0x06000108 RID: 264
		DbParameter MakeParam(string ParamName, DbType DbType, int Size);

		// Token: 0x06000109 RID: 265
		string GetLastIdSql();

		// Token: 0x0600010A RID: 266
		string ExecuteSql(string sqlstring);

		// Token: 0x0600010B RID: 267
		string RunSql(string sqlstring);
	}
}
