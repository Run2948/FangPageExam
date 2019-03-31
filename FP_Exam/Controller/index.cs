using FangPage.WMS.Web;
using FP_Exam.Model;
using System;

namespace FP_Exam.Controller
{
	public class index : LoginController
	{
		protected ExamConfig examconfig = new ExamConfig();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
		}
	}
}
