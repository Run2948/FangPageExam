using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200000E RID: 14
	[ModelPrefix("Exam")]
	public class ExamResult
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005348 File Offset: 0x00003548
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005360 File Offset: 0x00003560
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000536C File Offset: 0x0000356C
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005384 File Offset: 0x00003584
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

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005390 File Offset: 0x00003590
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000053A8 File Offset: 0x000035A8
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000053B4 File Offset: 0x000035B4
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000053CC File Offset: 0x000035CC
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000053D8 File Offset: 0x000035D8
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000053F0 File Offset: 0x000035F0
		[Map("WMS_UserInfo", "nickname")]
		public string nickname
		{
			get
			{
				return this.m_nickname;
			}
			set
			{
				this.m_nickname = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000053FC File Offset: 0x000035FC
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005414 File Offset: 0x00003614
		[Map("WMS_UserInfo", "departname")]
		public string departname
		{
			get
			{
				return this.m_departname;
			}
			set
			{
				this.m_departname = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005420 File Offset: 0x00003620
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00005438 File Offset: 0x00003638
		[Map("WMS_UserInfo", "idcard")]
		public string idcard
		{
			get
			{
				return this.m_idcard;
			}
			set
			{
				this.m_idcard = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005444 File Offset: 0x00003644
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000545C File Offset: 0x0000365C
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005468 File Offset: 0x00003668
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005480 File Offset: 0x00003680
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000548C File Offset: 0x0000368C
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000054A4 File Offset: 0x000036A4
		public int examid
		{
			get
			{
				return this.m_examid;
			}
			set
			{
				this.m_examid = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000054B0 File Offset: 0x000036B0
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000054C8 File Offset: 0x000036C8
		public string examname
		{
			get
			{
				return this.m_examname;
			}
			set
			{
				this.m_examname = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000054D4 File Offset: 0x000036D4
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000054EC File Offset: 0x000036EC
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000054F8 File Offset: 0x000036F8
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00005510 File Offset: 0x00003710
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000551C File Offset: 0x0000371C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005534 File Offset: 0x00003734
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005540 File Offset: 0x00003740
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005558 File Offset: 0x00003758
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005564 File Offset: 0x00003764
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000557C File Offset: 0x0000377C
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005588 File Offset: 0x00003788
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000055A0 File Offset: 0x000037A0
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000055AC File Offset: 0x000037AC
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000055CA File Offset: 0x000037CA
		public double score1
		{
			get
			{
				return Math.Round(this.m_score1, 2);
			}
			set
			{
				this.m_score1 = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000055D4 File Offset: 0x000037D4
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000055F2 File Offset: 0x000037F2
		public double score2
		{
			get
			{
				return Math.Round(this.m_score2, 2);
			}
			set
			{
				this.m_score2 = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000055FC File Offset: 0x000037FC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000561A File Offset: 0x0000381A
		public double score
		{
			get
			{
				return Math.Round(this.m_score, 2);
			}
			set
			{
				this.m_score = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005624 File Offset: 0x00003824
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000563C File Offset: 0x0000383C
		public int utime
		{
			get
			{
				return this.m_utime;
			}
			set
			{
				this.m_utime = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005648 File Offset: 0x00003848
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005660 File Offset: 0x00003860
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000566C File Offset: 0x0000386C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005684 File Offset: 0x00003884
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005690 File Offset: 0x00003890
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000056A8 File Offset: 0x000038A8
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000056B4 File Offset: 0x000038B4
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000056CC File Offset: 0x000038CC
		public DateTime examdatetime
		{
			get
			{
				return this.m_examdatetime;
			}
			set
			{
				this.m_examdatetime = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000056D8 File Offset: 0x000038D8
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000056F0 File Offset: 0x000038F0
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000056FC File Offset: 0x000038FC
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005714 File Offset: 0x00003914
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005720 File Offset: 0x00003920
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005738 File Offset: 0x00003938
		public int wrongs
		{
			get
			{
				return this.m_wrongs;
			}
			set
			{
				this.m_wrongs = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005744 File Offset: 0x00003944
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000575C File Offset: 0x0000395C
		public int unanswer
		{
			get
			{
				return this.m_unanswer;
			}
			set
			{
				this.m_unanswer = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005768 File Offset: 0x00003968
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005780 File Offset: 0x00003980
		public int exp
		{
			get
			{
				return this.m_exp;
			}
			set
			{
				this.m_exp = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000578C File Offset: 0x0000398C
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000057A4 File Offset: 0x000039A4
		public int getcredits
		{
			get
			{
				return this.m_getcredits;
			}
			set
			{
				this.m_getcredits = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000057B0 File Offset: 0x000039B0
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000057C8 File Offset: 0x000039C8
		public string exnote
		{
			get
			{
				return this.m_exnote;
			}
			set
			{
				this.m_exnote = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000057D4 File Offset: 0x000039D4
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000057EC File Offset: 0x000039EC
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000057F8 File Offset: 0x000039F8
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005810 File Offset: 0x00003A10
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000581C File Offset: 0x00003A1C
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005834 File Offset: 0x00003A34
		public int paper
		{
			get
			{
				return this.m_paper;
			}
			set
			{
				this.m_paper = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005840 File Offset: 0x00003A40
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005858 File Offset: 0x00003A58
		public string ip
		{
			get
			{
				return this.m_ip;
			}
			set
			{
				this.m_ip = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005864 File Offset: 0x00003A64
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000587C File Offset: 0x00003A7C
		public string mac
		{
			get
			{
				return this.m_mac;
			}
			set
			{
				this.m_mac = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005888 File Offset: 0x00003A88
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000058A0 File Offset: 0x00003AA0
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

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000058AC File Offset: 0x00003AAC
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000058C4 File Offset: 0x00003AC4
		public string client
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000058D0 File Offset: 0x00003AD0
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000058E8 File Offset: 0x00003AE8
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000058F4 File Offset: 0x00003AF4
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000592E File Offset: 0x00003B2E
		public string title
		{
			get
			{
				bool flag = this.m_title == "";
				if (flag)
				{
					this.m_title = this.examname;
				}
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005938 File Offset: 0x00003B38
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00005950 File Offset: 0x00003B50
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

		// Token: 0x04000076 RID: 118
		private int m_id;

		// Token: 0x04000077 RID: 119
		private int m_uid;

		// Token: 0x04000078 RID: 120
		private string m_username = string.Empty;

		// Token: 0x04000079 RID: 121
		private string m_realname = string.Empty;

		// Token: 0x0400007A RID: 122
		private string m_nickname = string.Empty;

		// Token: 0x0400007B RID: 123
		private string m_departname = string.Empty;

		// Token: 0x0400007C RID: 124
		private string m_idcard = string.Empty;

		// Token: 0x0400007D RID: 125
		private int m_channelid;

		// Token: 0x0400007E RID: 126
		private int m_sortid;

		// Token: 0x0400007F RID: 127
		private int m_examid;

		// Token: 0x04000080 RID: 128
		private string m_examname = string.Empty;

		// Token: 0x04000081 RID: 129
		private int m_examtime;

		// Token: 0x04000082 RID: 130
		private int m_examtype;

		// Token: 0x04000083 RID: 131
		private int m_showanswer = 1;

		// Token: 0x04000084 RID: 132
		private int m_allowdelete = 1;

		// Token: 0x04000085 RID: 133
		private double m_total;

		// Token: 0x04000086 RID: 134
		private double m_passmark;

		// Token: 0x04000087 RID: 135
		private double m_score1;

		// Token: 0x04000088 RID: 136
		private double m_score2;

		// Token: 0x04000089 RID: 137
		private double m_score;

		// Token: 0x0400008A RID: 138
		private int m_utime;

		// Token: 0x0400008B RID: 139
		private int m_islimit;

		// Token: 0x0400008C RID: 140
		private DateTime m_starttime = DbUtils.GetDateTime();

		// Token: 0x0400008D RID: 141
		private DateTime m_endtime = DbUtils.GetDateTime();

		// Token: 0x0400008E RID: 142
		private DateTime m_examdatetime = DbUtils.GetDateTime();

		// Token: 0x0400008F RID: 143
		private int m_credits;

		// Token: 0x04000090 RID: 144
		private int m_questions;

		// Token: 0x04000091 RID: 145
		private int m_wrongs;

		// Token: 0x04000092 RID: 146
		private int m_unanswer;

		// Token: 0x04000093 RID: 147
		private int m_exp;

		// Token: 0x04000094 RID: 148
		private int m_getcredits;

		// Token: 0x04000095 RID: 149
		private string m_exnote = string.Empty;

		// Token: 0x04000096 RID: 150
		private string m_attachid = string.Empty;

		// Token: 0x04000097 RID: 151
		private int m_status;

		// Token: 0x04000098 RID: 152
		private int m_paper = 1;

		// Token: 0x04000099 RID: 153
		private string m_ip = string.Empty;

		// Token: 0x0400009A RID: 154
		private string m_mac = string.Empty;

		// Token: 0x0400009B RID: 155
		private int m_isvideo = 0;

		// Token: 0x0400009C RID: 156
		private string m_client = string.Empty;

		// Token: 0x0400009D RID: 157
		private int m_papertype = 0;

		// Token: 0x0400009E RID: 158
		private string m_title = string.Empty;

		// Token: 0x0400009F RID: 159
		private string m_address = string.Empty;
	}
}
