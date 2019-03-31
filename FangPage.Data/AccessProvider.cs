using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace FangPage.Data
{
	// Token: 0x0200000B RID: 11
	internal class AccessProvider : IDbProvider
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000AD6A File Offset: 0x00008F6A
		public DbProviderFactory Instance()
		{
			return OleDbFactory.Instance;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000AD71 File Offset: 0x00008F71
		public void DeriveParameters(IDbCommand cmd)
		{
			if (cmd is OleDbCommand)
			{
				OleDbCommandBuilder.DeriveParameters(cmd as OleDbCommand);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000AD86 File Offset: 0x00008F86
		public DbParameter MakeParam(string ParamName, object Value)
		{
			return new OleDbParameter(ParamName, Value);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000AD8F File Offset: 0x00008F8F
		public DbParameter MakeParam(string ParamName, DbType DbType)
		{
			return this.MakeParam(ParamName, DbType, 0);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public DbParameter MakeParam(string ParamName, DbType DbType, int Size)
		{
			OleDbParameter result;
			if (Size > 0)
			{
				result = new OleDbParameter(ParamName, (OleDbType)DbType, Size);
			}
			else
			{
				result = new OleDbParameter(ParamName, (OleDbType)DbType);
			}
			return result;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000ADC1 File Offset: 0x00008FC1
		public string GetLastIdSql()
		{
			return "SELECT @@IDENTITY";
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public string ExecuteSql(string sqlstring)
		{
			string text = string.Empty;
			if (sqlstring != "")
			{
				OleDbConnection oleDbConnection = new OleDbConnection(DbConfigs.ConnectionString);
				oleDbConnection.Open();
				OleDbCommand oleDbCommand = new OleDbCommand();
				oleDbCommand.Connection = oleDbConnection;
				oleDbCommand.CommandType = CommandType.Text;
				foreach (string text2 in sqlstring.Split(new string[]
				{
					"GO\r\n",
					"Go\r\n",
					"go\r\n",
					"|",
					";"
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					using (OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction())
					{
						oleDbCommand.Transaction = oleDbTransaction;
						if (!(text2.Trim() == ""))
						{
							try
							{
								oleDbCommand.CommandText = text2;
								oleDbCommand.ExecuteNonQuery();
								oleDbTransaction.Commit();
							}
							catch (Exception ex)
							{
								oleDbTransaction.Rollback();
								text += ex.Message;
								break;
							}
						}
					}
				}
				oleDbConnection.Close();
			}
			return text;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public string RunSql(string sqlstring)
		{
			string text = string.Empty;
			if (sqlstring != "")
			{
				OleDbConnection oleDbConnection = new OleDbConnection(DbConfigs.ConnectionString);
				oleDbConnection.Open();
				OleDbCommand oleDbCommand = new OleDbCommand();
				oleDbCommand.Connection = oleDbConnection;
				oleDbCommand.CommandType = CommandType.Text;
				foreach (string text2 in sqlstring.Split(new string[]
				{
					"GO\r\n",
					"Go\r\n",
					"go\r\n"
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					using (OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction())
					{
						oleDbCommand.Transaction = oleDbTransaction;
						if (!(text2.Trim() == ""))
						{
							try
							{
								oleDbCommand.CommandText = text2;
								oleDbCommand.ExecuteNonQuery();
								oleDbTransaction.Commit();
							}
							catch (Exception ex)
							{
								oleDbTransaction.Rollback();
								text += ex.Message;
								break;
							}
						}
					}
				}
				oleDbConnection.Close();
			}
			return text;
		}
	}
}
