using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000011 RID: 17
	public class examtopicrandom : AdminController
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00008B8C File Offset: 0x00006D8C
		protected override void View()
		{
			this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examtopic.examid);
			this.sortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			this.channelid = this.channelinfo.id;
			this.sortlist = SortBll.GetSortList(this.channelid, 0);
			if (this.ispost)
			{
				string text = "";
				string text2 = "";
				SqlParam sqlParam = DbHelper.MakeAndWhere("channelid", this.channelid);
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
				{
					sqlParam
				});
				int num = 0;
				foreach (SortInfo sortInfo in list)
				{
					int @int = FPRequest.GetInt("randomcount_" + sortInfo.id);
					if (@int > 0)
					{
						if (text != "")
						{
							text += ",";
						}
						text += sortInfo.id;
						if (text2 != "")
						{
							text2 += ",";
						}
						text2 += @int;
						num += @int;
					}
				}
				if (num > this.examtopic.questions - this.examtopic.curquestions)
				{
					this.ShowErr("设定的随机题数不能大于总随机题数。");
					return;
				}
				if (this.action == "save")
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeSet("randomsort", text),
						DbHelper.MakeSet("randomcount", text2),
						DbHelper.MakeSet("randoms", num),
						DbHelper.MakeAndWhere("id", this.examtopicid)
					};
					DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
					base.AddMsg("随机题设置保存成功！");
					this.examtopic.randomsort = text;
					this.examtopic.randomcount = text2;
					this.link = string.Concat(new object[]
					{
						"examtopicrandom.aspx?examtopicid=",
						this.examtopicid,
						"&paper=",
						this.paper
					});
				}
				else if (this.action == "create")
				{
					string text3 = this.examtopic.questionlist;
					int[] array = FPUtils.SplitInt(text);
					int[] array2 = FPUtils.SplitInt(text2, ",", array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						if (array2[i] > 0)
						{
							string questionRandom = QuestionBll.GetQuestionRandom(this.channelid, array2[i], this.examtopic.type.ToString(), array[i], text3);
							if (questionRandom != "")
							{
								text3 += ((text3 == "") ? questionRandom : ("," + questionRandom));
							}
						}
					}
					this.examtopic.questionlist = text3;
					this.examtopic.curquestions = FPUtils.SplitInt(this.examtopic.questionlist).Length;
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeSet("questionlist", this.examtopic.questionlist),
						DbHelper.MakeSet("curquestions", this.examtopic.curquestions),
						DbHelper.MakeSet("randomsort", ""),
						DbHelper.MakeSet("randomcount", ""),
						DbHelper.MakeSet("randoms", 0),
						DbHelper.MakeAndWhere("id", this.examtopicid)
					};
					DbHelper.ExecuteUpdate<ExamTopic>(sqlparams);
					base.AddMsg("生成随机题目成功！");
					this.link = string.Concat(new object[]
					{
						"examtopicmanage.aspx?examid=",
						this.examtopic.examid,
						"&paper=",
						this.paper,
						"&examtopicid=",
						this.examtopicid
					});
				}
			}
			int[] array3 = FPUtils.SplitInt(this.examtopic.randomsort);
			int[] array4 = FPUtils.SplitInt(this.examtopic.randomcount, ",", array3.Length);
			for (int i = 0; i < array3.Length; i++)
			{
				this.randomlist.Add(array3[i], array4[i]);
			}
			SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, this.examtopic.questionlist);
			this.questionlist = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam2
			});
			foreach (ExamQuestion examQuestion in this.questionlist)
			{
				if (this.curlist.ContainsKey(examQuestion.sortid))
				{
					this.curlist[examQuestion.sortid] = this.curlist[examQuestion.sortid] + 1;
				}
				else
				{
					this.curlist.Add(examQuestion.sortid, 1);
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000091B8 File Offset: 0x000073B8
		protected string GetRandomCount(int sid)
		{
			if (this.randomlist.ContainsKey(sid))
			{
				if (this.randomlist[sid] > 0)
				{
					return this.randomlist[sid].ToString();
				}
			}
			return "";
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00009214 File Offset: 0x00007414
		protected int GetCurCount(int sid)
		{
			int result;
			if (this.curlist.ContainsKey(sid))
			{
				result = this.curlist[sid];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000924C File Offset: 0x0000744C
		protected string ShowChildSort(int parentid, string tree)
		{
			List<SortInfo> sortList = SortBll.GetSortList(this.channelid, parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (SortInfo sortInfo in sortList)
			{
				if (base.ischecked(sortInfo.id, this.role.sorts) || this.roleid == 1)
				{
					stringBuilder.Append("<tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">");
					stringBuilder.AppendLine("<td align=\"left\">" + tree);
					string text = "";
					if (sortInfo.hidden == 1)
					{
						text = "_hidden";
					}
					if (sortInfo.icon != "")
					{
						stringBuilder.AppendLine("<img src=\"" + sortInfo.icon + "\" width=\"16\" height=\"16\"  />");
					}
					else if (sortInfo.subcounts > 0)
					{
						stringBuilder.AppendLine(string.Concat(new string[]
						{
							"<img src=\"",
							this.adminpath,
							"images/folders",
							text,
							".gif\" width=\"16\" height=\"16\"  />"
						}));
					}
					else
					{
						stringBuilder.AppendLine(string.Concat(new string[]
						{
							"<img src=\"",
							this.adminpath,
							"images/folder",
							text,
							".gif\" width=\"16\" height=\"16\"  />"
						}));
					}
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						sortInfo.name,
						"(",
						this.GetQuestionCount(sortInfo.id),
						")</td>"
					}));
					stringBuilder.AppendLine("<td>" + this.GetCurCount(sortInfo.id) + "</td>");
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						"<td><input id=\"randomcount_",
						sortInfo.id,
						"\" name=\"randomcount_",
						sortInfo.id,
						"\" value=\"",
						this.GetRandomCount(sortInfo.id),
						"\" type=\"text\" /> </td>"
					}));
					stringBuilder.AppendLine("</tr>");
					stringBuilder.Append(this.ShowChildSort(sortInfo.id, tree));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00009518 File Offset: 0x00007718
		protected int GetQuestionCount(int sortid)
		{
			string childSorts = SortBll.GetChildSorts(sortid);
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts),
				DbHelper.MakeAndWhere("type", this.examtopic.type)
			};
			return DbHelper.ExecuteCount<ExamQuestion>(sqlparams);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00009574 File Offset: 0x00007774
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

		// Token: 0x04000042 RID: 66
		protected int channelid;

		// Token: 0x04000043 RID: 67
		protected int paper = FPRequest.GetInt("paper");

		// Token: 0x04000044 RID: 68
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000045 RID: 69
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		// Token: 0x04000046 RID: 70
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000047 RID: 71
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000048 RID: 72
		protected ExamTopic examtopic = new ExamTopic();

		// Token: 0x04000049 RID: 73
		protected List<SortInfo> sortlist = new List<SortInfo>();

		// Token: 0x0400004A RID: 74
		protected Dictionary<int, int> randomlist = new Dictionary<int, int>();

		// Token: 0x0400004B RID: 75
		protected Dictionary<int, int> curlist = new Dictionary<int, int>();

		// Token: 0x0400004C RID: 76
		private List<ExamQuestion> questionlist = new List<ExamQuestion>();
	}
}
