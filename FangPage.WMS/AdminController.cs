using System;
using FangPage.MVC;

namespace FangPage.WMS
{
	// Token: 0x02000007 RID: 7
	public class AdminController : WMSController
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002F24 File Offset: 0x00001124
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.userid <= 0)
			{
				if (!(this.pagename == "index.aspx"))
				{
					this.ShowErr("对不起，您尚未登录或超时!");
					base.SetMetaRefresh(2, "login.aspx");
					return;
				}
				base.Response.Redirect("login.aspx");
			}
			if (!this.isadmin)
			{
				this.ShowErr("对不起，您没有权限访问后台!");
			}
			else if (FPRequest.GetInt("topmenuid") > 0)
			{
				base.SaveTopURL(FPRequest.GetInt("topmenuid"));
			}
		}
	}
}
