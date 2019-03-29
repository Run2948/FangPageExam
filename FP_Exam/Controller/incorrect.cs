using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200002F RID: 47
	public class incorrect : LoginController
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00014C10 File Offset: 0x00012E10
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				string commandText = string.Format("SELECT SUM([curwrongs]) AS [totalwrongs] FROM [{0}Exam_ExamLogInfo] WHERE [channelid]={1} AND [uid]={2}", DbConfigs.Prefix, this.channelinfo.id, this.userid);
				this.total = FPUtils.StrToInt(DbHelper.ExecuteScalar(commandText));
				this.userexamlog = ExamBll.GetExamLogList(this.channelinfo.id, this.userid);
				List<SortAppInfo> sortAppList = SortBll.GetSortAppList("exam_");
				foreach (SortAppInfo sortAppInfo in sortAppList)
				{
					if (this.sortappidlist != "")
					{
						this.sortappidlist += ",";
					}
					this.sortappidlist += sortAppInfo.id;
				}
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("parentid", 0),
					DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
					DbHelper.MakeAndWhere("appid", WhereType.In, this.sortappidlist),
					DbHelper.MakeAndWhere("hidden", 0)
				};
				OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
				foreach (SortInfo sortInfo in list)
				{
					ExamLogInfo examLogInfo;
					if (this.userexamlog.ContainsKey(sortInfo.id))
					{
						examLogInfo = this.userexamlog[sortInfo.id];
					}
					else
					{
						examLogInfo = new ExamLogInfo();
					}
					examLogInfo.sortid = sortInfo.id;
					examLogInfo.sortname = sortInfo.name;
					examLogInfo.questions = sortInfo.posts;
					examLogInfo.subcounts = sortInfo.subcounts;
					this.examloglist.Add(examLogInfo);
				}
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00014E98 File Offset: 0x00013098
		protected string GetChildSort(int channelid, int parentid, int level)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
				DbHelper.MakeAndWhere("appid", WhereType.In, this.sortappidlist),
				DbHelper.MakeAndWhere("hidden", 0)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SortInfo sortInfo in list)
			{
				ExamLogInfo examLogInfo;
				if (this.userexamlog.ContainsKey(sortInfo.id))
				{
					examLogInfo = this.userexamlog[sortInfo.id];
				}
				else
				{
					examLogInfo = new ExamLogInfo();
					examLogInfo.sortid = sortInfo.id;
				}
				stringBuilder.AppendFormat("<tr class=\"keypoint keypoint-level-{0} child_row_{1}\">\r\n", level, sortInfo.parentid);
				if (sortInfo.subcounts > 0)
				{
					if (examLogInfo.wrongs > 0)
					{
						stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_{0}\" class=\"sprite sprite-expand i-20\"></span>\r\n", sortInfo.id);
						stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=wrong\" target=\"_blank\" class=\"btn btn-link link-button\">{1}(共{2}道错题)</a>\r\n", sortInfo.id, sortInfo.name, examLogInfo.wrongs);
						stringBuilder.AppendFormat("</span></td>\r\n", new object[0]);
					}
					else
					{
						stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_{1}\" class=\"sprite sprite-expand i-20\"></span>{0}(共{2}道错题)</span></td>\r\n", sortInfo.name, sortInfo.id, examLogInfo.wrongs);
					}
				}
				else if (examLogInfo.wrongs > 0)
				{
					stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand-holder i-20\"></span>\r\n", new object[0]);
					stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=wrong\" target=\"_blank\" class=\"btn btn-link link-button\">{1}(共{2}道错题)</a>\r\n", sortInfo.id, sortInfo.name, examLogInfo.wrongs);
					stringBuilder.AppendFormat("</span></td>\r\n", new object[0]);
				}
				else
				{
					stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand-holder i-20\"></span>{0}(共{1}道错题)</span></td>\r\n", sortInfo.name, examLogInfo.wrongs);
				}
				stringBuilder.AppendFormat("<td class=\"button-col\">", new object[0]);
				if (examLogInfo.wrongs > 0)
				{
					stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=wrong\" target=\"_blank\" class=\"btn btn-link link-button\"><span class=\"btn-inner\">查看题目</span></a>&nbsp;&nbsp;", sortInfo.id);
					stringBuilder.AppendFormat("<a href=\"testview_csk.aspx?channelid={0}&sortid={1}\" target=\"_blank\" class=\"btn btn-link link-button\"><span class=\"btn-inner\">专项练习</span></a>", channelid, sortInfo.id);
				}
				else
				{
					stringBuilder.AppendFormat("<span class=\"btn-inner\">查看题目</span>&nbsp;&nbsp;", new object[0]);
					stringBuilder.AppendFormat("<span class=\"btn-inner\">专项练习</span>", new object[0]);
				}
				stringBuilder.Append("</td></tr>\r\n");
				if (sortInfo.subcounts > 0)
				{
					stringBuilder.Append(this.GetChildSort(channelid, sortInfo.id, level + 1));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000F2 RID: 242
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000F3 RID: 243
		protected List<ExamLogInfo> examloglist = new List<ExamLogInfo>();

		// Token: 0x040000F4 RID: 244
		protected string sortappidlist = "";

		// Token: 0x040000F5 RID: 245
		protected Dictionary<int, ExamLogInfo> userexamlog = new Dictionary<int, ExamLogInfo>();

		// Token: 0x040000F6 RID: 246
		protected int total = 0;
	}
}
