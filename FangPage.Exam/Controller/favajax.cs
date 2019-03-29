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
	// Token: 0x0200002D RID: 45
	public class favajax : LoginController
	{
		// Token: 0x060000CB RID: 203 RVA: 0x0001420C File Offset: 0x0001240C
		protected override void View()
		{
			ExamQuestion examQuestion = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
			if (examQuestion.id == 0)
			{
				this.ShowErrMsg("对不起，该题目不存在或已被删除。");
			}
			SortInfo sortInfo = SortBll.GetSortInfo(examQuestion.sortid);
			if (this.option == 1)
			{
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
							examLogInfo.favs = 1;
							if (num == sortInfo.id)
							{
								examLogInfo.curfavs = 1;
							}
							examLogInfo.favlist = this.qid.ToString();
							DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
						}
						else
						{
							examLogInfo.favs++;
							if (num == sortInfo.id)
							{
								examLogInfo.curfavs++;
							}
							ExamLogInfo examLogInfo2 = examLogInfo;
							examLogInfo2.favlist += ((examLogInfo.favlist == "") ? this.qid.ToString() : ("," + this.qid.ToString()));
							DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
						}
					}
				}
			}
			else if (this.option == -1)
			{
				foreach (int num in FPUtils.SplitInt(sortInfo.parentlist))
				{
					if (num != 0)
					{
						ExamLogInfo examLogInfo = ExamBll.GetExamLogInfo(this.userid, num);
						if (examLogInfo.sortid != 0)
						{
							if (FPUtils.InArray(this.qid, examLogInfo.favlist))
							{
								examLogInfo.favs--;
								if (num == sortInfo.id)
								{
									examLogInfo.curfavs--;
								}
								string text = "";
								foreach (int num2 in FPUtils.SplitInt(examLogInfo.favlist))
								{
									if (num2 != this.qid)
									{
										text += ((text == "") ? num2.ToString() : ("," + num2.ToString()));
									}
								}
								examLogInfo.favlist = text;
								DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
							}
						}
					}
				}
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["message"] = "";
			hashtable["action"] = this.option;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0001459C File Offset: 0x0001279C
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x040000EB RID: 235
		protected int qid = FPRequest.GetInt("qid");

		// Token: 0x040000EC RID: 236
		protected int option = FPRequest.GetInt("action");
	}
}
