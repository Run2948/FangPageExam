using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x02000019 RID: 25
	[ModelPrefix("WMS")]
	public class DesktopInfo
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005838 File Offset: 0x00003A38
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00005850 File Offset: 0x00003A50
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000585C File Offset: 0x00003A5C
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00005874 File Offset: 0x00003A74
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005880 File Offset: 0x00003A80
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00005898 File Offset: 0x00003A98
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000058A4 File Offset: 0x00003AA4
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000058BC File Offset: 0x00003ABC
		public string icon
		{
			get
			{
				return this.m_icon;
			}
			set
			{
				this.m_icon = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000058C8 File Offset: 0x00003AC8
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000058E0 File Offset: 0x00003AE0
		public string lefturl
		{
			get
			{
				return this.m_lefturl;
			}
			set
			{
				this.m_lefturl = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000058EC File Offset: 0x00003AEC
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005904 File Offset: 0x00003B04
		public string righturl
		{
			get
			{
				return this.m_righturl;
			}
			set
			{
				this.m_righturl = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005910 File Offset: 0x00003B10
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00005928 File Offset: 0x00003B28
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005934 File Offset: 0x00003B34
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000594C File Offset: 0x00003B4C
		public int hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				this.m_hidden = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005958 File Offset: 0x00003B58
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00005970 File Offset: 0x00003B70
		public int system
		{
			get
			{
				return this.m_system;
			}
			set
			{
				this.m_system = value;
			}
		}

		// Token: 0x04000055 RID: 85
		private int m_id;

		// Token: 0x04000056 RID: 86
		private int m_uid;

		// Token: 0x04000057 RID: 87
		private string m_name = string.Empty;

		// Token: 0x04000058 RID: 88
		private string m_icon = string.Empty;

		// Token: 0x04000059 RID: 89
		private string m_lefturl = string.Empty;

		// Token: 0x0400005A RID: 90
		private string m_righturl = string.Empty;

		// Token: 0x0400005B RID: 91
		private string m_description = string.Empty;

		// Token: 0x0400005C RID: 92
		private int m_hidden;

		// Token: 0x0400005D RID: 93
		private int m_system;
	}
}
