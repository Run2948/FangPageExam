using System;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x0200000E RID: 14
	public class reg_mobile : APIController
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000035E8 File Offset: 0x000017E8
		protected override void Controller()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.userid > 0)
			{
				base.WriteErr("对不起，系统不允许重复注册用户。");
				return;
			}
			if (this.regconfig.regstatus != 1)
			{
				base.WriteErr("对不起，系统目前暂不允许新用户注册。");
				return;
			}
			if (this.regconfig.regctrl > 0)
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("regip", FPRequest.GetIP());
				UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
				{
					sqlParam
				});
				if (userInfo.id > 0)
				{
					int num = this.StrDateDiffHours(userInfo.joindatetime.Value, this.regconfig.regctrl);
					if (num < 0)
					{
						base.WriteErr("抱歉，您注册过于频繁，请在 " + (num * -1).ToString() + " 小时后再重新注册");
						return;
					}
				}
			}
			if (this.regconfig.ipaccess.Trim() != "")
			{
				string[] iparray = FPArray.SplitString(this.regconfig.ipaccess, "|");
				if (!FPArray.InIPArray(this.ip, iparray))
				{
					base.WriteErr("抱歉，系统设置了IP注册限制，您所在的IP段不允许注册");
					return;
				}
			}
			else if (this.regconfig.ipdenyaccess.Trim() != "")
			{
				string[] iparray2 = FPArray.SplitString(this.regconfig.ipdenyaccess, "|");
				if (FPArray.InIPArray(this.ip, iparray2))
				{
					base.WriteErr("对不起，您所在的IP段已被禁止注册");
					return;
				}
			}
			if (this.ispost)
			{
				string @string = FPRequest.GetString("mobile");
				string string2 = FPRequest.GetString("password");
				string string3 = FPRequest.GetString("smsverify");
				if (@string.Equals(""))
				{
					base.WriteErr("手机号码不能为空");
					return;
				}
				if (@string.Trim().Length > 11)
				{
					base.WriteErr("手机号码不能大于11个字符");
					return;
				}
				if (!Regex.IsMatch(@string.Trim(), "^[\\d|-]+$"))
				{
					base.WriteErr("手机号码中含有非法字符");
					return;
				}
				if (this.regconfig.accessmobile != "")
				{
					string[] mobilearray = FPArray.SplitString(this.regconfig.accessmobile, "|");
					if (!FPArray.InMobileArray(@string, mobilearray))
					{
						base.WriteErr("抱歉，系统设置了手机注册限制，您手机所在的号段不允许注册");
						return;
					}
				}
				else if (this.regconfig.censormobile != "")
				{
					string[] mobilearray2 = FPArray.SplitString(this.regconfig.censormobile, "|");
					if (FPArray.InMobileArray(@string, mobilearray2))
					{
						base.WriteErr("对不起，您手机所在的号段已被禁止注册");
						return;
					}
				}
				if (DbHelper.ExecuteCount<UserInfo>("[mobile]='" + @string + "'") > 0)
				{
					base.WriteErr("该手机号码已被注册,请选用其他号码");
					return;
				}
				if (string2.Equals(""))
				{
					base.WriteErr("密码不能为空");
					return;
				}
				if (this.regconfig.realname == 1 && FPRequest.GetString("realname") == "")
				{
					base.WriteErr("真实姓名不能为空");
					return;
				}
				if (this.regconfig.email == 1 && FPRequest.GetString("email") == "")
				{
					base.WriteErr("电子邮箱不能为空");
					return;
				}
				if (this.regconfig.idcard == 1 && FPRequest.GetString("idcard") == "")
				{
					base.WriteErr("身份证号码不能为空");
					return;
				}
				if (this.regconfig.depart == 1 && FPRequest.GetInt("departid") == 0)
				{
					base.WriteErr("请选择所在部门");
					return;
				}
				if (this.isseccode)
				{
					if (FPRequest.GetString("verify").Equals(""))
					{
						base.WriteErr("图片验证码不能为空");
						return;
					}
					if (!this.isvalid)
					{
						base.WriteErr("图片验证码错误");
						return;
					}
				}
				if (string3.Equals(""))
				{
					base.WriteErr("短信验证码不能为空");
					return;
				}
				string clientsms = string.Concat(new object[]
				{
					@string,
					"|",
					string3,
					"|",
					DbUtils.GetDateTime()
				});
				if (this.Session["FP_SMSVERIFY"] == null)
				{
					base.WriteErr("无效验证码");
					return;
				}
				string serversms = this.Session["FP_SMSVERIFY"].ToString();
				int num2 = SMS.CheckSMS(clientsms, serversms);
				if (num2 == 0)
				{
					base.WriteErr("验证手机号码不正确");
					return;
				}
				if (num2 == -1)
				{
					base.WriteErr("验证码不正确");
					return;
				}
				if (num2 == 2)
				{
					base.WriteErr("验证码已过期");
					return;
				}
				this.iuser = FPRequest.GetModel<UserInfo>();
				this.iuser.username = @string;
				this.iuser.password = FPUtils.MD5(this.iuser.password);
				this.iuser.credits = this.regconfig.credit;
				this.iuser.regip = FPRequest.GetIP();
				this.iuser.joindatetime = new DateTime?(DbUtils.GetDateTime());
				this.iuser.authstr = "";
				this.iuser.authflag = 0;
				this.iuser.roleid = 5;
				this.iuser.ismobile = 1;
				if (this.iuser.departid > 0 && this.iuser.departname == "")
				{
					Department departInfo = DepartmentBll.GetDepartInfo(this.iuser.departid);
					this.iuser.departname = departInfo.name;
				}
				DbHelper.ExecuteInsert<UserInfo>(this.iuser);
				if (this.iuser.id > 0)
				{
					if (this.regconfig.credit > 0 && this.iuser.credits > 0)
					{
						UserBll.Credit_AddLog(this.iuser.id, "用户注册", 0, this.iuser.credits);
					}
					SessionInfo sessionInfo = new SessionInfo();
					sessionInfo.sid = FPRandom.CreateCode(32);
					sessionInfo.uid = this.iuser.id;
					sessionInfo.username = this.iuser.username;
					sessionInfo.password = this.iuser.password;
					sessionInfo.realname = this.iuser.realname;
					sessionInfo.avatar = this.iuser.avatar;
					sessionInfo.roleid = this.iuser.roleid;
					sessionInfo.rolename = this.iuser.rolename;
					sessionInfo.departid = this.iuser.departid;
					sessionInfo.departname = this.iuser.departname;
					sessionInfo.address = this.ip;
					sessionInfo.platform = this.platform;
					sessionInfo.invisible = FPRequest.GetInt("invisible");
					sessionInfo.timeout = this.sysconfig.onlinetimeout;
					sessionInfo.token = FPUtils.MD5(sessionInfo.sid + sessionInfo.username + sessionInfo.password + sessionInfo.address);
					sessionInfo.logincheck = this.sysconfig.ssocheck;
					sessionInfo.port = this.port;
					sessionInfo.errmsg = "ok";
					SessionBll.CreateUserLogin(sessionInfo, this.sysconfig.loginonce);
					FPSession.Insert("FP_OLUSERINFO", this.iuser);
					FPSession.Remove("FP_PERMISSION");
					WMSUtils.WriteUserCookie(this.platform, this.port, sessionInfo.token, -1);
					SysBll.InsertLog(this.iuser.id, "用户登录", "登录，登录名：" + this.iuser.username, true);
					base.WriteSuccess("注册成功");
					return;
				}
				base.WriteErr("注册失败，请检查输入是否正确。");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003D6C File Offset: 0x00001F6C
		private int StrDateDiffHours(DateTime time, int hours)
		{
			TimeSpan timeSpan = DateTime.Now - time.AddHours((double)hours);
			if (timeSpan.TotalHours > 2147483647.0)
			{
				return int.MaxValue;
			}
			if (timeSpan.TotalHours < -2147483648.0)
			{
				return int.MinValue;
			}
			return (int)timeSpan.TotalHours;
		}

		// Token: 0x04000007 RID: 7
		protected RegConfig regconfig = new RegConfig();

		// Token: 0x04000008 RID: 8
		protected UserInfo iuser = new UserInfo();
	}
}
