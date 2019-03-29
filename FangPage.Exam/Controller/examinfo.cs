using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000022 RID: 34
	public class examinfo : BaseController
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000EF1C File Offset: 0x0000D11C
		protected override void View()
		{
			this._examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			if (this._examinfo.id == 0)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this._examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				this.channelid = this._examinfo.channelid;
				this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
				if (this.channelinfo.id == 0)
				{
					this.ShowErr("对不起，该试题题库不存在或已被删除。");
				}
				else
				{
					this._examinfo.views++;
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeSet("views", this._examinfo.views),
						DbHelper.MakeAndWhere("id", this._examinfo.id)
					};
					DbHelper.ExecuteUpdate<ExamInfo>(sqlparams);
				}
			}
		}

		// Token: 0x040000A6 RID: 166
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000A7 RID: 167
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000A8 RID: 168
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x040000A9 RID: 169
		protected ExamInfo _examinfo = new ExamInfo();
	}
}
