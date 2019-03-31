using System;

namespace FP_Exam.Model
{
	// Token: 0x02000013 RID: 19
	public class ImgInfo
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006208 File Offset: 0x00004408
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00006220 File Offset: 0x00004420
		public string key
		{
			get
			{
				return this.m_key;
			}
			set
			{
				this.m_key = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000622C File Offset: 0x0000442C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00006244 File Offset: 0x00004444
		public string src
		{
			get
			{
				return this.m_src;
			}
			set
			{
				this.m_src = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00006250 File Offset: 0x00004450
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00006268 File Offset: 0x00004468
		public string file
		{
			get
			{
				return this.m_file;
			}
			set
			{
				this.m_file = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00006274 File Offset: 0x00004474
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000628C File Offset: 0x0000448C
		public double width
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00006298 File Offset: 0x00004498
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000062B0 File Offset: 0x000044B0
		public double height
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

		// Token: 0x040000CE RID: 206
		private string m_key = string.Empty;

		// Token: 0x040000CF RID: 207
		private string m_src = string.Empty;

		// Token: 0x040000D0 RID: 208
		private string m_file = string.Empty;

		// Token: 0x040000D1 RID: 209
		private double m_width;

		// Token: 0x040000D2 RID: 210
		private double m_height;
	}
}
