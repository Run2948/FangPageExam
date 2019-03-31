using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200000A RID: 10
	[ModelPrefix("WMS")]
	public class CacheInfo
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004286 File Offset: 0x00002486
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000428E File Offset: 0x0000248E
		[Identity(true)]
		[PrimaryKey(true)]
		public int id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004297 File Offset: 0x00002497
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000429F File Offset: 0x0000249F
		public string name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000042A8 File Offset: 0x000024A8
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000042B0 File Offset: 0x000024B0
		public string key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000042B9 File Offset: 0x000024B9
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000042C1 File Offset: 0x000024C1
		public int expires
		{
			get
			{
				return this.m_expires;
			}
			set
			{
				this.m_expires = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000042CA File Offset: 0x000024CA
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000042D2 File Offset: 0x000024D2
		public DateTime cachedatetime
		{
			get
			{
				return this.m_cachedatetime;
			}
			set
			{
				this.m_cachedatetime = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000042DB File Offset: 0x000024DB
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000042E3 File Offset: 0x000024E3
		public string file
		{
			get
			{
				return this.m_file;
			}
			set
			{
				this.m_file = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000042EC File Offset: 0x000024EC
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000042F4 File Offset: 0x000024F4
		[BindField(false)]
		public int status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000042FD File Offset: 0x000024FD
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00004305 File Offset: 0x00002505
		[BindField(false)]
		public object obj
		{
			get
			{
				return this.m_obj;
			}
			set
			{
				this.m_obj = value;
			}
		}

		// Token: 0x0400004A RID: 74
		private int m_id;

		// Token: 0x0400004B RID: 75
		private string m_name = string.Empty;

		// Token: 0x0400004C RID: 76
		private string m_key = string.Empty;

		// Token: 0x0400004D RID: 77
		private int m_expires;

		// Token: 0x0400004E RID: 78
		private DateTime m_cachedatetime = DbUtils.GetDateTime();

		// Token: 0x0400004F RID: 79
		private string m_file = string.Empty;

		// Token: 0x04000050 RID: 80
		private int m_status;

		// Token: 0x04000051 RID: 81
		private object m_obj;
	}
}
