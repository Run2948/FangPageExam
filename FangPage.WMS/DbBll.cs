using System;
using System.Data;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using JRO;
using SQLDMO;

namespace FangPage.WMS
{
	// Token: 0x02000033 RID: 51
	public class DbBll
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public static string BackUpDatabase()
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "backup/datas");
			if (!Directory.Exists(mapPath))
			{
				Directory.CreateDirectory(mapPath);
			}
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			string str = dbConfig.dbname + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
			if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
			{
				SQLServer sqlserver = new SQLServerClass();
				sqlserver.Connect(dbConfig.dbpath, dbConfig.userid, dbConfig.password);
				try
				{
					if (File.Exists(mapPath + "\\" + dbConfig.dbname + ".bak"))
					{
						File.Delete(mapPath + "\\" + dbConfig.dbname + ".bak");
					}
					((_Backup)new BackupClass
					{
						Action = SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database,
						Initialize = true,
						Files = mapPath + "\\" + dbConfig.dbname + ".bak",
						Database = dbConfig.dbname
					}).SQLBackup(sqlserver);
					if (File.Exists(mapPath + "\\" + str + ".config"))
					{
						using (FPZip fpzip = new FPZip())
						{
							fpzip.AddFile(mapPath + "\\" + dbConfig.dbname + ".bak", dbConfig.dbname + ".bak");
							fpzip.ZipSave(mapPath + "\\" + str + ".config");
						}
						File.Delete(mapPath + "\\" + dbConfig.dbname + ".bak");
					}
					return string.Empty;
				}
				catch (Exception ex)
				{
					string text = ex.Message.Replace("'", " ");
					text = text.Replace("\n", " ");
					return text.Replace("\\", "/");
				}
				finally
				{
					sqlserver.DisConnect();
				}
			}
			string result;
			try
			{
				if (File.Exists(mapPath + "\\" + str + ".config"))
				{
					File.Delete(mapPath + "\\" + str + ".zip");
					using (FPZip fpzip = new FPZip())
					{
						fpzip.AddFile(WebConfig.WebPath + dbConfig.dbpath, dbConfig.dbpath);
						fpzip.ZipSave(mapPath + "\\" + str + ".config");
					}
				}
				result = string.Empty;
			}
			catch (Exception ex)
			{
				string text = ex.Message.Replace("'", " ");
				text = text.Replace("\n", " ");
				text = text.Replace("\\", "/");
				result = text;
			}
			return result;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008E34 File Offset: 0x00007034
		public static string RestoreDatabase(string backupfile)
		{
			string result;
			if (!File.Exists(backupfile))
			{
				result = "备份文件已不存在。";
			}
			else
			{
				DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
				if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
				{
					SQLServer sqlserver = new SQLServerClass();
					if (Path.GetExtension(backupfile) == ".zip" || Path.GetExtension(backupfile) == ".config")
					{
						FPZip.UnZipFile(backupfile, "");
					}
					backupfile = string.Concat(new string[]
					{
						Path.GetDirectoryName(backupfile),
						"\\",
						Path.GetFileName(backupfile),
						"\\",
						dbConfig.dbname,
						".bak"
					});
					if (!File.Exists(backupfile))
					{
						return "备份文件已不存在。";
					}
					try
					{
						sqlserver.Connect(dbConfig.dbpath, dbConfig.userid, dbConfig.password);
						QueryResults queryResults = sqlserver.EnumProcesses(-1);
						int num = -1;
						int num2 = -1;
						for (int i = 1; i <= queryResults.Columns; i++)
						{
							string text = queryResults.get_ColumnName(i);
							if (text.ToUpper().Trim() == "SPID")
							{
								num = i;
							}
							else if (text.ToUpper().Trim() == "DBNAME")
							{
								num2 = i;
							}
							if (num != -1 && num2 != -1)
							{
								break;
							}
						}
						for (int i = 1; i <= queryResults.Rows; i++)
						{
							int columnLong = queryResults.GetColumnLong(i, num);
							string columnString = queryResults.GetColumnString(i, num2);
							if (columnString.ToUpper() == dbConfig.dbname.ToUpper())
							{
								sqlserver.KillProcess(columnLong);
							}
						}
						((_Restore)new RestoreClass
						{
							Action = SQLDMO_RESTORE_TYPE.SQLDMORestore_Database,
							Files = backupfile,
							Database = dbConfig.dbname,
							ReplaceDatabase = true
						}).SQLRestore(sqlserver);
						File.Delete(backupfile);
						return string.Empty;
					}
					catch (Exception ex)
					{
						return ex.Message;
					}
					finally
					{
						sqlserver.DisConnect();
					}
				}
				string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + Path.GetDirectoryName(dbConfig.dbpath));
				try
				{
					if (!Directory.Exists(mapPath))
					{
						Directory.CreateDirectory(mapPath);
					}
					if (Path.GetExtension(backupfile) == ".zip" || Path.GetExtension(backupfile) == ".config")
					{
						FPZip.UnZipFile(backupfile, mapPath);
					}
					else
					{
						File.Copy(backupfile, mapPath + "\\" + dbConfig.dbname);
					}
					result = string.Empty;
				}
				catch (Exception ex)
				{
					result = ex.Message;
				}
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009190 File Offset: 0x00007390
		public static void ShrinkDatabase()
		{
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
			{
				DbBll.ShrinkSqlServer();
			}
			else
			{
				DbBll.ShrinkAccess();
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000091C8 File Offset: 0x000073C8
		public static void ShrinkSqlServer()
		{
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			string text = "";
			string dbname = dbConfig.dbname;
			text += "SET NOCOUNT ON ";
			text += "DECLARE @LogicalFileName sysname, @MaxMinutes INT, @NewSize INT ";
			text = text + "USE [" + dbname + "] -- 要操作的数据库名 ";
			text = text + "SELECT @LogicalFileName = '" + dbname + "_log', -- 日志文件名 ";
			text += "@MaxMinutes = 10, -- Limit on time allowed to wrap log. ";
			text += "@NewSize = 1 -- 你想设定的日志文件的大小(M) ";
			text += "-- Setup / initialize ";
			text += "DECLARE @OriginalSize int ";
			text += "SELECT @OriginalSize = 0";
			text += "FROM sysfiles ";
			text += "WHERE name = @LogicalFileName ";
			text += "SELECT 'Original Size of ' + db_name() + ' LOG is ' + ";
			text += "CONVERT(VARCHAR(30),@OriginalSize) + ' 8K pages or ' + ";
			text += "CONVERT(VARCHAR(30),(@OriginalSize*8/1024)) + 'MB' ";
			text += "FROM sysfiles ";
			text += "WHERE name = @LogicalFileName ";
			text += "CREATE TABLE DummyTrans ";
			text += "(DummyColumn char (8000) not null) ";
			text += "DECLARE @Counter INT, ";
			text += "@StartTime DATETIME, ";
			text += "@TruncLog VARCHAR(255) ";
			text += "SELECT @StartTime = GETDATE(), ";
			text += "@TruncLog = 'BACKUP LOG ' + db_name() + ' WITH TRUNCATE_ONLY' ";
			text += "DBCC SHRINKFILE (@LogicalFileName, @NewSize) ";
			text += "EXEC (@TruncLog) ";
			text += "-- Wrap the log if necessary. ";
			text += "WHILE @MaxMinutes > DATEDIFF (mi, @StartTime, GETDATE()) -- time has not expired ";
			text += "AND @OriginalSize = (SELECT size FROM sysfiles WHERE name = @LogicalFileName) ";
			text += "AND (@OriginalSize * 8 /1024) > @NewSize ";
			text += "BEGIN -- Outer loop. ";
			text += "SELECT @Counter = 0 ";
			text += "WHILE ((@Counter < @OriginalSize / 16) AND (@Counter < 50000)) ";
			text += "BEGIN -- update ";
			text += "INSERT DummyTrans VALUES ('Fill Log') ";
			text += "DELETE DummyTrans ";
			text += "SELECT @Counter = @Counter + 1 ";
			text += "END ";
			text += "EXEC (@TruncLog) ";
			text += "END ";
			text += "SELECT 'Final Size of ' + db_name() + ' LOG is ' + ";
			text += "CONVERT(VARCHAR(30),size) + ' 8K pages or ' + ";
			text += "CONVERT(VARCHAR(30),(size*8/1024)) + 'MB' ";
			text += "FROM sysfiles ";
			text += "WHERE name = @LogicalFileName ";
			text += "DROP TABLE DummyTrans ";
			text += "SET NOCOUNT OFF ";
			DbHelper.ExecuteSql(text);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009424 File Offset: 0x00007624
		public static void ShrinkAccess()
		{
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + dbConfig.dbpath);
			string mapPath2 = FPUtils.GetMapPath(WebConfig.WebPath + "cache/temp/");
			if (!File.Exists(mapPath))
			{
				throw new Exception("目标数据库不存在,无法压缩");
			}
			if (!Directory.Exists(mapPath2))
			{
				Directory.CreateDirectory(mapPath2);
			}
			if (File.Exists(mapPath2 + Path.GetFileName(mapPath)))
			{
				File.Delete(mapPath2 + Path.GetFileName(mapPath));
			}
			string destconnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mapPath2 + Path.GetFileName(mapPath);
			JetEngineClass jetEngineClass = new JetEngineClass();
			jetEngineClass.CompactDatabase(dbConfig.connectionstring, destconnection);
			File.Copy(mapPath2 + Path.GetFileName(mapPath), mapPath, true);
			File.Delete(mapPath2 + Path.GetFileName(mapPath));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000950C File Offset: 0x0000770C
		public static long GetDbSize()
		{
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			long num = 0L;
			if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
			{
				string commandText = "SELECT OBJECT_NAME(ID) AS TableName,SIZE = sum(reserved) * CONVERT(FLOAT, (SELECT LOW FROM MASTER.DBO.SPT_VALUES WHERE NUMBER = 1 AND TYPE = 'E')) FROM [sysindexes] WHERE [indid] IN (0,1,255) GROUP BY ID ORDER BY SIZE DESC";
				DataTable dataTable = DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					num += long.Parse(dataRow["size"].ToString());
				}
			}
			else
			{
				FileInfo fileInfo = new FileInfo(FPUtils.GetMapPath(WebConfig.WebPath + dbConfig.dbpath));
				num = fileInfo.Length;
			}
			return num;
		}
	}
}
