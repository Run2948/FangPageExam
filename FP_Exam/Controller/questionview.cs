using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class questionview : LoginController
	{
		protected int channelid;

		protected int sortid = FPRequest.GetInt("sortid");

		protected ExamLogInfo examloginfo = new ExamLogInfo();

		protected SortInfo sortinfo = new SortInfo();

		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H"
		};

		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();

		protected ExamConfig examconfig = new ExamConfig();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			bool flag = this.examconfig.teststatus == 0;
			if (flag)
			{
				this.ShowErr("对不起，系统已关闭了用户练习，不能再查看题目。");
			}
			else
			{
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool flag2 = this.sortinfo.id == 0;
				if (flag2)
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
					bool flag3 = this.examloginfo.sortid == 0;
					if (!flag3)
					{
						this.channelid = this.examloginfo.channelid;
						this.examloginfo.questions = this.sortinfo.posts;
						this.examloglist = ExamBll.GetExamLogList(this.channelid, this.userid);
						string text = "";
						bool flag4 = this.action == "wrong";
						if (flag4)
						{
							this.pagenav = string.Concat(new object[]
							{
								"我的错题(",
								this.sortinfo.name,
								")共",
								this.examloginfo.wrongs,
								"道题目"
							});
							text = this.examloginfo.wronglist;
						}
						else
						{
							bool flag5 = this.action == "note";
							if (flag5)
							{
								this.pagenav = string.Concat(new object[]
								{
									"我的笔记(",
									this.sortinfo.name,
									")共",
									this.examloginfo.notes,
									"道题目"
								});
								text = this.examloginfo.notelist;
							}
							else
							{
								bool flag6 = this.action == "fav";
								if (flag6)
								{
									this.pagenav = string.Concat(new object[]
									{
										"我的收藏(",
										this.sortinfo.name,
										")共",
										this.examloginfo.favs,
										"道题目"
									});
									text = this.examloginfo.favlist;
								}
							}
						}
						bool flag7 = text != "";
						if (flag7)
						{
							SqlParam[] sqlparams2 = new SqlParam[]
							{
								DbHelper.MakeAndWhere("id", WhereType.In, text),
								DbHelper.MakeOrderBy("display", OrderBy.ASC)
							};
							this.questionlist = DbHelper.ExecuteList<ExamQuestion>(sqlparams2);
							SqlParam[] sqlparams3 = new SqlParam[]
							{
								DbHelper.MakeAndWhere("qid", WhereType.In, text),
								DbHelper.MakeAndWhere("uid", this.userid)
							};
							List<ExamNote> list = DbHelper.ExecuteList<ExamNote>(sqlparams3);
							for (int i = 0; i < this.questionlist.Count; i++)
							{
								bool flag8 = FPArray.InArray(this.questionlist[i].id, this.examloginfo.favlist) >= 0;
								if (flag8)
								{
									this.questionlist[i].isfav = 1;
								}
								foreach (ExamNote current in list)
								{
									bool flag9 = current.qid == this.questionlist[i].id;
									if (flag9)
									{
										this.questionlist[i].note = current.note;
									}
								}
								bool flag10 = this.examloglist.ContainsKey(this.questionlist[i].sortid);
								if (flag10)
								{
									ExamLogInfo examLogInfo = this.examloglist[this.questionlist[i].sortid];
									bool flag11 = FPArray.InArray(this.questionlist[i].id, examLogInfo.qidlist) >= 0;
									if (flag11)
									{
										int[] array = FPArray.SplitInt(examLogInfo.qidlist);
										string[] array2 = FPArray.SplitString(examLogInfo.answerlist, "§", array.Length);
										for (int j = 0; j < array.Length; j++)
										{
											bool flag12 = array[j] == this.questionlist[i].id;
											if (flag12)
											{
												this.questionlist[i].useranswer = array2[j];
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		protected string FmAnswer(string content, int tid, string uanswer)
		{
			return content.Replace("(#answer)", string.Format("<input type=\"text\" id=\"answer_{0}\" name=\"answer_{0}\" value=\"{1}\" class=\"tkt\" readonly=\"readonly\"/>", tid, uanswer));
		}

		protected string Option(string[] opstr, int ascount)
		{
			string[] array = FPArray.SplitString("A,B,C,D,E,F,G,H");
			string text = "";
			bool flag = ascount > opstr.Length;
			if (flag)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					array[i],
					".",
					opstr[i],
					"<br/>"
				});
			}
			return text;
		}

		private string OptionAnswer(string optionlist, string answer)
		{
			string[] array = FPArray.SplitString("A,B,C,D,E,F,G,H");
			int[] array2 = FPArray.SplitInt(optionlist);
			string text = "";
			for (int i = 0; i < array2.Length; i++)
			{
				bool flag = FPArray.InArray(array[array2[i]], answer) >= 0;
				if (flag)
				{
					bool flag2 = text != "";
					if (flag2)
					{
						text += ",";
					}
					text += array[i];
				}
			}
			return text;
		}
	}
}
