using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200000F RID: 15
	[ModelPrefix("Exam")]
	public class ExamResultTopic
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005A44 File Offset: 0x00003C44
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005A5C File Offset: 0x00003C5C
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005A68 File Offset: 0x00003C68
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00005A80 File Offset: 0x00003C80
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

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005A8C File Offset: 0x00003C8C
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00005AA4 File Offset: 0x00003CA4
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005AB0 File Offset: 0x00003CB0
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005AC8 File Offset: 0x00003CC8
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005AD4 File Offset: 0x00003CD4
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005AEC File Offset: 0x00003CEC
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005AF8 File Offset: 0x00003CF8
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005B16 File Offset: 0x00003D16
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005B20 File Offset: 0x00003D20
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005B3E File Offset: 0x00003D3E
		public double score
		{
			get
			{
				return Math.Round(this.m_score, 2);
			}
			set
			{
				this.m_score = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005B48 File Offset: 0x00003D48
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005B60 File Offset: 0x00003D60
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005B6C File Offset: 0x00003D6C
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005B84 File Offset: 0x00003D84
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005B90 File Offset: 0x00003D90
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00005BA8 File Offset: 0x00003DA8
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

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005BB4 File Offset: 0x00003DB4
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00005BCC File Offset: 0x00003DCC
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

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005BD8 File Offset: 0x00003DD8
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005BF0 File Offset: 0x00003DF0
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005BFC File Offset: 0x00003DFC
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005C14 File Offset: 0x00003E14
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

		// Token: 0x040000A0 RID: 160
		private int m_id;

		// Token: 0x040000A1 RID: 161
		private int m_resultid;

		// Token: 0x040000A2 RID: 162
		private string m_type;

		// Token: 0x040000A3 RID: 163
		private string m_title = string.Empty;

		// Token: 0x040000A4 RID: 164
		private int m_display;

		// Token: 0x040000A5 RID: 165
		private double m_perscore;

		// Token: 0x040000A6 RID: 166
		private double m_score;

		// Token: 0x040000A7 RID: 167
		private int m_questions;

		// Token: 0x040000A8 RID: 168
		private int m_wrongs;

		// Token: 0x040000A9 RID: 169
		private string m_questionlist = string.Empty;

		// Token: 0x040000AA RID: 170
		private string m_answerlist = string.Empty;

		// Token: 0x040000AB RID: 171
		private string m_scorelist = string.Empty;

		// Token: 0x040000AC RID: 172
		private string m_correctlist = string.Empty;
	}
}
