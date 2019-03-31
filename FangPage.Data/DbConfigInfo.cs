using System;
using System.Configuration;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x02000008 RID: 8
	public class DbConfigInfo
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000022ED File Offset: 0x000004ED
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000022F5 File Offset: 0x000004F5
		public string webpath
		{
			get
			{
				return this.m_webpath;
			}
			set
			{
				this.m_webpath = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000022FE File Offset: 0x000004FE
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002306 File Offset: 0x00000506
		public DbType dbtype
		{
			get
			{
				return this.m_dbtype;
			}
			set
			{
				this.m_dbtype = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000230F File Offset: 0x0000050F
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002317 File Offset: 0x00000517
		public string dbpath
		{
			get
			{
				return this.m_dbpath;
			}
			set
			{
				this.m_dbpath = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002320 File Offset: 0x00000520
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002328 File Offset: 0x00000528
		public string dbname
		{
			get
			{
				return this.m_dbname;
			}
			set
			{
				this.m_dbname = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002331 File Offset: 0x00000531
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002339 File Offset: 0x00000539
		public string userid
		{
			get
			{
				return this.m_userid;
			}
			set
			{
				this.m_userid = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002342 File Offset: 0x00000542
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000234A File Offset: 0x0000054A
		public string password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002353 File Offset: 0x00000553
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000235B File Offset: 0x0000055B
		public string prefix
		{
			get
			{
				return this.m_prefix;
			}
			set
			{
				this.m_prefix = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002364 File Offset: 0x00000564
		public string OleDb
		{
			get
			{
				string text = ConfigurationManager.AppSettings["OLEDB"];
				if (string.IsNullOrEmpty(text))
				{
					text = "JET";
				}
				return text.ToUpper();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002398 File Offset: 0x00000598
		public string connectionstring
		{
			get
			{
				if (this.dbtype == DbType.SqlServer)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("Data Source={0}", this.dbpath);
					if (this.dbname != "")
					{
						stringBuilder.AppendFormat(";Initial Catalog={0}", this.dbname);
					}
					if (this.userid != "" && this.password != "")
					{
						stringBuilder.AppendFormat(";User ID={0};Password={1};Pooling=true", this.userid, this.password);
					}
					else
					{
						stringBuilder.Append(";Integrated Security=True");
					}
					this.m_connectionstring = stringBuilder.ToString();
				}
				else
				{
					if (this.OleDb == "JET")
					{
						this.m_connectionstring = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False", DbUtils.GetMapPath(this.webpath + this.dbpath));
					}
					else
					{
						this.m_connectionstring = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=False", DbUtils.GetMapPath(this.webpath + this.dbpath));
					}
					if (this.password != "")
					{
						this.m_connectionstring += string.Format(";Jet OLEDB:Database Password={0}", this.password);
					}
				}
				return this.m_connectionstring;
			}
		}

		// Token: 0x04000014 RID: 20
		private string m_webpath = "/";

		// Token: 0x04000015 RID: 21
		private DbType m_dbtype = DbType.Access;

		// Token: 0x04000016 RID: 22
		private string m_dbpath = "";

		// Token: 0x04000017 RID: 23
		private string m_dbname = "";

		// Token: 0x04000018 RID: 24
		private string m_userid = "";

		// Token: 0x04000019 RID: 25
		private string m_password = "";

		// Token: 0x0400001A RID: 26
		private string m_prefix = "";

		// Token: 0x0400001B RID: 27
		private string m_connectionstring = "";
	}
}
