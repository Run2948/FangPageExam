using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class usersearch : AdminController
	{
		protected new int departid = FPRequest.GetInt("departid");

		protected string keyword = FPRequest.GetString("keyword");

		protected List<UserInfo> userlist = new List<UserInfo>();

		protected override void Controller()
		{
			List<SqlParam> list = new List<SqlParam>();
			bool flag = this.departid > 0;
			if (flag)
			{
				list.Add(DbHelper.MakeAndWhere("departid", WhereType.In, this.GetChildDepartid(this.departid)));
			}
			bool flag2 = this.keyword != "";
			if (flag2)
			{
				list.Add(DbHelper.MakeAndWhere("username", WhereType.Like, this.keyword));
				list.Add(DbHelper.MakeOrWhere("realname", WhereType.Like, this.keyword));
			}
			bool flag3 = list.Count > 0;
			if (flag3)
			{
				this.userlist = DbHelper.ExecuteList<UserInfo>(list.ToArray());
			}
		}

		protected string GetChildDepartid(int parentid)
		{
			List<Department> departList = DepartmentBll.GetDepartList(parentid);
			string text = parentid.ToString();
			foreach (Department current in departList)
			{
				text = text + "," + this.GetChildDepartid(current.id);
			}
			return text;
		}
	}
}
