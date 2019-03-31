using System;
using System.Collections;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using LitJson;

namespace FP_Exam.Controller
{
	// Token: 0x02000015 RID: 21
	public class userexamajax : AdminController
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000B3BC File Offset: 0x000095BC
		protected override void View()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, this.examuser);
			List<UserInfo> list = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			string text2 = "";
			foreach (int num in FPUtils.SplitInt(this.examuser))
			{
				foreach (UserInfo userInfo in list)
				{
					if (num == userInfo.id && !FPUtils.InArray(num, text2))
					{
						if (text != "")
						{
							text += ",";
						}
						if (userInfo.realname != "")
						{
							text += userInfo.realname;
						}
						else
						{
							text += userInfo.username;
						}
						if (text2 != "")
						{
							text2 += ",";
						}
						text2 += num;
					}
				}
			}
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 0;
			hashtable["uname"] = text;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x0400006B RID: 107
		protected string examuser = FPRequest.GetString("examuser");
	}
}
