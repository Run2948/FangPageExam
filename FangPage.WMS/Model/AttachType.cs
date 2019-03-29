using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000015 RID: 21
	[ModelPrefix("WMS")]
	public class AttachType
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000053C4 File Offset: 0x000035C4
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000053DC File Offset: 0x000035DC
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000053E8 File Offset: 0x000035E8
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00005400 File Offset: 0x00003600
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
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000540C File Offset: 0x0000360C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00005424 File Offset: 0x00003624
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
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00005430 File Offset: 0x00003630
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00005448 File Offset: 0x00003648
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

		// Token: 0x0400003B RID: 59
		private int m_id;

		// Token: 0x0400003C RID: 60
		private string m_extension = string.Empty;

		// Token: 0x0400003D RID: 61
		private int m_maxsize;

		// Token: 0x0400003E RID: 62
		private string m_type = string.Empty;
	}
}
