using System;
using System.Collections;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;
using LitJson;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000031 RID: 49
	public class noteajax : LoginController
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00015284 File Offset: 0x00013484
		protected override void View()
		{
			ExamQuestion examQuestion = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
			if (examQuestion.id == 0)
			{
				this.ShowErrMsg("对不起，该题目不存在或已被删除。");
			}
			SortInfo sortInfo = SortBll.GetSortInfo(examQuestion.sortid);
			if (sortInfo.id == 0)
			{
				this.ShowErrMsg("对不起，该题目题库不存在或已被删除。");
			}
			foreach (int num in FPUtils.SplitInt(sortInfo.parentlist))
			{
				if (num != 0)
				{
					ExamLogInfo examLogInfo = ExamBll.GetExamLogInfo(this.userid, num);
					if (examLogInfo.sortid == 0)
					{
						examLogInfo.sortid = examQuestion.sortid;
						examLogInfo.channelid = examQuestion.channelid;
						examLogInfo.uid = this.userid;
						examLogInfo.notes = 1;
						examLogInfo.notelist = this.qid.ToString();
						if (num == sortInfo.id)
						{
							examLogInfo.curnotes = 1;
						}
						DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
					}
					else
					{
						if (!FPUtils.InArray(this.qid, examLogInfo.notelist))
						{
							examLogInfo.notes++;
							ExamLogInfo examLogInfo2 = examLogInfo;
							examLogInfo2.notelist += ((examLogInfo.notelist == "") ? this.qid.ToString() : ("," + this.qid.ToString()));
							if (num != sortInfo.id)
							{
								examLogInfo.curnotes++;
							}
						}
						DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
					}
				}
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("qid", this.qid),
				DbHelper.MakeAndWhere("uid", this.userid)
			};
			ExamNote examNote = DbHelper.ExecuteModel<ExamNote>(sqlparams);
			examNote.uid = this.userid;
			examNote.qid = this.qid;
			examNote.note = this.note;
			if (examNote.id > 0)
			{
				DbHelper.ExecuteUpdate<ExamNote>(examNote);
			}
			else
			{
				DbHelper.ExecuteInsert<ExamNote>(examNote);
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["message"] = "";
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00015550 File Offset: 0x00013750
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x040000F8 RID: 248
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x040000F9 RID: 249
		protected string note = FPRequest.GetString("note");
	}
}
