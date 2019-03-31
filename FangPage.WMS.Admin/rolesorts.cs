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
	// Token: 0x0200004C RID: 76
	public class rolesorts : SuperController
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000EE74 File Offset: 0x0000D074
		protected override void Controller()
		{
			this.roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.rid);
			if (this.roleinfo.id == 0)
			{
				this.ShowErr("对不起，该角色不存在或已被删除。");
				return;
			}
			if (this.ispost)
			{
				string @string = FPRequest.GetString("sorts");
				string text = "";
				foreach (string text2 in FPArray.SplitString(@string))
				{
					if (text != "")
					{
						text += ",";
					}
					if (text2.Length > 1 && FPUtils.StrToInt(text2.Substring(1, text2.Length - 1)) != 0)
					{
						text += text2.Substring(1, text2.Length - 1);
					}
				}
				this.roleinfo.sorts = text;
				DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
				if (this.roleinfo.id == this.roleid)
				{
					base.ResetUser();
				}
				FPCache.Remove("FP_ROLELIST");
				base.Response.Redirect(this.pagename + "?rid=" + this.rid);
			}
			foreach (ChannelInfo channelInfo in ChannelBll.GetChannelList())
			{
				if (this.zNodes != "")
				{
					this.zNodes += ",";
				}
				this.zNodes = string.Concat(new object[]
				{
					this.zNodes,
					"{ id: ",
					channelInfo.id,
					"0, pId: 0, name: \"",
					channelInfo.name,
					"\",open:true, icon: \"",
					this.webpath,
					this.sitepath,
					"/statics/images/sysmenu1.gif\" }"
				});
				string sortTree = this.GetSortTree(channelInfo.id, 0);
				if (sortTree != "")
				{
					this.zNodes = this.zNodes + "," + sortTree;
				}
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000F098 File Offset: 0x0000D298
		private string GetSortTree(int channelid, int parentid)
		{
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
			string text = "";
			foreach (SortInfo sortInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				string text2 = "";
				if (FPArray.Contain(this.roleinfo.sorts, sortInfo.id))
				{
					text2 = "checked:true,";
				}
				if (sortInfo.subcounts > 0)
				{
					text = string.Concat(new object[]
					{
						text,
						"{ id: ",
						channelid,
						sortInfo.id,
						", pId: ",
						channelid,
						parentid,
						", name: \"",
						sortInfo.name,
						"\",",
						text2,
						"open:true, icon: \"",
						this.webpath,
						this.sitepath,
						"/statics/images/folders.gif\" }"
					});
					string sortTree = this.GetSortTree(channelid, sortInfo.id);
					if (sortTree != "")
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
						channelid,
						sortInfo.id,
						", pId: ",
						channelid,
						parentid,
						", name: \"",
						sortInfo.name,
						"\",",
						text2,
						"open:true, icon: \"",
						this.webpath,
						this.sitepath,
						"/statics/images/folder.gif\" }"
					});
				}
			}
			return text;
		}

		// Token: 0x040000D9 RID: 217
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x040000DA RID: 218
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x040000DB RID: 219
		protected string zNodes = "";
	}
}
