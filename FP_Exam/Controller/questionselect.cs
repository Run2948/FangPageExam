using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class questionselect : AdminController
	{
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		protected int paper = FPRequest.GetInt("paper");

		protected ExamInfo examinfo = new ExamInfo();

		protected SortInfo sortinfo = new SortInfo();

		protected ExamTopic examtopic = new ExamTopic();

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected string zNodes = "";

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examtopic.examid);
			this.sortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			this.zNodes = this.GetSortTree(this.channelinfo.id, 0);
		}

		private string GetSortTree(int channelid, int parentid)
		{
			List<SortInfo> sortList = SortBll.GetSortList(channelid, parentid);
			string text = "";
			foreach (SortInfo current in sortList)
			{
				bool flag = !FPArray.Contain(this.role.sorts, current.id) && this.roleid != 1;
				if (!flag)
				{
					string text2 = string.Format("examtopicselect.aspx?examtopicid={0}&sortid={1}&paper={2}", this.examtopicid, current.id, this.paper);
					string childSorts = SortBll.GetChildSorts(current.id);
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts),
						DbHelper.MakeAndWhere("type", this.examtopic.type)
					};
					int num = DbHelper.ExecuteCount<ExamQuestion>(sqlparams);
					bool flag2 = text != "";
					if (flag2)
					{
						text += ",";
					}
					bool flag3 = current.subcounts > 0;
					if (flag3)
					{
						text = string.Concat(new object[]
						{
							text,
							"{ id: ",
							current.id,
							", pId: ",
							parentid,
							", name: \"",
							current.name,
							"(",
							num,
							")\",open:true, url: \"",
							text2,
							"\", target: \"frmmaindetail\", icon: \"",
							this.adminpath,
							"statics/images/folders.gif\" }"
						});
						string sortTree = this.GetSortTree(channelid, current.id);
						bool flag4 = sortTree != "";
						if (flag4)
						{
							text = text + "," + sortTree;
						}
					}
					else
					{
						text = string.Concat(new object[]
						{
							text,
							"{ id: ",
							current.id,
							", pId: ",
							parentid,
							", name: \"",
							current.name,
							"(",
							num,
							")\",open:true, url: \"",
							text2,
							"\", target: \"frmmaindetail\", icon: \"",
							this.adminpath,
							"statics/images/folder.gif\" }"
						});
					}
					bool flag5 = current.types != "" && current.showtype == 1;
					if (flag5)
					{
						List<TypeInfo> typeList = TypeBll.GetTypeList(current.types);
						for (int i = 0; i < typeList.Count; i++)
						{
							SqlParam[] sqlparams2 = new SqlParam[]
							{
								DbHelper.MakeAndWhere("typelist", WhereType.Contain, typeList[i].id),
								DbHelper.MakeAndWhere("sortid", current.id),
								DbHelper.MakeAndWhere("type", this.examtopic.type)
							};
							num = DbHelper.ExecuteCount<ExamQuestion>(sqlparams2);
							typeList[i].sortid = current.id;
							text = string.Concat(new object[]
							{
								text,
								",{ id: 10000",
								typeList[i].id,
								", pId: ",
								current.id,
								", name: \"",
								typeList[i].name,
								"(",
								num,
								")\",open:true, url: \"",
								text2,
								"&typeid=",
								typeList[i].id.ToString(),
								"\", target: \"frmmaindetail\", icon: \"",
								this.adminpath,
								"statics/images/type.gif\" }"
							});
						}
					}
				}
			}
			return text;
		}
	}
}
