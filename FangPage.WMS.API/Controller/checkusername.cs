using System;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x0200000B RID: 11
	public class checkusername : APIController
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000026F0 File Offset: 0x000008F0
		protected override void Controller()
		{
			RegConfig regConfig = RegConfigs.GetRegConfig();
			string @string = FPRequest.GetString("username");
			if (@string == "")
			{
				base.WriteErr("帐号名不能为空。");
				return;
			}
			if (this.isseccode && !this.isvalid)
			{
				base.WriteErr("验证码错误。");
				return;
			}
			if (!FPUtils.IsSafeSqlString(@string))
			{
				base.WriteErr("您使用的用户名有敏感字符");
				return;
			}
			if (this.InRestrictArray(@string, regConfig.restrict))
			{
				base.WriteErr("该用户名不允许使用");
				return;
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("username", @string);
			if (DbHelper.ExecuteCount<UserInfo>(new SqlParam[]
			{
				sqlParam
			}) == 0)
			{
				base.WriteSuccess("该帐号未被注册。");
				return;
			}
			base.WriteErr("对不起，该帐号已被注册。");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027A8 File Offset: 0x000009A8
		private bool InRestrictArray(string usernametxt, string restrict)
		{
			if (restrict == null || restrict == "")
			{
				return false;
			}
			restrict = Regex.Escape(restrict).Replace("\\*", "[\\w-]*");
			foreach (string text in FPArray.SplitString(restrict.ToLower(), ","))
			{
				if (new Regex(string.Format("^{0}$", text)).IsMatch(usernametxt) && !text.Trim().Equals(""))
				{
					return true;
				}
			}
			return false;
		}
	}
}
