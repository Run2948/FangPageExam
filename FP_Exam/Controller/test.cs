using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000033 RID: 51
	public class test : LoginController
	{
		// Token: 0x060000DE RID: 222 RVA: 0x00015D6C File Offset: 0x00013F6C
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			if (this.examconfig.teststatus == 0)
			{
				this.ShowErr("对不起，考试系统已关闭了用户练习。");
			}
			else
			{
				this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
				if (this.channelinfo.id == 0)
				{
					this.ShowErr("对不起，目前系统尚未创建题目库频道。");
				}
				else
				{
					this.starttime = DbUtils.GetDateTime();
					if (this.limit == 0)
					{
						this.limit = 50;
					}
					if (this.limit < 5)
					{
						this.ShowErr("为了更能准确体现您的能力，每次练习题目不能少于5题。");
					}
					else if (this.limit > this.examconfig.testcount)
					{
						this.ShowErr("对不起，每次练习题目数不得超过" + this.examconfig.testcount + "题。");
					}
					else
					{
						string questionRandom = QuestionBll.GetQuestionRandom(this.channelinfo.id, this.limit, this.type, this.sidlist);
						if (questionRandom != "")
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, questionRandom);
							OrderByParam orderby = DbHelper.MakeOrderBy("type", OrderBy.ASC);
							this.questionlist = DbHelper.ExecuteList<ExamQuestion>(orderby, new SqlParam[]
							{
								sqlParam
							});
							foreach (ExamQuestion examQuestion in this.questionlist)
							{
								if (this.qidlist != "")
								{
									this.qidlist += ",";
								}
								this.qidlist += examQuestion.id;
							}
							int num = this.examconfig.testtime * 60;
							int num2 = num / 3600;
							int num3 = (num - num2 * 3600) / 60;
							int num4 = (num - num2 * 3600 - num3 * 60) % 60;
							this.thetime = string.Concat(new string[]
							{
								num2.ToString("00"),
								":",
								num3.ToString("00"),
								":",
								num4.ToString("00")
							});
						}
						else
						{
							this.ShowErr("对不起，抽起的题库没有题目。");
						}
					}
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0001602C File Offset: 0x0001422C
		protected string FmAnswer(string content, int tid, int en)
		{
			return content.Replace("(#answer)", string.Format("<input type=\"text\" id=\"_{0}\" name=\"answer_{1}\" value=\"\" class=\"tkt\"/>", en, tid));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00016060 File Offset: 0x00014260
		protected string Option(string[] opstr, int ascount)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			string text = "";
			if (ascount > opstr.Length)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
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
			return text;
		}

		// Token: 0x04000102 RID: 258
		protected ExamConfig examconfig = new ExamConfig();

		// Token: 0x04000103 RID: 259
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000104 RID: 260
		protected int limit = FPRequest.GetInt("limit");

		// Token: 0x04000105 RID: 261
		protected string type = FPRequest.GetString("type");

		// Token: 0x04000106 RID: 262
		protected string sidlist = FPRequest.GetString("sidlist");

		// Token: 0x04000107 RID: 263
		protected string qidlist = "";

		// Token: 0x04000108 RID: 264
		protected int testtype = FPRequest.GetInt("testtype");

		// Token: 0x04000109 RID: 265
		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		// Token: 0x0400010A RID: 266
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F"
		};

		// Token: 0x0400010B RID: 267
		protected DateTime starttime;

		// Token: 0x0400010C RID: 268
		protected string thetime = "00:00:00";
	}
}
