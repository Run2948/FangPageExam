<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.smsconfigmanage" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：4.5*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("    <title>短信认证服务器配置</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("    <link href=\"../css/tab.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#btnsave\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"save\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#btnsend\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"send\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#getbalance\").click(function () {\r\n");
	ViewBuilder.Append("                $.get(\"smsbalance.aspx\", {\r\n");
	ViewBuilder.Append("                    account: $(\"#account\").val(),\r\n");
	ViewBuilder.Append("                    password: $(\"#password\").val()\r\n");
	ViewBuilder.Append("                }, function (data) {\r\n");
	ViewBuilder.Append("                    $(\"#balance\").html($(data).find(\"string\").text());\r\n");
	ViewBuilder.Append("                }, \"xml\");\r\n");
	ViewBuilder.Append("            }).trigger(\"click\");\r\n");
	ViewBuilder.Append("            PageNav(\"短信认证配置," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <input id=\"action\" name=\"action\" value=\"\" type=\"hidden\">\r\n");
	ViewBuilder.Append("      <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul id=\"mytab1\">\r\n");
	ViewBuilder.Append("            <li class=\"active\"><a href=\"smsconfigmanage.aspx\">短信服务器</a> </li>\r\n");
	ViewBuilder.Append("            <li class=\"normal\"><a href=\"msgtemplatemanage.aspx?type=2\">短信模板管理</a> </li>\r\n");
	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">短信认证服务器配置</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("            <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 用户账号： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                        <input name=\"account\" type=\"text\" value=\"" + smsconfig.account.ToString().Trim() + "\" id=\"account\" style=\"height:21px;width:300px;\">\r\n");
	ViewBuilder.Append("                        &nbsp;<a href=\"http://www.dxton.com/jiekou/#_case\" target=\"_blank\">注册帐号</a>  \r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 接口密码： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"password\" type=\"password\" value=\"" + smsconfig.password.ToString().Trim() + "\" id=\"password\" style=\"height:21px;width:300px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 帐户余额： </td>\r\n");
	ViewBuilder.Append("                    <td>￥<span id=\"balance\">0</span>元&nbsp;&nbsp;&nbsp;<a id=\"getbalance\" href=\"#\">刷新余额</a>&nbsp;&nbsp;<a href=\"http://www.dxton.com\" target=\"_blank\">帐户充值</a>&nbsp;&nbsp;<a href=\"http://www.dxton.com\" target=\"_blank\">服务商首页</a></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 测试发送： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"phone\" name=\"phone\" type=\"text\" value=\"" + phone.ToString() + "\" style=\"height:21px;width:300px;\">&nbsp;输入接收测试短信的手机号码</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\">\r\n");
	ViewBuilder.Append("                    <input type=\"button\" name=\"btnsave\" value=\"保存\" id=\"btnsave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    <input type=\"button\" name=\"btnsend\" value=\"测试发送\" id=\"btnsend\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table></td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("      <br>\r\n");
	ViewBuilder.Append("</form>\r\n");


	if (ispost)
	{

	ViewBuilder.Append("   <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("       layer.msg('" + msg.ToString() + "', 2, 1);\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");

	}	//end if



	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
