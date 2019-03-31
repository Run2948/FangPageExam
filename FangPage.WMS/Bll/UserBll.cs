using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200003E RID: 62
	public class UserBll
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000B9E1 File Offset: 0x00009BE1
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

		// Token: 0x060003EE RID: 1006 RVA: 0x0000BA14 File Offset: 0x00009C14
		public static UserInfo CheckLogin(string account, string password)
		{
			if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
			{
				return new UserInfo();
			}
			password = FPUtils.MD5(password);
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeWhere("("),
				DbHelper.MakeWhere("username", account),
				DbHelper.MakeOrWhere('(', "isemail", 1),
				DbHelper.MakeAndWhere("email", ')', account),
				DbHelper.MakeOrWhere('(', "ismobile", 1),
				DbHelper.MakeAndWhere("mobile", ')', account),
				DbHelper.MakeOrWhere('(', "isreal", 1),
				DbHelper.MakeAndWhere("idcard", ')', account),
				DbHelper.MakeWhere(")"),
				DbHelper.MakeAndWhere('(', "password", password),
				DbHelper.MakeOrWhere("password2", ')', password)
			});
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000BB04 File Offset: 0x00009D04
		public static UserInfo CheckMd5Login(string account, string md5_password)
		{
			if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(md5_password))
			{
				return new UserInfo();
			}
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeWhere("("),
				DbHelper.MakeWhere("username", account),
				DbHelper.MakeOrWhere('(', "isemail", 1),
				DbHelper.MakeAndWhere("email", ')', account),
				DbHelper.MakeOrWhere('(', "ismobile", 1),
				DbHelper.MakeAndWhere("mobile", ')', account),
				DbHelper.MakeOrWhere('(', "isreal", 1),
				DbHelper.MakeAndWhere("idcard", ')', account),
				DbHelper.MakeWhere(")"),
				DbHelper.MakeAndWhere('(', "password", md5_password),
				DbHelper.MakeOrWhere("password2", ')', md5_password)
			});
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public static UserInfo CheckLogin(string usercode)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("usercode", FPUtils.MD5(usercode + SysConfigs.GetConfig().passwordkey));
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000BC28 File Offset: 0x00009E28
		public static UserInfo CheckPassword(int id, string password)
		{
			return UserBll.CheckPassword(id, password, true);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000BC32 File Offset: 0x00009E32
		public static UserInfo CheckPassword(int id, string password, bool originalpassword)
		{
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", id),
				DbHelper.MakeAndWhere("password", originalpassword ? FPUtils.MD5(password) : password)
			});
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000BC6B File Offset: 0x00009E6B
		public static UserInfo CheckPassword2(int id, string password)
		{
			return UserBll.CheckPassword2(id, password, true);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000BC75 File Offset: 0x00009E75
		public static UserInfo CheckPassword2(int id, string password, bool originalpassword)
		{
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", id),
				DbHelper.MakeAndWhere("password2", originalpassword ? FPUtils.MD5(password) : password)
			});
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public static int CheckUserName(string username)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("username", username);
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				sqlParam
			}).id;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000BCDD File Offset: 0x00009EDD
		public static int UpdatePassword(int id, string password)
		{
			return DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeUpdate("password", FPUtils.MD5(password)),
				DbHelper.MakeAndWhere("id", id)
			});
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000BD10 File Offset: 0x00009F10
		public static int UpdatePassword(string username, string password)
		{
			return DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeUpdate("password", FPUtils.MD5(password)),
				DbHelper.MakeAndWhere("username", username)
			});
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000BD3E File Offset: 0x00009F3E
		public static int UpdatePassword2(int id, string password)
		{
			return DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeUpdate("password2", FPUtils.MD5(password)),
				DbHelper.MakeAndWhere("id", id)
			});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000BD71 File Offset: 0x00009F71
		public static void UpdateUserCredit(int uid, string cname, int type, int credits)
		{
			DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_UserInfo] SET [credits]=[credits]+{1} WHERE [id]={2}", DbConfigs.Prefix, credits, uid));
			UserBll.Credit_AddLog(uid, cname, type, credits);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000BD9E File Offset: 0x00009F9E
		public static void UpdateUserCredit(CreditInfo creditinfo)
		{
			DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_UserInfo] SET [credits]=[credits]+{1} WHERE [id]={2}", DbConfigs.Prefix, creditinfo.credits, creditinfo.uid));
			UserBll.Credit_AddLog(creditinfo);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000BDD2 File Offset: 0x00009FD2
		public static void UpdateUserExp(int uid, int exp)
		{
			DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_UserInfo] SET [exp]=[exp]+{1} WHERE [id]={2}", DbConfigs.Prefix, exp, uid));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000BDF5 File Offset: 0x00009FF5
		public static string GetUserSecques(int questionid, string answer)
		{
			if (questionid > 0)
			{
				return FPUtils.MD5(answer + FPUtils.MD5(questionid.ToString())).Substring(15, 8);
			}
			return "";
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000BE20 File Offset: 0x0000A020
		public static UserInfo GetMapUserInfo(int id)
		{
			return DbHelper.ExecuteModel<UserInfo>(id);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000BE28 File Offset: 0x0000A028
		public static UserInfo GetUserInfo(int id)
		{
			object obj = FPCache.Get("FP_USERLIST");
			List<UserInfo> list;
			if (obj != null)
			{
				list = (obj as List<UserInfo>);
				List<UserInfo> list2 = list.FindAll((UserInfo item) => item.id == id);
				if (list2.Count > 0)
				{
					return list2[0];
				}
			}
			else
			{
				list = new List<UserInfo>();
			}
			UserInfo mapUserInfo = UserBll.GetMapUserInfo(id);
			if (mapUserInfo.id > 0)
			{
				list.Add(mapUserInfo);
			}
			CacheBll.Insert("系统用户信息缓存", "FP_USERLIST", list);
			return mapUserInfo;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public static UserInfo GetUserInfo(string username)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("username", username);
			return DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BEDC File Offset: 0x0000A0DC
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

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BF0F File Offset: 0x0000A10F
		public static int Credit_AddLog(CreditInfo creditinfo)
		{
			return DbHelper.ExecuteInsert<CreditInfo>(creditinfo);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BF18 File Offset: 0x0000A118
		public static List<UserInfo> GetUserList(int departid)
		{
			string departIdList = DepartmentBll.GetDepartIdList(departid);
			return DbHelper.ExecuteList<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("departid", WhereType.In, departIdList),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BF54 File Offset: 0x0000A154
		public static string GetUserIdList(string keyword)
		{
			if (keyword == "")
			{
				return "";
			}
			return DbHelper.ExecuteField<UserInfo>(new SqlParam[]
			{
				DbHelper.MakeOrWhere("username", WhereType.Like, keyword),
				DbHelper.MakeOrWhere("realname", WhereType.Like, keyword)
			});
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BF94 File Offset: 0x0000A194
		public static string GetUserIdByDepart(string departs)
		{
			if (departs == "")
			{
				return "";
			}
			SqlParam sqlParam = DbHelper.MakeOrWhere("departid", WhereType.In, departs);
			return DbHelper.ExecuteField<UserInfo>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public static string GetUserIdByDepart()
		{
			return DbHelper.ExecuteField<UserInfo>();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public static int GetMaxUid()
		{
			object obj = FPCache.Get("FP_MAXUID");
			int num;
			if (obj != null)
			{
				num = FPUtils.StrToInt(obj);
			}
			else
			{
				num = FPUtils.StrToInt(DbHelper.ExecuteMax<UserInfo>("id"));
				FPCache.Insert("FP_MAXUID", num);
			}
			return num;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000C020 File Offset: 0x0000A220
		public static string UploadAvatar(HttpPostedFile postedFile, int maxwidth, int maxheight, int uid)
		{
			string result = "";
			try
			{
				string text = Path.GetExtension(postedFile.FileName).ToLower();
				if (FPArray.InArray(text, ".jpg|.gif|.png", "|") == -1)
				{
					result = "用户头像只允许上传jpg、png、gif图片类型。";
				}
				if (postedFile.ContentLength > 1048576)
				{
					result = "头像文件上传不得超过【1M】";
				}
				string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "common/avatar");
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				postedFile.SaveAs(mapPath + "\\" + uid.ToString() + text);
				if (maxwidth <= 0)
				{
					maxwidth = 150;
				}
				if (maxheight <= 0)
				{
					maxheight = 150;
				}
				FPThumb.MakeThumbnailImage(mapPath + "\\" + uid.ToString() + text, mapPath + "\\" + uid.ToString() + text, maxwidth, maxheight);
				DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
				{
					DbHelper.MakeUpdate("avatar", "common/avatar/" + uid.ToString() + text),
					DbHelper.MakeAndWhere("id", uid)
				});
			}
			catch (Exception)
			{
				result = "上传文件出错，请检查目录权限。";
			}
			return result;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000C14C File Offset: 0x0000A34C
		public static string UploadAvatar(string base64str, int uid)
		{
			string result = "";
			try
			{
				string mapPath = FPFile.GetMapPath(WebConfig.WebPath + "common/avatar");
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				string text = FPUtils.Base64ToImg(base64str, mapPath + "\\" + uid.ToString() + ".png");
				if (text != "")
				{
					result = text;
				}
				else
				{
					DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
					{
						DbHelper.MakeUpdate("avatar", "common/avatar/" + uid.ToString() + ".png"),
						DbHelper.MakeAndWhere("id", uid)
					});
				}
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}
	}
}
