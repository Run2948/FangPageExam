using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class selectdepart : FPController
	{
		protected int parentid = FPRequest.GetInt("parentid");

		protected override void Controller()
		{
			this.GetDepartTree(this.parentid);
		}

		private void GetDepartTree(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<Department> list = DbHelper.ExecuteList<Department>(new SqlParam[]
			{
				sqlParam
			});
			List<Dictionary<string, object>> list2 = new List<Dictionary<string, object>>();
			foreach (Department current in list)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("id", current.id);
				dictionary.Add("pId", current.parentid);
				dictionary.Add("name", current.name);
				bool flag = current.subcounts > 0;
				if (flag)
				{
					dictionary.Add("icon", this.adminpath + "statics/images/usergroups.gif");
				}
				else
				{
					dictionary.Add("icon", this.adminpath + "statics/images/users.gif");
				}
				list2.Add(dictionary);
			}
			FPResponse.WriteJson(list2);
		}
	}
}
