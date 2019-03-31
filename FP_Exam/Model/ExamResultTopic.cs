using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200003B RID: 59
	[ModelPrefix("Exam")]
	public class ExamResultTopic
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00018638 File Offset: 0x00016838
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00018650 File Offset: 0x00016850
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0001865C File Offset: 0x0001685C
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00018674 File Offset: 0x00016874
		public int resultid
		{
			get
			{
				return this.m_resultid;
			}
			set
			{
				this.m_resultid = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00018680 File Offset: 0x00016880
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00018698 File Offset: 0x00016898
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000186A4 File Offset: 0x000168A4
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000186BC File Offset: 0x000168BC
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000186C8 File Offset: 0x000168C8
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000186E0 File Offset: 0x000168E0
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

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000186EC File Offset: 0x000168EC
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0001870A File Offset: 0x0001690A
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00018714 File Offset: 0x00016914
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00018732 File Offset: 0x00016932
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0001873C File Offset: 0x0001693C
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00018754 File Offset: 0x00016954
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00018760 File Offset: 0x00016960
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00018778 File Offset: 0x00016978
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00018784 File Offset: 0x00016984
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0001879C File Offset: 0x0001699C
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000187A8 File Offset: 0x000169A8
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000187C0 File Offset: 0x000169C0
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000187CC File Offset: 0x000169CC
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000187E4 File Offset: 0x000169E4
		public string answerlist
		{
			get
			{
				return this.m_answerlist;
			}
			set
			{
				this.m_answerlist = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000187F0 File Offset: 0x000169F0
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00018808 File Offset: 0x00016A08
		public string scorelist
		{
			get
			{
				return this.m_scorelist;
			}
			set
			{
				this.m_scorelist = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00018814 File Offset: 0x00016A14
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0001882C File Offset: 0x00016A2C
		public string correctlist
		{
			get
			{
				return this.m_correctlist;
			}
			set
			{
				this.m_correctlist = value;
			}
		}

		// Token: 0x04000189 RID: 393
		private int m_id;

		// Token: 0x0400018A RID: 394
		private int m_resultid;

		// Token: 0x0400018B RID: 395
		private int m_type;

		// Token: 0x0400018C RID: 396
		private string m_title = string.Empty;

		// Token: 0x0400018D RID: 397
		private int m_display;

		// Token: 0x0400018E RID: 398
		private double m_perscore;

		// Token: 0x0400018F RID: 399
		private double m_score;

		// Token: 0x04000190 RID: 400
		private int m_questions;

		// Token: 0x04000191 RID: 401
		private int m_wrongs;

		// Token: 0x04000192 RID: 402
		private string m_questionlist = string.Empty;

		// Token: 0x04000193 RID: 403
		private string m_optionlist = string.Empty;

		// Token: 0x04000194 RID: 404
		private string m_answerlist = string.Empty;

		// Token: 0x04000195 RID: 405
		private string m_scorelist = string.Empty;

		// Token: 0x04000196 RID: 406
		private string m_correctlist = string.Empty;
	}
}
