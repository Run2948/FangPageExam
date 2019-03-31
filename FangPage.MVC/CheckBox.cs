using System;

namespace FangPage.MVC
{
	// Token: 0x02000002 RID: 2
	public class CheckBox : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CheckBox()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		public CheckBox(bool isCheckBox)
		{
			this.m_ischeckbox = isCheckBox;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000207D File Offset: 0x0000027D
		public CheckBox(string CheckName)
		{
			this.m_ischeckbox = true;
			this.m_checkname = CheckName;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000209E File Offset: 0x0000029E
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A6 File Offset: 0x000002A6
		public string CheckName
		{
			get
			{
				return this.m_checkname;
			}
			set
			{
				this.m_checkname = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020AF File Offset: 0x000002AF
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020B7 File Offset: 0x000002B7
		public bool IsCheckBox
		{
			get
			{
				return this.m_ischeckbox;
			}
			set
			{
				this.m_ischeckbox = value;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool m_ischeckbox;

		// Token: 0x04000002 RID: 2
		private string m_checkname = string.Empty;
	}
}
