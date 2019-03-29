using System;
using System.Data;
using System.Data.OleDb;

namespace FangPage.MVC
{
	// Token: 0x02000002 RID: 2
	public class FPExcel
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static DataTable GetExcelTable(string xlspath)
		{
			string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + xlspath + "';Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
			DataTable dataTable = new DataTable();
			using (OleDbConnection oleDbConnection = new OleDbConnection(connectionString))
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
			return dataTable;
		}
	}
}
