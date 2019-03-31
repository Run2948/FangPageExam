using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Exam.Model;

namespace FP_Exam.Wap.WapController
{
	// Token: 0x0200000B RID: 11
	public class saveusertime : LoginController
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000035F0 File Offset: 0x000017F0
		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				ExamResult examResult = ExamBll.GetExamResult(this.resultid);
				bool flag = examResult.id == 0;
				if (flag)
				{
					base.AddErr("对不起，该考试不存在或已被删除。");
				}
				else
				{
					SqlParam[] array = new SqlParam[]
					{
						DbHelper.MakeUpdate("utime", examResult.utime + this.utime),
						DbHelper.MakeAndWhere("id", this.resultid)
					};
					DbHelper.ExecuteUpdate<ExamResult>(array);
					var o = new
					{
						errcode = 0,
						errmsg = ""
					};
					FPResponse.WriteJson(o);
				}
			}
		}

		// Token: 0x04000020 RID: 32
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x04000021 RID: 33
		protected int utime = FPRequest.GetInt("utime");
	}
}
