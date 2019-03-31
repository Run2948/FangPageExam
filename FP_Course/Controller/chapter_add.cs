using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Course.Model;
using System;

namespace FP_Course.Controller
{
	public class chapter_add : AdminController
	{
		protected int id = FPRequest.GetInt("id");

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected int courseid = FPRequest.GetInt("courseid");

		protected CourseInfo courseinfo = new CourseInfo();

		protected ChapterInfo chapterinfo = new ChapterInfo();

		protected override void Controller()
		{
			bool flag = this.id > 0;
			if (flag)
			{
				this.chapterinfo = DbHelper.ExecuteModel<ChapterInfo>(this.id);
				bool flag2 = this.chapterinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("该章节已被删除或不存在。");
					return;
				}
				this.courseid = this.chapterinfo.courseid;
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("courseid", this.courseid);
				int num = DbHelper.ExecuteCount<ChapterInfo>(new SqlParam[]
				{
					sqlParam
				});
				this.chapterinfo.display = num + 1;
			}
			this.courseinfo = DbHelper.ExecuteModel<CourseInfo>(this.courseid);
			bool flag3 = this.courseinfo.id == 0;
			if (flag3)
			{
				this.ShowErr("该课程已被删除或不存在。");
			}
			else
			{
				this.sortid = this.courseinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool ispost = this.ispost;
				if (ispost)
				{
					this.chapterinfo = FPRequest.GetModel<ChapterInfo>(this.chapterinfo);
					bool flag4 = this.chapterinfo.id > 0;
					if (flag4)
					{
						DbHelper.ExecuteUpdate<ChapterInfo>(this.chapterinfo);
						base.AddMsg("更新成功!");
					}
					else
					{
						DbHelper.ExecuteInsert<ChapterInfo>(this.chapterinfo);
						base.AddMsg("添加成功!");
					}
					base.Response.Redirect("video_manage.aspx?courseid=" + this.courseid);
				}
			}
		}
	}
}
