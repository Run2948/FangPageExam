using System;
using System.Web;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x02000007 RID: 7
	public class avatar : LoginController
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002BB4 File Offset: 0x00000DB4
		protected override void Controller()
		{
			if (!this.ispost)
			{
				return;
			}
			if (!this.isfile)
			{
				this.ShowErr("请选择要上传的头像文件", "返回");
				return;
			}
			HttpPostedFile postedFile = FPRequest.Files["uploadimg"];
			this.msg = UserBll.UploadAvatar(postedFile, 150, 150, this.userid);
			if (this.msg == "")
			{
				base.ResetUser();
				base.AddMsg("更新头像成功。");
				return;
			}
			this.ShowErr(this.msg, "返回");
		}
	}
}
