<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.pluginmanage" %>
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
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>系统插件管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    function DeleteApp(pluname) {\r\n");
	ViewBuilder.Append("        if (confirm(\"你确定要删除该插件吗？删除可能会导致一些程序无法正常使用。\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("            $(\"#pluname\").val(pluname);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function DownLoadApp(pluname) {\r\n");
	ViewBuilder.Append("        if (confirm(\"您确定要下载[\" + pluname + \"]该应用吗？\\r\\n\\r\\n当前操作可能要运行一段时间，在此期间不要进行其它操作。\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"download\");\r\n");
	ViewBuilder.Append("            $(\"#pluname\").val(pluname);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    PageNav(\"系统插件管理," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form id=\"formpost\" method=\"post\" name=\"formpost\" action=\"\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\"> \r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"pluname\" id=\"pluname\" value=\"\"> \r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("    <td>\r\n");
	ViewBuilder.Append("    <div class=\"newslist\">\r\n");
	ViewBuilder.Append("       <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("        <ul>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/newapp.gif) 2px 6px no-repeat\"><a href=\"pluginadd.aspx\">新建</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"plugininstall.aspx\">安装</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"pluginmanage.aspx\">刷新</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/more.gif) 2px 6px no-repeat\"><a href=\"http://appstore.fangpage.com\" target=\"_blank\">更多插件下载</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"float:right; width:auto\"><strong>系统插件管理</strong></li>\r\n");
	ViewBuilder.Append("        </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("   <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  插件名称\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  插件目录\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  插件作者\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  插件版本\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  插件备注\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td width=\"150\">\r\n");
	ViewBuilder.Append("                  操作\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(PluginConfig plu in pluginlist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("                <td>\r\n");

	if (plu.frontpage!="")
	{


	if (plu.frontpage.StartsWith("http://"))
	{

	ViewBuilder.Append("                      <a href=\"" + plu.frontpage.ToString().Trim() + "\" target=\"_blank\">" + plu.name.ToString().Trim() + "</a>\r\n");

	}
	else
	{

	ViewBuilder.Append("                      <a href=\"" + webpath.ToString() + "" + plu.installpath.ToString().Trim() + "/" + plu.frontpage.ToString().Trim() + "\" target=\"_blank\">" + plu.name.ToString().Trim() + "</a>\r\n");

	}	//end if


	}
	else
	{

	ViewBuilder.Append("                   " + plu.name.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + plu.installpath.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + plu.author.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + plu.version.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + plu.notes.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  <a href=\"pluginadd.aspx?pluname=" + plu.installpath.ToString().Trim() + "\">编辑</a>\r\n");
	ViewBuilder.Append("                  <a href=\"pluginupdate.aspx?pluname=" + plu.installpath.ToString().Trim() + "\">更新</a>\r\n");
	ViewBuilder.Append("                  <a href=\"javascript:DeleteApp('" + plu.installpath.ToString().Trim() + "')\">卸载</a>\r\n");
	ViewBuilder.Append("                  <a href=\"javascript:DownLoadApp('" + plu.installpath.ToString().Trim() + "')\">下载</a>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </td></tr>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
