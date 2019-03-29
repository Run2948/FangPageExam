using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS
{
	// Token: 0x02000006 RID: 6
	public class WMSController : FPController
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002374 File Offset: 0x00000574
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.sysconfig.adminpath != "")
			{
				this.adminpath = this.webpath + this.sysconfig.adminpath + "/";
			}
			if (this.Session["FP_OLUSERINFO"] != null)
			{
				if (!this.IsTimeOut())
				{
					this.user = (this.Session["FP_OLUSERINFO"] as UserInfo);
					this.role = (this.Session["FP_ROLEINFO"] as RoleInfo);
					this.permlist = (this.Session["FP_PERMISSION"] as List<Permission>);
				}
				else
				{
					this.user = UserBll.CreateGuestUser();
					this.Session["FP_OLUSERINFO"] = this.user;
					this.role = RoleBll.GetRoleInfo(this.user.roleid);
					this.Session["FP_ROLEINFO"] = this.role;
					this.permlist = new PermissionBll().GetPermissionList(this.role.permission);
					this.Session["FP_PERMISSION"] = this.permlist;
				}
			}
			else
			{
				this.user = UserBll.GetOnlineUser(this.sysconfig.passwordkey, this.sysconfig.onlinetimeout);
				this.Session["FP_OLUSERINFO"] = this.user;
				this.role = RoleBll.GetRoleInfo(this.user.roleid);
				this.Session["FP_ROLEINFO"] = this.role;
				this.permlist = new PermissionBll().GetPermissionList(this.role.permission);
				this.Session["FP_PERMISSION"] = this.permlist;
			}
			this.userid = this.user.id;
			this.roleid = this.user.roleid;
			this.departid = this.user.departid;
			this.username = this.user.username;
			if (this.role.desktopurl == "")
			{
				this.role.desktopurl = "main.aspx";
			}
			UserBll.UpdateOnlineState(this.userid, this.sysconfig.onlinetimeout, this.sysconfig.onlinefrequency);
			this.iscuserr = FPUtils.InArray(this.cururl, this.sysconfig.customerrors);
			this.permission = this.GetPermission(this.cururl);
			if (this.roleid == 1 || this.permission.id > 0)
			{
				this.isperm = true;
			}
			if (this.roleid == 1 || this.role.isadmin == 1)
			{
				this.isadmin = true;
			}
			if (this.roleid != 1 && this.siteconfig.closed == 1 && this.pagename != "login.aspx" && this.pagename != "logout.aspx")
			{
				MessageBox.Show(this.siteconfig.closedreason);
			}
			if (this.roleid != 1 && this.siteconfig.ipaccess.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx" && this.ip != "127.0.0.1")
			{
				string[] iparray = FPUtils.SplitString(this.siteconfig.ipaccess, "\n");
				if (!FPUtils.InIPArray(this.ip, iparray))
				{
					MessageBox.Show("抱歉，系统设置了IP访问限制，您所在的IP地址无权访问本站点。");
				}
			}
			if (this.roleid != 1 && this.siteconfig.ipdenyaccess.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx" && this.ip != "127.0.0.1")
			{
				string[] iparray = FPUtils.SplitString(this.siteconfig.ipdenyaccess, "\n");
				if (FPUtils.InIPArray(this.ip, iparray))
				{
					MessageBox.Show("对不起，您所在的IP地址已被禁止访问本站点。");
				}
			}
			if (this.roleid != 1 && this.siteconfig.roles.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx")
			{
				if (!FPUtils.InArray(this.roleid, this.siteconfig.roles))
				{
					MessageBox.Show("对不起，您没有权限访问该站点。");
				}
			}
			this.isseccode = FPUtils.InArray(this.cururl, this.sysconfig.verifypage.Replace("|", ","));
			if (this.isseccode)
			{
				this.isvalid = (string.Compare(this.Session["FP_VERIFY"].ToString().ToLower(), FPRequest.GetString("verify").ToLower(), true) != 0);
			}
			this.isvip = (this.user.vipdays > 0);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002914 File Offset: 0x00000B14
		protected virtual void ShowErr()
		{
			if (!this.iscuserr)
			{
				MessageBox.Show(this.msg, "错误提示");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002940 File Offset: 0x00000B40
		protected virtual void ShowErr(string message)
		{
			base.AddErr(message);
			if (!this.iscuserr)
			{
				MessageBox.Show(this.msg, "错误提示");
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002974 File Offset: 0x00000B74
		protected Permission GetPermission(string pageurl)
		{
			Permission permission = new Permission();
			foreach (Permission permission2 in this.permlist)
			{
				if (FPUtils.InArray(pageurl, permission2.flagpage, "\r\n"))
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

		// Token: 0x06000014 RID: 20 RVA: 0x00002A80 File Offset: 0x00000C80
		private bool IsTimeOut()
		{
			DateTime lastCookieTime = WMSCookie.GetLastCookieTime();
			DateTime t = DateTime.Now.AddMinutes((double)(this.sysconfig.onlinetimeout * -1));
			return !(lastCookieTime >= t);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002ACA File Offset: 0x00000CCA
		protected void ResetUser()
		{
			this.Session["FP_OLUSERINFO"] = null;
			this.Session.Abandon();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002AEB File Offset: 0x00000CEB
		protected void SaveLeftURL(string value)
		{
			this.Session["FP_ADMIN_LEFTMENU"] = value;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B00 File Offset: 0x00000D00
		protected void SaveLeftURL()
		{
			this.SaveLeftURL(this.rawurl);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B10 File Offset: 0x00000D10
		protected void SaveRightURL(string value)
		{
			this.Session["FP_ADMIN_RIGHTMENU"] = value;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002B25 File Offset: 0x00000D25
		protected void SaveRightURL()
		{
			this.SaveRightURL(this.rawurl);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B35 File Offset: 0x00000D35
		protected void SaveTopURL(int value)
		{
			this.Session["FP_ADMIN_TOPMENU"] = value;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002B50 File Offset: 0x00000D50
		protected string UploadImg(HttpPostedFile postfile, string imgfile, bool isthumbnail, bool iswatermark, int imgmaxwidth, int imgmaxheight)
		{
			UpLoad upLoad = new UpLoad();
			string json = upLoad.FileSaveAs(postfile, "image", this.user, isthumbnail, iswatermark, imgmaxwidth, imgmaxheight);
			JsonData jsonData = JsonMapper.ToObject(json);
			if (jsonData["error"].ToString() == "")
			{
				if (imgfile != "")
				{
					if (File.Exists(FPUtils.GetMapPath(imgfile)))
					{
						File.Delete(FPUtils.GetMapPath(imgfile));
					}
				}
				imgfile = jsonData["filename"].ToString();
			}
			return imgfile;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002BFC File Offset: 0x00000DFC
		protected string UploadImg(HttpPostedFile postfile, string imgfile, int imgmaxwidth, int imgmaxheight)
		{
			return this.UploadImg(postfile, imgfile, false, false, imgmaxwidth, imgmaxheight);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C1C File Offset: 0x00000E1C
		protected string UploadImg(HttpPostedFile postfile, string imgfile)
		{
			return this.UploadImg(postfile, imgfile, false, false, 0, 0);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C3C File Offset: 0x00000E3C
		protected string GetSortNav(SortInfo sortinfo, string url)
		{
			string text = "";
			if (url.IndexOf("?") > 0)
			{
				url += "&";
			}
			else
			{
				url += "?";
			}
			url = this.rawpath + "/" + url;
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, sortinfo.parentlist);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				sqlParam
			});
			foreach (int num in FPUtils.SplitInt(sortinfo.parentlist))
			{
				if (num != 0)
				{
					foreach (SortInfo sortInfo in list)
					{
						if (sortInfo.id == num)
						{
							if (text != "")
							{
								text += "|";
							}
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								sortInfo.name,
								",",
								url,
								"channelid=",
								sortInfo.channelid,
								"&sortid=",
								sortInfo.id
							});
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002DFC File Offset: 0x00000FFC
		public static bool isnew(DateTime postdatetime, int days)
		{
			bool result;
			try
			{
				if (days >= 0)
				{
					string text = DateTime.Now.ToString("yyyy-MM-dd");
					string text2 = Convert.ToDateTime(postdatetime).ToString("yyyy-MM-dd");
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

		// Token: 0x04000005 RID: 5
		protected SysConfig sysconfig = SysConfigs.GetConfig();

		// Token: 0x04000006 RID: 6
		protected int userid = 0;

		// Token: 0x04000007 RID: 7
		protected int roleid = 0;

		// Token: 0x04000008 RID: 8
		protected int departid = 0;

		// Token: 0x04000009 RID: 9
		protected UserInfo user;

		// Token: 0x0400000A RID: 10
		protected RoleInfo role = new RoleInfo();

		// Token: 0x0400000B RID: 11
		protected int permid = 0;

		// Token: 0x0400000C RID: 12
		protected Permission permission = new Permission();

		// Token: 0x0400000D RID: 13
		protected List<Permission> permlist = new List<Permission>();

		// Token: 0x0400000E RID: 14
		protected internal string username = "";

		// Token: 0x0400000F RID: 15
		protected bool isperm = false;

		// Token: 0x04000010 RID: 16
		protected bool isadmin = false;

		// Token: 0x04000011 RID: 17
		protected bool isseccode = false;

		// Token: 0x04000012 RID: 18
		protected bool isvalid = false;

		// Token: 0x04000013 RID: 19
		protected bool iscuserr = false;

		// Token: 0x04000014 RID: 20
		protected bool isvip = false;

		// Token: 0x04000015 RID: 21
		protected string wmsver = "4.7";

		// Token: 0x04000016 RID: 22
		protected string verdate = "2015";
	}
}
