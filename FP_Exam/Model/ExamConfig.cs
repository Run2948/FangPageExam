using System;

namespace FP_Exam.Model
{
	// Token: 0x02000036 RID: 54
	public class ExamConfig
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000171A4 File Offset: 0x000153A4
		// (set) Token: 0x060000EA RID: 234 RVA: 0x000171BC File Offset: 0x000153BC
		public int autotime
		{
			get
			{
				return this.m_autotime;
			}
			set
			{
				this.m_autotime = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000171C8 File Offset: 0x000153C8
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000171E0 File Offset: 0x000153E0
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000171EC File Offset: 0x000153EC
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00017204 File Offset: 0x00015404
		public int teststatus
		{
			get
			{
				return this.m_teststatus;
			}
			set
			{
				this.m_teststatus = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00017210 File Offset: 0x00015410
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00017228 File Offset: 0x00015428
		public string questiontype
		{
			get
			{
				return this.m_questiontype;
			}
			set
			{
				this.m_questiontype = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00017234 File Offset: 0x00015434
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0001724C File Offset: 0x0001544C
		public int testcount
		{
			get
			{
				return this.m_testcount;
			}
			set
			{
				this.m_testcount = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00017258 File Offset: 0x00015458
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00017270 File Offset: 0x00015470
		public int testtime
		{
			get
			{
				return this.m_testtime;
			}
			set
			{
				this.m_testtime = value;
			}
		}

		// Token: 0x04000114 RID: 276
		private int m_autotime = 0;

		// Token: 0x04000115 RID: 277
		private int m_showanswer = 1;

		// Token: 0x04000116 RID: 278
		private int m_teststatus = 1;

		// Token: 0x04000117 RID: 279
		private string m_questiontype = string.Empty;

		// Token: 0x04000118 RID: 280
		private int m_testcount;

		// Token: 0x04000119 RID: 281
		private int m_testtime;
	}
}
