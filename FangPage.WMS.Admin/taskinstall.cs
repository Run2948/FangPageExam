using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000016 RID: 22
	public class taskinstall : SuperController
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000046FC File Offset: 0x000028FC
		protected override void Controller()
		{
			if (this.ispost)
			{
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				if (Path.GetExtension(fileName).ToLower() != ".task")
				{
					this.ShowErr("对不起，该文件不是方配系统任务安装文件类型。");
					return;
				}
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				if (File.Exists(mapPath + "\\" + fileName))
				{
					File.Delete(mapPath + "\\" + fileName);
				}
				FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
				if (Directory.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName)))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
				}
				FPZip.UnZip(mapPath + "\\" + fileName, "");
				File.Delete(mapPath + "\\" + fileName);
				if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\task.config"))
				{
					Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
					this.ShowErr("应用配置文件不存在或有错误。");
					return;
				}
				TaskInfo taskInfo = FPSerializer.Load<TaskInfo>(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\task.config");
				if (taskInfo.key == "")
				{
					taskInfo.key = Guid.NewGuid().ToString();
				}
				SqlParam sqlParam = DbHelper.MakeAndWhere("key", taskInfo.key);
				TaskInfo taskInfo2 = DbHelper.ExecuteModel<TaskInfo>(new SqlParam[]
				{
					sqlParam
				});
				if (taskInfo2.id > 0)
				{
					taskInfo2.type = taskInfo.type;
					taskInfo2.name = taskInfo.name;
					DbHelper.ExecuteUpdate<TaskInfo>(taskInfo2);
				}
				else
				{
					DbHelper.ExecuteInsert<TaskInfo>(taskInfo);
				}
				foreach (FileInfo fileInfo in new DirectoryInfo(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName)).GetFiles())
				{
					if (fileInfo.Extension == ".dll")
					{
						fileInfo.CopyTo(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + fileInfo.Name), true);
					}
				}
				base.Response.Redirect("taskmanage.aspx");
			}
		}
	}
}
