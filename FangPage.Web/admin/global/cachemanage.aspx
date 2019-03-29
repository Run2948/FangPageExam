<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.cachemanage" %>
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
	ViewBuilder.Append("    <title>系统缓存管理</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            PageNav(\"系统缓存管理,global/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">系统缓存管理</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("            <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"sysconfig\" type=\"checkbox\">系统配置缓存</td>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"siteconfig\" type=\"checkbox\">系统站点缓存</td>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"syssort\" type=\"checkbox\">系统栏目缓存</td>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"attachtype\" type=\"checkbox\">上传文件类型</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"sysdesktop\" type=\"checkbox\">系统桌面缓存</td>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"sorttype\" type=\"checkbox\">栏目分类缓存</td>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"permission\" type=\"checkbox\">用户权限缓存</td>\r\n");
	ViewBuilder.Append("                    <td align=\"left\"> \r\n");
	ViewBuilder.Append("                        <input id=\"cache\" name=\"cache\" value=\"department\" type=\"checkbox\">用户部门缓存</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td height=\"25\" colspan=\"4\"><input type=\"submit\" name=\"btnSave\" value=\"清除缓存\" id=\"btnSave\" class=\"adminsubmit_long\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table></td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("      <br>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
