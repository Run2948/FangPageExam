using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001C RID: 28
	[ModelPrefix("WMS")]
	public class MenuInfo
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006138 File Offset: 0x00004338
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000614F File Offset: 0x0000434F
		[PrimaryKey(true)]
		[Identity(true)]
		public int id { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006158 File Offset: 0x00004358
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000616F File Offset: 0x0000436F
		public int parentid { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006178 File Offset: 0x00004378
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00006190 File Offset: 0x00004390
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000619C File Offset: 0x0000439C
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000061B4 File Offset: 0x000043B4
		public string lefturl
		{
			get
			{
				return this.m_lefturl;
			}
			set
			{
				this.m_lefturl = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000061C0 File Offset: 0x000043C0
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000061D8 File Offset: 0x000043D8
		public string righturl
		{
			get
			{
				return this.m_righturl;
			}
			set
			{
				this.m_righturl = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000061E4 File Offset: 0x000043E4
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000061FB File Offset: 0x000043FB
		public int display { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006204 File Offset: 0x00004404
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000621B File Offset: 0x0000441B
		public int system { get; set; }

		// Token: 0x04000085 RID: 133
		private string m_name = "";

		// Token: 0x04000086 RID: 134
		private string m_lefturl = "";

		// Token: 0x04000087 RID: 135
		private string m_righturl = "";
	}
}
