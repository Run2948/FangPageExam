using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000017 RID: 23
	[ModelPrefix("WMS")]
	public class Permission
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005A6C File Offset: 0x00003C6C
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00005A74 File Offset: 0x00003C74
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00005A7D File Offset: 0x00003C7D
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00005A85 File Offset: 0x00003C85
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005A8E File Offset: 0x00003C8E
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00005A96 File Offset: 0x00003C96
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005A9F File Offset: 0x00003C9F
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00005AA7 File Offset: 0x00003CA7
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005AB0 File Offset: 0x00003CB0
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00005AB8 File Offset: 0x00003CB8
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005AC1 File Offset: 0x00003CC1
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00005AC9 File Offset: 0x00003CC9
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005AD2 File Offset: 0x00003CD2
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00005ADA File Offset: 0x00003CDA
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00005AE3 File Offset: 0x00003CE3
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00005AEB File Offset: 0x00003CEB
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

		// Token: 0x04000102 RID: 258
		private int m_id;

		// Token: 0x04000103 RID: 259
		private string m_name = string.Empty;

		// Token: 0x04000104 RID: 260
		private string m_flagpage = string.Empty;

		// Token: 0x04000105 RID: 261
		private int m_isadd;

		// Token: 0x04000106 RID: 262
		private int m_isupdate;

		// Token: 0x04000107 RID: 263
		private int m_isdelete;

		// Token: 0x04000108 RID: 264
		private int m_isaudit;

		// Token: 0x04000109 RID: 265
		private int m_status = 1;
	}
}
