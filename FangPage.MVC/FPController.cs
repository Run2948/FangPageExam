using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x0200000E RID: 14
	public class FPController : Page
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000092B8 File Offset: 0x000074B8
		public FPController()
		{
			this.port = FPArray.SplitInt(this.domain, ":", 2)[1];
			if (this.rawurl.IndexOf("/") >= 0)
			{
				if (this.rawurl.IndexOf("?") >= 0)
				{
					this.rawpath = this.rawurl.Substring(0, this.rawurl.IndexOf("?"));
					this.rawpath = this.rawpath.Substring(0, this.rawpath.LastIndexOf("/")) + "/";
				}
				else
				{
					this.rawpath = this.rawurl.Substring(0, this.rawurl.LastIndexOf("/")) + "/";
				}
			}
			else
			{
				this.rawpath = this.webpath;
			}
			this.cururl = this.rawurl.Substring(this.webpath.Length);
			this.pageurl = this.pagename;
			if (this.cururl.Contains("?"))
			{
				this.curname = this.cururl.Substring(0, this.cururl.IndexOf("?"));
				this.query = this.cururl.Substring(this.cururl.IndexOf("?") + 1);
				this.pageurl = this.pagename + "?" + this.query;
			}
			else
			{
				this.curname = this.cururl;
			}
			if (this.curname.IndexOf("/") > 0)
			{
				this.curpath = this.curname.Substring(0, this.curname.LastIndexOf("/")) + "/";
			}
			if (this.curname.IndexOf("/") >= 0)
			{
				this.sitepath = this.curname.Substring(0, this.curname.IndexOf("/"));
			}
			else
			{
				this.sitepath = WebConfig.SitePath;
			}
			if (this.sitepath == "sites")
			{
				this.sitepath = this.curpath.Substring(this.curpath.IndexOf("/") + 1).TrimEnd(new char[]
				{
					'/'
				});
			}
			if (!Directory.Exists(FPFile.GetMapPath(this.webpath + this.sitepath)))
			{
				this.sitepath = WebConfig.SitePath;
			}
			this.pagepath = this.webpath + this.sitepath + "/";
			this.siteinfo = SiteConfigs.GetSiteInfo(this.sitepath);
			this.adminpath = this.webpath + "admin/";
			this.plupath = this.webpath + "plugins/";
			this.apppath = this.webpath + "app/";
			this.sitetitle = this.siteinfo.sitetitle;
			this.pagetitle = this.siteinfo.sitetitle;
			this.CreateSeoInfo(this.siteinfo.keywords, this.siteinfo.description, this.siteinfo.otherhead);
			this.ispost = FPRequest.IsPost();
			this.isget = FPRequest.IsGet();
			this.isfile = FPRequest.IsPostFile();
			this.action = FPRequest.GetString("action");
			this.op = FPRequest.GetInt("op");
			try
			{
				this.ua = HttpContext.Current.Request.UserAgent.ToLower();
			}
			catch
			{
			}
			this.browser = this.getBrowserName(this.ua, out this.isie);
			this.args = FPArray.SplitString(Path.GetFileNameWithoutExtension(this.pagename), "-");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00009818 File Offset: 0x00007A18
		protected override void OnPreInit(EventArgs e)
		{
			this.Init();
			this.Controller();
			this.PreView();
			this.View();
			this.Complete();
			HttpContext.Current.Response.End();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009847 File Offset: 0x00007A47
		protected new virtual void Init()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009847 File Offset: 0x00007A47
		protected virtual void Controller()
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00009847 File Offset: 0x00007A47
		protected virtual void PreView()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00009847 File Offset: 0x00007A47
		protected virtual void View()
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00009847 File Offset: 0x00007A47
		protected virtual void Complete()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000984C File Offset: 0x00007A4C
		private string getBrowserName(string ua, out int iever)
		{
			iever = 0;
			if (string.IsNullOrEmpty(ua))
			{
				return "";
			}
			if (ua.IndexOf("msie 6") >= 0)
			{
				iever = 6;
				return "ie6";
			}
			if (ua.IndexOf("msie 7") >= 0)
			{
				iever = 7;
				return "ie7";
			}
			if (ua.IndexOf("msie 8") >= 0)
			{
				iever = 8;
				return "ie8";
			}
			if (ua.IndexOf("msie 9") >= 0)
			{
				iever = 9;
				return "ie9";
			}
			if (ua.IndexOf("msie 10") >= 0)
			{
				iever = 10;
				return "ie10";
			}
			if (ua.IndexOf("trident/7.0; rv:11.0") >= 0)
			{
				iever = 11;
				return "ie11";
			}
			if (ua.IndexOf("edge/") >= 0)
			{
				iever = 12;
				return "ie12";
			}
			if (ua.IndexOf("chrome") >= 0)
			{
				return "chrome";
			}
			if (ua.IndexOf("firefox") >= 0)
			{
				return "firefox";
			}
			if (ua.IndexOf("opera") >= 0)
			{
				return "opera";
			}
			if (ua.IndexOf("webkit") >= 0)
			{
				return "webkit";
			}
			return ua;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009960 File Offset: 0x00007B60
		protected string GetThumbnail(string imgpath, int maxsize)
		{
			return FPThumb.GetThumbnail(imgpath, maxsize);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000996C File Offset: 0x00007B6C
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000099BF File Offset: 0x00007BBF
		protected void AddMsg(string strinfo)
		{
			if (this.msg.Length == 0)
			{
				this.msg += strinfo;
				return;
			}
			this.msg = this.msg + "<br />" + strinfo;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000099F8 File Offset: 0x00007BF8
		public void SetMetaRefresh()
		{
			this.SetMetaRefresh(2, this.pagename);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00009A07 File Offset: 0x00007C07
		public void SetMetaRefresh(int sec)
		{
			this.SetMetaRefresh(sec, "index.aspx");
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009A18 File Offset: 0x00007C18
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

		// Token: 0x060000BB RID: 187 RVA: 0x00009A65 File Offset: 0x00007C65
		public void AddMeta(string metastr)
		{
			this.meta = this.meta + "\r\n<meta " + metastr + " />";
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009A84 File Offset: 0x00007C84
		private void CreateSeoInfo(string Seokeywords, string Seodescription, string Otherhead)
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

		// Token: 0x060000BD RID: 189 RVA: 0x00009B20 File Offset: 0x00007D20
		public void UpdateSeoInfo(string Seokeywords, string Seodescription)
		{
			string[] array = FPArray.SplitString(this.meta, "\r\n");
			this.meta = "";
			foreach (string text in array)
			{
				if (text.ToLower().IndexOf("name=\"keywords\"") > 0 && Seokeywords != null && Seokeywords.Trim() != "")
				{
					this.meta = this.meta + "<meta name=\"keywords\" content=\"" + FPUtils.RemoveHtml(Seokeywords).Replace("\"", " ") + "\" />\r\n";
				}
				else if (text.ToLower().IndexOf("name=\"description\"") > 0 && Seodescription != null && Seodescription.Trim() != "")
				{
					this.meta = this.meta + "<meta name=\"description\" content=\"" + FPUtils.RemoveHtml(Seodescription).Replace("\"", " ") + "\" />\r\n";
				}
				else
				{
					this.meta = this.meta + text + "\r\n";
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009C2C File Offset: 0x00007E2C
		private void AddSeoInfo(string Seokeywords, string Seodescription)
		{
			string[] array = FPArray.SplitString(this.meta, "\r\n");
			this.meta = "";
			foreach (string text in array)
			{
				if (text.ToLower().IndexOf("name=\"keywords\"") > 0 && Seokeywords != null && Seokeywords.Trim() != "")
				{
					this.meta = this.meta + "<meta name=\"keywords\" content=\"" + FPUtils.RemoveHtml(Seokeywords + "," + this.siteinfo.keywords).Replace("\"", " ") + "\" />\r\n";
				}
				else if (text.ToLower().IndexOf("name=\"description\"") > 0 && Seodescription != null && Seodescription.Trim() != "")
				{
					this.meta = this.meta + "<meta name=\"description\" content=\"" + FPUtils.RemoveHtml(this.siteinfo.description + "," + Seodescription).Replace("\"", " ") + "\" />\r\n";
				}
				else
				{
					this.meta = this.meta + text + "\r\n";
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009D65 File Offset: 0x00007F65
		public void AddLinkCss(string url)
		{
			this.link = this.link + "\r\n<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />";
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009D83 File Offset: 0x00007F83
		public void AddScript(string script_str)
		{
			this.script = this.script + "\r\n<script type=\"text/javascript\">" + script_str + "</script>";
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009DA1 File Offset: 0x00007FA1
		public void AddLinkScript(string script_src)
		{
			this.script = this.script + "\r\n<script type=\"text/javascript\" src=\"" + script_src + "\"></script>";
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009DC0 File Offset: 0x00007FC0
		protected string seturl(string param)
		{
			string text = "";
			param = param.Trim();
			if (this.query != "")
			{
				string text2 = "";
				foreach (string text3 in FPArray.SplitString(this.query, "&"))
				{
					bool flag = true;
					string[] array2 = FPArray.SplitString(param, "&");
					for (int j = 0; j < array2.Length; j++)
					{
						string[] array3 = FPArray.SplitString(array2[j], "=", 2);
						if (text3.StartsWith(array3[0] + "="))
						{
							flag = false;
						}
					}
					if (flag)
					{
						text2 = FPArray.Push(text2, text3, "&");
					}
				}
				string text4 = "";
				foreach (string text5 in FPArray.SplitString(param, "&"))
				{
					string[] array4 = FPArray.SplitString(text5, "=", 2);
					if (array4[1] != "" && array4[1] != "0")
					{
						text4 = FPArray.Push(text4, text5, "&");
					}
				}
				text = this.pagename;
				if (text2 != "" || text4 != "")
				{
					text += "?";
				}
				if (text2 != "")
				{
					if (text4 != "")
					{
						text = text + text2 + "&" + text4;
					}
					else
					{
						text += text2;
					}
				}
				else if (text4 != "")
				{
					text += text4;
				}
			}
			else
			{
				string text6 = "";
				foreach (string text7 in param.Trim().Split(new char[]
				{
					'&'
				}))
				{
					string[] array5 = FPArray.SplitString(text7, "=", 2);
					if (array5[1] != "" && array5[1] != "0")
					{
						text6 = FPArray.Push(text6, text7, "&");
					}
				}
				if (text6 != "")
				{
					text = this.pagename + "?" + text6;
				}
			}
			return text;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A004 File Offset: 0x00008204
		public static bool isnew(DateTime postdatetime, int days)
		{
			bool result;
			try
			{
				if (days >= 0)
				{
					DateTime.Now.ToString("yyyy-MM-dd");
					Convert.ToDateTime(postdatetime).ToString("yyyy-MM-dd");
					result = (DateTime.Now.Subtract(postdatetime).TotalDays < (double)days);
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A074 File Offset: 0x00008274
		public static string echo(string obj)
		{
			if (string.IsNullOrEmpty(obj))
			{
				return "";
			}
			return obj;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A085 File Offset: 0x00008285
		public static string echo(string[] array)
		{
			return FPController.echo(FPArray.Join(array));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000A092 File Offset: 0x00008292
		public static string echo(string obj, int len)
		{
			if (!string.IsNullOrEmpty(obj))
			{
				return FPUtils.CutString(obj, len);
			}
			return obj;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000A0A5 File Offset: 0x000082A5
		public static string echo(int obj)
		{
			return obj.ToString();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000A0AE File Offset: 0x000082AE
		public static string echo(int[] array)
		{
			return FPController.echo(FPArray.Join(array));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000A0BB File Offset: 0x000082BB
		public static string echo(DateTime obj)
		{
			return FPUtils.FormatDateTime(obj);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000A0C3 File Offset: 0x000082C3
		public static string echo(DateTime obj, string format)
		{
			return FPUtils.FormatDateTime(obj, format);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A0CC File Offset: 0x000082CC
		public static string echo(string obj, string format)
		{
			return FPUtils.FormatDateTime(obj, format);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A0D5 File Offset: 0x000082D5
		public static string echo(DateTime? obj)
		{
			return FPUtils.FormatDateTime(obj);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A0DD File Offset: 0x000082DD
		public static string echo(DateTime? obj, string fmstr)
		{
			return FPUtils.FormatDateTime(obj, fmstr);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000A0E6 File Offset: 0x000082E6
		public static string echo(object obj)
		{
			if (obj != null)
			{
				return obj.ToString().Trim();
			}
			return "";
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000A0FC File Offset: 0x000082FC
		public static string echo(string obj, string oldStr, string newStr)
		{
			if (string.IsNullOrEmpty(obj))
			{
				return "";
			}
			if (string.IsNullOrEmpty(oldStr))
			{
				return obj;
			}
			string[] array = FPArray.SplitString(oldStr, "|");
			string[] array2 = FPArray.SplitString(newStr, "|", array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != "")
				{
					obj = obj.Replace(array[i], array2[i]);
				}
			}
			return obj;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A168 File Offset: 0x00008368
		public static string echo(string obj, string oldStr, int newStr)
		{
			return FPController.echo(obj, oldStr, newStr.ToString());
		}

		// Token: 0x04000028 RID: 40
		protected StringBuilder ViewBuilder = new StringBuilder();

		// Token: 0x04000029 RID: 41
		protected int loop__id;

		// Token: 0x0400002A RID: 42
		protected string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		// Token: 0x0400002B RID: 43
		protected string serverip = FPRequest.GetServerIP();

		// Token: 0x0400002C RID: 44
		protected string iis = FPRequest.GetServerIIS();

		// Token: 0x0400002D RID: 45
		protected string ip = FPRequest.GetIP();

		// Token: 0x0400002E RID: 46
		protected string domain = FPRequest.GetDomain().ToLower();

		// Token: 0x0400002F RID: 47
		protected int port = FPRequest.GetPort();

		// Token: 0x04000030 RID: 48
		protected string random = FPRandom.CreateCode(4);

		// Token: 0x04000031 RID: 49
		protected string webpath = WebConfig.WebPath;

		// Token: 0x04000032 RID: 50
		protected string sitepath = "";

		// Token: 0x04000033 RID: 51
		protected SiteConfig siteinfo = new SiteConfig();

		// Token: 0x04000034 RID: 52
		protected string rawurl = FPRequest.GetRawUrl();

		// Token: 0x04000035 RID: 53
		protected string rawpath = "";

		// Token: 0x04000036 RID: 54
		protected string cururl = "";

		// Token: 0x04000037 RID: 55
		protected string curpath = "";

		// Token: 0x04000038 RID: 56
		protected string curname = "";

		// Token: 0x04000039 RID: 57
		protected string pagepath = "";

		// Token: 0x0400003A RID: 58
		protected string pageurl = "";

		// Token: 0x0400003B RID: 59
		protected string pagename = FPRequest.GetPageName();

		// Token: 0x0400003C RID: 60
		protected string sitetitle = "";

		// Token: 0x0400003D RID: 61
		protected string pagetitle = "";

		// Token: 0x0400003E RID: 62
		protected string query = "";

		// Token: 0x0400003F RID: 63
		protected string meta = "";

		// Token: 0x04000040 RID: 64
		protected string link = "";

		// Token: 0x04000041 RID: 65
		protected string script = "";

		// Token: 0x04000042 RID: 66
		protected string pagenav = "";

		// Token: 0x04000043 RID: 67
		protected string adminpath = "";

		// Token: 0x04000044 RID: 68
		protected string apppath = "";

		// Token: 0x04000045 RID: 69
		protected string plupath = "";

		// Token: 0x04000046 RID: 70
		protected string backurl = FPRequest.GetString("backurl");

		// Token: 0x04000047 RID: 71
		protected bool ispost;

		// Token: 0x04000048 RID: 72
		protected bool isget;

		// Token: 0x04000049 RID: 73
		protected bool isfile;

		// Token: 0x0400004A RID: 74
		protected int err;

		// Token: 0x0400004B RID: 75
		protected string msg = "";

		// Token: 0x0400004C RID: 76
		protected string action = "";

		// Token: 0x0400004D RID: 77
		protected int op;

		// Token: 0x0400004E RID: 78
		protected string ua = "";

		// Token: 0x0400004F RID: 79
		protected string browser = "";

		// Token: 0x04000050 RID: 80
		protected int isie;

		// Token: 0x04000051 RID: 81
		protected string[] args;

		// Token: 0x04000052 RID: 82
		protected int iswrite;
	}
}
