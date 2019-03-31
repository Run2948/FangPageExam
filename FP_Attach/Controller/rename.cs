using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Model;

namespace FP_Attach.Controller
{
	// Token: 0x0200000C RID: 12
	public class rename : LoginController
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003284 File Offset: 0x00001484
		protected override void Controller()
		{
			if (this.filename == "")
			{
				base.WriteErr("重命名文件名不能为空。");
				return;
			}
			AttachInfo attachInfo = DbHelper.ExecuteModel<AttachInfo>(this.aid);
			if (attachInfo.id == 0)
			{
				base.WriteErr("该附件不存在或已被删除。");
				return;
			}
			if (Path.GetExtension(attachInfo.filename).ToLower() != Path.GetExtension(this.filename).ToLower())
			{
				this.filename = Path.GetFileNameWithoutExtension(this.filename) + Path.GetExtension(attachInfo.filename);
			}
			DbHelper.ExecuteUpdate<AttachInfo>(new SqlParam[]
			{
				DbHelper.MakeUpdate("name", this.filename),
				DbHelper.MakeAndWhere("id", this.aid)
			});
			FPResponse.WriteJson(new
			{
				errcode = 0,
				errmsg = "",
				aid = this.aid
			});
		}

		// Token: 0x0400001E RID: 30
		protected int aid = FPRequest.GetInt("aid");

		// Token: 0x0400001F RID: 31
		protected string filename = FPRequest.GetString("filename");
	}
}
