using System;
using System.Collections;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.Exam.Model;
using LitJson;

namespace FangPage.Exam.Controller
{
	// Token: 0x0200000F RID: 15
	public class examtopicajax : AdminController
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00007D4C File Offset: 0x00005F4C
		protected override void View()
		{
			ExamTopic examTopic = DbHelper.ExecuteModel<ExamTopic>(this.tid);
			if (!FPUtils.InArray(this.qid, examTopic.questionlist) && this.tid > 0 && this.qid > 0 && this.option == 1)
			{
				if (examTopic.curquestions >= examTopic.questions - examTopic.randoms)
				{
					this.ShowErrMsg("该大题题目数已满，不能再添加题目");
					return;
				}
				examTopic.questionlist = ((examTopic.questionlist == "") ? this.qid.ToString() : (examTopic.questionlist + "," + this.qid));
				examTopic.curquestions = FPUtils.SplitInt(examTopic.questionlist).Length;
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeSet("questionlist", examTopic.questionlist),
					DbHelper.MakeSet("curquestions", examTopic.curquestions),
					DbHelper.MakeAndWhere("id", this.tid)
				};
				DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
			}
			if (this.tid > 0 && this.qid > 0 && this.option == -1)
			{
				string text = "";
				foreach (int num in FPUtils.SplitInt(examTopic.questionlist))
				{
					if (this.qid != num && num != 0)
					{
						if (text != "")
						{
							text += ",";
						}
						text += num;
					}
				}
				examTopic.questionlist = text;
				examTopic.curquestions = FPUtils.SplitInt(examTopic.questionlist).Length;
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeSet("questionlist", examTopic.questionlist),
					DbHelper.MakeSet("curquestions", examTopic.curquestions),
					DbHelper.MakeAndWhere("id", this.tid)
				};
				DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["curquestions"] = examTopic.curquestions;
			hashtable["questionlist"] = examTopic.questionlist;
			hashtable["action"] = this.option;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00008034 File Offset: 0x00006234
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x04000037 RID: 55
		protected int tid = FPRequest.GetInt("tid");

		// Token: 0x04000038 RID: 56
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x04000039 RID: 57
		protected int option = FPRequest.GetInt("action");
	}
}
