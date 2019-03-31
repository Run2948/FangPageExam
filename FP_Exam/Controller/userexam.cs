using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class userexam : AdminController
	{
		protected string keyword = FPRequest.GetString("keyword");

		protected List<UserInfo> userlist = new List<UserInfo>();

		protected string examuser = FPRequest.GetString("examuser");

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				int @int = FPRequest.GetInt("uid");
				int[] array = FPArray.SplitInt(this.examuser);
				this.examuser = "";
				int[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					int num = array2[i];
					bool flag = num != @int;
					if (flag)
					{
						bool flag2 = this.examuser != "";
						if (flag2)
						{
							this.examuser += ",";
						}
						this.examuser += num;
					}
				}
				base.Response.Redirect(string.Concat(new string[]
				{
					this.pagename,
					"?examuser=",
					this.examuser,
					"&keyword=",
					this.keyword
				}));
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.examuser));
			bool flag3 = this.keyword != "";
			if (flag3)
			{
				list.Add(DbHelper.MakeAndWhere("username", WhereType.Like, this.keyword));
				list.Add(DbHelper.MakeOrWhere("realname", WhereType.Like, this.keyword));
			}
			this.userlist = DbHelper.ExecuteList<UserInfo>(list.ToArray());
		}
	}
}
