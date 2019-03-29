using System;
using FangPage.Data;

namespace FangPage.WMS.Task
{
	// Token: 0x0200003C RID: 60
	[ModelPrefix("WMS")]
	public class TaskInfo
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000A24C File Offset: 0x0000844C
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000A264 File Offset: 0x00008464
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

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000A270 File Offset: 0x00008470
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000A288 File Offset: 0x00008488
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
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000A294 File Offset: 0x00008494
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000A2AC File Offset: 0x000084AC
		public string key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000A2B8 File Offset: 0x000084B8
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public int timeofday
		{
			get
			{
				return this.m_timeofday;
			}
			set
			{
				this.m_timeofday = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000A2DC File Offset: 0x000084DC
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000A2F4 File Offset: 0x000084F4
		public int minutes
		{
			get
			{
				return this.m_minutes;
			}
			set
			{
				this.m_minutes = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000A300 File Offset: 0x00008500
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000A318 File Offset: 0x00008518
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000A324 File Offset: 0x00008524
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000A33C File Offset: 0x0000853C
		public string lastexecuted
		{
			get
			{
				return this.m_lastexecuted;
			}
			set
			{
				this.m_lastexecuted = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000A348 File Offset: 0x00008548
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000A360 File Offset: 0x00008560
		public int enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		// Token: 0x0400013E RID: 318
		private int m_id;

		// Token: 0x0400013F RID: 319
		private string m_name = string.Empty;

		// Token: 0x04000140 RID: 320
		private string m_key = string.Empty;

		// Token: 0x04000141 RID: 321
		private int m_timeofday = -1;

		// Token: 0x04000142 RID: 322
		private int m_minutes = 60;

		// Token: 0x04000143 RID: 323
		private string m_type = string.Empty;

		// Token: 0x04000144 RID: 324
		private string m_lastexecuted = string.Empty;

		// Token: 0x04000145 RID: 325
		private int m_enabled = 1;
	}
}
