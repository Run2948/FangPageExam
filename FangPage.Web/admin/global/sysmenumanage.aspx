<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sysmenumanage" %>
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
	ViewBuilder.Append("<title>系统菜单管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
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
	ViewBuilder.Append("                $(\"#formpost\").submit(); \r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitdesk\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"desk\");\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统菜单管理,global/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"formpost\" method=\"post\" name=\"formpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td><div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"sysmenuadd.aspx\">添加</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"sysmenumanage.aspx\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/adddesk.gif) 2px 6px no-repeat\"><a id=\"submitdesk\" href=\"#\">创建桌面快捷</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>系统菜单管理</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td><table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"40\"><input id=\"checkall\" name=\"checkall\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td>菜单名称</td>\r\n");
	ViewBuilder.Append("              <td width=\"150\">添加子菜单</td>\r\n");
	ViewBuilder.Append("              <td width=\"100\">编辑</td>\r\n");
	ViewBuilder.Append("              <td width=\"100\">排序</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(MenuInfo menu in menulist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + menu.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td align=\"left\">├<img src=\"../images/sysmenu1.gif\" width=\"16\" height=\"16\">" + menu.name.ToString().Trim() + " </td>\r\n");
	ViewBuilder.Append("              <td><a style=\"color: #1317fc\" href=\"sysmenuadd.aspx?parentid=" + menu.id.ToString().Trim() + "\">添加子菜单</a></td>\r\n");
	ViewBuilder.Append("              <td><a style=\"color: #1317fc\" href=\"sysmenuadd.aspx?id=" + menu.id.ToString().Trim() + "\">编辑</a></td>\r\n");
	ViewBuilder.Append("              <td><a style=\"color: #1317fc\" href=\"sysmenudisplay.aspx?parentid=" + menu.parentid.ToString().Trim() + "\">排序</a></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(MenuInfo childmenu in GetChildMenu(menu.id))
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("               <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + childmenu.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("               <td align=\"left\">│&nbsp;&nbsp;├<img src=\"../images/sysmenu2.gif\" width=\"16\" height=\"16\">" + childmenu.name.ToString().Trim() + " </td>\r\n");
	ViewBuilder.Append("               <td><a style=\"color: #1317fc\" href=\"sysmenuadd.aspx?parentid=" + childmenu.id.ToString().Trim() + "\">添加子菜单</a></td>\r\n");
	ViewBuilder.Append("               <td><a style=\"color: #1317fc\" href=\"sysmenuadd.aspx?id=" + childmenu.id.ToString().Trim() + "\">编辑</a></td>\r\n");
	ViewBuilder.Append("               <td><a style=\"color: #1317fc\" href=\"sysmenudisplay.aspx?parentid=" + childmenu.parentid.ToString().Trim() + "\">排序</a></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(MenuInfo childmenu2 in GetChildMenu(childmenu.id))
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("               <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + childmenu2.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("               <td align=\"left\">│&nbsp;&nbsp;│&nbsp;&nbsp;├<img src=\"../images/sysmenu3.gif\" width=\"16\" height=\"16\">" + childmenu2.name.ToString().Trim() + " </td>\r\n");
	ViewBuilder.Append("               <td></td>\r\n");
	ViewBuilder.Append("               <td><a style=\"color: #1317fc\" href=\"sysmenuadd.aspx?id=" + childmenu2.id.ToString().Trim() + "\">编辑</a></td>\r\n");
	ViewBuilder.Append("               <td><a style=\"color: #1317fc\" href=\"sysmenudisplay.aspx?parentid=" + childmenu2.parentid.ToString().Trim() + "\">排序</a></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop


	}	//end loop


	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
