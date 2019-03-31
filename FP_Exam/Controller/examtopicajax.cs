using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections;

namespace FP_Exam.Controller
{
	public class examtopicajax : AdminController
	{
		protected int tid = FPRequest.GetInt("tid");

		protected int qid = FPRequest.GetInt("qid");

		protected int option = FPRequest.GetInt("action");

		protected override void Controller()
		{
			ExamTopic examTopic = DbHelper.ExecuteModel<ExamTopic>(this.tid);
			bool flag = FPArray.InArray(this.qid, examTopic.questionlist) == -1 && this.tid > 0 && this.qid > 0 && this.option == 1;
			if (flag)
			{
				bool flag2 = examTopic.curquestions >= examTopic.questions - examTopic.randoms;
				if (flag2)
				{
					this.ShowErrMsg("该大题题目数已满，不能再添加题目");
					return;
				}
				examTopic.questionlist = ((examTopic.questionlist == "") ? this.qid.ToString() : (examTopic.questionlist + "," + this.qid));
				examTopic.curquestions = FPArray.SplitInt(examTopic.questionlist).Length;
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeUpdate("questionlist", examTopic.questionlist),
					DbHelper.MakeUpdate("curquestions", examTopic.curquestions),
					DbHelper.MakeAndWhere("id", this.tid)
				};
				DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
			}
			bool flag3 = this.tid > 0 && this.qid > 0 && this.option == -1;
			if (flag3)
			{
				string text = "";
				int[] array = FPArray.SplitInt(examTopic.questionlist);
				for (int i = 0; i < array.Length; i++)
				{
					int num = array[i];
					bool flag4 = this.qid != num && num != 0;
					if (flag4)
					{
						bool flag5 = text != "";
						if (flag5)
						{
							text += ",";
						}
						text += num;
					}
				}
				examTopic.questionlist = text;
				bool flag6 = examTopic.questionlist != "";
				if (flag6)
				{
					examTopic.curquestions = FPArray.SplitInt(examTopic.questionlist).Length;
				}
				else
				{
					examTopic.curquestions = 0;
				}
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeUpdate("questionlist", examTopic.questionlist),
					DbHelper.MakeUpdate("curquestions", examTopic.curquestions),
					DbHelper.MakeAndWhere("id", this.tid)
				};
				DbHelper.ExecuteUpdate<ExamTopic>(sqlparams2);
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["curquestions"] = examTopic.curquestions;
			hashtable["questionlist"] = examTopic.questionlist;
			hashtable["action"] = this.option;
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
