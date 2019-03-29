using System;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000014 RID: 20
	public class questionadd : AdminController
	{
		// Token: 0x06000069 RID: 105 RVA: 0x0000AB4C File Offset: 0x00008D4C
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			if (this.id > 0)
			{
				this.questioninfo = DbHelper.ExecuteModel<ExamQuestion>(this.id);
				this.sortid = this.questioninfo.sortid;
				this.type = this.questioninfo.type;
				this.ascount = this.questioninfo.ascount;
				if (this.type == 1 || this.type == 2 || this.type == 3)
				{
					this.questioninfo.answer = this.questioninfo.answer.ToLower();
				}
			}
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.examid > 0 && this.examtopicid > 0)
			{
				this.reurl = string.Concat(new object[]
				{
					"examtopicmanage.aspx?examid=",
					this.examid,
					"&examtopicid=",
					this.examtopicid
				});
			}
			if (this.examid > 0)
			{
				this.reurl = "examtopicmanage.aspx?examid=" + this.examid;
			}
			else if (this.examtopicid > 0)
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
			if (this.ispost)
			{
				this.questioninfo.upperflg = 0;
				this.questioninfo.orderflg = 0;
				this.questioninfo.status = 0;
				this.questioninfo.isclear = 0;
				this.questioninfo = FPRequest.GetModel<ExamQuestion>(this.questioninfo);
				if (this.questioninfo.isclear == 1)
				{
					this.questioninfo.title = questionadd.GetTextFromHTML(this.questioninfo.title);
				}
				this.questioninfo.channelid = this.sortinfo.channelid;
				if (this.questioninfo.type == 1 || this.questioninfo.type == 2)
				{
					this.questioninfo.ascount = FPRequest.GetInt("ascount" + this.questioninfo.type);
					this.questioninfo.content = "";
					for (int i = 0; i < this.questioninfo.ascount; i++)
					{
						if (this.questioninfo.content != "")
						{
							ExamQuestion examQuestion = this.questioninfo;
							examQuestion.content += "§";
						}
						if (this.questioninfo.isclear == 1)
						{
							ExamQuestion examQuestion2 = this.questioninfo;
							examQuestion2.content += questionadd.GetTextFromHTML(FPRequest.GetString(string.Concat(new object[]
							{
								"option",
								this.questioninfo.type,
								"_",
								i.ToString()
							})));
						}
						else
						{
							ExamQuestion examQuestion3 = this.questioninfo;
							examQuestion3.content += FPRequest.GetString(string.Concat(new object[]
							{
								"option",
								this.questioninfo.type,
								"_",
								i.ToString()
							}));
						}
					}
				}
				else if (this.questioninfo.type == 3)
				{
					this.questioninfo.ascount = 2;
					this.questioninfo.content = "";
				}
				else if (this.questioninfo.type == 4)
				{
					this.questioninfo.ascount = FPUtils.SplitString(this.questioninfo.answer).Length;
					this.questioninfo.content = "";
				}
				else if (this.questioninfo.type == 5)
				{
					this.questioninfo.ascount = FPUtils.SplitString(this.questioninfo.answerkey).Length;
					this.questioninfo.content = "";
				}
				this.questioninfo.answer = FPRequest.GetString("answer" + this.questioninfo.type);
				if (this.questioninfo.id > 0)
				{
					DbHelper.ExecuteUpdate<ExamQuestion>(this.questioninfo);
					base.AddMsg("更新试题成功！");
				}
				else
				{
					this.questioninfo.uid = this.userid;
					this.questioninfo.id = DbHelper.ExecuteInsert<ExamQuestion>(this.questioninfo);
					SortBll.UpdateSortPosts(this.questioninfo.sortid, 1);
					if (this.examid > 0)
					{
						ExamTopic examTopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
						if (examTopic.curquestions >= examTopic.questions)
						{
							this.ShowErr("该大题题目数已满，不能再添加");
							return;
						}
						examTopic.questionlist = ((examTopic.questionlist == "") ? this.questioninfo.id.ToString() : (examTopic.questionlist + "," + this.questioninfo.id));
						examTopic.curquestions = FPUtils.SplitInt(examTopic.questionlist).Length;
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("questionlist", examTopic.questionlist),
							DbHelper.MakeSet("curquestions", examTopic.curquestions),
							DbHelper.MakeAndWhere("id", this.examtopicid)
						};
						DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
					}
					base.AddMsg("添加试题成功！");
				}
				if (this.action == "continue")
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
				if (File.Exists(FPUtils.GetMapPath(string.Concat(new object[]
				{
					this.webpath,
					"cache/qtxt_",
					this.id,
					".jpg"
				}))))
				{
					File.Delete(FPUtils.GetMapPath(string.Concat(new object[]
					{
						this.webpath,
						"cache/qtxt_",
						this.id,
						".jpg"
					})));
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public static string GetTextFromHTML(string HTML)
		{
			Regex regex = new Regex("</?(?!br|img)[^>]*>", RegexOptions.IgnoreCase);
			return regex.Replace(HTML, "");
		}

		// Token: 0x04000060 RID: 96
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x04000061 RID: 97
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000062 RID: 98
		protected int type = FPRequest.GetInt("type");

		// Token: 0x04000063 RID: 99
		protected int ascount = 4;

		// Token: 0x04000064 RID: 100
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000065 RID: 101
		protected ExamQuestion questioninfo = new ExamQuestion();

		// Token: 0x04000066 RID: 102
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x04000067 RID: 103
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		// Token: 0x04000068 RID: 104
		protected string reurl = "";

		// Token: 0x04000069 RID: 105
		protected int backtype = FPRequest.GetInt("backtype");

		// Token: 0x0400006A RID: 106
		protected ExamConfig examconfig = new ExamConfig();
	}
}
