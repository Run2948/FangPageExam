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
	public class wrong : LoginController
	{
		protected int qid = FPRequest.GetInt("qid");

		protected int option = FPRequest.GetInt("action");

		protected override void Controller()
		{
			ExamQuestion examQuestion = DbHelper.ExecuteModel<ExamQuestion>(this.qid);
			bool flag = examQuestion.id == 0;
			if (flag)
			{
				base.WriteErr("对不起，该题目不存在或已被删除。");
			}
			SortInfo sortInfo = SortBll.GetSortInfo(examQuestion.sortid);
			bool flag2 = this.option == 1;
			if (flag2)
			{
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
							examLogInfo.wrongs = 1;
							bool flag5 = num == sortInfo.id;
							if (flag5)
							{
								examLogInfo.curwrongs = 1;
							}
							examLogInfo.wronglist = this.qid.ToString();
							DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
						}
						else
						{
							examLogInfo.wrongs++;
							bool flag6 = num == sortInfo.id;
							if (flag6)
							{
								examLogInfo.curwrongs++;
							}
							ExamLogInfo expr_13A = examLogInfo;
							expr_13A.wronglist += ((examLogInfo.wronglist == "") ? this.qid.ToString() : ("," + this.qid.ToString()));
							DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
						}
					}
				}
			}
			else
			{
				bool flag7 = this.option == -1;
				if (flag7)
				{
					int[] array2 = FPArray.SplitInt(sortInfo.parentlist);
					for (int j = 0; j < array2.Length; j++)
					{
						int num2 = array2[j];
						bool flag8 = num2 == 0;
						if (!flag8)
						{
							ExamLogInfo examLogInfo2 = ExamBll.GetExamLogInfo(this.userid, num2);
							bool flag9 = examLogInfo2.sortid == 0;
							if (!flag9)
							{
								bool flag10 = FPArray.InArray(this.qid, examLogInfo2.wronglist) >= 0;
								if (flag10)
								{
									examLogInfo2.wrongs--;
									bool flag11 = num2 == sortInfo.id;
									if (flag11)
									{
										examLogInfo2.curwrongs--;
									}
									string text = "";
									int[] array3 = FPArray.SplitInt(examLogInfo2.wronglist);
									for (int k = 0; k < array3.Length; k++)
									{
										int num3 = array3[k];
										bool flag12 = num3 != this.qid;
										if (flag12)
										{
											text += ((text == "") ? num3.ToString() : ("," + num3.ToString()));
										}
									}
									examLogInfo2.wronglist = text;
									DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo2);
								}
							}
						}
					}
				}
			}
			var obj = new
			{
				errcode = 0,
				errmsg = "",
				action = this.option
			};
			FPResponse.WriteJson(obj);
		}
	}
}
