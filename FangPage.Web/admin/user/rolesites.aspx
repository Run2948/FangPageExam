<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.rolesites" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：4.1.1*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("    <title>桌面权限设置</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("    <link href=\"../css/tab.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("                PageBack(\"rolemanage.aspx\");\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            PageNav(\"用户角色管理,user/rolemanage.aspx|角色权限设置,user/" + pagename.ToString() + "?rid=" + rid.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form id=\"formpost\" method=\"post\" name=\"formpost\" action=\"\">\r\n");
	ViewBuilder.Append("        <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("            <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("                <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("                    <ul>\r\n");
	ViewBuilder.Append("                        <li class=\"normal\"><a href=\"rolesorts.aspx?rid=" + rid.ToString() + "\">栏目权限</a> </li>\r\n");
	ViewBuilder.Append("                        <li class=\"normal\"><a href=\"rolemenus.aspx?rid=" + rid.ToString() + "\">菜单权限</a></li>\r\n");
	ViewBuilder.Append("                        <li class=\"normal\"><a href=\"roledesktop.aspx?rid=" + rid.ToString() + "\">桌面权限</a></li>\r\n");
	ViewBuilder.Append("                        <li class=\"normal\"><a href=\"rolepermission.aspx?rid=" + rid.ToString() + "\">访问权限</a></li>\r\n");
	ViewBuilder.Append("                        <li class=\"active\"><a href=\"rolesites.aspx?rid=" + rid.ToString() + "\">站点权限</a></li>\r\n");
	ViewBuilder.Append("                    </ul>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div id=\"con_one_1\">\r\n");
	ViewBuilder.Append("            <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"newstitle\" bgcolor=\"#ffffff\">站点权限设置，角色：【" + roleinfo.name.ToString().Trim() + "】</td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("            <table style=\"width: 100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td colspan=\"2\">\r\n");
	ViewBuilder.Append("                         <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                         <tr>\r\n");

	loop__id=0;
	foreach(SiteConfig item in sitelist)
	{
	loop__id++;


	if (ischecked(item.sitepath,roleinfo.sites))
	{

	ViewBuilder.Append("                                <td><input id=\"sites\" name=\"sites\" value=\"" + item.sitepath.ToString().Trim() + "\" type=\"checkbox\" checked=\"checked\">" + item.name.ToString().Trim() + "</td>\r\n");

	}
	else
	{

	ViewBuilder.Append("                                <td><input id=\"sites\" name=\"sites\" value=\"" + item.sitepath.ToString().Trim() + "\" type=\"checkbox\">" + item.name.ToString().Trim() + "</td>\r\n");

	}	//end if


	if (loop__id%4==0)
	{

	ViewBuilder.Append("                                </tr>\r\n");
	ViewBuilder.Append("                                <tr>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                         </tr>\r\n");
	ViewBuilder.Append("                         </table>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                        <td>\r\n");
	ViewBuilder.Append("                            <input type=\"submit\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                            <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
