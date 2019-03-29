using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000018 RID: 24
	[ModelPrefix("WMS")]
	public class Department
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000567C File Offset: 0x0000387C
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00005694 File Offset: 0x00003894
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000056A0 File Offset: 0x000038A0
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000056B8 File Offset: 0x000038B8
		public int parentid
		{
			get
			{
				return this.m_parentid;
			}
			set
			{
				this.m_parentid = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000056C4 File Offset: 0x000038C4
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000056DC File Offset: 0x000038DC
		public string parentlist
		{
			get
			{
				return this.m_parentlist;
			}
			set
			{
				this.m_parentlist = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000056E8 File Offset: 0x000038E8
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00005700 File Offset: 0x00003900
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000570C File Offset: 0x0000390C
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005724 File Offset: 0x00003924
		public int usercount
		{
			get
			{
				return this.m_usercount;
			}
			set
			{
				this.m_usercount = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005730 File Offset: 0x00003930
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00005748 File Offset: 0x00003948
		public string description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005754 File Offset: 0x00003954
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000576C File Offset: 0x0000396C
		public int display
		{
			get
			{
				return this.m_display;
			}
			set
			{
				this.m_display = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005778 File Offset: 0x00003978
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00005790 File Offset: 0x00003990
		public int subcounts
		{
			get
			{
				return this.m_subcounts;
			}
			set
			{
				this.m_subcounts = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000579C File Offset: 0x0000399C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000057B4 File Offset: 0x000039B4
		public int posts
		{
			get
			{
				return this.m_posts;
			}
			set
			{
				this.m_posts = value;
			}
		}

		// Token: 0x0400004C RID: 76
		private int m_id;

		// Token: 0x0400004D RID: 77
		private int m_parentid;

		// Token: 0x0400004E RID: 78
		private string m_parentlist = string.Empty;

		// Token: 0x0400004F RID: 79
		private string m_name = string.Empty;

		// Token: 0x04000050 RID: 80
		private int m_usercount;

		// Token: 0x04000051 RID: 81
		private string m_description = string.Empty;

		// Token: 0x04000052 RID: 82
		private int m_display;

		// Token: 0x04000053 RID: 83
		private int m_subcounts;

		// Token: 0x04000054 RID: 84
		private int m_posts;
	}
}
