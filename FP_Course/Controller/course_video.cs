using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Course.Model;
using System;

namespace FP_Course.Controller
{
	public class course_video : LoginController
	{
		protected int videoid = FPRequest.GetInt("videoid");

		protected VideoInfo videoinfo = new VideoInfo();

		protected override void Controller()
		{
			this.videoinfo = DbHelper.ExecuteModel<VideoInfo>(this.videoid);
			bool flag = this.videoinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该课程文件不存在");
			}
			else
			{
				bool flag2 = this.videoinfo.videofile == "";
				if (flag2)
				{
					this.ShowErr("对不起，课程文件为空值。");
				}
			}
		}
	}
}
