using System;
using System.IO;
using System.Web;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Config;

namespace FangPage.WMS
{
	// Token: 0x02000003 RID: 3
	public class HttpModule : IHttpModule
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002835 File Offset: 0x00000A35
		public void Init(HttpApplication context)
		{
			context.BeginRequest += this.ReUrl_BeginRequest;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000284C File Offset: 0x00000A4C
		private void ReUrl_BeginRequest(object sender, EventArgs e)
		{
			this.webpath = WebConfig.WebPath;
			this.sysconfig = SysConfigs.GetConfig();
			HttpContext context = ((HttpApplication)sender).Context;
			string text = context.Request.Path.ToLower();
			if (text.StartsWith(this.webpath) && Path.GetExtension(text).ToLower() == ".aspx")
			{
				text = (text.StartsWith("/") ? text : ("/" + text));
				string text2 = text.Substring(this.webpath.Length);
				if (text.Substring(this.webpath.Length).IndexOf("/") == -1)
				{
					if (WebConfig.SitePath == "")
					{
						context.RewritePath(this.webpath + "common/index/" + text2, string.Empty, context.Request.QueryString.ToString());
						return;
					}
					text2 = ((WebConfig.SitePath == "") ? "" : (WebConfig.SitePath + "/")) + text2;
				}
				else if (!Directory.Exists(FPFile.GetMapPath(this.webpath + text2.Substring(0, text2.IndexOf("/")))))
				{
					text2 = ((WebConfig.SitePath == "") ? "" : (WebConfig.SitePath + "/")) + text2;
				}
				if (text2.StartsWith("sites/"))
				{
					text2 = text2.Substring(6, text2.Length - 6);
				}
				if (this.sysconfig.browsecreatesite == 1)
				{
					string text3 = "";
					if (text2.IndexOf("/") >= 0)
					{
						text3 = text2.Substring(0, text2.IndexOf("/"));
					}
					SiteConfig siteInfo = SiteConfigs.GetSiteInfo(text3);
					string text4 = text3;
					if (text3 == "app")
					{
						text4 = text2.Substring(4, text2.Length - 4);
						if (text4.IndexOf("/") >= 0)
						{
							text4 = text4.Substring(0, text4.IndexOf("/"));
						}
						AppConfig appInfo = AppConfigs.GetAppInfo(text4);
						siteInfo.dll = appInfo.dll;
					}
					else if (text3 == "plugins")
					{
						text4 = text2.Substring(8, text2.Length - 8);
						if (text4.IndexOf("/") >= 0)
						{
							text4 = text4.Substring(0, text4.IndexOf("/"));
						}
						PluginConfig pluInfo = PluginConfigs.GetPluInfo(text4);
						siteInfo.dll = pluInfo.dll;
					}
					if (siteInfo.autocreate == 1)
					{
						if (text2.EndsWith("_ctrl.aspx") || (FPArray.Contain("app,plugins", text3) && !File.Exists(FPFile.GetMapPath(this.webpath + "sites/" + text2)) && !File.Exists(FPFile.GetMapPath(this.webpath + text2))))
						{
							if (string.IsNullOrEmpty(siteInfo.dll))
							{
								context.RewritePath(this.webpath + "common/error/controller.aspx", string.Empty, context.Request.QueryString.ToString());
								return;
							}
							string[] array = FPArray.SplitString(siteInfo.dll, 2);
							if (array[0].EndsWith(".Controller") && array[1] == "")
							{
								array[1] = array[0].Replace(".Controller", "");
							}
							if (array[1] == "")
							{
								array[1] = array[0];
							}
							if (File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + array[1] + ".dll")))
							{
								context.RewritePath(this.webpath + "common/error/controller.aspx", string.Empty, context.Request.QueryString.ToString());
							}
							if (this.CheckController(text3, text4, array[1], text2))
							{
								FPFile.WriteFile(FPFile.GetMapPath(this.webpath + text2), string.Format("<%@ Page Inherits=\"{0}.{1},{2}\" %>", array[0], Path.GetFileName(text2).Replace("_ctrl.aspx", "").Replace(".aspx", ""), array[1]));
							}
						}
						else
						{
							this.CreateTemplate(siteInfo, "sites/" + text2, text2);
							context.Response.ClearContent();
						}
					}
				}
				context.RewritePath(this.webpath + text2, string.Empty, context.Request.QueryString.ToString());
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002CD0 File Offset: 0x00000ED0
		private void CreateTemplate(SiteConfig siteinfo, string viewpath, string aspxpath)
		{
			if (!File.Exists(FPFile.GetMapPath(this.webpath + viewpath)))
			{
				return;
			}
			if (this.CheckView(viewpath, aspxpath))
			{
                FPViews.CreateView(siteinfo, viewpath, aspxpath, 1, "", out _, out _);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002D20 File Offset: 0x00000F20
		private bool CheckView(string viewpath, string aspxpath)
		{
			string mapPath = FPFile.GetMapPath(this.webpath + aspxpath);
			if (!File.Exists(mapPath))
			{
				return true;
			}
			DateTime lastWriteTime = File.GetLastWriteTime(FPFile.GetMapPath(this.webpath + viewpath));
			DateTime lastWriteTime2 = File.GetLastWriteTime(mapPath);
			if (lastWriteTime > lastWriteTime2)
			{
				return true;
			}
			ViewConfig viewInfo = ViewConfigs.GetViewInfo(viewpath);
			if (viewInfo.path == "")
			{
				return true;
			}
			foreach (string text in viewInfo.include.Split(new char[]
			{
				';'
			}))
			{
				if (File.GetLastWriteTime(FPFile.GetMapPath(this.webpath + text.TrimStart(new char[]
				{
					'/'
				}))) > lastWriteTime2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002DEC File Offset: 0x00000FEC
		private bool CheckController(string sitepath, string installpath, string dll, string aspxpath)
		{
			string mapPath = FPFile.GetMapPath(this.webpath + aspxpath);
			if (!File.Exists(mapPath))
			{
				return true;
			}
			DateTime lastWriteTime = File.GetLastWriteTime(mapPath);
			if (File.GetLastWriteTime(FPFile.GetMapPath(this.webpath + "bin/" + dll + ".dll")) > lastWriteTime)
			{
				return true;
			}
			string mapPath2;
			if (sitepath == "app")
			{
				mapPath2 = FPFile.GetMapPath(this.webpath + "app/" + installpath + "/app.config");
			}
			else if (sitepath == "plugins")
			{
				mapPath2 = FPFile.GetMapPath(this.webpath + "plugins/" + installpath + "/plugin.config");
			}
			else
			{
				mapPath2 = FPFile.GetMapPath(this.webpath + "sites/" + installpath + "/site.config");
				if (!File.Exists(mapPath2))
				{
					mapPath2 = FPFile.GetMapPath(this.webpath + installpath + "/site.config");
				}
			}
			return File.Exists(mapPath2) && File.GetLastWriteTime(mapPath2) > lastWriteTime;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002EF9 File Offset: 0x000010F9
		public void Dispose()
		{
		}

		// Token: 0x0400001D RID: 29
		private string webpath = "";

		// Token: 0x0400001E RID: 30
		private SysConfig sysconfig;
	}
}
