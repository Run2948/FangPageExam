using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Exam.Model
{
	// Token: 0x0200000D RID: 13
	[ModelPrefix("Exam")]
	public class ExamQuestion
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004C0C File Offset: 0x00002E0C
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004C24 File Offset: 0x00002E24
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004C30 File Offset: 0x00002E30
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004C48 File Offset: 0x00002E48
		public string kid
		{
			get
			{
				return this.m_kid;
			}
			set
			{
				this.m_kid = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004C54 File Offset: 0x00002E54
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004C6C File Offset: 0x00002E6C
		public string parentid
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004C78 File Offset: 0x00002E78
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004C90 File Offset: 0x00002E90
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004C9C File Offset: 0x00002E9C
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004CB4 File Offset: 0x00002EB4
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004CC0 File Offset: 0x00002EC0
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004CD8 File Offset: 0x00002ED8
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004CE4 File Offset: 0x00002EE4
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004CFC File Offset: 0x00002EFC
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004D08 File Offset: 0x00002F08
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004D20 File Offset: 0x00002F20
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004D2C File Offset: 0x00002F2C
		[BindField(false)]
		public string typename
		{
			get
			{
				string text = "";
				bool flag = this.typelist != "";
				if (flag)
				{
					List<TypeInfo> typeListById = TypeBll.GetTypeListById(this.typelist);
					foreach (TypeInfo typeInfo in typeListById)
					{
						text = FPArray.Push(text, typeInfo.name);
					}
				}
				return text;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004DB8 File Offset: 0x00002FB8
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public string type
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004DDC File Offset: 0x00002FDC
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00004DF4 File Offset: 0x00002FF4
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004E00 File Offset: 0x00003000
		[BindField(false)]
		public string typestr
		{
			get
			{
				string type = this.type;
				uint num = FPUtils.ComputeStringHash(type);
				if (num <= 1357055645u)
				{
					if (num != 926022516u)
					{
						if (num != 1278589744u)
						{
							if (num == 1357055645u)
							{
								if (type == "TYPE_RADIO")
								{
									return "单选题";
								}
							}
						}
						else if (type == "TYPE_ANSWER")
						{
							return "问答题";
						}
					}
					else if (type == "TYPE_BLANK")
					{
						return "填空题";
					}
				}
				else if (num <= 1895525581u)
				{
					if (num != 1399786254u)
					{
						if (num == 1895525581u)
						{
							if (type == "TYPE_COMPREHEND")
							{
								return "理解题";
							}
						}
					}
					else if (type == "TYPE_TRUE_FALSE")
					{
						return "判断题";
					}
				}
				else if (num != 2302693563u)
				{
					if (num == 3833053042u)
					{
						if (type == "TYPE_MULTIPLE")
						{
							return "多选题";
						}
					}
				}
				else if (type == "TYPE_OPERATION")
				{
					return "操作题";
				}
				return "";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004F34 File Offset: 0x00003134
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004F4C File Offset: 0x0000314C
		public string title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004F58 File Offset: 0x00003158
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004F70 File Offset: 0x00003170
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004F7C File Offset: 0x0000317C
		[BindField(false)]
		public string[] option
		{
			get
			{
				return FPArray.SplitString(this.content, "§", this.ascount);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004FA4 File Offset: 0x000031A4
		[BindField(false)]
		public string[] option2
		{
			get
			{
				return FPArray.SplitString(this.content, "§", 8);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004FC8 File Offset: 0x000031C8
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004FE0 File Offset: 0x000031E0
		public string answer
		{
			get
			{
				return this.m_answer;
			}
			set
			{
				this.m_answer = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004FEC File Offset: 0x000031EC
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00005004 File Offset: 0x00003204
		[CheckBox(true)]
		public int upperflg
		{
			get
			{
				return this.m_upperflg;
			}
			set
			{
				this.m_upperflg = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005010 File Offset: 0x00003210
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005028 File Offset: 0x00003228
		[CheckBox(true)]
		public int orderflg
		{
			get
			{
				return this.m_orderflg;
			}
			set
			{
				this.m_orderflg = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005034 File Offset: 0x00003234
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000504C File Offset: 0x0000324C
		public string answerkey
		{
			get
			{
				return this.m_answerkey;
			}
			set
			{
				this.m_answerkey = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005058 File Offset: 0x00003258
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005070 File Offset: 0x00003270
		public int ascount
		{
			get
			{
				return this.m_ascount;
			}
			set
			{
				this.m_ascount = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000507C File Offset: 0x0000327C
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00005094 File Offset: 0x00003294
		public string explain
		{
			get
			{
				return this.m_explain;
			}
			set
			{
				this.m_explain = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000050A0 File Offset: 0x000032A0
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000050B8 File Offset: 0x000032B8
		public int difficulty
		{
			get
			{
				return this.m_difficulty;
			}
			set
			{
				this.m_difficulty = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000050C4 File Offset: 0x000032C4
		[BindField(false)]
		public string diffstr
		{
			get
			{
				string result;
				switch (this.difficulty)
				{
				case 0:
					result = "易";
					break;
				case 1:
					result = "较易";
					break;
				case 2:
					result = "一般";
					break;
				case 3:
					result = "较难";
					break;
				case 4:
					result = "难";
					break;
				default:
					result = "";
					break;
				}
				return result;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005128 File Offset: 0x00003328
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005140 File Offset: 0x00003340
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000514C File Offset: 0x0000334C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005164 File Offset: 0x00003364
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005170 File Offset: 0x00003370
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005188 File Offset: 0x00003388
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005194 File Offset: 0x00003394
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000051AC File Offset: 0x000033AC
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000051B8 File Offset: 0x000033B8
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000051D0 File Offset: 0x000033D0
		[CheckBox(true)]
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000051DC File Offset: 0x000033DC
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000051F4 File Offset: 0x000033F4
		[BindField(false)]
		public string useranswer
		{
			get
			{
				return this.m_useranswer;
			}
			set
			{
				this.m_useranswer = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005200 File Offset: 0x00003400
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000521E File Offset: 0x0000341E
		[BindField(false)]
		public double userscore
		{
			get
			{
				return Math.Round(this.m_userscore, 1);
			}
			set
			{
				this.m_userscore = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005228 File Offset: 0x00003428
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005240 File Offset: 0x00003440
		[BindField(false)]
		public int ismarker
		{
			get
			{
				return this.m_ismarker;
			}
			set
			{
				this.m_ismarker = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000524C File Offset: 0x0000344C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00005264 File Offset: 0x00003464
		[BindField(false)]
		public int isfav
		{
			get
			{
				return this.m_isfav;
			}
			set
			{
				this.m_isfav = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005270 File Offset: 0x00003470
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005288 File Offset: 0x00003488
		[BindField(false)]
		public string note
		{
			get
			{
				return this.m_note;
			}
			set
			{
				this.m_note = value;
			}
		}

		// Token: 0x04000059 RID: 89
		private int m_id;

		// Token: 0x0400005A RID: 90
		private string m_kid = string.Empty;

		// Token: 0x0400005B RID: 91
		private string m_parentid = string.Empty;

		// Token: 0x0400005C RID: 92
		private int m_uid;

		// Token: 0x0400005D RID: 93
		private int m_channelid;

		// Token: 0x0400005E RID: 94
		private int m_sortid;

		// Token: 0x0400005F RID: 95
		private string m_sortname = string.Empty;

		// Token: 0x04000060 RID: 96
		private string m_typelist = string.Empty;

		// Token: 0x04000061 RID: 97
		private string m_type;

		// Token: 0x04000062 RID: 98
		private int m_display;

		// Token: 0x04000063 RID: 99
		private string m_title = string.Empty;

		// Token: 0x04000064 RID: 100
		private string m_content = string.Empty;

		// Token: 0x04000065 RID: 101
		private string m_answer = string.Empty;

		// Token: 0x04000066 RID: 102
		private int m_upperflg;

		// Token: 0x04000067 RID: 103
		private int m_orderflg;

		// Token: 0x04000068 RID: 104
		private string m_answerkey = string.Empty;

		// Token: 0x04000069 RID: 105
		private int m_ascount;

		// Token: 0x0400006A RID: 106
		private string m_explain = string.Empty;

		// Token: 0x0400006B RID: 107
		private int m_difficulty = 2;

		// Token: 0x0400006C RID: 108
		private DateTime m_postdatetime = DbUtils.GetDateTime();

		// Token: 0x0400006D RID: 109
		private string m_attachid = string.Empty;

		// Token: 0x0400006E RID: 110
		private int m_exams;

		// Token: 0x0400006F RID: 111
		private int m_wrongs;

		// Token: 0x04000070 RID: 112
		private int m_status = 1;

		// Token: 0x04000071 RID: 113
		private string m_useranswer = string.Empty;

		// Token: 0x04000072 RID: 114
		private double m_userscore;

		// Token: 0x04000073 RID: 115
		private int m_ismarker;

		// Token: 0x04000074 RID: 116
		private int m_isfav;

		// Token: 0x04000075 RID: 117
		private string m_note = string.Empty;
	}
}
