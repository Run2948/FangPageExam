using System;
using FangPage.Common;

namespace FangPage.WMS.Model
{
	// Token: 0x02000012 RID: 18
	public class UserExtend
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004FFF File Offset: 0x000031FF
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005007 File Offset: 0x00003207
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005010 File Offset: 0x00003210
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005036 File Offset: 0x00003236
		public string markup
		{
			get
			{
				if (this.m_markup == "")
				{
					this.m_markup = this.name;
				}
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000503F File Offset: 0x0000323F
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005047 File Offset: 0x00003247
		public string type
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005050 File Offset: 0x00003250
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00005058 File Offset: 0x00003258
		public string typedata
		{
			get
			{
				return this.m_typedata;
			}
			set
			{
				this.m_typedata = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005061 File Offset: 0x00003261
		public string[] datalist
		{
			get
			{
				return FPArray.SplitString(this.typedata);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000506E File Offset: 0x0000326E
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00005076 File Offset: 0x00003276
		public string defvalue
		{
			get
			{
				return this.m_defvalue;
			}
			set
			{
				this.m_defvalue = value;
			}
		}

		// Token: 0x040000B1 RID: 177
		private string m_name = string.Empty;

		// Token: 0x040000B2 RID: 178
		private string m_markup = string.Empty;

		// Token: 0x040000B3 RID: 179
		private string m_type = string.Empty;

		// Token: 0x040000B4 RID: 180
		private string m_typedata = string.Empty;

		// Token: 0x040000B5 RID: 181
		private string m_defvalue = string.Empty;
	}
}
