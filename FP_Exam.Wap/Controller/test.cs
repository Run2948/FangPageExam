using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.Controller
{
	public class test : LoginController
	{
		protected ExamConfig examconfig = new ExamConfig();

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int limit = FPRequest.GetInt("limit");

		protected string type = "TYPE_RADIO,TYPE_MULTIPLE,TYPE_TRUE_FALSE,TYPE_BLANK,TYPE_ANSWER";

		protected string sidlist = FPRequest.GetString("sortid");

		protected string qidlist = "";

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

		protected DateTime starttime;

		protected string thetime = "00:00:00";

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			bool flag = this.examconfig.teststatus == 0;
			if (flag)
			{
				this.ShowErr("对不起，系统已关闭了用户练习。");
			}
			else
			{
				this.channelinfo = ChannelBll.GetChannelInfo("question");
				bool flag2 = this.channelinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，目前系统尚未创建题目库频道。");
				}
				else
				{
					this.starttime = DbUtils.GetDateTime();
					bool flag3 = this.limit == 0;
					if (flag3)
					{
						this.limit = 50;
					}
					bool flag4 = this.limit < 5;
					if (flag4)
					{
						this.ShowErr("为了更能准确体现您的能力，每次练习题目不能少于5题。");
					}
					else
					{
						bool flag5 = this.limit > this.examconfig.testcount;
						if (flag5)
						{
							this.ShowErr("对不起，每次练习题目数不得超过" + this.examconfig.testcount + "题。");
						}
						else
						{
							bool flag6 = this.sidlist == "";
							if (flag6)
							{
								this.ShowErr("对不起，您还没有选择练习题库。");
							}
							else
							{
								string questionTest = QuestionBll.GetQuestionTest(this.channelinfo.id, this.limit, this.type, this.sidlist);
								bool flag7 = questionTest != "";
								if (flag7)
								{
									SqlParam[] sqlparams = new SqlParam[]
									{
										DbHelper.MakeAndWhere("id", WhereType.In, questionTest),
										DbHelper.MakeOrderBy("display", OrderBy.ASC)
									};
									this.questionlist = DbHelper.ExecuteList<ExamQuestion>(sqlparams);
									this.limit = this.questionlist.Count;
									foreach (ExamQuestion current in this.questionlist)
									{
										bool flag8 = this.qidlist != "";
										if (flag8)
										{
											this.qidlist += ",";
										}
										this.qidlist += current.id;
									}
								}
								else
								{
									this.limit = 0;
									this.ShowErr("对不起，选起的题库没有题目。");
								}
							}
						}
					}
				}
			}
		}
	}
}
