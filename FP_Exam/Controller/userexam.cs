using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200001B RID: 27
	public class userexam : AdminController
	{
		// Token: 0x06000082 RID: 130 RVA: 0x0000D130 File Offset: 0x0000B330
		protected override void View()
		{
			if (this.ispost)
			{
				int @int = FPRequest.GetInt("uid");
				int[] array = FPUtils.SplitInt(this.examuser);
				this.examuser = "";
				foreach (int num in array)
				{
					if (num != @int)
					{
						if (this.examuser != "")
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
			if (this.keyword != "")
			{
				list.Add(DbHelper.MakeAndWhere("username", WhereType.Like, this.keyword));
				list.Add(DbHelper.MakeOrWhere("realname", WhereType.Like, this.keyword));
			}
			this.userlist = DbHelper.ExecuteList<UserInfo>(list.ToArray());
		}

		// Token: 0x04000086 RID: 134
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x04000087 RID: 135
		protected List<UserInfo> userlist = new List<UserInfo>();

		// Token: 0x04000088 RID: 136
		protected string examuser = FPRequest.GetString("examuser");
	}
}
