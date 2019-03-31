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
	public class favorite : LoginController
	{
		protected ChannelInfo channelinfo = new ChannelInfo();

		protected List<ExamLogInfo> examloglist = new List<ExamLogInfo>();

		protected string sortappidlist = "";

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
				string commandText = string.Format("SELECT SUM([curfavs]) AS [total] FROM [{0}Exam_ExamLogInfo] WHERE [channelid]={1} AND [uid]={2}", DbConfigs.Prefix, this.channelinfo.id, this.userid);
				this.total = FPUtils.StrToInt(DbHelper.ExecuteScalar(commandText));
				this.userexamlog = ExamBll.GetExamLogList(this.channelinfo.id, this.userid);
				List<SortAppInfo> sortAppList = SortBll.GetSortAppList("exam_");
				foreach (SortAppInfo current in sortAppList)
				{
					bool flag2 = this.sortappidlist != "";
					if (flag2)
					{
						this.sortappidlist += ",";
					}
					this.sortappidlist += current.id;
				}
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("parentid", 0),
					DbHelper.MakeAndWhere("channelid", this.channelinfo.id),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				};
				List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(sqlparams);
				foreach (SortInfo current2 in list)
				{
					bool flag3 = !FPArray.Contain(this.role.sorts, current2.id) && this.roleid != 1;
					if (!flag3)
					{
						bool flag4 = this.userexamlog.ContainsKey(current2.id);
						ExamLogInfo examLogInfo;
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
						bool flag4 = examLogInfo.favs > 0;
						if (flag4)
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_{0}\" class=\"sprite sprite-expand i-20\"></span>\r\n", current.id);
							stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=fav\" target=\"_blank\" class=\"btn btn-link link-button\">{1}(共{2}道收藏)</a>\r\n", current.id, current.name, examLogInfo.favs);
							stringBuilder.AppendFormat("</span></td>\r\n", new object[0]);
						}
						else
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_{1}\" class=\"sprite sprite-expand i-20\"></span>{0}(共{2}道收藏)</span></td>\r\n", current.name, current.id, examLogInfo.favs);
						}
					}
					else
					{
						bool flag5 = examLogInfo.favs > 0;
						if (flag5)
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand-holder i-20\"></span>\r\n", new object[0]);
							stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=fav\" target=\"_blank\" class=\"btn btn-link link-button\">{1}(共{2}道收藏)</a>\r\n", current.id, current.name, examLogInfo.favs);
							stringBuilder.AppendFormat("</span></td>\r\n", new object[0]);
						}
						else
						{
							stringBuilder.AppendFormat("<td class=\"name-col\"><span class=\"text\"><span class=\"sprite sprite-expand-holder i-20\"></span>{0}(共{1}道收藏)</span></td>\r\n", current.name, examLogInfo.favs);
						}
					}
					stringBuilder.AppendFormat("<td class=\"button-col\">", new object[0]);
					bool flag6 = examLogInfo.favs > 0;
					if (flag6)
					{
						stringBuilder.AppendFormat("<a href=\"questionview.aspx?sortid={0}&action=fav\" target=\"_blank\" class=\"btn btn-link link-button\"><span class=\"btn-inner\">查看题目</span></a>", current.id);
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
