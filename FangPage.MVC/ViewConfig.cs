using System;

namespace FangPage.MVC
{
	// Token: 0x02000008 RID: 8
	public class ViewConfig
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000032C5 File Offset: 0x000014C5
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000032CD File Offset: 0x000014CD
		public string path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000032D6 File Offset: 0x000014D6
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000032DE File Offset: 0x000014DE
		public string include
		{
			get
			{
				return this.m_include;
			}
			set
			{
				this.m_include = value;
			}
		}

		// Token: 0x04000003 RID: 3
		private string m_path = string.Empty;

		// Token: 0x04000004 RID: 4
		private string m_include = string.Empty;
	}
}
