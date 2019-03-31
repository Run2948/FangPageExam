using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP_Exam
{
	public class QuestionBll
	{
		public static void UpdateQuestionPost(string idlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, idlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ExamQuestion current in list)
			{
				SortBll.UpdateSortPosts(current.sortid, -1);
				TypeBll.UpdateTypePosts(current.typelist, current.sortid, -1);
			}
		}

		public static string GetQuestionTest(int channelid, int count, string type, string sort_type)
		{
			List<SortQuestion> list = new List<SortQuestion>();
			string[] array = FPArray.SplitString(sort_type);
			for (int i = 0; i < array.Length; i++)
			{
				string strContent = array[i];
				int[] sortid_typeid = FPArray.SplitInt(strContent, "_", 2);
				List<SortQuestion> list2 = QuestionBll.GetSortQuestionList().FindAll((SortQuestion item) => item.channelid == channelid && FPArray.InArray(item.type, type) >= 0 && item.sortid == sortid_typeid[0] && item.typeid == sortid_typeid[1]);
				bool flag = list2.Count > 0;
				if (flag)
				{
					list.AddRange(list2);
				}
			}
			string text = DbHelper.ExecuteField<SortQuestion>("questionlist", list);
			text = FPArray.RemoveSame(text);
			return QuestionBll.GetRandom(FPArray.SplitString(text), count);
		}

		public static string GetQuestionRandom(int channelid, int count, string type, int sortid, int typeid)
		{
			return QuestionBll.GetQuestionRandom(channelid, count, type, sortid, typeid, "");
		}

		public static string GetQuestionRandom(int channelid, int count, string type, int sortid, int typeid, string qidlist)
		{
			string text = QuestionBll.GetSortQuestions(channelid, type, sortid, typeid);
			bool flag = qidlist != "";
			if (flag)
			{
				text = FPArray.Remove(text, qidlist);
			}
			text = FPArray.RemoveSame(text);
			return QuestionBll.GetRandom(FPArray.SplitString(text), count);
		}

		public static string GetRandom(string[] idlist, int count)
		{
			bool flag = count > idlist.Length;
			if (flag)
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
				bool flag2 = array[num2];
				if (!flag2)
				{
					array[num2] = true;
					bool flag3 = text != "";
					if (flag3)
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

		public static string GetTopicQuestion(int channelid, ExamTopic examtopic)
		{
			string text = examtopic.questionlist;
			bool flag = examtopic.curquestions < examtopic.questions;
			if (flag)
			{
				bool flag2 = examtopic.randoms > 0;
				if (flag2)
				{
					string[] array = FPArray.SplitString(examtopic.randomsort);
					int[] array2 = FPArray.SplitInt(examtopic.randomcount, ",", array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						bool flag3 = array2[i] > 0;
						if (flag3)
						{
							int[] array3 = FPArray.SplitInt(array[i], "_", 2);
							string questionRandom = QuestionBll.GetQuestionRandom(channelid, array2[i], examtopic.type, array3[0], array3[1], text);
							bool flag4 = questionRandom != "";
							if (flag4)
							{
								text += ((text == "") ? questionRandom : ("," + questionRandom));
							}
						}
					}
					bool flag5 = examtopic.questions - examtopic.curquestions - examtopic.randoms > 0;
					if (flag5)
					{
						string questionRandom2 = QuestionBll.GetQuestionRandom(channelid, examtopic.questions - examtopic.curquestions - examtopic.randoms, examtopic.type, 0, 0, text);
						bool flag6 = questionRandom2 != "";
						if (flag6)
						{
							text += ((text == "") ? questionRandom2 : ("," + questionRandom2));
						}
					}
				}
				else
				{
					int count = examtopic.questions - examtopic.curquestions;
					string questionRandom3 = QuestionBll.GetQuestionRandom(channelid, count, examtopic.type, 0, 0, text);
					bool flag7 = questionRandom3 != "";
					if (flag7)
					{
						text += ((text == "") ? questionRandom3 : ("," + questionRandom3));
					}
				}
			}
			return text;
		}

		public static List<ExamQuestion> GetQuestionList(string qidlist)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, qidlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPArray.SplitInt(qidlist);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int num = array2[i];
				bool flag = num == 0;
				if (!flag)
				{
					bool flag2 = false;
					foreach (ExamQuestion current in list)
					{
						bool flag3 = current.id == num;
						if (flag3)
						{
							list2.Add(current);
							flag2 = true;
							break;
						}
					}
					bool flag4 = !flag2;
					if (flag4)
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

		public static void UpdateSortQuestion(int channelid, int sortid)
		{
			string[] array = FPArray.SplitString("TYPE_RADIO,TYPE_MULTIPLE,TYPE_TRUE_FALSE,TYPE_BLANK,TYPE_ANSWER,TYPE_OPERATION");
			for (int i = 0; i < array.Length; i++)
			{
				string type = array[i];
				QuestionBll.UpdateSortQuestion(channelid, sortid, type, 0);
			}
		}

		public static void UpdateSortQuestion(int channelid, int sortid, string typelist)
		{
			string[] array = FPArray.SplitString("TYPE_RADIO,TYPE_MULTIPLE,TYPE_TRUE_FALSE,TYPE_BLANK,TYPE_ANSWER,TYPE_OPERATION");
			for (int i = 0; i < array.Length; i++)
			{
				string type = array[i];
				QuestionBll.UpdateSortQuestion(channelid, sortid, type, typelist);
			}
		}

		public static void UpdateSortQuestion(int channelid, int sortid, string type, string typelist)
		{
			bool flag = typelist != "";
			if (flag)
			{
				int[] array = FPArray.SplitInt(typelist);
				for (int i = 0; i < array.Length; i++)
				{
					int num = array[i];
					bool flag2 = num > 0;
					if (flag2)
					{
						QuestionBll.UpdateSortQuestion(channelid, sortid, type, num);
					}
				}
			}
		}

		public static void UpdateSortQuestion(int channelid, int sortid, string type, int typeid)
		{
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("channelid", channelid));
			list.Add(DbHelper.MakeAndWhere("type", type));
			list.Add(DbHelper.MakeAndWhere("sortid", sortid));
			list.Add(DbHelper.MakeAndWhere("typeid", typeid));
			SortQuestion sortQuestion = DbHelper.ExecuteModel<SortQuestion>(list.ToArray());
			list.RemoveAt(2);
			list.RemoveAt(2);
			string childSorts = SortBll.GetChildSorts(sortid);
			list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
			bool flag = typeid > 0;
			if (flag)
			{
				list.Add(DbHelper.MakeAndWhere("typelist", WhereType.Contain, typeid));
			}
			sortQuestion.questionlist = DbHelper.ExecuteField<ExamQuestion>(list.ToArray());
			bool flag2 = sortQuestion.questionlist != "";
			if (flag2)
			{
				sortQuestion.counts = FPArray.SplitInt(sortQuestion.questionlist).Length;
			}
			else
			{
				sortQuestion.counts = 0;
			}
			bool flag3 = sortQuestion.id > 0;
			if (flag3)
			{
				DbHelper.ExecuteUpdate<SortQuestion>(sortQuestion);
			}
			else
			{
				sortQuestion.channelid = channelid;
				sortQuestion.sortid = sortid;
				sortQuestion.type = type;
				sortQuestion.typeid = typeid;
				DbHelper.ExecuteInsert<SortQuestion>(sortQuestion);
			}
			FPCache.Remove("FP_SORTQUESTIONLIST");
		}

		public static string GetSortQuestions(int channelid, string type, int sortid, int typeid)
		{
			string result = "";
			bool flag = sortid > 0;
			if (flag)
			{
				List<SortQuestion> list = QuestionBll.GetSortQuestionList().FindAll((SortQuestion item) => item.channelid == channelid && item.type == type && item.sortid == sortid && item.typeid == typeid);
				bool flag2 = list.Count > 0;
				if (flag2)
				{
					result = list[0].questionlist;
				}
			}
			else
			{
				List<SortQuestion> list2 = QuestionBll.GetSortQuestionList().FindAll((SortQuestion item) => item.channelid == channelid && item.type == type && item.typeid == 0);
				result = DbHelper.ExecuteField<SortQuestion>("questionlist", list2);
			}
			return result;
		}

		public static List<SortQuestion> GetSortQuestionList()
		{
			object obj = FPCache.Get("FP_SORTQUESTIONLIST");
			bool flag = obj != null;
			List<SortQuestion> list;
			if (flag)
			{
				list = (obj as List<SortQuestion>);
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeOrderBy("channelid", OrderBy.ASC);
				list = DbHelper.ExecuteList<SortQuestion>(new SqlParam[]
				{
					sqlParam
				});
				CacheBll.Insert("考试题库信息缓存", "FP_SORTQUESTIONLIST", list);
			}
			return list;
		}

		public static void ResetQuestionSort()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", "question");
			SortAppInfo sortAppInfo = DbHelper.ExecuteModel<SortAppInfo>(new SqlParam[]
			{
				sqlParam
			});
			bool flag = sortAppInfo.id > 0;
			if (flag)
			{
				SqlParam sqlParam2 = DbHelper.MakeAndWhere("appid", sortAppInfo.id);
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
				{
					sqlParam2
				});
				SortBll.ResetSortPosts<ExamQuestion>(list);
				foreach (SortInfo current in list)
				{
					TypeBll.ResetTypePosts<ExamQuestion>(current.types, current.id);
					QuestionBll.UpdateSortQuestion(current.channelid, current.id);
					bool flag2 = current.types != "";
					if (flag2)
					{
						QuestionBll.UpdateSortQuestion(current.channelid, current.id, current.types);
					}
				}
				sqlParam2 = DbHelper.MakeAndWhere("type", WhereType.In, "'TYPE_RADIO,TYPE_MULTIPLE,TYPE_TRUE_FALSE,TYPE_BLANK,TYPE_ANSWER,TYPE_OPERATION'");
				List<ExamQuestion> list2 = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
				{
					sqlParam2
				});
				for (int i = 0; i < list2.Count; i++)
				{
					bool flag3 = list2[i].type == "TYPE_RADIO" || list2[i].type == "TYPE_MULTIPLE";
					if (flag3)
					{
						string text = "";
						int num = 0;
						string[] array = FPArray.SplitString(list2[i].content, "§");
						for (int j = 0; j < array.Length; j++)
						{
							string text2 = array[j];
							bool flag4 = text2 != "";
							if (flag4)
							{
								bool flag5 = text != "";
								if (flag5)
								{
									text += "§";
								}
								text += text2;
								num++;
							}
						}
						list2[i].content = text;
						list2[i].ascount = num;
					}
					else
					{
						bool flag6 = list2[i].type == "TYPE_BLANK";
						if (flag6)
						{
							string text3 = "";
							int num2 = 0;
							string[] array2 = FPArray.SplitString(list2[i].answer, ",");
							for (int k = 0; k < array2.Length; k++)
							{
								string text4 = array2[k];
								bool flag7 = text4 != "";
								if (flag7)
								{
									bool flag8 = text3 != "";
									if (flag8)
									{
										text3 += ",";
									}
									text3 += text4;
									num2++;
								}
							}
							list2[i].answer = text3;
							list2[i].ascount = num2;
						}
						else
						{
							bool flag9 = list2[i].type == "TYPE_ANSWER";
							if (flag9)
							{
								string text5 = "";
								int num3 = 0;
								string[] array3 = FPArray.SplitString(list2[i].answerkey, ",");
								for (int l = 0; l < array3.Length; l++)
								{
									string text6 = array3[l];
									bool flag10 = text6 != "";
									if (flag10)
									{
										bool flag11 = text5 != "";
										if (flag11)
										{
											text5 += ",";
										}
										text5 += text6;
										num3++;
									}
								}
								list2[i].answerkey = text5;
								list2[i].ascount = num3;
							}
							else
							{
								bool flag12 = list2[i].type == "TYPE_OPERATION";
								if (flag12)
								{
									list2[i].ascount = 1;
								}
							}
						}
					}
					bool flag13 = list2[i].ascount == 0;
					if (flag13)
					{
						list2[i].ascount = 1;
					}
					DbHelper.ExecuteUpdate<ExamQuestion>(list2[i]);
				}
			}
		}
	}
}
