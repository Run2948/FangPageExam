using System;
using FangPage.Common;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x02000010 RID: 16
	[ModelPrefix("Exam")]
	public class ExamSignInfo
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005C60 File Offset: 0x00003E60
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005C78 File Offset: 0x00003E78
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00005C84 File Offset: 0x00003E84
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005C9C File Offset: 0x00003E9C
		public string ikey
		{
			get
			{
				return this.m_ikey;
			}
			set
			{
				this.m_ikey = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005CA8 File Offset: 0x00003EA8
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005CC0 File Offset: 0x00003EC0
		[LeftJoin("Exam_ExamInfo", "id")]
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

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005CCC File Offset: 0x00003ECC
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00005CE4 File Offset: 0x00003EE4
		[Map("Exam_ExamInfo", "name")]
		public string examname
		{
			get
			{
				return this.m_examname;
			}
			set
			{
				this.m_examname = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005CF0 File Offset: 0x00003EF0
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00005D08 File Offset: 0x00003F08
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

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00005D2C File Offset: 0x00003F2C
		public FPData signer
		{
			get
			{
				return this.m_signer;
			}
			set
			{
				this.m_signer = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00005D38 File Offset: 0x00003F38
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00005D50 File Offset: 0x00003F50
		public string img
		{
			get
			{
				return this.m_img;
			}
			set
			{
				this.m_img = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005D5C File Offset: 0x00003F5C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00005D74 File Offset: 0x00003F74
		public string payimg
		{
			get
			{
				return this.m_payimg;
			}
			set
			{
				this.m_payimg = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005D80 File Offset: 0x00003F80
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005D98 File Offset: 0x00003F98
		public string attachid
		{
			get
			{
				return this.m_attachid;
			}
			set
			{
				this.m_attachid = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005DA4 File Offset: 0x00003FA4
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005DBC File Offset: 0x00003FBC
		public string reason
		{
			get
			{
				return this.m_reason;
			}
			set
			{
				this.m_reason = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005DC8 File Offset: 0x00003FC8
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005DE0 File Offset: 0x00003FE0
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005DEC File Offset: 0x00003FEC
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005E04 File Offset: 0x00004004
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

		// Token: 0x040000AD RID: 173
		private int m_id;

		// Token: 0x040000AE RID: 174
		private string m_ikey = FPRandom.CreateCodeNum(DateTime.Now.ToString("yyyyMMddHHssmm"), 5);

		// Token: 0x040000AF RID: 175
		private int m_examid;

		// Token: 0x040000B0 RID: 176
		private string m_examname = string.Empty;

		// Token: 0x040000B1 RID: 177
		private int m_uid;

		// Token: 0x040000B2 RID: 178
		private FPData m_signer = new FPData();

		// Token: 0x040000B3 RID: 179
		private string m_img = string.Empty;

		// Token: 0x040000B4 RID: 180
		private string m_payimg = string.Empty;

		// Token: 0x040000B5 RID: 181
		private string m_attachid = string.Empty;

		// Token: 0x040000B6 RID: 182
		private string m_reason = string.Empty;

		// Token: 0x040000B7 RID: 183
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x040000B8 RID: 184
		private int m_status;
	}
}
