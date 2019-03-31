using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;
using System.Collections.Generic;
using System.IO;

namespace FP_Attach.Controller
{
	public class download2 : WebController
	{
		protected string aid = FPRequest.GetString("aid");

		protected string attachid = FPRequest.GetString("attachid");

		protected string filename = FPRequest.GetString("filename");

		protected override void Controller()
		{
			List<SqlParam> list = new List<SqlParam>();
			if (this.aid != "")
			{
				if (!FPUtils.IsNumericArray(this.aid))
				{
					this.ShowErr("对不起，附件参数错误。");
					return;
				}
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.aid));
			}
			if (this.attachid != "")
			{
				list.Add(DbHelper.MakeAndWhere("attachid", WhereType.In, this.attachid));
			}
			List<AttachInfo> list2 = DbHelper.ExecuteList<AttachInfo>(list.ToArray());
			if (list2.Count == 0)
			{
				this.ShowErr("对不起，没有附件文件。");
				return;
			}
			if (list2.Count == 1)
			{
				if (this.filename == "" || this.attachid != "")
				{
					this.filename = list2[0].name;
				}
				else
				{
					this.filename = Path.GetFileNameWithoutExtension(this.filename) + Path.GetExtension(list2[0].name);
				}
				DbHelper.ExecuteUpdate<AttachInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("downloads", list2[0].downloads + 1),
					DbHelper.MakeAndWhere("id", list2[0].id)
				});
				FPResponse.WriteDown(FPFile.GetMapPath(list2[0].filename), this.filename);
				return;
			}
			if (this.filename == "")
			{
				this.filename = "附件批量下载.zip";
			}
			else
			{
				this.filename = Path.GetFileNameWithoutExtension(this.filename) + ".zip";
			}
			using (FPZip fPZip = new FPZip())
			{
				DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_AttachInfo] SET [downloads]=[downloads]+1 WHERE [id] IN({1})", DbConfigs.Prefix, this.aid));
				foreach (AttachInfo current in list2)
				{
					fPZip.AddFile(FPFile.GetMapPath(current.filename), current.name);
				}
				fPZip.ZipDown(FPUtils.UrlEncode(this.filename));
			}
		}
	}
}
