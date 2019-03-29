<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.sitefileedit" %>
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
	ViewBuilder.Append("    <title>站点文件编辑</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("    <link rel=\"stylesheet\" href=\"" + webpath.ToString() + "plugins/codemirror/codemirror.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/codemirror.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/xml.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/htmlmixed.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/javascript.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/css.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "plugins/codemirror/sql.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("                PageBack(\"sitefilemanage.aspx?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "\");\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            PageNav(\"系统站点管理," + rawpath.ToString() + "sitefilemanage.aspx?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "|站点文件管理," + rawpath.ToString() + "sitefilemanage.aspx?sitepath=" + m_sitepath.ToString() + "|站点文件编辑," + rawpath.ToString() + "" + pagename.ToString() + "?sitepath=" + m_sitepath.ToString() + "&path=" + m_path.ToString() + "\");\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <style type=\"text/css\">\r\n");
	ViewBuilder.Append("        .CodeMirror {border: 1px solid #ddd;}\r\n");
	ViewBuilder.Append("    </style>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("        <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("            <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"newstitle\" bgcolor=\"#ffffff\">站点文件编辑</td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("            <table style=\"width: 100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("                            <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                                <tbody>\r\n");
	ViewBuilder.Append("                                    <tr>\r\n");
	ViewBuilder.Append("                                        <td>文件名称：<input id=\"name\" name=\"name\" type=\"text\" value=\"" + filename.ToString() + "\" style=\"height: 21px; width: 400px;\"></td>\r\n");
	ViewBuilder.Append("                                    </tr>\r\n");
	ViewBuilder.Append("                                    <tr>\r\n");
	ViewBuilder.Append("                                        <td>\r\n");
	ViewBuilder.Append("                                            <textarea name=\"content\" id=\"content\">" + content.ToString() + "</textarea>\r\n");
	ViewBuilder.Append("                                        </td>\r\n");
	ViewBuilder.Append("                                    </tr>\r\n");
	ViewBuilder.Append("                                    <tr>\r\n");
	ViewBuilder.Append("                                        <td style=\"padding-left: 20px\" height=\"25\">\r\n");
	ViewBuilder.Append("                                            <input type=\"submit\" name=\"btnSave\" value=\"保存\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                                            <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"返回\" type=\"button\">\r\n");
	ViewBuilder.Append("                                        </td>\r\n");
	ViewBuilder.Append("                                    </tr>\r\n");
	ViewBuilder.Append("                                </tbody>\r\n");
	ViewBuilder.Append("                            </table>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </form>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        var editor1 = CodeMirror.fromTextArea(document.getElementById(\"content\"), {\r\n");
	ViewBuilder.Append("            mode: \"text/x-mariadb\",\r\n");
	ViewBuilder.Append("            lineNumbers: true,\r\n");
	ViewBuilder.Append("            lineWrapping: true,\r\n");
	ViewBuilder.Append("            onCursorActivity: function () {\r\n");
	ViewBuilder.Append("                editor1.setLineClass(hlLine, null, null);\r\n");
	ViewBuilder.Append("                hlLine = editor1.setLineClass(editor1.getCursor().line, null, \"activeline\");\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        var hlLine = editor1.setLineClass(0, \"activeline\");\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
