using System;

namespace FangPage.WMS.Config
{
	// Token: 0x02000024 RID: 36
	public class EmailConfig
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000068E2 File Offset: 0x00004AE2
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x000068EA File Offset: 0x00004AEA
		public string smtp
		{
			get
			{
				return this.m_smtp;
			}
			set
			{
				this.m_smtp = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000068F3 File Offset: 0x00004AF3
		// (set) Token: 0x060002AA RID: 682 RVA: 0x000068FB File Offset: 0x00004AFB
		public int port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				this.m_port = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00006904 File Offset: 0x00004B04
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000690C File Offset: 0x00004B0C
		public int ssl
		{
			get
			{
				return this.m_ssl;
			}
			set
			{
				this.m_ssl = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00006915 File Offset: 0x00004B15
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000691D File Offset: 0x00004B1D
		public string sysemail
		{
			get
			{
				return this.m_sysemail;
			}
			set
			{
				this.m_sysemail = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00006926 File Offset: 0x00004B26
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000692E File Offset: 0x00004B2E
		public string username
		{
			get
			{
				return this.m_username;
			}
			set
			{
				this.m_username = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006937 File Offset: 0x00004B37
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000693F File Offset: 0x00004B3F
		public string password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00006948 File Offset: 0x00004B48
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00006950 File Offset: 0x00004B50
		public string fromname
		{
			get
			{
				return this.m_fromname;
			}
			set
			{
				this.m_fromname = value;
			}
		}

		// Token: 0x04000160 RID: 352
		private string m_smtp = "";

		// Token: 0x04000161 RID: 353
		private int m_port = 25;

		// Token: 0x04000162 RID: 354
		private int m_ssl;

		// Token: 0x04000163 RID: 355
		private string m_sysemail = "";

		// Token: 0x04000164 RID: 356
		private string m_username = "";

		// Token: 0x04000165 RID: 357
		private string m_password = "";

		// Token: 0x04000166 RID: 358
		private string m_fromname = "";
	}
}
