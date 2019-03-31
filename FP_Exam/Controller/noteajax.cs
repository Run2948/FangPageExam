using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FP_Exam.Model;
using System;

namespace FP_Exam.Controller
{
	public class noteajax : LoginController
	{
		protected int qid = FPRequest.GetInt("qid");

		protected string note = FPRequest.GetString("note");

		protected override void Controller()
		{
			ExamQuestion examQuestion = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
			bool flag = examQuestion.id == 0;
			if (flag)
			{
				base.WriteErr("对不起，该题目不存在或已被删除。");
			}
			SortInfo sortInfo = SortBll.GetSortInfo(examQuestion.sortid);
			bool flag2 = sortInfo.id == 0;
			if (flag2)
			{
				base.WriteErr("对不起，该题目题库不存在或已被删除。");
			}
			int[] array = FPArray.SplitInt(sortInfo.parentlist);
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				bool flag3 = num == 0;
				if (!flag3)
				{
					ExamLogInfo examLogInfo = ExamBll.GetExamLogInfo(this.userid, num);
					bool flag4 = examLogInfo.sortid == 0;
					if (flag4)
					{
						examLogInfo.sortid = examQuestion.sortid;
						examLogInfo.channelid = examQuestion.channelid;
						examLogInfo.uid = this.userid;
						examLogInfo.notes = 1;
						examLogInfo.notelist = this.qid.ToString();
						bool flag5 = num == sortInfo.id;
						if (flag5)
						{
							examLogInfo.curnotes = 1;
						}
						DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
					}
					else
					{
						bool flag6 = FPArray.InArray(this.qid, examLogInfo.notelist) == -1;
						if (flag6)
						{
							examLogInfo.notes++;
							ExamLogInfo expr_140 = examLogInfo;
							expr_140.notelist += ((examLogInfo.notelist == "") ? this.qid.ToString() : ("," + this.qid.ToString()));
							bool flag7 = num != sortInfo.id;
							if (flag7)
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
			bool flag8 = examNote.id > 0;
			if (flag8)
			{
				DbHelper.ExecuteUpdate<ExamNote>(examNote);
			}
			else
			{
				DbHelper.ExecuteInsert<ExamNote>(examNote);
			}
			var obj = new
			{
				errcode = 0,
				errmsg = ""
			};
			FPResponse.WriteJson(obj);
		}
	}
}
