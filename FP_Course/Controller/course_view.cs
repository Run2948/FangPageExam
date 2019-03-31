using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Course.Model;
using System;
using System.Collections.Generic;

namespace FP_Course.Controller
{
	public class course_view : LoginController
	{
		protected int id = FPRequest.GetInt("id");

		protected CourseInfo courseinfo = new CourseInfo();

		protected List<ChapterInfo> chapterlist = new List<ChapterInfo>();

		protected override void Controller()
		{
			this.courseinfo = DbHelper.ExecuteModel<CourseInfo>(this.id);
			bool flag = this.courseinfo.id == 0;
			if (flag)
			{
				this.ShowErr("该课程已不存在");
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("courseid", this.id),
				DbHelper.MakeSet("uid", this.userid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.chapterlist = DbHelper.ExecuteList<ChapterInfo>(sqlparams);
		}

		protected List<VideoInfo> GetVideoList(int courseid, int chapterid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("courseid", courseid),
				DbHelper.MakeAndWhere("chapterid", chapterid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			return DbHelper.ExecuteList<VideoInfo>(sqlparams);
		}
	}
}
