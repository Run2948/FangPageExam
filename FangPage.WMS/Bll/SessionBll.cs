using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200002D RID: 45
	public class SessionBll
	{
		// Token: 0x06000352 RID: 850 RVA: 0x000076F4 File Offset: 0x000058F4
		public static SessionInfo GetUserSession(int uid, string platform)
		{
			return DbHelper.ExecuteModel<SessionInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("uid", uid),
				DbHelper.MakeAndWhere("state", 1),
				DbHelper.MakeAndWhere("platform", platform)
			});
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00007740 File Offset: 0x00005940
		public static SessionInfo GetSessionInfo(string token)
		{
			return DbHelper.ExecuteModel<SessionInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("token", token),
				DbHelper.MakeOrderBy("updatetime", OrderBy.DESC)
			});
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000776C File Offset: 0x0000596C
		public static SessionInfo GetSessionInfo(string platform, string token, int logincheck)
		{
			SessionInfo sessionInfo = SessionBll.GetCacheSession(platform, token);
			if (sessionInfo == null)
			{
				sessionInfo = DbHelper.ExecuteModel<SessionInfo>(new SqlParam[]
				{
					DbHelper.MakeAndWhere("platform", platform),
					DbHelper.MakeAndWhere("token", token)
				});
			}
			return sessionInfo;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000077B0 File Offset: 0x000059B0
		public static SessionInfo UpdateSession(string platform, string token, int logincheck, int timeout, int expires, int loginonce, string address, int port)
		{
			SessionInfo sessionInfo = new SessionInfo();
			if (!string.IsNullOrEmpty(token))
			{
				sessionInfo = SessionBll.GetSessionInfo(platform, token, 0);
				if (sessionInfo.id > 0)
				{
					sessionInfo.logincheck = logincheck;
					sessionInfo.port = port;
					SessionBll.InsertCacheSession(sessionInfo);
				}
				else
				{
					sessionInfo = SessionBll.GetSessionInfo(token);
					if (sessionInfo.id > 0)
					{
						sessionInfo.platform = platform;
						sessionInfo.logincheck = logincheck;
						sessionInfo.port = port;
						SessionBll.CreateUserLogin(sessionInfo, loginonce);
						WMSUtils.WriteUserCookie(platform, port, sessionInfo.token, -1);
					}
				}
			}
			sessionInfo.logincheck = logincheck;
			sessionInfo.port = port;
			if (sessionInfo.id > 0)
			{
				if (sessionInfo.updatetime.AddMinutes((double)sessionInfo.timeout) < DateTime.Now)
				{
					SessionBll.SessionLogout(sessionInfo);
					WMSUtils.ClearUserCookie(platform, port);
					FPSession.Remove("FP_OLUSERINFO");
					FPSession.Remove("FP_PERMISSION");
					sessionInfo.id = 0;
					sessionInfo.uid = 0;
					sessionInfo.username = "游客";
					sessionInfo.realname = "游客";
					sessionInfo.password = "";
					sessionInfo.platform = platform;
					sessionInfo.roleid = 2;
					sessionInfo.address = address;
					sessionInfo.updatetime = DbUtils.GetDateTime();
					sessionInfo.token = "";
					sessionInfo.state = 1;
				}
				else
				{
					sessionInfo.updatetime = DbUtils.GetDateTime();
					if (sessionInfo.updatetime.AddMinutes((double)expires) < DateTime.Now)
					{
						DbHelper.ExecuteUpdate<SessionInfo>(new SqlParam[]
						{
							DbHelper.MakeUpdate("updatetime", sessionInfo.updatetime),
							DbHelper.MakeAndWhere("id", sessionInfo.id)
						});
					}
				}
			}
			else
			{
				sessionInfo.sid = "";
				sessionInfo.uid = 0;
				sessionInfo.username = "游客";
				sessionInfo.realname = "游客";
				sessionInfo.password = "";
				sessionInfo.platform = platform;
				sessionInfo.roleid = 2;
				sessionInfo.address = address;
				sessionInfo.updatetime = DbUtils.GetDateTime();
				sessionInfo.token = "";
				sessionInfo.state = 1;
			}
			return sessionInfo;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000079CC File Offset: 0x00005BCC
		public static void CreateUserLogin(SessionInfo sessioninfo, int loginonce)
		{
			foreach (SessionInfo sessionInfo in DbHelper.ExecuteList<SessionInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("platform", sessioninfo.platform),
				DbHelper.MakeAndWhere("uid", sessioninfo.uid)
			}))
			{
				if (sessionInfo.updatetime.AddMinutes((double)sessioninfo.timeout) < DateTime.Now)
				{
					DbHelper.ExecuteDelete<SessionInfo>(sessionInfo.id);
					SessionBll.RemoveCacheSession(sessionInfo.platform, sessionInfo.token);
				}
				else if (loginonce == 1)
				{
					DbHelper.ExecuteUpdate<SessionInfo>(new SqlParam[]
					{
						DbHelper.MakeUpdate("state", 0),
						DbHelper.MakeAndWhere("id", sessionInfo.id)
					});
					SessionBll.RemoveCacheSession(sessionInfo.platform, sessionInfo.token);
				}
			}
			sessioninfo.id = DbHelper.ExecuteInsert<SessionInfo>(sessioninfo);
			SessionBll.InsertCacheSession(sessioninfo);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00007AEC File Offset: 0x00005CEC
		public static void SessionLogout(SessionInfo sessioninfo)
		{
			SessionBll.DeleteSession(sessioninfo.platform, sessioninfo.token);
			if (sessioninfo.logincheck == 1)
			{
				SSO.Logout(sessioninfo.platform, sessioninfo.token);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00007B19 File Offset: 0x00005D19
		public static void InsertCacheSession(SessionInfo sessioninfo)
		{
			if (sessioninfo.timeout > 0)
			{
				FPCache.Insert("FP_USERONLINE_" + sessioninfo.platform + "_" + sessioninfo.token, sessioninfo, sessioninfo.timeout);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00007B4C File Offset: 0x00005D4C
		public static SessionInfo GetCacheSession(string platform, string token)
		{
			object obj = FPCache.Get("FP_USERONLINE_" + platform + "_" + token);
			if (obj != null)
			{
				return (SessionInfo)obj;
			}
			return null;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00007B7B File Offset: 0x00005D7B
		public static void RemoveCacheSession(string platform, string token)
		{
			FPCache.Remove("FP_USERONLINE_" + platform + "_" + token);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00007B93 File Offset: 0x00005D93
		public static void DeleteSession(string platform, string token)
		{
			DbHelper.ExecuteDelete<SessionInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("platform", platform),
				DbHelper.MakeAndWhere("token", token)
			});
			SessionBll.RemoveCacheSession(platform, token);
		}
	}
}
