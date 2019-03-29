using System;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class Pager
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005932 File Offset: 0x00003B32
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000594A File Offset: 0x00003B4A
		public int pageindex
		{
			get
			{
				if (this.m_pageindex <= 0)
				{
					this.m_pageindex = 1;
				}
				return this.m_pageindex;
			}
			set
			{
				this.m_pageindex = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005953 File Offset: 0x00003B53
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000595B File Offset: 0x00003B5B
		public int total
		{
			get
			{
				return this.m_total;
			}
			set
			{
				this.m_total = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005964 File Offset: 0x00003B64
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000597D File Offset: 0x00003B7D
		public int pagesize
		{
			get
			{
				if (this.m_pagesize <= 0)
				{
					this.m_pagesize = 20;
				}
				return this.m_pagesize;
			}
			set
			{
				this.m_pagesize = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005988 File Offset: 0x00003B88
		public int pagecount
		{
			get
			{
				this.m_pagecount = this.total / this.pagesize;
				if (this.total % this.pagesize != 0)
				{
					this.m_pagecount++;
				}
				if (this.m_pagecount < 1)
				{
					this.m_pagecount = 1;
				}
				return this.m_pagecount;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000059DB File Offset: 0x00003BDB
		public int nextpage
		{
			get
			{
				this.m_nextpage = this.pageindex + 1;
				if (this.m_nextpage > this.pagecount)
				{
					this.m_nextpage = this.pagecount;
				}
				return this.m_nextpage;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005A0B File Offset: 0x00003C0B
		public int prepage
		{
			get
			{
				this.m_prepage = this.pageindex - 1;
				if (this.m_prepage <= 0)
				{
					this.m_prepage = 1;
				}
				return this.m_prepage;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005A31 File Offset: 0x00003C31
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00005A49 File Offset: 0x00003C49
		public int extendpage
		{
			get
			{
				if (this.m_extendpage < 6)
				{
					this.m_extendpage = 6;
				}
				return this.m_extendpage;
			}
			set
			{
				this.m_extendpage = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005A54 File Offset: 0x00003C54
		public string pagenum
		{
			get
			{
				int num = 1;
				string text = "";
				if (this.pageurl.IndexOf("?") > 0)
				{
					string[] array = this.pageurl.Split(new char[]
					{
						'?'
					});
					text = array[0] + "?";
					string text2 = "";
					foreach (string text3 in array[1].Split(new char[]
					{
						'&'
					}))
					{
						if (text3.IndexOf("pageindex=") < 0)
						{
							text2 += ((text2 == "") ? text3 : ("&" + text3));
						}
					}
					if (text2 != "")
					{
						text = text + text2 + "&";
					}
				}
				else
				{
					text += "?";
				}
				string value = "<a href=\"" + text + "pageindex=1\" >首页</a>";
				string value2 = string.Concat(new object[]
				{
					"<a href=\"",
					text,
					"pageindex=",
					this.prepage,
					"\" >上一页</a>"
				});
				string value3 = string.Concat(new object[]
				{
					"<a href=\"",
					text,
					"pageindex=",
					this.nextpage,
					"\" >下一页</a>"
				});
				string value4 = string.Concat(new object[]
				{
					"<a href=\"",
					text,
					"pageindex=",
					this.pagecount,
					"\" >尾页</a>"
				});
				int num2;
				if (this.pagecount > this.extendpage)
				{
					if (this.pageindex - this.extendpage / 2 > 0)
					{
						if (this.pageindex + this.extendpage / 2 < this.pagecount)
						{
							num = this.pageindex - this.extendpage / 2;
							num2 = num + this.extendpage - 1;
						}
						else
						{
							num2 = this.pagecount;
							num = num2 - this.extendpage + 1;
							value4 = "";
						}
					}
					else
					{
						num2 = this.extendpage;
						value4 = "";
					}
				}
				else
				{
					num = 1;
					num2 = this.pagecount;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(value);
				stringBuilder.Append(value2);
				for (int j = num; j <= num2; j++)
				{
					if (j == this.pageindex)
					{
						stringBuilder.AppendFormat("<a class=\"current\" href=\"{0}{1}\">{2}</a>", text, "pageindex=" + j, j);
					}
					else
					{
						stringBuilder.AppendFormat("<a href=\"{0}{1}\">{2}</a>", text, "pageindex=" + j, j);
					}
				}
				stringBuilder.Append(value3);
				stringBuilder.Append(value4);
				this.m_pagenum = stringBuilder.ToString();
				return this.m_pagenum;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005D4A File Offset: 0x00003F4A
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005D52 File Offset: 0x00003F52
		public string pageurl
		{
			get
			{
				return this.m_pageurl;
			}
			set
			{
				this.m_pageurl = value;
			}
		}

		// Token: 0x0400001D RID: 29
		private int m_pageindex = 1;

		// Token: 0x0400001E RID: 30
		private int m_total;

		// Token: 0x0400001F RID: 31
		private int m_pagesize = 20;

		// Token: 0x04000020 RID: 32
		private int m_pagecount;

		// Token: 0x04000021 RID: 33
		private int m_nextpage;

		// Token: 0x04000022 RID: 34
		private int m_prepage;

		// Token: 0x04000023 RID: 35
		private int m_extendpage = 6;

		// Token: 0x04000024 RID: 36
		private string m_pagenum = "";

		// Token: 0x04000025 RID: 37
		private string m_pageurl = "";
	}
}
