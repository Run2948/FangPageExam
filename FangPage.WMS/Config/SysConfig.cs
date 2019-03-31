using System;

namespace FangPage.WMS.Config
{
	// Token: 0x02000027 RID: 39
	public class SysConfig
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00006D8E File Offset: 0x00004F8E
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00006D96 File Offset: 0x00004F96
		public string passwordkey
		{
			get
			{
				return this.m_passwordkey;
			}
			set
			{
				this.m_passwordkey = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00006D9F File Offset: 0x00004F9F
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00006DA7 File Offset: 0x00004FA7
		public string cookiedomain
		{
			get
			{
				return this.m_cookiedomain;
			}
			set
			{
				this.m_cookiedomain = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00006DB0 File Offset: 0x00004FB0
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public int onlinefrequency
		{
			get
			{
				if (this.m_onlinefrequency < 1)
				{
					this.m_onlinefrequency = 2;
				}
				return this.m_onlinefrequency;
			}
			set
			{
				this.m_onlinefrequency = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00006DD1 File Offset: 0x00004FD1
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00006DD9 File Offset: 0x00004FD9
		public int browsecreatesite
		{
			get
			{
				return this.m_browsecreatesite;
			}
			set
			{
				this.m_browsecreatesite = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00006DE2 File Offset: 0x00004FE2
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00006DEA File Offset: 0x00004FEA
		public string admintitle
		{
			get
			{
				return this.m_admintitle;
			}
			set
			{
				this.m_admintitle = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00006DF3 File Offset: 0x00004FF3
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00006DFB File Offset: 0x00004FFB
		public string platname
		{
			get
			{
				return this.m_platname;
			}
			set
			{
				this.m_platname = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00006E04 File Offset: 0x00005004
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00006E0C File Offset: 0x0000500C
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

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00006E15 File Offset: 0x00005015
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00006E1D File Offset: 0x0000501D
		public string adminpath
		{
			get
			{
				return this.m_adminpath;
			}
			set
			{
				this.m_adminpath = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00006E26 File Offset: 0x00005026
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00006E2E File Offset: 0x0000502E
		public string verifypage
		{
			get
			{
				return this.m_verifypage;
			}
			set
			{
				this.m_verifypage = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00006E37 File Offset: 0x00005037
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00006E3F File Offset: 0x0000503F
		public string customerrors
		{
			get
			{
				return this.m_customerrors;
			}
			set
			{
				this.m_customerrors = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00006E48 File Offset: 0x00005048
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00006E50 File Offset: 0x00005050
		public int onlinetimeout
		{
			get
			{
				return this.m_onlinetimeout;
			}
			set
			{
				this.m_onlinetimeout = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00006E59 File Offset: 0x00005059
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00006E61 File Offset: 0x00005061
		public int allowlog
		{
			get
			{
				return this.m_allowlog;
			}
			set
			{
				this.m_allowlog = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00006E6A File Offset: 0x0000506A
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00006E72 File Offset: 0x00005072
		public int attachimgmaxwidth
		{
			get
			{
				return this.m_attachimgmaxwidth;
			}
			set
			{
				this.m_attachimgmaxwidth = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00006E7B File Offset: 0x0000507B
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00006E83 File Offset: 0x00005083
		public int attachimgmaxheight
		{
			get
			{
				return this.m_attachimgmaxheight;
			}
			set
			{
				this.m_attachimgmaxheight = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00006E8C File Offset: 0x0000508C
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00006E94 File Offset: 0x00005094
		public int attachimgquality
		{
			get
			{
				return this.m_attachimgquality;
			}
			set
			{
				this.m_attachimgquality = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00006E9D File Offset: 0x0000509D
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00006EA5 File Offset: 0x000050A5
		public int thumbnailwidth
		{
			get
			{
				return this.m_thumbnailwidth;
			}
			set
			{
				this.m_thumbnailwidth = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00006EAE File Offset: 0x000050AE
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00006EB6 File Offset: 0x000050B6
		public int thumbnailheight
		{
			get
			{
				return this.m_thumbnailheight;
			}
			set
			{
				this.m_thumbnailheight = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00006EBF File Offset: 0x000050BF
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00006EC7 File Offset: 0x000050C7
		public int allowwatermark
		{
			get
			{
				return this.m_allowwatermark;
			}
			set
			{
				this.m_allowwatermark = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00006ED0 File Offset: 0x000050D0
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00006ED8 File Offset: 0x000050D8
		public int watermarkopacity
		{
			get
			{
				return this.m_watermarkopacity;
			}
			set
			{
				this.m_watermarkopacity = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00006EE1 File Offset: 0x000050E1
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00006EE9 File Offset: 0x000050E9
		public string watermarkpic
		{
			get
			{
				return this.m_watermarkpic;
			}
			set
			{
				this.m_watermarkpic = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00006EF2 File Offset: 0x000050F2
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00006EFA File Offset: 0x000050FA
		public int watermarkstatus
		{
			get
			{
				return this.m_watermarkstatus;
			}
			set
			{
				this.m_watermarkstatus = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00006F03 File Offset: 0x00005103
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00006F0B File Offset: 0x0000510B
		public int ssocheck
		{
			get
			{
				return this.m_ssocheck;
			}
			set
			{
				this.m_ssocheck = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00006F14 File Offset: 0x00005114
		// (set) Token: 0x0600032D RID: 813 RVA: 0x00006F1C File Offset: 0x0000511C
		public int loginonce
		{
			get
			{
				return this.m_loginonce;
			}
			set
			{
				this.m_loginonce = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00006F25 File Offset: 0x00005125
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00006F2D File Offset: 0x0000512D
		public string disableie
		{
			get
			{
				return this.m_disableie;
			}
			set
			{
				this.m_disableie = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00006F36 File Offset: 0x00005136
		// (set) Token: 0x06000331 RID: 817 RVA: 0x00006F3E File Offset: 0x0000513E
		public int isfree
		{
			get
			{
				return this.m_isfree;
			}
			set
			{
				this.m_isfree = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00006F47 File Offset: 0x00005147
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00006F4F File Offset: 0x0000514F
		public int acctype
		{
			get
			{
				return this.m_acctype;
			}
			set
			{
				this.m_acctype = value;
			}
		}

		// Token: 0x0400018B RID: 395
		private string m_passwordkey = "";

		// Token: 0x0400018C RID: 396
		private string m_cookiedomain = "";

		// Token: 0x0400018D RID: 397
		private int m_onlinefrequency = 2;

		// Token: 0x0400018E RID: 398
		private int m_browsecreatesite = 1;

		// Token: 0x0400018F RID: 399
		private string m_admintitle = "";

		// Token: 0x04000190 RID: 400
		private string m_platname = "";

		// Token: 0x04000191 RID: 401
		private string m_platform = "FP_WMS";

		// Token: 0x04000192 RID: 402
		private string m_adminpath = "admin";

		// Token: 0x04000193 RID: 403
		private string m_verifypage = "";

		// Token: 0x04000194 RID: 404
		private string m_customerrors = "";

		// Token: 0x04000195 RID: 405
		private int m_onlinetimeout = 20;

		// Token: 0x04000196 RID: 406
		private int m_allowlog;

		// Token: 0x04000197 RID: 407
		private int m_attachimgmaxwidth;

		// Token: 0x04000198 RID: 408
		private int m_attachimgmaxheight;

		// Token: 0x04000199 RID: 409
		private int m_attachimgquality = 80;

		// Token: 0x0400019A RID: 410
		private int m_thumbnailwidth;

		// Token: 0x0400019B RID: 411
		private int m_thumbnailheight;

		// Token: 0x0400019C RID: 412
		private int m_allowwatermark;

		// Token: 0x0400019D RID: 413
		private int m_watermarkopacity = 5;

		// Token: 0x0400019E RID: 414
		private string m_watermarkpic = "watermark.gif";

		// Token: 0x0400019F RID: 415
		private int m_watermarkstatus = 3;

		// Token: 0x040001A0 RID: 416
		private int m_ssocheck;

		// Token: 0x040001A1 RID: 417
		private int m_loginonce = 1;

		// Token: 0x040001A2 RID: 418
		private string m_disableie = "";

		// Token: 0x040001A3 RID: 419
		private int m_isfree;

		// Token: 0x040001A4 RID: 420
		private int m_acctype;
	}
}
