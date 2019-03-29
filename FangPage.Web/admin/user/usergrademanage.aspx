<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.usergrademanage" %>
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
	ViewBuilder.Append("<title>用户级别管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"../css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("            $('input[name=chkdel]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitdel\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"您确定要删除吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("                $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitreset\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"您确定要重置用户等级吗？\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"reset\");\r\n");
	ViewBuilder.Append("                $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"用户级别管理,user/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form id=\"formpost\" name=\"formpost\" action=\"\" method=\"post\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" id=\"action\" name=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td><div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"usergradeadd.aspx\">添加</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/users.gif) 2px 6px no-repeat\"><a id=\"submitreset\" href=\"#\">重置用户等级</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"usergrademanage.aspx\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>用户级别管理</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td><table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"40\"><input id=\"checkall\" name=\"checkall\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td> 编号 </td>\r\n");
	ViewBuilder.Append("              <td> 用户等级 </td>\r\n");
	ViewBuilder.Append("              <td> 经验值下限 </td>\r\n");
	ViewBuilder.Append("              <td> 经验值上限 </td>\r\n");
	ViewBuilder.Append("              <td> 星星数 </td>\r\n");
	ViewBuilder.Append("              <td> 描述 </td>\r\n");
	ViewBuilder.Append("              <td width=\"60\"> 编辑 </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(UserGrade grade in gradelist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + grade.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td>" + grade.id.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>" + grade.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>" + grade.explower.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>" + grade.expupper.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>" + grade.stars.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>" + grade.description.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td><a style=\"color: #1317fc\" href=\"usergradeadd.aspx?id=" + grade.id.ToString().Trim() + "\">编辑</a></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("   </table>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
