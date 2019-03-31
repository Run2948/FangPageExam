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
	public class course_manage : AdminController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected int typeid = FPRequest.GetInt("typeid");

		protected SortInfo sortinfo = new SortInfo();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected List<CourseInfo> courselist = new List<CourseInfo>();

		protected override void Controller()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool flag = this.sortinfo.id == 0 && this.sortid > 0;
			if (flag)
			{
				this.ShowErr("该栏目已被删除或不存在");
			}
			else
			{
				bool flag2 = this.sortinfo.id > 0;
				if (flag2)
				{
					this.channelid = this.sortinfo.channelid;
				}
				bool ispost = this.ispost;
				if (ispost)
				{
					string intString = FPRequest.GetIntString("chkid");
					bool flag3 = this.action == "delete";
					if (flag3)
					{
						DbHelper.ExecuteDelete<CourseInfo>(intString);
					}
				}
				List<SqlParam> list = new List<SqlParam>();
				bool flag4 = this.channelid > 0;
				if (flag4)
				{
					list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
				}
				bool flag5 = this.sortinfo.id > 0;
				if (flag5)
				{
					string childSorts = SortBll.GetChildSorts(this.sortinfo);
					list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
				}
				bool flag6 = this.typeid > 0;
				if (flag6)
				{
					list.Add(DbHelper.MakeAndWhere("typelist", WhereType.Contain, this.typeid));
				}
				bool flag7 = !this.isperm;
				if (flag7)
				{
					list.Add(DbHelper.MakeAndWhere("uid", this.userid));
				}
				list.Add(DbHelper.MakeOrderBy("istop", OrderBy.DESC));
				list.Add(DbHelper.MakeOrderBy("postdatetime", OrderBy.DESC));
				this.courselist = DbHelper.ExecuteList<CourseInfo>(this.pager, list.ToArray());
			}
		}
	}
}
