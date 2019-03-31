using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections;

namespace FP_Exam.Controller
{
	public class questionajax : AdminController
	{
		protected int qid = FPRequest.GetInt("qid");

		protected int status = FPRequest.GetInt("status");

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeUpdate("status", this.status),
					DbHelper.MakeAndWhere("id", this.qid)
				};
				DbHelper.ExecuteUpdate<ExamQuestion>(sqlparams);
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["status"] = this.status;
			FPResponse.WriteJson(hashtable);
		}

		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			FPResponse.WriteJson(hashtable);
		}
	}
}
