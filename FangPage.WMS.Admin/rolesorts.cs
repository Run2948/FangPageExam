using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000041 RID: 65
	public class rolesorts : SuperController
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000D124 File Offset: 0x0000B324
		protected override void View()
		{
			this.roleinfo = DbHelper.ExecuteModel<RoleInfo>(this.rid);
			if (this.roleinfo.id == 0)
			{
				this.ShowErr("对不起，该角色不存在或已被删除。");
			}
			else
			{
				if (this.ispost)
				{
					string @string = FPRequest.GetString("sorts");
					string text = "";
					string[] array = FPUtils.SplitString(@string);
					int i = 0;
					while (i < array.Length)
					{
						string text2 = array[i];
						if (text != "")
						{
							text += ",";
						}
						if (text2.Length > 1)
						{
							if (FPUtils.StrToInt(text2.Substring(1, text2.Length - 1)) != 0)
							{
								text += text2.Substring(1, text2.Length - 1);
							}
						}
						IL_E4:
						i++;
						continue;
						goto IL_E4;
					}
					this.roleinfo.sorts = text;
					DbHelper.ExecuteUpdate<RoleInfo>(this.roleinfo);
					if (this.roleinfo.id == this.roleid)
					{
						base.ResetUser();
					}
					base.Response.Redirect(this.pagename + "?rid=" + this.rid);
				}
				List<ChannelInfo> channelList = ChannelBll.GetChannelList();
				foreach (ChannelInfo channelInfo in channelList)
				{
					if (this.zNodes != "")
					{
						this.zNodes += ",";
					}
					object obj = this.zNodes;
					this.zNodes = string.Concat(new object[]
					{
						obj,
						"{ id: ",
						channelInfo.id,
						"0, pId: 0, name: \"",
						channelInfo.name,
						"\",open:true, icon: \"",
						this.webpath,
						(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
						"images/sysmenu1.gif\" }"
					});
					string sortTree = this.GetSortTree(channelInfo.id, 0);
					if (sortTree != "")
					{
						this.zNodes = this.zNodes + "," + sortTree;
					}
				}
				base.SaveRightURL();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000D40C File Offset: 0x0000B60C
		private string GetSortTree(int channelid, int parentid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("channelid", channelid),
				DbHelper.MakeAndWhere("parentid", parentid)
			};
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(sqlparams);
			string text = "";
			foreach (SortInfo sortInfo in list)
			{
				if (text != "")
				{
					text += ",";
				}
				string text2 = "";
				if (base.ischecked(sortInfo.id, this.roleinfo.sorts) || this.roleinfo.id == 1)
				{
					text2 = "checked:true,";
				}
				if (sortInfo.subcounts > 0)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
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
						(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
						"images/folder.gif\" }"
					});
				}
			}
			return text;
		}

		// Token: 0x040000A0 RID: 160
		protected int rid = FPRequest.GetInt("rid");

		// Token: 0x040000A1 RID: 161
		protected RoleInfo roleinfo = new RoleInfo();

		// Token: 0x040000A2 RID: 162
		protected string zNodes = "";
	}
}
