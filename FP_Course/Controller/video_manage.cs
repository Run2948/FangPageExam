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
	public class video_manage : AdminController
	{
		protected int courseid = FPRequest.GetInt("courseid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected CourseInfo courseinfo = new CourseInfo();

		protected List<ChapterInfo> chapterlist = new List<ChapterInfo>();

		protected override void Controller()
		{
			this.courseinfo = DbHelper.ExecuteModel<CourseInfo>(this.courseid);
			bool flag = this.courseinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该课程不存在或已被删除。");
			}
			else
			{
				this.sortid = this.courseinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool ispost = this.ispost;
				if (ispost)
				{
					bool flag2 = this.action == "delete";
					if (flag2)
					{
						int @int = FPRequest.GetInt("chapterid");
						DbHelper.ExecuteDelete<ChapterInfo>(@int);
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeAndWhere("chapterid", @int),
							DbHelper.MakeAndWhere("chapterid", @int)
						};
						DbHelper.ExecuteDelete<VideoInfo>(sqlparams);
					}
					else
					{
						bool flag3 = this.action == "display";
						if (flag3)
						{
							int int2 = FPRequest.GetInt("chapterid");
							List<VideoInfo> videoList = this.GetVideoList(this.courseid, int2);
							string text = "";
							foreach (VideoInfo current in videoList)
							{
								int int3 = FPRequest.GetInt("display_" + current.id);
								bool flag4 = text != "";
								if (flag4)
								{
									text += "|";
								}
								text += string.Format("UPDATE [{0}Train_VideoInfo] SET [display]={1} WHERE [id]={2}", DbConfigs.Prefix, int3, current.id);
							}
							DbHelper.ExecuteSql(text);
						}
						else
						{
							bool flag5 = this.action == "delete_video";
							if (flag5)
							{
								int int4 = FPRequest.GetInt("videoid");
								DbHelper.ExecuteDelete<VideoInfo>(int4);
							}
						}
					}
				}
				SqlParam[] sqlparams2 = new SqlParam[]
				{
					DbHelper.MakeAndWhere("courseid", this.courseid),
					DbHelper.MakeOrderBy("display", OrderBy.ASC)
				};
				this.chapterlist = DbHelper.ExecuteList<ChapterInfo>(sqlparams2);
			}
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
