using System;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x0200000C RID: 12
	public class login : WebController
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000031D8 File Offset: 0x000013D8
		protected override void Controller()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.backurl == "")
			{
				this.backurl = "index.aspx";
			}
			if (this.userid > 0)
			{
				base.Response.Redirect(this.backurl);
				return;
			}
			if (this.ispost)
			{
				string @string = FPRequest.GetString("username");
				string string2 = FPRequest.GetString("password");
				if (@string == "")
				{
					this.ShowErr("帐号不能为空");
					return;
				}
				if (string2 == "")
				{
					this.ShowErr("密码不能为空");
					return;
				}
				if (this.isseccode)
				{
					if (FPRequest.GetString("verify").Equals(""))
					{
						this.ShowErr("验证码不能为空");
						return;
					}
					if (!this.isvalid)
					{
						this.ShowErr("验证码错误");
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
						this.ShowErr(this.msg);
						return;
					}
				}
				if (userInfo.id <= 0)
				{
					SysBll.InsertLog(userInfo.id, "用户登录", "登录失败，登录名：" + @string + ",密码：" + string2, false);
					this.ShowErr("用户名或密码错误。");
					return;
				}
				if (userInfo.roleid == 4)
				{
					this.ShowErr("对不起，该帐户已被禁止登录");
					return;
				}
				if (userInfo.roleid == 3)
				{
					if (this.regconfig.regverify == 1)
					{
						this.ShowErr("您需要等待一些时间, 待系统管理员审核您的帐户后才可登录使用");
						return;
					}
					if (this.regconfig.regverify == 2)
					{
						this.ShowErr("请您到您的邮箱中点击激活链接来激活您的帐号");
						return;
					}
					this.ShowErr("抱歉, 您的用户身份尚未得到验证");
					return;
				}
				else
				{
					if (userInfo.state == 0)
					{
						this.ShowErr("抱歉, 您的帐号已被禁止使用。");
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
					sessionInfo.timeout = this.sysconfig.onlinetimeout;
					sessionInfo.token = FPUtils.MD5(sessionInfo.sid + sessionInfo.username + sessionInfo.password);
					sessionInfo.port = this.port;
					SessionBll.CreateUserLogin(sessionInfo, this.sysconfig.loginonce);
					FPSession.Insert("FP_OLUSERINFO", userInfo);
					FPSession.Remove("FP_PERMISSION");
					WMSUtils.WriteUserCookie(this.platform, this.port, sessionInfo.token, -1);
					SysBll.InsertLog(userInfo.id, "用户登录", "登录成功，登录名：" + userInfo.username, true);
					base.AddMsg("登录成功, 返回登录前页面");
					base.SetMetaRefresh(2, this.backurl);
					if (!this.iscuserr)
					{
						base.Response.Redirect(this.backurl);
						return;
					}
				}
			}
		}

		// Token: 0x04000003 RID: 3
		protected RegConfig regconfig = new RegConfig();
	}
}
