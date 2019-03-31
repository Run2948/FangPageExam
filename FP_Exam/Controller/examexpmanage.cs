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
	public class examexpmanage : AdminController
	{
		protected int id = FPRequest.GetInt("id");

		protected int sortid = FPRequest.GetInt("sortid");

		protected int examid = FPRequest.GetInt("examid");

		protected SortInfo sortinfo = new SortInfo();

		protected ExamInfo examinfo = new ExamInfo();

		protected List<ExpInfo> explist = new List<ExpInfo>();

		protected ExpInfo expinfo = new ExpInfo();

		protected override void Controller()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			bool flag = this.examinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool flag2 = this.sortinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该栏目不存在或已被删除。");
				}
				else
				{
					this.expinfo = DbHelper.ExecuteModel<ExpInfo>(this.id);
					bool ispost = this.ispost;
					if (ispost)
					{
						bool flag3 = this.action == "add";
						if (flag3)
						{
							this.expinfo = FPRequest.GetModel<ExpInfo>(this.expinfo);
							bool flag4 = this.expinfo.scorelower >= this.expinfo.scoreupper;
							if (flag4)
							{
								this.ShowErr("分数上限必须大于下限。");
								return;
							}
							this.expinfo.examid = this.examid;
							DbHelper.ExecuteInsert<ExpInfo>(this.expinfo);
						}
						else
						{
							bool flag5 = this.action == "update";
							if (flag5)
							{
								this.expinfo = FPRequest.GetModel<ExpInfo>(this.expinfo, "exp_");
								bool flag6 = this.expinfo.scorelower >= this.expinfo.scoreupper;
								if (flag6)
								{
									this.ShowErr("分数上限必须大于下限。");
									return;
								}
								DbHelper.ExecuteUpdate<ExpInfo>(this.expinfo);
							}
							else
							{
								bool flag7 = this.action == "delete";
								if (flag7)
								{
									DbHelper.ExecuteDelete<ExpInfo>(FPRequest.GetString("eid"));
								}
							}
						}
						base.Response.Redirect(this.pagename + "?examid=" + this.examid);
					}
					SqlParam sqlParam = DbHelper.MakeAndWhere("examid", this.examid);
					this.explist = DbHelper.ExecuteList<ExpInfo>(OrderBy.ASC, new SqlParam[]
					{
						sqlParam
					});
				}
			}
		}
	}
}
