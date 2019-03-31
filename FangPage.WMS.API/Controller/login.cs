using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x0200000C RID: 12
	public class login : APIController
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002830 File Offset: 0x00000A30
		protected override void Controller()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.ispost)
			{
				string @string = FPRequest.GetString("username");
				string string2 = FPRequest.GetString("password");
				int @int = FPRequest.GetInt("logintype");
				if (@string == "")
				{
					base.WriteErr("帐号不能为空");
					return;
				}
				if (string2 == "")
				{
					base.WriteErr("密码不能为空");
					return;
				}
				if (this.isseccode)
				{
					if (FPRequest.GetString("verify").Equals(""))
					{
						base.WriteErr("验证码不能为空");
						return;
					}
					if (!this.isvalid)
					{
						base.WriteErr("验证码错误");
						return;
					}
				}
				SessionInfo sessionInfo = new SessionInfo();
				UserInfo userInfo = new UserInfo();
				userInfo = UserBll.CheckLogin(@string, string2);
				if (this.sysconfig.ssocheck == 1 && (userInfo.issso == 1 || userInfo.id == 0))
				{
					SSO.CheckLogin(userInfo, @string, string2, out this.msg);
					if (this.msg != "")
					{
						base.WriteErr(this.msg);
						return;
					}
				}
				if (userInfo.id > 0)
				{
					if (userInfo.roleid == 4)
					{
						base.WriteErr("对不起，该帐户已被禁止登录");
						return;
					}
					if (userInfo.roleid == 3)
					{
						if (this.regconfig.regverify == 1)
						{
							base.WriteErr("您需要等待一些时间, 待系统管理员审核您的帐户后才可登录使用");
							return;
						}
						if (this.regconfig.regverify == 2)
						{
							base.WriteErr("请您到您的邮箱中点击激活链接来激活您的帐号");
							return;
						}
						base.WriteErr("抱歉, 您的用户身份尚未得到验证");
						return;
					}
					else
					{
						if (userInfo.state == 0)
						{
							base.WriteErr("抱歉, 您的帐号已被禁止使用。");
							return;
						}
						sessionInfo.sid = FPRandom.CreateCode(32);
						sessionInfo.uid = userInfo.id;
						sessionInfo.username = userInfo.username;
						sessionInfo.password = userInfo.password;
						sessionInfo.realname = userInfo.realname;
						sessionInfo.avatar = userInfo.avatar;
						sessionInfo.roleid = userInfo.roleid;
						sessionInfo.rolename = userInfo.rolename;
						sessionInfo.departid = userInfo.departid;
						sessionInfo.departname = userInfo.departname;
						sessionInfo.address = this.ip;
						sessionInfo.platform = this.platform;
						sessionInfo.invisible = FPRequest.GetInt("invisible");
						if (@int == 1)
						{
							sessionInfo.timeout = 8640;
						}
						else
						{
							sessionInfo.timeout = this.sysconfig.onlinetimeout;
						}
						sessionInfo.token = FPUtils.MD5(sessionInfo.sid + sessionInfo.username + sessionInfo.password + sessionInfo.address);
						sessionInfo.logincheck = this.sysconfig.ssocheck;
						sessionInfo.port = this.port;
						sessionInfo.errmsg = "ok";
						SessionBll.CreateUserLogin(sessionInfo, this.sysconfig.loginonce);
						FPSession.Insert("FP_OLUSERINFO", userInfo);
						FPSession.Remove("FP_PERMISSION");
						WMSUtils.WriteUserCookie(this.platform, this.port, sessionInfo.token, -1);
						SysBll.InsertLog(userInfo.id, "用户登录", "登录，登录名：" + userInfo.username, true);
						FPResponse.WriteJson(sessionInfo);
						return;
					}
				}
				else
				{
					SysBll.InsertLog(0, "用户登录", "登录失败，登录名：" + @string + ",密码：" + string2, false);
					base.WriteErr("用户名或密码错误。");
				}
			}
		}

		// Token: 0x04000004 RID: 4
		protected RegConfig regconfig = new RegConfig();
	}
}
