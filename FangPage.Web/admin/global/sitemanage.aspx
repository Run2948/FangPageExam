<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sitemanage" %>
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
	ViewBuilder.Append("<title>系统站点管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("            $('input[name=chkdel]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitdel\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"你确定要删除吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("                $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统站点管理," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("    function CreateSite(pathname) {\r\n");
	ViewBuilder.Append("        if (confirm(\"您确定要编译[\" + pathname + \"]站点吗？\\r\\n当前操作可能要运行一段时间，在此期间不要进行其它操作。\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"create\");\r\n");
	ViewBuilder.Append("            $(\"#sitepath\").val(pathname);\r\n");
	ViewBuilder.Append("            layer.load('正在编译中…',2);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function DownLoadSite(pathname) {\r\n");
	ViewBuilder.Append("        if (confirm(\"您确定要下载\" + pathname + \"站点吗？\\r\\n当前操作可能要运行一段时间，在此期间不要进行其它操作。\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"download\");\r\n");
	ViewBuilder.Append("            $(\"#sitepath\").val(pathname);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form id=\"formpost\" method=\"post\" name=\"formpost\" action=\"\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"sitepath\" id=\"sitepath\" value=\"\"> \r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("    <div class=\"newslist\">\r\n");
	ViewBuilder.Append("        <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("                <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("                <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"siteadd.aspx\">新建</a></li>\r\n");
	ViewBuilder.Append("                <li style=\"background: url(../images/wepsite.png) 2px 6px no-repeat\"><a href=\"siteinstall.aspx\">安装</a></li>\r\n");
	ViewBuilder.Append("                <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"sitemanage.aspx\">刷新</a></li>\r\n");
	ViewBuilder.Append("                <li style=\"background: url(../images/more.gif) 2px 6px no-repeat\"><a href=\"http://website.fangpage.com\" target=\"_blank\">更多站点下载</a></li>\r\n");
	ViewBuilder.Append("                <li style=\"float:right; width:auto\"><strong>系统站点管理</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </td></tr>\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("   <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("                <td width=\"40\">\r\n");
	ViewBuilder.Append("                    <input id=\"checkall\" name=\"checkall\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  站点名称\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  站点目录\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  站点作者\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  创建日期\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  站点版本\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  站点状态\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td width=\"150\">\r\n");
	ViewBuilder.Append("                   操作\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(SiteConfig site in sitelist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input id=\"chkdel\" name=\"chkdel\" value=\"" + site.sitepath.ToString().Trim() + "\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   <a href=\"" + webpath.ToString() + "" + site.sitepath.ToString().Trim() + "/\" target=\"_blank\">" + site.name.ToString().Trim() + "</a>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + site.sitepath.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + site.author.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                   " + site.createdate.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                " + site.version.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");

	if (site.closed==0)
	{

	ViewBuilder.Append("                运行中\r\n");

	}
	else
	{

	ViewBuilder.Append("                关闭\r\n");

	}	//end if

	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                  <a href=\"siteadd.aspx?sitepath=" + site.sitepath.ToString().Trim() + "\">配置</a>\r\n");
	ViewBuilder.Append("                  <a href=\"sitefilemanage.aspx?sitepath=" + site.sitepath.ToString().Trim() + "\">管理</a>\r\n");
	ViewBuilder.Append("                  <a href=\"javascript:CreateSite('" + site.sitepath.ToString().Trim() + "')\">编译</a>\r\n");
	ViewBuilder.Append("                  <a href=\"javascript:DownLoadSite('" + site.sitepath.ToString().Trim() + "')\">下载</a>\r\n");
	ViewBuilder.Append("                  <a href=\"siteupdate.aspx?sitepath=" + site.sitepath.ToString().Trim() + "\">更新</a>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </form>\r\n");

	if (action=="create")
	{



	if (ispost)
	{

	ViewBuilder.Append("   <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("       layer.msg('" + msg.ToString() + "', 2, 1);\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");

	}	//end if




	}	//end if

	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
