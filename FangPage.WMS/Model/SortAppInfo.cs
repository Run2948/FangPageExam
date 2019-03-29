using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000020 RID: 32
	[ModelPrefix("WMS")]
	public class SortAppInfo
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006730 File Offset: 0x00004930
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006748 File Offset: 0x00004948
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006754 File Offset: 0x00004954
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000676C File Offset: 0x0000496C
		public int appid
		{
			get
			{
				return this.m_appid;
			}
			set
			{
				this.m_appid = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006778 File Offset: 0x00004978
		[BindField(false)]
		public AppInfo AppInfo
		{
			get
			{
				if (this.m_appinfo == null)
				{
					this.m_appinfo = DbHelper.ExecuteModel<AppInfo>(this.appid);
				}
				return this.m_appinfo;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000067B4 File Offset: 0x000049B4
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000067CC File Offset: 0x000049CC
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000067D8 File Offset: 0x000049D8
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000067F0 File Offset: 0x000049F0
		public string markup
		{
			get
			{
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000067FC File Offset: 0x000049FC
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00006814 File Offset: 0x00004A14
		public string indexpage
		{
			get
			{
				return this.m_indexpage;
			}
			set
			{
				this.m_indexpage = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006820 File Offset: 0x00004A20
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00006838 File Offset: 0x00004A38
		public string viewpage
		{
			get
			{
				return this.m_viewpage;
			}
			set
			{
				this.m_viewpage = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00006844 File Offset: 0x00004A44
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000685C File Offset: 0x00004A5C
		public string installpath
		{
			get
			{
				return this.m_installpath;
			}
			set
			{
				this.m_installpath = value;
			}
		}

		// Token: 0x040000A5 RID: 165
		private int m_id;

		// Token: 0x040000A6 RID: 166
		private int m_appid;

		// Token: 0x040000A7 RID: 167
		private AppInfo m_appinfo;

		// Token: 0x040000A8 RID: 168
		private string m_name = string.Empty;

		// Token: 0x040000A9 RID: 169
		private string m_markup = string.Empty;

		// Token: 0x040000AA RID: 170
		private string m_indexpage = string.Empty;

		// Token: 0x040000AB RID: 171
		private string m_viewpage = string.Empty;

		// Token: 0x040000AC RID: 172
		private string m_installpath = string.Empty;
	}
}
