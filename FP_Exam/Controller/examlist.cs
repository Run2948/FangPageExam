using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class examlist : LoginController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected List<ExamInfo> _examlist = new List<ExamInfo>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected override void Controller()
		{
			bool flag = this.channelid == 0 && this.args.Length > 1;
			if (flag)
			{
				this.channelid = FPUtils.StrToInt(this.args[1]);
			}
			ChannelInfo channelInfo = ChannelBll.GetChannelInfo(this.channelid);
			bool flag2 = channelInfo.id == 0 && this.channelid > 0;
			if (flag2)
			{
				this.ShowErr("对不起，该考试频道不存在或已被删除。");
			}
			else
			{
				SortInfo sortInfo = SortBll.GetSortInfo(this.sortid);
				bool flag3 = sortInfo.id == 0 && this.sortid > 0;
				if (flag3)
				{
					this.ShowErr("对不起，该考试科目不存在或已被删除。");
				}
				else
				{
					bool flag4 = this.channelid == 0 && sortInfo.id > 0;
					if (flag4)
					{
						this.channelid = sortInfo.channelid;
					}
					List<SqlParam> list = new List<SqlParam>();
					list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
					list.Add(DbHelper.MakeAndWhere("status", 1));
					list.Add(DbHelper.MakeAndWhere("client[pc]", "1"));
					list.Add(DbHelper.MakeAndWhere('(', "examroles,examdeparts,examuser", WhereType.IsNullOrEmpty));
					list.Add(DbHelper.MakeOrWhere("examroles", WhereType.Contain, this.roleid));
					list.Add(DbHelper.MakeOrWhere("examdeparts", WhereType.Contain, this.departid));
					list.Add(DbHelper.MakeOrWhere("examuser", ')', WhereType.Contain, this.userid));
					list.Add(DbHelper.MakeAndWhere("([islimit]=0", WhereType.Custom));
					list.Add(DbHelper.MakeOrWhere("([islimit]=1", WhereType.Custom));
					list.Add(DbHelper.MakeAndWhere("starttime", WhereType.LessThanEqual, DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
					list.Add(DbHelper.MakeAndWhere("endtime", WhereType.GreaterThanEqual, DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
					list.Add(DbHelper.MakeWhere("))"));
					bool flag5 = sortInfo.id > 0;
					if (flag5)
					{
						list.Add(DbHelper.MakeAndWhere("sortid", sortInfo.id));
					}
					this._examlist = DbHelper.ExecuteList<ExamInfo>(this.pager, list.ToArray());
				}
			}
		}
	}
}
