<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.examhistory" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.Exam" %>
<%@ Import namespace="FangPage.Exam.Model" %>

<%@ Import namespace="FangPage.WMS" %>
<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">




override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.9*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html>\r\n");
	ViewBuilder.Append("<html lang=\"zh-CN\" class=\"default-layout\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n");
	ViewBuilder.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge,chrome=1\">\r\n");
	ViewBuilder.Append("<title>考试历史 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/report.css\">\r\n");

	ViewBuilder.Append("<!--[if lte IE 8]><link id=\"ie8Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie8.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if lte IE 7]><link id=\"ie7Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie7.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if gte IE 9]><link id=\"ie9Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/gte-ie9.css\")}\"/><![endif]-->\r\n");


	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    function ExamContinu(id) {\r\n");
	ViewBuilder.Append("        window.open(\"exam.aspx?resultid=\" + id);\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<!--[if gte IE 9]>\r\n");
	ViewBuilder.Append("  <style type=\"text/css\">\r\n");
	ViewBuilder.Append("    .gradient {\r\n");
	ViewBuilder.Append("       filter: none;\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("  </style>\r\n");
	ViewBuilder.Append("<![endif]-->\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<div class=\"wrap\">\r\n");

	ViewBuilder.Append("<div class=\"repeat-x default-t\">\r\n");
	ViewBuilder.Append("    <div class=\"header-left layout default-lt\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"header-right layout default-rt\"></div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div class=\"default-left repeat-y default-l\"></div>\r\n");
	ViewBuilder.Append("<div class=\"default-right repeat-y default-r\"></div>\r\n");
	ViewBuilder.Append("<div class=\"header\">\r\n");
	ViewBuilder.Append("    <div class=\"navbar-hd clearfix\">\r\n");
	ViewBuilder.Append("        <div class=\"nav-bar-links\">\r\n");
	ViewBuilder.Append("            <a href=\"http://www.fangpage.com\" class=\"course-link course-menu-trigger\" style=\"color:#ffffff\"><strong>方配在线考试系统版本：" + siteconfig.version.ToString().Trim() + "</strong></a>\r\n");

	if (isadmin)
	{

	ViewBuilder.Append("            <a href=\"" + adminpath.ToString() + "index.aspx\" class=\"course-link course-menu-trigger\" style=\"color:#ffffff\" target=\"_blank\"><strong>【登录考试系统后台】</strong></a>\r\n");

	}	//end if

	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"pull-right navbar-links\">\r\n");
	ViewBuilder.Append("            <a href=\"logout.aspx\" class=\"user-nav user-menu-trigger\"><span class=\"sprite sprite-profile i-20\"></span><span class=\"sprite sprite-profile-tip\"><span class=\"email\" style=\"color:#ffffff\">" + username.ToString() + "&nbsp;|&nbsp;[退出系统]</span><span class=\"sprite sprite-profile-tip-arrow\"></span></span></a>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"navbar-bd\">\r\n");
	ViewBuilder.Append("        <div class=\"container\">\r\n");
	ViewBuilder.Append("            <a href=\"index.aspx\" class=\"logo\">\r\n");
	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/logo.png\" alt=\"" + siteconfig.name.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("            </a>\r\n");
	ViewBuilder.Append("            <ul class=\"nav main-nav\">\r\n");
	ViewBuilder.Append("                <li \r\n");

	if (pagename=="index.aspx")
	{

	ViewBuilder.Append(" class=\"active\" \r\n");

	}	//end if

	ViewBuilder.Append("><a href=\"index.aspx\">练习与考试</a></li>\r\n");
	ViewBuilder.Append("                <li \r\n");

	if (pagename=="examreport.aspx")
	{

	ViewBuilder.Append(" class=\"active\" \r\n");

	}	//end if

	ViewBuilder.Append("><a href=\"examreport.aspx\">能力评估报告</a></li>\r\n");
	ViewBuilder.Append("                <li \r\n");

	if (pagename=="examhistory.aspx"||pagename=="incorrect.aspx"||pagename=="examnote.aspx"||pagename=="favorite.aspx")
	{

	ViewBuilder.Append(" class=\"active\" \r\n");

	}	//end if

	ViewBuilder.Append("><a href=\"examhistory.aspx\">考试历史</a></li>\r\n");

	if (pagename=="examlist.aspx"||pagename=="myexamlist.aspx")
	{

	ViewBuilder.Append("                <li class=\"active\"><a href=\"" + fullname.ToString() + "\">" + pagenav.ToString() + "</a></li>\r\n");

	}	//end if

	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <span class=\"repeat-x header-shadow-repeat header-shadow-repeat-l\"></span>\r\n");
	ViewBuilder.Append("    <div class=\"header-shadow\"></div>\r\n");
	ViewBuilder.Append("    <span class=\"repeat-x header-shadow-repeat header-shadow-repeat-r\"></span>\r\n");
	ViewBuilder.Append("</div>\r\n");


	ViewBuilder.Append("  <div class=\"container body-wrap main\">\r\n");
	ViewBuilder.Append("    <div class=\"box-wrap history-wrap\">\r\n");
	ViewBuilder.Append("      <div class=\"box\">\r\n");
	ViewBuilder.Append("        <div class=\"box-hd\">\r\n");
	ViewBuilder.Append("          <ul class=\"nav nav-underline\">\r\n");
	ViewBuilder.Append("            <li class=\"active\"><a href=\"examhistory.aspx\">考试记录</a></li>\r\n");
	ViewBuilder.Append("            <li><a href=\"incorrect.aspx\">我的错题</a></li>\r\n");
	ViewBuilder.Append("            <li><a href=\"examnote.aspx\">笔记题目</a></li>\r\n");
	ViewBuilder.Append("            <li><a href=\"favorite.aspx\">我的收藏</a></li>\r\n");
	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"box-bd\">\r\n");
	ViewBuilder.Append("          <div class=\"exercise-list-wrap list-wrap\">\r\n");
	ViewBuilder.Append("            <div class=\"list\">\r\n");
	ViewBuilder.Append("              <div class=\"list-bd\">\r\n");

	loop__id=0;
	foreach(ExamResult item in examresultlist)
	{
	loop__id++;

	ViewBuilder.Append("                <div class=\"exercise\">\r\n");

	if (item.status==0)
	{

	ViewBuilder.Append("                  <div class=\"pull-right\">\r\n");
	ViewBuilder.Append("                      <span class=\"btn btn-link\" style=\"color:#ff0000\">未完成</span> \r\n");
	ViewBuilder.Append("                      <span class=\"btn btn-normal open-exercise\"><span onclick=\"ExamContinu(" + item.id.ToString().Trim() + ");\" class=\"btn-inner\" style=\"color:#ff0000\">继续考试</span></span>\r\n");
	ViewBuilder.Append("                  </div>\r\n");
	ViewBuilder.Append("                  <div class=\"name\"><a href=\"exam.aspx?resultid=" + item.id.ToString().Trim() + "\" target=\"_blank\" style=\"color:#ff0000\">" + item.examname.ToString().Trim() + "(未完成)</a></div>\r\n");

	}
	else
	{

	ViewBuilder.Append("                  <div class=\"pull-right\">\r\n");
	ViewBuilder.Append("                    <a href=\"examresult.aspx?resultid=" + item.id.ToString().Trim() + "\" target=\"_blank\" class=\"btn btn-link link-button\"><span class=\"btn-inner\">考试分析</span></a>&nbsp;&nbsp;\r\n");
	ViewBuilder.Append("                    <a href=\"examanswer.aspx?resultid=" + item.id.ToString().Trim() + "\" target=\"_blank\" class=\"btn btn-link link-button\"><span class=\"btn-inner\">答案解析</span></a>\r\n");
	ViewBuilder.Append("                  </div>\r\n");
	ViewBuilder.Append("                  <div class=\"name\"><a href=\"examresult.aspx?resultid=" + item.id.ToString().Trim() + "\" target=\"_blank\">" + item.examname.ToString().Trim() + "(得分：" + item.score.ToString().Trim() + "分)</a></div>\r\n");

	}	//end if

	ViewBuilder.Append("                  <div class=\"meta\"> <span class=\"muted\">考试时间: " + FangPage.MVC.FPUtils.GetDate(item.starttime,"yyyy年MM月dd日 HH:mm") + "</span> <span class=\"muted\"> </span> </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end loop

	ViewBuilder.Append("              </div>\r\n");
	ViewBuilder.Append("              <div class=\"list-ft\"> </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");

	ViewBuilder.Append("<div class=\"footer repeat-x default-b\">\r\n");
	ViewBuilder.Append("    <div class=\"footer-left layout default-lb\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"footer-right layout default-rb\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"container\">\r\n");
	ViewBuilder.Append("        <div class=\"links\">\r\n");
	ViewBuilder.Append("            <p class=\"text-center\">\r\n");
	ViewBuilder.Append("               Copyright &copy; 2013-" + FangPage.MVC.FPUtils.GetDate(datetime,"yyyy") + " <a target=\"_blank\" href=\"http://www.fangpage.com\">方配软件(FangPage.Com)</a>&nbsp;&nbsp;版权所有，版本：" + siteconfig.version.ToString().Trim() + "\r\n");
	ViewBuilder.Append("            </p>\r\n");
	ViewBuilder.Append("            <p class=\"text-center\">官方网站：<a href=\"http://www.fangpage.com\" target=\"_blank\">http://www.fangpage.com</a>，QQ：12677206，电话：13481092810</p>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</div>\r\n");


	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
