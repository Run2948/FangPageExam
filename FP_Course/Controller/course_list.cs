using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Course.Model;
using System;
using System.Collections.Generic;

namespace FP_Course.Controller
{
	public class course_list : LoginController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected List<SortInfo> sortlist = new List<SortInfo>();

		protected List<CourseInfo> courselist = new List<CourseInfo>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected override void Controller()
		{
			bool flag = this.channelid == 0 && this.args.Length > 1;
			if (flag)
			{
				this.channelid = FPUtils.StrToInt(this.args[1]);
			}
			bool flag2 = this.sortid > 0;
			if (flag2)
			{
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				this.channelid = this.sortinfo.channelid;
			}
			List<SqlParam> list = new List<SqlParam>();
			bool flag3 = this.channelid > 0;
			if (flag3)
			{
				list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
			}
			list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, this.role.sorts));
			bool flag4 = this.sortid > 0;
			if (flag4)
			{
				list.Add(DbHelper.MakeAndWhere("sortid", this.sortid));
			}
			list.Add(DbHelper.MakeOrderBy("istop", OrderBy.DESC));
			list.Add(DbHelper.MakeOrderBy("postdatetime", OrderBy.DESC));
			this.courselist = DbHelper.ExecuteList<CourseInfo>(this.pager, list.ToArray());
			this.sortlist = SortBll.GetSortList(this.channelid, this.role.sorts);
		}
	}
}
