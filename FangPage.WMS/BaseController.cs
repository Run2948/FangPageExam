using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000002 RID: 2
	public class BaseController : FPController
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override void Init()
		{
			base.Init();
			if (this.sysconfig.acctype == 0)
			{
				base.Response.AppendHeader("Access-Control-Allow-Methods", "OPTIONS,POST,GET");
				base.Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
				base.Response.AppendHeader("Access-Control-Allow-Origin", "*");
			}
			if (this.sysconfig.disableie != "" && FPArray.Contain(this.sysconfig.disableie, this.browser))
			{
				base.Response.WriteFile(FPFile.GetMapPath(this.webpath + "common/browser/browser.aspx"));
				base.Response.End();
			}
			if (this.sysconfig.adminpath != "")
			{
				this.adminpath = this.webpath + this.sysconfig.adminpath + "/";
			}
			if (this.sitepath.ToLower() == "app")
			{
				string text = this.rawpath.Substring(this.apppath.Length, this.rawpath.Length - this.apppath.Length);
				if (text.IndexOf("/") >= 0)
				{
					text = text.Substring(0, text.IndexOf("/"));
				}
				this.setupinfo = SetupBll.GetSetupInfo("app", text);
			}
			else if (this.sitepath.ToLower() == "plugins")
			{
				string text2 = this.rawpath.Substring(this.plupath.Length, this.rawpath.Length - this.plupath.Length);
				if (text2.IndexOf("/") >= 0)
				{
					text2 = text2.Substring(0, text2.IndexOf("/"));
				}
				this.setupinfo = SetupBll.GetSetupInfo("plugins", text2);
			}
			else
			{
				this.setupinfo = SetupBll.GetSetupInfo(this.siteinfo);
			}
			this.version = this.setupinfo.version;
			if (this.platform == "")
			{
				this.platform = this.sysconfig.platform;
			}
			if (this.token == "")
			{
				this.token = WMSUtils.GetCookie(this.platform, this.port, "token");
			}
			this.session = SessionBll.UpdateSession(this.platform, this.token, this.sysconfig.ssocheck, this.sysconfig.onlinetimeout, this.sysconfig.onlinefrequency, this.sysconfig.loginonce, this.ip, this.port);
			if (this.session.id > 0)
			{
				if (FPSession.Get("FP_OLUSERINFO") != null)
				{
					this.user = (FPSession.Get("FP_OLUSERINFO") as UserInfo);
				}
				else
				{
					this.user = UserBll.GetUserInfo(this.session.uid);
					WMSUtils.WriteUserCookie(this.platform, this.port, this.session.token, -1);
					FPSession.Insert("FP_OLUSERINFO", this.user);
				}
			}
			else
			{
				FPSession.Remove("FP_OLUSERINFO");
				this.user = UserBll.CreateGuestUser();
			}
			this.role = RoleBll.GetRoleInfo(this.session.roleid);
			if (FPSession.Get("FP_PERMISSION") != null)
			{
				this.permlist = (FPSession.Get("FP_PERMISSION") as List<Permission>);
			}
			else
			{
				this.permlist = PermissionBll.GetPermissionList(this.role.permission);
				FPSession.Insert("FP_PERMISSION", this.permlist);
			}
			this.userid = this.session.uid;
			this.roleid = this.session.roleid;
			this.departid = this.user.departid;
			this.username = this.session.username;
			this.realname = this.session.realname;
			this.departer = DepartmentBll.GetManageDepartList(this.username);
			this.departs = FPArray.Push(this.departer, this.departid);
			this.iscuserr = (FPArray.InArray(this.curname, this.sysconfig.customerrors, new string[]
			{
				",",
				";",
				"|"
			}) >= 0);
			this.permission = this.GetPermission(this.curname);
			if (this.roleid == 1 || this.permission.id > 0)
			{
				this.isperm = true;
			}
			if (this.roleid == 1 || this.role.isadmin == 1)
			{
				this.isadmin = true;
			}
			this.isseccode = (FPArray.InArray(this.curname, this.sysconfig.verifypage, new string[]
			{
				",",
				";",
				"|"
			}) >= 0);
			if (this.isseccode && FPSession.Get("FP_VERIFY") != null)
			{
				this.isvalid = (string.Compare(FPSession.Get("FP_VERIFY").ToString().ToLower(), FPRequest.GetString("verify").ToLower(), true) == 0);
			}
			this.isfree = DbBll.GetDbType();
			if (this.sysconfig.isfree > 1 && this.isfree == 0)
			{
				this.isfree = this.sysconfig.isfree;
			}
			if (this.siteinfo.sitepath == this.sysconfig.adminpath && this.siteinfo.sitetitle != this.sysconfig.admintitle && this.sysconfig.admintitle != "")
			{
				this.siteinfo.sitetitle = this.sysconfig.admintitle;
				this.pagetitle = this.siteinfo.sitetitle;
				FPSerializer.Save<SiteConfig>(this.siteinfo, FPFile.GetMapPath(this.webpath + this.siteinfo.sitepath + "/site.config"));
				if (File.Exists(FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath + "/site.config")))
				{
					FPSerializer.Save<SiteConfig>(this.siteinfo, FPFile.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath + "/site.config"));
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000026B0 File Offset: 0x000008B0
		protected Permission GetPermission(string cur_name)
		{
			Permission permission = new Permission();
			foreach (Permission permission2 in this.permlist)
			{
				if (FPArray.InArray(cur_name, permission2.flagpage, "\r\n") >= 0)
				{
					if (permission.id == 0)
					{
						permission.id = permission2.id;
					}
					if (permission.isadd == 0)
					{
						permission.isadd = permission2.isadd;
					}
					if (permission.isupdate == 0)
					{
						permission.isupdate = permission2.isupdate;
					}
					if (permission.isdelete == 0)
					{
						permission.isdelete = permission2.isdelete;
					}
				}
			}
			return permission;
		}

		// Token: 0x04000001 RID: 1
		protected SysConfig sysconfig = SysConfigs.GetConfig();

		// Token: 0x04000002 RID: 2
		protected SetupInfo setupinfo = new SetupInfo();

		// Token: 0x04000003 RID: 3
		protected string webplat = "";

		// Token: 0x04000004 RID: 4
		protected string platform = FPRequest.GetString("platform");

		// Token: 0x04000005 RID: 5
		protected string token = FPRequest.GetString("token");

		// Token: 0x04000006 RID: 6
		protected SessionInfo session = new SessionInfo();

		// Token: 0x04000007 RID: 7
		protected int userid;

		// Token: 0x04000008 RID: 8
		protected int roleid;

		// Token: 0x04000009 RID: 9
		protected int departid;

		// Token: 0x0400000A RID: 10
		protected string departer = "";

		// Token: 0x0400000B RID: 11
		protected string departs = "";

		// Token: 0x0400000C RID: 12
		protected UserInfo user;

		// Token: 0x0400000D RID: 13
		protected RoleInfo role = new RoleInfo();

		// Token: 0x0400000E RID: 14
		protected int permid;

		// Token: 0x0400000F RID: 15
		protected Permission permission = new Permission();

		// Token: 0x04000010 RID: 16
		protected List<Permission> permlist = new List<Permission>();

		// Token: 0x04000011 RID: 17
		protected internal string username = "";

		// Token: 0x04000012 RID: 18
		protected internal string realname = "";

		// Token: 0x04000013 RID: 19
		protected bool isperm;

		// Token: 0x04000014 RID: 20
		protected bool isadmin;

		// Token: 0x04000015 RID: 21
		protected bool isseccode;

		// Token: 0x04000016 RID: 22
		protected bool isvalid;

		// Token: 0x04000017 RID: 23
		protected bool iscuserr;

		// Token: 0x04000018 RID: 24
		protected bool isvip;

		// Token: 0x04000019 RID: 25
		protected string wmsver = "7.4.1";

		// Token: 0x0400001A RID: 26
		protected int verdate = 2018;

		// Token: 0x0400001B RID: 27
		protected string version = "";

		// Token: 0x0400001C RID: 28
		protected int isfree;
	}
}
