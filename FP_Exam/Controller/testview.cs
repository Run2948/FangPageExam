using FangPage.Common;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class testview : LoginController
	{
		protected ChannelInfo channelinfo = new ChannelInfo();

		protected ExamConfig examconfig = new ExamConfig();

		protected string zNodes = "";

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			bool flag = this.channelinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				this.zNodes = this.GetSortTree(this.channelinfo.id, 0);
			}
		}

		private string GetSortTree(int channelid, int parentid)
		{
			List<SortInfo> sortList = SortBll.GetSortList(channelid, parentid);
			string text = "";
			foreach (SortInfo current in sortList)
			{
				bool flag = !FPArray.Contain(this.role.sorts, current.id);
				if (!flag)
				{
					bool flag2 = text != "";
					if (flag2)
					{
						text += ",";
					}
					bool flag3 = current.icon != "";
					if (flag3)
					{
						text = string.Concat(new object[]
						{
							text,
							"{ id: ",
							current.id,
							",value:\"",
							current.id,
							"_0\", pId: ",
							parentid,
							", name: \"",
							current.name,
							"(",
							current.posts,
							")\",icon: \"",
							current.icon,
							"\" }"
						});
					}
					else
					{
						bool flag4 = current.subcounts > 0;
						if (flag4)
						{
							text = string.Concat(new object[]
							{
								text,
								"{ id: ",
								current.id,
								",value:\"",
								current.id,
								"_0\", pId: ",
								parentid,
								", name: \"",
								current.name,
								"(",
								current.posts,
								")\",icon: \"",
								this.adminpath,
								"statics/images/folders.gif\" }"
							});
						}
						else
						{
							text = string.Concat(new object[]
							{
								text,
								"{ id: ",
								current.id,
								",value:\"",
								current.id,
								"_0\", pId: ",
								parentid,
								", name: \"",
								current.name,
								"(",
								current.posts,
								")\",icon: \"",
								this.adminpath,
								"statics/images/folder.gif\" }"
							});
						}
					}
					bool flag5 = current.subcounts > 0;
					if (flag5)
					{
						text = text + "," + this.GetSortTree(channelid, current.id);
					}
					bool flag6 = current.types != "" && current.showtype == 1;
					if (flag6)
					{
						List<TypeInfo> typeList = TypeBll.GetTypeList(current.types);
						for (int i = 0; i < typeList.Count; i++)
						{
							typeList[i].sortid = current.id;
							text = string.Concat(new object[]
							{
								text,
								",{ id: 111111",
								typeList[i].id,
								",value:\"",
								current.id,
								"_",
								typeList[i].id,
								"\", pId: ",
								current.id,
								", name: \"",
								typeList[i].name,
								"(",
								typeList[i].post,
								")\", icon: \"",
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
