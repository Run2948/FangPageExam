using System;
using FangPage.Data;

namespace FP_Exam.Model
{
	// Token: 0x0200000B RID: 11
	[ModelPrefix("Exam")]
	public class ExamLogInfo
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000047E0 File Offset: 0x000029E0
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000047F8 File Offset: 0x000029F8
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004804 File Offset: 0x00002A04
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000481C File Offset: 0x00002A1C
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004828 File Offset: 0x00002A28
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004840 File Offset: 0x00002A40
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000484C File Offset: 0x00002A4C
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004864 File Offset: 0x00002A64
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004870 File Offset: 0x00002A70
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004888 File Offset: 0x00002A88
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004894 File Offset: 0x00002A94
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000048AC File Offset: 0x00002AAC
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000048B8 File Offset: 0x00002AB8
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000048D0 File Offset: 0x00002AD0
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000048DC File Offset: 0x00002ADC
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000048F4 File Offset: 0x00002AF4
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004900 File Offset: 0x00002B00
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004918 File Offset: 0x00002B18
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004924 File Offset: 0x00002B24
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000493C File Offset: 0x00002B3C
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004948 File Offset: 0x00002B48
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004960 File Offset: 0x00002B60
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000496C File Offset: 0x00002B6C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004984 File Offset: 0x00002B84
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004990 File Offset: 0x00002B90
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000049A8 File Offset: 0x00002BA8
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000049B4 File Offset: 0x00002BB4
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000049CC File Offset: 0x00002BCC
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000049D8 File Offset: 0x00002BD8
		[BindField(false)]
		public string accuracy
		{
			get
			{
				bool flag = this.answers > 0;
				string result;
				if (flag)
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004A38 File Offset: 0x00002C38
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004A50 File Offset: 0x00002C50
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004A5C File Offset: 0x00002C5C
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004A74 File Offset: 0x00002C74
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004A80 File Offset: 0x00002C80
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00004A98 File Offset: 0x00002C98
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004AA4 File Offset: 0x00002CA4
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004ABC File Offset: 0x00002CBC
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00004AC8 File Offset: 0x00002CC8
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00004AE0 File Offset: 0x00002CE0
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004AEC File Offset: 0x00002CEC
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004B04 File Offset: 0x00002D04
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

		// Token: 0x04000041 RID: 65
		private int m_id;

		// Token: 0x04000042 RID: 66
		private int m_sortid;

		// Token: 0x04000043 RID: 67
		private int m_subcounts;

		// Token: 0x04000044 RID: 68
		private string m_sortname;

		// Token: 0x04000045 RID: 69
		private int m_questions;

		// Token: 0x04000046 RID: 70
		private int m_channelid;

		// Token: 0x04000047 RID: 71
		private int m_uid;

		// Token: 0x04000048 RID: 72
		private int m_answers;

		// Token: 0x04000049 RID: 73
		private string m_qidlist = string.Empty;

		// Token: 0x0400004A RID: 74
		private string m_answerlist = string.Empty;

		// Token: 0x0400004B RID: 75
		private string m_scorelist = string.Empty;

		// Token: 0x0400004C RID: 76
		private int m_wrongs;

		// Token: 0x0400004D RID: 77
		private int m_curwrongs;

		// Token: 0x0400004E RID: 78
		private string m_wronglist = string.Empty;

		// Token: 0x0400004F RID: 79
		private int m_notes;

		// Token: 0x04000050 RID: 80
		private int m_curnotes;

		// Token: 0x04000051 RID: 81
		private string m_notelist = string.Empty;

		// Token: 0x04000052 RID: 82
		private int m_favs;

		// Token: 0x04000053 RID: 83
		private int m_curfavs;

		// Token: 0x04000054 RID: 84
		private string m_favlist = string.Empty;
	}
}
