using System;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.Exam.Model
{
	// Token: 0x02000037 RID: 55
	[ModelPrefix("Exam")]
	public class ExamInfo
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00017380 File Offset: 0x00015580
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00017398 File Offset: 0x00015598
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
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000173A4 File Offset: 0x000155A4
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000173BC File Offset: 0x000155BC
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000173C8 File Offset: 0x000155C8
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000173E0 File Offset: 0x000155E0
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000173EC File Offset: 0x000155EC
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00017404 File Offset: 0x00015604
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00017410 File Offset: 0x00015610
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0001744C File Offset: 0x0001564C
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00017464 File Offset: 0x00015664
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00017470 File Offset: 0x00015670
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00017488 File Offset: 0x00015688
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00017494 File Offset: 0x00015694
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000174AC File Offset: 0x000156AC
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000174B8 File Offset: 0x000156B8
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000174D0 File Offset: 0x000156D0
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000174DC File Offset: 0x000156DC
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000174F4 File Offset: 0x000156F4
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00017500 File Offset: 0x00015700
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00017518 File Offset: 0x00015718
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00017524 File Offset: 0x00015724
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0001753C File Offset: 0x0001573C
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00017548 File Offset: 0x00015748
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00017560 File Offset: 0x00015760
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0001756C File Offset: 0x0001576C
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00017584 File Offset: 0x00015784
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00017590 File Offset: 0x00015790
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000175A8 File Offset: 0x000157A8
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000175B4 File Offset: 0x000157B4
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000175CC File Offset: 0x000157CC
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000175D8 File Offset: 0x000157D8
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000175F0 File Offset: 0x000157F0
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000175FC File Offset: 0x000157FC
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00017614 File Offset: 0x00015814
		public int optiondisplay
		{
			get
			{
				return this.m_optiondisplay;
			}
			set
			{
				this.m_optiondisplay = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00017620 File Offset: 0x00015820
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00017638 File Offset: 0x00015838
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
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00017644 File Offset: 0x00015844
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0001765C File Offset: 0x0001585C
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00017668 File Offset: 0x00015868
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00017680 File Offset: 0x00015880
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0001768C File Offset: 0x0001588C
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000176A4 File Offset: 0x000158A4
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000176B0 File Offset: 0x000158B0
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000176C8 File Offset: 0x000158C8
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000176D4 File Offset: 0x000158D4
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000176EC File Offset: 0x000158EC
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000176F8 File Offset: 0x000158F8
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00017710 File Offset: 0x00015910
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0001771C File Offset: 0x0001591C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00017734 File Offset: 0x00015934
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00017740 File Offset: 0x00015940
		[BindField(false)]
		public double avgscore
		{
			get
			{
				double result;
				if (this.exams > 0)
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00017790 File Offset: 0x00015990
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000177A8 File Offset: 0x000159A8
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000177B4 File Offset: 0x000159B4
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000177CC File Offset: 0x000159CC
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000177D8 File Offset: 0x000159D8
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000177F0 File Offset: 0x000159F0
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000177FC File Offset: 0x000159FC
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00017814 File Offset: 0x00015A14
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00017820 File Offset: 0x00015A20
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00017838 File Offset: 0x00015A38
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00017844 File Offset: 0x00015A44
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0001785C File Offset: 0x00015A5C
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00017868 File Offset: 0x00015A68
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00017880 File Offset: 0x00015A80
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

		// Token: 0x0400011A RID: 282
		private int m_id;

		// Token: 0x0400011B RID: 283
		private int m_uid;

		// Token: 0x0400011C RID: 284
		private int m_channelid;

		// Token: 0x0400011D RID: 285
		private int m_sortid;

		// Token: 0x0400011E RID: 286
		private SortInfo m_sortinfo;

		// Token: 0x0400011F RID: 287
		private string m_typelist = string.Empty;

		// Token: 0x04000120 RID: 288
		private string m_name = string.Empty;

		// Token: 0x04000121 RID: 289
		private double m_total = 100.0;

		// Token: 0x04000122 RID: 290
		private double m_passmark = 60.0;

		// Token: 0x04000123 RID: 291
		private int m_examtime = 60;

		// Token: 0x04000124 RID: 292
		private int m_islimit;

		// Token: 0x04000125 RID: 293
		private DateTime m_starttime = DbUtils.GetDateTime();

		// Token: 0x04000126 RID: 294
		private DateTime m_endtime = DbUtils.GetDateTime();

		// Token: 0x04000127 RID: 295
		private int m_repeats;

		// Token: 0x04000128 RID: 296
		private int m_showanswer = 1;

		// Token: 0x04000129 RID: 297
		private int m_allowdelete;

		// Token: 0x0400012A RID: 298
		private int m_display = 0;

		// Token: 0x0400012B RID: 299
		private int m_optiondisplay = 1;

		// Token: 0x0400012C RID: 300
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400012D RID: 301
		private int m_examtype;

		// Token: 0x0400012E RID: 302
		private string m_examroles = string.Empty;

		// Token: 0x0400012F RID: 303
		private string m_examdeparts = string.Empty;

		// Token: 0x04000130 RID: 304
		private string m_examuser = string.Empty;

		// Token: 0x04000131 RID: 305
		private int m_credits;

		// Token: 0x04000132 RID: 306
		private int m_exams;

		// Token: 0x04000133 RID: 307
		private double m_score;

		// Token: 0x04000134 RID: 308
		private int m_views;

		// Token: 0x04000135 RID: 309
		private int m_questions = 40;

		// Token: 0x04000136 RID: 310
		private int m_status = 1;

		// Token: 0x04000137 RID: 311
		private int m_papers = 1;

		// Token: 0x04000138 RID: 312
		private string m_description = string.Empty;

		// Token: 0x04000139 RID: 313
		private string m_content = string.Empty;

		// Token: 0x0400013A RID: 314
		private int m_iscopy = 0;
	}
}
