using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x02000009 RID: 9
	public class checkemail : APIController
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002620 File Offset: 0x00000820
		protected override void Controller()
		{
			string @string = FPRequest.GetString("email");
			if (@string == "")
			{
				base.WriteErr("邮箱号码不能为空");
				return;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("email", @string);
			if (DbHelper.ExecuteCount<UserInfo>(new SqlParam[]
			{
				sqlParam
			}) == 0)
			{
				base.WriteSuccess("该Email未被注册");
				return;
			}
			base.WriteErr("对不起，该Email已被注册");
		}
	}
}
