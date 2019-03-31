using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FangPage.Data;
using FP_Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000010 RID: 16
	public class examtopicmanage : AdminController
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000080D4 File Offset: 0x000062D4
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			if (this.examinfo.id == 0)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				if (this.ispost)
				{
					string @string = FPRequest.GetString("action");
					int @int = FPRequest.GetInt("examtopicid");
					int int2 = FPRequest.GetInt("tid");
					ExamTopic examTopic = DbHelper.ExecuteModel<ExamTopic>(@int);
					if (@string == "delete")
					{
						DbHelper.ExecuteDelete<ExamTopic>(@int);
						if (this.paper == 1)
						{
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [total]=[total]-{1},[questions]=[questions]-{2} WHERE [id]={3}", new object[]
							{
								DbConfigs.Prefix,
								examTopic.perscore * (double)examTopic.questions,
								examTopic.questions,
								this.examid
							});
							DbHelper.ExecuteSql(stringBuilder.ToString());
						}
					}
					else if (@string == "addpaper")
					{
						if (this.examinfo.papers == 4)
						{
							this.ShowErr("对不起，一场考试最多只能添加4份试卷。");
							return;
						}
						string text = string.Format("UPDATE [{0}Exam_ExamInfo] SET [papers]=[papers]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
						DbHelper.ExecuteSql(text);
						this.paper = this.examinfo.papers + 1;
					}
					else if (@string == "delpaper")
					{
						if (this.examinfo.papers == 1)
						{
							this.ShowErr("对不起，一场考试必须有一份试卷。");
							return;
						}
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [papers]=[papers]-1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
						stringBuilder.AppendFormat("DELETE FROM [{0}Exam_ExamTopic] WHERE [examid]={1} AND [paper]={2}", DbConfigs.Prefix, this.examid, this.paper);
						stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamTopic] SET [paper]=[paper]-1 WHERE [examid]={1} AND [paper]>{2}", DbConfigs.Prefix, this.examid, this.paper);
						DbHelper.ExecuteSql(stringBuilder.ToString());
						this.examinfo.papers = this.examinfo.papers - 1;
						if (this.paper > this.examinfo.papers)
						{
							this.paper = this.examinfo.papers;
						}
						List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(this.examid, 1);
						double num = 0.0;
						int num2 = 0;
						foreach (ExamTopic examTopic2 in examTopicList)
						{
							num += examTopic2.perscore * (double)examTopic2.questions;
							num2 += examTopic2.questions;
						}
						string sqlstring = string.Format("UPDATE [{0}Exam_ExamInfo] SET [questions]={1},[total]={2} WHERE [id]={3}", new object[]
						{
							DbConfigs.Prefix,
							num2,
							num,
							this.examid
						});
						DbHelper.ExecuteSql(sqlstring);
					}
					else if (@string == "saveas")
					{
						if (this.examinfo.papers == 4)
						{
							this.ShowErr("对不起，一场考试最多只能添加4份试卷。");
							return;
						}
						string text = string.Format("UPDATE [{0}Exam_ExamInfo] SET [papers]=[papers]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
						DbHelper.ExecuteSql(text);
						this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
						this.examinfo.papers = this.examinfo.papers + 1;
						for (int i = 0; i < this.examtopiclist.Count; i++)
						{
							this.examtopiclist[i].paper = this.examinfo.papers;
							DbHelper.ExecuteInsert<ExamTopic>(this.examtopiclist[i]);
						}
					}
					else if (@string == "deletetopic")
					{
						string text2 = "";
						foreach (int num3 in FPUtils.SplitInt(examTopic.questionlist))
						{
							if (int2 != num3 && num3 > 0)
							{
								if (text2 != "")
								{
									text2 += ",";
								}
								text2 += num3;
							}
						}
						examTopic.questionlist = text2;
						if (examTopic.questionlist.Length > 0)
						{
							examTopic.curquestions = FPUtils.SplitInt(examTopic.questionlist).Length;
						}
						else
						{
							examTopic.curquestions = 0;
						}
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("questionlist", examTopic.questionlist),
							DbHelper.MakeSet("curquestions", examTopic.curquestions),
							DbHelper.MakeAndWhere("id", @int)
						};
						DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
					}
					else if (@string == "display")
					{
						this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
						string text = "";
						foreach (ExamTopic examTopic3 in this.examtopiclist)
						{
							DataTable dataTable = new DataTable();
							dataTable.Columns.Add("display", typeof(int));
							dataTable.Columns.Add("qid", typeof(int));
							foreach (int num4 in FPUtils.SplitInt(examTopic3.questionlist))
							{
								DataRow dataRow = dataTable.NewRow();
								dataRow["display"] = FPRequest.GetInt("display_" + num4);
								dataRow["qid"] = num4;
								dataTable.Rows.Add(dataRow);
							}
							string text3 = "";
							foreach (DataRow dataRow2 in dataTable.Select("1=1", "display asc"))
							{
								if (text3 != "")
								{
									text3 += ",";
								}
								text3 += dataRow2["qid"].ToString();
							}
							if (text != "")
							{
								text += "|";
							}
							text += string.Format("UPDATE [{0}Exam_ExamTopic] SET [questionlist]='{1}' WHERE [id]={2}", DbConfigs.Prefix, text3, examTopic3.id);
						}
						DbHelper.ExecuteSql(text);
					}
					base.Response.Redirect(this.pagename + string.Format("?examid={0}&paper={1}&examtopicid={2}", this.examid, this.paper, @int));
				}
				this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeAndWhere("examid", this.examid),
					DbHelper.MakeAndWhere("paper", this.paper)
				};
				this.examinfo.questions = FPUtils.StrToInt(DbHelper.ExecuteSum<ExamTopic>("questions", sqlparams2));
				base.SaveRightURL();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00008990 File Offset: 0x00006B90
		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "_______");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000089B4 File Offset: 0x00006BB4
		protected string TypeStr(int _type)
		{
			string result;
			switch (_type)
			{
			case 1:
				result = "单选题";
				break;
			case 2:
				result = "多选题";
				break;
			case 3:
				result = "判断题";
				break;
			case 4:
				result = "填空题";
				break;
			case 5:
				result = "问答题";
				break;
			case 6:
				result = "打字题";
				break;
			default:
				result = "题目";
				break;
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00008A20 File Offset: 0x00006C20
		protected string DifficultyStr(int difficulty)
		{
			string result;
			switch (difficulty)
			{
			case 0:
				result = "易";
				break;
			case 1:
				result = "较易";
				break;
			case 2:
				result = "一般";
				break;
			case 3:
				result = "较难";
				break;
			case 4:
				result = "难";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00008A80 File Offset: 0x00006C80
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

		// Token: 0x0400003A RID: 58
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x0400003B RID: 59
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400003C RID: 60
		protected int paper = FPRequest.GetInt("paper", 1);

		// Token: 0x0400003D RID: 61
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x0400003E RID: 62
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x0400003F RID: 63
		protected List<ExamTopic> examtopiclist = new List<ExamTopic>();

		// Token: 0x04000040 RID: 64
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		// Token: 0x04000041 RID: 65
		protected ExamConfig examconfig = new ExamConfig();
	}
}
