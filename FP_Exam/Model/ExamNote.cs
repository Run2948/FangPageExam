using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200000C RID: 12
	[ModelPrefix("Exam")]
	public class ExamNote
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004B68 File Offset: 0x00002D68
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004B80 File Offset: 0x00002D80
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004B8C File Offset: 0x00002D8C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004BA4 File Offset: 0x00002DA4
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004BB0 File Offset: 0x00002DB0
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004BC8 File Offset: 0x00002DC8
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004BD4 File Offset: 0x00002DD4
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004BEC File Offset: 0x00002DEC
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

		// Token: 0x04000055 RID: 85
		private int m_id;

		// Token: 0x04000056 RID: 86
		private int m_qid;

		// Token: 0x04000057 RID: 87
		private int m_uid;

		// Token: 0x04000058 RID: 88
		private string m_note = string.Empty;
	}
}
