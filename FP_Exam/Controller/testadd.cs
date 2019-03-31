using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class testadd : AdminController
	{
		protected int sortid = FPRequest.GetInt("sortid");

		protected int id = FPRequest.GetInt("id");

		protected SortInfo sortinfo = new SortInfo();

		protected List<TypeInfo> typelist = new List<TypeInfo>();

		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		protected TestInfo testinfo = new TestInfo();

		protected override void Controller()
		{
			bool flag = this.id > 0;
			if (flag)
			{
				this.testinfo = DbHelper.ExecuteModel<TestInfo>(this.id);
				bool flag2 = this.testinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该试卷不存在或已被删除。");
					return;
				}
				this.sortid = this.testinfo.sortid;
			}
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool flag3 = this.sortinfo.id == 0;
			if (flag3)
			{
				this.ShowErr("对不起，该栏目不存在或已被删除。");
			}
			else
			{
				bool ispost = this.ispost;
				if (ispost)
				{
					this.testinfo = FPRequest.GetModel<TestInfo>(this.testinfo);
					this.testinfo.channelid = this.sortinfo.channelid;
					this.testinfo.sortid = this.sortid;
					this.testinfo.uid = this.userid;
					bool flag4 = this.testinfo.id > 0;
					if (flag4)
					{
						DbHelper.ExecuteUpdate<TestInfo>(this.testinfo);
					}
					else
					{
						DbHelper.ExecuteInsert<TestInfo>(this.testinfo);
					}
					FPResponse.Redirect("testmanage.aspx?sortid=" + this.sortid);
				}
				this.rolelist = DbHelper.ExecuteList<RoleInfo>();
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("id", WhereType.In, this.sortinfo.types),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				};
				this.typelist = DbHelper.ExecuteList<TypeInfo>(sqlparams);
			}
		}
	}
}
