using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000014 RID: 20
	[ModelPrefix("WMS")]
	public class MsgTempInfo
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000056CD File Offset: 0x000038CD
		// (set) Token: 0x06000190 RID: 400 RVA: 0x000056D5 File Offset: 0x000038D5
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000056DE File Offset: 0x000038DE
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000056E6 File Offset: 0x000038E6
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000056EF File Offset: 0x000038EF
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000056F7 File Offset: 0x000038F7
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005700 File Offset: 0x00003900
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00005708 File Offset: 0x00003908
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005711 File Offset: 0x00003911
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00005719 File Offset: 0x00003919
		public string subject
		{
			get
			{
				return this.m_subject;
			}
			set
			{
				this.m_subject = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005722 File Offset: 0x00003922
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000572A File Offset: 0x0000392A
		public string content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005733 File Offset: 0x00003933
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000573B File Offset: 0x0000393B
		public int status
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x040000DF RID: 223
		private int m_id;

		// Token: 0x040000E0 RID: 224
		private int m_type;

		// Token: 0x040000E1 RID: 225
		private string m_name = string.Empty;

		// Token: 0x040000E2 RID: 226
		private string m_markup = string.Empty;

		// Token: 0x040000E3 RID: 227
		private string m_subject = string.Empty;

		// Token: 0x040000E4 RID: 228
		private string m_content = string.Empty;

		// Token: 0x040000E5 RID: 229
		private int m_status = 1;
	}
}
