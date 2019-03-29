<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.attachtypemanage" %>
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
	ViewBuilder.Append("<title>上传附件类型</title>\r\n");
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
	ViewBuilder.Append("            if (confirm(\"你确定要删除吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#btn_add\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"add\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#btn_edit\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"edit\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"上传附件类型,global/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("        <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(../images/refresh.gif) 2px 6px no-repeat\"><a href=\"attachtypemanage.aspx\">刷新</a> </li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>上传附件类型</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("      <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"40\"><input id=\"checkall\" name=\"checkall\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("        	  <td>扩展名</td>\r\n");
	ViewBuilder.Append("        	  <td>最大尺寸(单位:KB)</td>\r\n");
	ViewBuilder.Append("        	  <td>文件类型</td>\r\n");
	ViewBuilder.Append("              <td>操作</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(AttachType item in attachlist)
	{
	loop__id++;


	if (item.id==id)
	{

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("                <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input type=\"text\" style=\"width:200px\" name=\"edit_extension\" id=\"edit_extension\" value=\"" + item.extension.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input type=\"text\" style=\"width:160px\" name=\"edit_maxsize\" id=\"edit_maxsize\" value=\"" + item.maxsize.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("                    <select style=\"width:80px\" onchange=\"document.getElementById('edit_maxsize').value=this.value\">\r\n");
	ViewBuilder.Append("                            <option value=\"\">选择大小</option>\r\n");
	ViewBuilder.Append("                            <option value=\"100\">100K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"200\">200K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"300\">300K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"400\">400K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"500\">500K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"600\">600K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"800\">800K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"1024\">1M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"2048\">2M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"4096\">4M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"5120\">5M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"8192\">8M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"10240\">10M</option>\r\n");
	ViewBuilder.Append("                     </select>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <select id=\"edit_type\" name=\"edit_type\" style=\"width:80px\">\r\n");
	ViewBuilder.Append("                        <option value=\"image\" \r\n");

	if (item.type=="image")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">图片</option>\r\n");
	ViewBuilder.Append("                        <option value=\"flash\" \r\n");

	if (item.type=="flash")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">动画</option>\r\n");
	ViewBuilder.Append("                        <option value=\"media\" \r\n");

	if (item.type=="media")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">视频</option>\r\n");
	ViewBuilder.Append("                        <option value=\"file\" \r\n");

	if (item.type=="file")
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">附件</option>\r\n");
	ViewBuilder.Append("                    </select>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <a id=\"btn_edit\" style=\"width: 30px; display: inline-block; color: #1317fc\" href=\"#\">更新</a>\r\n");
	ViewBuilder.Append("                    <a style=\"width: 30px; display: inline-block; color: #1317fc\" href=\"attachtypemanage.aspx\">取消</a>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}
	else
	{

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td><input id=\"chkdel\" name=\"chkdel\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.extension.ToString().Trim() + " </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.maxsize.ToString().Trim() + " </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">\r\n");

	if (item.type=="image")
	{

	ViewBuilder.Append("              图片\r\n");

	}
	else if (item.type=="flash")
	{

	ViewBuilder.Append("              动画\r\n");

	}
	else if (item.type=="media")
	{

	ViewBuilder.Append("              视频\r\n");

	}
	else if (item.type=="file")
	{

	ViewBuilder.Append("              附件\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("                 <a style=\"width: 30px; display: inline-block; color: #1317fc\" href=\"?id=" + item.id.ToString().Trim() + "\">编辑</a>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end if


	}	//end loop


	if (id==0)
	{

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("                <td><input id=\"chkdel\" name=\"chkdel\" value=\"0\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input type=\"text\" style=\"width:200px\" name=\"extension\" id=\"extension\" value=\"\">\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <input type=\"text\" style=\"width:160px\" name=\"maxsize\" id=\"maxsize\" value=\"\">\r\n");
	ViewBuilder.Append("                    <select style=\"width:80px\" onchange=\"document.getElementById('maxsize').value=this.value\">\r\n");
	ViewBuilder.Append("                            <option value=\"\">选择大小</option>\r\n");
	ViewBuilder.Append("                            <option value=\"100\">100K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"200\">200K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"300\">300K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"400\">400K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"500\">500K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"600\">600K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"800\">800K</option>\r\n");
	ViewBuilder.Append("                            <option value=\"1024\">1M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"2048\">2M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"4096\">4M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"5120\">5M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"8192\">8M</option>\r\n");
	ViewBuilder.Append("                            <option value=\"10240\">10M</option>\r\n");
	ViewBuilder.Append("                     </select>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <select id=\"type\" name=\"type\" style=\"width:80px\">\r\n");
	ViewBuilder.Append("                        <option value=\"image\">图片</option>\r\n");
	ViewBuilder.Append("                        <option value=\"flash\">动画</option>\r\n");
	ViewBuilder.Append("                        <option value=\"media\">视频</option>\r\n");
	ViewBuilder.Append("                        <option value=\"file\">附件</option>\r\n");
	ViewBuilder.Append("                    </select>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("                <td>\r\n");
	ViewBuilder.Append("                    <a id=\"btn_add\" style=\"width: 30px; display: inline-block; color: #1317fc\" href=\"#\">添加</a>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end if

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </form>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
