using System;

namespace FangPage.Data
{
	// Token: 0x02000010 RID: 16
	public class OrderByParam
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000058DC File Offset: 0x00003ADC
		public OrderByParam()
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000058EF File Offset: 0x00003AEF
		public OrderByParam(string ParamName, OrderBy OrderBy)
		{
			this.ParamName = ParamName;
			this.OrderBy = OrderBy;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005910 File Offset: 0x00003B10
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005918 File Offset: 0x00003B18
		public string ParamName
		{
			get
			{
				return this.m_ParamName;
			}
			set
			{
				this.m_ParamName = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005921 File Offset: 0x00003B21
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00005929 File Offset: 0x00003B29
		public OrderBy OrderBy
		{
			get
			{
				return this.m_OrderBy;
			}
			set
			{
				this.m_OrderBy = value;
			}
		}

		// Token: 0x04000018 RID: 24
		private string m_ParamName = "";

		// Token: 0x04000019 RID: 25
		private OrderBy m_OrderBy;
	}
}
