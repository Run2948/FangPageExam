using System;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web
{
	// Token: 0x02000003 RID: 3
	public class LoginController : WebController
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002300 File Offset: 0x00000500
		protected override void Init()
		{
			base.Init();
			if (this.userid <= 0)
			{
				if (File.Exists(FPFile.GetMapPath(this.webpath + this.sitepath + "/login.aspx")))
				{
					if (this.sitepath == WebConfig.SitePath)
					{
						this.backurl = this.webpath + "login.aspx";
					}
					else
					{
						this.backurl = this.webpath + this.sitepath + "/login.aspx";
					}
				}
				else
				{
					this.backurl = this.webpath + "login.aspx";
				}
				if (!(this.pagename == "index.aspx"))
				{
					base.SetMetaRefresh(2, this.backurl);
					this.ShowErr("对不起，您尚未登录或超时!", "登录|" + this.backurl);
					return;
				}
				base.Response.Redirect(this.backurl);
			}
			if (this.session.errcode > 0)
			{
				this.ShowErr(this.session.errmsg, "确定");
				return;
			}
			if (this.session.id > 0 && this.sysconfig.loginonce == 1 && this.session.state == 0)
			{
				SessionInfo userSession = SessionBll.GetUserSession(this.session.uid, this.session.platform);
				SessionBll.SessionLogout(this.session);
				WMSUtils.ClearUserCookie(this.sysconfig.platform, this.port);
				FPSession.Remove("FP_OLUSERINFO");
				FPSession.Remove("FP_PERMISSION");
				if (File.Exists(FPFile.GetMapPath(this.webpath + this.sitepath + "/login.aspx")))
				{
					if (this.sitepath == WebConfig.SitePath)
					{
						this.backurl = this.webpath + "login.aspx";
					}
					else
					{
						this.backurl = this.webpath + this.sitepath + "/login.aspx";
					}
				}
				else
				{
					this.backurl = this.webpath + "login.aspx";
				}
				if (userSession.id > 0)
				{
					this.ShowErr(string.Concat(new string[]
					{
						"您的帐号已在别的地方(IP:",
						userSession.address,
						"，时间:",
						userSession.createtime.ToString("yyyy-MM-dd HH:mm:ss"),
						")登录！如果不是您本人登录，请更改密码。"
					}), "确定|" + this.backurl);
					return;
				}
				this.ShowErr("您的帐号已在别的地方登录！如果不是您本人登录，请更改密码。", "确定|" + this.backurl);
			}
		}
	}
}
