<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller.exammonitor" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FP_Exam" %>
<%@ Import namespace="FP_Exam.Model" %>

<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.8*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>考试监控</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + adminpath.ToString() + "css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"" + adminpath.ToString() + "css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + adminpath.ToString() + "js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        PageNav(\"" + sortinfo.name.ToString().Trim() + "," + rawpath.ToString() + "exammanage.aspx?sortid=" + examinfo.sortid.ToString().Trim() + "|" + examinfo.name.ToString().Trim() + "," + rawpath.ToString() + "exammanage.aspx?sortid=" + examinfo.sortid.ToString().Trim() + "" + pagenav.ToString() + "|考试监控," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("            $('input[name=chkid]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitdel\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"您确定要删除该考生的考试吗？\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#submitchange\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"您确定要给所选的考生进行换位置吗？\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"change\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("    function ChangeExam(id) {\r\n");
	ViewBuilder.Append("        if (confirm(\"您确定要给该考生换位置吗？\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"change\");\r\n");
	ViewBuilder.Append("            $('#chkid_' + id).attr(\"checked\", \"checked\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<meta http-equiv=\"refresh\" content=\"20\">\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/tag.gif) 2px 6px no-repeat\"><a id=\"submitchange\" href=\"#\">换位</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/refresh.gif) 2px 6px no-repeat\"><a href=\"" + rawurl.ToString() + "\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/return.gif) 2px 6px no-repeat\"><a href=\"exammanage.aspx?sortid=" + examinfo.sortid.ToString().Trim() + "\">返回</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\">\r\n");
	ViewBuilder.Append("              <strong>" + examinfo.name.ToString().Trim() + "：在考人数" + examresultlist.Count.ToString().Trim() + "人</strong>\r\n");
	ViewBuilder.Append("              </li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"40\"><input id=\"checkall\" name=\"checkall\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("        	  <td>用户名</td>\r\n");
	ViewBuilder.Append("        	  <td>姓名</td>\r\n");
	ViewBuilder.Append("              <td>座位号</td>\r\n");
	ViewBuilder.Append("              <td>所在部门</td>\r\n");
	ViewBuilder.Append("              <td>开始时间</td>\r\n");
	ViewBuilder.Append("              <td>考试用时</td>\r\n");
	ViewBuilder.Append("              <td>IP地址</td>\r\n");
	ViewBuilder.Append("              <td>在线状态</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">操作</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(ExamResult item in examresultlist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td><input id=\"chkid_" + item.id.ToString().Trim() + "\" name=\"chkid\" value=\"" + item.id.ToString().Trim() + "\" type=\"checkbox\"></td>\r\n");
	ViewBuilder.Append("              <td>" + item.IUser.username.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.IUser.realname.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.IUser.nickname.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.IUser.Department.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + FangPage.MVC.FPUtils.GetDate(item.examdatetime,"yyyy-MM-dd HH:mm:ss") + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + (item.utime/60+1).ToString().Trim() + "分钟</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.ip.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>\r\n");

	if (item.IUser.onlinestate==1)
	{

	ViewBuilder.Append("                  <span style=\"color:#1317fc\">在线</span>\r\n");

	}
	else
	{

	ViewBuilder.Append("                  <span style=\"color:#00ff21\">离线</span>\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("              <a style=\"color: #1317fc\" href=\"javascript:ChangeExam(" + item.id.ToString().Trim() + ")\">换位</a>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("  </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
