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
	// Token: 0x0200000D RID: 13
	public class reg_acount : APIController
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002B90 File Offset: 0x00000D90
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
				string @string = FPRequest.GetString("username");
				string string2 = FPRequest.GetString("password");
				string string3 = FPRequest.GetString("repeat");
				if (@string.Equals(""))
				{
					base.WriteErr("用户名不能为空");
					return;
				}
				if (@string.Length < 2)
				{
					base.WriteErr("对不起，用户名不能小于2个字符");
					return;
				}
				if (@string.Length > 20)
				{
					base.WriteErr("对不起，用户名不能大于20个字符");
					return;
				}
				if (!FPUtils.IsSafeSqlString(@string))
				{
					base.WriteErr("对不起，您使用的用户名有敏感字符");
					return;
				}
				if (this.InRestrictArray(@string, this.regconfig.restrict))
				{
					base.WriteErr("对不起，该用户名：" + @string + " 不允许使用");
					return;
				}
				if (UserBll.CheckUserName(@string) > 0)
				{
					base.WriteErr("该用户名已经存在，请使用别的用户名");
					return;
				}
				if (string2.Equals(""))
				{
					base.WriteErr("密码不能为空");
					return;
				}
				if (string2 != string3)
				{
					base.WriteErr("对不起，两次输入密码不相同");
					return;
				}
				if (this.regconfig.realname == 1 && FPRequest.GetString("realname") == "")
				{
					base.WriteErr("真实姓名不能为空");
					return;
				}
				if (this.regconfig.mobile == 1 && FPRequest.GetString("mobile") == "")
				{
					base.WriteErr("手机号码不能为空");
					return;
				}
				string string4 = FPRequest.GetString("email");
				if ((this.regconfig.email == 1 || this.regconfig.regverify == 2) && string4 == "")
				{
					base.WriteErr("电子邮箱不能为空");
					return;
				}
				if (string4 != "")
				{
					if (!FPUtils.IsEmail(string4))
					{
						base.WriteErr("邮箱格式不正确");
						return;
					}
					string emailHostName = this.GetEmailHostName(string4);
					if (this.regconfig.accessemail.Trim() != "")
					{
						if (FPArray.InArray(emailHostName, this.regconfig.accessemail, "|") == -1)
						{
							base.WriteErr("本站点只允许使用以下域名的Email地址注册：" + this.regconfig.accessemail);
							return;
						}
					}
					else if (this.regconfig.censoremail.Trim() != "" && FPArray.InArray(emailHostName, this.regconfig.censoremail, "|") >= 0)
					{
						base.WriteErr("本站点不允许使用以下域名的Email地址注册: " + this.regconfig.censoremail);
						return;
					}
				}
				if (DbHelper.ExecuteCount<UserInfo>("[email]='" + string4 + "'") > 0)
				{
					base.WriteErr("邮箱: \"" + string4 + "\" 已经被其他用户使用");
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
						base.WriteErr("验证码不能为空");
						return;
					}
					if (!this.isvalid)
					{
						base.WriteErr("验证码错误");
						return;
					}
				}
				this.iuser = FPRequest.GetModel<UserInfo>();
				this.iuser.password = FPUtils.MD5(this.iuser.password);
				this.iuser.credits = this.regconfig.credit;
				this.iuser.regip = FPRequest.GetIP();
				this.iuser.joindatetime = new DateTime?(DbUtils.GetDateTime());
				if (this.iuser.departid > 0)
				{
					Department departInfo = DepartmentBll.GetDepartInfo(this.iuser.departid);
					this.iuser.departname = departInfo.name;
				}
				if (this.regconfig.regverify == 1)
				{
					this.iuser.authstr = "";
					this.iuser.authflag = 1;
					this.iuser.roleid = 3;
				}
				else if (this.regconfig.regverify == 2)
				{
					this.iuser.authstr = FPRandom.CreateAuth(20);
					this.iuser.authflag = 1;
					this.iuser.roleid = 3;
					string newValue = string.Concat(new string[]
					{
						"<pre style=\"width:100%;word-wrap:break-word\"><a href=\"http://",
						this.domain,
						this.rawpath,
						"activationuser.aspx?authstr=",
						this.iuser.authstr,
						"\"  target=\"_blank\">http://",
						this.domain,
						this.rawpath,
						"activationuser.aspx?authstr=",
						this.iuser.authstr,
						"</a></pre>"
					});
					MsgTempInfo msgTemplate = MsgTempBll.GetMsgTemplate("email_register");
					msgTemplate.content = msgTemplate.content.Replace("【用户名】", this.iuser.username).Replace("【邮箱帐号】", this.iuser.email).Replace("【激活链接】", newValue);
					Email.Send(string4, msgTemplate.subject, msgTemplate.content);
				}
				else
				{
					this.iuser.authstr = "";
					this.iuser.authflag = 0;
					this.iuser.roleid = 5;
				}
				DbHelper.ExecuteInsert<UserInfo>(this.iuser);
				if (this.iuser.id > 0)
				{
					if (this.regconfig.credit > 0 && this.iuser.credits > 0)
					{
						UserBll.Credit_AddLog(this.iuser.id, "用户注册", 0, this.iuser.credits);
					}
					if (this.regconfig.regverify == 0)
					{
                        SessionInfo sessionInfo = new SessionInfo
                        {
                            sid = FPRandom.CreateCode(32),
                            uid = this.iuser.id,
                            username = this.iuser.username,
                            password = this.iuser.password,
                            realname = this.iuser.realname,
                            avatar = this.iuser.avatar,
                            roleid = this.iuser.roleid,
                            rolename = this.iuser.rolename,
                            departid = this.iuser.departid,
                            departname = this.iuser.departname,
                            address = this.ip,
                            platform = this.platform,
                            invisible = FPRequest.GetInt("invisible"),
                            timeout = this.sysconfig.onlinetimeout
                        };
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
					if (this.regconfig.regverify == 2)
					{
						base.WriteErr(2, "您的注册邮箱[" + this.iuser.email + "]将收到一封认证邮件，请登录您的邮箱查收，并点击邮件中的链接完成激活。激活成功后，可以使用站内所有功能，再次感谢您的加入。");
						return;
					}
					base.WriteErr(1, "注册成功，但需要管理员审核才能登录，请耐心等待...");
					return;
				}
				else
				{
					base.WriteErr("注册失败，请检查输入是否正确");
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000034B8 File Offset: 0x000016B8
		private bool InRestrictArray(string usernametxt, string restrict)
		{
			if (restrict == null || restrict == "")
			{
				return false;
			}
			restrict = Regex.Escape(restrict).Replace("\\*", "[\\w-]*");
			foreach (string text in FPArray.SplitString(restrict.ToLower(), "|"))
			{
				if (new Regex(string.Format("^{0}$", text)).IsMatch(usernametxt.ToLower()) && !text.Trim().Equals(""))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003544 File Offset: 0x00001744
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

		// Token: 0x06000024 RID: 36 RVA: 0x0000359D File Offset: 0x0000179D
		private string GetEmailHostName(string strEmail)
		{
			if (strEmail.IndexOf("@") < 0)
			{
				return "";
			}
			return strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
		}

		// Token: 0x04000005 RID: 5
		protected RegConfig regconfig = new RegConfig();

		// Token: 0x04000006 RID: 6
		protected UserInfo iuser = new UserInfo();
	}
}
