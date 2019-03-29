using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000014 RID: 20
	[ModelPrefix("WMS")]
	public class AttachInfo
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000051C4 File Offset: 0x000033C4
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000051DC File Offset: 0x000033DC
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000051E8 File Offset: 0x000033E8
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00005200 File Offset: 0x00003400
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000520C File Offset: 0x0000340C
		[BindField(false)]
		public UserInfo UserInfo
		{
			get
			{
				if (this.m_iuser == null)
				{
					this.m_iuser = DbHelper.ExecuteModel<UserInfo>(this.uid);
				}
				return this.m_iuser;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00005248 File Offset: 0x00003448
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00005260 File Offset: 0x00003460
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000526C File Offset: 0x0000346C
		[BindField(false)]
		public SortInfo SortInfo
		{
			get
			{
				if (this.m_sortinfo == null)
				{
					this.m_sortinfo = DbHelper.ExecuteModel<SortInfo>(this.sortid);
				}
				return this.m_sortinfo;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000052A8 File Offset: 0x000034A8
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000052C0 File Offset: 0x000034C0
		public string filename
		{
			get
			{
				return this.m_filename;
			}
			set
			{
				this.m_filename = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000052CC File Offset: 0x000034CC
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000052E4 File Offset: 0x000034E4
		public string filetype
		{
			get
			{
				return this.m_filetype;
			}
			set
			{
				this.m_filetype = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000052F0 File Offset: 0x000034F0
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00005308 File Offset: 0x00003508
		public long filesize
		{
			get
			{
				return this.m_filesize;
			}
			set
			{
				this.m_filesize = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005314 File Offset: 0x00003514
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000532C File Offset: 0x0000352C
		public string originalname
		{
			get
			{
				return this.m_originalname;
			}
			set
			{
				this.m_originalname = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005338 File Offset: 0x00003538
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00005350 File Offset: 0x00003550
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000535C File Offset: 0x0000355C
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00005374 File Offset: 0x00003574
		public DateTime postdatetime
		{
			get
			{
				return this.m_postdatetime;
			}
			set
			{
				this.m_postdatetime = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005380 File Offset: 0x00003580
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00005398 File Offset: 0x00003598
		public int downloads
		{
			get
			{
				return this.m_downloads;
			}
			set
			{
				this.m_downloads = value;
			}
		}

		// Token: 0x0400002F RID: 47
		private int m_id;

		// Token: 0x04000030 RID: 48
		private int m_uid;

		// Token: 0x04000031 RID: 49
		private UserInfo m_iuser;

		// Token: 0x04000032 RID: 50
		private int m_sortid;

		// Token: 0x04000033 RID: 51
		private SortInfo m_sortinfo;

		// Token: 0x04000034 RID: 52
		private string m_filename = string.Empty;

		// Token: 0x04000035 RID: 53
		private string m_filetype = string.Empty;

		// Token: 0x04000036 RID: 54
		private long m_filesize;

		// Token: 0x04000037 RID: 55
		private string m_originalname = string.Empty;

		// Token: 0x04000038 RID: 56
		private string m_description = string.Empty;

		// Token: 0x04000039 RID: 57
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400003A RID: 58
		private int m_downloads;
	}
}
