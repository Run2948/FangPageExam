<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.top" %>
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
	ViewBuilder.Append("    <title>方配网站管理系统(WMS)</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <meta content=\"IE=edge,chrome=1\" http-equiv=\"X-UA-Compatible\">\r\n");
	ViewBuilder.Append("    <link href=\"css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        function ConfirmLogout() {\r\n");
	ViewBuilder.Append("            if (confirm('您确认要退出系统吗？'))\r\n");
	ViewBuilder.Append("                return true;\r\n");
	ViewBuilder.Append("            else\r\n");
	ViewBuilder.Append("                return false;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body oncontextmenu=\"return false\" ondragstart=\"return false\">\r\n");
	ViewBuilder.Append("    <div class=\"top\">\r\n");
	ViewBuilder.Append("        <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n");
	ViewBuilder.Append("            <tbody>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                    <td width=\"193\" valign=\"top\">\r\n");
	ViewBuilder.Append("                       <a href=\"http://www.fangpage.com\" target=\"_blank\"><img src=\"images/logo.gif\" width=\"193\" height=\"64\" border=\"0\" alt=\"方配软件技术有限公司(www.fangpage.com)\"></a>\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                        <div class=\"nav\">\r\n");
	ViewBuilder.Append("                            <ul class=\"TabBarLevel1\" id=\"TabPage1\">\r\n");

	loop__id=0;
	foreach(MenuInfo menu in menulist)
	{
	loop__id++;


	if (menu.id==1&&roleid==1)
	{

	ViewBuilder.Append("                                  <li id=\"Tab" + loop__id.ToString() + "\" \r\n");

	if (menu.id==topmenuid)
	{

	ViewBuilder.Append(" class=\"Selected\" \r\n");

	}	//end if

	ViewBuilder.Append("><a href=\"javascript:void();\" target=\"leftframe\" onclick=\"javascript:switchTab('TabPage1','Tab" + loop__id.ToString() + "','sysmenu.aspx?parentid=" + menu.id.ToString().Trim() + "&topmenuid=" + menu.id.ToString().Trim() + "','main.aspx');\">\r\n");
	ViewBuilder.Append("                                    " + menu.name.ToString().Trim() + "</a></li>\r\n");

	}
	else
	{


	if (ischecked(menu.id,role.menus))
	{


	if (menu.lefturl!="")
	{

	ViewBuilder.Append("                                    <li id=\"Tab" + loop__id.ToString() + "\" \r\n");

	if (menu.id==topmenuid)
	{

	ViewBuilder.Append(" class=\"Selected\" \r\n");

	}	//end if

	ViewBuilder.Append("><a href=\"javascript:void();\" target=\"leftframe\" onclick=\"javascript:switchTab('TabPage1','Tab" + loop__id.ToString() + "','" + menu.lefturl.ToString().Trim() + "','" + menu.righturl.ToString().Trim() + "');\">\r\n");
	ViewBuilder.Append("                                    " + menu.name.ToString().Trim() + "</a></li>\r\n");

	}
	else
	{

	ViewBuilder.Append("                                    <li id=\"Tab" + loop__id.ToString() + "\" \r\n");

	if (menu.id==topmenuid)
	{

	ViewBuilder.Append(" class=\"Selected\" \r\n");

	}	//end if

	ViewBuilder.Append("><a href=\"javascript:void();\" target=\"mainframe\" onclick=\"javascript:switchTab('TabPage1','Tab" + loop__id.ToString() + "','sysmenu.aspx?parentid=" + menu.id.ToString().Trim() + "&topmenuid=" + menu.id.ToString().Trim() + "','" + menu.righturl.ToString().Trim() + "');\">\r\n");
	ViewBuilder.Append("                                    " + menu.name.ToString().Trim() + "</a></li>\r\n");

	}	//end if


	}	//end if


	}	//end if


	}	//end loop

	ViewBuilder.Append("                            </ul>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("            </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <div class=\"righttop\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("                <li class=\"righttophelp\"><a href=\"http://wms.fangpage.com\" target=\"_blank\">\r\n");
	ViewBuilder.Append("                    系统帮助</a></li>\r\n");
	ViewBuilder.Append("                <li class=\"righttopexit\"><a href=\"logout.aspx\" title=\"退出系统管理\" onclick=\"return ConfirmLogout()\" target=\"_top\">\r\n");
	ViewBuilder.Append("                    退出</a></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"adminmenu\">\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        var now = new Date();\r\n");
	ViewBuilder.Append("        var hour = now.getHours();\r\n");
	ViewBuilder.Append("        if (hour < 12) {document.write(\"上午好！\")}\r\n");
	ViewBuilder.Append("        else if (hour < 14) {document.write(\"中午好！\")}\r\n");
	ViewBuilder.Append("        else if (hour < 18) {document.write(\"下午好！\")}\r\n");
	ViewBuilder.Append("        else {document.write(\"晚上好！\")}\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>" + username.ToString() + " \r\n");
	ViewBuilder.Append("        <em>|</em><img src=\"images/exit.png\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"logout.aspx\" title=\"退出系统管理\" onclick=\"return ConfirmLogout()\" target=\"_top\">退出</a> \r\n");
	ViewBuilder.Append("        <em>|</em><img src=\"images/users.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"user/userprofile.aspx\" target=\"mainframe\">个人资料</a> \r\n");
	ViewBuilder.Append("        <em>|</em><img src=\"images/quick.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"user/userpwd.aspx\" target=\"mainframe\">修改密码</a>\r\n");

	if (isperm)
	{

	ViewBuilder.Append("        <em>|</em><img src=\"images/more.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"global/sitemanage.aspx\" target=\"mainframe\">站点管理</a>\r\n");

	}	//end if

	ViewBuilder.Append("        <em>|</em><img src=\"images/bdesk.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"" + role.desktopurl.ToString().Trim() + "\" target=\"mainframe\">后台桌面</a> \r\n");
	ViewBuilder.Append("        <em>|</em><img src=\"images/wepsite.png\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"" + webpath.ToString() + "index.aspx\" target=\"_blank\">系统前台</a>\r\n");

	if (isperm)
	{

	ViewBuilder.Append("        <em>|</em><img src=\"images/fillup.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"global/sysupdate.aspx\" target=\"mainframe\">系统升级</a>\r\n");

	}	//end if

	ViewBuilder.Append("        <em>|</em><img src=\"images/website.gif\" width=\"16\" height=\"16\" style=\"vertical-align:middle\"><a href=\"http://www.fangpage.com\" target=\"_blank\">官方网站</a>\r\n");
	ViewBuilder.Append("        <em>|</em> 后台版本：V" + version.ToString() + "\r\n");
	ViewBuilder.Append("        <span style=\"float:right;height:20px;overflow:hidden;\" id=\"clock\">0</span>\r\n");
	ViewBuilder.Append("        <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("            // client time when loaded\r\n");
	ViewBuilder.Append("            var ldc = new Date();\r\n");
	ViewBuilder.Append("            // server time when loaded\r\n");
	ViewBuilder.Append("            var lds = new Date(i10(\"" + FangPage.MVC.FPUtils.GetDate(datetime,"yyyy") + "\"), i10(\"" + FangPage.MVC.FPUtils.GetDate(datetime,"MM") + "\") - 1, i10(\"" + FangPage.MVC.FPUtils.GetDate(datetime,"dd") + "\"), i10(\"" + FangPage.MVC.FPUtils.GetDate(datetime,"HH") + "\"), i10(\"" + FangPage.MVC.FPUtils.GetDate(datetime,"mm") + "\"), i10(\"" + FangPage.MVC.FPUtils.GetDate(datetime,"ss") + "\"));\r\n");
	ViewBuilder.Append("            // start clock\r\n");
	ViewBuilder.Append("            var clk = document.getElementById(\"clock\");\r\n");
	ViewBuilder.Append("            var d = new initArray(\r\n");
	ViewBuilder.Append("                \" 星期日\",\r\n");
	ViewBuilder.Append("                \" 星期一\",\r\n");
	ViewBuilder.Append("                \" 星期二\",\r\n");
	ViewBuilder.Append("                \" 星期三\",\r\n");
	ViewBuilder.Append("                \" 星期四\",\r\n");
	ViewBuilder.Append("                \" 星期五\",\r\n");
	ViewBuilder.Append("                \" 星期六\");\r\n");
	ViewBuilder.Append("            var strdate = \"<font style='font-size:9pt;font-family: 宋体'>" + FangPage.MVC.FPUtils.GetDate(datetime,"yyyy") + "年\" + (lds.getMonth() + 1) + \"月\" + lds.getDate() + \"日\" + d[lds.getDay() + 1] + \"</font> \";\r\n");
	ViewBuilder.Append("            tick();\r\n");
	ViewBuilder.Append("            function tick() {\r\n");
	ViewBuilder.Append("                if (clk && clk.innerHTML) {\r\n");
	ViewBuilder.Append("                    // current client time\r\n");
	ViewBuilder.Append("                    var cc = new Date();\r\n");
	ViewBuilder.Append("                    // current server time\r\n");
	ViewBuilder.Append("                    var cs = new Date(cc.getTime() - ldc.getTime() + lds.getTime());\r\n");
	ViewBuilder.Append("                    // set time\r\n");
	ViewBuilder.Append("                    clk.innerHTML = strdate + to2c(cs.getHours()) + ':' + to2c(cs.getMinutes()) + ':' + to2c(cs.getSeconds())+'&nbsp;&nbsp;';\r\n");
	ViewBuilder.Append("                    // set next tick\r\n");
	ViewBuilder.Append("                    setTimeout(tick, 1000);\r\n");
	ViewBuilder.Append("                }\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            function to2c(n) {\r\n");
	ViewBuilder.Append("                return (n < 10 ? '0' + n : '' + n);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            function i10(s) {\r\n");
	ViewBuilder.Append("                return parseInt(s, 10);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            function initArray() {\r\n");
	ViewBuilder.Append("                this.length = initArray.arguments.length\r\n");
	ViewBuilder.Append("                for (var i = 0; i < this.length; i++)\r\n");
	ViewBuilder.Append("                    this[i + 1] = initArray.arguments[i]\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"logionout\">\r\n");
	ViewBuilder.Append("        <div id=\"topleft\" class=\"adminleft\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("                <li id=\"leftnav\"><a href=\"global/sysmenumanage.aspx\" target=\"mainframe\">菜单管理</a></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div id=\"pagenav\" class=\"adminright\">\r\n");
	ViewBuilder.Append("            <a href=\"main.aspx\" target=\"mainframe\" class=\"daohanglink\">后台桌面</a>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        function switchTab(tabpage, tabid,lefturl,righturl) {\r\n");
	ViewBuilder.Append("            $(\"#TabPage1 li\").removeClass('Selected');\r\n");
	ViewBuilder.Append("            $(\"#TabPage1\").find(\"#\" + tabid).addClass('Selected');\r\n");
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
