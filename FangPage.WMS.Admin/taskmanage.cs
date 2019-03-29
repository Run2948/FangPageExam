using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Task;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001E RID: 30
	public class taskmanage : SuperController
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00006580 File Offset: 0x00004780
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					string @string = FPRequest.GetString("chkid");
					DbHelper.ExecuteDelete<TaskInfo>(@string);
					TaskManager.Reset();
				}
				else if (this.action == "download")
				{
					int @int = FPRequest.GetInt("tid");
					TaskInfo taskInfo = DbHelper.ExecuteModel<TaskInfo>(@int);
					string text = FPUtils.SplitString(taskInfo.type, ",", 2)[1];
					if (text == "")
					{
						this.ShowErr("任务类型不正确。");
						return;
					}
					if (!File.Exists(FPUtils.GetMapPath(this.webpath + "bin/" + text + ".dll")))
					{
						this.ShowErr("任务类库DLL不存在或已被删除。");
						return;
					}
					using (FPZip fpzip = new FPZip())
					{
						if (File.Exists(FPUtils.GetMapPath(this.webpath + "cache/task.config")))
						{
							File.Delete(FPUtils.GetMapPath(this.webpath + "cache/task.config"));
						}
						FPSerializer.Save<TaskInfo>(taskInfo, FPUtils.GetMapPath(this.webpath + "cache/task.config"));
						fpzip.AddFile(FPUtils.GetMapPath(this.webpath + "cache/task.config"), "");
						fpzip.AddFile(FPUtils.GetMapPath(this.webpath + "bin/" + text + ".dll"), "");
						fpzip.ZipDown(FPUtils.UrlEncode(taskInfo.name + ".task"));
					}
				}
				else if (this.action == "run")
				{
					int @int = FPRequest.GetInt("tid");
					TaskInfo taskInfo = DbHelper.ExecuteModel<TaskInfo>(@int);
					if (taskInfo.id == 0)
					{
						this.ShowErr("对不起，该任务不存在或已被删除。");
						return;
					}
					try
					{
						TaskManager.Execute(taskInfo);
					}
					catch (Exception ex)
					{
						this.ShowErr("执行计划任务失败:" + ex.Message);
						return;
					}
				}
			}
			this.tasklist = DbHelper.ExecuteList<TaskInfo>(OrderBy.ASC);
			base.SaveRightURL();
		}

		// Token: 0x04000039 RID: 57
		protected List<TaskInfo> tasklist = new List<TaskInfo>();
	}
}
