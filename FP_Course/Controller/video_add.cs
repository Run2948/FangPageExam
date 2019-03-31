using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Course.Model;
using System;
using System.Web;

namespace FP_Course.Controller
{
	public class video_add : AdminController
	{
		protected int id = FPRequest.GetInt("id");

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected int chapterid = FPRequest.GetInt("chapterid");

		protected ChapterInfo chapterinfo = new ChapterInfo();

		protected int courseid = FPRequest.GetInt("courseid");

		protected CourseInfo courseinfo = new CourseInfo();

		protected VideoInfo videoinfo = new VideoInfo();

		protected string coureurl = "";

		protected override void Controller()
		{
			bool flag = this.id > 0;
			if (flag)
			{
				this.videoinfo = DbHelper.ExecuteModel<VideoInfo>(this.id);
				bool flag2 = this.videoinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("该视频已被删除或不存在。");
					return;
				}
				this.chapterid = this.videoinfo.chapterid;
			}
			this.chapterinfo = DbHelper.ExecuteModel<ChapterInfo>(this.chapterid);
			bool flag3 = this.chapterinfo.id == 0;
			if (flag3)
			{
				this.ShowErr("该章节已被删除或不存在。");
			}
			else
			{
				this.courseid = this.chapterinfo.courseid;
				this.courseinfo = DbHelper.ExecuteModel<CourseInfo>(this.courseid);
				bool flag4 = this.courseinfo.id == 0;
				if (flag4)
				{
					this.ShowErr("该课程已被删除或不存在。");
				}
				else
				{
					this.sortid = this.courseinfo.sortid;
					this.sortinfo = SortBll.GetSortInfo(this.sortid);
					this.coureurl = "course_manage.aspx?sortid=" + this.sortid;
					bool flag5 = this.videoinfo.attachid == "";
					if (flag5)
					{
						this.videoinfo.attachid = FPRandom.CreateCode(20);
					}
					bool ispost = this.ispost;
					if (ispost)
					{
						this.videoinfo = FPRequest.GetModel<VideoInfo>(this.videoinfo);
						bool isfile = this.isfile;
						if (isfile)
						{
							HttpPostedFile files = FPRequest.Files["uploadfile"];
							AttachInfo attachInfo = AttachBll.Upload(files, this.videoinfo.attachid, this.userid, this.setupinfo.markup);
							bool flag6 = attachInfo.id > 0;
							if (flag6)
							{
								this.videoinfo.videofile = attachInfo.filename;
							}
						}
						bool flag7 = this.videoinfo.id > 0;
						if (flag7)
						{
							DbHelper.ExecuteUpdate<VideoInfo>(this.videoinfo);
							base.AddMsg("更新成功!");
						}
						else
						{
							SqlParam[] sqlparams = new SqlParam[]
							{
								DbHelper.MakeAndWhere("courseid", this.courseid),
								DbHelper.MakeAndWhere("chapterid", this.chapterid)
							};
							this.videoinfo.display = DbHelper.ExecuteCount<VideoInfo>(sqlparams) + 1;
							this.videoinfo.courseid = this.courseid;
							this.videoinfo.chapterid = this.chapterid;
							DbHelper.ExecuteInsert<VideoInfo>(this.videoinfo);
							base.AddMsg("添加成功!");
						}
						base.Response.Redirect("video_manage.aspx?courseid=" + this.courseid);
					}
				}
			}
		}
	}
}
