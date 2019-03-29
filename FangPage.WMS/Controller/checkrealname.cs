using System;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x0200004F RID: 79
	public class checkrealname : LoginController
	{
		// Token: 0x0600030D RID: 781 RVA: 0x0000C90C File Offset: 0x0000AB0C
		protected override void View()
		{
			this.fulluserinfo = DbHelper.ExecuteModel<FullUserInfo>(this.userid);
			if (this.user.isreal == 1 && this.fulluserinfo.idcard != "" && this.fulluserinfo.idcard.Length > 4)
			{
				this.fulluserinfo.idcard = "****" + this.fulluserinfo.idcard.Substring(this.fulluserinfo.idcard.Length - 4);
			}
			if (this.ispost)
			{
				this.fulluserinfo = FPRequest.GetModel<FullUserInfo>(this.fulluserinfo);
				if (FPRequest.GetString("realname") == "")
				{
					this.ShowErr("真实姓名不能为空。");
				}
				else if (this.fulluserinfo.idcard == "")
				{
					this.ShowErr("身份证号码不能为空。");
				}
				else if (this.fulluserinfo.idcard.Length > 20)
				{
					this.ShowErr("身份证号码不能大于20个字符");
				}
				else if (this.fulluserinfo.idcard.Trim() != "" && !Regex.IsMatch(this.fulluserinfo.idcard.Trim(), "^[\\x20-\\x80]+$"))
				{
					this.ShowErr("身份证号码中含有非法字符");
				}
				else
				{
					if (this.isfile)
					{
						this.fulluserinfo.idcardface = base.UploadImg(FPRequest.Files["idcardface"], this.fulluserinfo.idcardface);
						this.fulluserinfo.idcardback = base.UploadImg(FPRequest.Files["idcardback"], this.fulluserinfo.idcardback);
						this.fulluserinfo.idcardper = base.UploadImg(FPRequest.Files["idcardper"], this.fulluserinfo.idcardper);
					}
					if (this.fulluserinfo.idcardface == "")
					{
						this.ShowErr("请上传身份证正面图片。");
					}
					else if (this.fulluserinfo.idcardback == "")
					{
						this.ShowErr("请上传身份证背面图片。");
					}
					else if (this.fulluserinfo.idcardper == "")
					{
						this.ShowErr("请上传身份证手持图片。");
					}
					else
					{
						DbHelper.ExecuteUpdate<FullUserInfo>(this.fulluserinfo);
						base.ResetUser();
						base.AddMsg("您的身份信息已提交成功，等待管理员的审核。");
					}
				}
			}
		}

		// Token: 0x04000152 RID: 338
		protected FullUserInfo fulluserinfo = new FullUserInfo();
	}
}
