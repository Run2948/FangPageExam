using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FP_Exam.Controller
{
	public class questionadd : AdminController
	{
		protected int sortid = FPRequest.GetInt("sortid");

		protected int id = FPRequest.GetInt("id");

		protected string type = FPRequest.GetString("type");

		protected int ascount = 4;

		protected SortInfo sortinfo = new SortInfo();

		protected ExamQuestion questioninfo = new ExamQuestion();

		protected int examid = FPRequest.GetInt("examid");

		protected int examtopicid = FPRequest.GetInt("examtopicid");

		protected string reurl = "";

		protected string backtype = FPRequest.GetString("backtype");

		protected ExamConfig examconfig = new ExamConfig();

		protected List<TypeInfo> typelist = new List<TypeInfo>();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			bool flag = this.type == "";
			if (flag)
			{
				this.type = "TYPE_RADIO";
			}
			bool flag2 = this.id > 0;
			if (flag2)
			{
				this.questioninfo = DbHelper.ExecuteModel<ExamQuestion>(this.id);
				this.sortid = this.questioninfo.sortid;
				this.type = this.questioninfo.type;
				this.ascount = this.questioninfo.ascount;
			}
			else
			{
				this.questioninfo.type = this.type;
			}
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool flag3 = this.examid > 0 && this.examtopicid > 0;
			if (flag3)
			{
				this.reurl = string.Concat(new object[]
				{
					"examtopicmanage.aspx?examid=",
					this.examid,
					"&examtopicid=",
					this.examtopicid
				});
			}
			bool flag4 = this.examid > 0;
			if (flag4)
			{
				this.reurl = "examtopicmanage.aspx?examid=" + this.examid;
			}
			else
			{
				bool flag5 = this.examtopicid > 0;
				if (flag5)
				{
					this.reurl = "examtopicselect.aspx?examtopicid=" + this.examtopicid;
				}
				else
				{
					this.reurl = string.Concat(new object[]
					{
						"questionmanage.aspx?sortid=",
						this.sortid,
						"&type=",
						this.backtype
					});
				}
			}
			bool ispost = this.ispost;
			if (ispost)
			{
				this.questioninfo.typelist = "";
				this.questioninfo = FPRequest.GetModel<ExamQuestion>(this.questioninfo);
				this.questioninfo.title = questionadd.GetTextFromHTML(this.questioninfo.title);
				this.questioninfo.explain = questionadd.GetTextFromHTML(this.questioninfo.explain);
				this.questioninfo.channelid = this.sortinfo.channelid;
				bool flag6 = this.questioninfo.type == "TYPE_RADIO";
				if (flag6)
				{
					this.questioninfo.display = 1;
					this.questioninfo.ascount = FPRequest.GetInt("ascount_TYPE_RADIO");
					this.questioninfo.content = "";
					for (int i = 0; i < this.questioninfo.ascount; i++)
					{
						bool flag7 = this.questioninfo.content != "";
						if (flag7)
						{
							ExamQuestion expr_291 = this.questioninfo;
							expr_291.content += "§";
						}
						ExamQuestion expr_2AE = this.questioninfo;
						expr_2AE.content += questionadd.GetTextFromHTML(FPRequest.GetString("option_TYPE_RADIO_" + i.ToString()));
					}
				}
				else
				{
					bool flag8 = this.questioninfo.type == "TYPE_MULTIPLE";
					if (flag8)
					{
						this.questioninfo.display = 2;
						this.questioninfo.ascount = FPRequest.GetInt("ascount_TYPE_MULTIPLE");
						this.questioninfo.content = "";
						for (int j = 0; j < this.questioninfo.ascount; j++)
						{
							bool flag9 = this.questioninfo.content != "";
							if (flag9)
							{
								ExamQuestion expr_37A = this.questioninfo;
								expr_37A.content += "§";
							}
							ExamQuestion expr_397 = this.questioninfo;
							expr_397.content += questionadd.GetTextFromHTML(FPRequest.GetString("option_TYPE_MULTIPLE_" + j.ToString()));
						}
					}
					else
					{
						bool flag10 = this.questioninfo.type == "TYPE_TRUE_FALSE";
						if (flag10)
						{
							this.questioninfo.display = 3;
							this.questioninfo.ascount = 2;
							this.questioninfo.content = "";
						}
						else
						{
							bool flag11 = this.questioninfo.type == "TYPE_BLANK";
							if (flag11)
							{
								this.questioninfo.display = 4;
								this.questioninfo.ascount = FPArray.SplitString(this.questioninfo.answer).Length;
								this.questioninfo.content = "";
							}
							else
							{
								bool flag12 = this.questioninfo.type == "TYPE_ANSWER";
								if (flag12)
								{
									this.questioninfo.display = 5;
									this.questioninfo.ascount = FPArray.SplitString(this.questioninfo.answerkey).Length;
									this.questioninfo.content = "";
								}
								else
								{
									bool flag13 = this.questioninfo.type == "TYPE_OPERATION";
									if (flag13)
									{
										this.questioninfo.display = 6;
										this.questioninfo.ascount = 1;
										this.questioninfo.content = "";
									}
								}
							}
						}
					}
				}
				this.questioninfo.answer = questionadd.GetTextFromHTML(FPRequest.GetString("answer_" + this.questioninfo.type));
				bool flag14 = this.questioninfo.id > 0;
				if (flag14)
				{
					DbHelper.ExecuteUpdate<ExamQuestion>(this.questioninfo);
					base.AddMsg("更新试题成功！");
				}
				else
				{
					this.questioninfo.uid = this.userid;
					DbHelper.ExecuteInsert<ExamQuestion>(this.questioninfo);
					SortBll.UpdateSortPosts(this.questioninfo.sortid, 1);
					TypeBll.UpdateTypePosts(this.questioninfo.typelist, this.questioninfo.sortid, 1);
					QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid);
					bool flag15 = this.sortinfo.types != "";
					if (flag15)
					{
						QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid, this.sortinfo.types);
					}
					bool flag16 = this.examid > 0;
					if (flag16)
					{
						ExamTopic examTopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
						bool flag17 = examTopic.curquestions >= examTopic.questions;
						if (flag17)
						{
							this.ShowErr("该大题题目数已满，不能再添加");
							return;
						}
						examTopic.questionlist = FPArray.Push(examTopic.questionlist, this.questioninfo.id);
						examTopic.curquestions = FPArray.SplitInt(examTopic.questionlist).Length;
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeUpdate("questionlist", examTopic.questionlist),
							DbHelper.MakeUpdate("curquestions", examTopic.curquestions),
							DbHelper.MakeAndWhere("id", this.examtopicid)
						};
						DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
					}
					base.AddMsg("添加试题成功！");
				}
				bool flag18 = this.action == "continue";
				if (flag18)
				{
					this.link = string.Format("questionadd.aspx?sortid={0}&examid={1}&examtopicid={2}&type={3}", new object[]
					{
						this.sortid,
						this.examid,
						this.examtopicid,
						this.backtype
					});
				}
				else
				{
					this.link = this.reurl;
				}
			}
			bool flag19 = this.questioninfo.attachid == "";
			if (flag19)
			{
				this.questioninfo.attachid = FPRandom.CreateAuth(20);
			}
			SqlParam[] sqlparams2 = new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", WhereType.In, this.sortinfo.types),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.typelist = DbHelper.ExecuteList<TypeInfo>(sqlparams2);
		}

		public static string GetTextFromHTML(string HTML)
		{
			Regex regex = new Regex("</?(?!img|span|/span|strong|/strong|em|/em||u|/u|a|/a|br)[^>]*>", RegexOptions.IgnoreCase);
			return regex.Replace(HTML, "");
		}
	}
}
