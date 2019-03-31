using System;
using System.Configuration;

namespace FangPage.Data
{
	// Token: 0x02000009 RID: 9
	public class DbConfigs
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002547 File Offset: 0x00000747
		static DbConfigs()
		{
			DbConfigs.ReSet();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000254E File Offset: 0x0000074E
		public static void ReSet()
		{
			DbConfigs.m_dbconfig = DbConfigs.GetRealDbConfig();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000255A File Offset: 0x0000075A
		public static DbConfigInfo GetDbConfig()
		{
			return DbConfigs.m_dbconfig;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002561 File Offset: 0x00000761
		public static string ConnectionString
		{
			get
			{
				return DbConfigs.GetDbConfig().connectionstring;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000256D File Offset: 0x0000076D
		public static string Prefix
		{
			get
			{
				return DbConfigs.GetDbConfig().prefix;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002579 File Offset: 0x00000779
		public static DbType DbType
		{
			get
			{
				return DbConfigs.GetDbConfig().dbtype;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002588 File Offset: 0x00000788
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
				string[] array = DES.Decode(ConfigurationManager.ConnectionStrings["dbconnstring"].ConnectionString).Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = DbUtils.SplitString(array[i], "=", 2);
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

		// Token: 0x0400001C RID: 28
		private static DbConfigInfo m_dbconfig;
	}
}
