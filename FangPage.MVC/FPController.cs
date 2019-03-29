using System;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace FangPage.MVC
{
	// Token: 0x0200000D RID: 13
	public class FPController : Page, IRequiresSessionState
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00007D00 File Offset: 0x00005F00
		public FPController()
		{
			if (this.rawurl.IndexOf("/") >= 0)
			{
				this.rawpath = this.rawurl.Substring(0, this.rawurl.LastIndexOf("/"));
				this.rawpath += "/";
			}
			else
			{
				this.rawpath = this.webpath;
			}
			this.fullurl = this.rawurl.Substring(this.webpath.Length);
			if (this.fullurl.IndexOf("?") >= 0)
			{
				this.cururl = this.fullurl.Substring(0, this.fullurl.IndexOf("?"));
			}
			else
			{
				this.cururl = this.fullurl;
			}
			if (this.cururl.IndexOf("/") > 0)
			{
				this.curpath = this.cururl.Substring(0, this.cururl.LastIndexOf("/"));
				this.curpath += "/";
			}
			this.adminpath = this.webpath + "admin/";
			this.plupath = this.webpath + "plugins/";
			string[] array = this.fullurl.Split(new char[]
			{
				'/'
			});
			this.fullname = array[array.Length - 1];
			if (this.cururl.IndexOf("/") >= 0)
			{
				this.sitepath = this.cururl.Substring(0, this.cururl.IndexOf("/"));
			}
			else
			{
				this.sitepath = WebConfig.SitePath;
			}
			this.siteconfig = SiteConfigs.GetSiteInfo(this.sitepath);
			this.pagetitle = this.siteconfig.sitetitle;
			this.AddMetaSeo(this.siteconfig.keywords, this.siteconfig.description, this.siteconfig.otherhead);
			this.ispost = FPRequest.IsPost();
			this.isget = FPRequest.IsGet();
			this.isfile = FPRequest.IsPostFile();
			this.link = this.pagename;
			this.action = FPRequest.GetString("action").ToLower();
			this.browser = this.getBrowserName();
			this.isie = this.browser.StartsWith("ie");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000809C File Offset: 0x0000629C
		private string getBrowserName()
		{
			string text = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToLower();
			string result;
			if (text.IndexOf("msie 6") >= 0)
			{
				result = "ie6";
			}
			else if (text.IndexOf("msie 7") >= 0)
			{
				result = "ie7";
			}
			else if (text.IndexOf("msie 8") >= 0)
			{
				result = "ie8";
			}
			else if (text.IndexOf("msie 9") >= 0)
			{
				result = "ie9";
			}
			else if (text.IndexOf("msie 11") >= 0)
			{
				result = "ie11";
			}
			else if (text.IndexOf("chrome") >= 0)
			{
				result = "chrome";
			}
			else if (text.IndexOf("firefox") >= 0)
			{
				result = "firefox";
			}
			else if (text.IndexOf("opera") >= 0)
			{
				result = "opera";
			}
			else if (text.IndexOf("webkit") >= 0)
			{
				result = "webkit";
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000081D1 File Offset: 0x000063D1
		protected override void OnInitComplete(EventArgs e)
		{
			base.OnInitComplete(e);
			this.View();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000081E3 File Offset: 0x000063E3
		protected virtual void View()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000081E8 File Offset: 0x000063E8
		protected string GetThumbnail(string imgpath, int maxsize)
		{
			return FPThumb.GetThumbnail(imgpath, maxsize);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00008204 File Offset: 0x00006404
		protected void AddErr(string errinfo)
		{
			if (this.msg.Length == 0)
			{
				this.msg += errinfo;
			}
			else
			{
				this.msg = this.msg + "<br />" + errinfo;
			}
			this.err++;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00008264 File Offset: 0x00006464
		protected void AddMsg(string strinfo)
		{
			if (this.msg.Length == 0)
			{
				this.msg += strinfo;
			}
			else
			{
				this.msg = this.msg + "<br />" + strinfo;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000082B6 File Offset: 0x000064B6
		public void SetMetaRefresh()
		{
			this.SetMetaRefresh(2, this.pagename);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000082C7 File Offset: 0x000064C7
		public void SetMetaRefresh(int sec)
		{
			this.SetMetaRefresh(sec, "index.aspx");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000082D8 File Offset: 0x000064D8
		public void SetMetaRefresh(int sec, string url)
		{
			this.meta = string.Concat(new string[]
			{
				this.meta,
				"\r\n<meta http-equiv=\"refresh\" content=\"",
				sec.ToString(),
				"; url=",
				url,
				"\" />"
			});
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00008328 File Offset: 0x00006528
		public void AddMeta(string metastr)
		{
			this.meta = this.meta + "\r\n<meta " + metastr + " />";
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00008348 File Offset: 0x00006548
		private void AddMetaSeo(string Seokeywords, string Seodescription, string Otherhead)
		{
			if (Seokeywords != "")
			{
				this.meta = this.meta + "<meta name=\"keywords\" content=\"" + FPUtils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
			}
			if (Seodescription != "")
			{
				this.meta = this.meta + "<meta name=\"description\" content=\"" + FPUtils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
			}
			this.meta += Otherhead;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000083F0 File Offset: 0x000065F0
		public void UpdateSeoInfo(string Seokeywords, string Seodescription)
		{
			string[] array = FPUtils.SplitString(this.meta, "\r\n");
			this.meta = "";
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				if (text.ToLower().IndexOf("name=\"keywords\"") <= 0)
				{
					goto IL_A0;
				}
				if (Seokeywords == null || !(Seokeywords.Trim() != ""))
				{
					goto IL_A0;
				}
				this.meta = this.meta + "<meta name=\"keywords\" content=\"" + FPUtils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
				IL_129:
				i++;
				continue;
				IL_A0:
				if (text.ToLower().IndexOf("name=\"description\"") > 0)
				{
					if (Seodescription != null && Seodescription.Trim() != "")
					{
						this.meta = this.meta + "<meta name=\"description\" content=\"" + FPUtils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
						goto IL_129;
					}
				}
				this.meta = this.meta + text + "\r\n";
				goto IL_129;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000853C File Offset: 0x0000673C
		private void AddSeoInfo(string Seokeywords, string Seodescription)
		{
			string[] array = FPUtils.SplitString(this.meta, "\r\n");
			this.meta = "";
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				if (text.ToLower().IndexOf("name=\"keywords\"") <= 0)
				{
					goto IL_B5;
				}
				if (Seokeywords == null || !(Seokeywords.Trim() != ""))
				{
					goto IL_B5;
				}
				this.meta = this.meta + "<meta name=\"keywords\" content=\"" + FPUtils.RemoveHtml(Seokeywords + "," + this.siteconfig.keywords).Replace("\"", " ") + "\" />\r\n";
				IL_153:
				i++;
				continue;
				IL_B5:
				if (text.ToLower().IndexOf("name=\"description\"") > 0)
				{
					if (Seodescription != null && Seodescription.Trim() != "")
					{
						this.meta = this.meta + "<meta name=\"description\" content=\"" + FPUtils.RemoveHtml(this.siteconfig.description + "," + Seodescription).Replace("\"", " ") + "\" />\r\n";
						goto IL_153;
					}
				}
				this.meta = this.meta + text + "\r\n";
				goto IL_153;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000086AF File Offset: 0x000068AF
		public void AddLinkCss(string url)
		{
			this.link = this.link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />";
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000086CE File Offset: 0x000068CE
		public void AddScript(string scriptstr)
		{
			this.script = this.script + "\r\n<script type=\"text/javascript\">" + scriptstr + "</script>";
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000086F0 File Offset: 0x000068F0
		protected bool ischecked(int cid, string values)
		{
			return FPUtils.InArray(cid, values);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000870C File Offset: 0x0000690C
		protected bool ischecked(string cstr, string values)
		{
			return FPUtils.InArray(cstr, values);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00008728 File Offset: 0x00006928
		protected string plugins(string pluname)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (pluname.ToLower() == "editor")
			{
				stringBuilder.AppendFormat("<link rel=\"stylesheet\" href=\"{0}editor/themes/default/default.css\" />\r\n", this.plupath);
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}editor/kindeditor.js\"></script>\r\n", this.plupath);
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}editor/lang/zh_CN.js\"></script>", this.plupath);
			}
			else if (pluname.ToLower() == "ztree")
			{
				stringBuilder.AppendFormat("<link rel=\"stylesheet\" href=\"{0}ztree/zTreeStyle.css\" type=\"text/css\"/>\r\n", this.plupath);
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}ztree/jquery.ztree.core-3.5.js\"></script>\r\n", this.plupath);
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}ztree/jquery.ztree.excheck-3.5.js\"></script>", this.plupath);
			}
			else if (pluname.ToLower() == "calendar")
			{
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}calendar/WdatePicker.js\"></script>", this.plupath);
			}
			else if (pluname.IndexOf("-") > 0)
			{
				string text = pluname.Substring(0, pluname.IndexOf("-"));
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", string.Concat(new string[]
				{
					this.webpath,
					"plugins/",
					text,
					"/",
					pluname
				}));
			}
			else if (pluname.EndsWith(".js"))
			{
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", string.Concat(new string[]
				{
					this.webpath,
					"plugins/",
					pluname,
					"/",
					pluname
				}));
			}
			else
			{
				stringBuilder.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", string.Concat(new string[]
				{
					this.webpath,
					"plugins/",
					pluname,
					"/",
					pluname,
					".js"
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400001B RID: 27
		protected StringBuilder ViewBuilder = new StringBuilder();

		// Token: 0x0400001C RID: 28
		protected string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		// Token: 0x0400001D RID: 29
		protected string serverip = FPRequest.GetServerIP();

		// Token: 0x0400001E RID: 30
		protected string ip = FPRequest.GetIP();

		// Token: 0x0400001F RID: 31
		protected string webpath = WebConfig.WebPath;

		// Token: 0x04000020 RID: 32
		protected string sitepath = "";

		// Token: 0x04000021 RID: 33
		protected SiteConfig siteconfig = new SiteConfig();

		// Token: 0x04000022 RID: 34
		protected string domain = FPRequest.GetCurrentFullHost().ToLower();

		// Token: 0x04000023 RID: 35
		protected string rawurl = FPRequest.GetRawUrl();

		// Token: 0x04000024 RID: 36
		protected string rawpath = "";

		// Token: 0x04000025 RID: 37
		protected string fullurl = "";

		// Token: 0x04000026 RID: 38
		protected string cururl = "";

		// Token: 0x04000027 RID: 39
		protected string curpath = "";

		// Token: 0x04000028 RID: 40
		protected string adminpath = "";

		// Token: 0x04000029 RID: 41
		protected string plupath = "";

		// Token: 0x0400002A RID: 42
		protected string fullname = "";

		// Token: 0x0400002B RID: 43
		protected string pagename = FPRequest.GetPageName();

		// Token: 0x0400002C RID: 44
		protected string pagetitle = "";

		// Token: 0x0400002D RID: 45
		protected string meta = "";

		// Token: 0x0400002E RID: 46
		protected string link;

		// Token: 0x0400002F RID: 47
		protected string script;

		// Token: 0x04000030 RID: 48
		protected string pagenav = "";

		// Token: 0x04000031 RID: 49
		protected bool ispost;

		// Token: 0x04000032 RID: 50
		protected bool isget;

		// Token: 0x04000033 RID: 51
		protected bool isfile;

		// Token: 0x04000034 RID: 52
		protected int err = 0;

		// Token: 0x04000035 RID: 53
		protected string msg = "";

		// Token: 0x04000036 RID: 54
		protected string action = "";

		// Token: 0x04000037 RID: 55
		protected string browser = "";

		// Token: 0x04000038 RID: 56
		protected bool isie = false;
	}
}
