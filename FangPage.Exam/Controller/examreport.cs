using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000029 RID: 41
	public class examreport : LoginController
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00012850 File Offset: 0x00010A50
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				string sqlstring = string.Format("SELECT COUNT([uid]) FROM (SELECT DISTINCT [uid] FROM [{0}Exam_ExamResult] WHERE [channelid]={1} AND [status]>0) AS TA", DbConfigs.Prefix, this.channelinfo.id);
				this.examusers = this.GetScalarTotal(sqlstring);
				sqlstring = string.Format("SELECT AVG([score]) FROM [{0}Exam_ExamResult] WHERE [channelid]={1} AND [uid]={2} AND [status]>0", DbConfigs.Prefix, this.channelinfo.id, this.userid);
				this.avg_my = this.GetScalarTotal(sqlstring);
				sqlstring = string.Format("SELECT AVG([score]) FROM [{0}Exam_ExamResult] WHERE [channelid]={1} AND [status]>0", DbConfigs.Prefix, this.channelinfo.id);
				this.avg_total = this.GetScalarTotal(sqlstring);
				sqlstring = string.Format("SELECT COUNT(*) FROM (SELECT [uid],AVG([score]) AS [scoreavg] FROM [{0}Exam_ExamResult] WHERE [channelid]={1} AND [status]>0 GROUP BY [uid]) AS TA WHERE [scoreavg]>{2}", DbConfigs.Prefix, this.channelinfo.id, this.avg_my);
				this.avg_display = FPUtils.StrToInt(this.GetScalarTotal(sqlstring)) + 1;
				sqlstring = string.Format("SELECT SUM([answers]) AS [answers],SUM([wrongs]) AS [wrongs] FROM [{0}Exam_ExamLogInfo] WHERE [channelid]={1}", DbConfigs.Prefix, this.channelinfo.id);
				ExamLogInfo examLogInfo = this.GetExamLogInfo(sqlstring);
				this.accuracy_total = examLogInfo.accuracy;
				sqlstring = string.Format("SELECT SUM([answers]) AS [answers],SUM([wrongs]) AS [wrongs] FROM [{0}Exam_ExamLogInfo] WHERE [channelid]={1} AND [uid]={2}", DbConfigs.Prefix, this.channelinfo.id, this.userid);
				examLogInfo = this.GetExamLogInfo(sqlstring);
				this.accuracy_my = examLogInfo.accuracy;
				sqlstring = string.Format("SELECT COUNT(*) FROM (SELECT [uid],SUM(wrongs) AS [wrongs] FROM [{0}Exam_ExamLogInfo] WHERE [channelid]={1} GROUP BY [uid]) AS TA WHERE [wrongs]<{2}", DbConfigs.Prefix, this.channelinfo.id, examLogInfo.wrongs);
				this.accuracy_display = FPUtils.StrToInt(this.GetScalarTotal(sqlstring)) + 1;
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
					DbHelper.MakeAndWhere("status", WhereType.GreaterThan, 0),
					DbHelper.MakeAndWhere("uid", this.userid)
				};
				List<ExamResult> list = DbHelper.ExecuteList<ExamResult>(OrderBy.ASC, sqlparams);
				int num = 1;
				foreach (ExamResult examResult in list)
				{
					if (this.examresult != "")
					{
						this.examresult += ",";
					}
					this.examresult += string.Format("['', {0}]", examResult.score);
					num++;
				}
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
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeAndWhere("parentid", 0),
					DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
					DbHelper.MakeAndWhere("appid", WhereType.In, this.sortappidlist),
					DbHelper.MakeAndWhere("hidden", 0)
				};
				OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				List<SortInfo> list2 = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams2);
				foreach (SortInfo sortInfo in list2)
				{
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

		// Token: 0x060000BE RID: 190 RVA: 0x00012D18 File Offset: 0x00010F18
		protected string GetChildSort(int channelid, int parentid, int level)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", channelid),
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
					stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span  id=\"row_{1}\" class=\"sprite sprite-expand sprite-expand\"></span>{0}</span></td>\r\n", sortInfo.name, sortInfo.id);
				}
				else
				{
					stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand sprite-noexpand\"></span>{0}</span></td>\r\n", sortInfo.name);
				}
				stringBuilder.AppendFormat("<td class=\"count-col\">{0}道/{1}道</td>\r\n", examLogInfo.answers, sortInfo.posts);
				stringBuilder.AppendFormat("<td class=\"count-col\">{0}道</td>\r\n", examLogInfo.answers - examLogInfo.wrongs);
				if (examLogInfo.answers > 0)
				{
					stringBuilder.AppendFormat("<td class=\"capacity-col\"><span class=\"progressBar\">{0}%</span></td>\r\n", examLogInfo.accuracy);
				}
				else
				{
					stringBuilder.AppendFormat("<td class=\"capacity-col\"><span class=\"progressBar\">{0}%</span></td>\r\n", 0);
				}
				stringBuilder.Append("</tr>\r\n");
				if (sortInfo.subcounts > 0)
				{
					stringBuilder.Append(this.GetChildSort(channelid, sortInfo.id, level + 1));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00012F70 File Offset: 0x00011170
		private string GetScalarTotal(string sqlstring)
		{
			double value = 0.0;
			string result;
			if (!double.TryParse(DbHelper.ExecuteScalar(sqlstring).ToString(), out value))
			{
				result = "0";
			}
			else
			{
				result = Math.Round(value, 1).ToString();
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00012FBC File Offset: 0x000111BC
		private ExamLogInfo GetExamLogInfo(string sqlstring)
		{
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, sqlstring);
			ExamLogInfo examLogInfo = new ExamLogInfo();
			if (dataReader.Read())
			{
				examLogInfo.answers = FPUtils.StrToInt(dataReader["answers"]);
				examLogInfo.wrongs = FPUtils.StrToInt(dataReader["wrongs"]);
			}
			dataReader.Close();
			return examLogInfo;
		}

		// Token: 0x040000CF RID: 207
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000D0 RID: 208
		protected List<ExamLogInfo> examloglist = new List<ExamLogInfo>();

		// Token: 0x040000D1 RID: 209
		protected string sortappidlist = "";

		// Token: 0x040000D2 RID: 210
		protected Dictionary<int, ExamLogInfo> userexamlog = new Dictionary<int, ExamLogInfo>();

		// Token: 0x040000D3 RID: 211
		protected string examresult = "";

		// Token: 0x040000D4 RID: 212
		protected int usertotal = 0;

		// Token: 0x040000D5 RID: 213
		protected string avg_my = "0";

		// Token: 0x040000D6 RID: 214
		protected string avg_total = "0";

		// Token: 0x040000D7 RID: 215
		protected int avg_display = 0;

		// Token: 0x040000D8 RID: 216
		protected string accuracy_my = "0";

		// Token: 0x040000D9 RID: 217
		protected string accuracy_total = "0";

		// Token: 0x040000DA RID: 218
		protected int accuracy_display = 0;

		// Token: 0x040000DB RID: 219
		protected string examusers = "0";
	}
}
