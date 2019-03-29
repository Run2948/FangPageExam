<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sortmanage" %>
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
	ViewBuilder.Append("<title>站点栏目管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/tab.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    function DeleteSort(id)\r\n");
	ViewBuilder.Append("    {\r\n");
	ViewBuilder.Append("        if (confirm(\"你确定要删除吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("            $(\"#id\").val(id);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function DeleteType(id) {\r\n");
	ViewBuilder.Append("        if (confirm(\"你确定要去除该栏目的分类吗？\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"deltype\");\r\n");
	ViewBuilder.Append("            $(\"#id\").val(id);\r\n");
	ViewBuilder.Append("            $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    PageNav(\"站点栏目管理," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"formpost\" method=\"post\" name=\"formpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" id=\"id\" name=\"id\" value=\"\">\r\n");
	ViewBuilder.Append("  <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul id=\"mytab1\">\r\n");

	loop__id=0;
	foreach(ChannelInfo channel in channellist)
	{
	loop__id++;


	if (channel.id==channelid)
	{

	ViewBuilder.Append("            <li class=\"active\"><a href=\"sortmanage.aspx?channelid=" + channel.id.ToString().Trim() + "\">" + channel.name.ToString().Trim() + "</a> </li>\r\n");

	}
	else
	{

	ViewBuilder.Append("            <li class=\"normal\"><a href=\"sortmanage.aspx?channelid=" + channel.id.ToString().Trim() + "\">" + channel.name.ToString().Trim() + "</a> </li>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\" style=\"background-color:white;width:100%\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td>\r\n");
	ViewBuilder.Append("       <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/add.gif) 2px 6px no-repeat\"><a href=\"sortadd.aspx?channelid=" + channelid.ToString() + "\">添加</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"sortmanage.aspx?channelid=" + channelid.ToString() + "\">刷新</a> </li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>站点栏目管理</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("       </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td>\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"60\">栏目ID</td>\r\n");
	ViewBuilder.Append("        	  <td>栏目名称</td>\r\n");
	ViewBuilder.Append("              <td width=\"80\">栏目标识</td>\r\n");
	ViewBuilder.Append("              <td width=\"80\">栏目应用</td>\r\n");
	ViewBuilder.Append("        	  <td width=\"80\">添加子栏目</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">编辑</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">删除</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">排序</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	string tree = "├";
	

	loop__id=0;
	foreach(SortInfo sorts in sortlist)
	{
	loop__id++;

	string hidden = "";
	

	if (sorts.hidden==1)
	{

	 hidden = "_hidden";
	

	}	//end if

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + sorts.id.ToString().Trim() + " </td>\r\n");
	ViewBuilder.Append("              <td align=\"left\">├\r\n");

	if (sorts.icon=="")
	{


	if (sorts.subcounts>0)
	{

	 sorts.icon = "../images/folders"+hidden+".gif";
	

	}
	else
	{

	 sorts.icon = "../images/folder"+hidden+".gif";
	

	}	//end if


	}	//end if


	if (sorts.subcounts>0)
	{

	ViewBuilder.Append("              <img src=\"" + sorts.icon.ToString().Trim() + "\" width=\"16\" height=\"16\"><span style=\"font-weight:bold;\">" + sorts.name.ToString().Trim() + "</span>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"" + sorts.icon.ToString().Trim() + "\" width=\"16\" height=\"16\">" + sorts.name.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("                  " + sorts.markup.ToString().Trim() + "\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");

	if (sorts.SortAppInfo.name!="")
	{

	ViewBuilder.Append("              " + sorts.SortAppInfo.name.ToString().Trim() + "\r\n");

	}
	else
	{

	ViewBuilder.Append("              无\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td><a href=\"sortadd.aspx?channelid=" + channelid.ToString() + "&parentid=" + sorts.id.ToString().Trim() + "\">添加子栏目</a></td>\r\n");
	ViewBuilder.Append("              <td><a style=\"color: #1317fc\" href=\"sortadd.aspx?channelid=" + channelid.ToString() + "&id=" + sorts.id.ToString().Trim() + "\">编辑</a> </td>\r\n");
	ViewBuilder.Append("              <td><a onclick=\"DeleteSort(" + sorts.id.ToString().Trim() + ")\" href=\"#\">删除</a></td>\r\n");
	ViewBuilder.Append("              <td><a style=\"color: #1317fc\" href=\"sortdisplay.aspx?channelid=" + channelid.ToString() + "&parentid=" + sorts.parentid.ToString().Trim() + "\">排序</a></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            " + ShowChildSort(sorts.id,tree).ToString() + "\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
