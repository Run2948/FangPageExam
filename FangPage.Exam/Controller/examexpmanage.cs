using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000009 RID: 9
	public class examexpmanage : AdminController
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000056A4 File Offset: 0x000038A4
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
				if (this.sortinfo.id == 0)
				{
					this.ShowErr("对不起，该栏目不存在或已被删除。");
				}
				else
				{
					this.expinfo = DbHelper.ExecuteModel<ExpInfo>(this.id);
					if (this.ispost)
					{
						if (this.action == "add")
						{
							this.expinfo = FPRequest.GetModel<ExpInfo>(this.expinfo);
							this.expinfo.examid = this.examid;
							DbHelper.ExecuteInsert<ExpInfo>(this.expinfo);
						}
						else if (this.action == "default")
						{
							ExpInfo expInfo = new ExpInfo();
							expInfo.examid = this.examid;
							expInfo.scorelower = 0;
							expInfo.scoreupper = 60;
							expInfo.exp = 0;
							expInfo.comment = "您的成绩不及格，加油哦";
							DbHelper.ExecuteInsert<ExpInfo>(expInfo);
							expInfo.scorelower = 60;
							expInfo.scoreupper = 70;
							expInfo.exp = 1;
							expInfo.comment = "您的成绩免强及格，仍需努力哦";
							DbHelper.ExecuteInsert<ExpInfo>(expInfo);
							expInfo.scorelower = 70;
							expInfo.scoreupper = 80;
							expInfo.exp = 2;
							expInfo.comment = "您的成绩中等水平，努力更上一台价";
							DbHelper.ExecuteInsert<ExpInfo>(expInfo);
							expInfo.scorelower = 80;
							expInfo.scoreupper = 90;
							expInfo.exp = 3;
							expInfo.comment = "您的成绩中上水平，努力保持";
							DbHelper.ExecuteInsert<ExpInfo>(expInfo);
							expInfo.scorelower = 90;
							expInfo.scoreupper = 100;
							expInfo.exp = 4;
							expInfo.comment = "您的成绩不错，不要骄傲";
							DbHelper.ExecuteInsert<ExpInfo>(expInfo);
						}
						else if (this.action == "update")
						{
							this.expinfo = FPRequest.GetModel<ExpInfo>(this.expinfo, "exp_");
							DbHelper.ExecuteUpdate<ExpInfo>(this.expinfo);
						}
						else if (this.action == "delete")
						{
							DbHelper.ExecuteDelete<ExpInfo>(FPRequest.GetString("eid"));
						}
						base.Response.Redirect(this.pagename + "?examid=" + this.examid);
					}
					SqlParam sqlParam = DbHelper.MakeAndWhere("examid", this.examid);
					this.explist = DbHelper.ExecuteList<ExpInfo>(OrderBy.ASC, new SqlParam[]
					{
						sqlParam
					});
					base.SaveRightURL();
				}
			}
		}

		// Token: 0x04000011 RID: 17
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000012 RID: 18
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x04000013 RID: 19
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x04000014 RID: 20
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000015 RID: 21
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000016 RID: 22
		protected List<ExpInfo> explist = new List<ExpInfo>();

		// Token: 0x04000017 RID: 23
		protected ExpInfo expinfo = new ExpInfo();
	}
}
