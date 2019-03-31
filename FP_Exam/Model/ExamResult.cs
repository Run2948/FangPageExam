using System;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FP_Exam.Model
{
	// Token: 0x0200003A RID: 58
	[ModelPrefix("Exam")]
	public class ExamResult
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0001813C File Offset: 0x0001633C
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00018154 File Offset: 0x00016354
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00018160 File Offset: 0x00016360
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00018178 File Offset: 0x00016378
		public int uid
		{
			get
			{
				return this.m_uid;
			}
			set
			{
				this.m_uid = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00018184 File Offset: 0x00016384
		[BindField(false)]
		public UserInfo IUser
		{
			get
			{
				if (this.m_iuser == null)
				{
					this.m_iuser = DbHelper.ExecuteModel<UserInfo>(this.uid);
				}
				return this.m_iuser;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000181C0 File Offset: 0x000163C0
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000181D8 File Offset: 0x000163D8
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000181E4 File Offset: 0x000163E4
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000181FC File Offset: 0x000163FC
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00018208 File Offset: 0x00016408
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00018220 File Offset: 0x00016420
		public int examid
		{
			get
			{
				return this.m_examid;
			}
			set
			{
				this.m_examid = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0001822C File Offset: 0x0001642C
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00018244 File Offset: 0x00016444
		public string examname
		{
			get
			{
				return this.m_examname;
			}
			set
			{
				this.m_examname = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00018250 File Offset: 0x00016450
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00018268 File Offset: 0x00016468
		public int examtime
		{
			get
			{
				return this.m_examtime;
			}
			set
			{
				this.m_examtime = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00018274 File Offset: 0x00016474
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0001828C File Offset: 0x0001648C
		public int examtype
		{
			get
			{
				return this.m_examtype;
			}
			set
			{
				this.m_examtype = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00018298 File Offset: 0x00016498
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000182B0 File Offset: 0x000164B0
		public int showanswer
		{
			get
			{
				return this.m_showanswer;
			}
			set
			{
				this.m_showanswer = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000182BC File Offset: 0x000164BC
		// (set) Token: 0x060001AE RID: 430 RVA: 0x000182D4 File Offset: 0x000164D4
		public int allowdelete
		{
			get
			{
				return this.m_allowdelete;
			}
			set
			{
				this.m_allowdelete = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000182E0 File Offset: 0x000164E0
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000182F8 File Offset: 0x000164F8
		public double total
		{
			get
			{
				return this.m_total;
			}
			set
			{
				this.m_total = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00018304 File Offset: 0x00016504
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0001831C File Offset: 0x0001651C
		public double passmark
		{
			get
			{
				return this.m_passmark;
			}
			set
			{
				this.m_passmark = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00018328 File Offset: 0x00016528
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00018346 File Offset: 0x00016546
		public double score1
		{
			get
			{
				return Math.Round(this.m_score1, 1);
			}
			set
			{
				this.m_score1 = value;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00018350 File Offset: 0x00016550
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0001836E File Offset: 0x0001656E
		public double score2
		{
			get
			{
				return Math.Round(this.m_score2, 1);
			}
			set
			{
				this.m_score2 = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00018378 File Offset: 0x00016578
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00018396 File Offset: 0x00016596
		public double score
		{
			get
			{
				return Math.Round(this.m_score, 1);
			}
			set
			{
				this.m_score = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000183A0 File Offset: 0x000165A0
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000183B8 File Offset: 0x000165B8
		public int utime
		{
			get
			{
				return this.m_utime;
			}
			set
			{
				this.m_utime = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000183C4 File Offset: 0x000165C4
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000183DC File Offset: 0x000165DC
		public int islimit
		{
			get
			{
				return this.m_islimit;
			}
			set
			{
				this.m_islimit = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000183E8 File Offset: 0x000165E8
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00018400 File Offset: 0x00016600
		public DateTime starttime
		{
			get
			{
				return this.m_starttime;
			}
			set
			{
				this.m_starttime = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0001840C File Offset: 0x0001660C
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00018424 File Offset: 0x00016624
		public DateTime endtime
		{
			get
			{
				return this.m_endtime;
			}
			set
			{
				this.m_endtime = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00018430 File Offset: 0x00016630
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00018448 File Offset: 0x00016648
		public DateTime examdatetime
		{
			get
			{
				return this.m_examdatetime;
			}
			set
			{
				this.m_examdatetime = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00018454 File Offset: 0x00016654
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0001846C File Offset: 0x0001666C
		public int credits
		{
			get
			{
				return this.m_credits;
			}
			set
			{
				this.m_credits = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00018478 File Offset: 0x00016678
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00018490 File Offset: 0x00016690
		public int questions
		{
			get
			{
				return this.m_questions;
			}
			set
			{
				this.m_questions = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0001849C File Offset: 0x0001669C
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000184B4 File Offset: 0x000166B4
		public int wrongs
		{
			get
			{
				return this.m_wrongs;
			}
			set
			{
				this.m_wrongs = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000184C0 File Offset: 0x000166C0
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000184D8 File Offset: 0x000166D8
		public int unanswer
		{
			get
			{
				return this.m_unanswer;
			}
			set
			{
				this.m_unanswer = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000184E4 File Offset: 0x000166E4
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000184FC File Offset: 0x000166FC
		public int exp
		{
			get
			{
				return this.m_exp;
			}
			set
			{
				this.m_exp = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00018508 File Offset: 0x00016708
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00018520 File Offset: 0x00016720
		public int getcredits
		{
			get
			{
				return this.m_getcredits;
			}
			set
			{
				this.m_getcredits = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0001852C File Offset: 0x0001672C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00018544 File Offset: 0x00016744
		public string exnote
		{
			get
			{
				return this.m_exnote;
			}
			set
			{
				this.m_exnote = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00018550 File Offset: 0x00016750
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00018568 File Offset: 0x00016768
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00018574 File Offset: 0x00016774
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0001858C File Offset: 0x0001678C
		public int paper
		{
			get
			{
				return this.m_paper;
			}
			set
			{
				this.m_paper = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00018598 File Offset: 0x00016798
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000185B0 File Offset: 0x000167B0
		public string ip
		{
			get
			{
				return this.m_ip;
			}
			set
			{
				this.m_ip = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000185BC File Offset: 0x000167BC
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000185D4 File Offset: 0x000167D4
		public string mac
		{
			get
			{
				return this.m_mac;
			}
			set
			{
				this.m_mac = value;
			}
		}

		// Token: 0x04000169 RID: 361
		private int m_id;

		// Token: 0x0400016A RID: 362
		private int m_uid;

		// Token: 0x0400016B RID: 363
		private UserInfo m_iuser;

		// Token: 0x0400016C RID: 364
		private int m_channelid;

		// Token: 0x0400016D RID: 365
		private int m_sortid;

		// Token: 0x0400016E RID: 366
		private int m_examid;

		// Token: 0x0400016F RID: 367
		private string m_examname = string.Empty;

		// Token: 0x04000170 RID: 368
		private int m_examtime;

		// Token: 0x04000171 RID: 369
		private int m_examtype;

		// Token: 0x04000172 RID: 370
		private int m_showanswer = 1;

		// Token: 0x04000173 RID: 371
		private int m_allowdelete = 1;

		// Token: 0x04000174 RID: 372
		private double m_total;

		// Token: 0x04000175 RID: 373
		private double m_passmark;

		// Token: 0x04000176 RID: 374
		private double m_score1;

		// Token: 0x04000177 RID: 375
		private double m_score2;

		// Token: 0x04000178 RID: 376
		private double m_score;

		// Token: 0x04000179 RID: 377
		private int m_utime;

		// Token: 0x0400017A RID: 378
		private int m_islimit;

		// Token: 0x0400017B RID: 379
		private DateTime m_starttime = DbUtils.GetDateTime();

		// Token: 0x0400017C RID: 380
		private DateTime m_endtime = DbUtils.GetDateTime();

		// Token: 0x0400017D RID: 381
		private DateTime m_examdatetime = DbUtils.GetDateTime();

		// Token: 0x0400017E RID: 382
		private int m_credits;

		// Token: 0x0400017F RID: 383
		private int m_questions;

		// Token: 0x04000180 RID: 384
		private int m_wrongs;

		// Token: 0x04000181 RID: 385
		private int m_unanswer;

		// Token: 0x04000182 RID: 386
		private int m_exp;

		// Token: 0x04000183 RID: 387
		private int m_getcredits;

		// Token: 0x04000184 RID: 388
		private string m_exnote = string.Empty;

		// Token: 0x04000185 RID: 389
		private int m_status;

		// Token: 0x04000186 RID: 390
		private int m_paper = 1;

		// Token: 0x04000187 RID: 391
		private string m_ip = string.Empty;

		// Token: 0x04000188 RID: 392
		private string m_mac = string.Empty;
	}
}
