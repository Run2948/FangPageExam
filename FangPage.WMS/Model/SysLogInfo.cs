using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000019 RID: 25
	[ModelPrefix("WMS")]
	public class SysLogInfo
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00005C71 File Offset: 0x00003E71
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00005C79 File Offset: 0x00003E79
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00005C82 File Offset: 0x00003E82
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00005C8A File Offset: 0x00003E8A
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00005C93 File Offset: 0x00003E93
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00005C9B File Offset: 0x00003E9B
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00005CA4 File Offset: 0x00003EA4
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00005CAC File Offset: 0x00003EAC
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00005CB5 File Offset: 0x00003EB5
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00005CBD File Offset: 0x00003EBD
		public string content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005CC6 File Offset: 0x00003EC6
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00005CCE File Offset: 0x00003ECE
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00005CD7 File Offset: 0x00003ED7
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00005CDF File Offset: 0x00003EDF
		public string ip
		{
			get
			{
				return this.m_ip;
			}
			set
			{
				this.m_ip = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005CE8 File Offset: 0x00003EE8
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00005CF0 File Offset: 0x00003EF0
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

		// Token: 0x04000117 RID: 279
		private int m_id;

		// Token: 0x04000118 RID: 280
		private int m_uid;

		// Token: 0x04000119 RID: 281
		private string m_realname = string.Empty;

		// Token: 0x0400011A RID: 282
		private string m_name = string.Empty;

		// Token: 0x0400011B RID: 283
		private string m_content = string.Empty;

		// Token: 0x0400011C RID: 284
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400011D RID: 285
		private string m_ip = string.Empty;

		// Token: 0x0400011E RID: 286
		private int m_status;
	}
}
