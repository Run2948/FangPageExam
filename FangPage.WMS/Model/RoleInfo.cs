using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000018 RID: 24
	[ModelPrefix("WMS")]
	public class RoleInfo
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005B19 File Offset: 0x00003D19
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00005B21 File Offset: 0x00003D21
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005B2A File Offset: 0x00003D2A
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00005B32 File Offset: 0x00003D32
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005B3B File Offset: 0x00003D3B
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00005B43 File Offset: 0x00003D43
		public string markup
		{
			get
			{
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00005B4C File Offset: 0x00003D4C
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00005B54 File Offset: 0x00003D54
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005B5D File Offset: 0x00003D5D
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00005B65 File Offset: 0x00003D65
		public string desktopurl
		{
			get
			{
				return this.m_desktopurl;
			}
			set
			{
				this.m_desktopurl = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00005B6E File Offset: 0x00003D6E
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00005B76 File Offset: 0x00003D76
		public string sorts
		{
			get
			{
				return this.m_sorts;
			}
			set
			{
				this.m_sorts = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005B7F File Offset: 0x00003D7F
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x00005B87 File Offset: 0x00003D87
		public string menus
		{
			get
			{
				return this.m_menus;
			}
			set
			{
				this.m_menus = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00005B90 File Offset: 0x00003D90
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00005B98 File Offset: 0x00003D98
		public string desktop
		{
			get
			{
				return this.m_desktop;
			}
			set
			{
				this.m_desktop = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005BA1 File Offset: 0x00003DA1
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00005BA9 File Offset: 0x00003DA9
		public string permission
		{
			get
			{
				return this.m_permission;
			}
			set
			{
				this.m_permission = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005BB2 File Offset: 0x00003DB2
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00005BBA File Offset: 0x00003DBA
		public int isadmin
		{
			get
			{
				return this.m_isadmin;
			}
			set
			{
				this.m_isadmin = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005BC3 File Offset: 0x00003DC3
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00005BCB File Offset: 0x00003DCB
		public int isupload
		{
			get
			{
				return this.m_isupload;
			}
			set
			{
				this.m_isupload = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00005BD4 File Offset: 0x00003DD4
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00005BDC File Offset: 0x00003DDC
		public int isdownload
		{
			get
			{
				return this.m_isdownload;
			}
			set
			{
				this.m_isdownload = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00005BE5 File Offset: 0x00003DE5
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00005BED File Offset: 0x00003DED
		public int issms
		{
			get
			{
				return this.m_issms;
			}
			set
			{
				this.m_issms = value;
			}
		}

		// Token: 0x0400010A RID: 266
		private int m_id;

		// Token: 0x0400010B RID: 267
		private string m_name = string.Empty;

		// Token: 0x0400010C RID: 268
		private string m_markup = string.Empty;

		// Token: 0x0400010D RID: 269
		private string m_description = string.Empty;

		// Token: 0x0400010E RID: 270
		private string m_desktopurl = string.Empty;

		// Token: 0x0400010F RID: 271
		private string m_sorts = string.Empty;

		// Token: 0x04000110 RID: 272
		private string m_menus = string.Empty;

		// Token: 0x04000111 RID: 273
		private string m_desktop = string.Empty;

		// Token: 0x04000112 RID: 274
		private string m_permission = string.Empty;

		// Token: 0x04000113 RID: 275
		private int m_isadmin;

		// Token: 0x04000114 RID: 276
		private int m_isupload = 1;

		// Token: 0x04000115 RID: 277
		private int m_isdownload = 1;

		// Token: 0x04000116 RID: 278
		private int m_issms;
	}
}
