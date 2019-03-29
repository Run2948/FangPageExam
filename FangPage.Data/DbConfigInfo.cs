using System;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x02000004 RID: 4
	public class DbConfigInfo
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020CC File Offset: 0x000002CC
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020D4 File Offset: 0x000002D4
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020DD File Offset: 0x000002DD
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020E5 File Offset: 0x000002E5
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020EE File Offset: 0x000002EE
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020F6 File Offset: 0x000002F6
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020FF File Offset: 0x000002FF
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002107 File Offset: 0x00000307
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002110 File Offset: 0x00000310
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002118 File Offset: 0x00000318
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002121 File Offset: 0x00000321
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002129 File Offset: 0x00000329
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002132 File Offset: 0x00000332
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000213A File Offset: 0x0000033A
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002144 File Offset: 0x00000344
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
					this.m_connectionstring = string.Format("Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Persist Security Info=True", DbUtils.GetMapPath(this.webpath + this.dbpath));
					if (this.password != "")
					{
						this.m_connectionstring += string.Format(";Jet OLEDB:Database Password={0}", this.password);
					}
				}
				return this.m_connectionstring;
			}
		}

		// Token: 0x04000003 RID: 3
		private string m_webpath = "/";

		// Token: 0x04000004 RID: 4
		private DbType m_dbtype = DbType.Access;

		// Token: 0x04000005 RID: 5
		private string m_dbpath = "";

		// Token: 0x04000006 RID: 6
		private string m_dbname = "";

		// Token: 0x04000007 RID: 7
		private string m_userid = "";

		// Token: 0x04000008 RID: 8
		private string m_password = "";

		// Token: 0x04000009 RID: 9
		private string m_prefix = "";

		// Token: 0x0400000A RID: 10
		private string m_connectionstring = "";
	}
}
