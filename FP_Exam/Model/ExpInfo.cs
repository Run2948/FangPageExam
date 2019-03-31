using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200003E RID: 62
	[ModelPrefix("Exam")]
	public class ExpInfo
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00018B58 File Offset: 0x00016D58
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00018B70 File Offset: 0x00016D70
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00018B7C File Offset: 0x00016D7C
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00018B94 File Offset: 0x00016D94
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00018BA0 File Offset: 0x00016DA0
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00018BB8 File Offset: 0x00016DB8
		public int scorelower
		{
			get
			{
				return this.m_scorelower;
			}
			set
			{
				this.m_scorelower = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00018BC4 File Offset: 0x00016DC4
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00018BDC File Offset: 0x00016DDC
		public int scoreupper
		{
			get
			{
				return this.m_scoreupper;
			}
			set
			{
				this.m_scoreupper = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00018BE8 File Offset: 0x00016DE8
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00018C00 File Offset: 0x00016E00
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00018C0C File Offset: 0x00016E0C
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00018C24 File Offset: 0x00016E24
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

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00018C30 File Offset: 0x00016E30
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00018C48 File Offset: 0x00016E48
		public string comment
		{
			get
			{
				return this.m_comment;
			}
			set
			{
				this.m_comment = value;
			}
		}

		// Token: 0x040001A9 RID: 425
		private int m_id;

		// Token: 0x040001AA RID: 426
		private int m_examid;

		// Token: 0x040001AB RID: 427
		private int m_scorelower;

		// Token: 0x040001AC RID: 428
		private int m_scoreupper;

		// Token: 0x040001AD RID: 429
		private int m_exp;

		// Token: 0x040001AE RID: 430
		private int m_credits;

		// Token: 0x040001AF RID: 431
		private string m_comment = string.Empty;
	}
}
