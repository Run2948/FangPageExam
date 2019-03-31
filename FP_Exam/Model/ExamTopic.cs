using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200003C RID: 60
	[ModelPrefix("Exam")]
	public class ExamTopic
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000188A4 File Offset: 0x00016AA4
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000188BC File Offset: 0x00016ABC
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000188C8 File Offset: 0x00016AC8
		// (set) Token: 0x060001FA RID: 506 RVA: 0x000188E0 File Offset: 0x00016AE0
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000188EC File Offset: 0x00016AEC
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00018904 File Offset: 0x00016B04
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00018910 File Offset: 0x00016B10
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00018928 File Offset: 0x00016B28
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00018934 File Offset: 0x00016B34
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0001894C File Offset: 0x00016B4C
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00018958 File Offset: 0x00016B58
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00018970 File Offset: 0x00016B70
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0001897C File Offset: 0x00016B7C
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00018994 File Offset: 0x00016B94
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000189A0 File Offset: 0x00016BA0
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000189B8 File Offset: 0x00016BB8
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000189C4 File Offset: 0x00016BC4
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000189E2 File Offset: 0x00016BE2
		public double perscore
		{
			get
			{
				return Math.Round(this.m_perscore, 1);
			}
			set
			{
				this.m_perscore = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000189EC File Offset: 0x00016BEC
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00018A04 File Offset: 0x00016C04
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00018A10 File Offset: 0x00016C10
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00018A28 File Offset: 0x00016C28
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00018A34 File Offset: 0x00016C34
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00018A4C File Offset: 0x00016C4C
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00018A58 File Offset: 0x00016C58
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00018A70 File Offset: 0x00016C70
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00018A7C File Offset: 0x00016C7C
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00018A94 File Offset: 0x00016C94
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

		// Token: 0x04000197 RID: 407
		private int m_id;

		// Token: 0x04000198 RID: 408
		private int m_examid;

		// Token: 0x04000199 RID: 409
		private int m_type;

		// Token: 0x0400019A RID: 410
		private string m_title = string.Empty;

		// Token: 0x0400019B RID: 411
		private int m_display;

		// Token: 0x0400019C RID: 412
		private int m_questions = 30;

		// Token: 0x0400019D RID: 413
		private int m_curquestions;

		// Token: 0x0400019E RID: 414
		private string m_questionlist = string.Empty;

		// Token: 0x0400019F RID: 415
		private double m_perscore = 1.0;

		// Token: 0x040001A0 RID: 416
		private string m_randomsort = string.Empty;

		// Token: 0x040001A1 RID: 417
		private string m_randomcount = string.Empty;

		// Token: 0x040001A2 RID: 418
		private int m_randoms;

		// Token: 0x040001A3 RID: 419
		private int m_paper = 1;

		// Token: 0x040001A4 RID: 420
		private string m_optionlist = string.Empty;
	}
}
