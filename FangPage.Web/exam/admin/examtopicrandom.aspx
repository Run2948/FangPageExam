<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.examtopicrandom" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.Exam" %>
<%@ Import namespace="FangPage.Exam.Model" %>

<%@ Import namespace="FangPage.WMS.Model" %>
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
	ViewBuilder.Append("<title>随机选题设置</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + adminpath.ToString() + "css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"" + adminpath.ToString() + "css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + adminpath.ToString() + "js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	string navurl = "exammanage.aspx";
	
	ViewBuilder.Append("        PageNav(\"" + GetSortNav(sortinfo,navurl).ToString() + "|试题设置," + rawpath.ToString() + "examtopicmanage.aspx?examid=" + examinfo.id.ToString().Trim() + "|随机选题," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        $(\"#btnsava\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"save\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $(\"#btncreate\").click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"你确定要随机生成固定题吗？生成后将清除以下的随机设置\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"create\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input id=\"action\" name=\"action\" value=\"\" type=\"hidden\">\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td>\r\n");
	ViewBuilder.Append("       <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/save.gif) 2px 6px no-repeat\"><a id=\"btnsava\" href=\"javascript:void()\">保存随机设置</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/question.gif) 2px 6px no-repeat\"><a id=\"btncreate\" href=\"javascript:void()\">随机生成固定题</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/refresh.gif) 2px 6px no-repeat\"><a href=\"" + rawurl.ToString() + "\">刷新</a> </li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/return.gif) 2px 6px no-repeat\"><a href=\"examtopicmanage.aspx?examid=" + examinfo.id.ToString().Trim() + "&paper=" + paper.ToString() + "\">返回</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\"><strong>随机选题：[" + examinfo.name.ToString().Trim() + "" + GetPaper(paper).ToString() + "]</strong></li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("       </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td>\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr class=\"thead\">\r\n");
	ViewBuilder.Append("        	  <td align=\"left\">" + examtopic.title.ToString().Trim() + "(总题数<span style=\"color:Red\">" + examtopic.questions.ToString().Trim() + "</span>题，固定题<span style=\"color:Red\" id=\"curquestions\">" + examtopic.curquestions.ToString().Trim() + "</span>题，随机题<span style=\"color:Red\">" + (examtopic.questions-examtopic.curquestions).ToString().Trim() + "</span>题)</td>\r\n");
	ViewBuilder.Append("              <td>固定题</td>\r\n");
	ViewBuilder.Append("              <td>已设随机题<span style=\"color:#ff0000\">" + examtopic.randoms.ToString().Trim() + "</span>题</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	string tree = "├";
	

	loop__id=0;
	foreach(SortInfo sorts in sortlist)
	{
	loop__id++;


	if (ischecked(sorts.id,role.sorts)==false&&roleid!=1)
	{

	continue;


	}	//end if

	string hidden = "";
	

	if (sorts.hidden==1)
	{

	 hidden = "_hidden";
	

	}	//end if

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td align=\"left\">├\r\n");

	if (sorts.icon!="")
	{

	ViewBuilder.Append("              <img src=\"" + sorts.icon.ToString().Trim() + "\" width=\"16\" height=\"16\">\r\n");

	}
	else if (sorts.subcounts>0)
	{

	ViewBuilder.Append("              <img src=\"" + adminpath.ToString() + "images/folders" + hidden.ToString() + ".gif\" width=\"16\" height=\"16\">\r\n");

	}
	else
	{

	ViewBuilder.Append("              <img src=\"" + adminpath.ToString() + "images/folder" + hidden.ToString() + ".gif\" width=\"16\" height=\"16\">\r\n");

	}	//end if

	ViewBuilder.Append("              " + sorts.name.ToString().Trim() + "(" + GetQuestionCount(sorts.id).ToString() + ")</td>\r\n");
	ViewBuilder.Append("              <td>" + GetCurCount(sorts.id).ToString() + "</td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("                  <input id=\"randomcount_" + sorts.id.ToString().Trim() + "\" name=\"randomcount_" + sorts.id.ToString().Trim() + "\" value=\"" + GetRandomCount(sorts.id).ToString() + "\" type=\"text\">  \r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            " + ShowChildSort(sorts.id,tree).ToString() + "\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("</form>\r\n");

	if (ispost)
	{

	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    layer.msg('" + msg.ToString() + "', 0, 1);\r\n");
	ViewBuilder.Append("    var bar = 0;\r\n");
	ViewBuilder.Append("    count();\r\n");
	ViewBuilder.Append("    function count() {\r\n");
	ViewBuilder.Append("        bar = bar + 4;\r\n");
	ViewBuilder.Append("        if (bar < 80) {\r\n");
	ViewBuilder.Append("            setTimeout(\"count()\", 100);\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        else {\r\n");
	ViewBuilder.Append("            window.location.href = \"" + link.ToString() + "\";\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");

	}	//end if

	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
