using System;

namespace FangPage.WMS.Model
{
	// Token: 0x02000022 RID: 34
	public class SysConfig
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006B54 File Offset: 0x00004D54
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00006B6C File Offset: 0x00004D6C
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006B78 File Offset: 0x00004D78
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00006B90 File Offset: 0x00004D90
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006B9C File Offset: 0x00004D9C
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00006BCB File Offset: 0x00004DCB
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006BD8 File Offset: 0x00004DD8
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00006BF0 File Offset: 0x00004DF0
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006BFC File Offset: 0x00004DFC
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00006C14 File Offset: 0x00004E14
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006C20 File Offset: 0x00004E20
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00006C38 File Offset: 0x00004E38
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006C44 File Offset: 0x00004E44
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006C5C File Offset: 0x00004E5C
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006C68 File Offset: 0x00004E68
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00006C80 File Offset: 0x00004E80
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00006C8C File Offset: 0x00004E8C
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00006CA4 File Offset: 0x00004EA4
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006CB0 File Offset: 0x00004EB0
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00006CC8 File Offset: 0x00004EC8
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00006CD4 File Offset: 0x00004ED4
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00006CEC File Offset: 0x00004EEC
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

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006CF8 File Offset: 0x00004EF8
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00006D10 File Offset: 0x00004F10
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006D1C File Offset: 0x00004F1C
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006D34 File Offset: 0x00004F34
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006D40 File Offset: 0x00004F40
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00006D58 File Offset: 0x00004F58
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00006D64 File Offset: 0x00004F64
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00006D7C File Offset: 0x00004F7C
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006D88 File Offset: 0x00004F88
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00006DA0 File Offset: 0x00004FA0
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006DAC File Offset: 0x00004FAC
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006DC4 File Offset: 0x00004FC4
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006DD0 File Offset: 0x00004FD0
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006DE8 File Offset: 0x00004FE8
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00006DF4 File Offset: 0x00004FF4
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00006E0C File Offset: 0x0000500C
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

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00006E18 File Offset: 0x00005018
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00006E30 File Offset: 0x00005030
		public int taskinterval
		{
			get
			{
				return this.m_taskinterval;
			}
			set
			{
				this.m_taskinterval = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00006E3C File Offset: 0x0000503C
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006E54 File Offset: 0x00005054
		public int develop
		{
			get
			{
				return this.m_develop;
			}
			set
			{
				this.m_develop = value;
			}
		}

		// Token: 0x040000BE RID: 190
		private string m_passwordkey = "";

		// Token: 0x040000BF RID: 191
		private string m_cookiedomain = "";

		// Token: 0x040000C0 RID: 192
		private int m_onlinefrequency = 2;

		// Token: 0x040000C1 RID: 193
		private int m_browsecreatesite = 1;

		// Token: 0x040000C2 RID: 194
		private string m_admintitle = "";

		// Token: 0x040000C3 RID: 195
		private string m_adminpath = "admin";

		// Token: 0x040000C4 RID: 196
		private string m_verifypage = "";

		// Token: 0x040000C5 RID: 197
		private string m_customerrors = "";

		// Token: 0x040000C6 RID: 198
		private int m_onlinetimeout = 20;

		// Token: 0x040000C7 RID: 199
		private int m_allowlog = 0;

		// Token: 0x040000C8 RID: 200
		private int m_attachimgmaxwidth = 0;

		// Token: 0x040000C9 RID: 201
		private int m_attachimgmaxheight = 0;

		// Token: 0x040000CA RID: 202
		private int m_attachimgquality = 80;

		// Token: 0x040000CB RID: 203
		private int m_thumbnailwidth = 0;

		// Token: 0x040000CC RID: 204
		private int m_thumbnailheight = 0;

		// Token: 0x040000CD RID: 205
		private int m_allowwatermark = 0;

		// Token: 0x040000CE RID: 206
		private int m_watermarkopacity = 5;

		// Token: 0x040000CF RID: 207
		private string m_watermarkpic = "watermark.gif";

		// Token: 0x040000D0 RID: 208
		private int m_watermarkstatus = 3;

		// Token: 0x040000D1 RID: 209
		private int m_taskinterval = 0;

		// Token: 0x040000D2 RID: 210
		private int m_develop = 0;
	}
}
