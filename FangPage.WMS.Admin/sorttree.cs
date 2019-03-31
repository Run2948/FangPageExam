using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000043 RID: 67
	public class sorttree : AdminController
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000D028 File Offset: 0x0000B228
		protected override void Controller()
		{
			if (this.channelid > 0)
			{
				this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
			}
			if (this.channelinfo.description == "")
			{
				this.channelinfo.description = this.channelinfo.name;
			}
			this.zNodes = this.GetSortTree(this.sortid);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000D090 File Offset: 0x0000B290
		protected string GetSortTree(int parentid)
		{
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
			string text = "";
			foreach (SortInfo sortInfo in list)
			{
				if (FPArray.Contain(this.role.sorts, sortInfo.id) || this.roleid == 1)
				{
					if (text != "")
					{
						text += ",";
					}
					string text2 = sortInfo.otherurl;
					if (sortInfo.appid > 0 && text2 == "")
					{
						if (this.channelid > 0)
						{
							text2 = "channelid=" + this.channelid;
						}
						text2 += ((text2 == "") ? ("sortid=" + sortInfo.id) : ("&sortid=" + sortInfo.id));
						string[] array = FPArray.SplitString(sortInfo.SortAppInfo.indexpage, ";");
						string text3;
						if (array.Length - 1 > this.index)
						{
							text3 = array[this.index];
						}
						else
						{
							text3 = array[array.Length - 1];
						}
						string str;
						if (text3.StartsWith("{") || text3.StartsWith("/") || text3.StartsWith("http://") || sortInfo.SortAppInfo.guid == "")
						{
							str = text3.Replace("{webpath}", this.webpath);
						}
						else if (sortInfo.SortAppInfo.type == "sites")
						{
							str = this.webpath + sortInfo.SortAppInfo.installpath + "/" + text3;
						}
						else
						{
							str = string.Concat(new string[]
							{
								this.webpath,
								sortInfo.SortAppInfo.type,
								"/",
								sortInfo.SortAppInfo.installpath,
								"/",
								text3
							});
						}
						if (text3.IndexOf("?") > 0)
						{
							text2 = str + "&" + text2;
						}
						else
						{
							text2 = str + "?" + text2;
						}
					}
					string text4 = sortInfo.name;
					if (this.posts == 1)
					{
						text4 = string.Concat(new object[]
						{
							text4,
							"(",
							sortInfo.posts,
							")"
						});
					}
					if (sortInfo.icon == "")
					{
						if (sortInfo.subcounts > 0)
						{
							sortInfo.icon = this.webpath + this.sitepath + "/statics/images/folders.gif";
						}
						else
						{
							sortInfo.icon = this.webpath + this.sitepath + "/statics/images/folder.gif";
						}
					}
					if (sortInfo.subcounts > 0)
					{
						text = string.Concat(new object[]
						{
							text,
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
						text = string.Concat(new object[]
						{
							text,
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
					if (sortInfo.types != "" && sortInfo.showtype == 1)
					{
						List<TypeInfo> typeList = TypeBll.GetTypeList(sortInfo.types);
						for (int i = 0; i < typeList.Count; i++)
						{
							typeList[i].sortid = sortInfo.id;
							if (this.posts == 1)
							{
								text = string.Concat(new object[]
								{
									text,
									",{ id: 10000",
									typeList[i].id,
									", pId: ",
									sortInfo.id,
									", name: \"",
									typeList[i].name,
									"(",
									typeList[i].post.ToString(),
									")\",open:true, url: \"",
									text2,
									"&typeid=",
									typeList[i].id.ToString(),
									"\", target: \"mainframe\", icon: \"",
									this.webpath,
									this.sitepath,
									"/statics/images/type.gif\" }"
								});
							}
							else
							{
								text = string.Concat(new object[]
								{
									text,
									",{ id: 10000",
									typeList[i].id,
									", pId: ",
									sortInfo.id,
									", name: \"",
									typeList[i].name,
									"\",open:true, url: \"",
									text2,
									"&typeid=",
									typeList[i].id.ToString(),
									"\", target: \"mainframe\", icon: \"",
									this.webpath,
									this.sitepath,
									"/statics/images/type.gif\" }"
								});
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x040000B8 RID: 184
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000B9 RID: 185
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000BA RID: 186
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x040000BB RID: 187
		protected int posts = FPRequest.GetInt("posts");

		// Token: 0x040000BC RID: 188
		protected int index = FPRequest.GetInt("index");

		// Token: 0x040000BD RID: 189
		protected string zNodes = "";
	}
}
