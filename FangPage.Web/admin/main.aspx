<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.main" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<%@ Import namespace="FangPage.Data" %>
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
	ViewBuilder.Append("    <title>后台桌面</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <meta content=\"IE=edge,chrome=1\" http-equiv=\"X-UA-Compatible\">\r\n");
	ViewBuilder.Append("    <link href=\"css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("    <link href=\"css/desktop.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            PageNav(\"\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        var _hmt = _hmt || [];\r\n");
	ViewBuilder.Append("        (function () {\r\n");
	ViewBuilder.Append("            var hm = document.createElement(\"script\");\r\n");
	ViewBuilder.Append("            hm.src = \"//hm.baidu.com/hm.js?35483845f92e384129fb5d03f9d7c3cf\";\r\n");
	ViewBuilder.Append("            var s = document.getElementsByTagName(\"script\")[0];\r\n");
	ViewBuilder.Append("            s.parentNode.insertBefore(hm, s);\r\n");
	ViewBuilder.Append("        })();\r\n");
	ViewBuilder.Append("   </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <noscript>\r\n");
	ViewBuilder.Append("        <iframe src=\"*\"></iframe>\r\n");
	ViewBuilder.Append("    </noscript>\r\n");
	ViewBuilder.Append("    <div class=\"main_main\">\r\n");
	ViewBuilder.Append("        <img src=\"images/systime.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\">\r\n");
	ViewBuilder.Append("        <span class=\"main_time\">服务器IP：" + serverip.ToString() + "，您的IP：" + ip.ToString() + "</span>\r\n");
	ViewBuilder.Append("        <img src=\"images/websize.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\">\r\n");
	ViewBuilder.Append("        <span class=\"main_time\">系统总大小：" + websize.ToString() + "</span>\r\n");
	ViewBuilder.Append("        <img src=\"images/database.png\" width=\"16\" height=\"16\" style=\"vertical-align:middle\">\r\n");
	ViewBuilder.Append("        <span class=\"main_time\">数据库类型："+ dbconfig.dbtype.ToString().Trim() + "，数据库大小：" + dbsize.ToString() + "</span>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"main_line\">\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <table cellspacing=\"0\" cellpadding=\"0\" width=\"100%\" height=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("                <td valign=\"top\">\r\n");
	ViewBuilder.Append("                    <div class=\"applist\">\r\n");
	ViewBuilder.Append("                        <ul>\r\n");

	loop__id=0;
	foreach(DesktopInfo desktop in desktoplist)
	{
	loop__id++;

	ViewBuilder.Append("                            <li><a href=\"javascript:void(0);\" title=\"" + desktop.name.ToString().Trim() + "\" onclick=\"OpenUrl('','" + desktop.lefturl.ToString().Trim() + "')\">\r\n");

	if (desktop.icon!="")
	{

	ViewBuilder.Append("                                <span style=\"background: url(" + desktop.icon.ToString().Trim() + ") 16px 5px no-repeat\">\r\n");

	}
	else
	{

	ViewBuilder.Append("                                <span style=\"background: url(images/desktop.gif) 16px 6px no-repeat\">\r\n");

	}	//end if

	ViewBuilder.Append("                                " + desktop.name.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                                </span></span></a>\r\n");
	ViewBuilder.Append("                            </li>\r\n");

	}	//end loop

	ViewBuilder.Append("                        </ul>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        function OpenUrl(lefturl,righturl) {\r\n");
	ViewBuilder.Append("            if (lefturl != ''){\r\n");
	ViewBuilder.Append("                window.parent.frames[\"leftframe\"].location = lefturl;\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            if (righturl != '') {\r\n");
	ViewBuilder.Append("                window.parent.frames[\"mainframe\"].location = righturl;\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
