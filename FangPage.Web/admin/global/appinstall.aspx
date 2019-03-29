<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.WMS.Admin.appinstall" %>
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
	ViewBuilder.Append("<title>系统应用安装</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link href=\"../css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"../js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        $(\"#btncancle\").click(function () {\r\n");
	ViewBuilder.Append("            window.location.href = \"appmanage.aspx\";\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        PageNav(\"系统应用管理," + rawpath.ToString() + "appmanage.aspx|系统应用安装," + rawpath.ToString() + "" + pagename.ToString() + "\");\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\" enctype=\"multipart/form-data\">\r\n");
	ViewBuilder.Append("    <input id=\"filename\" name=\"filename\" value=\"" + filename.ToString() + "\" type=\"hidden\">\r\n");
	ViewBuilder.Append("    <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("      <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">系统应用安装</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("     </table>\r\n");

	if (step=="step1")
	{

	ViewBuilder.Append("      <input id=\"step\" name=\"step\" value=\"step2\" type=\"hidden\">\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("             <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");

	if (appinfo.name!="")
	{

	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用名称： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       " + appinfo.name.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用版本： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       " + appinfo.version.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用作者： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       " + appinfo.author.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用备注： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       " + appinfo.notes.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");

	}
	else
	{

	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用名称： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                        <input id=\"name\" name=\"name\" value=\"" + appinfo.name.ToString().Trim() + "\" type=\"text\" style=\"width:200px\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用版本： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"version\" name=\"version\" value=\"" + appinfo.version.ToString().Trim() + "\" type=\"text\" style=\"width:200px\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用作者： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"author\" name=\"author\" value=\"" + appinfo.author.ToString().Trim() + "\" type=\"text\" style=\"width:200px\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用备注： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"notes\" name=\"notes\" value=\"" + appinfo.notes.ToString().Trim() + "\" type=\"text\" style=\"width:200px\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");

	}	//end if

	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\">安装目录：</td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"installpath\" name=\"installpath\" type=\"text\" value=\"" + appinfo.setpath.ToString().Trim() + "\" style=\"height:21px;width:200px;\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\">\r\n");
	ViewBuilder.Append("                    <input type=\"submit\" name=\"btnSave\" value=\"安装\" id=\"btnSave\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"取消\" type=\"button\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table></td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");

	}
	else
	{

	ViewBuilder.Append("      <input id=\"step\" name=\"step\" value=\"step1\" type=\"hidden\">\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("          <tr>\r\n");
	ViewBuilder.Append("            <td class=\"tdbg\">\r\n");
	ViewBuilder.Append("             <table cellspacing=\"0\" cellpadding=\"3\" width=\"100%\" border=\"0\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"> 应用安装文件： </td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"uploadfile\" name=\"uploadfile\" type=\"file\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td class=\"td_class\"></td>\r\n");
	ViewBuilder.Append("                    <td height=\"25\">\r\n");
	ViewBuilder.Append("                    <input type=\"submit\" name=\"btnSave\" value=\"下一步\" id=\"Submit1\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("                    <input id=\"btncancle\" class=\"adminsubmit_short\" name=\"btncancle\" value=\"取消\" type=\"button\">\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("              </table></td>\r\n");
	ViewBuilder.Append("          </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");

	}	//end if

	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
