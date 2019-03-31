using System;

namespace FangPage.WMS.Config
{
	// Token: 0x02000023 RID: 35
	public class AppConfig
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00006687 File Offset: 0x00004887
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000668F File Offset: 0x0000488F
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00006698 File Offset: 0x00004898
		// (set) Token: 0x06000283 RID: 643 RVA: 0x000066A0 File Offset: 0x000048A0
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000066AC File Offset: 0x000048AC
		// (set) Token: 0x06000285 RID: 645 RVA: 0x000066F9 File Offset: 0x000048F9
		public string markup
		{
			get
			{
				if (this.m_markup == "" && this.installpath != "")
				{
					this.m_markup = "app_" + this.installpath;
				}
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00006702 File Offset: 0x00004902
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000670A File Offset: 0x0000490A
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00006713 File Offset: 0x00004913
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000671B File Offset: 0x0000491B
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00006724 File Offset: 0x00004924
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000672C File Offset: 0x0000492C
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00006735 File Offset: 0x00004935
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000673D File Offset: 0x0000493D
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00006746 File Offset: 0x00004946
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000674E File Offset: 0x0000494E
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00006757 File Offset: 0x00004957
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000675F File Offset: 0x0000495F
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00006768 File Offset: 0x00004968
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00006770 File Offset: 0x00004970
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00006779 File Offset: 0x00004979
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00006781 File Offset: 0x00004981
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000678A File Offset: 0x0000498A
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00006792 File Offset: 0x00004992
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000679B File Offset: 0x0000499B
		// (set) Token: 0x06000299 RID: 665 RVA: 0x000067A3 File Offset: 0x000049A3
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600029A RID: 666 RVA: 0x000067AC File Offset: 0x000049AC
		// (set) Token: 0x0600029B RID: 667 RVA: 0x000067B4 File Offset: 0x000049B4
		public string sortapps
		{
			get
			{
				return this.m_sortapps;
			}
			set
			{
				this.m_sortapps = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000067BD File Offset: 0x000049BD
		// (set) Token: 0x0600029D RID: 669 RVA: 0x000067C5 File Offset: 0x000049C5
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000067CE File Offset: 0x000049CE
		// (set) Token: 0x0600029F RID: 671 RVA: 0x000067D6 File Offset: 0x000049D6
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000067DF File Offset: 0x000049DF
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000067E7 File Offset: 0x000049E7
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000067F0 File Offset: 0x000049F0
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x000067F8 File Offset: 0x000049F8
		public int width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00006801 File Offset: 0x00004A01
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00006809 File Offset: 0x00004A09
		public int height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x0400014D RID: 333
		private string m_name = string.Empty;

		// Token: 0x0400014E RID: 334
		private string m_guid = string.Empty;

		// Token: 0x0400014F RID: 335
		private string m_markup = string.Empty;

		// Token: 0x04000150 RID: 336
		private string m_platform = string.Empty;

		// Token: 0x04000151 RID: 337
		private string m_installpath = string.Empty;

		// Token: 0x04000152 RID: 338
		private string m_author = "方配";

		// Token: 0x04000153 RID: 339
		private string m_version = "1.0.0";

		// Token: 0x04000154 RID: 340
		private string m_adminurl = "";

		// Token: 0x04000155 RID: 341
		private string m_indexurl = "";

		// Token: 0x04000156 RID: 342
		private string m_icon = "";

		// Token: 0x04000157 RID: 343
		private string m_homepage = string.Empty;

		// Token: 0x04000158 RID: 344
		private string m_notes = string.Empty;

		// Token: 0x04000159 RID: 345
		private string m_dll = string.Empty;

		// Token: 0x0400015A RID: 346
		private string m_sortapps = string.Empty;

		// Token: 0x0400015B RID: 347
		private string m_createdate = string.Empty;

		// Token: 0x0400015C RID: 348
		private string m_updatedate = string.Empty;

		// Token: 0x0400015D RID: 349
		private string m_size = string.Empty;

		// Token: 0x0400015E RID: 350
		private int m_width;

		// Token: 0x0400015F RID: 351
		private int m_height;
	}
}
