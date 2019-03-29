using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200003D RID: 61
	[ModelPrefix("Exam")]
	public class ExamNote
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00018AB4 File Offset: 0x00016CB4
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00018ACC File Offset: 0x00016CCC
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00018AD8 File Offset: 0x00016CD8
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00018AF0 File Offset: 0x00016CF0
		public int qid
		{
			get
			{
				return this.m_qid;
			}
			set
			{
				this.m_qid = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00018AFC File Offset: 0x00016CFC
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00018B14 File Offset: 0x00016D14
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00018B20 File Offset: 0x00016D20
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00018B38 File Offset: 0x00016D38
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

		// Token: 0x040001A5 RID: 421
		private int m_id;

		// Token: 0x040001A6 RID: 422
		private int m_qid;

		// Token: 0x040001A7 RID: 423
		private int m_uid;

		// Token: 0x040001A8 RID: 424
		private string m_note = string.Empty;
	}
}
