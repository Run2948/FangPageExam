using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Model;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam
{
	public class ExamBll
	{
		public static string GetExamSorts(string idlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, idlist);
			List<ExamInfo> list = DbHelper.ExecuteList<ExamInfo>(new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			foreach (ExamInfo current in list)
			{
				bool flag = text != "";
				if (flag)
				{
					text += ",";
				}
				text += current.sortid;
			}
			return text;
		}

		public static void AddExamLarge(int examid)
		{
			ExamTopic examTopic = new ExamTopic();
			examTopic.type = "TYPE_RADIO";
			examTopic.title = "第一题、单选题";
			examTopic.examid = examid;
			examTopic.perscore = 2.0;
			examTopic.display = 1;
			examTopic.questions = 30;
			examTopic.paper = 1;
			DbHelper.ExecuteInsert<ExamTopic>(examTopic);
			examTopic.type = "TYPE_MULTIPLE";
			examTopic.title = "第二题、多选题";
			examTopic.examid = examid;
			examTopic.perscore = 4.0;
			examTopic.display = 2;
			examTopic.questions = 10;
			examTopic.paper = 1;
			DbHelper.ExecuteInsert<ExamTopic>(examTopic);
		}

		public static List<ExamInfo> GetExamList(int top)
		{
			return DbHelper.ExecuteList<ExamInfo>(top);
		}

		public static List<ExamTopic> GetExamTopicList(int examid, int paper)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("examid", examid),
				DbHelper.MakeAndWhere("paper", paper),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			return DbHelper.ExecuteList<ExamTopic>(sqlparams);
		}

		public static Dictionary<int, ExamLogInfo> GetExamLogList(int channelid, int uid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("uid", uid)
			};
			List<ExamLogInfo> list = DbHelper.ExecuteList<ExamLogInfo>(sqlparams);
			Dictionary<int, ExamLogInfo> dictionary = new Dictionary<int, ExamLogInfo>();
			foreach (ExamLogInfo current in list)
			{
				dictionary.Add(current.sortid, current);
			}
			return dictionary;
		}

		public static ExamLogInfo GetExamLogInfo(int uid, int sortid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("uid", uid),
				DbHelper.MakeAndWhere("sortid", sortid)
			};
			return DbHelper.ExecuteModel<ExamLogInfo>(sqlparams);
		}

		public static ExamResult GetExamResult(int resultid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", resultid);
			return DbHelper.ExecuteModel<ExamResult>(new SqlParam[]
			{
				sqlParam
			});
		}

		public static List<ExamResultTopic> GetExamResultTopicList(int resultid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("resultid", resultid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			return DbHelper.ExecuteList<ExamResultTopic>(sqlparams);
		}

		public static void UpdateExamLog(SortInfo sortinfo, int uid, ExamQuestion question, bool iswrong)
		{
			int[] array = FPArray.SplitInt(sortinfo.parentlist);
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				bool flag = num == 0;
				if (!flag)
				{
					ExamLogInfo examLogInfo = ExamBll.GetExamLogInfo(uid, num);
					bool flag2 = examLogInfo.sortid == 0;
					if (flag2)
					{
						examLogInfo.sortid = num;
						examLogInfo.uid = uid;
						examLogInfo.channelid = sortinfo.channelid;
						examLogInfo.answers = 1;
						examLogInfo.wrongs = (iswrong ? 1 : 0);
						bool flag3 = num == sortinfo.id;
						if (flag3)
						{
							examLogInfo.curwrongs = (iswrong ? 1 : 0);
						}
						examLogInfo.qidlist = question.id.ToString();
						examLogInfo.answerlist = question.useranswer;
						examLogInfo.scorelist = (iswrong ? "0" : "1");
						examLogInfo.wronglist = (iswrong ? question.id.ToString() : "");
						DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
					}
					else
					{
						bool flag4 = FPArray.InArray(question.id, examLogInfo.qidlist) >= 0;
						if (flag4)
						{
							int[] array2 = FPArray.SplitInt(examLogInfo.qidlist);
							int[] array3 = FPArray.SplitInt(examLogInfo.scorelist, ",", array2.Length);
							string[] array4 = FPArray.SplitString(examLogInfo.answerlist, "§", array2.Length);
							string text = "";
							string text2 = "";
							for (int j = 0; j < array2.Length; j++)
							{
								bool flag5 = array2[j] == question.id;
								if (flag5)
								{
									bool flag6 = array3[j] == 1 && iswrong;
									if (flag6)
									{
										array3[j] = 0;
										array4[j] = question.useranswer;
										examLogInfo.wrongs++;
										bool flag7 = num == sortinfo.id;
										if (flag7)
										{
											examLogInfo.curwrongs++;
										}
										bool flag8 = examLogInfo.wronglist != "";
										if (flag8)
										{
											ExamLogInfo expr_1F4 = examLogInfo;
											expr_1F4.wronglist += ",";
										}
										ExamLogInfo expr_20C = examLogInfo;
										expr_20C.wronglist += question.id;
									}
									else
									{
										bool flag9 = array3[j] == 0 && !iswrong;
										if (flag9)
										{
											array3[j] = 1;
											array4[j] = question.useranswer;
											examLogInfo.wrongs--;
											bool flag10 = num == sortinfo.id;
											if (flag10)
											{
												examLogInfo.curwrongs--;
											}
											string text3 = "";
											int[] array5 = FPArray.SplitInt(examLogInfo.wronglist);
											for (int k = 0; k < array5.Length; k++)
											{
												int num2 = array5[k];
												bool flag11 = num2 != question.id;
												if (flag11)
												{
													bool flag12 = text3 != "";
													if (flag12)
													{
														text3 += ",";
													}
													text3 += num2;
												}
											}
											examLogInfo.wronglist = text3;
										}
									}
								}
								bool flag13 = text != "";
								if (flag13)
								{
									text += ",";
								}
								text += array3[j];
								bool flag14 = text2 != "";
								if (flag14)
								{
									text2 += "§";
								}
								text2 += array4[j];
							}
							examLogInfo.scorelist = text;
							examLogInfo.answerlist = text2;
						}
						else
						{
							examLogInfo.answers++;
							examLogInfo.wrongs += (iswrong ? 1 : 0);
							bool flag15 = num == sortinfo.id;
							if (flag15)
							{
								examLogInfo.curwrongs += (iswrong ? 1 : 0);
							}
							ExamLogInfo expr_3EA = examLogInfo;
							expr_3EA.qidlist += ((examLogInfo.qidlist == "") ? question.id.ToString() : ("," + question.id.ToString()));
							if (iswrong)
							{
								ExamLogInfo expr_443 = examLogInfo;
								expr_443.scorelist += ((examLogInfo.scorelist == "") ? "0" : ",0");
								ExamLogInfo expr_473 = examLogInfo;
								expr_473.wronglist += ((examLogInfo.wronglist == "") ? question.id.ToString() : ("," + question.id.ToString()));
							}
							else
							{
								ExamLogInfo expr_4C5 = examLogInfo;
								expr_4C5.scorelist += ((examLogInfo.scorelist == "") ? "1" : ",1");
							}
							ExamLogInfo expr_4F6 = examLogInfo;
							expr_4F6.answerlist += ((examLogInfo.answerlist == "") ? question.useranswer : ("§" + question.useranswer));
						}
						DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
					}
				}
			}
		}

		public static ExpInfo GetExamExpByScore(double score, int examid)
		{
			bool flag = score < 0.0;
			if (flag)
			{
				score = 0.0;
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("examid", examid));
			list.Add(DbHelper.MakeAndWhere("scorelower", WhereType.LessThanEqual, score));
			int num = FPUtils.StrToInt(DbHelper.ExecuteMax<ExpInfo>("scoreupper"));
			bool flag2 = score >= (double)num;
			if (flag2)
			{
				list.Add(DbHelper.MakeAndWhere("scoreupper", num));
			}
			else
			{
				list.Add(DbHelper.MakeAndWhere("scoreupper", WhereType.GreaterThan, score));
			}
			return DbHelper.ExecuteModel<ExpInfo>(list.ToArray());
		}
	}
}
