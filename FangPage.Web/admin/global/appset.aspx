<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.appset" %>
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
	ViewBuilder.Append("<title>系统应用配置</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("<link href=\"../css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("<link href=\"../css/tab.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"appmanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统应用管理," + rawpath.ToString() + "appmanage.aspx|系统应用配置," + rawpath.ToString() + "" + pagename.ToString() + "?id=" + id.ToString() + "\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\"> \r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("    <td>\r\n");

	if (id>0)
	{

	ViewBuilder.Append("      <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul id=\"mytab1\">\r\n");
	ViewBuilder.Append("            <li class=\"active\"><a href=\"appset.aspx?id=" + id.ToString() + "\">应用配置</a> </li>\r\n");
	ViewBuilder.Append("            <li class=\"normal\"><a href=\"appsortmanage.aspx?id=" + id.ToString() + "\">栏目功能</a> </li>\r\n");

	if (appinfo.frontpage!=""&&appinfo.target=="_self")
	{

	ViewBuilder.Append("            <li class=\"normal\"><a href=\"appuserseting.aspx?id=" + id.ToString() + "\">应用主页</a> </li>\r\n");

	}	//end if

	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");

	}
	else
	{

	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">新建系统应用</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");

	}	//end if

	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用名称： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"name\" name=\"name\" type=\"text\" value=\"" + appinfo.name.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用目录： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"installpath\" name=\"installpath\" type=\"text\" value=\"" + appinfo.installpath.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用作者： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"author\" name=\"author\" type=\"text\" value=\"" + appinfo.author.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用版本： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"version\" name=\"version\" type=\"text\" value=\"" + appinfo.version.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用主页： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"frontpage\" name=\"frontpage\" type=\"text\" value=\"" + appinfo.frontpage.ToString().Trim() + "\" style=\"height:21px;width:400px;\">&nbsp;\r\n");
	ViewBuilder.Append("                <input id=\"target\" name=\"target\" \r\n");

	if (appinfo.target=="_blank")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"_blank\" type=\"checkbox\">新窗口打开\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用备注： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"notes\" name=\"notes\" type=\"text\" value=\"" + appinfo.notes.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	if (id>0)
	{

	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"> 应用标识码： </td>\r\n");
	ViewBuilder.Append("            <td><input id=\"guid\" name=\"guid\" disabled=\"disabled\" type=\"text\" value=\"" + appinfo.guid.ToString().Trim() + "\" style=\"height:21px;width:400px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end if

	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("            <input type=\"submit\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            <input type=\"button\" id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("    </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
