using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Model
{
	// Token: 0x02000013 RID: 19
	[ModelPrefix("WMS")]
	public class UserInfo
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000050BE File Offset: 0x000032BE
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000050C6 File Offset: 0x000032C6
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

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000050CF File Offset: 0x000032CF
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000050D7 File Offset: 0x000032D7
		[LeftJoin("WMS_RoleInfo", "id")]
		public int roleid
		{
			get
			{
				return this.m_roleid;
			}
			set
			{
				this.m_roleid = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000050E0 File Offset: 0x000032E0
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000050E8 File Offset: 0x000032E8
		[Map("WMS_RoleInfo", "name")]
		public string rolename
		{
			get
			{
				return this.m_rolename;
			}
			set
			{
				this.m_rolename = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000050F1 File Offset: 0x000032F1
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000050F9 File Offset: 0x000032F9
		public string roles
		{
			get
			{
				return this.m_roles;
			}
			set
			{
				this.m_roles = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005102 File Offset: 0x00003302
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000510A File Offset: 0x0000330A
		public int departid
		{
			get
			{
				return this.m_departid;
			}
			set
			{
				this.m_departid = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005113 File Offset: 0x00003313
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000511B File Offset: 0x0000331B
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005124 File Offset: 0x00003324
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000512C File Offset: 0x0000332C
		public string departs
		{
			get
			{
				return this.m_departs;
			}
			set
			{
				this.m_departs = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005135 File Offset: 0x00003335
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000513D File Offset: 0x0000333D
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005146 File Offset: 0x00003346
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000514E File Offset: 0x0000334E
		[LeftJoin("WMS_GradeInfo", "id")]
		public int gradeid
		{
			get
			{
				return this.m_gradeid;
			}
			set
			{
				this.m_gradeid = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005157 File Offset: 0x00003357
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000515F File Offset: 0x0000335F
		[Map("WMS_GradeInfo", "name")]
		public string gradename
		{
			get
			{
				return this.m_gradename;
			}
			set
			{
				this.m_gradename = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005168 File Offset: 0x00003368
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005170 File Offset: 0x00003370
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000517C File Offset: 0x0000337C
		[BindField(false)]
		public string usertype
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000051F4 File Offset: 0x000033F4
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000051FC File Offset: 0x000033FC
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005205 File Offset: 0x00003405
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000520D File Offset: 0x0000340D
		public string password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005216 File Offset: 0x00003416
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000521E File Offset: 0x0000341E
		public string password2
		{
			get
			{
				return this.m_password2;
			}
			set
			{
				this.m_password2 = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005227 File Offset: 0x00003427
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000522F File Offset: 0x0000342F
		public string email
		{
			get
			{
				return this.m_email;
			}
			set
			{
				this.m_email = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005238 File Offset: 0x00003438
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005240 File Offset: 0x00003440
		public int isemail
		{
			get
			{
				return this.m_isemail;
			}
			set
			{
				this.m_isemail = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005249 File Offset: 0x00003449
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005251 File Offset: 0x00003451
		public string mobile
		{
			get
			{
				return this.m_mobile;
			}
			set
			{
				this.m_mobile = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000525A File Offset: 0x0000345A
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005262 File Offset: 0x00003462
		public int ismobile
		{
			get
			{
				return this.m_ismobile;
			}
			set
			{
				this.m_ismobile = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000526B File Offset: 0x0000346B
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005291 File Offset: 0x00003491
		public string realname
		{
			get
			{
				if (this.m_realname == "")
				{
					this.m_realname = this.username;
				}
				return this.m_realname;
			}
			set
			{
				this.m_realname = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000529A File Offset: 0x0000349A
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000052A2 File Offset: 0x000034A2
		public string cardtype
		{
			get
			{
				return this.m_cardtype;
			}
			set
			{
				this.m_cardtype = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000052AB File Offset: 0x000034AB
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000052B3 File Offset: 0x000034B3
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

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000052BC File Offset: 0x000034BC
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000052C4 File Offset: 0x000034C4
		public int isreal
		{
			get
			{
				return this.m_isreal;
			}
			set
			{
				this.m_isreal = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000052CD File Offset: 0x000034CD
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000052D5 File Offset: 0x000034D5
		public string usercode
		{
			get
			{
				return this.m_usercode;
			}
			set
			{
				this.m_usercode = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000052DE File Offset: 0x000034DE
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000052E6 File Offset: 0x000034E6
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000052EF File Offset: 0x000034EF
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000052F7 File Offset: 0x000034F7
		public string avatar
		{
			get
			{
				return this.m_avatar;
			}
			set
			{
				this.m_avatar = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00005300 File Offset: 0x00003500
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005308 File Offset: 0x00003508
		public string sex
		{
			get
			{
				return this.m_sex;
			}
			set
			{
				this.m_sex = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005311 File Offset: 0x00003511
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00005319 File Offset: 0x00003519
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005322 File Offset: 0x00003522
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000532A File Offset: 0x0000352A
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005333 File Offset: 0x00003533
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000533B File Offset: 0x0000353B
		public string regip
		{
			get
			{
				return this.m_regip;
			}
			set
			{
				this.m_regip = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00005344 File Offset: 0x00003544
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000534C File Offset: 0x0000354C
		public DateTime? joindatetime
		{
			get
			{
				return this.m_joindatetime;
			}
			set
			{
				this.m_joindatetime = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00005355 File Offset: 0x00003555
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000535D File Offset: 0x0000355D
		public string secques
		{
			get
			{
				return this.m_secques;
			}
			set
			{
				this.m_secques = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005366 File Offset: 0x00003566
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000536E File Offset: 0x0000356E
		public string authstr
		{
			get
			{
				return this.m_authstr;
			}
			set
			{
				this.m_authstr = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005377 File Offset: 0x00003577
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000537F File Offset: 0x0000357F
		public DateTime authtime
		{
			get
			{
				return this.m_authtime;
			}
			set
			{
				this.m_authtime = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005388 File Offset: 0x00003588
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005390 File Offset: 0x00003590
		public int authflag
		{
			get
			{
				return this.m_authflag;
			}
			set
			{
				this.m_authflag = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005399 File Offset: 0x00003599
		// (set) Token: 0x0600017F RID: 383 RVA: 0x000053A1 File Offset: 0x000035A1
		public int sumlogin
		{
			get
			{
				return this.m_sumlogin;
			}
			set
			{
				this.m_sumlogin = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000053AA File Offset: 0x000035AA
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000053B2 File Offset: 0x000035B2
		public string lastip
		{
			get
			{
				return this.m_lastip;
			}
			set
			{
				this.m_lastip = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000053BB File Offset: 0x000035BB
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000053C3 File Offset: 0x000035C3
		public DateTime lastvisit
		{
			get
			{
				return this.m_lastvisit;
			}
			set
			{
				this.m_lastvisit = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000053CC File Offset: 0x000035CC
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000053D4 File Offset: 0x000035D4
		public string vipdate
		{
			get
			{
				return this.m_vipdate;
			}
			set
			{
				this.m_vipdate = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000053E0 File Offset: 0x000035E0
		[BindField(false)]
		public int vipdays
		{
			get
			{
				if (this.vipdate == "")
				{
					return 0;
				}
				DateTime d = FPUtils.StrToDateTime(this.vipdate, "yyyy-MM-dd");
				DateTime d2 = FPUtils.StrToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
				return Convert.ToInt32((d - d2).TotalDays) + 1;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000543E File Offset: 0x0000363E
		[BindField(false)]
		public int isvip
		{
			get
			{
				if (this.vipdays > 0)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000544C File Offset: 0x0000364C
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00005454 File Offset: 0x00003654
		public int state
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000545D File Offset: 0x0000365D
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00005465 File Offset: 0x00003665
		public int issso
		{
			get
			{
				return this.m_issso;
			}
			set
			{
				this.m_issso = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005470 File Offset: 0x00003670
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00005596 File Offset: 0x00003796
		public FPData extend
		{
			get
			{
				if (this.m_extend.ContainsKey("bday") && this.m_extend["bday"] == "")
				{
					if (this.idcard.Length == 18)
					{
						this.m_extend["bday"] = string.Concat(new string[]
						{
							this.idcard.Substring(6, 4),
							"-",
							this.idcard.Substring(10, 2),
							"-",
							this.idcard.Substring(12, 2)
						});
					}
					if (this.idcard.Length == 15)
					{
						this.m_extend["bday"] = string.Concat(new string[]
						{
							"19",
							this.idcard.Substring(6, 2),
							"-",
							this.idcard.Substring(8, 2),
							"-",
							this.idcard.Substring(10, 2)
						});
					}
				}
				return this.m_extend;
			}
			set
			{
				this.m_extend = value;
			}
		}

		// Token: 0x040000B6 RID: 182
		private int m_id;

		// Token: 0x040000B7 RID: 183
		private int m_roleid;

		// Token: 0x040000B8 RID: 184
		private string m_rolename = string.Empty;

		// Token: 0x040000B9 RID: 185
		private string m_roles = string.Empty;

		// Token: 0x040000BA RID: 186
		private int m_departid;

		// Token: 0x040000BB RID: 187
		private string m_departname = string.Empty;

		// Token: 0x040000BC RID: 188
		private string m_departs = string.Empty;

		// Token: 0x040000BD RID: 189
		private int m_display;

		// Token: 0x040000BE RID: 190
		private int m_gradeid;

		// Token: 0x040000BF RID: 191
		private string m_gradename = string.Empty;

		// Token: 0x040000C0 RID: 192
		private string m_types = string.Empty;

		// Token: 0x040000C1 RID: 193
		private string m_username = string.Empty;

		// Token: 0x040000C2 RID: 194
		private string m_password = string.Empty;

		// Token: 0x040000C3 RID: 195
		private string m_password2 = string.Empty;

		// Token: 0x040000C4 RID: 196
		private string m_email = string.Empty;

		// Token: 0x040000C5 RID: 197
		private int m_isemail;

		// Token: 0x040000C6 RID: 198
		private string m_mobile = string.Empty;

		// Token: 0x040000C7 RID: 199
		private int m_ismobile;

		// Token: 0x040000C8 RID: 200
		private string m_realname = string.Empty;

		// Token: 0x040000C9 RID: 201
		private string m_cardtype = string.Empty;

		// Token: 0x040000CA RID: 202
		private string m_idcard = string.Empty;

		// Token: 0x040000CB RID: 203
		private int m_isreal;

		// Token: 0x040000CC RID: 204
		private string m_usercode;

		// Token: 0x040000CD RID: 205
		private string m_nickname = string.Empty;

		// Token: 0x040000CE RID: 206
		private string m_avatar = string.Empty;

		// Token: 0x040000CF RID: 207
		private string m_sex = string.Empty;

		// Token: 0x040000D0 RID: 208
		private int m_exp;

		// Token: 0x040000D1 RID: 209
		private int m_credits;

		// Token: 0x040000D2 RID: 210
		private string m_regip = string.Empty;

		// Token: 0x040000D3 RID: 211
		private DateTime? m_joindatetime;

		// Token: 0x040000D4 RID: 212
		private string m_secques = string.Empty;

		// Token: 0x040000D5 RID: 213
		private string m_authstr = string.Empty;

		// Token: 0x040000D6 RID: 214
		private DateTime m_authtime = DbUtils.GetDateTime();

		// Token: 0x040000D7 RID: 215
		private int m_authflag;

		// Token: 0x040000D8 RID: 216
		private int m_sumlogin;

		// Token: 0x040000D9 RID: 217
		private string m_lastip = string.Empty;

		// Token: 0x040000DA RID: 218
		private DateTime m_lastvisit = DbUtils.GetDateTime();

		// Token: 0x040000DB RID: 219
		private string m_vipdate = string.Empty;

		// Token: 0x040000DC RID: 220
		private int m_state = 1;

		// Token: 0x040000DD RID: 221
		private int m_issso;

		// Token: 0x040000DE RID: 222
		private FPData m_extend = new FPData();
	}
}
