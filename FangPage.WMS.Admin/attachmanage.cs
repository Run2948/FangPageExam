using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000007 RID: 7
	public class attachmanage : SuperController
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000285C File Offset: 0x00000A5C
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("chkid");
				if (this.action == "delete")
				{
					AttachBll.DeleteById(@string);
				}
				else if (this.action == "download")
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, @string);
					List<AttachInfo> list = DbHelper.ExecuteList<AttachInfo>(new SqlParam[]
					{
						sqlParam
					});
					if (list.Count == 0)
					{
						this.ShowErr("对不起，没有附件文件。");
						return;
					}
					if (list.Count == 1)
					{
						DbHelper.ExecuteUpdate<AttachInfo>(new SqlParam[]
						{
							DbHelper.MakeUpdate("downloads", list[0].downloads + 1),
							DbHelper.MakeAndWhere("id", list[0].id)
						});
						FPResponse.WriteDown(FPFile.GetMapPath(list[0].filename), list[0].name);
					}
					else
					{
						string str = "附件批量下载.zip";
						using (FPZip fpzip = new FPZip())
						{
							DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_AttachInfo] SET [downloads]=[downloads]+1 WHERE [id] IN({1})", DbConfigs.Prefix, @string));
							foreach (AttachInfo attachInfo in this.attachlist)
							{
								fpzip.AddFile(FPFile.GetMapPath(attachInfo.filename), attachInfo.name);
							}
							fpzip.ZipDown(FPUtils.UrlEncode(str));
						}
					}
				}
				base.Response.Redirect(this.rawurl);
			}
			List<SqlParam> list2 = new List<SqlParam>();
			if (this.filename != "")
			{
				list2.Add(DbHelper.MakeAndWhere("name", WhereType.Like, this.filename));
			}
			if (this.author != "")
			{
				string userIdList = UserBll.GetUserIdList(this.author);
				if (userIdList != "")
				{
					list2.Add(DbHelper.MakeAndWhere("uid", WhereType.In, userIdList));
				}
			}
			if (this.starttime != "")
			{
				list2.Add(DbHelper.MakeAndWhere("updatetime", WhereType.GreaterThanEqual, this.starttime));
			}
			if (this.endtime != "")
			{
				list2.Add(DbHelper.MakeAndWhere("updatetime", WhereType.LessThanEqual, this.endtime));
			}
			this.attachlist = DbHelper.ExecuteList<AttachInfo>(this.pager, list2.ToArray());
			this.plugininfo = PluginConfigs.GetPluInfoByMarkup("attach_viewer");
		}

		// Token: 0x0400000A RID: 10
		protected string filename = FPRequest.GetString("filename");

		// Token: 0x0400000B RID: 11
		protected string author = FPRequest.GetString("author");

		// Token: 0x0400000C RID: 12
		protected string starttime = FPRequest.GetString("starttime");

		// Token: 0x0400000D RID: 13
		protected string endtime = FPRequest.GetString("endtime");

		// Token: 0x0400000E RID: 14
		protected List<AttachInfo> attachlist = new List<AttachInfo>();

		// Token: 0x0400000F RID: 15
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x04000010 RID: 16
		protected PluginConfig plugininfo = new PluginConfig();
	}
}
