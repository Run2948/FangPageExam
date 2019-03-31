using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;

namespace FP_Exam.Controller
{
	public class examsign_audit : LoginController
	{
		protected int id = FPRequest.GetInt("id");

		protected ExamSignInfo examsigninfo = new ExamSignInfo();

		protected ExamInfo examinfo = new ExamInfo();

		protected override void Controller()
		{
			this.examsigninfo = DbHelper.ExecuteModel<ExamSignInfo>(this.id);
			bool flag = this.examsigninfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该报名不存在或已被删除。");
			}
			bool flag2 = this.roleid != 1;
			if (flag2)
			{
				this.ShowErr("对不起，您没有权限。");
			}
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examsigninfo.examid);
			bool flag3 = this.examinfo.id == 0;
			if (flag3)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
			bool ispost = this.ispost;
			if (ispost)
			{
				bool flag4 = this.action == "pass";
				if (flag4)
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeUpdate("status", 2),
						DbHelper.MakeUpdate("reason", FPRequest.GetString("reason")),
						DbHelper.MakeAndWhere("id", this.id)
					};
					DbHelper.ExecuteUpdate<ExamSignInfo>(sqlparams);
					this.examinfo.examuser = FPArray.Push(this.examinfo.examuser, this.examsigninfo.uid);
					SqlParam[] sqlparams2 = new SqlParam[]
					{
						DbHelper.MakeUpdate("examuser", this.examinfo.examuser),
						DbHelper.MakeAndWhere("id", this.examinfo.id)
					};
					DbHelper.ExecuteUpdate<ExamInfo>(sqlparams2);
				}
				else
				{
					bool flag5 = this.action == "unpass";
					if (flag5)
					{
						SqlParam[] sqlparams3 = new SqlParam[]
						{
							DbHelper.MakeUpdate("status", 3),
							DbHelper.MakeUpdate("reason", FPRequest.GetString("reason")),
							DbHelper.MakeAndWhere("id", this.id)
						};
						DbHelper.ExecuteUpdate<ExamSignInfo>(sqlparams3);
						this.examinfo.examuser = FPArray.Remove(this.examinfo.examuser, this.examsigninfo.uid.ToString());
						SqlParam[] sqlparams4 = new SqlParam[]
						{
							DbHelper.MakeUpdate("examuser", this.examinfo.examuser),
							DbHelper.MakeAndWhere("id", this.examinfo.id)
						};
						DbHelper.ExecuteUpdate<ExamInfo>(sqlparams4);
					}
				}
				FPResponse.Redirect(this.pageurl);
			}
		}
	}
}
