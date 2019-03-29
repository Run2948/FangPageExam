<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sysconfigmanage" %>
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
	ViewBuilder.Append("    <title>系统基础配置</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            PageNav(\"系统基础配置,global/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">系统基础配置</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 系统默认访问站点： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\">\r\n");
	ViewBuilder.Append("                        <select id=\"mainsite\" name=\"mainsite\">\r\n");
	ViewBuilder.Append("                            <option value=\"\">空白站点</option>\r\n");

	loop__id=0;
	foreach(SiteConfig sites in sitelist)
	{
	loop__id++;

	ViewBuilder.Append("                            <option value=\"" + sites.sitepath.ToString().Trim() + "\" \r\n");

	if (sites.sitepath==mainsite)
	{

	ViewBuilder.Append(" selected=\"\\\"selected\\\"\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + sites.name.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                            </option>\r\n");

	}	//end loop

	ViewBuilder.Append("                        </select>\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 登录在线过期时间： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"onlinetimeout\" type=\"text\" value=\"" + sysconfiginfo.onlinetimeout.ToString().Trim() + "\" id=\"onlinetimeout\" style=\"height:21px;width:400px;\">&nbsp;分钟</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 刷新用户在线频率： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"delonlinefrequency\" type=\"text\" value=\"" + sysconfiginfo.onlinefrequency.ToString().Trim() + "\" id=\"onlinefrequency\" style=\"height:21px;width:400px;\">&nbsp;分钟</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 计划任务执行频率： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"taskinterval\" type=\"text\" value=\"" + sysconfiginfo.taskinterval.ToString().Trim() + "\" id=\"taskinterval\" style=\"height:21px;width:400px;\">&nbsp;分钟，0不执行，需要重启程序才能生效</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 使用验证码的页面： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input name=\"verifypage\" type=\"text\" value=\"" + sysconfiginfo.verifypage.ToString().Trim() + "\" id=\"verifypage\" style=\"width:400px;\">&nbsp;多个页面请用英文的\"|\"号隔开\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 自定义错误的页面： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input name=\"customerrors\" type=\"text\" value=\"" + sysconfiginfo.customerrors.ToString().Trim() + "\" id=\"customerrors\" style=\"width:400px;\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 身份验证Cookie域： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input name=\"cookiedomain\" type=\"text\" value=\"" + sysconfiginfo.cookiedomain.ToString().Trim() + "\" id=\"cookiedomain\" style=\"width:400px;\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 系统后台目录名称： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"adminpath\" type=\"text\" value=\"" + sysconfiginfo.adminpath.ToString().Trim() + "\" id=\"adminpath\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 系统后台标题名称： </td>\r\n");
	ViewBuilder.Append("                    <td><input name=\"admintitle\" type=\"text\" value=\"" + sysconfiginfo.admintitle.ToString().Trim() + "\" id=\"admintitle\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 是否记录操作日志： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"allowlog\" name=\"allowlog\" type=\"radio\" \r\n");

	if (sysconfiginfo.allowlog==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">是<input id=\"allowlog\" name=\"allowlog\" type=\"radio\" \r\n");

	if (sysconfiginfo.allowlog==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"0\">否&nbsp;&nbsp;&nbsp;&nbsp;为了提高系统的安全，在站点稳定上线之后请开启。\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 浏览自动编译视图： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"browsecreatesite\" name=\"browsecreatesite\" type=\"radio\" \r\n");

	if (sysconfiginfo.browsecreatesite==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">是<input id=\"browsecreatesite\" name=\"browsecreatesite\" type=\"radio\" \r\n");

	if (sysconfiginfo.browsecreatesite==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"0\">否&nbsp;&nbsp;&nbsp;&nbsp;为了提高系统性能，在站点稳定上线之后请关闭。\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 系统详细错误提示： </td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input id=\"customerror\" name=\"customerror\" type=\"radio\" \r\n");

	if (customerror==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">开<input id=\"customerror\" name=\"customerror\" type=\"radio\" \r\n");

	if (customerror==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"0\">关&nbsp;&nbsp;&nbsp;&nbsp;为了提高系统的安全，在调试完之后请关闭详细错误提示。\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\"><input type=\"submit\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("      <br>\r\n");
	ViewBuilder.Append("    </div>\r\n");
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
