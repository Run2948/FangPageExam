using System;
using System.Collections.Generic;
using System.Data;
using FangPage.Data;
using FP_Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000013 RID: 19
	public class outputpaper : AdminController
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00009A40 File Offset: 0x00007C40
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else if (this.ispost)
			{
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
				this.examinfo.passmark = this.examinfo.passmark * this.examinfo.total / 100.0;
				this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
				int num = 0;
				for (int i = 0; i < this.examtopiclist.Count; i++)
				{
					this.examtopiclist[i].questionlist = QuestionBll.GetTopicQuestion(this.channelinfo.id, this.examtopiclist[i]);
					string[] array = FPUtils.SplitString(this.examtopiclist[i].questionlist);
					if (this.examinfo.display == 0)
					{
						this.examtopiclist[i].questionlist = QuestionBll.GetRandom(array, array.Length);
					}
					num += array.Length;
					this.examtopiclist[i].questions = array.Length;
				}
				this.examinfo.questions = num;
				AsposeWordApp asposeWordApp = new AsposeWordApp();
				if (this.papersize == "a4")
				{
					asposeWordApp.Open(FPUtils.GetMapPath("images\\exampaper_a4.doc"));
				}
				else
				{
					asposeWordApp.Open(FPUtils.GetMapPath("images\\exampaper_a3.doc"));
				}
				asposeWordApp.InsertText("examtitle", this.examinfo.name);
				asposeWordApp.InsertText("subtitle", this.GetPaper(this.paper));
				asposeWordApp.MoveToBookmark("content");
				DataTable dataTable = new DataTable();
				DataColumn column = new DataColumn("s0", Type.GetType("System.String"));
				dataTable.Columns.Add(column);
				int num2 = 1;
				foreach (ExamTopic examTopic in this.examtopiclist)
				{
					column = new DataColumn("s" + num2, Type.GetType("System.String"));
					dataTable.Columns.Add(column);
					num2++;
				}
				column = new DataColumn("s" + num2, Type.GetType("System.String"));
				dataTable.Columns.Add(column);
				DataRow dataRow = dataTable.NewRow();
				dataRow["s0"] = "题  号";
				num2 = 1;
				foreach (ExamTopic examTopic in this.examtopiclist)
				{
					dataRow["s" + num2] = this.GetNum(num2);
					num2++;
				}
				dataRow["s" + num2] = "总  分";
				dataTable.Rows.Add(dataRow);
				dataRow = dataTable.NewRow();
				dataRow["s0"] = "得  分";
				num2 = 1;
				foreach (ExamTopic examTopic in this.examtopiclist)
				{
					dataRow["s" + num2] = "";
					num2++;
				}
				dataRow["s" + num2] = "";
				dataTable.Rows.Add(dataRow);
				asposeWordApp.InsertTable(dataTable, true);
				asposeWordApp.InsertLineBreak();
				num2 = 1;
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
					foreach (ExamQuestion examQuestion in this.GetQuestionList(examTopic.questionlist))
					{
						if (examQuestion.type == 1 || examQuestion.type == 2)
						{
							if (this.papertype == 1)
							{
								asposeWordApp.Writeln(string.Concat(new object[]
								{
									num2,
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
								asposeWordApp.Writeln(num2 + "、" + examQuestion.title, 12.0, false, "left");
								asposeWordApp.Writeln(this.Option(asposeWordApp, examQuestion.option, examQuestion.ascount, examQuestion.optionlist), 12.0, false, "left");
							}
						}
						else if (examQuestion.type == 3)
						{
							if (this.papertype == 1)
							{
								asposeWordApp.Writeln((string.Concat(new object[]
								{
									num2,
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
									num2,
									"、",
									examQuestion.title,
									"  (    )"
								}), 12.0, false, "left");
							}
						}
						else if (examQuestion.type == 4)
						{
							if (this.papertype == 1)
							{
								asposeWordApp.Writeln(num2 + "、" + this.FmAnswer(examQuestion.title), 12.0, false, "left");
								asposeWordApp.Writeln("答案：" + examQuestion.answer, 12.0, false, "left");
								if (examQuestion.explain != "")
								{
									asposeWordApp.Writeln("答案解析：" + examQuestion.explain, 12.0, false, "left");
								}
							}
							else
							{
								asposeWordApp.Writeln(num2 + "、" + this.FmAnswer(examQuestion.title), 12.0, false, "left");
							}
						}
						else if (examQuestion.type == 5)
						{
							if (this.papertype == 1)
							{
								asposeWordApp.Writeln(num2 + "、" + examQuestion.title, 12.0, false, "left");
								asposeWordApp.Writeln("答：" + examQuestion.answer, 12.0, false, "left");
								if (examQuestion.explain != "")
								{
									asposeWordApp.Writeln("答案解析：" + examQuestion.explain, 12.0, false, "left");
								}
							}
							else
							{
								asposeWordApp.Writeln(num2 + "、" + examQuestion.title, 12.0, false, "left");
								asposeWordApp.InsertLineBreak(6);
							}
						}
						else if (examQuestion.type == 6)
						{
							asposeWordApp.Writeln(num2 + "、" + examQuestion.title, 12.0, false, "left");
						}
						num2++;
					}
				}
				if (this.papertype == 0)
				{
					asposeWordApp.InsertPagebreak();
					asposeWordApp.Writeln("参考答案", 12.0, true, "center");
					num2 = 1;
					foreach (ExamTopic examTopic in this.examtopiclist)
					{
						asposeWordApp.InsertLineBreak();
						asposeWordApp.Writeln(examTopic.title, 12.0, true, "left");
						foreach (ExamQuestion examQuestion in this.GetQuestionList(examTopic.questionlist))
						{
							if (examQuestion.type == 3)
							{
								asposeWordApp.Write((num2 + "、" + examQuestion.answer == "Y") ? "正确" : "错误  ", 12.0, false, "left");
							}
							else if (examQuestion.type == 6)
							{
								asposeWordApp.Write(num2 + "、请按题目打字  ", 12.0, false, "left");
							}
							else
							{
								asposeWordApp.Write(string.Concat(new object[]
								{
									num2,
									"、",
									examQuestion.answer,
									"  "
								}), 12.0, false, "left");
							}
							num2++;
						}
					}
				}
				asposeWordApp.Save(base.Response, this.examinfo.name + this.GetPaper(this.paper) + ".doc");
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000A734 File Offset: 0x00008934
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

		// Token: 0x06000063 RID: 99 RVA: 0x0000A824 File Offset: 0x00008A24
		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "__________");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000A848 File Offset: 0x00008A48
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

		// Token: 0x06000065 RID: 101 RVA: 0x0000A910 File Offset: 0x00008B10
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

		// Token: 0x06000066 RID: 102 RVA: 0x0000A990 File Offset: 0x00008B90
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

		// Token: 0x06000067 RID: 103 RVA: 0x0000A9E4 File Offset: 0x00008BE4
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

		// Token: 0x04000058 RID: 88
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000059 RID: 89
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x0400005A RID: 90
		protected int paper = FPRequest.GetInt("paper");

		// Token: 0x0400005B RID: 91
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x0400005C RID: 92
		protected List<ExamTopic> examtopiclist = new List<ExamTopic>();

		// Token: 0x0400005D RID: 93
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

		// Token: 0x0400005E RID: 94
		protected string papersize = FPRequest.GetString("papersize");

		// Token: 0x0400005F RID: 95
		protected int papertype = FPRequest.GetInt("papertype");
	}
}
