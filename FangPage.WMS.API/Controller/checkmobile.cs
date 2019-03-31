using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x0200000A RID: 10
	public class checkmobile : APIController
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002688 File Offset: 0x00000888
		protected override void Controller()
		{
			string @string = FPRequest.GetString("mobile");
			if (@string == "")
			{
				base.WriteErr("手机号码不能为空。");
				return;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("mobile", @string);
			if (DbHelper.ExecuteCount<UserInfo>(new SqlParam[]
			{
				sqlParam
			}) == 0)
			{
				base.WriteSuccess("该手机号未被注册。");
				return;
			}
			base.WriteErr("对不起，该手机号已被注册。");
		}
	}
}
