using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FP_Exam.Model
{
	// Token: 0x02000039 RID: 57
	[ModelPrefix("Exam")]
	public class ExamQuestion
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00017CD8 File Offset: 0x00015ED8
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00017CF0 File Offset: 0x00015EF0
		[PrimaryKey(true)]
		[Identity(true)]
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00017CFC File Offset: 0x00015EFC
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00017D14 File Offset: 0x00015F14
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00017D20 File Offset: 0x00015F20
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00017D38 File Offset: 0x00015F38
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00017D44 File Offset: 0x00015F44
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00017D5C File Offset: 0x00015F5C
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00017D68 File Offset: 0x00015F68
		[BindField(false)]
		public SortInfo SortInfo
		{
			get
			{
				if (this.m_sortinfo == null)
				{
					this.m_sortinfo = DbHelper.ExecuteModel<SortInfo>(this.sortid);
				}
				return this.m_sortinfo;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00017DA4 File Offset: 0x00015FA4
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00017DBC File Offset: 0x00015FBC
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00017DC8 File Offset: 0x00015FC8
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00017DE0 File Offset: 0x00015FE0
		public string title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00017DEC File Offset: 0x00015FEC
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00017E04 File Offset: 0x00016004
		public string content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00017E10 File Offset: 0x00016010
		[BindField(false)]
		public string[] option
		{
			get
			{
				return FPUtils.SplitString(this.content, "§", this.ascount);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00017E38 File Offset: 0x00016038
		[BindField(false)]
		public string[] option2
		{
			get
			{
				return FPUtils.SplitString(this.content, "§", 6);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00017E5C File Offset: 0x0001605C
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00017E74 File Offset: 0x00016074
		public string answer
		{
			get
			{
				return this.m_answer;
			}
			set
			{
				this.m_answer = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00017E80 File Offset: 0x00016080
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00017E98 File Offset: 0x00016098
		public int upperflg
		{
			get
			{
				return this.m_upperflg;
			}
			set
			{
				this.m_upperflg = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00017EA4 File Offset: 0x000160A4
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00017EBC File Offset: 0x000160BC
		public int orderflg
		{
			get
			{
				return this.m_orderflg;
			}
			set
			{
				this.m_orderflg = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00017EC8 File Offset: 0x000160C8
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00017EE0 File Offset: 0x000160E0
		public string answerkey
		{
			get
			{
				return this.m_answerkey;
			}
			set
			{
				this.m_answerkey = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00017EEC File Offset: 0x000160EC
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00017F04 File Offset: 0x00016104
		public int ascount
		{
			get
			{
				return this.m_ascount;
			}
			set
			{
				this.m_ascount = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00017F10 File Offset: 0x00016110
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00017F28 File Offset: 0x00016128
		public string explain
		{
			get
			{
				return this.m_explain;
			}
			set
			{
				this.m_explain = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00017F34 File Offset: 0x00016134
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00017F4C File Offset: 0x0001614C
		public int difficulty
		{
			get
			{
				return this.m_difficulty;
			}
			set
			{
				this.m_difficulty = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00017F58 File Offset: 0x00016158
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00017F70 File Offset: 0x00016170
		public DateTime postdatetime
		{
			get
			{
				return this.m_postdatetime;
			}
			set
			{
				this.m_postdatetime = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00017F7C File Offset: 0x0001617C
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00017F94 File Offset: 0x00016194
		public int exams
		{
			get
			{
				return this.m_exams;
			}
			set
			{
				this.m_exams = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00017FA0 File Offset: 0x000161A0
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00017FB8 File Offset: 0x000161B8
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00017FC4 File Offset: 0x000161C4
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00017FDC File Offset: 0x000161DC
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00017FE8 File Offset: 0x000161E8
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00018000 File Offset: 0x00016200
		public int isclear
		{
			get
			{
				return this.m_isclear;
			}
			set
			{
				this.m_isclear = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0001800C File Offset: 0x0001620C
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00018024 File Offset: 0x00016224
		[BindField(false)]
		public string useranswer
		{
			get
			{
				return this.m_useranswer;
			}
			set
			{
				this.m_useranswer = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00018030 File Offset: 0x00016230
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0001804E File Offset: 0x0001624E
		[BindField(false)]
		public double userscore
		{
			get
			{
				return Math.Round(this.m_userscore, 1);
			}
			set
			{
				this.m_userscore = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00018058 File Offset: 0x00016258
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00018070 File Offset: 0x00016270
		[BindField(false)]
		public string optionlist
		{
			get
			{
				return this.m_optionlist;
			}
			set
			{
				this.m_optionlist = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0001807C File Offset: 0x0001627C
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00018094 File Offset: 0x00016294
		[BindField(false)]
		public int isfav
		{
			get
			{
				return this.m_isfav;
			}
			set
			{
				this.m_isfav = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000180A0 File Offset: 0x000162A0
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000180B8 File Offset: 0x000162B8
		[BindField(false)]
		public string note
		{
			get
			{
				return this.m_note;
			}
			set
			{
				this.m_note = value;
			}
		}

		// Token: 0x04000150 RID: 336
		private int m_id;

		// Token: 0x04000151 RID: 337
		private int m_uid;

		// Token: 0x04000152 RID: 338
		private int m_channelid;

		// Token: 0x04000153 RID: 339
		private int m_sortid;

		// Token: 0x04000154 RID: 340
		private SortInfo m_sortinfo;

		// Token: 0x04000155 RID: 341
		private int m_type;

		// Token: 0x04000156 RID: 342
		private string m_title = string.Empty;

		// Token: 0x04000157 RID: 343
		private string m_content = string.Empty;

		// Token: 0x04000158 RID: 344
		private string m_answer = string.Empty;

		// Token: 0x04000159 RID: 345
		private int m_upperflg;

		// Token: 0x0400015A RID: 346
		private int m_orderflg;

		// Token: 0x0400015B RID: 347
		private string m_answerkey = string.Empty;

		// Token: 0x0400015C RID: 348
		private int m_ascount;

		// Token: 0x0400015D RID: 349
		private string m_explain = string.Empty;

		// Token: 0x0400015E RID: 350
		private int m_difficulty = 2;

		// Token: 0x0400015F RID: 351
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x04000160 RID: 352
		private int m_exams;

		// Token: 0x04000161 RID: 353
		private int m_wrongs;

		// Token: 0x04000162 RID: 354
		private int m_status = 1;

		// Token: 0x04000163 RID: 355
		private int m_isclear = 1;

		// Token: 0x04000164 RID: 356
		private string m_useranswer = string.Empty;

		// Token: 0x04000165 RID: 357
		private double m_userscore;

		// Token: 0x04000166 RID: 358
		private string m_optionlist = string.Empty;

		// Token: 0x04000167 RID: 359
		private int m_isfav;

		// Token: 0x04000168 RID: 360
		private string m_note = string.Empty;
	}
}
