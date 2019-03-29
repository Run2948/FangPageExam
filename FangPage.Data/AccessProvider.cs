using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace FangPage.Data
{
	// Token: 0x02000009 RID: 9
	internal class AccessProvider : IDbProvider
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00005152 File Offset: 0x00003352
		public DbProviderFactory Instance()
		{
			return OleDbFactory.Instance;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005159 File Offset: 0x00003359
		public void DeriveParameters(IDbCommand cmd)
		{
			if (cmd is OleDbCommand)
			{
				OleDbCommandBuilder.DeriveParameters(cmd as OleDbCommand);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000516E File Offset: 0x0000336E
		public DbParameter MakeParam(string ParamName, object Value)
		{
			return new OleDbParameter(ParamName, Value);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005177 File Offset: 0x00003377
		public DbParameter MakeParam(string ParamName, DbType DbType)
		{
			return this.MakeParam(ParamName, DbType, 0);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005184 File Offset: 0x00003384
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

		// Token: 0x0600009F RID: 159 RVA: 0x000051A9 File Offset: 0x000033A9
		public string GetLastIdSql()
		{
			return "SELECT @@IDENTITY";
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000051B0 File Offset: 0x000033B0
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
				string[] array = sqlstring.Split(new string[]
				{
					"GO\r\n",
					"Go\r\n",
					"go\r\n",
					"|"
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
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
