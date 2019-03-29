using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000017 RID: 23
	[ModelPrefix("WMS")]
	public class CreditInfo
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005520 File Offset: 0x00003720
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00005538 File Offset: 0x00003738
		[PrimaryKey(true)]
		[Identity(true)]
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005544 File Offset: 0x00003744
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000555C File Offset: 0x0000375C
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005568 File Offset: 0x00003768
		[BindField(false)]
		public UserInfo UserInfo
		{
			get
			{
				if (this.m_iuser == null)
				{
					this.m_iuser = DbHelper.ExecuteModel<UserInfo>(this.uid);
				}
				return this.m_iuser;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000055A4 File Offset: 0x000037A4
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000055BC File Offset: 0x000037BC
		public string name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000055C8 File Offset: 0x000037C8
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000055E0 File Offset: 0x000037E0
		public int type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000055EC File Offset: 0x000037EC
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005604 File Offset: 0x00003804
		public int credits
		{
			get
			{
				return this.m_credits;
			}
			set
			{
				this.m_credits = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005610 File Offset: 0x00003810
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005628 File Offset: 0x00003828
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005634 File Offset: 0x00003834
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000564C File Offset: 0x0000384C
		public int doid
		{
			get
			{
				return this.m_doid;
			}
			set
			{
				this.m_doid = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005658 File Offset: 0x00003858
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00005670 File Offset: 0x00003870
		public string doname
		{
			get
			{
				return this.m_doname;
			}
			set
			{
				this.m_doname = value;
			}
		}

		// Token: 0x04000043 RID: 67
		private int m_id;

		// Token: 0x04000044 RID: 68
		private int m_uid;

		// Token: 0x04000045 RID: 69
		private UserInfo m_iuser;

		// Token: 0x04000046 RID: 70
		private string m_name = string.Empty;

		// Token: 0x04000047 RID: 71
		private int m_type;

		// Token: 0x04000048 RID: 72
		private int m_credits;

		// Token: 0x04000049 RID: 73
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400004A RID: 74
		private int m_doid;

		// Token: 0x0400004B RID: 75
		private string m_doname;
	}
}
