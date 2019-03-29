using System;
using System.Collections;
using System.Text.RegularExpressions;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS.Tools
{
	// Token: 0x02000045 RID: 69
	public class userajax : WMSController
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000B0D8 File Offset: 0x000092D8
		protected override void View()
		{
			RegConfig regConfig = RegConfigs.GetRegConfig();
			string @string = FPRequest.GetString("username");
			if (@string == "")
			{
				this.ShowErrMsg("用户名不能为空");
			}
			else if (!FPUtils.IsSafeSqlString(@string))
			{
				this.ShowErrMsg("您使用的用户名有敏感字符");
			}
			else if (this.InRestrictArray(@string, regConfig.restrict))
			{
				this.ShowErrMsg("该用户名不允许使用");
			}
			else if (UserBll.CheckUserName(@string))
			{
				this.ShowErrMsg("该用户名已被使用");
			}
			else
			{
				Hashtable hashtable = new Hashtable();
				hashtable["error"] = 0;
				hashtable["message"] = "正确";
				base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
				base.Response.Write(JsonMapper.ToJson(hashtable));
				base.Response.End();
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000B1D4 File Offset: 0x000093D4
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["error"] = 1;
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000B23C File Offset: 0x0000943C
		private bool InRestrictArray(string usernametxt, string restrict)
		{
			bool result;
			if (restrict == null || restrict == "")
			{
				result = false;
			}
			else
			{
				restrict = Regex.Escape(restrict).Replace("\\*", "[\\w-]*");
				foreach (string text in FPUtils.SplitString(restrict.ToLower(), ","))
				{
					Regex regex = new Regex(string.Format("^{0}$", text));
					if (regex.IsMatch(usernametxt) && !text.Trim().Equals(""))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}
	}
}
