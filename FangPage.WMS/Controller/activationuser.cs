using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000049 RID: 73
	public class activationuser : WMSController
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		protected override void View()
		{
			if (this.authstr != "")
			{
				List<SqlParam> list = new List<SqlParam>();
				list.Add(DbHelper.MakeAndWhere("authstr", this.authstr));
				list.Add(DbHelper.MakeAndWhere("authflag", 1));
				if (DbConfigs.DbType == DbType.Access)
				{
					list.Add(DbHelper.MakeAndWhere("DATEDIFF(\"d\",[authtime],NOW())<=3", WhereType.Custom, ""));
				}
				else
				{
					list.Add(DbHelper.MakeAndWhere("DateDiff(d,[authtime],getdate())<=3", WhereType.Custom, ""));
				}
				UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(list.ToArray());
				if (userInfo.id == 0)
				{
					this.ShowErr("该用户验证码过期或不存在。");
				}
				else
				{
					if (userInfo.roleid == 3)
					{
						userInfo.roleid = 5;
					}
					string sqlstring = string.Format("UPDATE [{0}WMS_UserInfo] SET  [isemail]=1,[roleid]={1},[authstr]='',[authflag]=0,[authtime]='{2}' WHERE [id]={3}", new object[]
					{
						DbConfigs.Prefix,
						userInfo.roleid,
						DbUtils.GetDateTime(),
						userInfo.id
					});
					string text = DbHelper.ExecuteSql(sqlstring);
					if (text != "")
					{
						this.ShowErr(text);
					}
					else
					{
						MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_registed");
						msgTemplate.content = msgTemplate.content.Replace("【用户名】", userInfo.username).Replace("【邮箱帐号】", userInfo.email);
						Email.Send(userInfo.email, msgTemplate.name, msgTemplate.content);
						base.ResetUser();
						base.AddMsg("您当前的帐号已经激活，请返回登录页进行登录。");
					}
				}
			}
			else
			{
				this.ShowErr("您当前的激活链接已无效。");
			}
		}

		// Token: 0x0400014D RID: 333
		private string authstr = FPRequest.GetString("authstr");
	}
}
