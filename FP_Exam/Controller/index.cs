using System;
using FangPage.WMS;
using FangPage.WMS.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000030 RID: 48
	public class index : LoginController
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x0001522C File Offset: 0x0001342C
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
		}

		// Token: 0x040000F7 RID: 247
		protected ChannelInfo channelinfo = new ChannelInfo();
	}
}
