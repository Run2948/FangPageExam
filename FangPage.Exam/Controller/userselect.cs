using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x0200001E RID: 30
	public class userselect : AdminController
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		protected override void View()
		{
			this.zNodes = this.GetDepartTree(0);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		private string GetDepartTree(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<Department> list = DbHelper.ExecuteList<Department>(orderby, new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			foreach (Department department in list)
			{
				string text2 = string.Format("usersearch.aspx?tab={0}&examuser={1}&departid={2}", this.tab, this.examuser, department.id);
				if (text != "")
				{
					text += ",";
				}
				if (department.subcounts > 0)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"{ id: ",
						department.id,
						", pId: ",
						parentid,
						", name: \"",
						department.name,
						"\",open:true, url: \"",
						text2,
						"\", target: \"frmmaindetail\", icon: \"",
						this.webpath,
						(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
						"images/usergroups.gif\" }"
					});
					text = text + "," + this.GetDepartTree(department.id);
				}
				else
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"{ id: ",
						department.id,
						", pId: ",
						parentid,
						", name: \"",
						department.name,
						"\",open:true, url: \"",
						text2,
						"\", target: \"frmmaindetail\", icon: \"",
						this.webpath,
						(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
						"images/users.gif\" }"
					});
				}
			}
			return text;
		}

		// Token: 0x0400008D RID: 141
		protected string zNodes = "";

		// Token: 0x0400008E RID: 142
		protected int tab = FPRequest.GetInt("tab");

		// Token: 0x0400008F RID: 143
		protected string examuser = FPRequest.GetString("examuser");
	}
}
