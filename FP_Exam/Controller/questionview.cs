using System;
using System.Collections.Generic;
using FangPage.Data;
using FP_Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000032 RID: 50
	public class questionview : LoginController
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000155E0 File Offset: 0x000137E0
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.sortinfo.id == 0)
			{
				this.ShowErr("对不起，该题库不存在或已被删除。");
			}
			else
			{
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("sortid", this.sortid),
					DbHelper.MakeAndWhere("uid", this.userid)
				};
				this.examloginfo = DbHelper.ExecuteModel<ExamLogInfo>(sqlparams);
				if (this.examloginfo.sortid != 0)
				{
					this.channelid = this.examloginfo.channelid;
					this.examloginfo.questions = this.sortinfo.posts;
					this.examloglist = ExamBll.GetExamLogList(this.channelid, this.userid);
					string text = "";
					if (this.action == "wrong")
					{
						this.pagenav = string.Concat(new object[]
						{
							"错题(",
							this.sortinfo.name,
							")共",
							this.examloginfo.wrongs,
							"道题目"
						});
						text = this.examloginfo.wronglist;
					}
					else if (this.action == "note")
					{
						this.pagenav = string.Concat(new object[]
						{
							"笔记(",
							this.sortinfo.name,
							")共",
							this.examloginfo.notes,
							"道题目"
						});
						text = this.examloginfo.notelist;
					}
					else if (this.action == "fav")
					{
						this.pagenav = string.Concat(new object[]
						{
							"收藏(",
							this.sortinfo.name,
							")共",
							this.examloginfo.favs,
							"道题目"
						});
						text = this.examloginfo.favlist;
					}
					if (text != "")
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, text);
						OrderByParam orderby = DbHelper.MakeOrderBy("type", OrderBy.ASC);
						this.questionlist = DbHelper.ExecuteList<ExamQuestion>(orderby, new SqlParam[]
						{
							sqlParam
						});
						SqlParam[] sqlparams2 = new SqlParam[]
						{
							DbHelper.MakeAndWhere("qid", WhereType.In, text),
							DbHelper.MakeAndWhere("uid", this.userid)
						};
						List<ExamNote> list = DbHelper.ExecuteList<ExamNote>(sqlparams2);
						for (int i = 0; i < this.questionlist.Count; i++)
						{
							if (FPUtils.InArray(this.questionlist[i].id, this.examloginfo.favlist))
							{
								this.questionlist[i].isfav = 1;
							}
							foreach (ExamNote examNote in list)
							{
								if (examNote.qid == this.questionlist[i].id)
								{
									this.questionlist[i].note = examNote.note;
								}
							}
							if (this.examloglist.ContainsKey(this.questionlist[i].sortid))
							{
								ExamLogInfo examLogInfo = this.examloglist[this.questionlist[i].sortid];
								if (FPUtils.InArray(this.questionlist[i].id, examLogInfo.qidlist))
								{
									int[] array = FPUtils.SplitInt(examLogInfo.qidlist);
									string[] array2 = FPUtils.SplitString(examLogInfo.optionlist, "|", array.Length);
									string[] array3 = FPUtils.SplitString(examLogInfo.answerlist, "§", array.Length);
									for (int j = 0; j < array.Length; j++)
									{
										if (array[j] == this.questionlist[i].id)
										{
											this.questionlist[i].useranswer = array3[j];
											this.questionlist[i].optionlist = array2[j];
											this.questionlist[i].answer = this.OptionAnswer(array2[j], this.questionlist[i].answer);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00015B24 File Offset: 0x00013D24
		protected string FmAnswer(string content, int tid, string uanswer)
		{
			return content.Replace("(#answer)", string.Format("<input type=\"text\" id=\"answer_{0}\" name=\"answer_{0}\" value=\"{1}\" class=\"tkt\" readonly=\"readonly\"/>", tid, uanswer));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00015B54 File Offset: 0x00013D54
		protected string Option(string[] opstr, int ascount, string optionlist)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			int[] array2 = FPUtils.SplitInt(optionlist, ",", ascount);
			string text = "";
			if (ascount > opstr.Length)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				if (optionlist != "")
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						array[i],
						".",
						opstr[array2[i]],
						"<br/>"
					});
				}
				else
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						array[i],
						".",
						opstr[i],
						"<br/>"
					});
				}
			}
			return text;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00015C48 File Offset: 0x00013E48
		private string OptionAnswer(string optionlist, string answer)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			int[] array2 = FPUtils.SplitInt(optionlist);
			string text = "";
			for (int i = 0; i < array2.Length; i++)
			{
				if (FPUtils.InArray(array[array2[i]], answer))
				{
					if (text != "")
					{
						text += ",";
					}
					text += array[i];
				}
			}
			return text;
		}

		// Token: 0x040000FA RID: 250
		protected int channelid;

		// Token: 0x040000FB RID: 251
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x040000FC RID: 252
		protected ExamLogInfo examloginfo = new ExamLogInfo();

		// Token: 0x040000FD RID: 253
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x040000FE RID: 254
		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		// Token: 0x040000FF RID: 255
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F"
		};

		// Token: 0x04000100 RID: 256
		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();

		// Token: 0x04000101 RID: 257
		protected ExamConfig examconfig = new ExamConfig();
	}
}
