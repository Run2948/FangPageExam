using System;
using System.IO;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS.Controller
{
	// Token: 0x0200004A RID: 74
	public class avatar : LoginController
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		protected override void View()
		{
			this.iuser = DbHelper.ExecuteModel<UserInfo>(this.userid);
			if (this.iuser.id == 0)
			{
				this.ShowErr("用户不存在。");
			}
			else if (this.ispost)
			{
				if (this.isfile)
				{
					HttpPostedFile postedFile = FPRequest.Files["uploadimg"];
					UpLoad upLoad = new UpLoad();
					string json = upLoad.FileSaveAs(postedFile, "image", this.user, false, false, 100, 100);
					JsonData jsonData = JsonMapper.ToObject(json);
					if (jsonData["error"].ToString() == "")
					{
						if (this.iuser.avatar != "")
						{
							if (File.Exists(FPUtils.GetMapPath(this.iuser.avatar)))
							{
								File.Delete(FPUtils.GetMapPath(this.iuser.avatar));
							}
						}
						this.iuser.avatar = jsonData["filename"].ToString();
						DbHelper.ExecuteUpdate<UserInfo>(this.iuser);
						base.ResetUser();
						base.AddMsg("更新头像成功。");
					}
					else
					{
						this.ShowErr(jsonData["error"].ToString());
					}
				}
				else
				{
					this.ShowErr("请选择要上传的头像文件");
				}
			}
		}

		// Token: 0x0400014E RID: 334
		protected UserInfo iuser = new UserInfo();
	}
}
