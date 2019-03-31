using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x0200000A RID: 10
	public class getpwd : WebController
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000304C File Offset: 0x0000124C
		protected override void Controller()
		{
			if (this.authstr != "")
			{
				List<SqlParam> list = new List<SqlParam>();
				list.Add(DbHelper.MakeAndWhere("authstr", this.authstr));
				list.Add(DbHelper.MakeAndWhere("authflag", 2));
				if (DbConfigs.DbType == DbType.Access)
				{
					list.Add(DbHelper.MakeAndWhere("DATEDIFF(\"m\",[authtime],NOW())<=30", WhereType.Custom, ""));
				}
				else
				{
					list.Add(DbHelper.MakeAndWhere("DateDiff(m,[authtime],getdate())<=30", WhereType.Custom, ""));
				}
				UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(list.ToArray());
				if (userInfo.id == 0)
				{
					this.ShowErr("用户验证码过期或不存在。");
					return;
				}
				if (this.ispost)
				{
					string @string = FPRequest.GetString("password");
					string string2 = FPRequest.GetString("repeat");
					if (@string != string2)
					{
						this.ShowErr("两次输入密码不一致。");
						return;
					}
					DbHelper.ExecuteUpdate<UserInfo>(new List<SqlParam>
					{
						DbHelper.MakeUpdate("password", FPUtils.MD5(@string)),
						DbHelper.MakeUpdate("authflag", 0),
						DbHelper.MakeUpdate("authstr", ""),
						DbHelper.MakeAndWhere("id", userInfo.id)
					}.ToArray());
					base.AddMsg("密码更改成功");
					return;
				}
			}
			else
			{
				this.ShowErr("您当前的修改密码链接已无效。");
			}
		}

		// Token: 0x04000002 RID: 2
		private string authstr = FPRequest.GetString("authstr");
	}
}
