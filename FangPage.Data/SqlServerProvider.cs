using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace FangPage.Data
{
	// Token: 0x0200000A RID: 10
	internal class SqlServerProvider : IDbProvider
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000052DC File Offset: 0x000034DC
		public DbProviderFactory Instance()
		{
			return SqlClientFactory.Instance;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000052E3 File Offset: 0x000034E3
		public void DeriveParameters(IDbCommand cmd)
		{
			if (cmd is SqlCommand)
			{
				SqlCommandBuilder.DeriveParameters(cmd as SqlCommand);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000052F8 File Offset: 0x000034F8
		public DbParameter MakeParam(string ParamName, object Value)
		{
			return new SqlParameter(ParamName, Value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005301 File Offset: 0x00003501
		public DbParameter MakeParam(string ParamName, DbType DbType)
		{
			return this.MakeParam(ParamName, DbType, 0);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000530C File Offset: 0x0000350C
		public DbParameter MakeParam(string ParamName, DbType DbType, int Size)
		{
			SqlParameter result;
			if (Size > 0)
			{
				result = new SqlParameter(ParamName, (SqlDbType)DbType, Size);
			}
			else
			{
				result = new SqlParameter(ParamName, (SqlDbType)DbType);
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005331 File Offset: 0x00003531
		public string GetLastIdSql()
		{
			return "SELECT SCOPE_IDENTITY()";
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005338 File Offset: 0x00003538
		public string RunSql(string sqlstring)
		{
			string text = string.Empty;
			if (sqlstring != "")
			{
				SqlConnection sqlConnection = new SqlConnection(DbConfigs.ConnectionString);
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandType = CommandType.Text;
				string[] array = sqlstring.Split(new string[]
				{
					"GO\r\n",
					"Go\r\n",
					"go\r\n",
					"|"
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
				{
					using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
					{
						sqlCommand.Transaction = sqlTransaction;
						if (!(text2.Trim() == ""))
						{
							try
							{
								sqlCommand.CommandText = text2;
								sqlCommand.ExecuteNonQuery();
								sqlTransaction.Commit();
							}
							catch (Exception ex)
							{
								sqlTransaction.Rollback();
								text += ex.Message;
								break;
							}
						}
					}
				}
				sqlConnection.Close();
			}
			return text;
		}
	}
}
