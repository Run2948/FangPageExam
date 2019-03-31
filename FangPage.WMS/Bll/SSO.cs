using System;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000030 RID: 48
	public class SSO
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000084D7 File Offset: 0x000066D7
		public static void ReSetConfig()
		{
			FPCache.Remove("FP_SSOCONFIG");
			SSO.ssoconfig = SSOConfigs.GetSSOConfig();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000084F0 File Offset: 0x000066F0
		public static UserInfo CheckLogin(UserInfo user, string username, string password, out string errmsg)
		{
			UserInfo userInfo = new UserInfo();
			errmsg = "";
			try
			{
				FPHttp fphttp = new FPHttp();
				FPData fpdata = new FPData();
				fpdata.Add("secret", SSO.ssoconfig.secret);
				fpdata.Add("username", username);
				fpdata.Add("password", password);
				userInfo = FPJson.ToModel<UserInfo>(fphttp.Post(SSO.ssoconfig.server + "/" + SSO.ssoconfig.oauth, fpdata));
			}
			catch (Exception)
			{
				errmsg = "无法连接单点登录服务器";
			}
			if (userInfo.id > 0)
			{
				if (user.id > 0)
				{
					user.password = userInfo.password;
					user.password2 = userInfo.password2;
					user.realname = userInfo.realname;
					user.nickname = userInfo.nickname;
					user.avatar = userInfo.avatar;
					DbHelper.ExecuteUpdate<UserInfo>(user);
				}
				else
				{
					if (RoleBll.CheckRole(userInfo.rolename).id == 0)
					{
						user.roleid = DbHelper.ExecuteInsert<RoleInfo>(new RoleInfo
						{
							name = userInfo.rolename
						});
					}
					user.password = userInfo.password;
					user.password2 = userInfo.password2;
					user.realname = userInfo.realname;
					user.nickname = userInfo.nickname;
					user.avatar = userInfo.avatar;
					DbHelper.ExecuteInsert<UserInfo>(user);
				}
			}
			else
			{
				user.id = 0;
				errmsg = "用户名或密码错误。";
			}
			return user;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000866C File Offset: 0x0000686C
		public static void Logout(string platform, string token)
		{
			try
			{
				FPHttp fphttp = new FPHttp();
				FPData fpdata = new FPData();
				fpdata.Add("secret", SSO.ssoconfig.secret);
				fpdata.Add("token", token);
				fphttp.Post(SSO.ssoconfig.server + "/" + SSO.ssoconfig.logout, fpdata);
			}
			catch
			{
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000086E0 File Offset: 0x000068E0
		public static UserInfo GetUserInfo(string username)
		{
			UserInfo result = new UserInfo();
			try
			{
				FPHttp fphttp = new FPHttp();
				FPData fpdata = new FPData();
				fpdata.Add("secret", SSO.ssoconfig.secret);
				fpdata.Add("username", username);
				result = FPJson.ToModel<UserInfo>(fphttp.Post(SSO.ssoconfig.server + "/" + SSO.ssoconfig.getuserinfo, fpdata));
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00008760 File Offset: 0x00006960
		public static UserInfo GetFullUserInfo(string username)
		{
			UserInfo result = new UserInfo();
			try
			{
				FPHttp fphttp = new FPHttp();
				FPData fpdata = new FPData();
				fpdata.Add("secret", SSO.ssoconfig.secret);
				fpdata.Add("type", "full");
				fpdata.Add("username", username);
				result = FPJson.ToModel<UserInfo>(fphttp.Post(SSO.ssoconfig.server + "/" + SSO.ssoconfig.getuserinfo, fpdata));
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000087F0 File Offset: 0x000069F0
		public static void AddUser(UserInfo userinfo)
		{
			try
			{
				new FPHttp().PostModel<UserInfo>(SSO.ssoconfig.server + "/" + SSO.ssoconfig.adduser, userinfo);
			}
			catch
			{
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000883C File Offset: 0x00006A3C
		public static void UpdateUser(FPData textParams)
		{
			try
			{
				new FPHttp().Post(SSO.ssoconfig.server + "/" + SSO.ssoconfig.adduser, textParams);
			}
			catch
			{
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00008888 File Offset: 0x00006A88
		public static void UpdatePassword(string username, string newpwd)
		{
			new UserInfo();
			try
			{
				FPHttp fphttp = new FPHttp();
				FPData fpdata = new FPData();
				fpdata.Add("secret", SSO.ssoconfig.secret);
				fpdata.Add("username", username);
				fpdata.Add("newpwd", newpwd);
				fphttp.Post(SSO.ssoconfig.server + "/" + SSO.ssoconfig.updatepwd, fpdata);
			}
			catch
			{
			}
		}

		// Token: 0x040001A8 RID: 424
		private static SSOConfig ssoconfig = SSOConfigs.GetSSOConfig();
	}
}
