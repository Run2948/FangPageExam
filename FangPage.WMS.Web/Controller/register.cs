using System;
using System.Collections.Generic;
using System.Text;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x0200000E RID: 14
	public class register : WebController
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000035B8 File Offset: 0x000017B8
		protected override void Controller()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.userid > 0)
			{
				this.ShowErr("对不起，系统不允许重复注册用户。");
				return;
			}
			this.departlist = DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == 0);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003614 File Offset: 0x00001814
		protected string GetChildDepartment(int parentid, string tree)
		{
			List<Department> list = DepartmentBll.GetDepartList().FindAll((Department item) => item.parentid == parentid);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in list)
			{
				stringBuilder.Append("  <option value=\"" + department.id + "\"");
				stringBuilder.AppendFormat(">{0}</option>", tree + department.name);
				stringBuilder.AppendLine(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000004 RID: 4
		protected RegConfig regconfig = new RegConfig();

		// Token: 0x04000005 RID: 5
		protected List<Department> departlist = new List<Department>();
	}
}
