using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;
using System;

namespace FP_Exam.Wap.Controller
{
	public class examlist : LoginController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected override void Controller()
		{
			bool flag = this.channelid == 0 && this.args.Length > 1;
			if (flag)
			{
				this.channelid = FPUtils.StrToInt(this.args[1]);
			}
		}
	}
}
