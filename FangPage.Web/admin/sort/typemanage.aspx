<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.typemanage" %>
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
	ViewBuilder.Append("<title>信息分类管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btnsave\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"order\");\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"typemanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"信息分类管理," + rawpath.ToString() + "typemanage.aspx\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("    function DeleteType(id) {\r\n");
	ViewBuilder.Append("        if (confirm(\"你确定要删除吗？删除后将无法进行恢复。\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("            $(\"#id\").val(id);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form id=\"formpost\" method=\"post\" name=\"formpost\" action=\"\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"id\" id=\"id\" value=\"\"> \r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("    <div class=\"newslist\">\r\n");
	ViewBuilder.Append("      <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("        <ul>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"typeadd.aspx\">添加</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"typemanage.aspx\">刷新</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"float:right; width:auto\"><strong>信息分类管理</strong></li>\r\n");
	ViewBuilder.Append("        </ul>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("   <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("        <tr class=\"thead\">\r\n");
	ViewBuilder.Append("            <td width=\"60\">分类ID</td>\r\n");
	ViewBuilder.Append("            <td>分类名称</td>\r\n");
	ViewBuilder.Append("            <td>分类标识</td>\r\n");
	ViewBuilder.Append("            <td>描述</td>\r\n");
	ViewBuilder.Append("            <td width=\"80\">添加子分类</td>\r\n");
	ViewBuilder.Append("            <td width=\"40\">编辑</td>\r\n");
	ViewBuilder.Append("            <td width=\"40\">删除</td>\r\n");
	ViewBuilder.Append("            <td width=\"40\">排序</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	loop__id=0;
	foreach(TypeInfo types in typelist)
	{
	loop__id++;

	ViewBuilder.Append("        <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                " + types.id.ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td align=\"left\">├\r\n");

	if (types.subcounts>0)
	{

	ViewBuilder.Append("                <img src=\"../images/types.gif\" width=\"16\" height=\"16\">\r\n");

	}
	else
	{

	ViewBuilder.Append("                <img src=\"../images/type.gif\" width=\"16\" height=\"16\">\r\n");

	}	//end if

	ViewBuilder.Append("                " + types.name.ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                " + types.markup.ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                " + types.description.ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");

	if (types.parentid==0)
	{

	ViewBuilder.Append("            <a href=\"typeadd.aspx?parentid=" + types.id.ToString().Trim() + "\">添加子分类</a>\r\n");

	}	//end if

	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td><a style=\"color: #1317fc\" href=\"typeadd.aspx?id=" + types.id.ToString().Trim() + "\">编辑</a> </td>\r\n");
	ViewBuilder.Append("            <td><a style=\"color: #1317fc\" href=\"typedisplay.aspx?parentid=" + types.parentid.ToString().Trim() + "\">排序</a></td>\r\n");
	ViewBuilder.Append("            <td><a onclick=\"DeleteType(" + types.id.ToString().Trim() + ")\" href=\"#\">删除</a></td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	loop__id=0;
	foreach(TypeInfo childtype in GetChildType(types.id))
	{
	loop__id++;

	ViewBuilder.Append("        <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("        <td>" + childtype.id.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("        <td align=\"left\">│&nbsp;&nbsp;├\r\n");

	if (childtype.subcounts>0)
	{

	ViewBuilder.Append("            <img src=\"../images/types.gif\" width=\"16\" height=\"16\">\r\n");

	}
	else
	{

	ViewBuilder.Append("            <img src=\"../images/type.gif\" width=\"16\" height=\"16\">\r\n");

	}	//end if

	ViewBuilder.Append("            " + childtype.name.ToString().Trim() + "\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        <td>" + childtype.description.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("        <td></td>\r\n");
	ViewBuilder.Append("        <td><a style=\"color: #1317fc\" href=\"typeadd.aspx?id=" + childtype.id.ToString().Trim() + "\">编辑</a> </td>\r\n");
	ViewBuilder.Append("        <td><a style=\"color: #1317fc\" href=\"typedisplay.aspx?parentid=" + childtype.parentid.ToString().Trim() + "\">排序</a></td>\r\n");
	ViewBuilder.Append("        <td><a onclick=\"DeleteType(" + childtype.id.ToString().Trim() + ")\" href=\"#\">删除</a></td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	}	//end loop


	}	//end loop

	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
