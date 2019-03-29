using System;
using System.Collections.Generic;
using FangPage.Exam;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000021 RID: 33
	public class examhistory : LoginController
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000EE9C File Offset: 0x0000D09C
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				this.examresultlist = ExamBll.GetExamResultList(this.channelinfo.id, this.userid);
			}
		}

		// Token: 0x040000A4 RID: 164
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000A5 RID: 165
		protected List<ExamResult> examresultlist = new List<ExamResult>();
	}
}
