using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP_Exam.Controller
{
	public class examtopicrandom : AdminController
	{
		protected int channelid;

		protected int paper = FPRequest.GetInt("paper");

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int examtopicid = FPRequest.GetInt("examtopicid");

		protected ExamInfo examinfo = new ExamInfo();

		protected SortInfo sortinfo = new SortInfo();

		protected ExamTopic examtopic = new ExamTopic();

		protected List<SortInfo> sortlist = new List<SortInfo>();

		protected Dictionary<string, int> randomlist = new Dictionary<string, int>();

		protected Dictionary<string, int> curlist = new Dictionary<string, int>();

		private List<ExamQuestion> questionlist = new List<ExamQuestion>();

		protected override void Controller()
		{
			this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examtopic.examid);
			this.sortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			this.channelid = this.channelinfo.id;
			this.sortlist = SortBll.GetSortList(this.channelid, 0);
			bool ispost = this.ispost;
			if (ispost)
			{
				string text = "";
				string text2 = "";
				SqlParam sqlParam = DbHelper.MakeAndWhere("channelid", this.channelid);
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
				{
					sqlParam
				});
				int num = 0;
				foreach (SortInfo current in list)
				{
					bool flag = current.types != "";
					if (flag)
					{
						List<TypeInfo> typeList = TypeBll.GetTypeList(current.types);
						foreach (TypeInfo current2 in typeList)
						{
							int @int = FPRequest.GetInt(string.Concat(new object[]
							{
								"randomcount_",
								current.id,
								"_",
								current2.id
							}));
							bool flag2 = @int > 0;
							if (flag2)
							{
								bool flag3 = text != "";
								if (flag3)
								{
									text += ",";
								}
								text = string.Concat(new object[]
								{
									text,
									current.id,
									"_",
									current2.id
								});
								bool flag4 = text2 != "";
								if (flag4)
								{
									text2 += ",";
								}
								text2 += @int;
								num += @int;
							}
						}
					}
					int int2 = FPRequest.GetInt("randomcount_" + current.id + "_0");
					bool flag5 = int2 > 0;
					if (flag5)
					{
						bool flag6 = text != "";
						if (flag6)
						{
							text += ",";
						}
						text = text + current.id + "_0";
						bool flag7 = text2 != "";
						if (flag7)
						{
							text2 += ",";
						}
						text2 += int2;
						num += int2;
					}
				}
				bool flag8 = num > this.examtopic.questions - this.examtopic.curquestions;
				if (flag8)
				{
					this.ShowErr("设定的随机题数不能大于总随机题数。");
					return;
				}
				bool flag9 = this.action == "save";
				if (flag9)
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeUpdate("randomsort", text),
						DbHelper.MakeUpdate("randomcount", text2),
						DbHelper.MakeUpdate("randoms", num),
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
				else
				{
					bool flag10 = this.action == "create";
					if (flag10)
					{
						string text3 = this.examtopic.questionlist;
						string[] array = FPArray.SplitString(text);
						int[] array2 = FPArray.SplitInt(text2, ",", array.Length);
						for (int i = 0; i < array.Length; i++)
						{
							bool flag11 = array2[i] > 0;
							if (flag11)
							{
								int[] array3 = FPArray.SplitInt(array[i], "_", 2);
								string questionRandom = QuestionBll.GetQuestionRandom(this.channelid, array2[i], this.examtopic.type, array3[0], array3[1], text3);
								bool flag12 = questionRandom != "";
								if (flag12)
								{
									text3 += ((text3 == "") ? questionRandom : ("," + questionRandom));
								}
							}
						}
						this.examtopic.questionlist = text3;
						this.examtopic.curquestions = FPArray.SplitInt(this.examtopic.questionlist).Length;
						SqlParam[] sqlparams2 = new SqlParam[]
						{
							DbHelper.MakeUpdate("questionlist", this.examtopic.questionlist),
							DbHelper.MakeUpdate("curquestions", this.examtopic.curquestions),
							DbHelper.MakeUpdate("randomsort", ""),
							DbHelper.MakeUpdate("randomcount", ""),
							DbHelper.MakeUpdate("randoms", 0),
							DbHelper.MakeAndWhere("id", this.examtopicid)
						};
						DbHelper.ExecuteUpdate<ExamTopic>(sqlparams2);
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
			}
			string[] array4 = FPArray.SplitString(this.examtopic.randomsort);
			int[] array5 = FPArray.SplitInt(this.examtopic.randomcount, ",", array4.Length);
			for (int j = 0; j < array4.Length; j++)
			{
				this.randomlist.Add(array4[j], array5[j]);
			}
			SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, this.examtopic.questionlist);
			this.questionlist = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam2
			});
			foreach (ExamQuestion current3 in this.questionlist)
			{
				bool flag13 = this.curlist.ContainsKey(current3.sortid + "_0");
				if (flag13)
				{
					this.curlist[current3.sortid + "_0"] = this.curlist[current3.sortid + "_0"] + 1;
				}
				else
				{
					this.curlist.Add(current3.sortid + "_0", 1);
				}
				bool flag14 = current3.typelist != "";
				if (flag14)
				{
					int[] array6 = FPArray.SplitInt(current3.typelist);
					for (int k = 0; k < array6.Length; k++)
					{
						int num2 = array6[k];
						bool flag15 = this.curlist.ContainsKey(current3.sortid + "_" + num2);
						if (flag15)
						{
							this.curlist[current3.sortid + "_" + num2] = this.curlist[current3.sortid + "_" + num2] + 1;
						}
						else
						{
							this.curlist.Add(current3.sortid + "_" + num2, 1);
						}
					}
				}
			}
		}

		protected string GetRandomCount(int sid, int typeid)
		{
			bool flag = this.randomlist.ContainsKey(sid + "_" + typeid);
			string result;
			if (flag)
			{
				bool flag2 = this.randomlist[sid + "_" + typeid] > 0;
				if (flag2)
				{
					result = this.randomlist[sid + "_" + typeid].ToString();
					return result;
				}
			}
			result = "";
			return result;
		}

		protected int GetCurCount(int sid, int typeid)
		{
			bool flag = this.curlist.ContainsKey(sid + "_" + typeid);
			int result;
			if (flag)
			{
				result = this.curlist[sid + "_" + typeid];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		protected string ShowChildSort(int parentid, string tree)
		{
			List<SortInfo> sortList = SortBll.GetSortList(this.channelid, parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (SortInfo current in sortList)
			{
				bool flag = !FPArray.Contain(this.role.sorts, current.id) && this.roleid != 1;
				if (!flag)
				{
					stringBuilder.Append("<tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">");
					stringBuilder.AppendLine("<td align=\"left\">" + tree);
					bool flag2 = current.icon != "";
					if (flag2)
					{
						stringBuilder.AppendLine("<img src=\"" + current.icon + "\" width=\"16\" height=\"16\"  />");
					}
					else
					{
						bool flag3 = current.subcounts > 0;
						if (flag3)
						{
							stringBuilder.AppendLine("<img src=\"" + this.adminpath + "statics/images/folders.gif\" width=\"16\" height=\"16\"  />");
						}
						else
						{
							stringBuilder.AppendLine("<img src=\"" + this.adminpath + "statics/images/folder.gif\" width=\"16\" height=\"16\"  />");
						}
					}
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						current.name,
						"(",
						this.GetQuestionCount(current.id),
						")</td>"
					}));
					stringBuilder.AppendLine("<td>" + this.GetCurCount(current.id, 0) + "</td>");
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						"<td><input id=\"randomcount_",
						current.id,
						"_0\" name=\"randomcount_",
						current.id,
						"_0\" value=\"",
						this.GetRandomCount(current.id, 0),
						"\" type=\"text\" /> </td>"
					}));
					stringBuilder.AppendLine("</tr>");
					stringBuilder.Append(this.ShowChildSort(current.id, tree));
					bool flag4 = current.types != "" && current.showtype == 1;
					if (flag4)
					{
						List<TypeInfo> typeList = TypeBll.GetTypeList(current.types);
						foreach (TypeInfo current2 in typeList)
						{
							stringBuilder.Append("<tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">");
							stringBuilder.AppendLine("<td align=\"left\">│  " + tree);
							stringBuilder.AppendLine("<img src=\"" + this.adminpath + "statics/images/type.gif\" width=\"16\" height=\"16\"  />");
							stringBuilder.AppendLine(string.Concat(new object[]
							{
								current2.name,
								"(",
								this.GetQuestionCount(current.id, current2.id),
								")</td>"
							}));
							stringBuilder.AppendLine("<td>" + this.GetCurCount(current.id, current2.id) + "</td>");
							stringBuilder.AppendLine(string.Concat(new object[]
							{
								"<td><input id=\"randomcount_",
								current.id,
								"_",
								current2.id,
								"\" name=\"randomcount_",
								current.id,
								"_",
								current2.id,
								"\" value=\"",
								this.GetRandomCount(current.id, current2.id),
								"\" type=\"text\" /> </td>"
							}));
							stringBuilder.AppendLine("</tr>");
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		protected List<TypeInfo> GetTypeList(string types)
		{
			return TypeBll.GetTypeList(types);
		}

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

		protected int GetQuestionCount(int sortid, int typeid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("sortid", sortid),
				DbHelper.MakeAndWhere("typelist", WhereType.Contain, typeid),
				DbHelper.MakeAndWhere("type", this.examtopic.type)
			};
			return DbHelper.ExecuteCount<ExamQuestion>(sqlparams);
		}

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
	}
}
