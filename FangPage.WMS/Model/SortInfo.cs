using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000021 RID: 33
	[ModelPrefix("WMS")]
	public class SortInfo
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000068D8 File Offset: 0x00004AD8
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000068F0 File Offset: 0x00004AF0
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000068FC File Offset: 0x00004AFC
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00006914 File Offset: 0x00004B14
		public int channelid
		{
			get
			{
				return this.m_channelid;
			}
			set
			{
				this.m_channelid = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00006920 File Offset: 0x00004B20
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00006938 File Offset: 0x00004B38
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006944 File Offset: 0x00004B44
		[BindField(false)]
		public SortAppInfo SortAppInfo
		{
			get
			{
				if (this.m_sortappinfo == null)
				{
					this.m_sortappinfo = DbHelper.ExecuteModel<SortAppInfo>(this.appid);
				}
				return this.m_sortappinfo;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00006980 File Offset: 0x00004B80
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00006998 File Offset: 0x00004B98
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000069A4 File Offset: 0x00004BA4
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000069BC File Offset: 0x00004BBC
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000069C8 File Offset: 0x00004BC8
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000069E0 File Offset: 0x00004BE0
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000069EC File Offset: 0x00004BEC
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00006A04 File Offset: 0x00004C04
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006A10 File Offset: 0x00004C10
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00006A28 File Offset: 0x00004C28
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006A34 File Offset: 0x00004C34
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00006A4C File Offset: 0x00004C4C
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006A58 File Offset: 0x00004C58
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006A70 File Offset: 0x00004C70
		public string icon
		{
			get
			{
				return this.m_icon;
			}
			set
			{
				this.m_icon = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00006A7C File Offset: 0x00004C7C
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00006A94 File Offset: 0x00004C94
		public string img
		{
			get
			{
				return this.m_img;
			}
			set
			{
				this.m_img = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006AA0 File Offset: 0x00004CA0
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00006AB8 File Offset: 0x00004CB8
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006AC4 File Offset: 0x00004CC4
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00006ADC File Offset: 0x00004CDC
		public string types
		{
			get
			{
				return this.m_types;
			}
			set
			{
				this.m_types = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00006AE8 File Offset: 0x00004CE8
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00006B00 File Offset: 0x00004D00
		public string otherurl
		{
			get
			{
				return this.m_otherurl;
			}
			set
			{
				this.m_otherurl = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006B0C File Offset: 0x00004D0C
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00006B24 File Offset: 0x00004D24
		public int hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006B30 File Offset: 0x00004D30
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00006B48 File Offset: 0x00004D48
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

		// Token: 0x040000AD RID: 173
		private int m_id;

		// Token: 0x040000AE RID: 174
		private int m_channelid;

		// Token: 0x040000AF RID: 175
		private int m_appid;

		// Token: 0x040000B0 RID: 176
		private int m_display;

		// Token: 0x040000B1 RID: 177
		private int m_parentid;

		// Token: 0x040000B2 RID: 178
		private string m_parentlist = string.Empty;

		// Token: 0x040000B3 RID: 179
		private string m_name = string.Empty;

		// Token: 0x040000B4 RID: 180
		private string m_markup = string.Empty;

		// Token: 0x040000B5 RID: 181
		private string m_description = string.Empty;

		// Token: 0x040000B6 RID: 182
		private string m_icon = string.Empty;

		// Token: 0x040000B7 RID: 183
		private string m_img = string.Empty;

		// Token: 0x040000B8 RID: 184
		private int m_subcounts;

		// Token: 0x040000B9 RID: 185
		private string m_types = string.Empty;

		// Token: 0x040000BA RID: 186
		private string m_otherurl = string.Empty;

		// Token: 0x040000BB RID: 187
		private int m_hidden;

		// Token: 0x040000BC RID: 188
		private int m_posts;

		// Token: 0x040000BD RID: 189
		private SortAppInfo m_sortappinfo;
	}
}
