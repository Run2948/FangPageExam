using FangPage.Common;
using FangPage.Data;
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
	public class examreport : LoginController
	{
		protected ChannelInfo channelinfo = new ChannelInfo();

		protected List<ExamLogInfo> examloglist = new List<ExamLogInfo>();

		protected string sortappidlist = "";

		protected Dictionary<int, ExamLogInfo> userexamlog = new Dictionary<int, ExamLogInfo>();

		protected string examresult = "";

		protected int usertotal = 0;

		protected string avg_my = "0";

		protected string avg_total = "0";

		protected int avg_display = 0;

		protected string accuracy_my = "0";

		protected string accuracy_total = "0";

		protected int accuracy_display = 0;

		protected string examusers = "0";

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			bool flag = this.channelinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				string sqlstring = string.Format("SELECT COUNT([uid]) FROM (SELECT DISTINCT [uid] FROM [{0}Exam_ExamResult] WHERE [status]>0) AS TA", DbConfigs.Prefix);
				this.examusers = this.GetScalarTotal(sqlstring);
				sqlstring = string.Format("SELECT AVG([score]) FROM [{0}Exam_ExamResult] WHERE [uid]={1} AND [status]>0", DbConfigs.Prefix, this.userid);
				this.avg_my = this.GetScalarTotal(sqlstring);
				sqlstring = string.Format("SELECT AVG([score]) FROM [{0}Exam_ExamResult] WHERE [status]>0", DbConfigs.Prefix);
				this.avg_total = this.GetScalarTotal(sqlstring);
				sqlstring = string.Format("SELECT COUNT(*) FROM (SELECT [uid],AVG([score]) AS [scoreavg] FROM [{0}Exam_ExamResult] WHERE [status]>0 GROUP BY [uid]) AS TA WHERE [scoreavg]>{1}", DbConfigs.Prefix, this.avg_my);
				this.avg_display = FPUtils.StrToInt(this.GetScalarTotal(sqlstring)) + 1;
				sqlstring = string.Format("SELECT SUM([answers]) AS [answers],SUM([wrongs]) AS [wrongs] FROM [{0}Exam_ExamLogInfo]", DbConfigs.Prefix);
				ExamLogInfo examLogInfo = this.GetExamLogInfo(sqlstring);
				this.accuracy_total = examLogInfo.accuracy;
				sqlstring = string.Format("SELECT SUM([answers]) AS [answers],SUM([wrongs]) AS [wrongs] FROM [{0}Exam_ExamLogInfo] WHERE [uid]={1}", DbConfigs.Prefix, this.userid);
				examLogInfo = this.GetExamLogInfo(sqlstring);
				this.accuracy_my = examLogInfo.accuracy;
				sqlstring = string.Format("SELECT COUNT(*) FROM (SELECT [uid],SUM(wrongs) AS [wrongs] FROM [{0}Exam_ExamLogInfo] GROUP BY [uid]) AS TA WHERE [wrongs]<{1}", DbConfigs.Prefix, examLogInfo.wrongs);
				this.accuracy_display = FPUtils.StrToInt(this.GetScalarTotal(sqlstring)) + 1;
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("status", WhereType.GreaterThan, 0),
					DbHelper.MakeAndWhere("uid", this.userid)
				};
				List<ExamResult> list = DbHelper.ExecuteList<ExamResult>(OrderBy.ASC, sqlparams);
				int num = 1;
				foreach (ExamResult current in list)
				{
					bool flag2 = this.examresult != "";
					if (flag2)
					{
						this.examresult += ",";
					}
					this.examresult += string.Format("['', {0}]", current.score);
					num++;
				}
				this.userexamlog = ExamBll.GetExamLogList(this.channelinfo.id, this.userid);
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeAndWhere("parentid", 0),
					DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				};
				List<SortInfo> list2 = DbHelper.ExecuteList<SortInfo>(sqlparams2);
				foreach (SortInfo current2 in list2)
				{
					bool flag3 = !FPArray.Contain(this.role.sorts, current2.id) && this.roleid != 1;
					if (!flag3)
					{
						bool flag4 = this.userexamlog.ContainsKey(current2.id);
						if (flag4)
						{
							examLogInfo = this.userexamlog[current2.id];
						}
						else
						{
							examLogInfo = new ExamLogInfo();
						}
						examLogInfo.sortid = current2.id;
						examLogInfo.sortname = current2.name;
						examLogInfo.questions = current2.posts;
						examLogInfo.subcounts = current2.subcounts;
						this.examloglist.Add(examLogInfo);
					}
				}
			}
		}

		protected string GetChildSort(int channelid, int parentid, int level)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(sqlparams);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SortInfo current in list)
			{
				bool flag = !FPArray.Contain(this.role.sorts, current.id) && this.roleid != 1;
				if (!flag)
				{
					bool flag2 = this.userexamlog.ContainsKey(current.id);
					ExamLogInfo examLogInfo;
					if (flag2)
					{
						examLogInfo = this.userexamlog[current.id];
					}
					else
					{
						examLogInfo = new ExamLogInfo();
						examLogInfo.sortid = current.id;
					}
					stringBuilder.AppendFormat("<tr class=\"keypoint keypoint-level-{0} child_row_{1}\">\r\n", level, current.parentid);
					bool flag3 = current.subcounts > 0;
					if (flag3)
					{
						stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span  id=\"row_{1}\" class=\"sprite sprite-expand sprite-expand\"></span>{0}</span></td>\r\n", current.name, current.id);
					}
					else
					{
						stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand sprite-noexpand\"></span>{0}</span></td>\r\n", current.name);
					}
					stringBuilder.AppendFormat("<td class=\"count-col\">{0}道/{1}道</td>\r\n", examLogInfo.answers, current.posts);
					stringBuilder.AppendFormat("<td class=\"count-col\">{0}道</td>\r\n", examLogInfo.answers - examLogInfo.wrongs);
					bool flag4 = examLogInfo.answers > 0;
					if (flag4)
					{
						stringBuilder.AppendFormat("<td class=\"capacity-col\"><span class=\"progressBar\">{0}%</span></td>\r\n", examLogInfo.accuracy);
					}
					else
					{
						stringBuilder.AppendFormat("<td class=\"capacity-col\"><span class=\"progressBar\">{0}%</span></td>\r\n", 0);
					}
					stringBuilder.Append("</tr>\r\n");
					bool flag5 = current.subcounts > 0;
					if (flag5)
					{
						stringBuilder.Append(this.GetChildSort(channelid, current.id, level + 1));
					}
				}
			}
			return stringBuilder.ToString();
		}

		private string GetScalarTotal(string sqlstring)
		{
			double value = 0.0;
			bool flag = !double.TryParse(DbHelper.ExecuteScalar(sqlstring).ToString(), out value);
			string result;
			if (flag)
			{
				result = "0";
			}
			else
			{
				result = Math.Round(value, 2).ToString();
			}
			return result;
		}

		private ExamLogInfo GetExamLogInfo(string sqlstring)
		{
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, sqlstring);
			ExamLogInfo examLogInfo = new ExamLogInfo();
			bool flag = dataReader.Read();
			if (flag)
			{
				examLogInfo.answers = FPUtils.StrToInt(dataReader["answers"]);
				examLogInfo.wrongs = FPUtils.StrToInt(dataReader["wrongs"]);
			}
			dataReader.Close();
			return examLogInfo;
		}
	}
}
