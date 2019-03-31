using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam
{
	// Token: 0x02000004 RID: 4
	public class ExamBll
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000311C File Offset: 0x0000131C
		public static string GetExamSorts(string idlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, idlist);
			List<ExamInfo> list = DbHelper.ExecuteList<ExamInfo>(new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			foreach (ExamInfo examInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				text += examInfo.sortid;
			}
			return text;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000031D4 File Offset: 0x000013D4
		public static void AddExamLarge(int examid)
		{
			ExamTopic examTopic = new ExamTopic();
			examTopic.type = 1;
			examTopic.title = "第一题、单选题";
			examTopic.examid = examid;
			examTopic.perscore = 2.0;
			examTopic.display = 1;
			examTopic.questions = 30;
			examTopic.paper = 1;
			DbHelper.ExecuteInsert<ExamTopic>(examTopic);
			examTopic.type = 2;
			examTopic.title = "第二题、多选题";
			examTopic.examid = examid;
			examTopic.perscore = 4.0;
			examTopic.display = 2;
			examTopic.questions = 10;
			examTopic.paper = 1;
			DbHelper.ExecuteInsert<ExamTopic>(examTopic);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003280 File Offset: 0x00001480
		public static List<ExamInfo> GetExamList(int top)
		{
			return DbHelper.ExecuteTopList<ExamInfo>(top);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003298 File Offset: 0x00001498
		public static List<ExamTopic> GetExamTopicList(int examid, int paper)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("examid", examid),
				DbHelper.MakeAndWhere("paper", paper)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<ExamTopic>(orderby, sqlparams);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000032EC File Offset: 0x000014EC
		public static Dictionary<int, ExamLogInfo> GetExamLogList(int channelid, int uid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("uid", uid)
			};
			List<ExamLogInfo> list = DbHelper.ExecuteList<ExamLogInfo>(sqlparams);
			Dictionary<int, ExamLogInfo> dictionary = new Dictionary<int, ExamLogInfo>();
			foreach (ExamLogInfo examLogInfo in list)
			{
				dictionary.Add(examLogInfo.sortid, examLogInfo);
			}
			return dictionary;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003394 File Offset: 0x00001594
		public static ExamLogInfo GetExamLogInfo(int uid, int sortid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("uid", uid),
				DbHelper.MakeAndWhere("sortid", sortid)
			};
			return DbHelper.ExecuteModel<ExamLogInfo>(sqlparams);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000033DC File Offset: 0x000015DC
		public static List<ExamResult> GetExamResultList(int channelid, int uid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("uid", uid)
			};
			return DbHelper.ExecuteList<ExamResult>(sqlparams);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003424 File Offset: 0x00001624
		public static ExamResult GetExamResult(int resultid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", resultid);
			return DbHelper.ExecuteModel<ExamResult>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003458 File Offset: 0x00001658
		public static List<ExamResultTopic> GetExamResultTopicList(int resultid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("resultid", resultid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<ExamResultTopic>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000349C File Offset: 0x0000169C
		public static void UpdateExamLog(SortInfo sortinfo, int uid, ExamQuestion question, bool iswrong)
		{
			foreach (int num in FPUtils.SplitInt(sortinfo.parentlist))
			{
				if (num != 0)
				{
					ExamLogInfo examLogInfo = ExamBll.GetExamLogInfo(uid, num);
					if (examLogInfo.sortid == 0)
					{
						examLogInfo.sortid = num;
						examLogInfo.uid = uid;
						examLogInfo.channelid = sortinfo.channelid;
						examLogInfo.answers = 1;
						examLogInfo.wrongs = (iswrong ? 1 : 0);
						if (num == sortinfo.id)
						{
							examLogInfo.curwrongs = (iswrong ? 1 : 0);
						}
						examLogInfo.qidlist = question.id.ToString();
						examLogInfo.optionlist = question.optionlist;
						examLogInfo.answerlist = question.useranswer;
						examLogInfo.scorelist = (iswrong ? "0" : "1");
						examLogInfo.wronglist = (iswrong ? question.id.ToString() : "");
						DbHelper.ExecuteInsert<ExamLogInfo>(examLogInfo);
					}
					else
					{
						if (FPUtils.InArray(question.id, examLogInfo.qidlist))
						{
							int[] array2 = FPUtils.SplitInt(examLogInfo.qidlist);
							string[] array3 = FPUtils.SplitString(examLogInfo.optionlist, "|", array2.Length);
							int[] array4 = FPUtils.SplitInt(examLogInfo.scorelist, ",", array2.Length);
							string[] array5 = FPUtils.SplitString(examLogInfo.answerlist, "§", array2.Length);
							string text = "";
							string text2 = "";
							string text3 = "";
							for (int j = 0; j < array2.Length; j++)
							{
								if (array2[j] == question.id)
								{
									if (array4[j] == 1 && iswrong)
									{
										array4[j] = 0;
										array5[j] = question.useranswer;
										array3[j] = question.optionlist;
										examLogInfo.wrongs++;
										if (num == sortinfo.id)
										{
											examLogInfo.curwrongs++;
										}
										if (examLogInfo.wronglist != "")
										{
											ExamLogInfo examLogInfo2 = examLogInfo;
											examLogInfo2.wronglist += ",";
										}
										ExamLogInfo examLogInfo3 = examLogInfo;
										examLogInfo3.wronglist += question.id;
									}
									else if (array4[j] == 0 && !iswrong)
									{
										array4[j] = 1;
										array5[j] = question.useranswer;
										array3[j] = question.optionlist;
										examLogInfo.wrongs--;
										if (num == sortinfo.id)
										{
											examLogInfo.curwrongs--;
										}
										string text4 = "";
										foreach (int num2 in FPUtils.SplitInt(examLogInfo.wronglist))
										{
											if (num2 != question.id)
											{
												if (text4 != "")
												{
													text4 += ",";
												}
												text4 += num2;
											}
										}
										examLogInfo.wronglist = text4;
									}
								}
								if (text != "")
								{
									text += ",";
								}
								text += array4[j];
								if (text2 != "")
								{
									text2 += "§";
								}
								text2 += array5[j];
								if (text3 != "")
								{
									text3 += "|";
								}
								text3 += array3[j];
							}
							examLogInfo.scorelist = text;
							examLogInfo.answerlist = text2;
							examLogInfo.optionlist = text3;
						}
						else
						{
							examLogInfo.answers++;
							examLogInfo.wrongs += (iswrong ? 1 : 0);
							if (num == sortinfo.id)
							{
								examLogInfo.curwrongs += (iswrong ? 1 : 0);
							}
							ExamLogInfo examLogInfo4 = examLogInfo;
							examLogInfo4.qidlist += ((examLogInfo.qidlist == "") ? question.id.ToString() : ("," + question.id.ToString()));
							if (iswrong)
							{
								ExamLogInfo examLogInfo5 = examLogInfo;
								examLogInfo5.scorelist += ((examLogInfo.scorelist == "") ? "0" : ",0");
								ExamLogInfo examLogInfo6 = examLogInfo;
								examLogInfo6.wronglist += ((examLogInfo.wronglist == "") ? question.id.ToString() : ("," + question.id.ToString()));
							}
							else
							{
								ExamLogInfo examLogInfo7 = examLogInfo;
								examLogInfo7.scorelist += ((examLogInfo.scorelist == "") ? "1" : ",1");
							}
							ExamLogInfo examLogInfo8 = examLogInfo;
							examLogInfo8.answerlist += ((examLogInfo.answerlist == "") ? question.useranswer : ("§" + question.useranswer));
							ExamLogInfo examLogInfo9 = examLogInfo;
							examLogInfo9.optionlist += ((examLogInfo.optionlist == "") ? question.optionlist : ("|" + question.optionlist));
						}
						DbHelper.ExecuteUpdate<ExamLogInfo>(examLogInfo);
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public static ExpInfo GetExamExpByScore(double score, int examid)
		{
			if (score < 0.0)
			{
				score = 0.0;
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("examid", examid));
			list.Add(DbHelper.MakeAndWhere("scorelower", WhereType.LessThanEqual, score));
			int num = FPUtils.StrToInt(DbHelper.ExecuteMax<ExpInfo>("scoreupper"));
			if (score >= (double)num)
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
