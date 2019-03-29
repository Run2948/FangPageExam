using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Web;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Task;

namespace FangPage.WMS
{
	// Token: 0x0200000A RID: 10
	public class HttpModule : IHttpModule
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000031EC File Offset: 0x000013EC
		public void Init(HttpApplication context)
		{
			context.BeginRequest += this.ReUrl_BeginRequest;
			if (HttpModule.eventTimer == null && SysConfigs.TaskInterval > 0)
			{
				HttpModule.eventTimer = new Timer(new TimerCallback(this.TasksEventWorkCallback), context.Context, 60000, SysConfigs.TaskInterval * 60000);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003258 File Offset: 0x00001458
		private void TasksEventWorkCallback(object sender)
		{
			try
			{
				if (SysConfigs.TaskInterval > 0)
				{
					TaskManager.Execute();
				}
			}
			catch (Exception ex)
			{
				TaskLog.WriteFailedLog("执行计划任务失败:" + ex.Message);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000032B0 File Offset: 0x000014B0
		private void ReUrl_BeginRequest(object sender, EventArgs e)
		{
			this.webpath = WebConfig.WebPath;
			this.sysconfig = SysConfigs.GetConfig();
			HttpContext context = ((HttpApplication)sender).Context;
			string text = context.Request.Path.ToLower();
			if (text.StartsWith(this.webpath))
			{
				if (Path.GetExtension(text).ToLower() == ".aspx")
				{
					text = (text.StartsWith("/") ? text : ("/" + text));
					string text2 = text.Substring(this.webpath.Length);
					if (text.Substring(this.webpath.Length).IndexOf("/") == -1)
					{
						text2 = ((WebConfig.SitePath == "") ? "" : (WebConfig.SitePath + "/")) + text2;
					}
					else if (!Directory.Exists(FPUtils.GetMapPath(this.webpath + text2.Substring(0, text2.IndexOf("/")))))
					{
						text2 = ((WebConfig.SitePath == "") ? "" : (WebConfig.SitePath + "/")) + text2;
					}
					if (text2.StartsWith("sites/"))
					{
						text2 = text2.Substring(6, text2.Length - 6);
					}
					string viewpath = "sites/" + text2;
					if (this.sysconfig.browsecreatesite == 1)
					{
						string sitepath = "";
						if (text2.IndexOf("/") >= 0)
						{
							sitepath = text2.Substring(0, text2.IndexOf("/"));
						}
						SiteConfig siteInfo = SiteConfigs.GetSiteInfo(sitepath);
						if (siteInfo.autocreate == 1)
						{
							this.includedata = FPViews.GetViewInclude();
							this.CreateTemplate(siteInfo, viewpath, text2);
							context.Response.ClearContent();
						}
					}
					context.RewritePath(this.webpath + text2, string.Empty, context.Request.QueryString.ToString());
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000034FC File Offset: 0x000016FC
		private void CreateTemplate(SiteConfig siteconfig, string viewpath, string aspxpath)
		{
			if (File.Exists(FPUtils.GetMapPath(this.webpath + viewpath)))
			{
				if (!this.CheckView(viewpath, aspxpath))
				{
					string text = "";
					string text2 = "";
					FPViews.CreateView(siteconfig, this.webpath, viewpath, aspxpath, 1, "", out text, out text2);
					if (this.includedata[viewpath] == null)
					{
						this.includedata.Add(viewpath, text);
						FPViews.AddViewInclude(viewpath, text);
					}
					else if (text != this.includedata[viewpath].ToString())
					{
						this.includedata[viewpath] = text;
						FPViews.UpdateViewInclude(viewpath, text);
					}
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000035C8 File Offset: 0x000017C8
		private bool CheckView(string viewpath, string aspxpath)
		{
			string mapPath = FPUtils.GetMapPath(this.webpath + aspxpath);
			bool result;
			if (!File.Exists(mapPath))
			{
				result = false;
			}
			else
			{
				string mapPath2 = FPUtils.GetMapPath(this.webpath + viewpath);
				DateTime lastWriteTime = File.GetLastWriteTime(mapPath2);
				DateTime lastWriteTime2 = File.GetLastWriteTime(mapPath);
				if (lastWriteTime > lastWriteTime2)
				{
					result = false;
				}
				else if (this.includedata[viewpath] == null)
				{
					result = false;
				}
				else
				{
					foreach (string str in this.includedata[viewpath].ToString().Split(new char[]
					{
						','
					}))
					{
						lastWriteTime = File.GetLastWriteTime(FPUtils.GetMapPath(this.webpath + str));
						if (lastWriteTime > lastWriteTime2)
						{
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000036D5 File Offset: 0x000018D5
		public void Dispose()
		{
			HttpModule.eventTimer = null;
		}

		// Token: 0x04000018 RID: 24
		private string webpath = "";

		// Token: 0x04000019 RID: 25
		private SysConfig sysconfig;

		// Token: 0x0400001A RID: 26
		private Hashtable includedata = new Hashtable();

		// Token: 0x0400001B RID: 27
		private static Timer eventTimer;
	}
}
