using System;

namespace FangPage.WMS.Config
{
	// Token: 0x02000020 RID: 32
	public class SSOConfig
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000063E7 File Offset: 0x000045E7
		// (set) Token: 0x06000264 RID: 612 RVA: 0x000063EF File Offset: 0x000045EF
		public string secret
		{
			get
			{
				return this.m_secret;
			}
			set
			{
				this.m_secret = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000063F8 File Offset: 0x000045F8
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00006400 File Offset: 0x00004600
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00006409 File Offset: 0x00004609
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00006411 File Offset: 0x00004611
		public string oauth
		{
			get
			{
				return this.m_oauth;
			}
			set
			{
				this.m_oauth = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000641A File Offset: 0x0000461A
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00006422 File Offset: 0x00004622
		public string logout
		{
			get
			{
				return this.m_logout;
			}
			set
			{
				this.m_logout = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000642B File Offset: 0x0000462B
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00006433 File Offset: 0x00004633
		public string getdeptlist
		{
			get
			{
				return this.m_getdeptlist;
			}
			set
			{
				this.m_getdeptlist = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000643C File Offset: 0x0000463C
		// (set) Token: 0x0600026E RID: 622 RVA: 0x00006444 File Offset: 0x00004644
		public string getuserlist
		{
			get
			{
				return this.m_getuserlist;
			}
			set
			{
				this.m_getuserlist = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000644D File Offset: 0x0000464D
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00006455 File Offset: 0x00004655
		public string getuserinfo
		{
			get
			{
				return this.m_getuserinfo;
			}
			set
			{
				this.m_getuserinfo = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000645E File Offset: 0x0000465E
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00006466 File Offset: 0x00004666
		public string adduser
		{
			get
			{
				return this.m_adduser;
			}
			set
			{
				this.m_adduser = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000646F File Offset: 0x0000466F
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00006477 File Offset: 0x00004677
		public string updateuser
		{
			get
			{
				return this.m_updateuser;
			}
			set
			{
				this.m_updateuser = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00006480 File Offset: 0x00004680
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00006488 File Offset: 0x00004688
		public string updatepwd
		{
			get
			{
				return this.m_updatepwd;
			}
			set
			{
				this.m_updatepwd = value;
			}
		}

		// Token: 0x04000141 RID: 321
		private string m_secret = "";

		// Token: 0x04000142 RID: 322
		private string m_server = "";

		// Token: 0x04000143 RID: 323
		private string m_oauth = "oauth.aspx";

		// Token: 0x04000144 RID: 324
		private string m_logout = "logout.aspx";

		// Token: 0x04000145 RID: 325
		private string m_getdeptlist = "getdeptlist.aspx";

		// Token: 0x04000146 RID: 326
		private string m_getuserlist = "getuserlist.aspx";

		// Token: 0x04000147 RID: 327
		private string m_getuserinfo = "getuserinfo.aspx";

		// Token: 0x04000148 RID: 328
		private string m_adduser = "adduser.aspx";

		// Token: 0x04000149 RID: 329
		private string m_updateuser = "updateuser.aspx";

		// Token: 0x0400014A RID: 330
		private string m_updatepwd = "updatepwd.aspx";
	}
}
