<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.usersearch" %>
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
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("<title>系统用户搜索</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            PageBack(\"usermanage.aspx\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统用户管理,user/usermanage.aspx|用户搜索,user/usersearch.aspx\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"get\" action=\"usermanage.aspx\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">用户搜索</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("             <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                  <td class=\"td_class\">所在角色： </td>\r\n");
	ViewBuilder.Append("                  <td>\r\n");
	ViewBuilder.Append("                      <select id=\"s_roleid\" name=\"s_roleid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                        <option value=\"0\">--选择角色--</option>\r\n");

	loop__id=0;
	foreach(RoleInfo role in rolelist)
	{
	loop__id++;


	if (role.id!=3)
	{

	ViewBuilder.Append("                            <option value=\"" + role.id.ToString().Trim() + "\" \r\n");

	if (role.id==s_roleid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + role.name.ToString().Trim() + "</option>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                      </select>\r\n");
	ViewBuilder.Append("                  </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                  <td class=\"td_class\">所在部门： </td>\r\n");
	ViewBuilder.Append("                  <td>\r\n");
	string tree = "├";
	
	ViewBuilder.Append("                      <select id=\"s_departid\" name=\"s_departid\" style=\"width: 204px\">\r\n");
	ViewBuilder.Append("                        <option value=\"0\">--选择部门--</option>\r\n");

	loop__id=0;
	foreach(Department item in deparlist)
	{
	loop__id++;

	ViewBuilder.Append("                        <option value=\"" + item.id.ToString().Trim() + "\" \r\n");

	if (item.id==s_departid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">├" + item.name.ToString().Trim() + "</option>\r\n");
	ViewBuilder.Append("                        " + GetChildDepartment(item.id,tree).ToString() + "\r\n");

	}	//end loop

	ViewBuilder.Append("                      </select>\r\n");
	ViewBuilder.Append("                  </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                <td class=\"td_class\">查询关键词： </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input style=\"width: 200px\" id=\"keyword\" name=\"keyword\" value=\"" + keyword.ToString() + "\">\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                <td class=\"td_class\">查询字段： </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input id=\"s_username\" name=\"s_username\" type=\"checkbox\" \r\n");

	if (s_username==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">用户名\r\n");
	ViewBuilder.Append("                    <input id=\"s_realname\" name=\"s_realname\" type=\"checkbox\" \r\n");

	if (s_realname==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">真实姓名\r\n");
	ViewBuilder.Append("                    <input id=\"s_mobile\" name=\"s_mobile\" type=\"checkbox\" \r\n");

	if (s_mobile==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">手机号码\r\n");
	ViewBuilder.Append("                    <input id=\"s_email\" name=\"s_email\" type=\"checkbox\" \r\n");

	if (s_email==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\">电子邮箱\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                <td height=\"25\">\r\n");
	ViewBuilder.Append("                <input type=\"submit\" name=\"btnsave\" value=\"搜索\" id=\"btnsave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
