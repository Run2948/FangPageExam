using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.Exam;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000035 RID: 53
	public class testview : LoginController
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00016C80 File Offset: 0x00014E80
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				List<SortAppInfo> sortAppList = SortBll.GetSortAppList("exam_");
				string text = "";
				foreach (SortAppInfo sortAppInfo in sortAppList)
				{
					if (text != "")
					{
						text += ",";
					}
					text += sortAppInfo.id;
				}
				this.zNodes = this.GetSortTree(this.channelinfo.id, 0, text);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00016D70 File Offset: 0x00014F70
		private string GetSortTree(int channelid, int parentid, string appids)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("appid", WhereType.In, appids),
				DbHelper.MakeAndWhere("hidden", 0)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
			string text = "";
			foreach (SortInfo sortInfo in list)
			{
				if (base.ischecked(sortInfo.id, this.role.sorts) || this.roleid == 1)
				{
					if (text != "")
					{
						text += ",";
					}
					string text2 = "";
					if (sortInfo.icon != "")
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"{ id: ",
							sortInfo.id,
							", pId: ",
							parentid,
							", name: \"",
							sortInfo.name,
							"(",
							sortInfo.posts,
							")\",",
							text2,
							"target: \"mainframe\", icon: \"",
							sortInfo.icon,
							"\" }"
						});
					}
					else if (sortInfo.subcounts > 0)
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"{ id: ",
							sortInfo.id,
							", pId: ",
							parentid,
							", name: \"",
							sortInfo.name,
							"(",
							sortInfo.posts,
							")\",",
							text2,
							"target: \"mainframe\", icon: \"",
							this.webpath,
							(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
							"images/folders.gif\" }"
						});
					}
					else
					{
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							"{ id: ",
							sortInfo.id,
							", pId: ",
							parentid,
							", name: \"",
							sortInfo.name,
							"(",
							sortInfo.posts,
							")\",",
							text2,
							"target: \"mainframe\", icon: \"",
							this.webpath,
							(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
							"images/folder.gif\" }"
						});
					}
					if (sortInfo.subcounts > 0)
					{
						text = text + "," + this.GetSortTree(channelid, sortInfo.id, appids);
					}
				}
			}
			return text;
		}

		// Token: 0x04000111 RID: 273
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000112 RID: 274
		protected ExamConfig examconfig = new ExamConfig();

		// Token: 0x04000113 RID: 275
		protected string zNodes = "";
	}
}
