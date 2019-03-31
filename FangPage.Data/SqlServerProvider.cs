using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace FangPage.Data
{
	// Token: 0x0200000C RID: 12
	internal class SqlServerProvider : IDbProvider
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public DbProviderFactory Instance()
		{
			return SqlClientFactory.Instance;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public void DeriveParameters(IDbCommand cmd)
		{
			if (cmd is SqlCommand)
			{
				SqlCommandBuilder.DeriveParameters(cmd as SqlCommand);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000B00C File Offset: 0x0000920C
		public DbParameter MakeParam(string ParamName, object Value)
		{
			return new SqlParameter(ParamName, Value);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000B015 File Offset: 0x00009215
		public DbParameter MakeParam(string ParamName, DbType DbType)
		{
			return this.MakeParam(ParamName, DbType, 0);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000B020 File Offset: 0x00009220
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

		// Token: 0x060000E8 RID: 232 RVA: 0x0000B045 File Offset: 0x00009245
		public string GetLastIdSql()
		{
			return "SELECT SCOPE_IDENTITY()";
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000B04C File Offset: 0x0000924C
		public string ExecuteSql(string sqlstring)
		{
			string text = string.Empty;
			if (sqlstring != "")
			{
				SqlConnection sqlConnection = new SqlConnection(DbConfigs.ConnectionString);
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandType = CommandType.Text;
				foreach (string text2 in sqlstring.Split(new string[]
				{
					"GO\r\n",
					"Go\r\n",
					"go\r\n",
					"|",
					";"
				}, StringSplitOptions.RemoveEmptyEntries))
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

		// Token: 0x060000EA RID: 234 RVA: 0x0000B168 File Offset: 0x00009368
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
				foreach (string text2 in sqlstring.Split(new string[]
				{
					"GO\r\n",
					"Go\r\n",
					"go\r\n"
				}, StringSplitOptions.RemoveEmptyEntries))
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
