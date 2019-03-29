using System;

namespace FangPage.WMS
{
	// Token: 0x02000009 RID: 9
	public class LoginController : WMSController
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003174 File Offset: 0x00001374
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.userid <= 0)
			{
				if (this.pagename == "index.aspx")
				{
					base.Response.Redirect("login.aspx");
				}
				else
				{
					this.ShowErr("对不起，您尚未登录或超时!");
					base.SetMetaRefresh(2, "login.aspx");
				}
			}
		}
	}
}
