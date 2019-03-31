using System;
using FangPage.Common;
using FangPage.Data;

namespace FP_Course.Model
{
	// Token: 0x02000006 RID: 6
	[ModelPrefix("Course")]
	public class CourseInfo
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025F4 File Offset: 0x000007F4
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000260C File Offset: 0x0000080C
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002618 File Offset: 0x00000818
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002630 File Offset: 0x00000830
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000263C File Offset: 0x0000083C
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002654 File Offset: 0x00000854
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002678 File Offset: 0x00000878
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002684 File Offset: 0x00000884
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000269C File Offset: 0x0000089C
		[LeftJoin("WMS_SortInfo", "id")]
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
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000026C0 File Offset: 0x000008C0
		[Map("WMS_SortInfo", "name")]
		public string sortname
		{
			get
			{
				return this.m_sortname;
			}
			set
			{
				this.m_sortname = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026CC File Offset: 0x000008CC
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000026E4 File Offset: 0x000008E4
		public string typelist
		{
			get
			{
				return this.m_typelist;
			}
			set
			{
				this.m_typelist = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026F0 File Offset: 0x000008F0
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002708 File Offset: 0x00000908
		public string teacherid
		{
			get
			{
				return this.m_teacherid;
			}
			set
			{
				this.m_teacherid = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002714 File Offset: 0x00000914
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000272C File Offset: 0x0000092C
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002750 File Offset: 0x00000950
		public double price
		{
			get
			{
				return this.m_price;
			}
			set
			{
				this.m_price = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000275C File Offset: 0x0000095C
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002774 File Offset: 0x00000974
		public double old_price
		{
			get
			{
				return this.m_old_price;
			}
			set
			{
				this.m_old_price = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002780 File Offset: 0x00000980
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002798 File Offset: 0x00000998
		[NText(true)]
		public string content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027A4 File Offset: 0x000009A4
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000027BC File Offset: 0x000009BC
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027C8 File Offset: 0x000009C8
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000027E0 File Offset: 0x000009E0
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000027EC File Offset: 0x000009EC
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002804 File Offset: 0x00000A04
		public int istop
		{
			get
			{
				return this.m_istop;
			}
			set
			{
				this.m_istop = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002810 File Offset: 0x00000A10
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002828 File Offset: 0x00000A28
		public int learns
		{
			get
			{
				return this.m_learns;
			}
			set
			{
				this.m_learns = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002834 File Offset: 0x00000A34
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000284C File Offset: 0x00000A4C
		public string buys
		{
			get
			{
				return this.m_buys;
			}
			set
			{
				this.m_buys = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002858 File Offset: 0x00000A58
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002870 File Offset: 0x00000A70
		public int views
		{
			get
			{
				return this.m_views;
			}
			set
			{
				this.m_views = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002894 File Offset: 0x00000A94
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028A0 File Offset: 0x00000AA0
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000028B8 File Offset: 0x00000AB8
		public string favorites
		{
			get
			{
				return this.m_favorites;
			}
			set
			{
				this.m_favorites = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000028C4 File Offset: 0x00000AC4
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000028DC File Offset: 0x00000ADC
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028E8 File Offset: 0x00000AE8
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002900 File Offset: 0x00000B00
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000290C File Offset: 0x00000B0C
		[BindField(false)]
		public int buy_status
		{
			get
			{
				int result = 0;
				bool flag = FPArray.InArray(this.userid, this.buys) >= 0;
				if (flag)
				{
					result = 1;
				}
				return result;
			}
		}

		// Token: 0x0400000E RID: 14
		private int m_id;

		// Token: 0x0400000F RID: 15
		private int m_uid;

		// Token: 0x04000010 RID: 16
		private string m_realname = string.Empty;

		// Token: 0x04000011 RID: 17
		private int m_channelid;

		// Token: 0x04000012 RID: 18
		private int m_sortid;

		// Token: 0x04000013 RID: 19
		private string m_sortname = string.Empty;

		// Token: 0x04000014 RID: 20
		private string m_typelist = string.Empty;

		// Token: 0x04000015 RID: 21
		private string m_teacherid = string.Empty;

		// Token: 0x04000016 RID: 22
		private string m_name = string.Empty;

		// Token: 0x04000017 RID: 23
		private double m_price;

		// Token: 0x04000018 RID: 24
		private double m_old_price;

		// Token: 0x04000019 RID: 25
		private string m_content = string.Empty;

		// Token: 0x0400001A RID: 26
		private string m_img = string.Empty;

		// Token: 0x0400001B RID: 27
		private string m_attachid = string.Empty;

		// Token: 0x0400001C RID: 28
		private int m_istop;

		// Token: 0x0400001D RID: 29
		private int m_learns;

		// Token: 0x0400001E RID: 30
		private string m_buys = string.Empty;

		// Token: 0x0400001F RID: 31
		private int m_views;

		// Token: 0x04000020 RID: 32
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x04000021 RID: 33
		private string m_favorites = string.Empty;

		// Token: 0x04000022 RID: 34
		private int m_status;

		// Token: 0x04000023 RID: 35
		private int m_userid;
	}
}
