using System;
using FangPage.MVC;
using FangPage.WMS.Bll;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x02000007 RID: 7
	public class avatar : LoginController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002550 File Offset: 0x00000750
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("image");
				if (@string == "")
				{
					base.WriteErr("请选择图片");
					return;
				}
				this.msg = UserBll.UploadAvatar(@string, this.userid);
				if (this.msg == "")
				{
					base.ResetUser();
					base.WriteSuccess("更新头像成功");
					return;
				}
				base.WriteErr(this.msg);
			}
		}
	}
}
