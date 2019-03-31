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
	public class testsort : AdminController
	{
		protected int channelid;

		protected int testid = FPRequest.GetInt("testid");

		protected SortInfo sortinfo = new SortInfo();

		protected TestInfo testinfo = new TestInfo();

		protected List<SortInfo> sortlist = new List<SortInfo>();

		protected override void Controller()
		{
			this.testinfo = DbHelper.ExecuteModel<TestInfo>(this.testid);
			bool flag = this.testinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该练习已被删除或者不存在。");
			}
			else
			{
				ChannelInfo channelInfo = ChannelBll.GetChannelInfo("question");
				this.channelid = channelInfo.id;
				this.sortinfo = SortBll.GetSortInfo(this.testinfo.sortid);
				this.sortlist = SortBll.GetSortList(this.channelid, 0);
				bool ispost = this.ispost;
				if (ispost)
				{
					string @string = FPRequest.GetString("sorts");
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeUpdate("sorts", @string),
						DbHelper.MakeAndWhere("id", this.testid)
					};
					DbHelper.ExecuteUpdate<TestInfo>(sqlparams);
					base.AddMsg("练习题库保存成功！");
				}
			}
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
					stringBuilder.AppendLine(string.Concat(new object[]
					{
						"<td><input id=\"sorts\" name=\"sorts\" value=\"",
						current.id,
						"_0\" type=\"checkbox\" ",
						FPArray.Contain(this.testinfo.sorts, current.id + "_0") ? "checked" : "",
						" /></td>"
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
							stringBuilder.AppendLine(string.Concat(new object[]
							{
								"<td><input id=\"sorts\" name=\"sorts\" value=\"",
								current.id,
								"_",
								current2.id,
								"\" type=\"checkbox\" ",
								FPArray.Contain(this.testinfo.sorts, current.id + "_" + current2.id) ? "checked" : "",
								" /> </td>"
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
				DbHelper.MakeAndWhere("type", this.testinfo.typelist)
			};
			return DbHelper.ExecuteCount<ExamQuestion>(sqlparams);
		}

		protected int GetQuestionCount(int sortid, int typeid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("sortid", sortid),
				DbHelper.MakeAndWhere("typelist", WhereType.Contain, typeid),
				DbHelper.MakeAndWhere("type", this.testinfo.typelist)
			};
			return DbHelper.ExecuteCount<ExamQuestion>(sqlparams);
		}
	}
}
