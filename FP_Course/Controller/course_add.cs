using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Course.Model;
using System;
using System.Collections.Generic;

namespace FP_Course.Controller
{
	public class course_add : AdminController
	{
		protected int id = FPRequest.GetInt("id");

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected CourseInfo courseinfo = new CourseInfo();

		protected List<TypeInfo> typelist = new List<TypeInfo>();

		protected override void Controller()
		{
			bool flag = this.id > 0;
			if (flag)
			{
				this.courseinfo = DbHelper.ExecuteModel<CourseInfo>(this.id);
				bool flag2 = this.courseinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该文章不存在或已被删除。");
					return;
				}
				this.sortid = this.courseinfo.sortid;
			}
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool flag3 = this.sortinfo.id == 0;
			if (flag3)
			{
				this.ShowErr("对不起，该栏目不存在或已被删除。");
			}
			else
			{
				bool flag4 = this.courseinfo.attachid == "";
				if (flag4)
				{
					this.courseinfo.attachid = FPRandom.CreateCode(20);
				}
				bool ispost = this.ispost;
				if (ispost)
				{
					this.courseinfo.typelist = "";
					this.courseinfo = FPRequest.GetModel<CourseInfo>(this.courseinfo);
					bool flag5 = this.courseinfo.id > 0;
					if (flag5)
					{
						DbHelper.ExecuteUpdate<CourseInfo>(this.courseinfo);
						base.AddMsg("更新成功！");
					}
					else
					{
						this.courseinfo.uid = this.userid;
						this.courseinfo.channelid = this.sortinfo.channelid;
						this.courseinfo.id = DbHelper.ExecuteInsert<CourseInfo>(this.courseinfo);
						base.AddMsg("添加成功！");
					}
				}
				this.typelist = TypeBll.GetTypeList(this.sortinfo.types);
			}
		}
	}
}
