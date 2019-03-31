using System;
using System.Collections;
using System.Data;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using JRO;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200003B RID: 59
	public class DbBll
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0000AA3C File Offset: 0x00008C3C
		public static string BackUpDatabase()
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "backup/datas");
			if (!Directory.Exists(mapPath))
			{
				Directory.CreateDirectory(mapPath);
			}
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
			{
				try
				{
					string str = dbConfig.dbname + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
					if (File.Exists(mapPath + "\\" + str + ".bak"))
					{
						File.Delete(mapPath + "\\" + str + ".bak");
					}
					string commandText = string.Format("backup database {0} to disk='{1}'", dbConfig.dbname, mapPath + "\\" + str + ".bak");
					DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
					return string.Empty;
				}
				catch
				{
					return "对不起，数据库服务器不支持远程备份。";
				}
			}
			string str2 = Path.GetFileNameWithoutExtension(dbConfig.dbpath) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
			string result;
			try
			{
				if (File.Exists(mapPath + "\\" + str2 + ".config"))
				{
					File.Delete(mapPath + "\\" + str2 + ".config");
				}
				using (FPZip fpzip = new FPZip())
				{
					fpzip.AddFile(WebConfig.WebPath + dbConfig.dbpath, dbConfig.dbpath);
					fpzip.ZipSave(mapPath + "\\" + str2 + ".config");
				}
				result = string.Empty;
			}
			catch (Exception ex)
			{
				result = ex.Message.Replace("'", " ").Replace("\n", " ").Replace("\\", "/");
			}
			return result;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000AC24 File Offset: 0x00008E24
		public static string RestoreDatabase(string backupfile)
		{
			if (!File.Exists(backupfile))
			{
				return "备份文件已不存在。";
			}
			if (Path.GetExtension(backupfile) != ".zip" && Path.GetExtension(backupfile) != ".config" && Path.GetExtension(backupfile) != ".bak")
			{
				return "该文件不是数据库备份文件。";
			}
			bool flag = false;
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
			{
				if (Path.GetExtension(backupfile) == ".zip" || Path.GetExtension(backupfile) == ".config")
				{
					flag = true;
					FPZip.UnZip(backupfile, "");
					backupfile = string.Concat(new string[]
					{
						Path.GetDirectoryName(backupfile),
						"\\",
						Path.GetFileName(backupfile),
						"\\",
						dbConfig.dbname,
						".bak"
					});
				}
				if (!File.Exists(backupfile))
				{
					return "备份文件已不存在。";
				}
				try
				{
					DbHelper.ExecuteSql(string.Format("restore database {0} from disk='{1}' ", dbConfig.dbname, backupfile));
					if (flag)
					{
						File.Delete(backupfile);
					}
					return string.Empty;
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + Path.GetDirectoryName(dbConfig.dbpath));
			string result;
			try
			{
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				if (Path.GetExtension(backupfile) == ".zip" || Path.GetExtension(backupfile) == ".config")
				{
					FPZip.UnZip(backupfile, mapPath);
				}
				else
				{
					File.Copy(backupfile, mapPath + "\\" + dbConfig.dbname);
				}
				result = string.Empty;
			}
			catch (Exception ex2)
			{
				result = ex2.Message;
			}
			return result;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public static void ShrinkDatabase()
		{
			if (DbConfigs.GetDbConfig().dbtype == FangPage.Data.DbType.SqlServer)
			{
				DbBll.ShrinkSqlServer();
				return;
			}
			DbBll.ShrinkAccess();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000ADFC File Offset: 0x00008FFC
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

		// Token: 0x060003D8 RID: 984 RVA: 0x0000B058 File Offset: 0x00009258
		public static void ShrinkAccess()
		{
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + dbConfig.dbpath);
			string mapPath2 = FPFile.GetMapPath(WebConfig.WebPath + "cache/temp/");
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
			new JetEngineClass().CompactDatabase(dbConfig.connectionstring, destconnection);
			File.Copy(mapPath2 + Path.GetFileName(mapPath), mapPath, true);
			File.Delete(mapPath2 + Path.GetFileName(mapPath));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B124 File Offset: 0x00009324
		public static long GetDbSize()
		{
			DbConfigInfo dbConfig = DbConfigs.GetDbConfig();
			long num = 0L;
			if (dbConfig.dbtype == FangPage.Data.DbType.SqlServer)
			{
				string commandText = "SELECT OBJECT_NAME(ID) AS TableName,SIZE = sum(reserved) * CONVERT(FLOAT, (SELECT LOW FROM MASTER.DBO.SPT_VALUES WHERE NUMBER = 1 AND TYPE = 'E')) FROM [sysindexes] WHERE [indid] IN (0,1,255) GROUP BY ID ORDER BY SIZE DESC";
                IEnumerator enumerator = DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0].Rows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    object obj = enumerator.Current;
                    DataRow dataRow = (DataRow)obj;
                    num += long.Parse(dataRow["size"].ToString());
                }
                return num;
            }
			num = new FileInfo(FPFile.GetMapPath(WebConfig.WebPath + dbConfig.dbpath)).Length;
			return num;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B1DC File Offset: 0x000093DC
		public static int GetDbType()
		{
			if (DbConfigs.GetDbConfig().dbtype == FangPage.Data.DbType.SqlServer)
			{
				return 0;
			}
			return 1;
		}
	}
}
