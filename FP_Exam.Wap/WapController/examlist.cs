using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.WapController
{
	public class examlist : LoginController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected List<ExamInfo> _examlist = new List<ExamInfo>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected override void Controller()
		{
			this.iswrite = 2;
			this.pager.pagesize = 10;
			bool flag = this.channelid == 0 && this.args.Length > 1;
			if (flag)
			{
				this.channelid = FPUtils.StrToInt(this.args[1]);
			}
			SortInfo sortInfo = SortBll.GetSortInfo(this.sortid);
			bool flag2 = this.channelid == 0 && sortInfo.id > 0;
			if (flag2)
			{
				this.channelid = sortInfo.channelid;
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
			list.Add(DbHelper.MakeAndWhere("status", 1));
			list.Add(DbHelper.MakeAndWhere("client[mobile]", "1"));
			list.Add(DbHelper.MakeAndWhere('(', "examroles,examdeparts,examuser", WhereType.IsNullOrEmpty));
			list.Add(DbHelper.MakeOrWhere("examroles", WhereType.Contain, this.roleid));
			list.Add(DbHelper.MakeOrWhere("examdeparts", WhereType.Contain, this.departid));
			list.Add(DbHelper.MakeOrWhere("examuser", ')', WhereType.Contain, this.userid));
			list.Add(DbHelper.MakeAndWhere("([islimit]=0", WhereType.Custom));
			list.Add(DbHelper.MakeOrWhere("([islimit]=1", WhereType.Custom));
			list.Add(DbHelper.MakeAndWhere("starttime", WhereType.LessThanEqual, DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
			list.Add(DbHelper.MakeAndWhere("endtime", WhereType.GreaterThanEqual, DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
			list.Add(DbHelper.MakeWhere("))"));
			bool flag3 = sortInfo.id > 0;
			if (flag3)
			{
				list.Add(DbHelper.MakeAndWhere("sortid", sortInfo.id));
			}
			this._examlist = DbHelper.ExecuteList<ExamInfo>(this.pager, list.ToArray());
		}

		protected override void Complete()
		{
			var obj = new
			{
				errcode = 0,
				errmsg = "",
				html = this.ViewBuilder.ToString(),
				page = this.pager.pageindex,
				pagecount = this.pager.pagecount
			};
			FPResponse.WriteJson(obj);
		}
	}
}
