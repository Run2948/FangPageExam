<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.tasklog" %>
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
	ViewBuilder.Append("    <title>系统计划任务执行日志</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("    <link rel=\"stylesheet\" href=\"" + webpath.ToString() + "plugins/codemirror/lib/codemirror.css\">\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/lib/codemirror.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/mode/sql.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href = \"taskmanage.aspx\";\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            PageNav(\"系统计划任务管理," + rawpath.ToString() + "taskmanage.aspx|系统计划任务执行日志," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <style type=\"text/css\">\r\n");
	ViewBuilder.Append("     .CodeMirror {\r\n");
	ViewBuilder.Append("         border: 1px solid #ddd;\r\n");
	ViewBuilder.Append("     }\r\n");
	ViewBuilder.Append("      .CodeMirror pre  \r\n");
	ViewBuilder.Append("      {\r\n");
	ViewBuilder.Append("          padding-left: 7px; \r\n");
	ViewBuilder.Append("          line-height: 1.5; \r\n");
	ViewBuilder.Append("      }\r\n");
	ViewBuilder.Append("    </style>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">系统计划任务执行日志</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("              <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                     <textarea name=\"logcontent\" id=\"logcontent\" style=\"width:100%;height:600px\">" + logcontent.ToString() + "</textarea>\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td height=\"25\" align=\"left\" style=\"padding-left:60px;\">\r\n");
	ViewBuilder.Append("                    <input type=\"submit\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    <input type=\"button\" id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table>\r\n");
	ViewBuilder.Append("             </td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    window.onload = function () {\r\n");
	ViewBuilder.Append("        var mime = 'text/x-mariadb';\r\n");
	ViewBuilder.Append("        // get mime type\r\n");
	ViewBuilder.Append("        if (window.location.href.indexOf('mime=') > -1) {\r\n");
	ViewBuilder.Append("            mime = window.location.href.substr(window.location.href.indexOf('mime=') + 5);\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        window.editor = CodeMirror.fromTextArea(document.getElementById('logcontent'), {\r\n");
	ViewBuilder.Append("            mode: mime,\r\n");
	ViewBuilder.Append("            indentWithTabs: true,\r\n");
	ViewBuilder.Append("            smartIndent: true,\r\n");
	ViewBuilder.Append("            lineNumbers: true,\r\n");
	ViewBuilder.Append("            matchBrackets: true,\r\n");
	ViewBuilder.Append("            autofocus: true\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    };\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
