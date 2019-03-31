using System;
using System.Collections.Generic;
using FangPage.Data;

namespace FP_Course.Model
{
	// Token: 0x02000005 RID: 5
	[ModelPrefix("Course")]
	public class ChapterInfo
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000024A4 File Offset: 0x000006A4
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024B0 File Offset: 0x000006B0
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000024C8 File Offset: 0x000006C8
		public int courseid
		{
			get
			{
				return this.m_courseid;
			}
			set
			{
				this.m_courseid = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024D4 File Offset: 0x000006D4
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024EC File Offset: 0x000006EC
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002510 File Offset: 0x00000710
		public int videos
		{
			get
			{
				return this.m_videos;
			}
			set
			{
				this.m_videos = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000251C File Offset: 0x0000071C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002534 File Offset: 0x00000734
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002540 File Offset: 0x00000740
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002558 File Offset: 0x00000758
		[TempField(true)]
		public int uid
		{
			get
			{
				return this.m_uid;
			}
			set
			{
				this.m_uid = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002564 File Offset: 0x00000764
		[BindField(false)]
		public List<VideoInfo> videolist
		{
			get
			{
				SqlParam[] array = new SqlParam[]
				{
					DbHelper.MakeAndWhere("courseid", this.courseid),
					DbHelper.MakeAndWhere("chapterid", this.id),
					DbHelper.MakeAndWhere("status", 1),
					DbHelper.MakeSet("uid", this.uid)
				};
				return DbHelper.ExecuteList<VideoInfo>(array);
			}
		}

		// Token: 0x04000008 RID: 8
		private int m_id;

		// Token: 0x04000009 RID: 9
		private int m_courseid;

		// Token: 0x0400000A RID: 10
		private string m_name = string.Empty;

		// Token: 0x0400000B RID: 11
		private int m_videos;

		// Token: 0x0400000C RID: 12
		private int m_display;

		// Token: 0x0400000D RID: 13
		private int m_uid;
	}
}
