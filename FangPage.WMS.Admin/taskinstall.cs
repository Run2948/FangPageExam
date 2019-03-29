using System;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Task;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000F RID: 15
	public class taskinstall : SuperController
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00003C60 File Offset: 0x00001E60
		protected override void View()
		{
			if (this.ispost)
			{
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".task")
				{
					this.ShowErr("对不起，该文件不是方配系统任务安装文件类型。");
				}
				else
				{
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
					FPZip.UnZipFile(mapPath + "\\" + fileName, "");
					File.Delete(mapPath + "\\" + fileName);
					if (!File.Exists(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "\\task.config"))
					{
						Directory.Delete(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName), true);
						this.ShowErr("应用配置文件不存在或有错误。");
					}
					else
					{
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
						DirectoryInfo directoryInfo = new DirectoryInfo(mapPath + "\\" + Path.GetFileNameWithoutExtension(fileName));
						foreach (FileInfo fileInfo in directoryInfo.GetFiles())
						{
							if (fileInfo.Extension == ".dll")
							{
								fileInfo.CopyTo(FPUtils.GetMapPath(WebConfig.WebPath + "bin/" + fileInfo.Name), true);
							}
						}
						base.Response.Redirect("taskmanage.aspx");
					}
				}
			}
		}
	}
}
