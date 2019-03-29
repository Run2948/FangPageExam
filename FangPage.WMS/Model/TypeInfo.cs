using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000028 RID: 40
	[ModelPrefix("WMS")]
	public class TypeInfo
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007444 File Offset: 0x00005644
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000745C File Offset: 0x0000565C
		[Identity(true)]
		[PrimaryKey(true)]
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007468 File Offset: 0x00005668
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00007480 File Offset: 0x00005680
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000748C File Offset: 0x0000568C
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000074A4 File Offset: 0x000056A4
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000074B0 File Offset: 0x000056B0
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000074C8 File Offset: 0x000056C8
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000074D4 File Offset: 0x000056D4
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000074EC File Offset: 0x000056EC
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000074F8 File Offset: 0x000056F8
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00007510 File Offset: 0x00005710
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000751C File Offset: 0x0000571C
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00007534 File Offset: 0x00005734
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00007540 File Offset: 0x00005740
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00007558 File Offset: 0x00005758
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00007564 File Offset: 0x00005764
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000757C File Offset: 0x0000577C
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

		// Token: 0x040000FE RID: 254
		private int m_id;

		// Token: 0x040000FF RID: 255
		private int m_parentid;

		// Token: 0x04000100 RID: 256
		private string m_name = string.Empty;

		// Token: 0x04000101 RID: 257
		private string m_markup = string.Empty;

		// Token: 0x04000102 RID: 258
		private string m_description = string.Empty;

		// Token: 0x04000103 RID: 259
		private int m_display;

		// Token: 0x04000104 RID: 260
		private string m_img = string.Empty;

		// Token: 0x04000105 RID: 261
		private int m_subcounts;

		// Token: 0x04000106 RID: 262
		private int m_posts;
	}
}
