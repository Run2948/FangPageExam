using System;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Controller
{
	// Token: 0x02000056 RID: 86
	public class login : WMSController
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000D178 File Offset: 0x0000B378
		protected override void View()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			if (this.reurl == "")
			{
				this.reurl = "index.aspx";
			}
			if (this.userid > 0)
			{
				base.Response.Redirect(this.reurl);
			}
			else if (this.ispost)
			{
				string @string = FPRequest.GetString("username");
				string string2 = FPRequest.GetString("password");
				if (@string == "")
				{
					this.ShowErr("帐号不能为空");
				}
				else if (string2 == "")
				{
					this.ShowErr("密码不能为空");
				}
				else
				{
					if (this.isseccode)
					{
						if (FPRequest.GetString("verify").Equals(""))
						{
							this.ShowErr("验证码不能为空");
							return;
						}
						if (string.Compare(this.Session["FP_VERIFY"].ToString().ToLower(), FPRequest.GetString("verify").ToLower(), true) != 0)
						{
							this.ShowErr("验证码错误");
							return;
						}
					}
					UserInfo userInfo = UserBll.CheckLogin(@string, string2);
					if (userInfo.id > 0)
					{
						if (userInfo.roleid == 4)
						{
							this.ShowErr("对不起，该用户已被禁止登录");
						}
						else if (userInfo.roleid == 3)
						{
							if (this.regconfig.regverify == 1)
							{
								this.ShowErr("您需要等待一些时间, 待系统管理员审核您的帐户后才可登录使用");
							}
							else if (this.regconfig.regverify == 2)
							{
								this.ShowErr("请您到您的邮箱中点击激活链接来激活您的帐号");
							}
							else
							{
								this.ShowErr("抱歉, 您的用户身份尚未得到验证");
							}
						}
						else
						{
							WMSCookie.WriteUserCookie(userInfo, FPUtils.StrToInt(FPRequest.GetString("expires"), -1), this.sysconfig.passwordkey);
							this.Session["FP_OLUSERINFO"] = userInfo;
							this.Session["FP_ROLEINFO"] = userInfo.RoleInfo;
							this.Session["FP_PERMISSION"] = new PermissionBll().GetPermissionList(userInfo.RoleInfo.permission);
							SysBll.InsertLog(userInfo.id, userInfo.username, "用户登录", "登录用户：" + userInfo.username, true);
							base.AddMsg("登录成功, 返回登录前页面");
							base.SetMetaRefresh(2, this.reurl);
							if (!this.iscuserr)
							{
								base.Response.Redirect(this.reurl);
							}
						}
					}
					else
					{
						SysBll.InsertLog(userInfo.id, userInfo.username, "用户登录", "输入用户：" + @string + ",密码：" + string2, false);
						this.ShowErr("帐号或密码错误");
					}
				}
			}
		}

		// Token: 0x04000156 RID: 342
		protected internal string reurl = FPRequest.GetString("reurl");

		// Token: 0x04000157 RID: 343
		protected RegConfig regconfig = new RegConfig();
	}
}
