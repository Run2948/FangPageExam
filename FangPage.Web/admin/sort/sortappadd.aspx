<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sortappadd" %>
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
	ViewBuilder.Append("    <title>添加编辑栏目功能</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href = \"sortappmanage.aspx\";\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            PageNav(\"栏目功能管理," + rawpath.ToString() + "sortappmanage.aspx|添加编辑栏目功能," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("        <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("            <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"newstitle\" bgcolor=\"#ffffff\">添加编辑栏目功能</td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("            <table style=\"width: 100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\">栏目功能名称： </td>\r\n");
	ViewBuilder.Append("                        <td>\r\n");
	ViewBuilder.Append("                            <input id=\"name\" name=\"name\" type=\"text\" value=\"" + sortappinfo.name.ToString().Trim() + "\" style=\"height: 21px; width: 400px;\"></td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\">栏目功能标识： </td>\r\n");
	ViewBuilder.Append("                        <td>\r\n");
	ViewBuilder.Append("                            <input id=\"markup\" name=\"markup\" type=\"text\" value=\"" + sortappinfo.markup.ToString().Trim() + "\" style=\"height: 21px; width: 400px;\"></td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\">所属系统应用： </td>\r\n");
	ViewBuilder.Append("                        <td>\r\n");
	ViewBuilder.Append("                            <select id=\"appid\" name=\"appid\">\r\n");
	ViewBuilder.Append("                                <option value=\"0\">选择系统应用</option>\r\n");

	loop__id=0;
	foreach(AppInfo apps in applist)
	{
	loop__id++;

	ViewBuilder.Append("                                <option value=\"" + apps.id.ToString().Trim() + "\" \r\n");

	if (apps.id==sortappinfo.appid)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">" + apps.name.ToString().Trim() + "</option>\r\n");

	}	//end loop

	ViewBuilder.Append("                            </select>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\">功能管理页地址： </td>\r\n");
	ViewBuilder.Append("                        <td>\r\n");
	ViewBuilder.Append("                            <input id=\"indexpage\" name=\"indexpage\" type=\"text\" value=\"" + sortappinfo.indexpage.ToString().Trim() + "\" style=\"height: 21px; width: 400px;\"></td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\">功能详细页地址： </td>\r\n");
	ViewBuilder.Append("                        <td>\r\n");
	ViewBuilder.Append("                            <input id=\"viewpage\" name=\"viewpage\" type=\"text\" value=\"" + sortappinfo.viewpage.ToString().Trim() + "\" style=\"height: 21px; width: 400px;\"></td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                        <td height=\"25\">\r\n");
	ViewBuilder.Append("                            <input type=\"submit\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                            <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
