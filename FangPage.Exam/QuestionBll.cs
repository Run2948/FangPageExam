using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam
{
	// Token: 0x02000006 RID: 6
	public class QuestionBll
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00003D4C File Offset: 0x00001F4C
		public static string GetQuestionSorts(string idlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, idlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			foreach (ExamQuestion examQuestion in list)
			{
				if (text != "")
				{
					text += ",";
				}
				text += examQuestion.sortid;
			}
			return text;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003E04 File Offset: 0x00002004
		public static string GetQuestionRandom(int channelid, int count, string type, string sidlist)
		{
			return QuestionBll.GetQuestionRandom(channelid, count, type, sidlist, "");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003E24 File Offset: 0x00002024
		public static string GetQuestionRandom(int channelid, int count, string type, string sidlist, string qidlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT [id] FROM [{0}Exam_ExamQuestion] WHERE [status]=1", DbConfigs.Prefix);
			if (channelid > 0)
			{
				stringBuilder.AppendFormat(" AND [channelid]={0}", channelid);
			}
			if (type != "")
			{
				stringBuilder.AppendFormat(" AND [type] IN({0})", type);
			}
			if (sidlist != "")
			{
				stringBuilder.AppendFormat(" AND [sortid] IN({0})", sidlist);
			}
			if (qidlist != "")
			{
				stringBuilder.AppendFormat(" AND [id] NOT IN({0})", qidlist);
			}
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString());
			string text = "";
			while (dataReader.Read())
			{
				if (text != "")
				{
					text += ",";
				}
				text += dataReader["id"].ToString();
			}
			dataReader.Close();
			return QuestionBll.GetRandom(FPUtils.SplitString(text), count);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003F4C File Offset: 0x0000214C
		public static string GetQuestionRandom(int channelid, int count, string type, int sortid, string qidlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT [id] FROM [{0}Exam_ExamQuestion] WHERE [status]=1", DbConfigs.Prefix);
			if (channelid > 0)
			{
				stringBuilder.AppendFormat(" AND [channelid]={0}", channelid);
			}
			if (type != "")
			{
				stringBuilder.AppendFormat(" AND [type] IN({0})", type);
			}
			if (sortid > 0)
			{
				SortInfo sortInfo = SortBll.GetSortInfo(sortid);
				string childSorts = SortBll.GetChildSorts(sortInfo);
				stringBuilder.AppendFormat(" AND [sortid] IN({0})", childSorts);
			}
			if (qidlist != "")
			{
				stringBuilder.AppendFormat(" AND [id] NOT IN({0})", qidlist);
			}
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString());
			string text = "";
			while (dataReader.Read())
			{
				if (text != "")
				{
					text += ",";
				}
				text += dataReader["id"].ToString();
			}
			dataReader.Close();
			return QuestionBll.GetRandom(FPUtils.SplitString(text), count);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004084 File Offset: 0x00002284
		public static string GetRandom(string[] idlist, int count)
		{
			if (count > idlist.Length)
			{
				count = idlist.Length;
			}
			string text = "";
			bool[] array = new bool[idlist.Length];
			int num = 0;
			do
			{
				Random random = new Random();
				int num2 = random.Next(idlist.Length);
				if (!array[num2])
				{
					array[num2] = true;
					if (text != "")
					{
						text += ",";
					}
					text += idlist[num2];
					num++;
				}
			}
			while (num < count);
			return text;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004124 File Offset: 0x00002324
		public static string GetTopicQuestion(int channelid, ExamTopic examtopic)
		{
			string text = examtopic.questionlist;
			string type;
			if (examtopic.type == -1)
			{
				type = "1,2";
			}
			else
			{
				type = examtopic.type.ToString();
			}
			if (examtopic.curquestions < examtopic.questions)
			{
				if (examtopic.randoms > 0)
				{
					int[] array = FPUtils.SplitInt(examtopic.randomsort);
					int[] array2 = FPUtils.SplitInt(examtopic.randomcount, ",", array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						if (array2[i] > 0)
						{
							string questionRandom = QuestionBll.GetQuestionRandom(channelid, array2[i], type, array[i], text);
							if (questionRandom != "")
							{
								text += ((text == "") ? questionRandom : ("," + questionRandom));
							}
						}
					}
					if (examtopic.questions - examtopic.curquestions - examtopic.randoms > 0)
					{
						string questionRandom = QuestionBll.GetQuestionRandom(channelid, examtopic.questions - examtopic.curquestions - examtopic.randoms - 1, type, "", text);
						if (questionRandom != "")
						{
							text += ((text == "") ? questionRandom : ("," + questionRandom));
						}
					}
				}
				else
				{
					int count = examtopic.questions - examtopic.curquestions;
					string questionRandom = QuestionBll.GetQuestionRandom(channelid, count, type, "", text);
					if (questionRandom != "")
					{
						text += ((text == "") ? questionRandom : ("," + questionRandom));
					}
				}
			}
			return text;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004328 File Offset: 0x00002528
		public static List<ExamQuestion> GetQuestionList(string qidlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, qidlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPUtils.SplitInt(qidlist);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			foreach (int num in array)
			{
				if (num != 0)
				{
					bool flag = false;
					foreach (ExamQuestion examQuestion in list)
					{
						if (examQuestion.id == num)
						{
							list2.Add(examQuestion);
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list2.Add(new ExamQuestion
						{
							id = num
						});
					}
				}
			}
			return list2;
		}
	}
}
