using System;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class Pager
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000BAFC File Offset: 0x00009CFC
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000BB04 File Offset: 0x00009D04
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

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000BB0D File Offset: 0x00009D0D
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000BB48 File Offset: 0x00009D48
		public int pageindex
		{
			get
			{
				if (this.m_pageindex > this.pagecount && this.total > 0)
				{
					this.m_pageindex = this.pagecount;
				}
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000BB51 File Offset: 0x00009D51
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000BB59 File Offset: 0x00009D59
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000BB62 File Offset: 0x00009D62
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000BB7B File Offset: 0x00009D7B
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000BB84 File Offset: 0x00009D84
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000BBD7 File Offset: 0x00009DD7
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000BC07 File Offset: 0x00009E07
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000BC2D File Offset: 0x00009E2D
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000BC45 File Offset: 0x00009E45
		public int extpage
		{
			get
			{
				if (this.m_extpage < 6)
				{
					this.m_extpage = 6;
				}
				return this.m_extpage;
			}
			set
			{
				this.m_extpage = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000BC50 File Offset: 0x00009E50
		public int startpage
		{
			get
			{
				if (this.pagecount > this.extpage)
				{
					if (this.pageindex - this.extpage / 2 > 0)
					{
						if (this.pageindex + this.extpage / 2 < this.pagecount)
						{
							this.m_startpage = this.pageindex - this.extpage / 2;
						}
						else
						{
							this.m_startpage = this.pagecount - this.extpage + 1;
						}
					}
				}
				else
				{
					this.m_startpage = 1;
				}
				return this.m_startpage;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public int endpage
		{
			get
			{
				if (this.pagecount > this.extpage)
				{
					if (this.pageindex - this.extpage / 2 > 0)
					{
						if (this.pageindex + this.extpage / 2 < this.pagecount)
						{
							this.m_endpage = this.pageindex + this.extpage / 2 - 1;
						}
						else
						{
							this.m_endpage = this.pagecount;
						}
					}
					else
					{
						this.m_endpage = this.extpage;
					}
				}
				else
				{
					this.m_endpage = this.pagecount;
				}
				return this.m_endpage;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public string pagenum
		{
			get
			{
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
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(value);
				stringBuilder.Append(value2);
				for (int j = this.startpage; j <= this.endpage; j++)
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

		// Token: 0x04000029 RID: 41
		private string m_pageurl = "";

		// Token: 0x0400002A RID: 42
		private int m_pageindex = 1;

		// Token: 0x0400002B RID: 43
		private int m_total;

		// Token: 0x0400002C RID: 44
		private int m_pagesize = 20;

		// Token: 0x0400002D RID: 45
		private int m_pagecount;

		// Token: 0x0400002E RID: 46
		private int m_nextpage;

		// Token: 0x0400002F RID: 47
		private int m_prepage;

		// Token: 0x04000030 RID: 48
		private int m_extpage = 5;

		// Token: 0x04000031 RID: 49
		private int m_startpage = 1;

		// Token: 0x04000032 RID: 50
		private int m_endpage = 1;

		// Token: 0x04000033 RID: 51
		private string m_pagenum = "";
	}
}
