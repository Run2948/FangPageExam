using System;
using System.Data;
using System.IO;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000002 RID: 2
	public class dbbackup : SuperController
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					string @string = FPRequest.GetString("chkdel");
					foreach (string str in @string.Split(new char[]
					{
						','
					}))
					{
						if (File.Exists(dbbackup.backuppath + "\\" + str))
						{
							File.Delete(dbbackup.backuppath + "\\" + str);
						}
					}
				}
				else if (this.action == "restore")
				{
					string backupfile = dbbackup.backuppath + "\\" + FPRequest.GetString("restore");
					string text = DbBll.RestoreDatabase(backupfile);
				}
				else if (this.action == "backup")
				{
					string text = DbBll.BackUpDatabase();
					if (text == string.Empty)
					{
						this.ShowErr(text);
						return;
					}
				}
			}
			this.dbfilelist = this.GetDbFileList();
			base.SaveRightURL();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000219C File Offset: 0x0000039C
		private DataTable CreateDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					{
						"id",
						Type.GetType("System.Int32")
					},
					{
						"filename",
						Type.GetType("System.String")
					},
					{
						"size",
						Type.GetType("System.String")
					},
					{
						"createtime",
						Type.GetType("System.String")
					},
					{
						"fullname",
						Type.GetType("System.String")
					}
				}
			};
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000223C File Offset: 0x0000043C
		public DataTable GetDbFileList()
		{
			DataTable dataTable = this.CreateDataTable();
			if (Directory.Exists(dbbackup.backuppath))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dbbackup.backuppath);
				int num = 1;
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if (fileInfo.Extension == ".config" || fileInfo.Extension == ".zip")
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["id"] = num;
						dataRow["filename"] = fileInfo.Name;
						dataRow["size"] = FPUtils.FormatBytesStr(fileInfo.Length);
						dataRow["createtime"] = fileInfo.CreationTime.ToString();
						dataRow["fullname"] = "backup/datas/" + fileInfo.Name;
						dataTable.Rows.Add(dataRow);
						num++;
					}
				}
			}
			return dataTable;
		}

		// Token: 0x04000001 RID: 1
		private static string backuppath = FPUtils.GetMapPath(WebConfig.WebPath + "backup/datas");

		// Token: 0x04000002 RID: 2
		protected DataTable dbfilelist = new DataTable();
	}
}
