using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200000B RID: 11
	public class exammonitor : AdminController
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000062DC File Offset: 0x000044DC
		protected override void View()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			if (this.examinfo.id == 0)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				if (this.ispost)
				{
					string a = FPRequest.GetString("action").ToLower();
					string @string = FPRequest.GetString("chkid");
					if (a == "delete")
					{
						if (DbHelper.ExecuteDelete<ExamResult>(@string) > 0)
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("resultid", WhereType.In, @string);
							DbHelper.ExecuteDelete<ExamResultTopic>(new SqlParam[]
							{
								sqlParam
							});
						}
					}
					else if (a == "change")
					{
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("ip", ""),
							DbHelper.MakeSet("mac", ""),
							DbHelper.MakeAndWhere("id", WhereType.In, @string)
						};
						DbHelper.ExecuteUpdate<ExamResult>(sqlparams);
					}
				}
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeAndWhere("examid", this.examid),
					DbHelper.MakeAndWhere("status", 0)
				};
				OrderByParam orderby = DbHelper.MakeOrderBy("id", OrderBy.DESC);
				this.examresultlist = DbHelper.ExecuteList<ExamResult>(orderby, sqlparams2);
				base.SaveRightURL();
			}
		}

		// Token: 0x04000022 RID: 34
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x04000023 RID: 35
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000024 RID: 36
		protected List<ExamResult> examresultlist = new List<ExamResult>();

		// Token: 0x04000025 RID: 37
		protected int sortid;

		// Token: 0x04000026 RID: 38
		protected SortInfo sortinfo = new SortInfo();
	}
}
