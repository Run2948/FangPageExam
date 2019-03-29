using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000029 RID: 41
	[BindTable("UserInfo")]
	[ModelPrefix("WMS")]
	public class FullUserInfo : UserInfo
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000076B4 File Offset: 0x000058B4
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000076CC File Offset: 0x000058CC
		public string bday
		{
			get
			{
				return this.m_bday;
			}
			set
			{
				this.m_bday = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000076D8 File Offset: 0x000058D8
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000076F0 File Offset: 0x000058F0
		public string bloodtype
		{
			get
			{
				return this.m_bloodtype;
			}
			set
			{
				this.m_bloodtype = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000076FC File Offset: 0x000058FC
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00007714 File Offset: 0x00005914
		public string height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007720 File Offset: 0x00005920
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00007738 File Offset: 0x00005938
		public string weight
		{
			get
			{
				return this.m_weight;
			}
			set
			{
				this.m_weight = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00007744 File Offset: 0x00005944
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000775C File Offset: 0x0000595C
		public int married
		{
			get
			{
				return this.m_married;
			}
			set
			{
				this.m_married = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00007768 File Offset: 0x00005968
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00007780 File Offset: 0x00005980
		public string education
		{
			get
			{
				return this.m_education;
			}
			set
			{
				this.m_education = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000778C File Offset: 0x0000598C
		// (set) Token: 0x0600021E RID: 542 RVA: 0x000077A4 File Offset: 0x000059A4
		public string school
		{
			get
			{
				return this.m_school;
			}
			set
			{
				this.m_school = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000077B0 File Offset: 0x000059B0
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000077C8 File Offset: 0x000059C8
		public string job
		{
			get
			{
				return this.m_job;
			}
			set
			{
				this.m_job = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000077D4 File Offset: 0x000059D4
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000077EC File Offset: 0x000059EC
		public string position
		{
			get
			{
				return this.m_position;
			}
			set
			{
				this.m_position = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000077F8 File Offset: 0x000059F8
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00007810 File Offset: 0x00005A10
		public string politics
		{
			get
			{
				return this.m_politics;
			}
			set
			{
				this.m_politics = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000781C File Offset: 0x00005A1C
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00007834 File Offset: 0x00005A34
		public string company
		{
			get
			{
				return this.m_company;
			}
			set
			{
				this.m_company = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00007840 File Offset: 0x00005A40
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00007858 File Offset: 0x00005A58
		public string nation
		{
			get
			{
				return this.m_nation;
			}
			set
			{
				this.m_nation = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000787C File Offset: 0x00005A7C
		public string phone
		{
			get
			{
				return this.m_phone;
			}
			set
			{
				this.m_phone = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007888 File Offset: 0x00005A88
		// (set) Token: 0x0600022C RID: 556 RVA: 0x000078A0 File Offset: 0x00005AA0
		public string qq
		{
			get
			{
				return this.m_qq;
			}
			set
			{
				this.m_qq = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000078AC File Offset: 0x00005AAC
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000078C4 File Offset: 0x00005AC4
		public string blog
		{
			get
			{
				return this.m_blog;
			}
			set
			{
				this.m_blog = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000078D0 File Offset: 0x00005AD0
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000078E8 File Offset: 0x00005AE8
		public string province
		{
			get
			{
				return this.m_province;
			}
			set
			{
				this.m_province = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000078F4 File Offset: 0x00005AF4
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000790C File Offset: 0x00005B0C
		public string city
		{
			get
			{
				return this.m_city;
			}
			set
			{
				this.m_city = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00007918 File Offset: 0x00005B18
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00007930 File Offset: 0x00005B30
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000793C File Offset: 0x00005B3C
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00007954 File Offset: 0x00005B54
		public string zipcode
		{
			get
			{
				return this.m_zipcode;
			}
			set
			{
				this.m_zipcode = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00007960 File Offset: 0x00005B60
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00007978 File Offset: 0x00005B78
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00007984 File Offset: 0x00005B84
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000799C File Offset: 0x00005B9C
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000079A8 File Offset: 0x00005BA8
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000079C0 File Offset: 0x00005BC0
		public int isidcard
		{
			get
			{
				return this.m_isidcard;
			}
			set
			{
				this.m_isidcard = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000079CC File Offset: 0x00005BCC
		// (set) Token: 0x0600023E RID: 574 RVA: 0x000079E4 File Offset: 0x00005BE4
		public string idcardface
		{
			get
			{
				return this.m_idcardface;
			}
			set
			{
				this.m_idcardface = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000079F0 File Offset: 0x00005BF0
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007A08 File Offset: 0x00005C08
		public string idcardback
		{
			get
			{
				return this.m_idcardback;
			}
			set
			{
				this.m_idcardback = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007A14 File Offset: 0x00005C14
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007A2C File Offset: 0x00005C2C
		public string idcardper
		{
			get
			{
				return this.m_idcardper;
			}
			set
			{
				this.m_idcardper = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007A38 File Offset: 0x00005C38
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007A50 File Offset: 0x00005C50
		public DateTime idcarvalidity
		{
			get
			{
				return this.m_idcarvalidity;
			}
			set
			{
				this.m_idcarvalidity = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007A5C File Offset: 0x00005C5C
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00007A74 File Offset: 0x00005C74
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

		// Token: 0x04000107 RID: 263
		private string m_bday = string.Empty;

		// Token: 0x04000108 RID: 264
		private string m_bloodtype = string.Empty;

		// Token: 0x04000109 RID: 265
		private string m_height = string.Empty;

		// Token: 0x0400010A RID: 266
		private string m_weight = string.Empty;

		// Token: 0x0400010B RID: 267
		private int m_married;

		// Token: 0x0400010C RID: 268
		private string m_education = string.Empty;

		// Token: 0x0400010D RID: 269
		private string m_school = string.Empty;

		// Token: 0x0400010E RID: 270
		private string m_job = string.Empty;

		// Token: 0x0400010F RID: 271
		private string m_position = string.Empty;

		// Token: 0x04000110 RID: 272
		private string m_politics = string.Empty;

		// Token: 0x04000111 RID: 273
		private string m_company = string.Empty;

		// Token: 0x04000112 RID: 274
		private string m_nation = string.Empty;

		// Token: 0x04000113 RID: 275
		private string m_phone = string.Empty;

		// Token: 0x04000114 RID: 276
		private string m_qq = string.Empty;

		// Token: 0x04000115 RID: 277
		private string m_blog = string.Empty;

		// Token: 0x04000116 RID: 278
		private string m_province = string.Empty;

		// Token: 0x04000117 RID: 279
		private string m_city = string.Empty;

		// Token: 0x04000118 RID: 280
		private string m_address = string.Empty;

		// Token: 0x04000119 RID: 281
		private string m_zipcode = string.Empty;

		// Token: 0x0400011A RID: 282
		private string m_note = string.Empty;

		// Token: 0x0400011B RID: 283
		private string m_idcard = string.Empty;

		// Token: 0x0400011C RID: 284
		private int m_isidcard;

		// Token: 0x0400011D RID: 285
		private string m_idcardface = string.Empty;

		// Token: 0x0400011E RID: 286
		private string m_idcardback = string.Empty;

		// Token: 0x0400011F RID: 287
		private string m_idcardper = string.Empty;

		// Token: 0x04000120 RID: 288
		private DateTime m_idcarvalidity = DbUtils.GetDateTime();

		// Token: 0x04000121 RID: 289
		private string m_content = string.Empty;
	}
}
