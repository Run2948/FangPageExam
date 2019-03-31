using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000009 RID: 9
	[ModelPrefix("WMS")]
	public class AttachType
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004224 File Offset: 0x00002424
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000422C File Offset: 0x0000242C
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00004235 File Offset: 0x00002435
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000423D File Offset: 0x0000243D
		public string extension
		{
			get
			{
				return this.m_extension;
			}
			set
			{
				this.m_extension = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004246 File Offset: 0x00002446
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000424E File Offset: 0x0000244E
		public int maxsize
		{
			get
			{
				return this.m_maxsize;
			}
			set
			{
				this.m_maxsize = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004257 File Offset: 0x00002457
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000425F File Offset: 0x0000245F
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

		// Token: 0x04000046 RID: 70
		private int m_id;

		// Token: 0x04000047 RID: 71
		private string m_extension = string.Empty;

		// Token: 0x04000048 RID: 72
		private int m_maxsize;

		// Token: 0x04000049 RID: 73
		private string m_type = string.Empty;
	}
}
