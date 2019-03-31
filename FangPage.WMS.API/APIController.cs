using System;
using FangPage.Common;
using FangPage.MVC;

namespace FangPage.WMS.API
{
	// Token: 0x02000004 RID: 4
	public class APIController : BaseController
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021C4 File Offset: 0x000003C4
		protected override void Init()
		{
			base.Init();
			this.iswrite = 1;
			if (this.session.errcode > 0)
			{
				this.WriteErr(this.session.errmsg);
			}
			if (this.roleid != 1 && this.siteinfo.closed == 1 && this.pagename != "login.aspx" && this.pagename != "logout.aspx")
			{
				this.WriteErr(this.siteinfo.closedreason);
			}
			if (this.roleid != 1 && this.siteinfo.ipaccess.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx" && this.ip != "127.0.0.1")
			{
				string[] iparray = FPArray.SplitString(this.siteinfo.ipaccess, "\n");
				if (!FPArray.InIPArray(this.ip, iparray))
				{
					this.WriteErr("抱歉，系统设置了IP访问限制，您所在的IP地址无权访问本站点。");
				}
			}
			else if (this.roleid != 1 && this.siteinfo.ipdenyaccess.Trim() != "" && this.pagename != "login.aspx" && this.pagename != "logout.aspx" && this.ip != "127.0.0.1")
			{
				string[] iparray2 = FPArray.SplitString(this.siteinfo.ipdenyaccess, "\n");
				if (FPArray.InIPArray(this.ip, iparray2))
				{
					this.WriteErr("对不起，您所在的IP地址已被禁止访问本站点。");
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
					this.WriteErr("对不起，您没有权限访问该站点。");
				}
			}
			this.isseccode = (FPArray.InArray(this.curname, this.sysconfig.verifypage, new string[]
			{
				",",
				";",
				"|"
			}) >= 0);
			if (this.isseccode)
			{
				this.isvalid = (string.Compare(this.Session["FP_VERIFY"].ToString().ToLower(), FPRequest.GetString("verify").ToLower(), true) == 0);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000247F File Offset: 0x0000067F
		protected void WriteErr(string errmsg)
		{
			base.AddErr(errmsg);
			FPResponse.WriteJson(new
			{
				errcode = 110,
				errmsg = errmsg
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002495 File Offset: 0x00000695
		protected void WriteErr(int errcode, string errmsg)
		{
			base.AddErr(errmsg);
			FPResponse.WriteJson(new
			{
				errcode,
				errmsg
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024AA File Offset: 0x000006AA
		protected void WriteSuccess()
		{
			FPResponse.WriteJson(new
			{
				errcode = 0,
				errmsg = ""
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024BC File Offset: 0x000006BC
		protected void WriteSuccess(string errmsg)
		{
			FPResponse.WriteJson(new
			{
				errcode = 0,
				errmsg = errmsg
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024CA File Offset: 0x000006CA
		protected void ResetUser()
		{
			this.Session["FP_OLUSERINFO"] = null;
			this.Session.Abandon();
		}
	}
}
