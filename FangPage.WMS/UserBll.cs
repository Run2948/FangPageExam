using System;
using System.Collections.Generic;
using System.Data.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000047 RID: 71
	public class UserBll
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000B470 File Offset: 0x00009670
		public static UserInfo CreateGuestUser()
		{
			return new UserInfo
			{
				id = 0,
				roleid = 2,
				departid = 0,
				username = "游客",
				nickname = "游客"
			};
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B4BC File Offset: 0x000096BC
		public static UserInfo CheckLogin(string username, string password)
		{
			string commandText = string.Format("SELECT * FROM [{0}WMS_UserInfo] WHERE ([username]=@account OR ([isemail]=1 AND [email]=@account) OR ([ismobile]=1 AND [mobile]=@account)) AND [password]=@password", DbConfigs.Prefix);
			DbParameter[] dbparams = new DbParameter[]
			{
				DbHelper.MakeInParam("@account", username),
				DbHelper.MakeInParam("@password", FPUtils.MD5(password))
			};
			return DbHelper.ExecuteModel<UserInfo>(commandText, dbparams);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B518 File Offset: 0x00009718
		public static UserInfo CheckPassword(int id, string password)
		{
			return UserBll.CheckPassword(id, password, true);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000B534 File Offset: 0x00009734
		public static UserInfo CheckPassword(int id, string password, bool originalpassword)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", id),
				DbHelper.MakeAndWhere("password", originalpassword ? FPUtils.MD5(password) : password)
			};
			return DbHelper.ExecuteModel<UserInfo>(sqlparams);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000B584 File Offset: 0x00009784
		public static UserInfo CheckPassword2(int id, string password)
		{
			return UserBll.CheckPassword2(id, password, true);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000B5A0 File Offset: 0x000097A0
		public static UserInfo CheckPassword2(int id, string password, bool originalpassword)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", id),
				DbHelper.MakeAndWhere("password2", originalpassword ? FPUtils.MD5(password) : password)
			};
			return DbHelper.ExecuteModel<UserInfo>(sqlparams);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public static bool CheckUserName(string username)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("username", username);
			UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
			return userInfo.id > 0;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000B62C File Offset: 0x0000982C
		public static UserInfo GetOnlineUser(string passwordkey, int timeout)
		{
			return UserBll.GetOnlineUser(passwordkey, timeout, 0);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000B648 File Offset: 0x00009848
		public static UserInfo GetOnlineUser(string passwordkey, int timeout, int uid)
		{
			UserInfo userInfo = new UserInfo();
			int num = FPUtils.StrToInt(WMSCookie.GetCookie("userid"), uid);
			string text = DES.Decode(WMSCookie.GetCookie("password"), passwordkey).Trim();
			if (text.Length == 0)
			{
				num = 0;
			}
			if (num > 0)
			{
				DateTime lastCookieTime = WMSCookie.GetLastCookieTime();
				DateTime t = DateTime.Now.AddMinutes((double)(timeout * -1));
				if (lastCookieTime >= t)
				{
					UserInfo userInfo2 = UserBll.CheckPassword(num, text, false);
					if (userInfo2.id > 0)
					{
						return userInfo2;
					}
				}
				WMSCookie.ClearUserCookie();
			}
			return UserBll.CreateGuestUser();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B70C File Offset: 0x0000990C
		public static void UpdateOnlineState(int uid, int timeout, int onlinefrequency)
		{
			if (uid > 0)
			{
				UserBll.UpdateUserState(uid, 1);
			}
			if (UserBll._lastRemoveTimeout == 0 || Environment.TickCount - UserBll._lastRemoveTimeout > 60000 * onlinefrequency)
			{
				UserBll.UpdateExpiredOnlineUsers(timeout);
				UserBll._lastRemoveTimeout = Environment.TickCount;
			}
			WMSCookie.WriteCookie("lastactivity", FPUtils.GetDateTime());
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000B778 File Offset: 0x00009978
		private static void UpdateExpiredOnlineUsers(int timeOut)
		{
			DateTime dateTime = Convert.ToDateTime(DateTime.Now.AddMinutes((double)(timeOut * -1)).ToLocalTime().ToString());
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeSet("onlinestate", 0),
				DbHelper.MakeAndWhere("lastvisit", WhereType.LessThan, dateTime)
			};
			DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B7EC File Offset: 0x000099EC
		public static void UpdateUserState(int id, int onlinestate)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeSet("onlinestate", onlinestate),
				DbHelper.MakeSet("lastvisit", DbUtils.GetDateTime()),
				DbHelper.MakeAndWhere("id", id)
			};
			DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B848 File Offset: 0x00009A48
		public static int UpdatePassword(int id, string password)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeSet("password", FPUtils.MD5(password)),
				DbHelper.MakeAndWhere("id", id)
			};
			return DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B890 File Offset: 0x00009A90
		public static int UpdatePassword2(int id, string password)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeSet("password2", FPUtils.MD5(password)),
				DbHelper.MakeAndWhere("id", id)
			};
			return DbHelper.ExecuteUpdate<UserInfo>(sqlparams);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		public static void UpdateUserCredit(int uid, string cname, int type, int credits)
		{
			string sqlstring = string.Format("UPDATE [{0}WMS_UserInfo] SET [credits]=[credits]+{1} WHERE [id]={2}", DbConfigs.Prefix, credits, uid);
			DbHelper.ExecuteSql(sqlstring);
			UserBll.Credit_AddLog(uid, cname, type, credits);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B914 File Offset: 0x00009B14
		public static void UpdateUserCredit(CreditInfo creditinfo)
		{
			string sqlstring = string.Format("UPDATE [{0}WMS_UserInfo] SET [credits]=[credits]+{1} WHERE [id]={2}", DbConfigs.Prefix, creditinfo.credits, creditinfo.uid);
			DbHelper.ExecuteSql(sqlstring);
			UserBll.Credit_AddLog(creditinfo);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B958 File Offset: 0x00009B58
		public static void UpdateUserExp(int uid, int exp)
		{
			UserInfo userInfo = UserBll.GetUserInfo(uid);
			userInfo.exp += exp;
			int num = FPUtils.StrToInt(DbHelper.ExecuteMax<UserGrade>("expupper"));
			if (userInfo.exp > num)
			{
				userInfo.exp = num;
			}
			UserGrade userGradeByExpHigher = UserBll.GetUserGradeByExpHigher(userInfo.exp);
			string sqlstring = string.Format("UPDATE [{0}WMS_UserInfo] SET [gradeid]={1},[exp]=[exp]+{2} WHERE [id]={3}", new object[]
			{
				DbConfigs.Prefix,
				userGradeByExpHigher.id,
				exp,
				uid
			});
			DbHelper.ExecuteSql(sqlstring);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public static string GetUserSecques(int questionid, string answer)
		{
			string result;
			if (questionid > 0)
			{
				result = FPUtils.MD5(answer + FPUtils.MD5(questionid.ToString())).Substring(15, 8);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000BA44 File Offset: 0x00009C44
		public static UserInfo GetUserInfo(int id)
		{
			return DbHelper.ExecuteModel<UserInfo>(id);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000BA5C File Offset: 0x00009C5C
		public static FullUserInfo GetFullUserInfo(int uid)
		{
			return DbHelper.ExecuteModel<FullUserInfo>(uid);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000BA74 File Offset: 0x00009C74
		public static int Credit_AddLog(int uid, string name, int type, int credits)
		{
			return DbHelper.ExecuteInsert<CreditInfo>(new CreditInfo
			{
				uid = uid,
				name = name,
				type = type,
				credits = credits,
				postdatetime = DbUtils.GetDateTime()
			});
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		public static int Credit_AddLog(CreditInfo creditinfo)
		{
			return DbHelper.ExecuteInsert<CreditInfo>(creditinfo);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public static UserGrade GetUserGradeByExpHigher(int expHigher)
		{
			if (expHigher < 0)
			{
				expHigher = 0;
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("explower", WhereType.LessThanEqual, expHigher));
			int num = FPUtils.StrToInt(DbHelper.ExecuteMax<UserGrade>("expupper"));
			if (expHigher >= num)
			{
				list.Add(DbHelper.MakeAndWhere("expupper", num));
			}
			else
			{
				list.Add(DbHelper.MakeAndWhere("expupper", WhereType.GreaterThan, expHigher));
			}
			return DbHelper.ExecuteModel<UserGrade>(list.ToArray());
		}

		// Token: 0x0400014C RID: 332
		private static int _lastRemoveTimeout;
	}
}
