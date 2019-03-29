using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000036 RID: 54
	public class sorttree : AdminController
	{
		// Token: 0x06000082 RID: 130 RVA: 0x0000B5F1 File Offset: 0x000097F1
		protected override void View()
		{
			this.zNodes = this.GetSortTree(this.sortid);
			base.SaveLeftURL(this.fullname);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000B614 File Offset: 0x00009814
		protected string GetSortTree(int parentid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelid)
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
					if (sortInfo.appid > 0)
					{
						if (this.channelid > 0)
						{
							text2 = "channelid=" + this.channelid;
						}
						text2 += ((text2 == "") ? ("sortid=" + sortInfo.id) : ("&sortid=" + sortInfo.id));
						string str;
						if (sortInfo.SortAppInfo.indexpage.StartsWith("{") || sortInfo.SortAppInfo.indexpage.StartsWith("/") || sortInfo.SortAppInfo.appid == 0)
						{
							str = sortInfo.SortAppInfo.indexpage.Replace("{webpath}", this.webpath);
						}
						else
						{
							str = this.webpath + sortInfo.SortAppInfo.installpath + "/" + sortInfo.SortAppInfo.indexpage;
						}
						if (sortInfo.SortAppInfo.indexpage.IndexOf("?") > 0)
						{
							text2 = str + text2;
						}
						else
						{
							text2 = str + "?" + text2;
						}
					}
					string text3 = "";
					if (sortInfo.hidden == 1)
					{
						text3 = "_hidden";
					}
					string text4 = sortInfo.name;
					if (this.posts == 1)
					{
						object obj = text4;
						text4 = string.Concat(new object[]
						{
							obj,
							"(",
							sortInfo.posts,
							")"
						});
					}
					if (sortInfo.icon == "")
					{
						if (sortInfo.subcounts > 0)
						{
							sortInfo.icon = string.Concat(new string[]
							{
								this.webpath,
								(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
								"images/folders",
								text3,
								".gif"
							});
						}
						else
						{
							sortInfo.icon = string.Concat(new string[]
							{
								this.webpath,
								(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
								"images/folder",
								text3,
								".gif"
							});
						}
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
							text4,
							"\",font:{'font-weight':'bold'},open:true, url: \"",
							text2,
							"\", target: \"mainframe\", icon: \"",
							sortInfo.icon,
							"\" }"
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
							text4,
							"\",open:true, url: \"",
							text2,
							"\", target: \"mainframe\", icon: \"",
							sortInfo.icon,
							"\" }"
						});
					}
					if (sortInfo.subcounts > 0)
					{
						string sortTree = this.GetSortTree(sortInfo.id);
						if (sortTree != "")
						{
							text = text + "," + sortTree;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04000080 RID: 128
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000081 RID: 129
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x04000082 RID: 130
		protected int posts = FPRequest.GetInt("posts");

		// Token: 0x04000083 RID: 131
		protected string zNodes = "";
	}
}
