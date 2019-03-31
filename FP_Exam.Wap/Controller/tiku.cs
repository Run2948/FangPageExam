using FangPage.Common;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.Controller
{
	public class tiku : LoginController
	{
		protected ExamConfig examconfig = new ExamConfig();

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected List<SortInfo> sortlist = new List<SortInfo>();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			this.sortlist = this.GetSortList(this.channelinfo.id, 0);
		}

		protected List<SortInfo> GetSortList(int channelid, int parentid)
		{
			return SortBll.GetSortList(channelid, parentid).FindAll((SortInfo item) => FPArray.InArray(item.id, this.role.sorts) > -1);
		}
	}
}
