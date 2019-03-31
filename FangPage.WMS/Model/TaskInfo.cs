using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001C RID: 28
	[ModelPrefix("WMS")]
	public class TaskInfo
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00005F3D File Offset: 0x0000413D
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00005F45 File Offset: 0x00004145
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

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005F4E File Offset: 0x0000414E
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00005F56 File Offset: 0x00004156
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00005F5F File Offset: 0x0000415F
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00005F67 File Offset: 0x00004167
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00005F70 File Offset: 0x00004170
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00005F78 File Offset: 0x00004178
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00005F81 File Offset: 0x00004181
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00005F89 File Offset: 0x00004189
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00005F92 File Offset: 0x00004192
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00005F9A File Offset: 0x0000419A
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005FA3 File Offset: 0x000041A3
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00005FAB File Offset: 0x000041AB
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

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005FB4 File Offset: 0x000041B4
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00005FBC File Offset: 0x000041BC
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

		// Token: 0x04000133 RID: 307
		private int m_id;

		// Token: 0x04000134 RID: 308
		private string m_name = string.Empty;

		// Token: 0x04000135 RID: 309
		private string m_key = string.Empty;

		// Token: 0x04000136 RID: 310
		private int m_timeofday = -1;

		// Token: 0x04000137 RID: 311
		private int m_minutes = 60;

		// Token: 0x04000138 RID: 312
		private string m_type = string.Empty;

		// Token: 0x04000139 RID: 313
		private string m_lastexecuted = string.Empty;

		// Token: 0x0400013A RID: 314
		private int m_enabled = 1;
	}
}
