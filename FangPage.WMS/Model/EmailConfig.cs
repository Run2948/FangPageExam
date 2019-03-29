using System;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001A RID: 26
	public class EmailConfig
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000059D8 File Offset: 0x00003BD8
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x000059F0 File Offset: 0x00003BF0
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000059FC File Offset: 0x00003BFC
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005A14 File Offset: 0x00003C14
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005A20 File Offset: 0x00003C20
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00005A38 File Offset: 0x00003C38
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005A44 File Offset: 0x00003C44
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005A5C File Offset: 0x00003C5C
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005A68 File Offset: 0x00003C68
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005A80 File Offset: 0x00003C80
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005A8C File Offset: 0x00003C8C
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005AA4 File Offset: 0x00003CA4
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005AB0 File Offset: 0x00003CB0
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005AC8 File Offset: 0x00003CC8
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

		// Token: 0x0400005E RID: 94
		private string m_smtp = "";

		// Token: 0x0400005F RID: 95
		private int m_port = 25;

		// Token: 0x04000060 RID: 96
		private int m_ssl = 0;

		// Token: 0x04000061 RID: 97
		private string m_sysemail = "";

		// Token: 0x04000062 RID: 98
		private string m_username = "";

		// Token: 0x04000063 RID: 99
		private string m_password = "";

		// Token: 0x04000064 RID: 100
		private string m_fromname = "";
	}
}
