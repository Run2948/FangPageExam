using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Model
{
	// Token: 0x0200000E RID: 14
	[ModelPrefix("WMS")]
	public class DesktopInfo
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000478C File Offset: 0x0000298C
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004794 File Offset: 0x00002994
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000479D File Offset: 0x0000299D
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000047A5 File Offset: 0x000029A5
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000047AE File Offset: 0x000029AE
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000047B6 File Offset: 0x000029B6
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000047BF File Offset: 0x000029BF
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000047C7 File Offset: 0x000029C7
		public string app
		{
			get
			{
				return this.m_app;
			}
			set
			{
				this.m_app = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000047D0 File Offset: 0x000029D0
		[BindField(false)]
		public SetupInfo SetupInfo
		{
			get
			{
				if (this.m_setupinfo == null)
				{
					this.m_setupinfo = SetupBll.GetSetupInfoByMarkup(this.app);
				}
				return this.m_setupinfo;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000047F1 File Offset: 0x000029F1
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000047F9 File Offset: 0x000029F9
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004802 File Offset: 0x00002A02
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000480A File Offset: 0x00002A0A
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004813 File Offset: 0x00002A13
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000481B File Offset: 0x00002A1B
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004824 File Offset: 0x00002A24
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000482C File Offset: 0x00002A2C
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004835 File Offset: 0x00002A35
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000483D File Offset: 0x00002A3D
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004846 File Offset: 0x00002A46
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000484E File Offset: 0x00002A4E
		public string attach_icon
		{
			get
			{
				return this.m_attach_icon;
			}
			set
			{
				this.m_attach_icon = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004858 File Offset: 0x00002A58
		[BindField(false)]
		public string map_icon
		{
			get
			{
				if (this.icon != "")
				{
					if (this.app != "" && !this.icon.StartsWith("/") && !this.icon.StartsWith("http://") && !this.icon.StartsWith("\\") && !this.icon.StartsWith("https://"))
					{
						if (this.SetupInfo.type == "sites")
						{
							this.m_map_icon = WebConfig.WebPath + this.SetupInfo.installpath + "/" + this.icon;
						}
						else
						{
							this.m_map_icon = string.Concat(new string[]
							{
								WebConfig.WebPath,
								this.SetupInfo.type,
								"/",
								this.SetupInfo.installpath,
								"/",
								this.icon
							});
						}
					}
					else
					{
						this.m_map_icon = this.icon;
					}
				}
				else
				{
					this.m_map_icon = WebConfig.WebPath + "common/images/desktop.png";
				}
				return this.m_map_icon;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000499B File Offset: 0x00002B9B
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000049A3 File Offset: 0x00002BA3
		public string url
		{
			get
			{
				return this.m_url;
			}
			set
			{
				this.m_url = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000049AC File Offset: 0x00002BAC
		[BindField(false)]
		public string map_url
		{
			get
			{
				if (this.app != "" && !this.url.StartsWith("/") && !this.url.StartsWith("http://") && !this.url.StartsWith("\\") && !this.url.StartsWith("https://"))
				{
					if (this.SetupInfo.type == "sites")
					{
						this.m_map_url = WebConfig.WebPath + this.SetupInfo.installpath + "/" + this.url;
					}
					else
					{
						this.m_map_url = string.Concat(new string[]
						{
							WebConfig.WebPath,
							this.SetupInfo.type,
							"/",
							this.SetupInfo.installpath,
							"/",
							this.url
						});
					}
				}
				else
				{
					this.m_map_url = this.url;
				}
				return this.m_map_url;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004AC3 File Offset: 0x00002CC3
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004ACB File Offset: 0x00002CCB
		public string target
		{
			get
			{
				return this.m_target;
			}
			set
			{
				this.m_target = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004AD4 File Offset: 0x00002CD4
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004ADC File Offset: 0x00002CDC
		public int desk
		{
			get
			{
				return this.m_desk;
			}
			set
			{
				this.m_desk = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004AE5 File Offset: 0x00002CE5
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004AED File Offset: 0x00002CED
		public int width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004AF6 File Offset: 0x00002CF6
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004AFE File Offset: 0x00002CFE
		public int height
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004B07 File Offset: 0x00002D07
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004B0F File Offset: 0x00002D0F
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004B18 File Offset: 0x00002D18
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004B20 File Offset: 0x00002D20
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

		// Token: 0x04000073 RID: 115
		private int m_id;

		// Token: 0x04000074 RID: 116
		private int m_uid;

		// Token: 0x04000075 RID: 117
		private string m_platform = string.Empty;

		// Token: 0x04000076 RID: 118
		private string m_app = string.Empty;

		// Token: 0x04000077 RID: 119
		private SetupInfo m_setupinfo;

		// Token: 0x04000078 RID: 120
		private int m_parentid;

		// Token: 0x04000079 RID: 121
		private string m_name = string.Empty;

		// Token: 0x0400007A RID: 122
		private string m_markup = string.Empty;

		// Token: 0x0400007B RID: 123
		private string m_description = string.Empty;

		// Token: 0x0400007C RID: 124
		private string m_icon = string.Empty;

		// Token: 0x0400007D RID: 125
		private string m_map_icon = string.Empty;

		// Token: 0x0400007E RID: 126
		private string m_attach_icon = string.Empty;

		// Token: 0x0400007F RID: 127
		private string m_url = string.Empty;

		// Token: 0x04000080 RID: 128
		private string m_map_url = string.Empty;

		// Token: 0x04000081 RID: 129
		private string m_target = string.Empty;

		// Token: 0x04000082 RID: 130
		private int m_desk;

		// Token: 0x04000083 RID: 131
		private int m_width;

		// Token: 0x04000084 RID: 132
		private int m_height;

		// Token: 0x04000085 RID: 133
		private int m_hidden;

		// Token: 0x04000086 RID: 134
		private int m_display;
	}
}
