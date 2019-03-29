<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sitefilemanage" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<%@ Import namespace="System.Data" %>
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
	ViewBuilder.Append("<title>站点文件管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/datagrid.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("            $('input[name=fileid]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitdel\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"你确定要删除吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("                $(\"#formpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统站点管理," + rawpath.ToString() + "sitemanage.aspx|站点文件管理," + rawpath.ToString() + "" + pagename.ToString() + "?sitepath=" + m_sitepath.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
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
	ViewBuilder.Append("       <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("        <ul>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"sitefilemanage.aspx?sitepath=" + m_sitepath.ToString() + "\">刷新</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"background: url(../images/return.gif) 2px 6px no-repeat\"><a href=\"" + reurl.ToString() + "\">返回</a></li>\r\n");
	ViewBuilder.Append("            <li style=\"float:right; width:auto\"><strong>站点文件管理</strong></li>\r\n");
	ViewBuilder.Append("        </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr><td>\r\n");
	ViewBuilder.Append("   <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("        <tr class=\"thead\">\r\n");
	ViewBuilder.Append("            <td width=\"40\">\r\n");
	ViewBuilder.Append("                <input id=\"checkall\" name=\"checkall\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                文件名称\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                修改时间\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                文件类型\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                文件大小\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td width=\"100\">\r\n");
	ViewBuilder.Append("                操作\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	loop__id=0;
	foreach(DataRow files in filelist.Rows)
	{
	loop__id++;

	ViewBuilder.Append("        <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                <input id=\"fileid\" name=\"fileid\" value=\"" + files["id"].ToString().Trim() + "\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                <input id=\"type" + files["id"].ToString().Trim() + "\" name=\"type" + files["id"].ToString().Trim() + "\" value=\"" + files["type"].ToString().Trim() + "\" type=\"hidden\">\r\n");
	ViewBuilder.Append("                <input id=\"file" + files["id"].ToString().Trim() + "\" name=\"file" + files["id"].ToString().Trim() + "\" value=\"" + files["name"].ToString().Trim() + "\" type=\"hidden\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td valign=\"middle\" align=\"left\">\r\n");
	ViewBuilder.Append("                <img src=\"" + files["icon"].ToString().Trim() + "\" align=\"middle\">\r\n");

	if (files["type"].ToString()=="文件夹")
	{

	ViewBuilder.Append("                <a href=\"sitefilemanage.aspx?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "" + files["name"].ToString().Trim() + "\">" + files["name"].ToString().Trim() + "</a>\r\n");

	}
	else if (files["type"].ToString()=="xml"||files["type"].ToString()=="htm"||files["type"].ToString()=="html"||files["type"].ToString()=="css"||files["type"].ToString()=="js"||files["type"].ToString()=="txt"||files["type"].ToString()=="aspx"||files["type"].ToString()=="ascx"||files["type"].ToString()=="asp"||files["type"].ToString()=="config")
	{

	ViewBuilder.Append("                <a href=\"sitefileedit.aspx?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "" + files["name"].ToString().Trim() + "\">" + files["name"].ToString().Trim() + "</a>\r\n");

	}
	else
	{

	ViewBuilder.Append("                <a href=\"" + webpath.ToString() + "sites/" + m_sitepath.ToString() + "/" + m_path.ToString() + "" + files["name"].ToString().Trim() + "\" target=\"_blank\">" + files["name"].ToString().Trim() + "</a>\r\n");

	}	//end if

	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                " + files["updateTime"].ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                " + files["type"].ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                " + files["size"].ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("                <a href=\"#\">改名</a>\r\n");

	if (files["type"].ToString()=="文件夹")
	{

	ViewBuilder.Append("                <a href=\"sitefilemanage.aspx?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "" + files["name"].ToString().Trim() + "\">打开</a>\r\n");

	}
	else if (files["type"].ToString()=="xml"||files["type"].ToString()=="htm"||files["type"].ToString()=="html"||files["type"].ToString()=="css"||files["type"].ToString()=="js"||files["type"].ToString()=="txt"||files["type"].ToString()=="aspx"||files["type"].ToString()=="ascx"||files["type"].ToString()=="asp"||files["type"].ToString()=="config")
	{

	ViewBuilder.Append("                <a href=\"sitefileedit.aspx?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "" + files["name"].ToString().Trim() + "\">编辑</a>\r\n");

	}
	else
	{

	ViewBuilder.Append("                <a href=\"" + webpath.ToString() + "sites/" + m_sitepath.ToString() + "/" + m_path.ToString() + "" + files["name"].ToString().Trim() + "\">打开</a>\r\n");

	}	//end if

	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

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
