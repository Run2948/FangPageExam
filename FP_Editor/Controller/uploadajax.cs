using System;
using System.Collections;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Editor.Controller
{
	// Token: 0x02000003 RID: 3
	public class uploadajax : LoginController
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002470 File Offset: 0x00000670
		protected override void Controller()
		{
			if (this.roleid != 1 && this.role.isupload != 1)
			{
				this.ShowErrMsg("对不起，您所在的用户角色没有上传文件权限!");
				return;
			}
			this.EditorFile();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000249C File Offset: 0x0000069C
		private void EditorFile()
		{
			if (FPRequest.Files["imgfile"] == null)
			{
				this.ShowErrMsg("请选择要上传文件!");
				return;
			}
			string @string = FPRequest.GetString("attachid");
			string string2 = FPRequest.GetString("app");
			int @int = FPRequest.GetInt("postid");
			AttachInfo attachInfo = new AttachInfo();
			attachInfo = AttachBll.Upload(FPRequest.Files[0], @string, this.userid, string2, @int);
			if (attachInfo.error != "")
			{
				this.ShowErrMsg(attachInfo.error);
				return;
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["url"] = attachInfo.filename;
			hashtable["title"] = attachInfo.name;
			FPResponse.WriteJson(hashtable);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002563 File Offset: 0x00000763
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			FPResponse.WriteJson(hashtable);
		}
	}
}
