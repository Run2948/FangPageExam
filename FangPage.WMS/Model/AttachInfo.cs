using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Model
{
	// Token: 0x02000008 RID: 8
	[ModelPrefix("WMS")]
	public class AttachInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00003F41 File Offset: 0x00002141
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00003F49 File Offset: 0x00002149
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003F52 File Offset: 0x00002152
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00003F5A File Offset: 0x0000215A
		public string attachid
		{
			get
			{
				return this.m_attachid;
			}
			set
			{
				this.m_attachid = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00003F63 File Offset: 0x00002163
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00003F6B File Offset: 0x0000216B
		public int parentid
		{
			get
			{
				return this.m_parentid;
			}
			set
			{
				this.m_parentid = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00003F74 File Offset: 0x00002174
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00003F7C File Offset: 0x0000217C
		public string platform
		{
			get
			{
				return this.m_platform;
			}
			set
			{
				this.m_platform = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003F85 File Offset: 0x00002185
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00003F8D File Offset: 0x0000218D
		[LeftJoin("WMS_UserInfo", "id")]
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00003F96 File Offset: 0x00002196
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00003F9E File Offset: 0x0000219E
		[Map("WMS_UserInfo", "username")]
		public string username
		{
			get
			{
				return this.m_username;
			}
			set
			{
				this.m_username = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003FA7 File Offset: 0x000021A7
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00003FAF File Offset: 0x000021AF
		[Map("WMS_UserInfo", "realname")]
		public string realname
		{
			get
			{
				return this.m_realname;
			}
			set
			{
				this.m_realname = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003FB8 File Offset: 0x000021B8
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00003FC0 File Offset: 0x000021C0
		public string app
		{
			get
			{
				return this.m_app;
			}
			set
			{
				this.m_app = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003FC9 File Offset: 0x000021C9
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00003FD1 File Offset: 0x000021D1
		public int postid
		{
			get
			{
				return this.m_postid;
			}
			set
			{
				this.m_postid = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003FDA File Offset: 0x000021DA
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003FE2 File Offset: 0x000021E2
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
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003FEB File Offset: 0x000021EB
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00003FF3 File Offset: 0x000021F3
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003FFC File Offset: 0x000021FC
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00004004 File Offset: 0x00002204
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00004010 File Offset: 0x00002210
		[BindField(false)]
		public string icon
		{
			get
			{
				string text = Path.GetExtension(this.filename);
				if (text.StartsWith("."))
				{
					text = text.Substring(1, text.Length - 1);
				}
				string text2 = WebConfig.WebPath + "common/file/" + text + ".gif";
				if (File.Exists(FPFile.GetMapPath(text2)))
				{
					return text2;
				}
				text2 = WebConfig.WebPath + "common/file/" + text + ".png";
				if (File.Exists(FPFile.GetMapPath(text2)))
				{
					return text2;
				}
				return WebConfig.WebPath + "common/file/unknow.gif";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000409F File Offset: 0x0000229F
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000040A7 File Offset: 0x000022A7
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000040B0 File Offset: 0x000022B0
		[BindField(false)]
		public string size
		{
			get
			{
				this.m_size = FPFile.FormatBytesStr(this.filesize);
				return this.m_size;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000040C9 File Offset: 0x000022C9
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000040D1 File Offset: 0x000022D1
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000040DA File Offset: 0x000022DA
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000040E2 File Offset: 0x000022E2
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000040EB File Offset: 0x000022EB
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000040F3 File Offset: 0x000022F3
		public int reads
		{
			get
			{
				return this.m_reads;
			}
			set
			{
				this.m_reads = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000040FC File Offset: 0x000022FC
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00004104 File Offset: 0x00002304
		public DateTime uploadtime
		{
			get
			{
				return this.m_uploadtime;
			}
			set
			{
				this.m_uploadtime = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000410D File Offset: 0x0000230D
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00004115 File Offset: 0x00002315
		public int issync
		{
			get
			{
				return this.m_issync;
			}
			set
			{
				this.m_issync = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000411E File Offset: 0x0000231E
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00004126 File Offset: 0x00002326
		[TempField(true)]
		public int departid
		{
			get
			{
				return this.m_departid;
			}
			set
			{
				this.m_departid = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000412F File Offset: 0x0000232F
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00004137 File Offset: 0x00002337
		[TempField(true)]
		public int userid
		{
			get
			{
				return this.m_userid;
			}
			set
			{
				this.m_userid = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004140 File Offset: 0x00002340
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00004148 File Offset: 0x00002348
		[TempField(true)]
		public int isview
		{
			get
			{
				return this.m_isview;
			}
			set
			{
				this.m_isview = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00004151 File Offset: 0x00002351
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00004159 File Offset: 0x00002359
		[TempField(true)]
		public int iseditor
		{
			get
			{
				return this.m_iseditor;
			}
			set
			{
				this.m_iseditor = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00004162 File Offset: 0x00002362
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000416A File Offset: 0x0000236A
		[TempField(true)]
		public int isdownload
		{
			get
			{
				return this.m_isdownload;
			}
			set
			{
				this.m_isdownload = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00004173 File Offset: 0x00002373
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000417B File Offset: 0x0000237B
		[TempField(true)]
		public int isdelete
		{
			get
			{
				return this.m_isdelete;
			}
			set
			{
				this.m_isdelete = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00004184 File Offset: 0x00002384
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000418C File Offset: 0x0000238C
		[BindField(false)]
		public string error
		{
			get
			{
				return this.m_error;
			}
			set
			{
				this.m_error = value;
			}
		}

		// Token: 0x0400002C RID: 44
		private int m_id;

		// Token: 0x0400002D RID: 45
		private string m_attachid = string.Empty;

		// Token: 0x0400002E RID: 46
		private int m_parentid;

		// Token: 0x0400002F RID: 47
		private string m_platform = string.Empty;

		// Token: 0x04000030 RID: 48
		private int m_uid;

		// Token: 0x04000031 RID: 49
		private string m_username = string.Empty;

		// Token: 0x04000032 RID: 50
		private string m_realname = string.Empty;

		// Token: 0x04000033 RID: 51
		private string m_app;

		// Token: 0x04000034 RID: 52
		private int m_postid;

		// Token: 0x04000035 RID: 53
		private string m_name = string.Empty;

		// Token: 0x04000036 RID: 54
		private string m_filename = string.Empty;

		// Token: 0x04000037 RID: 55
		private string m_filetype = string.Empty;

		// Token: 0x04000038 RID: 56
		private long m_filesize;

		// Token: 0x04000039 RID: 57
		private string m_size = string.Empty;

		// Token: 0x0400003A RID: 58
		private string m_description = string.Empty;

		// Token: 0x0400003B RID: 59
		private int m_downloads;

		// Token: 0x0400003C RID: 60
		private int m_reads;

		// Token: 0x0400003D RID: 61
		private DateTime m_uploadtime = DbUtils.GetDateTime();

		// Token: 0x0400003E RID: 62
		private int m_issync;

		// Token: 0x0400003F RID: 63
		private int m_departid;

		// Token: 0x04000040 RID: 64
		private int m_userid;

		// Token: 0x04000041 RID: 65
		private int m_isview;

		// Token: 0x04000042 RID: 66
		private int m_iseditor;

		// Token: 0x04000043 RID: 67
		private int m_isdownload;

		// Token: 0x04000044 RID: 68
		private int m_isdelete;

		// Token: 0x04000045 RID: 69
		private string m_error = string.Empty;
	}
}
