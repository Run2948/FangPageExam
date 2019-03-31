using System;
using FangPage.MVC;
using FangPage.WMS.API;

namespace FP_Exam.Wap.WapController
{
	// Token: 0x02000005 RID: 5
	public class checkanswer : LoginController
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000260C File Offset: 0x0000080C
		protected override void Controller()
		{
		}

		// Token: 0x0400000B RID: 11
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x0400000C RID: 12
		protected string answer = FPRequest.GetString("answer");

		// Token: 0x0400000D RID: 13
		protected int resultid = FPRequest.GetInt("resultid");
	}
}
