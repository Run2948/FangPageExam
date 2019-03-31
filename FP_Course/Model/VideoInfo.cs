using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;

namespace FP_Course.Model
{
	// Token: 0x02000007 RID: 7
	[ModelPrefix("Course")]
	public class VideoInfo
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002A54 File Offset: 0x00000C54
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A78 File Offset: 0x00000C78
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002A84 File Offset: 0x00000C84
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002A9C File Offset: 0x00000C9C
		public int chapterid
		{
			get
			{
				return this.m_chapterid;
			}
			set
			{
				this.m_chapterid = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002AC0 File Offset: 0x00000CC0
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002ACC File Offset: 0x00000CCC
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public string videofile
		{
			get
			{
				return this.m_videofile;
			}
			set
			{
				this.m_videofile = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002AF0 File Offset: 0x00000CF0
		[BindField(false)]
		public string filetype
		{
			get
			{
				this.m_filetype = Path.GetExtension(this.videofile).ToLower();
				return this.m_filetype;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002B20 File Offset: 0x00000D20
		[BindField(false)]
		public string icon
		{
			get
			{
				string text = this.filetype;
				bool flag = text.StartsWith(".");
				if (flag)
				{
					text = text.Substring(1, text.Length - 1);
				}
				string text2 = WebConfig.WebPath + "common/file/" + text + ".gif";
				bool flag2 = File.Exists(FPFile.GetMapPath(text2));
				string result;
				if (flag2)
				{
					result = text2;
				}
				else
				{
					text2 = WebConfig.WebPath + "common/file/" + text + ".png";
					bool flag3 = File.Exists(FPFile.GetMapPath(text2));
					if (flag3)
					{
						result = text2;
					}
					else
					{
						result = WebConfig.WebPath + "common/file/unknow.gif";
					}
				}
				return result;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002BC4 File Offset: 0x00000DC4
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002BDC File Offset: 0x00000DDC
		public string img
		{
			get
			{
				return this.m_img;
			}
			set
			{
				this.m_img = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002BE8 File Offset: 0x00000DE8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002C00 File Offset: 0x00000E00
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002C24 File Offset: 0x00000E24
		public int videotime
		{
			get
			{
				return this.m_videotime;
			}
			set
			{
				this.m_videotime = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002C30 File Offset: 0x00000E30
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002C48 File Offset: 0x00000E48
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002C54 File Offset: 0x00000E54
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002C6C File Offset: 0x00000E6C
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002C78 File Offset: 0x00000E78
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002C90 File Offset: 0x00000E90
		public string studys
		{
			get
			{
				return this.m_studys;
			}
			set
			{
				this.m_studys = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002C9C File Offset: 0x00000E9C
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002CB4 File Offset: 0x00000EB4
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002CC0 File Offset: 0x00000EC0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002CD8 File Offset: 0x00000ED8
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002CE4 File Offset: 0x00000EE4
		[BindField(false)]
		public int study_status
		{
			get
			{
				int result = 0;
				bool flag = FPArray.InArray(this.uid, this.studys) >= 0;
				if (flag)
				{
					result = 1;
				}
				return result;
			}
		}

		// Token: 0x04000024 RID: 36
		private int m_id;

		// Token: 0x04000025 RID: 37
		private int m_courseid;

		// Token: 0x04000026 RID: 38
		private int m_chapterid;

		// Token: 0x04000027 RID: 39
		private string m_name = string.Empty;

		// Token: 0x04000028 RID: 40
		private string m_videofile = string.Empty;

		// Token: 0x04000029 RID: 41
		private string m_filetype = string.Empty;

		// Token: 0x0400002A RID: 42
		private string m_img = string.Empty;

		// Token: 0x0400002B RID: 43
		private string m_attachid = string.Empty;

		// Token: 0x0400002C RID: 44
		private int m_videotime;

		// Token: 0x0400002D RID: 45
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400002E RID: 46
		private int m_display;

		// Token: 0x0400002F RID: 47
		private string m_studys = string.Empty;

		// Token: 0x04000030 RID: 48
		private int m_status = 1;

		// Token: 0x04000031 RID: 49
		private int m_uid;
	}
}
