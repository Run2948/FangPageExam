using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200000C RID: 12
	[ModelPrefix("WMS")]
	public class CreditInfo
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000043DC File Offset: 0x000025DC
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000043E4 File Offset: 0x000025E4
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000043ED File Offset: 0x000025ED
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000043F5 File Offset: 0x000025F5
		[LeftJoin("WMS_UserInfo", "id")]
		public int uid
		{
			get
			{
				return this.m_uid;
			}
			set
			{
				this.m_uid = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000043FE File Offset: 0x000025FE
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004406 File Offset: 0x00002606
		[Map("WMS_UserInfo", "realname")]
		public string realname
		{
			get
			{
				return this.m_realname;
			}
			set
			{
				this.m_realname = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000440F File Offset: 0x0000260F
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004417 File Offset: 0x00002617
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004420 File Offset: 0x00002620
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004428 File Offset: 0x00002628
		public int type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004431 File Offset: 0x00002631
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004439 File Offset: 0x00002639
		public int credits
		{
			get
			{
				return this.m_credits;
			}
			set
			{
				this.m_credits = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004442 File Offset: 0x00002642
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000444A File Offset: 0x0000264A
		public DateTime postdatetime
		{
			get
			{
				return this.m_postdatetime;
			}
			set
			{
				this.m_postdatetime = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004453 File Offset: 0x00002653
		// (set) Token: 0x06000089 RID: 137 RVA: 0x0000445B File Offset: 0x0000265B
		public int doid
		{
			get
			{
				return this.m_doid;
			}
			set
			{
				this.m_doid = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004464 File Offset: 0x00002664
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000446C File Offset: 0x0000266C
		public string doname
		{
			get
			{
				return this.m_doname;
			}
			set
			{
				this.m_doname = value;
			}
		}

		// Token: 0x04000058 RID: 88
		private int m_id;

		// Token: 0x04000059 RID: 89
		private int m_uid;

		// Token: 0x0400005A RID: 90
		private string m_realname = string.Empty;

		// Token: 0x0400005B RID: 91
		private string m_name = string.Empty;

		// Token: 0x0400005C RID: 92
		private int m_type;

		// Token: 0x0400005D RID: 93
		private int m_credits;

		// Token: 0x0400005E RID: 94
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400005F RID: 95
		private int m_doid;

		// Token: 0x04000060 RID: 96
		private string m_doname;
	}
}
