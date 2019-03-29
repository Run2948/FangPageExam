<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.emailconfigmanage" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：3.8.20*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("    <title>认证服务配置</title>\r\n");
	ViewBuilder.Append("" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("    <link href=\"../css/tab.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("                $('input[name=chkdel]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#btnsave\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"save\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#btnsend\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"send\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            PageNav(\"认证服务配置,global/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <input id=\"action\" name=\"action\" value=\"\" type=\"hidden\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul id=\"mytab1\">\r\n");
	ViewBuilder.Append("            <li class=\"active\"><a href=\"#\">邮箱服务器</a> </li>\r\n");
	ViewBuilder.Append("            <li class=\"normal\"><a href=\"#\">短信服务器</a> </li>\r\n");
	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">邮箱服务器配置</td>\r\n");
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
	ViewBuilder.Append("                    <td class=\"td_class\"> 邮箱服务器： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input name=\"smtp\" type=\"text\" value=\"" + emailconfig.smtp.ToString().Trim() + "\" id=\"smtp\" style=\"width:300px;\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 服务器端口： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                        <input name=\"port\" type=\"text\" value=\"" + emailconfig.port.ToString().Trim() + "\" id=\"port\" style=\"height:21px;width:300px;\">\r\n");
	ViewBuilder.Append("                        <input id=\"ssl\" name=\"ssl\" value=\"1\" \r\n");

	if (emailconfig.ssl==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">SSL加密\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 邮箱登录帐号： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"username\" type=\"text\" value=\"" + emailconfig.username.ToString().Trim() + "\" id=\"username\" style=\"height:21px;width:300px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 邮箱登录密码： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"password\" type=\"text\" value=\"" + emailconfig.password.ToString().Trim() + "\" id=\"password\" style=\"height:21px;width:300px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 发送人邮箱： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"sysemail\" name=\"sysemail\" type=\"text\" value=\"" + emailconfig.sysemail.ToString().Trim() + "\" style=\"height:21px;width:300px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 发送人姓名： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"fromname\" name=\"fromname\" type=\"text\" value=\"" + emailconfig.fromname.ToString().Trim() + "\" style=\"height:21px;width:300px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 测试发送地址： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"testmail\" name=\"testmail\" type=\"text\" value=\"\" style=\"height:21px;width:300px;\"></td>\r\n");
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
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");


	if (ispost)
	{

	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    layer.msg('" + msg.ToString() + "', 0, 1);\r\n");
	ViewBuilder.Append("    var bar = 0;\r\n");
	ViewBuilder.Append("    count();\r\n");
	ViewBuilder.Append("    function count() {\r\n");
	ViewBuilder.Append("        bar = bar + 4;\r\n");
	ViewBuilder.Append("        if (bar < 80) {\r\n");
	ViewBuilder.Append("            setTimeout(\"count()\", 100);\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        else {\r\n");
	ViewBuilder.Append("            window.location.href = \"" + link.ToString() + "\";\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");

	}	//end if



	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
