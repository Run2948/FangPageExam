using System;

namespace FangPage.WMS.Config
{
	// Token: 0x0200001E RID: 30
	public class SMSConfig
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000623B File Offset: 0x0000443B
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00006243 File Offset: 0x00004443
		public string server
		{
			get
			{
				return this.m_server;
			}
			set
			{
				this.m_server = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000624C File Offset: 0x0000444C
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00006254 File Offset: 0x00004454
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000625D File Offset: 0x0000445D
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00006265 File Offset: 0x00004465
		public string account
		{
			get
			{
				return this.m_account;
			}
			set
			{
				this.m_account = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000626E File Offset: 0x0000446E
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00006276 File Offset: 0x00004476
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

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000627F File Offset: 0x0000447F
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00006287 File Offset: 0x00004487
		public string result
		{
			get
			{
				return this.m_result;
			}
			set
			{
				this.m_result = value;
			}
		}

		// Token: 0x0400013B RID: 315
		private string m_server = "";

		// Token: 0x0400013C RID: 316
		private int m_port;

		// Token: 0x0400013D RID: 317
		private string m_account = "";

		// Token: 0x0400013E RID: 318
		private string m_password = "";

		// Token: 0x0400013F RID: 319
		private string m_result = "";
	}
}
