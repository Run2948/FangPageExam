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
	public class exammonitor : AdminController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected ExamInfo examinfo = new ExamInfo();

		protected List<ExamResult> examresultlist = new List<ExamResult>();

		protected int sortid;

		protected SortInfo sortinfo = new SortInfo();

		protected override void Controller()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			bool flag = this.examinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool ispost = this.ispost;
				if (ispost)
				{
					string a = FPRequest.GetString("action").ToLower();
					string @string = FPRequest.GetString("chkid");
					bool flag2 = a == "delete";
					if (flag2)
					{
						bool flag3 = DbHelper.ExecuteDelete<ExamResult>(@string) > 0;
						if (flag3)
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("resultid", WhereType.In, @string);
							DbHelper.ExecuteDelete<ExamResultTopic>(new SqlParam[]
							{
								sqlParam
							});
						}
					}
					else
					{
						bool flag4 = a == "change";
						if (flag4)
						{
							SqlParam[] sqlparams = new SqlParam[]
							{
								DbHelper.MakeUpdate("ip", ""),
								DbHelper.MakeUpdate("mac", ""),
								DbHelper.MakeAndWhere("id", WhereType.In, @string)
							};
							DbHelper.ExecuteUpdate<ExamResult>(sqlparams);
						}
					}
				}
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeAndWhere("examid", this.examid),
					DbHelper.MakeAndWhere("status", 0),
					DbHelper.MakeOrderBy("id", OrderBy.DESC)
				};
				this.examresultlist = DbHelper.ExecuteList<ExamResult>(sqlparams2);
			}
		}
	}
}
