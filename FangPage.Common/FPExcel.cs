using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace FangPage.Common
{
	// Token: 0x0200000C RID: 12
	public class FPExcel
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003958 File Offset: 0x00001B58
		public static DataTable GetExcelTable(string xlspath)
		{
			return FPExcel.GetExcelTable(xlspath, true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003974 File Offset: 0x00001B74
		public static DataTable GetExcelTable(string xlspath, bool first)
		{
			bool flag = string.IsNullOrEmpty(FPExcel.OleDb);
			if (flag)
			{
				FPExcel.OleDb = "JET";
			}
			string text = "NO";
			bool flag2 = !first;
			if (flag2)
			{
				text = "YES";
			}
			bool flag3 = FPExcel.OleDb.ToUpper() == "JET";
			string connectionString;
			if (flag3)
			{
				connectionString = string.Concat(new string[]
				{
					"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='",
					xlspath,
					"';Extended Properties='Excel 8.0;HDR=",
					text,
					";IMEX=1'"
				});
			}
			else
			{
				connectionString = string.Concat(new string[]
				{
					"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='",
					xlspath,
					"';Extended Properties='Excel 12.0;HDR=",
					text,
					";IMEX=1'"
				});
			}
			DataTable dataTable = new DataTable();
			using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
			{
				try
				{
					oleDbConnection.Open();
					DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
					{
						null,
						null,
						null,
						"Table"
					});
					string[] array = new string[oleDbSchemaTable.Rows.Count];
					for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
					{
						array[i] = oleDbSchemaTable.Rows[i]["TABLE_NAME"].ToString();
					}
					string selectCommandText = "SELECT * FROM [" + array[0] + "]";
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, oleDbConnection);
					oleDbDataAdapter.Fill(dataTable);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			bool flag4 = dataTable.Rows.Count >= 1;
			if (flag4)
			{
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					string text2 = dataTable.Rows[0].ItemArray[j].ToString().Trim();
					bool flag5 = !string.IsNullOrEmpty(text2);
					if (flag5)
					{
						dataTable.Columns[j].ColumnName = text2;
					}
				}
				dataTable.Rows.RemoveAt(0);
			}
			return dataTable;
		}

		// Token: 0x04000023 RID: 35
		private static string OleDb = ConfigurationManager.AppSettings["OLEDB"];
	}
}
