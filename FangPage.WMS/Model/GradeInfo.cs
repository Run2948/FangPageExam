using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001B RID: 27
	[ModelPrefix("WMS")]
	public class GradeInfo
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00005EDB File Offset: 0x000040DB
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00005EE3 File Offset: 0x000040E3
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

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00005EEC File Offset: 0x000040EC
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00005EF4 File Offset: 0x000040F4
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005EFD File Offset: 0x000040FD
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00005F05 File Offset: 0x00004105
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

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00005F0E File Offset: 0x0000410E
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00005F16 File Offset: 0x00004116
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

		// Token: 0x0400012F RID: 303
		private int m_id;

		// Token: 0x04000130 RID: 304
		private int m_display;

		// Token: 0x04000131 RID: 305
		private string m_name = string.Empty;

		// Token: 0x04000132 RID: 306
		private string m_description = string.Empty;
	}
}
