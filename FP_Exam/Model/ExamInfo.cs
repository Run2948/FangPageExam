using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;

namespace FP_Exam.Model
{
	// Token: 0x0200000A RID: 10
	[ModelPrefix("Exam")]
	public class ExamInfo
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004070 File Offset: 0x00002270
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00004088 File Offset: 0x00002288
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004094 File Offset: 0x00002294
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000040AC File Offset: 0x000022AC
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000040B8 File Offset: 0x000022B8
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000040D0 File Offset: 0x000022D0
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000040DC File Offset: 0x000022DC
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000040F4 File Offset: 0x000022F4
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004100 File Offset: 0x00002300
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00004118 File Offset: 0x00002318
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00004124 File Offset: 0x00002324
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000413C File Offset: 0x0000233C
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004148 File Offset: 0x00002348
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00004160 File Offset: 0x00002360
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000416C File Offset: 0x0000236C
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00004184 File Offset: 0x00002384
		public int type
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004190 File Offset: 0x00002390
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000041A8 File Offset: 0x000023A8
		public double total
		{
			get
			{
				return this.m_total;
			}
			set
			{
				this.m_total = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000041B4 File Offset: 0x000023B4
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000041CC File Offset: 0x000023CC
		public double passmark
		{
			get
			{
				return this.m_passmark;
			}
			set
			{
				this.m_passmark = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000041D8 File Offset: 0x000023D8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000041F0 File Offset: 0x000023F0
		public int examtime
		{
			get
			{
				return this.m_examtime;
			}
			set
			{
				this.m_examtime = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000041FC File Offset: 0x000023FC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004214 File Offset: 0x00002414
		public int islimit
		{
			get
			{
				return this.m_islimit;
			}
			set
			{
				this.m_islimit = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004220 File Offset: 0x00002420
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00004238 File Offset: 0x00002438
		public DateTime starttime
		{
			get
			{
				return this.m_starttime;
			}
			set
			{
				this.m_starttime = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000425C File Offset: 0x0000245C
		public DateTime endtime
		{
			get
			{
				return this.m_endtime;
			}
			set
			{
				this.m_endtime = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004268 File Offset: 0x00002468
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00004280 File Offset: 0x00002480
		public int repeats
		{
			get
			{
				return this.m_repeats;
			}
			set
			{
				this.m_repeats = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000428C File Offset: 0x0000248C
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000042A4 File Offset: 0x000024A4
		public int showanswer
		{
			get
			{
				return this.m_showanswer;
			}
			set
			{
				this.m_showanswer = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000042B0 File Offset: 0x000024B0
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000042C8 File Offset: 0x000024C8
		public int allowdelete
		{
			get
			{
				return this.m_allowdelete;
			}
			set
			{
				this.m_allowdelete = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000042D4 File Offset: 0x000024D4
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000042EC File Offset: 0x000024EC
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000042F8 File Offset: 0x000024F8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004310 File Offset: 0x00002510
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000431C File Offset: 0x0000251C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004334 File Offset: 0x00002534
		[CheckBox(true)]
		public int examtype
		{
			get
			{
				return this.m_examtype;
			}
			set
			{
				this.m_examtype = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004340 File Offset: 0x00002540
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00004358 File Offset: 0x00002558
		public string examroles
		{
			get
			{
				return this.m_examroles;
			}
			set
			{
				this.m_examroles = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004364 File Offset: 0x00002564
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000437C File Offset: 0x0000257C
		public string examdeparts
		{
			get
			{
				return this.m_examdeparts;
			}
			set
			{
				this.m_examdeparts = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004388 File Offset: 0x00002588
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000043A0 File Offset: 0x000025A0
		public string examuser
		{
			get
			{
				return this.m_examuser;
			}
			set
			{
				this.m_examuser = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000043AC File Offset: 0x000025AC
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000043C4 File Offset: 0x000025C4
		public int credits
		{
			get
			{
				return this.m_credits;
			}
			set
			{
				this.m_credits = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000043D0 File Offset: 0x000025D0
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000043E8 File Offset: 0x000025E8
		public int exams
		{
			get
			{
				return this.m_exams;
			}
			set
			{
				this.m_exams = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000043F4 File Offset: 0x000025F4
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000440C File Offset: 0x0000260C
		public double score
		{
			get
			{
				return this.m_score;
			}
			set
			{
				this.m_score = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004418 File Offset: 0x00002618
		[BindField(false)]
		public double avgscore
		{
			get
			{
				bool flag = this.exams > 0;
				double result;
				if (flag)
				{
					result = Convert.ToDouble((this.score / (double)this.exams).ToString("0.0"));
				}
				else
				{
					result = 0.0;
				}
				return result;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004464 File Offset: 0x00002664
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000447C File Offset: 0x0000267C
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004488 File Offset: 0x00002688
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000044A0 File Offset: 0x000026A0
		public int questions
		{
			get
			{
				return this.m_questions;
			}
			set
			{
				this.m_questions = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000044AC File Offset: 0x000026AC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000044C4 File Offset: 0x000026C4
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
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000044D0 File Offset: 0x000026D0
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000044E8 File Offset: 0x000026E8
		public int papers
		{
			get
			{
				return this.m_papers;
			}
			set
			{
				this.m_papers = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000044F4 File Offset: 0x000026F4
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000452E File Offset: 0x0000272E
		public string title
		{
			get
			{
				bool flag = this.m_title == "";
				if (flag)
				{
					this.m_title = this.name;
				}
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004538 File Offset: 0x00002738
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004550 File Offset: 0x00002750
		public string address
		{
			get
			{
				return this.m_address;
			}
			set
			{
				this.m_address = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000455C File Offset: 0x0000275C
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004574 File Offset: 0x00002774
		public string opentime
		{
			get
			{
				return this.m_opentime;
			}
			set
			{
				this.m_opentime = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004580 File Offset: 0x00002780
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004598 File Offset: 0x00002798
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000045A4 File Offset: 0x000027A4
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000045BC File Offset: 0x000027BC
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000045C8 File Offset: 0x000027C8
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000045E0 File Offset: 0x000027E0
		[CheckBox(true)]
		public int iscopy
		{
			get
			{
				return this.m_iscopy;
			}
			set
			{
				this.m_iscopy = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000045EC File Offset: 0x000027EC
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004604 File Offset: 0x00002804
		[CheckBox(true)]
		public int isvideo
		{
			get
			{
				return this.m_isvideo;
			}
			set
			{
				this.m_isvideo = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004610 File Offset: 0x00002810
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004628 File Offset: 0x00002828
		[CheckBox(true)]
		public int iswitch
		{
			get
			{
				return this.m_iswitch;
			}
			set
			{
				this.m_iswitch = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004634 File Offset: 0x00002834
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000464C File Offset: 0x0000284C
		[CheckBox(true)]
		public int issign
		{
			get
			{
				return this.m_issign;
			}
			set
			{
				this.m_issign = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004658 File Offset: 0x00002858
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004670 File Offset: 0x00002870
		[CheckBox(true)]
		public FPData client
		{
			get
			{
				return this.m_client;
			}
			set
			{
				this.m_client = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000467C File Offset: 0x0000287C
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00004694 File Offset: 0x00002894
		[CheckBox(true)]
		public int papertype
		{
			get
			{
				return this.m_papertype;
			}
			set
			{
				this.m_papertype = value;
			}
		}

		// Token: 0x04000017 RID: 23
		private int m_id;

		// Token: 0x04000018 RID: 24
		private int m_uid;

		// Token: 0x04000019 RID: 25
		private int m_channelid;

		// Token: 0x0400001A RID: 26
		private int m_sortid;

		// Token: 0x0400001B RID: 27
		private string m_sortname = string.Empty;

		// Token: 0x0400001C RID: 28
		private string m_typelist = string.Empty;

		// Token: 0x0400001D RID: 29
		private string m_name = string.Empty;

		// Token: 0x0400001E RID: 30
		private int m_type;

		// Token: 0x0400001F RID: 31
		private double m_total = 100.0;

		// Token: 0x04000020 RID: 32
		private double m_passmark = 60.0;

		// Token: 0x04000021 RID: 33
		private int m_examtime = 60;

		// Token: 0x04000022 RID: 34
		private int m_islimit;

		// Token: 0x04000023 RID: 35
		private DateTime m_starttime = DbUtils.GetDateTime();

		// Token: 0x04000024 RID: 36
		private DateTime m_endtime = DbUtils.GetDateTime();

		// Token: 0x04000025 RID: 37
		private int m_repeats;

		// Token: 0x04000026 RID: 38
		private int m_showanswer = 1;

		// Token: 0x04000027 RID: 39
		private int m_allowdelete;

		// Token: 0x04000028 RID: 40
		private int m_display = 0;

		// Token: 0x04000029 RID: 41
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400002A RID: 42
		private int m_examtype;

		// Token: 0x0400002B RID: 43
		private string m_examroles = string.Empty;

		// Token: 0x0400002C RID: 44
		private string m_examdeparts = string.Empty;

		// Token: 0x0400002D RID: 45
		private string m_examuser = string.Empty;

		// Token: 0x0400002E RID: 46
		private FPData m_flag = new FPData();

		// Token: 0x0400002F RID: 47
		private int m_credits;

		// Token: 0x04000030 RID: 48
		private int m_exams;

		// Token: 0x04000031 RID: 49
		private double m_score;

		// Token: 0x04000032 RID: 50
		private int m_views;

		// Token: 0x04000033 RID: 51
		private int m_questions = 40;

		// Token: 0x04000034 RID: 52
		private int m_status = 1;

		// Token: 0x04000035 RID: 53
		private int m_papers = 1;

		// Token: 0x04000036 RID: 54
		private string m_title = string.Empty;

		// Token: 0x04000037 RID: 55
		private string m_address = string.Empty;

		// Token: 0x04000038 RID: 56
		private string m_opentime = string.Empty;

		// Token: 0x04000039 RID: 57
		private string m_description = string.Empty;

		// Token: 0x0400003A RID: 58
		private string m_content = string.Empty;

		// Token: 0x0400003B RID: 59
		private int m_iscopy = 0;

		// Token: 0x0400003C RID: 60
		private int m_isvideo = 0;

		// Token: 0x0400003D RID: 61
		private int m_iswitch = 1;

		// Token: 0x0400003E RID: 62
		private int m_issign = 0;

		// Token: 0x0400003F RID: 63
		private FPData m_client = new FPData(new
		{
			pc = "1",
			mobile = "1"
		});

		// Token: 0x04000040 RID: 64
		private int m_papertype = 0;
	}
}
