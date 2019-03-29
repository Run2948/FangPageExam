using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000050 RID: 80
	public class getpass : WMSController
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000CBF0 File Offset: 0x0000ADF0
		protected override void View()
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
				}
				else if (this.ispost)
				{
					string @string = FPRequest.GetString("password");
					string string2 = FPRequest.GetString("repeat");
					if (@string != string2)
					{
						this.ShowErr("两次输入密码不一致。");
					}
					else
					{
						DbHelper.ExecuteUpdate<UserInfo>(new List<SqlParam>
						{
							DbHelper.MakeSet("password", FPUtils.MD5(@string)),
							DbHelper.MakeSet("authflag", 0),
							DbHelper.MakeSet("authstr", ""),
							DbHelper.MakeAndWhere("id", userInfo.id)
						}.ToArray());
						base.AddMsg("密码更改成功");
					}
				}
			}
			else
			{
				this.ShowErr("您当前的修改密码链接已无效。");
			}
		}

		// Token: 0x04000153 RID: 339
		private string authstr = FPRequest.GetString("authstr");
	}
}
