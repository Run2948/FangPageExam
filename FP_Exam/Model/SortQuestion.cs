using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x02000014 RID: 20
	[ModelPrefix("Exam")]
	public class SortQuestion
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000062E4 File Offset: 0x000044E4
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x000062FC File Offset: 0x000044FC
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006308 File Offset: 0x00004508
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00006320 File Offset: 0x00004520
		public int channelid
		{
			get
			{
				return this.m_channelid;
			}
			set
			{
				this.m_channelid = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000632C File Offset: 0x0000452C
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00006344 File Offset: 0x00004544
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00006350 File Offset: 0x00004550
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00006368 File Offset: 0x00004568
		public int sortid
		{
			get
			{
				return this.m_sortid;
			}
			set
			{
				this.m_sortid = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006374 File Offset: 0x00004574
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000638C File Offset: 0x0000458C
		public int typeid
		{
			get
			{
				return this.m_typeid;
			}
			set
			{
				this.m_typeid = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00006398 File Offset: 0x00004598
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000063B0 File Offset: 0x000045B0
		public int counts
		{
			get
			{
				return this.m_counts;
			}
			set
			{
				this.m_counts = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000063BC File Offset: 0x000045BC
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000063D4 File Offset: 0x000045D4
		public string questionlist
		{
			get
			{
				return this.m_questionlist;
			}
			set
			{
				this.m_questionlist = value;
			}
		}

		// Token: 0x040000D3 RID: 211
		private int m_id;

		// Token: 0x040000D4 RID: 212
		private int m_channelid;

		// Token: 0x040000D5 RID: 213
		private string m_type;

		// Token: 0x040000D6 RID: 214
		private int m_sortid;

		// Token: 0x040000D7 RID: 215
		private int m_typeid;

		// Token: 0x040000D8 RID: 216
		private int m_counts;

		// Token: 0x040000D9 RID: 217
		private string m_questionlist = string.Empty;
	}
}
