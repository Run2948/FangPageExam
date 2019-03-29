using System;
using System.Collections.Generic;
using System.Data;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000026 RID: 38
	public class exampreview : LoginController
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000104F4 File Offset: 0x0000E6F4
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				this.starttime = DbUtils.GetDateTime();
				this.examconfig = ExamConifgs.GetExamConfig();
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
				if (this.examinfo.id == 0)
				{
					this.ShowErr("对不起，该考试已不存在或者被删除。");
				}
				else
				{
					this.sortid = this.examinfo.sortid;
					this.sortinfo = SortBll.GetSortInfo(this.sortid);
					if (!this.isperm && this.examinfo.uid != this.userid)
					{
						this.ShowErr("对不起，您没有权限预览该试卷。");
					}
					else
					{
						this.examinfo.passmark = this.examinfo.passmark * this.examinfo.total / 100.0;
						this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
						if (this.ispost)
						{
							string @string = FPRequest.GetString("papersize");
							int @int = FPRequest.GetInt("papertype");
							AsposeWordApp asposeWordApp = new AsposeWordApp();
							if (@string == "a4")
							{
								asposeWordApp.Open(FPUtils.GetMapPath("admin/images/exampaper_a4.doc"));
							}
							else
							{
								asposeWordApp.Open(FPUtils.GetMapPath("admin/images/exampaper_a3.doc"));
							}
							asposeWordApp.InsertText("examtitle", this.examinfo.name);
							asposeWordApp.InsertText("subtitle", this.GetPaper(this.paper));
							asposeWordApp.MoveToBookmark("content");
							DataTable dataTable = new DataTable();
							DataColumn column = new DataColumn("s0", Type.GetType("System.String"));
							dataTable.Columns.Add(column);
							int num = 1;
							foreach (ExamTopic examTopic in this.examtopiclist)
							{
								column = new DataColumn("s" + num, Type.GetType("System.String"));
								dataTable.Columns.Add(column);
								num++;
							}
							column = new DataColumn("s" + num, Type.GetType("System.String"));
							dataTable.Columns.Add(column);
							DataRow dataRow = dataTable.NewRow();
							dataRow["s0"] = "题  号";
							num = 1;
							foreach (ExamTopic examTopic in this.examtopiclist)
							{
								dataRow["s" + num] = this.GetNum(num);
								num++;
							}
							dataRow["s" + num] = "总  分";
							dataTable.Rows.Add(dataRow);
							dataRow = dataTable.NewRow();
							dataRow["s0"] = "得  分";
							num = 1;
							foreach (ExamTopic examTopic in this.examtopiclist)
							{
								dataRow["s" + num] = "";
								num++;
							}
							dataRow["s" + num] = "";
							dataTable.Rows.Add(dataRow);
							asposeWordApp.InsertTable(dataTable, true);
							asposeWordApp.InsertLineBreak();
							num = 1;
							foreach (ExamTopic examTopic in this.examtopiclist)
							{
								asposeWordApp.InsertScoreTable(true, true, string.Concat(new object[]
								{
									examTopic.title,
									"  (共",
									examTopic.questions,
									"题，每题",
									examTopic.perscore,
									"分，共",
									(double)examTopic.questions * examTopic.perscore,
									"分)"
								}));
								foreach (ExamQuestion examQuestion in this.GetQuestionList(FPRequest.GetString("qidlist_" + examTopic.id)))
								{
									if (examQuestion.type == 1 || examQuestion.type == 2)
									{
										if (@int == 1)
										{
											asposeWordApp.Writeln(string.Concat(new object[]
											{
												num,
												"、",
												examQuestion.title,
												"  ",
												examQuestion.answer
											}), 12.0, false, "left");
											asposeWordApp.Writeln(this.Option(asposeWordApp, examQuestion.option, examQuestion.ascount, examQuestion.optionlist), 12.0, false, "left");
											if (examQuestion.explain != "")
											{
												asposeWordApp.Writeln("答案解析：" + examQuestion.explain, 12.0, false, "left");
											}
										}
										else
										{
											asposeWordApp.Writeln(num + "、" + examQuestion.title, 12.0, false, "left");
											asposeWordApp.Writeln(this.Option(asposeWordApp, examQuestion.option, examQuestion.ascount, examQuestion.optionlist), 12.0, false, "left");
										}
									}
									else if (examQuestion.type == 3)
									{
										if (@int == 1)
										{
											asposeWordApp.Writeln((string.Concat(new object[]
											{
												num,
												"、",
												examQuestion.title,
												"  (",
												examQuestion.answer
											}) == "Y") ? "正确" : "错误)", 12.0, false, "left");
											if (examQuestion.explain != "")
											{
												asposeWordApp.Writeln("答案解析：" + examQuestion.explain, 12.0, false, "left");
											}
										}
										else
										{
											asposeWordApp.Writeln(string.Concat(new object[]
											{
												num,
												"、",
												examQuestion.title,
												"  (    )"
											}), 12.0, false, "left");
										}
									}
									else if (examQuestion.type == 4)
									{
										if (@int == 1)
										{
											asposeWordApp.Writeln(num + "、" + this.FmAnswer(examQuestion.title), 12.0, false, "left");
											asposeWordApp.Writeln("答案：" + examQuestion.answer, 12.0, false, "left");
											if (examQuestion.explain != "")
											{
												asposeWordApp.Writeln("答案解析：" + examQuestion.explain, 12.0, false, "left");
											}
										}
										else
										{
											asposeWordApp.Writeln(num + "、" + this.FmAnswer(examQuestion.title), 12.0, false, "left");
										}
									}
									else if (examQuestion.type == 5)
									{
										if (@int == 1)
										{
											asposeWordApp.Writeln(num + "、" + examQuestion.title, 12.0, false, "left");
											asposeWordApp.Writeln("答：" + examQuestion.answer, 12.0, false, "left");
											if (examQuestion.explain != "")
											{
												asposeWordApp.Writeln("答案解析：" + examQuestion.explain, 12.0, false, "left");
											}
										}
										else
										{
											asposeWordApp.Writeln(num + "、" + examQuestion.title, 12.0, false, "left");
											asposeWordApp.InsertLineBreak(6);
										}
									}
									else if (examQuestion.type == 6)
									{
										asposeWordApp.Writeln(num + "、" + examQuestion.title, 12.0, false, "left");
									}
									num++;
								}
							}
							if (@int == 0)
							{
								asposeWordApp.InsertPagebreak();
								asposeWordApp.Writeln("参考答案", 12.0, true, "center");
								num = 1;
								foreach (ExamTopic examTopic in this.examtopiclist)
								{
									asposeWordApp.InsertLineBreak();
									asposeWordApp.Writeln(examTopic.title, 12.0, true, "left");
									foreach (ExamQuestion examQuestion in this.GetQuestionList(FPRequest.GetString("qidlist_" + examTopic.id)))
									{
										if (examQuestion.type == 3)
										{
											asposeWordApp.Write((num + "、" + examQuestion.answer == "Y") ? "正确" : "错误  ", 12.0, false, "left");
										}
										else if (examQuestion.type == 6)
										{
											asposeWordApp.Write(num + "、请按题目打字  ", 12.0, false, "left");
										}
										else
										{
											asposeWordApp.Write(string.Concat(new object[]
											{
												num,
												"、",
												examQuestion.answer,
												"  "
											}), 12.0, false, "left");
										}
										num++;
									}
								}
							}
							asposeWordApp.Save(base.Response, this.examinfo.name + this.GetPaper(this.paper) + ".doc");
						}
						int num2 = 0;
						for (int i = 0; i < this.examtopiclist.Count; i++)
						{
							this.examtopiclist[i].questionlist = QuestionBll.GetTopicQuestion(this.channelinfo.id, this.examtopiclist[i]);
							string[] array = FPUtils.SplitString(this.examtopiclist[i].questionlist);
							if (this.examinfo.display == 0)
							{
								this.examtopiclist[i].questionlist = QuestionBll.GetRandom(array, array.Length);
							}
							num2 += array.Length;
							this.examtopiclist[i].questions = array.Length;
						}
						this.examinfo.questions = num2;
						this.questionlist = new int[this.examinfo.questions];
					}
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000112BC File Offset: 0x0000F4BC
		protected List<ExamQuestion> GetQuestionList(string questionids)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, questionids);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			foreach (int num in FPUtils.SplitInt(questionids))
			{
				foreach (ExamQuestion examQuestion in list)
				{
					if (examQuestion.id == num)
					{
						examQuestion.optionlist = this.OptionInt(examQuestion.ascount, this.examinfo.optiondisplay);
						list2.Add(examQuestion);
					}
				}
			}
			return list2;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000113AC File Offset: 0x0000F5AC
		protected string FmAnswer(string content, int tid, int en)
		{
			return content.Replace("(#answer)", string.Format("<input type=\"text\" id=\"_{0}\" name=\"answer_{1}\" value=\"\" class=\"tkt\"/>", en, tid));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000113E0 File Offset: 0x0000F5E0
		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "__________");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00011404 File Offset: 0x0000F604
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

		// Token: 0x060000AD RID: 173 RVA: 0x000114F8 File Offset: 0x0000F6F8
		protected string OptionInt(int ascount, int optiondisplay)
		{
			string text = "";
			for (int i = 0; i < ascount; i++)
			{
				if (text != "")
				{
					text += ",";
				}
				text += i.ToString();
			}
			if (optiondisplay == 0)
			{
				string[] array = FPUtils.SplitString(text);
				text = QuestionBll.GetRandom(array, array.Length);
			}
			return text;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00011578 File Offset: 0x0000F778
		protected string Option(AsposeWordApp wd, string[] opstr, int ascount, string optionlist)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			int[] array2 = FPUtils.SplitInt(optionlist, ",", ascount);
			string result = "";
			if (ascount > opstr.Length)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				if (optionlist != "")
				{
					wd.Writeln(array[i] + "." + opstr[array2[i]], 12.0, false, "left");
				}
				else
				{
					wd.Writeln(array[i] + "." + opstr[i], 12.0, false, "left");
				}
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00011640 File Offset: 0x0000F840
		protected string GetPaper(int paper)
		{
			string result;
			switch (paper)
			{
			case 1:
				result = "A卷";
				break;
			case 2:
				result = "B卷";
				break;
			case 3:
				result = "C卷";
				break;
			case 4:
				result = "D卷";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00011694 File Offset: 0x0000F894
		protected string GetNum(int num)
		{
			string result;
			switch (num)
			{
			case 1:
				result = "一";
				break;
			case 2:
				result = "二";
				break;
			case 3:
				result = "三";
				break;
			case 4:
				result = "四";
				break;
			case 5:
				result = "五";
				break;
			case 6:
				result = "六";
				break;
			case 7:
				result = "七";
				break;
			case 8:
				result = "八";
				break;
			case 9:
				result = "九";
				break;
			case 10:
				result = "十";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		// Token: 0x040000B9 RID: 185
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000BA RID: 186
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x040000BB RID: 187
		protected int paper = FPRequest.GetInt("paper", 1);

		// Token: 0x040000BC RID: 188
		protected int sortid;

		// Token: 0x040000BD RID: 189
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x040000BE RID: 190
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x040000BF RID: 191
		protected List<ExamTopic> examtopiclist = new List<ExamTopic>();

		// Token: 0x040000C0 RID: 192
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H",
			"I"
		};

		// Token: 0x040000C1 RID: 193
		protected int[] questionlist;

		// Token: 0x040000C2 RID: 194
		protected DateTime starttime;

		// Token: 0x040000C3 RID: 195
		protected ExamConfig examconfig = new ExamConfig();
	}
}
