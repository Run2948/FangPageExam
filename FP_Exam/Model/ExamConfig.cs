using System;

namespace FP_Exam.Model
{
	// Token: 0x02000009 RID: 9
	public class ExamConfig
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003EFC File Offset: 0x000020FC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003F14 File Offset: 0x00002114
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003F20 File Offset: 0x00002120
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003F38 File Offset: 0x00002138
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003F44 File Offset: 0x00002144
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003F5C File Offset: 0x0000215C
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003F68 File Offset: 0x00002168
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003F80 File Offset: 0x00002180
		public int examstatus
		{
			get
			{
				return this.m_examstatus;
			}
			set
			{
				this.m_examstatus = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003F8C File Offset: 0x0000218C
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00003FA4 File Offset: 0x000021A4
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003FB0 File Offset: 0x000021B0
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003FC8 File Offset: 0x000021C8
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003FEC File Offset: 0x000021EC
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003FF8 File Offset: 0x000021F8
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00004010 File Offset: 0x00002210
		public int signers
		{
			get
			{
				return this.m_signers;
			}
			set
			{
				this.m_signers = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000401C File Offset: 0x0000221C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00004034 File Offset: 0x00002234
		public int exporttype
		{
			get
			{
				return this.m_exporttype;
			}
			set
			{
				this.m_exporttype = value;
			}
		}

		// Token: 0x0400000E RID: 14
		private int m_autotime = 0;

		// Token: 0x0400000F RID: 15
		private int m_showanswer = 1;

		// Token: 0x04000010 RID: 16
		private int m_teststatus = 1;

		// Token: 0x04000011 RID: 17
		private int m_examstatus = 1;

		// Token: 0x04000012 RID: 18
		private string m_questiontype = string.Empty;

		// Token: 0x04000013 RID: 19
		private int m_testcount;

		// Token: 0x04000014 RID: 20
		private int m_testtime;

		// Token: 0x04000015 RID: 21
		private int m_signers;

		// Token: 0x04000016 RID: 22
		private int m_exporttype;
	}
}
