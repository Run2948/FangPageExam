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
	public class favajax : LoginController
	{
		protected int qid = FPRequest.GetInt("qid");

		protected int option = FPRequest.GetInt("action");

		protected string uanswer = FPRequest.GetString("uanswer");

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
				base.WriteErr("该题目的题库不存在或已被删除。");
			}
			else
			{
				string[] array = FPArray.SplitString(this.uanswer, "§", 2);
				bool flag3 = this.option == 1;
				if (flag3)
				{
					int[] array2 = FPArray.SplitInt(sortInfo.parentlist);
					for (int i = 0; i < array2.Length; i++)
					{
						int num = array2[i];
						bool flag4 = num == 0;
						if (!flag4)
						{
							ExamLogInfo examLogInfo = ExamBll.GetExamLogInfo(this.userid, num);
							bool flag5 = examLogInfo.id == 0;
							if (flag5)
							{
								examLogInfo.sortid = num;
								examLogInfo.channelid = examQuestion.channelid;
								examLogInfo.uid = this.userid;
								examLogInfo.favs = 1;
								bool flag6 = num == sortInfo.id;
								if (flag6)
								{
									examLogInfo.curfavs = 1;
								}
								examLogInfo.favlist = this.qid.ToString();
								examLogInfo.qidlist = this.qid.ToString();
								examLogInfo.answerlist = array[0];
								DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
							}
							else
							{
								examLogInfo.favs++;
								bool flag7 = num == sortInfo.id;
								if (flag7)
								{
									examLogInfo.curfavs++;
								}
								ExamLogInfo expr_18C = examLogInfo;
								expr_18C.favlist += ((examLogInfo.favlist == "") ? this.qid.ToString() : ("," + this.qid.ToString()));
								bool flag8 = FPArray.InArray(this.qid, examLogInfo.qidlist) >= 0;
								if (flag8)
								{
									int[] array3 = FPArray.SplitInt(examLogInfo.qidlist);
									string[] array4 = FPArray.SplitString(examLogInfo.answerlist, "§", array3.Length);
									string text = "";
									for (int j = 0; j < array3.Length; j++)
									{
										bool flag9 = array3[j] == examQuestion.id;
										if (flag9)
										{
											array4[j] = array[0];
										}
										bool flag10 = text != "";
										if (flag10)
										{
											text += "§";
										}
										text += array4[j];
									}
									examLogInfo.answerlist = text;
								}
								else
								{
									ExamLogInfo expr_29B = examLogInfo;
									expr_29B.qidlist += ((examLogInfo.qidlist == "") ? this.qid.ToString() : ("," + this.qid.ToString()));
									ExamLogInfo expr_2E3 = examLogInfo;
									expr_2E3.answerlist += ((examLogInfo.answerlist == "") ? array[0] : ("§" + array[0]));
								}
								DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
							}
						}
					}
				}
				else
				{
					bool flag11 = this.option == -1;
					if (flag11)
					{
						int[] array5 = FPArray.SplitInt(sortInfo.parentlist);
						for (int k = 0; k < array5.Length; k++)
						{
							int num2 = array5[k];
							bool flag12 = num2 == 0;
							if (!flag12)
							{
								ExamLogInfo examLogInfo2 = ExamBll.GetExamLogInfo(this.userid, num2);
								bool flag13 = examLogInfo2.sortid == 0;
								if (!flag13)
								{
									bool flag14 = FPArray.InArray(this.qid, examLogInfo2.favlist) >= 0;
									if (flag14)
									{
										examLogInfo2.favs--;
										bool flag15 = num2 == sortInfo.id;
										if (flag15)
										{
											examLogInfo2.curfavs--;
										}
										string text2 = "";
										int[] array6 = FPArray.SplitInt(examLogInfo2.favlist);
										for (int l = 0; l < array6.Length; l++)
										{
											int num3 = array6[l];
											bool flag16 = num3 != this.qid;
											if (flag16)
											{
												text2 += ((text2 == "") ? num3.ToString() : ("," + num3.ToString()));
											}
										}
										examLogInfo2.favlist = text2;
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
}
