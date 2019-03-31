using System;
using FangPage.Data;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Model
{
	// Token: 0x02000015 RID: 21
	[ModelPrefix("WMS")]
	public class SortAppInfo
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000577F File Offset: 0x0000397F
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005787 File Offset: 0x00003987
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005790 File Offset: 0x00003990
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00005798 File Offset: 0x00003998
		public string guid
		{
			get
			{
				return this.m_guid;
			}
			set
			{
				this.m_guid = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000057A1 File Offset: 0x000039A1
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000057A9 File Offset: 0x000039A9
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000057B2 File Offset: 0x000039B2
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000057BA File Offset: 0x000039BA
		public string installpath
		{
			get
			{
				return this.m_installpath;
			}
			set
			{
				this.m_installpath = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000057C3 File Offset: 0x000039C3
		[BindField(false)]
		public SetupInfo SetupInfo
		{
			get
			{
				if (this.m_setupinfo == null)
				{
					this.m_setupinfo = SetupBll.GetSetupInfo(this.type, this.installpath);
				}
				return this.m_setupinfo;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000057EA File Offset: 0x000039EA
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000057F2 File Offset: 0x000039F2
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000057FB File Offset: 0x000039FB
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00005803 File Offset: 0x00003A03
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000580C File Offset: 0x00003A0C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00005814 File Offset: 0x00003A14
		public string indexpage
		{
			get
			{
				return this.m_indexpage;
			}
			set
			{
				this.m_indexpage = value;
			}
		}

		// Token: 0x040000E6 RID: 230
		private int m_id;

		// Token: 0x040000E7 RID: 231
		private string m_guid = string.Empty;

		// Token: 0x040000E8 RID: 232
		private string m_type = string.Empty;

		// Token: 0x040000E9 RID: 233
		private string m_installpath = string.Empty;

		// Token: 0x040000EA RID: 234
		private SetupInfo m_setupinfo;

		// Token: 0x040000EB RID: 235
		private string m_name = string.Empty;

		// Token: 0x040000EC RID: 236
		private string m_markup = string.Empty;

		// Token: 0x040000ED RID: 237
		private string m_indexpage = string.Empty;
	}
}
