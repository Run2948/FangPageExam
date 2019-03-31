using System;
using FangPage.Data;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Model
{
	// Token: 0x02000016 RID: 22
	[ModelPrefix("WMS")]
	public class SortInfo
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005875 File Offset: 0x00003A75
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000587D File Offset: 0x00003A7D
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005886 File Offset: 0x00003A86
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000588E File Offset: 0x00003A8E
		public int channelid
		{
			get
			{
				return this.m_channelid;
			}
			set
			{
				this.m_channelid = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005897 File Offset: 0x00003A97
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000589F File Offset: 0x00003A9F
		public int appid
		{
			get
			{
				return this.m_appid;
			}
			set
			{
				this.m_appid = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000058A8 File Offset: 0x00003AA8
		[BindField(false)]
		public SortAppInfo SortAppInfo
		{
			get
			{
				if (this.m_sortappinfo == null)
				{
					this.m_sortappinfo = SortBll.GetSortAppInfo(this.appid);
				}
				return this.m_sortappinfo;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000058C9 File Offset: 0x00003AC9
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000058D1 File Offset: 0x00003AD1
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000058DA File Offset: 0x00003ADA
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x000058E2 File Offset: 0x00003AE2
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000058EB File Offset: 0x00003AEB
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000058F3 File Offset: 0x00003AF3
		public string parentlist
		{
			get
			{
				return this.m_parentlist;
			}
			set
			{
				this.m_parentlist = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000058FC File Offset: 0x00003AFC
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00005904 File Offset: 0x00003B04
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000590D File Offset: 0x00003B0D
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005915 File Offset: 0x00003B15
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000591E File Offset: 0x00003B1E
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00005926 File Offset: 0x00003B26
		public int pagesize
		{
			get
			{
				return this.m_pagesize;
			}
			set
			{
				this.m_pagesize = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000592F File Offset: 0x00003B2F
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005937 File Offset: 0x00003B37
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005940 File Offset: 0x00003B40
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005948 File Offset: 0x00003B48
		public string icon
		{
			get
			{
				return this.m_icon;
			}
			set
			{
				this.m_icon = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005951 File Offset: 0x00003B51
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00005959 File Offset: 0x00003B59
		public string attach_icon
		{
			get
			{
				return this.m_attach_icon;
			}
			set
			{
				this.m_attach_icon = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005962 File Offset: 0x00003B62
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000596A File Offset: 0x00003B6A
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005973 File Offset: 0x00003B73
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000597B File Offset: 0x00003B7B
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005984 File Offset: 0x00003B84
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000598C File Offset: 0x00003B8C
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005995 File Offset: 0x00003B95
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000599D File Offset: 0x00003B9D
		public string types
		{
			get
			{
				return this.m_types;
			}
			set
			{
				this.m_types = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000059A6 File Offset: 0x00003BA6
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000059AE File Offset: 0x00003BAE
		public int showtype
		{
			get
			{
				return this.m_showtype;
			}
			set
			{
				this.m_showtype = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000059B7 File Offset: 0x00003BB7
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x000059BF File Offset: 0x00003BBF
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000059C8 File Offset: 0x00003BC8
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x000059D0 File Offset: 0x00003BD0
		public int posts
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

		// Token: 0x040000EE RID: 238
		private int m_id;

		// Token: 0x040000EF RID: 239
		private int m_channelid;

		// Token: 0x040000F0 RID: 240
		private int m_appid;

		// Token: 0x040000F1 RID: 241
		private int m_display;

		// Token: 0x040000F2 RID: 242
		private int m_parentid;

		// Token: 0x040000F3 RID: 243
		private string m_parentlist = string.Empty;

		// Token: 0x040000F4 RID: 244
		private string m_name = string.Empty;

		// Token: 0x040000F5 RID: 245
		private string m_markup = string.Empty;

		// Token: 0x040000F6 RID: 246
		private int m_pagesize = 20;

		// Token: 0x040000F7 RID: 247
		private string m_description = string.Empty;

		// Token: 0x040000F8 RID: 248
		private string m_icon = string.Empty;

		// Token: 0x040000F9 RID: 249
		private string m_attach_icon = string.Empty;

		// Token: 0x040000FA RID: 250
		private string m_img = string.Empty;

		// Token: 0x040000FB RID: 251
		private string m_attach_img = string.Empty;

		// Token: 0x040000FC RID: 252
		private int m_subcounts;

		// Token: 0x040000FD RID: 253
		private string m_types = string.Empty;

		// Token: 0x040000FE RID: 254
		private int m_showtype = 1;

		// Token: 0x040000FF RID: 255
		private string m_otherurl = string.Empty;

		// Token: 0x04000100 RID: 256
		private int m_posts;

		// Token: 0x04000101 RID: 257
		private SortAppInfo m_sortappinfo;
	}
}
