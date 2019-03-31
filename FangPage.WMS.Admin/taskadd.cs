using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000023 RID: 35
	public class taskadd : SuperController
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000072D8 File Offset: 0x000054D8
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.taskinfo = DbHelper.ExecuteModel<TaskInfo>(this.id);
			}
			if (this.ispost)
			{
				string @string = FPRequest.GetString("name");
				FPRequest.GetString("type");
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
				if (FPRequest.GetInt("timeofday") == 1)
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
				base.Response.Redirect("taskmanage.aspx");
			}
		}

		// Token: 0x0400004F RID: 79
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000050 RID: 80
		protected TaskInfo taskinfo = new TaskInfo();
	}
}
