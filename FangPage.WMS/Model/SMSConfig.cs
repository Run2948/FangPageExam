using System;

namespace FangPage.WMS.Model
{
	// Token: 0x02000005 RID: 5
	public class SMSConfig
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002308 File Offset: 0x00000508
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002320 File Offset: 0x00000520
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000232C File Offset: 0x0000052C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002344 File Offset: 0x00000544
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002350 File Offset: 0x00000550
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002368 File Offset: 0x00000568
		public string posturl
		{
			get
			{
				return this.m_posturl;
			}
			set
			{
				this.m_posturl = value;
			}
		}

		// Token: 0x04000002 RID: 2
		private string m_account = "";

		// Token: 0x04000003 RID: 3
		private string m_password = "";

		// Token: 0x04000004 RID: 4
		private string m_posturl = "http://sms.106jiekou.com/utf8/sms.aspx";
	}
}
