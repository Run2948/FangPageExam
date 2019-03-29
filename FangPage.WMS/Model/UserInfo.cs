using System;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Model
{
	// Token: 0x0200001B RID: 27
	[ModelPrefix("WMS")]
	public class UserInfo
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005AD4 File Offset: 0x00003CD4
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005AEC File Offset: 0x00003CEC
		[PrimaryKey(true)]
		[Identity(true)]
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005AF8 File Offset: 0x00003CF8
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005B10 File Offset: 0x00003D10
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005B1C File Offset: 0x00003D1C
		[BindField(false)]
		public RoleInfo RoleInfo
		{
			get
			{
				if (this.m_roleinfo == null)
				{
					this.m_roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.roleid);
				}
				return this.m_roleinfo;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005B58 File Offset: 0x00003D58
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00005B70 File Offset: 0x00003D70
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005B7C File Offset: 0x00003D7C
		[BindField(false)]
		public Department Department
		{
			get
			{
				if (this.m_department == null)
				{
					this.m_department = DbHelper.ExecuteModel<Department>(this.departid);
				}
				return this.m_department;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005BB8 File Offset: 0x00003DB8
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005BD0 File Offset: 0x00003DD0
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005BDC File Offset: 0x00003DDC
		[BindField(false)]
		public UserGrade UserGrade
		{
			get
			{
				if (this.m_usergrade == null)
				{
					this.m_usergrade = DbHelper.ExecuteModel<UserGrade>(this.gradeid);
				}
				return this.m_usergrade;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005C18 File Offset: 0x00003E18
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00005C30 File Offset: 0x00003E30
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005C3C File Offset: 0x00003E3C
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005C54 File Offset: 0x00003E54
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005C60 File Offset: 0x00003E60
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005C78 File Offset: 0x00003E78
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005C84 File Offset: 0x00003E84
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00005C9C File Offset: 0x00003E9C
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005CA8 File Offset: 0x00003EA8
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005CC0 File Offset: 0x00003EC0
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005CCC File Offset: 0x00003ECC
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00005CE4 File Offset: 0x00003EE4
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005CF0 File Offset: 0x00003EF0
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005D08 File Offset: 0x00003F08
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005D14 File Offset: 0x00003F14
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00005D2C File Offset: 0x00003F2C
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005D38 File Offset: 0x00003F38
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005D75 File Offset: 0x00003F75
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

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005D80 File Offset: 0x00003F80
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005D98 File Offset: 0x00003F98
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005DA4 File Offset: 0x00003FA4
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005DBC File Offset: 0x00003FBC
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005DC8 File Offset: 0x00003FC8
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005DE0 File Offset: 0x00003FE0
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005DEC File Offset: 0x00003FEC
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005E04 File Offset: 0x00004004
		public int sex
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005E10 File Offset: 0x00004010
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005E28 File Offset: 0x00004028
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005E34 File Offset: 0x00004034
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005E4C File Offset: 0x0000404C
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005E58 File Offset: 0x00004058
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00005E70 File Offset: 0x00004070
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005E7C File Offset: 0x0000407C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005E94 File Offset: 0x00004094
		public DateTime joindatetime
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005EA0 File Offset: 0x000040A0
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005EB8 File Offset: 0x000040B8
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005EC4 File Offset: 0x000040C4
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005EDC File Offset: 0x000040DC
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005EE8 File Offset: 0x000040E8
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005F00 File Offset: 0x00004100
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005F0C File Offset: 0x0000410C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00005F24 File Offset: 0x00004124
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00005F30 File Offset: 0x00004130
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00005F48 File Offset: 0x00004148
		public int onlinestate
		{
			get
			{
				return this.m_onlinestate;
			}
			set
			{
				this.m_onlinestate = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00005F54 File Offset: 0x00004154
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00005F6C File Offset: 0x0000416C
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005F78 File Offset: 0x00004178
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00005F90 File Offset: 0x00004190
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005F9C File Offset: 0x0000419C
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00005FB4 File Offset: 0x000041B4
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005FC0 File Offset: 0x000041C0
		[BindField(false)]
		public int vipdays
		{
			get
			{
				int result;
				if (this.vipdate == "")
				{
					result = 0;
				}
				else
				{
					DateTime d = Convert.ToDateTime(FPUtils.GetDateTime(this.vipdate, "yyyy-MM-dd"));
					DateTime d2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
					result = Convert.ToInt32((d - d2).TotalDays) + 1;
				}
				return result;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006038 File Offset: 0x00004238
		[BindField(false)]
		public int isvip
		{
			get
			{
				int result;
				if (this.vipdays > 0)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
				return result;
			}
		}

		// Token: 0x04000065 RID: 101
		private int m_id;

		// Token: 0x04000066 RID: 102
		private int m_roleid = 2;

		// Token: 0x04000067 RID: 103
		private RoleInfo m_roleinfo;

		// Token: 0x04000068 RID: 104
		private int m_departid;

		// Token: 0x04000069 RID: 105
		private Department m_department;

		// Token: 0x0400006A RID: 106
		private int m_gradeid;

		// Token: 0x0400006B RID: 107
		private UserGrade m_usergrade;

		// Token: 0x0400006C RID: 108
		private string m_type;

		// Token: 0x0400006D RID: 109
		private string m_username = string.Empty;

		// Token: 0x0400006E RID: 110
		private string m_password = string.Empty;

		// Token: 0x0400006F RID: 111
		private string m_password2 = string.Empty;

		// Token: 0x04000070 RID: 112
		private string m_email = string.Empty;

		// Token: 0x04000071 RID: 113
		private int m_isemail;

		// Token: 0x04000072 RID: 114
		private string m_mobile = string.Empty;

		// Token: 0x04000073 RID: 115
		private int m_ismobile;

		// Token: 0x04000074 RID: 116
		private string m_realname = string.Empty;

		// Token: 0x04000075 RID: 117
		private int m_isreal;

		// Token: 0x04000076 RID: 118
		private string m_nickname = string.Empty;

		// Token: 0x04000077 RID: 119
		private string m_avatar = string.Empty;

		// Token: 0x04000078 RID: 120
		private int m_sex = -1;

		// Token: 0x04000079 RID: 121
		private int m_exp;

		// Token: 0x0400007A RID: 122
		private int m_credits;

		// Token: 0x0400007B RID: 123
		private string m_regip = string.Empty;

		// Token: 0x0400007C RID: 124
		private DateTime m_joindatetime = DbUtils.GetDateTime();

		// Token: 0x0400007D RID: 125
		private string m_secques = string.Empty;

		// Token: 0x0400007E RID: 126
		private string m_authstr = string.Empty;

		// Token: 0x0400007F RID: 127
		private DateTime m_authtime = DbUtils.GetDateTime();

		// Token: 0x04000080 RID: 128
		private int m_authflag;

		// Token: 0x04000081 RID: 129
		private int m_onlinestate;

		// Token: 0x04000082 RID: 130
		private string m_lastip = string.Empty;

		// Token: 0x04000083 RID: 131
		private DateTime m_lastvisit = DbUtils.GetDateTime();

		// Token: 0x04000084 RID: 132
		private string m_vipdate = string.Empty;
	}
}
