using System;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001E RID: 30
	public class RegConfig
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000063D8 File Offset: 0x000045D8
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000063F0 File Offset: 0x000045F0
		public int regstatus
		{
			get
			{
				return this.m_regstatus;
			}
			set
			{
				this.m_regstatus = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000063FC File Offset: 0x000045FC
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006414 File Offset: 0x00004614
		public int realname
		{
			get
			{
				return this.m_realname;
			}
			set
			{
				this.m_realname = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006420 File Offset: 0x00004620
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00006438 File Offset: 0x00004638
		public int email
		{
			get
			{
				return this.m_email;
			}
			set
			{
				this.m_email = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006444 File Offset: 0x00004644
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000645C File Offset: 0x0000465C
		public int mobile
		{
			get
			{
				return this.m_mobile;
			}
			set
			{
				this.m_mobile = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006468 File Offset: 0x00004668
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00006480 File Offset: 0x00004680
		public int rules
		{
			get
			{
				return this.m_rules;
			}
			set
			{
				this.m_rules = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000648C File Offset: 0x0000468C
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000064A4 File Offset: 0x000046A4
		public int credit
		{
			get
			{
				return this.m_credit;
			}
			set
			{
				this.m_credit = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000064B0 File Offset: 0x000046B0
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000064C8 File Offset: 0x000046C8
		public int regverify
		{
			get
			{
				return this.m_regverify;
			}
			set
			{
				this.m_regverify = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000064D4 File Offset: 0x000046D4
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000064EC File Offset: 0x000046EC
		public int regctrl
		{
			get
			{
				return this.m_regctrl;
			}
			set
			{
				this.m_regctrl = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000064F8 File Offset: 0x000046F8
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00006510 File Offset: 0x00004710
		public string restrict
		{
			get
			{
				return this.m_restrict;
			}
			set
			{
				this.m_restrict = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000651C File Offset: 0x0000471C
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00006534 File Offset: 0x00004734
		public string ipregctrl
		{
			get
			{
				return this.m_ipregctrl;
			}
			set
			{
				this.m_ipregctrl = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006540 File Offset: 0x00004740
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00006558 File Offset: 0x00004758
		public string accessemail
		{
			get
			{
				return this.m_accessemail;
			}
			set
			{
				this.m_accessemail = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006564 File Offset: 0x00004764
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000657C File Offset: 0x0000477C
		public string censoremail
		{
			get
			{
				return this.m_censoremail;
			}
			set
			{
				this.m_censoremail = value;
			}
		}

		// Token: 0x04000094 RID: 148
		private int m_regstatus = 1;

		// Token: 0x04000095 RID: 149
		private int m_realname = 0;

		// Token: 0x04000096 RID: 150
		private int m_email = 0;

		// Token: 0x04000097 RID: 151
		private int m_mobile = 0;

		// Token: 0x04000098 RID: 152
		private int m_rules = 0;

		// Token: 0x04000099 RID: 153
		private int m_credit = 0;

		// Token: 0x0400009A RID: 154
		private int m_regverify = 0;

		// Token: 0x0400009B RID: 155
		private int m_regctrl = 0;

		// Token: 0x0400009C RID: 156
		private string m_restrict = "";

		// Token: 0x0400009D RID: 157
		private string m_ipregctrl = "";

		// Token: 0x0400009E RID: 158
		private string m_accessemail = "";

		// Token: 0x0400009F RID: 159
		private string m_censoremail = "";
	}
}
