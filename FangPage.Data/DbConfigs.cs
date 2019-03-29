using System;
using System.Configuration;

namespace FangPage.Data
{
	// Token: 0x02000005 RID: 5
	public class DbConfigs
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000022B7 File Offset: 0x000004B7
		static DbConfigs()
		{
			DbConfigs.ReSet();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000022BE File Offset: 0x000004BE
		public static void ReSet()
		{
			DbConfigs.m_dbconfig = DbConfigs.GetRealDbConfig();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022CA File Offset: 0x000004CA
		public static DbConfigInfo GetDbConfig()
		{
			return DbConfigs.m_dbconfig;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022D1 File Offset: 0x000004D1
		public static string ConnectionString
		{
			get
			{
				return DbConfigs.GetDbConfig().connectionstring;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022DD File Offset: 0x000004DD
		public static string Prefix
		{
			get
			{
				return DbConfigs.GetDbConfig().prefix;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022E9 File Offset: 0x000004E9
		public static DbType DbType
		{
			get
			{
				return DbConfigs.GetDbConfig().dbtype;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022F8 File Offset: 0x000004F8
		public static DbConfigInfo GetRealDbConfig()
		{
			DbConfigInfo dbConfigInfo = new DbConfigInfo();
			try
			{
				if (ConfigurationManager.AppSettings["webpath"] != null)
				{
					dbConfigInfo.webpath = ConfigurationManager.AppSettings["webpath"];
				}
				if (string.IsNullOrEmpty(dbConfigInfo.webpath))
				{
					dbConfigInfo.webpath = "/";
				}
				if (!dbConfigInfo.webpath.StartsWith("/"))
				{
					dbConfigInfo.webpath = "/" + dbConfigInfo.webpath;
				}
				if (!dbConfigInfo.webpath.EndsWith("/"))
				{
					DbConfigInfo dbConfigInfo2 = dbConfigInfo;
					dbConfigInfo2.webpath += "/";
				}
				string text = DES.Decode(ConfigurationManager.ConnectionStrings["dbconnstring"].ConnectionString);
				foreach (string strContent in text.Split(new char[]
				{
					';'
				}))
				{
					string[] array2 = DbUtils.SplitString(strContent, "=", 2);
					if (array2[0].ToLower() == "sqlserver")
					{
						dbConfigInfo.dbtype = DbType.SqlServer;
						dbConfigInfo.dbpath = array2[1];
					}
					if (array2[0].ToLower() == "access")
					{
						dbConfigInfo.dbtype = DbType.Access;
						dbConfigInfo.dbpath = array2[1];
						if (dbConfigInfo.dbpath.StartsWith("/"))
						{
							dbConfigInfo.dbpath = dbConfigInfo.dbpath.Substring(1, dbConfigInfo.dbpath.Length - 1);
						}
					}
					if (array2[0].ToLower() == "dbname")
					{
						dbConfigInfo.dbname = array2[1];
					}
					if (array2[0].ToLower() == "userid")
					{
						dbConfigInfo.userid = array2[1];
					}
					if (array2[0].ToLower() == "password")
					{
						dbConfigInfo.password = array2[1];
					}
					if (array2[0].ToLower() == "prefix")
					{
						dbConfigInfo.prefix = array2[1];
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return dbConfigInfo;
		}

		// Token: 0x0400000B RID: 11
		private static DbConfigInfo m_dbconfig;
	}
}
