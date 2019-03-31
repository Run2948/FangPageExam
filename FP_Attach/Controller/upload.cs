using System;
using System.Collections;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Attach.Controller
{
	// Token: 0x0200000D RID: 13
	public class upload : LoginController
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000338C File Offset: 0x0000158C
		protected override void Controller()
		{
			if (this.isfile)
			{
				string @string = FPRequest.GetString("attachid");
				string string2 = FPRequest.GetString("app");
				int @int = FPRequest.GetInt("postid");
				AttachInfo attachInfo = new AttachInfo();
				attachInfo = AttachBll.Upload(FPRequest.Files[0], @string, this.userid, string2, @int);
				if (attachInfo.error != "")
				{
					base.WriteErr(attachInfo.error);
					return;
				}
				Hashtable hashtable = new Hashtable();
				hashtable["errcode"] = 0;
				hashtable["errmsg"] = "";
				hashtable["aid"] = attachInfo.id;
				hashtable["attachid"] = attachInfo.attachid;
				hashtable["name"] = attachInfo.name;
				hashtable["filename"] = attachInfo.filename;
				hashtable["filetype"] = attachInfo.filetype;
				hashtable["icon"] = attachInfo.icon;
				hashtable["size"] = attachInfo.size;
				FPResponse.WriteJson(hashtable);
			}
		}
	}
}
