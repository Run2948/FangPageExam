using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Model
{
	// Token: 0x0200000D RID: 13
	[ModelPrefix("WMS")]
	public class Department
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000449E File Offset: 0x0000269E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000044A6 File Offset: 0x000026A6
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000044AF File Offset: 0x000026AF
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000044B7 File Offset: 0x000026B7
		public string keyid
		{
			get
			{
				return this.m_keyid;
			}
			set
			{
				this.m_keyid = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000044C0 File Offset: 0x000026C0
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000044C8 File Offset: 0x000026C8
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000044D1 File Offset: 0x000026D1
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000044D9 File Offset: 0x000026D9
		public string parentlist
		{
			get
			{
				return this.m_parentlist;
			}
			set
			{
				this.m_parentlist = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000044E2 File Offset: 0x000026E2
		[BindField(false)]
		public int layer
		{
			get
			{
				if (this.parentlist != "")
				{
					this.m_layer = FPArray.SplitInt(this.parentlist).Length - 1;
				}
				return this.m_layer;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004511 File Offset: 0x00002711
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00004519 File Offset: 0x00002719
		public string departlist
		{
			get
			{
				return this.m_departlist;
			}
			set
			{
				this.m_departlist = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004522 File Offset: 0x00002722
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000452A File Offset: 0x0000272A
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004534 File Offset: 0x00002734
		// (set) Token: 0x0600009B RID: 155 RVA: 0x000045D2 File Offset: 0x000027D2
		public string longname
		{
			get
			{
				if (this.m_longname == "")
				{
					foreach (string text in FPArray.SplitString(this.departlist, ">"))
					{
						if (text.StartsWith(this.m_longname))
						{
							this.m_longname += text.Substring(this.m_longname.Length, text.Length - this.m_longname.Length);
						}
						else
						{
							this.m_longname += text;
						}
					}
				}
				return this.m_longname;
			}
			set
			{
				this.m_longname = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000045DB File Offset: 0x000027DB
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000045E3 File Offset: 0x000027E3
		public string shortname
		{
			get
			{
				return this.m_shortname;
			}
			set
			{
				this.m_shortname = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000045EC File Offset: 0x000027EC
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000045F4 File Offset: 0x000027F4
		public string markup
		{
			get
			{
				return this.m_markup;
			}
			set
			{
				this.m_markup = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000045FD File Offset: 0x000027FD
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004605 File Offset: 0x00002805
		public string types
		{
			get
			{
				return this.m_types;
			}
			set
			{
				this.m_types = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004610 File Offset: 0x00002810
		[BindField(false)]
		public string depttype
		{
			get
			{
				string text = "";
				if (this.types != "")
				{
					foreach (TypeInfo typeInfo in TypeBll.GetTypeListById(this.types))
					{
						text = FPArray.Push(text, typeInfo.name);
					}
				}
				return text;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004688 File Offset: 0x00002888
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004690 File Offset: 0x00002890
		public string manager
		{
			get
			{
				return this.m_manager;
			}
			set
			{
				this.m_manager = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004699 File Offset: 0x00002899
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000046A1 File Offset: 0x000028A1
		public string departer
		{
			get
			{
				return this.m_departer;
			}
			set
			{
				this.m_departer = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000046AA File Offset: 0x000028AA
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000046B2 File Offset: 0x000028B2
		public int usercount
		{
			get
			{
				return this.m_usercount;
			}
			set
			{
				this.m_usercount = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000046BB File Offset: 0x000028BB
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000046C3 File Offset: 0x000028C3
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000046CC File Offset: 0x000028CC
		// (set) Token: 0x060000AC RID: 172 RVA: 0x000046D4 File Offset: 0x000028D4
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000046DD File Offset: 0x000028DD
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000046E5 File Offset: 0x000028E5
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000046EE File Offset: 0x000028EE
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000046F6 File Offset: 0x000028F6
		public int posts
		{
			get
			{
				return this.m_posts;
			}
			set
			{
				this.m_posts = value;
			}
		}

		// Token: 0x04000061 RID: 97
		private int m_id;

		// Token: 0x04000062 RID: 98
		private string m_keyid = string.Empty;

		// Token: 0x04000063 RID: 99
		private int m_parentid;

		// Token: 0x04000064 RID: 100
		private string m_parentlist = string.Empty;

		// Token: 0x04000065 RID: 101
		private int m_layer;

		// Token: 0x04000066 RID: 102
		private string m_departlist = string.Empty;

		// Token: 0x04000067 RID: 103
		private string m_name = string.Empty;

		// Token: 0x04000068 RID: 104
		private string m_longname = string.Empty;

		// Token: 0x04000069 RID: 105
		private string m_shortname = string.Empty;

		// Token: 0x0400006A RID: 106
		private string m_markup = string.Empty;

		// Token: 0x0400006B RID: 107
		private string m_types = string.Empty;

		// Token: 0x0400006C RID: 108
		private string m_manager = string.Empty;

		// Token: 0x0400006D RID: 109
		private string m_departer = string.Empty;

		// Token: 0x0400006E RID: 110
		private int m_usercount;

		// Token: 0x0400006F RID: 111
		private string m_description = string.Empty;

		// Token: 0x04000070 RID: 112
		private int m_display;

		// Token: 0x04000071 RID: 113
		private int m_subcounts;

		// Token: 0x04000072 RID: 114
		private int m_posts;
	}
}
