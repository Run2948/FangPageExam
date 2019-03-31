using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Exam.Model;

namespace FP_Exam.Wap.WapController
{
	// Token: 0x02000007 RID: 7
	public class examhistory : LoginController
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002E9C File Offset: 0x0000109C
		protected override void Controller()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("uid", this.userid);
			this.examresultlist = DbHelper.ExecuteList<ExamResult>(this.pager, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002EDC File Offset: 0x000010DC
		protected override void Complete()
		{
			var o = new
			{
				errcode = 0,
				errmsg = "",
				html = this.ViewBuilder.ToString(),
				page = this.pager.pageindex,
				pagecount = this.pager.pagecount
			};
			FPResponse.WriteJson(o);
		}

		// Token: 0x04000012 RID: 18
		protected List<ExamResult> examresultlist = new List<ExamResult>();

		// Token: 0x04000013 RID: 19
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
