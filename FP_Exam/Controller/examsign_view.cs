using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;

namespace FP_Exam.Controller
{
	public class examsign_view : LoginController
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
			bool flag2 = this.examsigninfo.uid != this.userid && this.roleid != 1;
			if (flag2)
			{
				this.ShowErr("对不起，您没有权限。");
			}
			bool flag3 = this.pagename == "examsign_print.aspx" && this.examsigninfo.status != 2;
			if (flag3)
			{
				this.ShowErr("对不起，该报名尚未通过审核，不能打印准考证。");
			}
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examsigninfo.examid);
			bool flag4 = this.examinfo.id == 0;
			if (flag4)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
		}
	}
}
