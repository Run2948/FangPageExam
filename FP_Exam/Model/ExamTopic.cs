using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x02000011 RID: 17
	[ModelPrefix("Exam")]
	public class ExamTopic
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00005E90 File Offset: 0x00004090
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00005EA8 File Offset: 0x000040A8
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
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005EB4 File Offset: 0x000040B4
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00005ECC File Offset: 0x000040CC
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00005ED8 File Offset: 0x000040D8
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00005EF0 File Offset: 0x000040F0
		public string type
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00005EFC File Offset: 0x000040FC
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00005F14 File Offset: 0x00004114
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005F20 File Offset: 0x00004120
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00005F38 File Offset: 0x00004138
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005F44 File Offset: 0x00004144
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00005F5C File Offset: 0x0000415C
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005F68 File Offset: 0x00004168
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00005F80 File Offset: 0x00004180
		public int curquestions
		{
			get
			{
				return this.m_curquestions;
			}
			set
			{
				this.m_curquestions = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005F8C File Offset: 0x0000418C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005FA4 File Offset: 0x000041A4
		public string questionlist
		{
			get
			{
				return this.m_questionlist;
			}
			set
			{
				this.m_questionlist = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005FB0 File Offset: 0x000041B0
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00005FCE File Offset: 0x000041CE
		public double perscore
		{
			get
			{
				return Math.Round(this.m_perscore, 2);
			}
			set
			{
				this.m_perscore = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00005FD8 File Offset: 0x000041D8
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00005FF0 File Offset: 0x000041F0
		public string randomsort
		{
			get
			{
				return this.m_randomsort;
			}
			set
			{
				this.m_randomsort = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005FFC File Offset: 0x000041FC
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00006014 File Offset: 0x00004214
		public string randomcount
		{
			get
			{
				return this.m_randomcount;
			}
			set
			{
				this.m_randomcount = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00006020 File Offset: 0x00004220
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00006038 File Offset: 0x00004238
		public int randoms
		{
			get
			{
				return this.m_randoms;
			}
			set
			{
				this.m_randoms = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00006044 File Offset: 0x00004244
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000605C File Offset: 0x0000425C
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00006068 File Offset: 0x00004268
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00006080 File Offset: 0x00004280
		[BindField(false)]
		public string allquestions
		{
			get
			{
				return this.m_allquestions;
			}
			set
			{
				this.m_allquestions = value;
			}
		}

		// Token: 0x040000B9 RID: 185
		private int m_id;

		// Token: 0x040000BA RID: 186
		private int m_examid;

		// Token: 0x040000BB RID: 187
		private string m_type;

		// Token: 0x040000BC RID: 188
		private string m_title = string.Empty;

		// Token: 0x040000BD RID: 189
		private int m_display;

		// Token: 0x040000BE RID: 190
		private int m_questions = 30;

		// Token: 0x040000BF RID: 191
		private int m_curquestions;

		// Token: 0x040000C0 RID: 192
		private string m_questionlist = string.Empty;

		// Token: 0x040000C1 RID: 193
		private double m_perscore = 1.0;

		// Token: 0x040000C2 RID: 194
		private string m_randomsort = string.Empty;

		// Token: 0x040000C3 RID: 195
		private string m_randomcount = string.Empty;

		// Token: 0x040000C4 RID: 196
		private int m_randoms;

		// Token: 0x040000C5 RID: 197
		private int m_paper = 1;

		// Token: 0x040000C6 RID: 198
		private string m_allquestions = string.Empty;
	}
}
