using System;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000058 RID: 88
	public class register : WMSController
	{
		// Token: 0x0600031F RID: 799 RVA: 0x0000D54C File Offset: 0x0000B74C
		protected override void View()
		{
			if (this.reurl == "")
			{
				this.reurl = "login.aspx";
			}
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.ispost)
			{
				if (this.userid > 0)
				{
					this.ShowErr("对不起，系统不允许重复注册用户。");
				}
				else if (this.regconfig.regstatus != 1)
				{
					this.ShowErr("对不起，系统目前暂不允许新用户注册。");
				}
				else
				{
					if (this.regconfig.regctrl > 0)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("regip", FPRequest.GetIP());
						UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
						{
							sqlParam
						});
						if (userInfo.id > 0)
						{
							int num = this.StrDateDiffHours(userInfo.joindatetime, this.regconfig.regctrl);
							if (num < 0)
							{
								this.ShowErr("抱歉，系统设置了IP注册间隔限制，您必须在 " + (num * -1).ToString() + " 小时后才可以重新注册");
								return;
							}
						}
					}
					if (this.regconfig.ipregctrl.Trim() != "")
					{
						string[] iparray = FPUtils.SplitString(this.regconfig.ipregctrl, "|");
						if (FPUtils.InIPArray(FPRequest.GetIP(), iparray))
						{
							this.ShowErr("抱歉，系统设置了IP注册限制，您所在的IP段不允许注册。");
							return;
						}
					}
					string @string = FPRequest.GetString("username");
					string string2 = FPRequest.GetString("password");
					string text = FPRequest.GetString("email").Trim().ToLower();
					string string3 = FPRequest.GetString("realname");
					string string4 = FPRequest.GetString("idcard");
					string text2 = FPRequest.GetString("mobile").Trim();
					if (@string.Equals(""))
					{
						this.ShowErr("用户名不能为空。");
					}
					else if (@string.Length < 3)
					{
						this.ShowErr("对不起，用户名不能小于3个字符");
					}
					else if (@string.Length > 20)
					{
						this.ShowErr("对不起，用户名不能大于20个字符");
					}
					else if (!FPUtils.IsSafeSqlString(@string))
					{
						this.ShowErr("对不起，您使用的用户名有敏感字符");
					}
					else if (this.InRestrictArray(@string, this.regconfig.restrict))
					{
						this.ShowErr("对不起，该用户名：" + @string + " 不允许使用");
					}
					else if (UserBll.CheckUserName(@string))
					{
						this.ShowErr("该用户名已经存在，请使用别的用户名。");
					}
					else if (string2.Equals(""))
					{
						this.ShowErr("密码不能为空");
					}
					else if (string2 != FPRequest.GetString("repeat"))
					{
						this.ShowErr("对不起，两次输入密码不相同");
					}
					else if (this.regconfig.email == 1 && text == "")
					{
						this.ShowErr("Email不能为空");
					}
					else if (text.Trim() != "" && !FPUtils.IsEmail(text))
					{
						this.ShowErr("Email格式不正确");
					}
					else
					{
						if (text.Trim() != "")
						{
							if (DbHelper.ExecuteCount<UserInfo>("[email]='" + text + "'") > 0)
							{
								this.ShowErr("邮箱: \"" + text + "\" 已经被其他用户使用");
								return;
							}
						}
						string emailHostName = this.GetEmailHostName(text);
						if (text.Trim() != "" && this.regconfig.accessemail.Trim() != "")
						{
							if (!FPUtils.InArray(emailHostName, this.regconfig.accessemail, "|"))
							{
								this.ShowErr("本站点只允许使用以下域名的Email地址注册：" + this.regconfig.accessemail);
								return;
							}
						}
						else if (text.Trim() != "" && this.regconfig.censoremail.Trim() != "")
						{
							if (FPUtils.InArray(text, this.regconfig.censoremail, "|"))
							{
								this.ShowErr("本站点不允许使用以下域名的Email地址注册: " + this.regconfig.censoremail);
								return;
							}
						}
						if (this.regconfig.realname == 1)
						{
							if (string3.Equals(""))
							{
								this.ShowErr("真实姓名不能为空");
								return;
							}
						}
						if (this.InRestrictArray(string3, this.regconfig.restrict))
						{
							this.ShowErr("对不起，该姓名：[" + string3 + "]不允许使用");
						}
						else
						{
							if (this.regconfig.mobile == 1)
							{
								if (text2.Equals(""))
								{
									this.ShowErr("手机号码不能为空");
									return;
								}
							}
							if (text2.Trim().Length > 20)
							{
								this.ShowErr("手机号码不能大于20个字符");
							}
							else if (text2.Trim() != "" && !Regex.IsMatch(text2.Trim(), "^[\\d|-]+$"))
							{
								this.ShowErr("手机号码中含有非法字符");
							}
							else
							{
								if (this.regconfig.rules == 1)
								{
									if (FPRequest.GetInt("rules", 0) != 1)
									{
										this.ShowErr("对不起，您没有选择同意网站许可协议");
										return;
									}
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
								this.iuser = FPRequest.GetModel<UserInfo>();
								this.iuser.password = FPUtils.MD5(this.iuser.password);
								this.iuser.credits = this.regconfig.credit;
								this.iuser.regip = FPRequest.GetIP();
								this.iuser.joindatetime = DbUtils.GetDateTime();
								if (this.regconfig.regverify == 1)
								{
									this.iuser.authstr = "";
									this.iuser.authflag = 1;
									this.iuser.roleid = 3;
								}
								else if (this.regconfig.regverify == 2)
								{
									this.iuser.authstr = WMSUtils.CreateAuthStr(20);
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
									Email.Send(text, msgTemplate.name, msgTemplate.content);
								}
								else
								{
									this.iuser.authstr = "";
									this.iuser.authflag = 0;
									this.iuser.roleid = 5;
								}
								this.iuser.id = DbHelper.ExecuteInsert<UserInfo>(this.iuser);
								if (this.iuser.id > 0)
								{
									if (this.regconfig.credit > 0 && this.iuser.credits > 0)
									{
										UserBll.Credit_AddLog(this.iuser.id, "用户注册", 0, this.iuser.credits);
									}
									if (this.regconfig.regverify == 1)
									{
										base.AddMsg("注册成功, 但需要等待管理员审核后您的帐户才能生效。");
									}
									else if (this.regconfig.regverify == 2)
									{
										base.AddMsg("您的注册邮箱[" + this.iuser.email + "]将收到一封认证邮件，请登录您的邮箱查收，并点击邮件中的链接完成激活。激活成功后，可以使用站内所有功能，再次感谢您的加入。");
									}
									else
									{
										base.AddMsg("注册成功, 请点击下面链接返回登录。");
									}
								}
								else
								{
									this.ShowErr("注册失败，请检查输入是否正确。");
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000DF28 File Offset: 0x0000C128
		private bool InRestrictArray(string usernametxt, string restrict)
		{
			bool result;
			if (restrict == null || restrict == "")
			{
				result = false;
			}
			else
			{
				restrict = Regex.Escape(restrict).Replace("\\*", "[\\w-]*");
				foreach (string text in FPUtils.SplitString(restrict.ToLower(), "|"))
				{
					Regex regex = new Regex(string.Format("^{0}$", text));
					if (regex.IsMatch(usernametxt.ToLower()) && !text.Trim().Equals(""))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000DFE4 File Offset: 0x0000C1E4
		private int StrDateDiffHours(DateTime time, int hours)
		{
			TimeSpan timeSpan = DateTime.Now - time.AddHours((double)hours);
			int result;
			if (timeSpan.TotalHours > 2147483647.0)
			{
				result = int.MaxValue;
			}
			else if (timeSpan.TotalHours < -2147483648.0)
			{
				result = int.MinValue;
			}
			else
			{
				result = (int)timeSpan.TotalHours;
			}
			return result;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E058 File Offset: 0x0000C258
		private string GetEmailHostName(string strEmail)
		{
			string result;
			if (strEmail.IndexOf("@") < 0)
			{
				result = "";
			}
			else
			{
				result = strEmail.Substring(strEmail.LastIndexOf("@")).ToLower();
			}
			return result;
		}

		// Token: 0x04000158 RID: 344
		protected string reurl = FPRequest.GetString("reurl");

		// Token: 0x04000159 RID: 345
		protected RegConfig regconfig = new RegConfig();

		// Token: 0x0400015A RID: 346
		protected UserInfo iuser = new UserInfo();
	}
}
