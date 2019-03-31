using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x02000006 RID: 6
	public class activationuser : WebController
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000029FC File Offset: 0x00000BFC
		protected override void Controller()
		{
			if (!(this.authstr != ""))
			{
				this.ShowErr("您当前的激活链接已无效。");
				return;
			}
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
				return;
			}
			if (userInfo.roleid == 3)
			{
				userInfo.roleid = 5;
			}
			string text = DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_UserInfo] SET  [isemail]=1,[roleid]={1},[authstr]='',[authflag]=0,[authtime]='{2}' WHERE [id]={3}", new object[]
			{
				DbConfigs.Prefix,
				userInfo.roleid,
				DbUtils.GetDateTime(),
				userInfo.id
			}));
			if (text != "")
			{
				this.ShowErr(text);
				return;
			}
			MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_registed");
			if (msgTemplate.id > 0)
			{
				msgTemplate.content = msgTemplate.content.Replace("【用户名】", userInfo.username).Replace("【姓名】", userInfo.realname).Replace("【邮箱帐号】", userInfo.email);
				Email.Send(userInfo.email, msgTemplate.name, msgTemplate.content);
			}
			base.ResetUser();
			base.AddMsg("您当前的帐号已经激活，请返回登录页进行登录。");
		}

		// Token: 0x04000001 RID: 1
		private string authstr = FPRequest.GetString("authstr");
	}
}
