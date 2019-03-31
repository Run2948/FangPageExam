using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200001A RID: 26
	public class questionselect : AdminController
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000CD04 File Offset: 0x0000AF04
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examtopic.examid);
			this.sortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			this.zNodes = this.GetSortTree(this.channelinfo.id, 0);
			base.SaveRightURL();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000CD80 File Offset: 0x0000AF80
		private string GetSortTree(int channelid, int parentid)
		{
			List<SortInfo> sortList = SortBll.GetSortList(channelid, parentid);
			string text = "";
			foreach (SortInfo sortInfo in sortList)
			{
				if (base.ischecked(sortInfo.id, this.role.sorts) || this.roleid == 1)
				{
					string text2 = string.Format("examtopicselect.aspx?examtopicid={0}&sortid={1}&paper={2}", this.examtopicid, sortInfo.id, this.paper);
					string childSorts = SortBll.GetChildSorts(sortInfo.id);
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts),
						DbHelper.MakeAndWhere("type", this.examtopic.type)
					};
					int num = DbHelper.ExecuteCount<ExamQuestion>(sqlparams);
					if (text != "")
					{
						text += ",";
					}
					if (sortInfo.subcounts > 0)
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
							num,
							")\",open:true, url: \"",
							text2,
							"\", target: \"frmmaindetail\", icon: \"",
							this.webpath,
							(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
							"images/folders.gif\" }"
						});
						string sortTree = this.GetSortTree(channelid, sortInfo.id);
						if (sortTree != "")
						{
							text = text + "," + sortTree;
						}
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
							num,
							")\",open:true, url: \"",
							text2,
							"\", target: \"frmmaindetail\", icon: \"",
							this.webpath,
							(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
							"images/folder.gif\" }"
						});
					}
				}
			}
			return text;
		}

		// Token: 0x0400007F RID: 127
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		// Token: 0x04000080 RID: 128
		protected int paper = FPRequest.GetInt("paper");

		// Token: 0x04000081 RID: 129
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000082 RID: 130
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000083 RID: 131
		protected ExamTopic examtopic = new ExamTopic();

		// Token: 0x04000084 RID: 132
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000085 RID: 133
		protected string zNodes = "";
	}
}
