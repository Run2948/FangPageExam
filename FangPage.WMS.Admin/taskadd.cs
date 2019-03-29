using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Task;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001C RID: 28
	public class taskadd : SuperController
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00006368 File Offset: 0x00004568
		protected override void View()
		{
			if (this.id > 0)
			{
				this.taskinfo = DbHelper.ExecuteModel<TaskInfo>(this.id);
			}
			if (this.ispost)
			{
				string @string = FPRequest.GetString("name");
				string string2 = FPRequest.GetString("type");
				if (string.IsNullOrEmpty(@string))
				{
					this.ShowErr("计划任务名称不能为空!");
					return;
				}
				this.taskinfo = FPRequest.GetModel<TaskInfo>(this.taskinfo);
				if (this.taskinfo.key == "")
				{
					this.taskinfo.key = Guid.NewGuid().ToString();
				}
				int @int = FPRequest.GetInt("timeofday");
				if (@int == 1)
				{
					this.taskinfo.timeofday = FPRequest.GetInt("hour") * 60 + FPRequest.GetInt("minute");
				}
				else
				{
					this.taskinfo.timeofday = -1;
					this.taskinfo.minutes = FPRequest.GetInt("timeserval");
				}
				if (this.taskinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<TaskInfo>(this.taskinfo);
				}
				else
				{
					DbHelper.ExecuteInsert<TaskInfo>(this.taskinfo);
				}
				TaskManager.Reset();
				base.Response.Redirect("taskmanage.aspx");
			}
			base.SaveRightURL();
		}

		// Token: 0x04000036 RID: 54
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000037 RID: 55
		protected TaskInfo taskinfo = new TaskInfo();
	}
}
