using System;

namespace FangPage.WMS.Config
{
	// Token: 0x02000025 RID: 37
	public class PluginConfig
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000069AE File Offset: 0x00004BAE
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x000069B6 File Offset: 0x00004BB6
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

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000069BF File Offset: 0x00004BBF
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x000069C7 File Offset: 0x00004BC7
		public string guid
		{
			get
			{
				return this.m_guid;
			}
			set
			{
				this.m_guid = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000069D0 File Offset: 0x00004BD0
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00006A1D File Offset: 0x00004C1D
		public string markup
		{
			get
			{
				if (this.m_markup == "" && this.installpath != "")
				{
					this.m_markup = "plugins_" + this.installpath;
				}
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00006A26 File Offset: 0x00004C26
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00006A2E File Offset: 0x00004C2E
		public string platform
		{
			get
			{
				return this.m_platform;
			}
			set
			{
				this.m_platform = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00006A37 File Offset: 0x00004C37
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00006A3F File Offset: 0x00004C3F
		public string installpath
		{
			get
			{
				return this.m_installpath;
			}
			set
			{
				this.m_installpath = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00006A48 File Offset: 0x00004C48
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00006A50 File Offset: 0x00004C50
		public string author
		{
			get
			{
				return this.m_author;
			}
			set
			{
				this.m_author = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00006A59 File Offset: 0x00004C59
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00006A61 File Offset: 0x00004C61
		public string version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				this.m_version = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00006A6A File Offset: 0x00004C6A
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00006A72 File Offset: 0x00004C72
		public string dll
		{
			get
			{
				return this.m_dll;
			}
			set
			{
				this.m_dll = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00006A7B File Offset: 0x00004C7B
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00006A83 File Offset: 0x00004C83
		public string adminurl
		{
			get
			{
				return this.m_adminurl;
			}
			set
			{
				this.m_adminurl = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00006A8C File Offset: 0x00004C8C
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00006A94 File Offset: 0x00004C94
		public string indexurl
		{
			get
			{
				return this.m_indexurl;
			}
			set
			{
				this.m_indexurl = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00006A9D File Offset: 0x00004C9D
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00006AA5 File Offset: 0x00004CA5
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00006AAE File Offset: 0x00004CAE
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00006AB6 File Offset: 0x00004CB6
		public string homepage
		{
			get
			{
				return this.m_homepage;
			}
			set
			{
				this.m_homepage = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00006ABF File Offset: 0x00004CBF
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00006AC7 File Offset: 0x00004CC7
		public string notes
		{
			get
			{
				return this.m_notes;
			}
			set
			{
				this.m_notes = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00006AD0 File Offset: 0x00004CD0
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public string createdate
		{
			get
			{
				return this.m_createdate;
			}
			set
			{
				this.m_createdate = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00006AE1 File Offset: 0x00004CE1
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x00006AE9 File Offset: 0x00004CE9
		public string updatedate
		{
			get
			{
				return this.m_updatedate;
			}
			set
			{
				this.m_updatedate = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00006AF2 File Offset: 0x00004CF2
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x00006AFA File Offset: 0x00004CFA
		public string size
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x04000167 RID: 359
		private string m_name = "";

		// Token: 0x04000168 RID: 360
		private string m_guid = "";

		// Token: 0x04000169 RID: 361
		private string m_markup = string.Empty;

		// Token: 0x0400016A RID: 362
		private string m_platform = string.Empty;

		// Token: 0x0400016B RID: 363
		private string m_installpath = "";

		// Token: 0x0400016C RID: 364
		private string m_author = "方配";

		// Token: 0x0400016D RID: 365
		private string m_version = "1.0.0";

		// Token: 0x0400016E RID: 366
		private string m_dll = "";

		// Token: 0x0400016F RID: 367
		private string m_adminurl = "";

		// Token: 0x04000170 RID: 368
		private string m_indexurl = "";

		// Token: 0x04000171 RID: 369
		private string m_icon = "";

		// Token: 0x04000172 RID: 370
		private string m_homepage = "";

		// Token: 0x04000173 RID: 371
		private string m_notes = "";

		// Token: 0x04000174 RID: 372
		private string m_createdate = string.Empty;

		// Token: 0x04000175 RID: 373
		private string m_updatedate = string.Empty;

		// Token: 0x04000176 RID: 374
		private string m_size = string.Empty;
	}
}
