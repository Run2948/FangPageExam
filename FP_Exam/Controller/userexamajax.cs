using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class userexamajax : AdminController
	{
		protected string examuser = FPRequest.GetString("examuser");

		protected override void Controller()
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, this.examuser);
			List<UserInfo> list = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			string text2 = "";
			int[] array = FPArray.SplitInt(this.examuser);
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				foreach (UserInfo current in list)
				{
					bool flag = num == current.id && FPArray.InArray(num, text2) == -1;
					if (flag)
					{
						bool flag2 = text != "";
						if (flag2)
						{
							text += ",";
						}
						bool flag3 = current.realname != "";
						if (flag3)
						{
							text += current.realname;
						}
						else
						{
							text += current.username;
						}
						bool flag4 = text2 != "";
						if (flag4)
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
			FPResponse.WriteJson(hashtable);
		}
	}
}
