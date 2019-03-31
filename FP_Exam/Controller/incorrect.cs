using FangPage.Common;
using FangPage.Data;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP_Exam.Controller
{
	public class incorrect : LoginController
	{
		protected ChannelInfo channelinfo = new ChannelInfo();

		protected List<ExamLogInfo> examloglist = new List<ExamLogInfo>();

		protected Dictionary<int, ExamLogInfo> userexamlog = new Dictionary<int, ExamLogInfo>();

		protected int total = 0;

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
				string commandText = string.Format("SELECT SUM([curwrongs]) AS [totalwrongs] FROM [{0}Exam_ExamLogInfo] WHERE [channelid]={1} AND [uid]={2}", DbConfigs.Prefix, this.channelinfo.id, this.userid);
				this.total = FPUtils.StrToInt(DbHelper.ExecuteScalar(commandText));
				this.userexamlog = ExamBll.GetExamLogList(this.channelinfo.id, this.userid);
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("parentid", 0),
					DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				};
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(sqlparams);
				foreach (SortInfo current in list)
				{
					bool flag2 = !FPArray.Contain(this.role.sorts, current.id) && this.roleid != 1;
					if (!flag2)
					{
						bool flag3 = this.userexamlog.ContainsKey(current.id);
						ExamLogInfo examLogInfo;
						if (flag3)
						{
							examLogInfo = this.userexamlog[current.id];
						}
						else
						{
							examLogInfo = new ExamLogInfo();
						}
						examLogInfo.sortid = current.id;
						examLogInfo.sortname = current.name;
						examLogInfo.questions = current.posts;
						examLogInfo.subcounts = current.subcounts;
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
				DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
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
						bool flag4 = examLogInfo.wrongs > 0;
						if (flag4)
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_{0}\" class=\"sprite sprite-expand i-20\"></span>\r\n", current.id);
							stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=wrong\" target=\"_blank\" class=\"btn btn-link link-button\">{1}(共{2}道错题)</a>\r\n", current.id, current.name, examLogInfo.wrongs);
							stringBuilder.AppendFormat("</span></td>\r\n", new object[0]);
						}
						else
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_{1}\" class=\"sprite sprite-expand i-20\"></span>{0}(共{2}道错题)</span></td>\r\n", current.name, current.id, examLogInfo.wrongs);
						}
					}
					else
					{
						bool flag5 = examLogInfo.wrongs > 0;
						if (flag5)
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand-holder i-20\"></span>\r\n", new object[0]);
							stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=wrong\" target=\"_blank\" class=\"btn btn-link link-button\">{1}(共{2}道错题)</a>\r\n", current.id, current.name, examLogInfo.wrongs);
							stringBuilder.AppendFormat("</span></td>\r\n", new object[0]);
						}
						else
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand-holder i-20\"></span>{0}(共{1}道错题)</span></td>\r\n", current.name, examLogInfo.wrongs);
						}
					}
					stringBuilder.AppendFormat("<td class=\"button-col\">", new object[0]);
					bool flag6 = examLogInfo.wrongs > 0;
					if (flag6)
					{
						stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=wrong\" target=\"_blank\" class=\"btn btn-link link-button\"><span class=\"btn-inner\">查看题目</span></a>", current.id);
					}
					else
					{
						stringBuilder.AppendFormat("<span class=\"btn-inner\">查看题目</span>", new object[0]);
					}
					stringBuilder.Append("</td></tr>\r\n");
					bool flag7 = current.subcounts > 0;
					if (flag7)
					{
						stringBuilder.Append(this.GetChildSort(channelid, current.id, level + 1));
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
