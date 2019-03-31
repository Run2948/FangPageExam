using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FP_Exam.Controller
{
	public class examtopicmanage : AdminController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected int paper = FPRequest.GetInt("paper", 1);

		protected ExamInfo examinfo = new ExamInfo();

		protected SortInfo sortinfo = new SortInfo();

		protected List<ExamTopic> examtopiclist = new List<ExamTopic>();

		protected int examtopicid = FPRequest.GetInt("examtopicid");

		protected ExamConfig examconfig = new ExamConfig();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			bool flag = this.examinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool ispost = this.ispost;
				if (ispost)
				{
					string @string = FPRequest.GetString("action");
					int @int = FPRequest.GetInt("examtopicid");
					int int2 = FPRequest.GetInt("tid");
					ExamTopic examTopic = DbHelper.ExecuteModel<ExamTopic>(@int);
					bool flag2 = @string == "delete";
					if (flag2)
					{
						DbHelper.ExecuteDelete<ExamTopic>(@int);
						bool flag3 = this.paper == 1;
						if (flag3)
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
					else
					{
						bool flag4 = @string == "addpaper";
						if (flag4)
						{
							bool flag5 = this.examinfo.papers == 4;
							if (flag5)
							{
								this.ShowErr("对不起，一场考试最多只能添加4份试卷。");
								return;
							}
							string sqlstring = string.Format("UPDATE [{0}Exam_ExamInfo] SET [papers]=[papers]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
							DbHelper.ExecuteSql(sqlstring);
							this.paper = this.examinfo.papers + 1;
						}
						else
						{
							bool flag6 = @string == "delpaper";
							if (flag6)
							{
								bool flag7 = this.examinfo.papers == 1;
								if (flag7)
								{
									this.ShowErr("对不起，一场考试必须有一份试卷。");
									return;
								}
								string sqlstring2 = string.Format("UPDATE [{0}Exam_ExamInfo] SET [papers]=[papers]-1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
								DbHelper.ExecuteSql(sqlstring2);
								string sqlstring3 = string.Format("DELETE FROM [{0}Exam_ExamTopic] WHERE [examid]={1} AND [paper]={2}", DbConfigs.Prefix, this.examid, this.paper);
								DbHelper.ExecuteSql(sqlstring3);
								string sqlstring4 = string.Format("UPDATE [{0}Exam_ExamTopic] SET [paper]=[paper]-1 WHERE [examid]={1} AND [paper]>{2}", DbConfigs.Prefix, this.examid, this.paper);
								DbHelper.ExecuteSql(sqlstring4);
								this.examinfo.papers = this.examinfo.papers - 1;
								bool flag8 = this.paper > this.examinfo.papers;
								if (flag8)
								{
									this.paper = this.examinfo.papers;
								}
								List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(this.examid, 1);
								double num = 0.0;
								int num2 = 0;
								foreach (ExamTopic current in examTopicList)
								{
									num += current.perscore * (double)current.questions;
									num2 += current.questions;
								}
								string sqlstring5 = string.Format("UPDATE [{0}Exam_ExamInfo] SET [questions]={1},[total]={2} WHERE [id]={3}", new object[]
								{
									DbConfigs.Prefix,
									num2,
									num,
									this.examid
								});
								DbHelper.ExecuteSql(sqlstring5);
							}
							else
							{
								bool flag9 = @string == "saveas";
								if (flag9)
								{
									bool flag10 = this.examinfo.papers == 4;
									if (flag10)
									{
										this.ShowErr("对不起，一场考试最多只能添加4份试卷。");
										return;
									}
									string sqlstring6 = string.Format("UPDATE [{0}Exam_ExamInfo] SET [papers]=[papers]+1 WHERE [id]={1}", DbConfigs.Prefix, this.examid);
									DbHelper.ExecuteSql(sqlstring6);
									this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
									this.examinfo.papers = this.examinfo.papers + 1;
									for (int i = 0; i < this.examtopiclist.Count; i++)
									{
										this.examtopiclist[i].paper = this.examinfo.papers;
										DbHelper.ExecuteInsert<ExamTopic>(this.examtopiclist[i]);
									}
								}
								else
								{
									bool flag11 = @string == "deletetopic";
									if (flag11)
									{
										string text = "";
										int[] array = FPArray.SplitInt(examTopic.questionlist);
										for (int j = 0; j < array.Length; j++)
										{
											int num3 = array[j];
											bool flag12 = int2 != num3 && num3 > 0;
											if (flag12)
											{
												bool flag13 = text != "";
												if (flag13)
												{
													text += ",";
												}
												text += num3;
											}
										}
										examTopic.questionlist = text;
										bool flag14 = examTopic.questionlist.Length > 0;
										if (flag14)
										{
											examTopic.curquestions = FPArray.SplitInt(examTopic.questionlist).Length;
										}
										else
										{
											examTopic.curquestions = 0;
										}
										SqlParam[] sqlparams = new SqlParam[]
										{
											DbHelper.MakeUpdate("questionlist", examTopic.questionlist),
											DbHelper.MakeUpdate("curquestions", examTopic.curquestions),
											DbHelper.MakeAndWhere("id", @int)
										};
										DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
									}
									else
									{
										bool flag15 = @string == "display";
										if (flag15)
										{
											this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
											string text2 = "";
											foreach (ExamTopic current2 in this.examtopiclist)
											{
												bool flag16 = current2.questionlist == "";
												if (!flag16)
												{
													DataTable dataTable = new DataTable();
													dataTable.Columns.Add("display", typeof(int));
													dataTable.Columns.Add("qid", typeof(int));
													int[] array2 = FPArray.SplitInt(current2.questionlist);
													for (int k = 0; k < array2.Length; k++)
													{
														int num4 = array2[k];
														DataRow dataRow = dataTable.NewRow();
														dataRow["display"] = FPRequest.GetInt("display_" + num4);
														dataRow["qid"] = num4;
														dataTable.Rows.Add(dataRow);
													}
													string text3 = "";
													DataRow[] array3 = dataTable.Select("1=1", "display asc");
													for (int l = 0; l < array3.Length; l++)
													{
														DataRow dataRow2 = array3[l];
														bool flag17 = text3 != "";
														if (flag17)
														{
															text3 += ",";
														}
														text3 += dataRow2["qid"].ToString();
													}
													bool flag18 = text3 == "0";
													if (flag18)
													{
														text3 = "";
													}
													bool flag19 = text2 != "";
													if (flag19)
													{
														text2 += "|";
													}
													text2 += string.Format("UPDATE [{0}Exam_ExamTopic] SET [questionlist]='{1}' WHERE [id]={2}", DbConfigs.Prefix, text3, current2.id);
												}
											}
											bool flag20 = text2 != "";
											if (flag20)
											{
												DbHelper.ExecuteSql(text2);
											}
										}
									}
								}
							}
						}
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
			}
		}

		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "_______");
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
	}
}
