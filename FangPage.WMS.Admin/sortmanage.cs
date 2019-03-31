using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003F RID: 63
	public class sortmanage : AdminController
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000C69C File Offset: 0x0000A89C
		protected override void Controller()
		{
			this.channellist = ChannelBll.GetChannelList();
			if (this.channelid == 0 && this.channellist.Count > 0)
			{
				this.channelid = this.channellist[0].id;
			}
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				int @int = FPRequest.GetInt("id");
				if (this.action.Equals("delete"))
				{
					SortInfo sortInfo = DbHelper.ExecuteModel<SortInfo>(@int);
					if (DbHelper.ExecuteDelete<SortInfo>(@int) > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("DELETE FROM [{0}WMS_SortInfo] WHERE [id] IN (SELECT [id] FROM [{0}WMS_SortInfo]  WHERE [parentlist] LIKE '{1},%');", DbConfigs.Prefix, sortInfo.parentlist);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [subcounts]=[subcounts]-1 WHERE [id]={1};", DbConfigs.Prefix, sortInfo.parentid);
						DbHelper.ExecuteSql(stringBuilder.ToString());
					}
				}
				FPCache.Remove("FP_SORTTREE" + this.channelid.ToString());
				base.Response.Redirect("sortmanage.aspx?channelid=" + this.channelid);
			}
			this.sortlist = SortBll.GetSortList(this.channelid, 0);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		protected string ShowChildSort(int parentid, string tree)
		{
			List<SortInfo> sortList = SortBll.GetSortList(this.channelid, parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (SortInfo sortInfo in sortList)
			{
				stringBuilder.AppendLine("<tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">");
				stringBuilder.AppendLine("<td align=\"center\">" + sortInfo.id + "</td>");
				stringBuilder.AppendLine("<td align=\"left\">" + tree);
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
				stringBuilder.Append("<img src=\"" + sortInfo.icon + "\" width=\"16\" height=\"16\"  />");
				if (sortInfo.subcounts > 0)
				{
					stringBuilder.Append("<span style=\"font-weight:bold;\">" + sortInfo.name + "</span></td>");
				}
				else
				{
					stringBuilder.Append(sortInfo.name + "</td>");
				}
				stringBuilder.AppendLine("<td>" + sortInfo.markup + "</td>");
				if (sortInfo.SortAppInfo.name != "")
				{
					stringBuilder.AppendLine("<td>" + sortInfo.SortAppInfo.name + "</td>");
				}
				else
				{
					stringBuilder.AppendLine("<td>无</td>");
				}
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"sortadd.aspx?channelid=",
					this.channelid,
					"&parentid=",
					sortInfo.id,
					"\">添加子栏目</a></td>"
				}));
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"sortadd.aspx?channelid=",
					this.channelid,
					"&id=",
					sortInfo.id,
					"\">编辑</a></td>"
				}));
				stringBuilder.AppendLine("<td><a id=\"submitdel\" onclick=\"DeleteSort(" + sortInfo.id + ")\" href=\"#\">删除</a></td>");
				stringBuilder.AppendLine(string.Concat(new object[]
				{
					"<td><a style=\"color: #1317fc\"  href=\"sortdisplay.aspx?channelid=",
					this.channelid,
					"&parentid=",
					sortInfo.parentid,
					"\">排序</a></td></tr>"
				}));
				stringBuilder.Append(this.ShowChildSort(sortInfo.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000AB RID: 171
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000AC RID: 172
		protected List<ChannelInfo> channellist = new List<ChannelInfo>();

		// Token: 0x040000AD RID: 173
		protected List<SortInfo> sortlist = new List<SortInfo>();
	}
}
