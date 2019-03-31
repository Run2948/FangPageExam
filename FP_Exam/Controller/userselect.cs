using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class userselect : AdminController
	{
		protected string zNodes = "";

		protected int tab = FPRequest.GetInt("tab");

		protected string examuser = FPRequest.GetString("examuser");

		protected override void Controller()
		{
			this.zNodes = this.GetDepartTree(0);
		}

		private string GetDepartTree(int parentid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			List<Department> list = DbHelper.ExecuteList<Department>(sqlparams);
			string text = "";
			foreach (Department current in list)
			{
				string text2 = string.Format("usersearch.aspx?tab={0}&examuser={1}&departid={2}", this.tab, this.examuser, current.id);
				bool flag = text != "";
				if (flag)
				{
					text += ",";
				}
				bool flag2 = current.subcounts > 0;
				if (flag2)
				{
					text = string.Concat(new object[]
					{
						text,
						"{ id: ",
						current.id,
						", pId: ",
						parentid,
						", name: \"",
						current.name,
						"\",open:true, url: \"",
						text2,
						"\", target: \"frmmaindetail\", icon: \"",
						this.adminpath,
						"statics/images/usergroups.gif\" }"
					});
					text = text + "," + this.GetDepartTree(current.id);
				}
				else
				{
					text = string.Concat(new object[]
					{
						text,
						"{ id: ",
						current.id,
						", pId: ",
						parentid,
						", name: \"",
						current.name,
						"\",open:true, url: \"",
						text2,
						"\", target: \"frmmaindetail\", icon: \"",
						this.adminpath,
						"statics/images/users.gif\" }"
					});
				}
			}
			return text;
		}
	}
}
