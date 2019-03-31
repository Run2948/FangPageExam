using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using System;

namespace FP_Exam.Controller
{
	public class login : APIController
	{
		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				string @string = FPRequest.GetString("username");
				string string2 = FPRequest.GetString("password");
				string string3 = FPRequest.GetString("realname");
				int @int = FPRequest.GetInt("sex");
				string string4 = FPRequest.GetString("email");
				string string5 = FPRequest.GetString("mobile");
				int int2 = FPRequest.GetInt("status");
				string string6 = FPRequest.GetString("departid");
				string string7 = FPRequest.GetString("departname");
				FPData fPData = new FPData();
				bool flag = @string != "";
				if (flag)
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("username", @string);
					UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
					{
						sqlParam
					});
					SqlParam sqlParam2 = DbHelper.MakeAndWhere("name", string7);
					Department department = DbHelper.ExecuteModel<Department>(new SqlParam[]
					{
						sqlParam2
					});
					bool flag2 = department.id == 0;
					if (flag2)
					{
						department.name = string7;
						department.keyid = string6;
						DbHelper.ExecuteInsert<Department>(department);
					}
					userInfo.password = string2;
					userInfo.realname = string3;
					userInfo.email = string4;
					userInfo.mobile = string5;
					userInfo.departid = department.id;
					userInfo.departname = string7;
					bool flag3 = @int == 0;
					if (flag3)
					{
						userInfo.sex = "男";
					}
					else
					{
						userInfo.sex = "女";
					}
					bool flag4 = userInfo.id > 0;
					if (flag4)
					{
						DbHelper.ExecuteUpdate<UserInfo>(userInfo);
					}
					else
					{
						DbHelper.ExecuteInsert<UserInfo>(userInfo);
					}
					SessionInfo sessionInfo = new SessionInfo();
					sessionInfo.sid = FPRandom.CreateCode(32);
					sessionInfo.uid = userInfo.id;
					sessionInfo.username = userInfo.username;
					sessionInfo.password = userInfo.password;
					sessionInfo.realname = userInfo.realname;
					sessionInfo.roleid = userInfo.roleid;
					sessionInfo.rolename = userInfo.rolename;
					sessionInfo.departid = userInfo.departid;
					sessionInfo.departname = userInfo.departname;
					sessionInfo.address = this.ip;
					sessionInfo.platform = this.platform;
					sessionInfo.invisible = FPRequest.GetInt("invisible");
					sessionInfo.timeout = this.sysconfig.onlinetimeout;
					sessionInfo.token = FPUtils.MD5(sessionInfo.sid + sessionInfo.username + sessionInfo.password + sessionInfo.address);
					sessionInfo.errmsg = "用户名或密码错误。";
					sessionInfo.logincheck = this.sysconfig.ssocheck;
					sessionInfo.port = this.port;
					sessionInfo.errmsg = "ok";
					SessionBll.CreateUserLogin(sessionInfo, this.sysconfig.loginonce);
					FPSession.Insert("FP_OLUSERINFO", userInfo);
					FPSession.Remove("FP_PERMISSION");
					WMSUtils.WriteUserCookie(this.platform, this.port, sessionInfo.token, -1);
					SysBll.InsertLog(userInfo.id, "用户登录", "登录，登录名：" + userInfo.username, true);
					fPData["errcode"] = "0";
					fPData["errmsg"] = "";
				}
				else
				{
					fPData["errcode"] = "1";
					fPData["errmsg"] = "用户登录错误";
				}
				FPResponse.WriteJson(fPData);
			}
		}
	}
}
