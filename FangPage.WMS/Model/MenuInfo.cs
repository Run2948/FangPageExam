using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000011 RID: 17
	[ModelPrefix("WMS")]
	public class MenuInfo
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004F32 File Offset: 0x00003132
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004F3A File Offset: 0x0000313A
		[Identity(true)]
		[PrimaryKey(true)]
		public int id { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004F43 File Offset: 0x00003143
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004F4B File Offset: 0x0000314B
		public int parentid { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004F54 File Offset: 0x00003154
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00004F5C File Offset: 0x0000315C
		public string platform
		{
			get
			{
				return this.m_platform;
			}
			set
			{
				this.m_platform = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004F65 File Offset: 0x00003165
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00004F6D File Offset: 0x0000316D
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004F76 File Offset: 0x00003176
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00004F7E File Offset: 0x0000317E
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004F87 File Offset: 0x00003187
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00004F8F File Offset: 0x0000318F
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004F98 File Offset: 0x00003198
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00004FA0 File Offset: 0x000031A0
		public int display { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004FA9 File Offset: 0x000031A9
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00004FB1 File Offset: 0x000031B1
		public int hidden { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004FBA File Offset: 0x000031BA
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00004FC2 File Offset: 0x000031C2
		public int system { get; set; }

		// Token: 0x040000AA RID: 170
		private string m_platform = "";

		// Token: 0x040000AB RID: 171
		private string m_name = "";

		// Token: 0x040000AC RID: 172
		private string m_lefturl = "";

		// Token: 0x040000AD RID: 173
		private string m_righturl = "";
	}
}
