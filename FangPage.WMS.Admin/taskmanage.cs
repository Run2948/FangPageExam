using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000024 RID: 36
	public class taskmanage : SuperController
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00007428 File Offset: 0x00005628
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<TaskInfo>(FPRequest.GetString("chkid"));
				}
				else if (this.action == "download")
				{
					TaskInfo taskInfo = DbHelper.ExecuteModel<TaskInfo>(FPRequest.GetInt("tid"));
					string text = FPArray.SplitString(taskInfo.type, ",", 2)[1];
					if (text == "")
					{
						this.ShowErr("任务类型不正确。");
						return;
					}
					if (!File.Exists(FPFile.GetMapPath(this.webpath + "bin/" + text + ".dll")))
					{
						this.ShowErr("任务类库DLL不存在或已被删除。");
						return;
					}
					using (FPZip fpzip = new FPZip())
					{
						if (File.Exists(FPFile.GetMapPath(this.webpath + "cache/task.config")))
						{
							File.Delete(FPFile.GetMapPath(this.webpath + "cache/task.config"));
						}
						FPSerializer.Save<TaskInfo>(taskInfo, FPFile.GetMapPath(this.webpath + "cache/task.config"));
						fpzip.AddFile(FPFile.GetMapPath(this.webpath + "cache/task.config"), "");
						fpzip.AddFile(FPFile.GetMapPath(this.webpath + "bin/" + text + ".dll"), "");
						fpzip.ZipDown(FPUtils.UrlEncode(taskInfo.name + ".task"));
					}
				}
			}
			this.tasklist = DbHelper.ExecuteList<TaskInfo>(OrderBy.ASC);
		}

		// Token: 0x04000051 RID: 81
		protected List<TaskInfo> tasklist = new List<TaskInfo>();
	}
}
