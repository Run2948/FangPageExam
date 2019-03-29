using System;
using System.Collections;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.Exam.Model;
using LitJson;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000016 RID: 22
	public class questionajax : AdminController
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000B5A8 File Offset: 0x000097A8
		protected override void View()
		{
			if (this.ispost)
			{
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeSet("status", this.status),
					DbHelper.MakeAndWhere("id", this.qid)
				};
				DbHelper.ExecuteUpdate<ExamQuestion>(sqlparams);
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["status"] = this.status;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000B668 File Offset: 0x00009868
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x0400006C RID: 108
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x0400006D RID: 109
		protected int status = FPRequest.GetInt("status");
	}
}
