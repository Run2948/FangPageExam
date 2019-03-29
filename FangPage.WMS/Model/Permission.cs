using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000025 RID: 37
	[ModelPrefix("WMS")]
	public class Permission
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006FA4 File Offset: 0x000051A4
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00006FBC File Offset: 0x000051BC
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006FC8 File Offset: 0x000051C8
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00006FE0 File Offset: 0x000051E0
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006FEC File Offset: 0x000051EC
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00007004 File Offset: 0x00005204
		public string flagpage
		{
			get
			{
				return this.m_flagpage;
			}
			set
			{
				this.m_flagpage = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00007010 File Offset: 0x00005210
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00007028 File Offset: 0x00005228
		public int isadd
		{
			get
			{
				return this.m_isadd;
			}
			set
			{
				this.m_isadd = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00007034 File Offset: 0x00005234
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000704C File Offset: 0x0000524C
		public int isupdate
		{
			get
			{
				return this.m_isupdate;
			}
			set
			{
				this.m_isupdate = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00007058 File Offset: 0x00005258
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00007070 File Offset: 0x00005270
		public int isdelete
		{
			get
			{
				return this.m_isdelete;
			}
			set
			{
				this.m_isdelete = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000707C File Offset: 0x0000527C
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00007094 File Offset: 0x00005294
		public int isaudit
		{
			get
			{
				return this.m_isaudit;
			}
			set
			{
				this.m_isaudit = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000070A0 File Offset: 0x000052A0
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000070B8 File Offset: 0x000052B8
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

		// Token: 0x040000E3 RID: 227
		private int m_id;

		// Token: 0x040000E4 RID: 228
		private string m_name = string.Empty;

		// Token: 0x040000E5 RID: 229
		private string m_flagpage = string.Empty;

		// Token: 0x040000E6 RID: 230
		private int m_isadd;

		// Token: 0x040000E7 RID: 231
		private int m_isupdate;

		// Token: 0x040000E8 RID: 232
		private int m_isdelete;

		// Token: 0x040000E9 RID: 233
		private int m_isaudit;

		// Token: 0x040000EA RID: 234
		private int m_status = 1;
	}
}
