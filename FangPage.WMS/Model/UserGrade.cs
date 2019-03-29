using System;
using FangPage.Data;

namespace FangPage.WMS.Model
{
	// Token: 0x0200002A RID: 42
	[ModelPrefix("WMS")]
	public class UserGrade
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007AA0 File Offset: 0x00005CA0
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00007AB8 File Offset: 0x00005CB8
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007AC4 File Offset: 0x00005CC4
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00007ADC File Offset: 0x00005CDC
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00007AE8 File Offset: 0x00005CE8
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00007B00 File Offset: 0x00005D00
		public int stars
		{
			get
			{
				return this.m_stars;
			}
			set
			{
				this.m_stars = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00007B0C File Offset: 0x00005D0C
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00007B24 File Offset: 0x00005D24
		public int explower
		{
			get
			{
				return this.m_explower;
			}
			set
			{
				this.m_explower = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00007B30 File Offset: 0x00005D30
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00007B48 File Offset: 0x00005D48
		public int expupper
		{
			get
			{
				return this.m_expupper;
			}
			set
			{
				this.m_expupper = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007B54 File Offset: 0x00005D54
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00007B6C File Offset: 0x00005D6C
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

		// Token: 0x04000122 RID: 290
		private int m_id;

		// Token: 0x04000123 RID: 291
		private string m_name = string.Empty;

		// Token: 0x04000124 RID: 292
		private int m_stars;

		// Token: 0x04000125 RID: 293
		private int m_explower;

		// Token: 0x04000126 RID: 294
		private int m_expupper;

		// Token: 0x04000127 RID: 295
		private string m_description = string.Empty;
	}
}
