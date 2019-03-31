using System;
using FangPage.Common;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001A RID: 26
	[ModelPrefix("WMS")]
	public class TypeInfo
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005D38 File Offset: 0x00003F38
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00005D40 File Offset: 0x00003F40
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00005D49 File Offset: 0x00003F49
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00005D51 File Offset: 0x00003F51
		public int parentid
		{
			get
			{
				return this.m_parentid;
			}
			set
			{
				this.m_parentid = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00005D5A File Offset: 0x00003F5A
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00005D62 File Offset: 0x00003F62
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00005D6B File Offset: 0x00003F6B
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00005D73 File Offset: 0x00003F73
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00005D7C File Offset: 0x00003F7C
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00005D84 File Offset: 0x00003F84
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00005D8D File Offset: 0x00003F8D
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00005D95 File Offset: 0x00003F95
		public int required
		{
			get
			{
				return this.m_required;
			}
			set
			{
				this.m_required = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00005D9E File Offset: 0x00003F9E
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00005DA6 File Offset: 0x00003FA6
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005DAF File Offset: 0x00003FAF
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00005DB7 File Offset: 0x00003FB7
		public string otherurl
		{
			get
			{
				return this.m_otherurl;
			}
			set
			{
				this.m_otherurl = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00005DC0 File Offset: 0x00003FC0
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00005DC8 File Offset: 0x00003FC8
		public int display
		{
			get
			{
				return this.m_display;
			}
			set
			{
				this.m_display = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00005DD1 File Offset: 0x00003FD1
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00005DD9 File Offset: 0x00003FD9
		public string img
		{
			get
			{
				return this.m_img;
			}
			set
			{
				this.m_img = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00005DE2 File Offset: 0x00003FE2
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00005DEA File Offset: 0x00003FEA
		public string attach_img
		{
			get
			{
				return this.m_attach_img;
			}
			set
			{
				this.m_attach_img = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00005DF3 File Offset: 0x00003FF3
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00005DFB File Offset: 0x00003FFB
		public int subcounts
		{
			get
			{
				return this.m_subcounts;
			}
			set
			{
				this.m_subcounts = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00005E04 File Offset: 0x00004004
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00005E0C File Offset: 0x0000400C
		public string sortids
		{
			get
			{
				return this.m_sortids;
			}
			set
			{
				this.m_sortids = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00005E15 File Offset: 0x00004015
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00005E1D File Offset: 0x0000401D
		public string posts
		{
			get
			{
				return this.m_posts;
			}
			set
			{
				this.m_posts = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00005E26 File Offset: 0x00004026
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00005E2E File Offset: 0x0000402E
		[BindField(false)]
		public int sortid
		{
			get
			{
				return this.m_sortid;
			}
			set
			{
				this.m_sortid = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005E38 File Offset: 0x00004038
		[BindField(false)]
		public int post
		{
			get
			{
				this.m_post = FPArray.GetInt(this.sortids, this.posts, this.sortid.ToString());
				return this.m_post;
			}
		}

		// Token: 0x0400011F RID: 287
		private int m_id;

		// Token: 0x04000120 RID: 288
		private int m_parentid;

		// Token: 0x04000121 RID: 289
		private string m_name = string.Empty;

		// Token: 0x04000122 RID: 290
		private string m_markup = string.Empty;

		// Token: 0x04000123 RID: 291
		private int m_type;

		// Token: 0x04000124 RID: 292
		private int m_required;

		// Token: 0x04000125 RID: 293
		private string m_description = string.Empty;

		// Token: 0x04000126 RID: 294
		private string m_otherurl = string.Empty;

		// Token: 0x04000127 RID: 295
		private int m_display;

		// Token: 0x04000128 RID: 296
		private string m_img = string.Empty;

		// Token: 0x04000129 RID: 297
		private string m_attach_img = string.Empty;

		// Token: 0x0400012A RID: 298
		private int m_subcounts;

		// Token: 0x0400012B RID: 299
		private string m_sortids = string.Empty;

		// Token: 0x0400012C RID: 300
		private string m_posts = string.Empty;

		// Token: 0x0400012D RID: 301
		private int m_sortid;

		// Token: 0x0400012E RID: 302
		private int m_post;
	}
}
