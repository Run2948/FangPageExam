using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x02000038 RID: 56
	[ModelPrefix("Exam")]
	public class ExamLogInfo
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000178F0 File Offset: 0x00015AF0
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00017908 File Offset: 0x00015B08
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00017914 File Offset: 0x00015B14
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0001792C File Offset: 0x00015B2C
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00017938 File Offset: 0x00015B38
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00017950 File Offset: 0x00015B50
		[BindField(false)]
		public int subcounts
		{
			get
			{
				return this.m_subcounts;
			}
			set
			{
				this.m_subcounts = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0001795C File Offset: 0x00015B5C
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00017974 File Offset: 0x00015B74
		[BindField(false)]
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00017980 File Offset: 0x00015B80
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00017998 File Offset: 0x00015B98
		[BindField(false)]
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000179A4 File Offset: 0x00015BA4
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000179BC File Offset: 0x00015BBC
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000179C8 File Offset: 0x00015BC8
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000179E0 File Offset: 0x00015BE0
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000179EC File Offset: 0x00015BEC
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00017A04 File Offset: 0x00015C04
		public int answers
		{
			get
			{
				return this.m_answers;
			}
			set
			{
				this.m_answers = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00017A10 File Offset: 0x00015C10
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00017A28 File Offset: 0x00015C28
		public string qidlist
		{
			get
			{
				return this.m_qidlist;
			}
			set
			{
				this.m_qidlist = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00017A34 File Offset: 0x00015C34
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00017A4C File Offset: 0x00015C4C
		public string optionlist
		{
			get
			{
				return this.m_optionlist;
			}
			set
			{
				this.m_optionlist = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00017A58 File Offset: 0x00015C58
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00017A70 File Offset: 0x00015C70
		public string answerlist
		{
			get
			{
				return this.m_answerlist;
			}
			set
			{
				this.m_answerlist = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00017A7C File Offset: 0x00015C7C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00017A94 File Offset: 0x00015C94
		public string scorelist
		{
			get
			{
				return this.m_scorelist;
			}
			set
			{
				this.m_scorelist = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00017AA0 File Offset: 0x00015CA0
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00017AB8 File Offset: 0x00015CB8
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00017AC4 File Offset: 0x00015CC4
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00017ADC File Offset: 0x00015CDC
		public int curwrongs
		{
			get
			{
				return this.m_curwrongs;
			}
			set
			{
				this.m_curwrongs = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00017AE8 File Offset: 0x00015CE8
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00017B00 File Offset: 0x00015D00
		public string wronglist
		{
			get
			{
				return this.m_wronglist;
			}
			set
			{
				this.m_wronglist = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00017B0C File Offset: 0x00015D0C
		[BindField(false)]
		public string accuracy
		{
			get
			{
				string result;
				if (this.answers > 0)
				{
					result = ((double)(this.answers - this.wrongs) * 1.0 / (double)this.answers * 100.0).ToString("0");
				}
				else
				{
					result = "0";
				}
				return result;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00017B70 File Offset: 0x00015D70
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00017B88 File Offset: 0x00015D88
		public int notes
		{
			get
			{
				return this.m_notes;
			}
			set
			{
				this.m_notes = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00017B94 File Offset: 0x00015D94
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00017BAC File Offset: 0x00015DAC
		public int curnotes
		{
			get
			{
				return this.m_curnotes;
			}
			set
			{
				this.m_curnotes = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00017BB8 File Offset: 0x00015DB8
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00017BD0 File Offset: 0x00015DD0
		public string notelist
		{
			get
			{
				return this.m_notelist;
			}
			set
			{
				this.m_notelist = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00017BDC File Offset: 0x00015DDC
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00017BF4 File Offset: 0x00015DF4
		public int favs
		{
			get
			{
				return this.m_favs;
			}
			set
			{
				this.m_favs = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00017C00 File Offset: 0x00015E00
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00017C18 File Offset: 0x00015E18
		public int curfavs
		{
			get
			{
				return this.m_curfavs;
			}
			set
			{
				this.m_curfavs = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00017C24 File Offset: 0x00015E24
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00017C3C File Offset: 0x00015E3C
		public string favlist
		{
			get
			{
				return this.m_favlist;
			}
			set
			{
				this.m_favlist = value;
			}
		}

		// Token: 0x0400013B RID: 315
		private int m_id;

		// Token: 0x0400013C RID: 316
		private int m_sortid;

		// Token: 0x0400013D RID: 317
		private int m_subcounts;

		// Token: 0x0400013E RID: 318
		private string m_sortname;

		// Token: 0x0400013F RID: 319
		private int m_questions;

		// Token: 0x04000140 RID: 320
		private int m_channelid;

		// Token: 0x04000141 RID: 321
		private int m_uid;

		// Token: 0x04000142 RID: 322
		private int m_answers;

		// Token: 0x04000143 RID: 323
		private string m_qidlist = string.Empty;

		// Token: 0x04000144 RID: 324
		private string m_optionlist = string.Empty;

		// Token: 0x04000145 RID: 325
		private string m_answerlist = string.Empty;

		// Token: 0x04000146 RID: 326
		private string m_scorelist = string.Empty;

		// Token: 0x04000147 RID: 327
		private int m_wrongs;

		// Token: 0x04000148 RID: 328
		private int m_curwrongs;

		// Token: 0x04000149 RID: 329
		private string m_wronglist = string.Empty;

		// Token: 0x0400014A RID: 330
		private int m_notes;

		// Token: 0x0400014B RID: 331
		private int m_curnotes;

		// Token: 0x0400014C RID: 332
		private string m_notelist = string.Empty;

		// Token: 0x0400014D RID: 333
		private int m_favs;

		// Token: 0x0400014E RID: 334
		private int m_curfavs;

		// Token: 0x0400014F RID: 335
		private string m_favlist = string.Empty;
	}
}
