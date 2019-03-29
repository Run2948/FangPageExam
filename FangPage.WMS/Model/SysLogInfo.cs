using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000027 RID: 39
	[ModelPrefix("WMS")]
	public class SysLogInfo
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000072EC File Offset: 0x000054EC
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00007304 File Offset: 0x00005504
		[PrimaryKey(true)]
		[Identity(true)]
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00007310 File Offset: 0x00005510
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00007328 File Offset: 0x00005528
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007334 File Offset: 0x00005534
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000734C File Offset: 0x0000554C
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00007358 File Offset: 0x00005558
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00007370 File Offset: 0x00005570
		public string title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000737C File Offset: 0x0000557C
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00007394 File Offset: 0x00005594
		public string description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000073A0 File Offset: 0x000055A0
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000073B8 File Offset: 0x000055B8
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000073C4 File Offset: 0x000055C4
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000073DC File Offset: 0x000055DC
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000073E8 File Offset: 0x000055E8
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00007400 File Offset: 0x00005600
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

		// Token: 0x040000F6 RID: 246
		private int m_id;

		// Token: 0x040000F7 RID: 247
		private int m_uid;

		// Token: 0x040000F8 RID: 248
		private string m_name;

		// Token: 0x040000F9 RID: 249
		private string m_title = string.Empty;

		// Token: 0x040000FA RID: 250
		private string m_description = string.Empty;

		// Token: 0x040000FB RID: 251
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x040000FC RID: 252
		private string m_ip = string.Empty;

		// Token: 0x040000FD RID: 253
		private int m_status;
	}
}
