using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;

namespace FP_Exam.Model
{
	// Token: 0x02000015 RID: 21
	[ModelPrefix("Exam")]
	public class TestInfo
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000063F4 File Offset: 0x000045F4
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000640C File Offset: 0x0000460C
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006418 File Offset: 0x00004618
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00006430 File Offset: 0x00004630
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000643C File Offset: 0x0000463C
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00006454 File Offset: 0x00004654
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00006460 File Offset: 0x00004660
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00006478 File Offset: 0x00004678
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00006484 File Offset: 0x00004684
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000649C File Offset: 0x0000469C
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

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000064A8 File Offset: 0x000046A8
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x000064C0 File Offset: 0x000046C0
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000064CC File Offset: 0x000046CC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x000064E4 File Offset: 0x000046E4
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000064F0 File Offset: 0x000046F0
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x00006508 File Offset: 0x00004708
		public int repeats
		{
			get
			{
				return this.m_repeats;
			}
			set
			{
				this.m_repeats = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006514 File Offset: 0x00004714
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000652C File Offset: 0x0000472C
		[CheckBox(true)]
		public FPData client
		{
			get
			{
				return this.m_client;
			}
			set
			{
				this.m_client = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00006538 File Offset: 0x00004738
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00006550 File Offset: 0x00004750
		public string flag_roles
		{
			get
			{
				return this.m_flag_roles;
			}
			set
			{
				this.m_flag_roles = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000655C File Offset: 0x0000475C
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00006574 File Offset: 0x00004774
		public string flag_departs
		{
			get
			{
				return this.m_flag_departs;
			}
			set
			{
				this.m_flag_departs = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00006580 File Offset: 0x00004780
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00006598 File Offset: 0x00004798
		public string flag_users
		{
			get
			{
				return this.m_flag_users;
			}
			set
			{
				this.m_flag_users = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000065A4 File Offset: 0x000047A4
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000065BC File Offset: 0x000047BC
		public string questions
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000065C8 File Offset: 0x000047C8
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000065E0 File Offset: 0x000047E0
		public string sorts
		{
			get
			{
				return this.m_sorts;
			}
			set
			{
				this.m_sorts = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000065EC File Offset: 0x000047EC
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00006604 File Offset: 0x00004804
		public int counts
		{
			get
			{
				return this.m_counts;
			}
			set
			{
				this.m_counts = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00006610 File Offset: 0x00004810
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00006628 File Offset: 0x00004828
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

		// Token: 0x040000DA RID: 218
		private int m_id;

		// Token: 0x040000DB RID: 219
		private int m_uid;

		// Token: 0x040000DC RID: 220
		private int m_channelid;

		// Token: 0x040000DD RID: 221
		private int m_sortid;

		// Token: 0x040000DE RID: 222
		private string m_sortname = string.Empty;

		// Token: 0x040000DF RID: 223
		private string m_typelist = string.Empty;

		// Token: 0x040000E0 RID: 224
		private string m_name = string.Empty;

		// Token: 0x040000E1 RID: 225
		private int m_repeats;

		// Token: 0x040000E2 RID: 226
		private FPData m_client = new FPData(new
		{
			pc = "1",
			mobile = "1"
		});

		// Token: 0x040000E3 RID: 227
		private string m_flag_roles = string.Empty;

		// Token: 0x040000E4 RID: 228
		private string m_flag_departs = string.Empty;

		// Token: 0x040000E5 RID: 229
		private string m_flag_users = string.Empty;

		// Token: 0x040000E6 RID: 230
		private string m_sorts = string.Empty;

		// Token: 0x040000E7 RID: 231
		private string m_questions = string.Empty;

		// Token: 0x040000E8 RID: 232
		private int m_counts;

		// Token: 0x040000E9 RID: 233
		private int m_status;
	}
}
