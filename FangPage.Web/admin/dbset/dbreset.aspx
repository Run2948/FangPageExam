<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.dbreset" %>
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
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("<title>重置库表标识列</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btnsubmit\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"本操作将重置所选择表的数据，届时该表所有的数据将被清空，您确认要重置吗？\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"table\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#btnreset\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"本操作将重置系统所有数据，届时系统所有数据将被清空，您确认要重置系统吗？\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"system\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"重置清空数据库,dbset/" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <input id=\"action\" name=\"action\" value=\"\" type=\"hidden\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">重置清空数据库</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td>\r\n");
	ViewBuilder.Append("              <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td style=\"font-weight:bold;\" colspan=\"2\">\r\n");
	ViewBuilder.Append("                        选择要重置的数据库表：本操作将会清空该表所有的数据，并重置该表的标识列，请谨慎操作。\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td colspan=\"2\">\r\n");
	ViewBuilder.Append("                        <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                         <tr>\r\n");
	int n = 1;
	

	loop__id=0;
	foreach(DataRow row in dbtablelist.Rows)
	{
	loop__id++;


	if (IsWMSTable(row["TABLE_NAME"].ToString()))
	{

	ViewBuilder.Append("                             <td align=\"left\"><input id=\"tablename\" name=\"tablename\" value=\"" + row["TABLE_NAME"].ToString().Trim() + "\" type=\"checkbox\">" + row["TABLE_NAME"].ToString().Trim() + "</td>\r\n");

	if (n%4==0)
	{

	ViewBuilder.Append("                                </tr>\r\n");
	ViewBuilder.Append("                                <tr>\r\n");

	}	//end if

	 n = n+1;
	

	}	//end if


	}	//end loop

	ViewBuilder.Append("                         </tr>\r\n");
	ViewBuilder.Append("                         </table>\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td style=\"width:30px;\"></td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\" align=\"left\">\r\n");
	ViewBuilder.Append("                    <input type=\"button\" name=\"btnsubmit\" value=\"重置表\" id=\"btnsubmit\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td style=\"font-weight:bold;\" colspan=\"2\">\r\n");
	ViewBuilder.Append("                        重置系统：本操作将清空系统原有数据，请做好备份工作后再操作。\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td style=\"width:30px;\"></td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\" align=\"left\">\r\n");
	ViewBuilder.Append("                    <input type=\"button\" name=\"btnreset\" value=\"重置系统\" id=\"btnreset\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table>\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");

	if (ispost)
	{

	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        layer.msg('重置成功!', 0, 1);\r\n");
	ViewBuilder.Append("        var bar = 0;\r\n");
	ViewBuilder.Append("        count();\r\n");
	ViewBuilder.Append("        function count() {\r\n");
	ViewBuilder.Append("            bar = bar + 4;\r\n");
	ViewBuilder.Append("            if (bar < 80) {\r\n");
	ViewBuilder.Append("                setTimeout(\"count()\", 100);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else {\r\n");
	ViewBuilder.Append("                window.location.href = \"" + link.ToString() + "\";\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");

	}	//end if

	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
