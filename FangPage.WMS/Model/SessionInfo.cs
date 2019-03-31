using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200000F RID: 15
	[ModelPrefix("WMS")]
	public class SessionInfo
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004BB8 File Offset: 0x00002DB8
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004BC0 File Offset: 0x00002DC0
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004BC9 File Offset: 0x00002DC9
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004BD1 File Offset: 0x00002DD1
		public string sid
		{
			get
			{
				return this.m_sid;
			}
			set
			{
				this.m_sid = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004BDA File Offset: 0x00002DDA
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004BE2 File Offset: 0x00002DE2
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004BEB File Offset: 0x00002DEB
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004BF3 File Offset: 0x00002DF3
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004C04 File Offset: 0x00002E04
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004C0D File Offset: 0x00002E0D
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004C15 File Offset: 0x00002E15
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004C1E File Offset: 0x00002E1E
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004C26 File Offset: 0x00002E26
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004C2F File Offset: 0x00002E2F
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004C37 File Offset: 0x00002E37
		public string platform
		{
			get
			{
				return this.m_platform;
			}
			set
			{
				this.m_platform = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004C40 File Offset: 0x00002E40
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004C48 File Offset: 0x00002E48
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004C51 File Offset: 0x00002E51
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004C59 File Offset: 0x00002E59
		public string token
		{
			get
			{
				return this.m_token;
			}
			set
			{
				this.m_token = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004C62 File Offset: 0x00002E62
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004C6A File Offset: 0x00002E6A
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004C73 File Offset: 0x00002E73
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00004C7B File Offset: 0x00002E7B
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004C84 File Offset: 0x00002E84
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004C8C File Offset: 0x00002E8C
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004C95 File Offset: 0x00002E95
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004C9D File Offset: 0x00002E9D
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004CA6 File Offset: 0x00002EA6
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004CAE File Offset: 0x00002EAE
		public DateTime createtime
		{
			get
			{
				return this.m_createtime;
			}
			set
			{
				this.m_createtime = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004CB7 File Offset: 0x00002EB7
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004CBF File Offset: 0x00002EBF
		public DateTime updatetime
		{
			get
			{
				return this.m_updatetime;
			}
			set
			{
				this.m_updatetime = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004CC8 File Offset: 0x00002EC8
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public int timeout
		{
			get
			{
				return this.m_timeout;
			}
			set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004CD9 File Offset: 0x00002ED9
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00004CE1 File Offset: 0x00002EE1
		public int invisible
		{
			get
			{
				return this.m_invisible;
			}
			set
			{
				this.m_invisible = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004CEA File Offset: 0x00002EEA
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00004CF2 File Offset: 0x00002EF2
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004CFB File Offset: 0x00002EFB
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004D03 File Offset: 0x00002F03
		[BindField(false)]
		public int logincheck
		{
			get
			{
				return this.m_logincheck;
			}
			set
			{
				this.m_logincheck = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004D0C File Offset: 0x00002F0C
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004D14 File Offset: 0x00002F14
		[BindField(false)]
		public int port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				this.m_port = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004D1D File Offset: 0x00002F1D
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004D25 File Offset: 0x00002F25
		[BindField(false)]
		public int errcode
		{
			get
			{
				return this.m_errcode;
			}
			set
			{
				this.m_errcode = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004D2E File Offset: 0x00002F2E
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004D36 File Offset: 0x00002F36
		[BindField(false)]
		public string errmsg
		{
			get
			{
				return this.m_errmsg;
			}
			set
			{
				this.m_errmsg = value;
			}
		}

		// Token: 0x04000087 RID: 135
		private int m_id;

		// Token: 0x04000088 RID: 136
		private string m_sid = string.Empty;

		// Token: 0x04000089 RID: 137
		private int m_uid;

		// Token: 0x0400008A RID: 138
		private string m_username = string.Empty;

		// Token: 0x0400008B RID: 139
		private string m_realname = string.Empty;

		// Token: 0x0400008C RID: 140
		private string m_password = string.Empty;

		// Token: 0x0400008D RID: 141
		private string m_avatar = string.Empty;

		// Token: 0x0400008E RID: 142
		private string m_platform = string.Empty;

		// Token: 0x0400008F RID: 143
		private string m_address = string.Empty;

		// Token: 0x04000090 RID: 144
		private string m_token = string.Empty;

		// Token: 0x04000091 RID: 145
		private int m_roleid;

		// Token: 0x04000092 RID: 146
		private string m_rolename = string.Empty;

		// Token: 0x04000093 RID: 147
		private int m_departid;

		// Token: 0x04000094 RID: 148
		private string m_departname = string.Empty;

		// Token: 0x04000095 RID: 149
		private DateTime m_createtime = DbUtils.GetDateTime();

		// Token: 0x04000096 RID: 150
		private DateTime m_updatetime = DbUtils.GetDateTime();

		// Token: 0x04000097 RID: 151
		private int m_timeout;

		// Token: 0x04000098 RID: 152
		private int m_invisible;

		// Token: 0x04000099 RID: 153
		private int m_state = 1;

		// Token: 0x0400009A RID: 154
		private int m_logincheck;

		// Token: 0x0400009B RID: 155
		private string m_cookiename = string.Empty;

		// Token: 0x0400009C RID: 156
		private int m_port;

		// Token: 0x0400009D RID: 157
		private int m_errcode;

		// Token: 0x0400009E RID: 158
		private string m_errmsg = string.Empty;
	}
}
