<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.desktopadd" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.WMS.Admin" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：4.5*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>添加编辑桌面</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"../css/admin.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"desktopmanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统桌面管理,global/desktopmanage.aspx|添加编辑桌面,global/" + pagename.ToString() + "?id=" + id.ToString() + "\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\" enctype=\"multipart/form-data\">\r\n");
	ViewBuilder.Append("  <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("    <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">添加编辑桌面</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">桌面名称： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"name\" name=\"name\" value=\"" + desktopinfo.name.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">桌面图标： </td>\r\n");
	ViewBuilder.Append("          <td style=\"width: 200px\">\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"icon\" name=\"icon\" value=\"" + desktopinfo.icon.ToString().Trim() + "\" type=\"text\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("          <td align=\"left\">\r\n");

	if (desktopinfo.icon!="")
	{

	ViewBuilder.Append("              <img src=\"" + desktopinfo.icon.ToString().Trim() + "\" width=\"32\" height=\"32\" alt=\"" + desktopinfo.name.ToString().Trim() + "\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"../images/desktop.gif\" width=\"32\" height=\"32\" alt=\"" + desktopinfo.name.ToString().Trim() + "\">\r\n");

	}	//end if

	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">上传图标： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input id=\"uploadicon\" name=\"uploadicon\" type=\"file\" style=\"width: 200px\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">桌面描述： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"description\" name=\"description\" value=\"" + desktopinfo.description.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">左栏地址： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"lefturl\" name=\"lefturl\" value=\"" + desktopinfo.lefturl.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">右栏地址： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input style=\"width: 200px\" id=\"righturl\" name=\"righturl\" value=\"" + desktopinfo.righturl.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">隐藏桌面： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input id=\"hidden\" name=\"hidden\" value=\"1\" \r\n");

	if (desktopinfo.hidden==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">是/否\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	if (isperm)
	{

	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\">系统桌面： </td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("              <input id=\"system\" name=\"system\" value=\"1\" \r\n");

	if (desktopinfo.system==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">是/否\r\n");
	ViewBuilder.Append("          </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");

	}	//end if

	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("          <td colspan=\"2\">\r\n");
	ViewBuilder.Append("            <input name=\"submit\" value=\"保存\" type=\"submit\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\"></td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
