using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x02000012 RID: 18
	[ModelPrefix("Exam")]
	public class ExpInfo
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000060F8 File Offset: 0x000042F8
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00006110 File Offset: 0x00004310
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000611C File Offset: 0x0000431C
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00006134 File Offset: 0x00004334
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006140 File Offset: 0x00004340
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00006158 File Offset: 0x00004358
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006164 File Offset: 0x00004364
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000617C File Offset: 0x0000437C
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006188 File Offset: 0x00004388
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000061A0 File Offset: 0x000043A0
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000061AC File Offset: 0x000043AC
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x000061C4 File Offset: 0x000043C4
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000061D0 File Offset: 0x000043D0
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000061E8 File Offset: 0x000043E8
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

		// Token: 0x040000C7 RID: 199
		private int m_id;

		// Token: 0x040000C8 RID: 200
		private int m_examid;

		// Token: 0x040000C9 RID: 201
		private int m_scorelower;

		// Token: 0x040000CA RID: 202
		private int m_scoreupper;

		// Token: 0x040000CB RID: 203
		private int m_exp;

		// Token: 0x040000CC RID: 204
		private int m_credits;

		// Token: 0x040000CD RID: 205
		private string m_comment = string.Empty;
	}
}
