<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.usercheckinfo" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：4.5*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>用户实名认证审核</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"usercheckmanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"用户实名认证,user/usercheckmanage.aspx|用户实名认证审核," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"aspnetform\" method=\"post\" name=\"aspnetform\" action=\"\">\r\n");
	ViewBuilder.Append("  <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">用户实名认证审核</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">用户名： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 300px\" id=\"name\" name=\"name\" disabled=\"disabled\" value=\"" + userinfo.username.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">真实姓名： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 300px\" id=\"realname\" name=\"realname\" value=\"" + userinfo.realname.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">身份证号： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input style=\"width: 300px\" id=\"idcard\" name=\"idcard\" value=\"" + userinfo.idcard.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">性别： </td>\r\n");
	int sex = userinfo.sex;
	
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input type=\"radio\" id=\"sex\" name=\"sex\" value=\"0\" \r\n");

	if (sex==0)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">女\r\n");
	ViewBuilder.Append("              <input type=\"radio\" id=\"sex\" name=\"sex\" value=\"1\" \r\n");

	if (sex==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">男\r\n");
	ViewBuilder.Append("              <input type=\"radio\" id=\"sex\" name=\"sex\" value=\"-1\" \r\n");

	if (sex==-1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(">保密\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">身份证正面： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	if (userinfo.idcardface!="")
	{

	ViewBuilder.Append("              <img src=\"" + userinfo.idcardface.ToString().Trim() + "\" width=\"300\" height=\"200\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"../images/default.gif\" width=\"150\" height=\"150\">\r\n");

	}	//end if

	ViewBuilder.Append("<br>\r\n");
	ViewBuilder.Append("              <a href=\"" + userinfo.idcardface.ToString().Trim() + "\" target=\"_blank\">查看大图</a><br>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">身份证反面： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	if (userinfo.idcardback!="")
	{

	ViewBuilder.Append("              <img src=\"" + userinfo.idcardback.ToString().Trim() + "\" width=\"300\" height=\"200\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"../images/default.gif\" width=\"150\" height=\"150\">\r\n");

	}	//end if

	ViewBuilder.Append("<br>\r\n");
	ViewBuilder.Append("              <a href=\"" + userinfo.idcardback.ToString().Trim() + "\" target=\"_blank\">查看大图</a><br>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">手持身份证： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");

	if (userinfo.idcardper!="")
	{

	ViewBuilder.Append("              <img src=\"" + userinfo.idcardper.ToString().Trim() + "\" width=\"300\" height=\"200\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"../images/default.gif\" width=\"150\" height=\"150\">\r\n");

	}	//end if

	ViewBuilder.Append("<br>\r\n");
	ViewBuilder.Append("              <a href=\"" + userinfo.idcardper.ToString().Trim() + "\" target=\"_blank\">查看大图</a><br>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">审核状态： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("              <input id=\"isidcard\" name=\"isidcard\" \r\n");

	if (userinfo.isidcard==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\" type=\"radio\">审核通过\r\n");
	ViewBuilder.Append("              <input id=\"isidcard\" name=\"isidcard\" \r\n");

	if (userinfo.isidcard==-1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"-1\" type=\"radio\">审核未通过\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">审核备注： </td>\r\n");
	ViewBuilder.Append("          <td>\r\n");
	ViewBuilder.Append("             <textarea name=\"content\" rows=\"5\" cols=\"30\" id=\"content\" style=\"height:80px;width:200px;\">" + userinfo.content.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td><input name=\"submit\" value=\"保存\" type=\"submit\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\"></td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
