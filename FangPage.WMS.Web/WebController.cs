using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web
{
	// Token: 0x02000005 RID: 5
	public class WebController : BaseController
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000025BC File Offset: 0x000007BC
		protected override void Init()
		{
			base.Init();
			if (this.session.errcode > 0)
			{
				MsgBox.Show(this.session.errmsg);
			}
			if (FPRequest.GetString("token") != "")
			{
				base.Response.Redirect(base.seturl("token="));
			}
			if (this.roleid != 1 && this.siteinfo.closed == 1 && this.pagename != "login.aspx" && this.pagename != "logout.aspx")
			{
				MsgBox.Show(this.siteinfo.closedreason);
			}
			if (this.roleid != 1 && this.siteinfo.ipaccess.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx" && this.ip != "127.0.0.1")
			{
				string[] iparray = FPArray.SplitString(this.siteinfo.ipaccess, "\n");
				if (!FPArray.InIPArray(this.ip, iparray))
				{
					MsgBox.Show("抱歉，系统设置了IP访问限制，您所在的IP地址无权访问本站点。");
				}
			}
			else if (this.roleid != 1 && this.siteinfo.ipdenyaccess.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx" && this.ip != "127.0.0.1")
			{
				string[] iparray2 = FPArray.SplitString(this.siteinfo.ipdenyaccess, "\n");
				if (FPArray.InIPArray(this.ip, iparray2))
				{
					MsgBox.Show("对不起，您所在的IP地址已被禁止访问本站点。");
				}
			}
			if (this.roleid != 1 && this.siteinfo.roles.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx")
			{
				if (this.roleid == 2)
				{
					base.Response.Redirect(this.webpath + "login.aspx");
				}
				if (FPArray.InArray(this.roleid, this.siteinfo.roles) == -1)
				{
					MsgBox.Show("对不起，您没有权限访问该站点。");
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002817 File Offset: 0x00000A17
		protected virtual void ShowErr(string message)
		{
			base.AddErr(message);
			if (!this.iscuserr)
			{
				MsgBox.Show(message, "错误提示", "返回");
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002838 File Offset: 0x00000A38
		protected virtual void ShowErr(string message, string btn)
		{
			base.AddErr(message);
			if (!this.iscuserr)
			{
				MsgBox.Show(this.msg, "错误提示", btn);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000285A File Offset: 0x00000A5A
		protected virtual void ShowErr(string message, string title, string btn)
		{
			base.AddErr(message);
			if (!this.iscuserr)
			{
				MsgBox.Show(this.msg, title, btn);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002878 File Offset: 0x00000A78
		protected void ResetUser()
		{
			this.Session["FP_OLUSERINFO"] = null;
			this.Session.Abandon();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002898 File Offset: 0x00000A98
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
			url = this.rawpath + url;
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, sortinfo.parentlist);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				sqlParam
			});
			foreach (int num in FPArray.SplitInt(sortinfo.parentlist))
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
							text = string.Concat(new object[]
							{
								text,
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
	}
}
