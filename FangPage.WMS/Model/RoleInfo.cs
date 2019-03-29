using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000026 RID: 38
	[ModelPrefix("WMS")]
	public class RoleInfo
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00007128 File Offset: 0x00005328
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00007140 File Offset: 0x00005340
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000714C File Offset: 0x0000534C
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00007164 File Offset: 0x00005364
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007170 File Offset: 0x00005370
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00007188 File Offset: 0x00005388
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007194 File Offset: 0x00005394
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000071AC File Offset: 0x000053AC
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000071B8 File Offset: 0x000053B8
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000071D0 File Offset: 0x000053D0
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000071DC File Offset: 0x000053DC
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000071F4 File Offset: 0x000053F4
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007200 File Offset: 0x00005400
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007218 File Offset: 0x00005418
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007224 File Offset: 0x00005424
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000723C File Offset: 0x0000543C
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007248 File Offset: 0x00005448
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00007260 File Offset: 0x00005460
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000726C File Offset: 0x0000546C
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007284 File Offset: 0x00005484
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007290 File Offset: 0x00005490
		// (set) Token: 0x060001EB RID: 491 RVA: 0x000072A8 File Offset: 0x000054A8
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

		// Token: 0x040000EB RID: 235
		private int m_id;

		// Token: 0x040000EC RID: 236
		private string m_name = string.Empty;

		// Token: 0x040000ED RID: 237
		private string m_description = string.Empty;

		// Token: 0x040000EE RID: 238
		private string m_desktopurl = string.Empty;

		// Token: 0x040000EF RID: 239
		private string m_sorts = string.Empty;

		// Token: 0x040000F0 RID: 240
		private string m_menus = string.Empty;

		// Token: 0x040000F1 RID: 241
		private string m_desktop = string.Empty;

		// Token: 0x040000F2 RID: 242
		private string m_permission = string.Empty;

		// Token: 0x040000F3 RID: 243
		private int m_isadmin;

		// Token: 0x040000F4 RID: 244
		private int m_isupload;

		// Token: 0x040000F5 RID: 245
		private int m_isdownload;
	}
}
