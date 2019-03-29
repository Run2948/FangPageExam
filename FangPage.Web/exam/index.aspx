<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller.index" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FP_Exam" %>
<%@ Import namespace="FP_Exam.Model" %>

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
	ViewBuilder.Append("    <meta name=\"renderer\" content=\"webkit\">\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge,chrome=1\">\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n");
	ViewBuilder.Append("    <title>" + pagetitle.ToString() + " " + siteconfig.version.ToString().Trim() + " - Powered By FangPage.COM</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/index.css\">\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-1.8.2.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");

	ViewBuilder.Append("<!--[if lte IE 8]><link id=\"ie8Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie8.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if lte IE 7]><link id=\"ie7Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie7.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if gte IE 9]><link id=\"ie9Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/gte-ie9.css\")}\"/><![endif]-->\r\n");


	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            $(\"#instant\").click(function () {\r\n");
	ViewBuilder.Append("                window.open(\"testview_instant.aspx\");\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#csk\").click(function () {\r\n");
	ViewBuilder.Append("                window.open(\"testview_csk.aspx\");\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#paper\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href=\"examlist.aspx?channelid=2\";\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("            $(\"#myexam\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href = \"examlist.aspx?channelid=3\";\r\n");
	ViewBuilder.Append("            })\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("    <div class=\"wrap\">\r\n");

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


	ViewBuilder.Append("        <div class=\"container body-wrap main\">\r\n");
	ViewBuilder.Append("            <div class=\"home-wrap\">\r\n");
	ViewBuilder.Append("                <div class=\"section-wrap false false\">\r\n");
	ViewBuilder.Append("                    <div class=\"section\">\r\n");
	ViewBuilder.Append("                        <span class=\"sprite pull-left sprite-section-smart-15\"></span>\r\n");
	ViewBuilder.Append("                        <div class=\"overflow\">\r\n");
	ViewBuilder.Append("                            <h2><span class=\"fir fir-title\"><span class=\"fir-text\">随机练习</span></span></h2>\r\n");
	ViewBuilder.Append("                            <div class=\"content\">\r\n");
	ViewBuilder.Append("                                <p>覆盖所有题库，综合随机组卷，提升您的综合能力</p>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"button-row\">\r\n");
	ViewBuilder.Append("                                <span id=\"instant\" class=\"btn btn-primary create-exercise\">\r\n");
	ViewBuilder.Append("                                    <div class=\"btn-inner\">随机练习</div>\r\n");
	ViewBuilder.Append("                                </span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div class=\"section-wrap false section-right\">\r\n");
	ViewBuilder.Append("                    <div class=\"section\">\r\n");
	ViewBuilder.Append("                        <span class=\"sprite pull-left sprite-section-csk\"></span>\r\n");
	ViewBuilder.Append("                        <div class=\"overflow\">\r\n");
	ViewBuilder.Append("                            <h2><span class=\"fir fir-title\"><span class=\"fir-text\">强化练习</span></span></h2>\r\n");
	ViewBuilder.Append("                            <div class=\"content\">\r\n");
	ViewBuilder.Append("                                <p>自主选择专项或具体考点，各个击破</p>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"button-row\">\r\n");
	ViewBuilder.Append("                                <span id=\"csk\" class=\"btn btn-primary select-csk\"><span class=\"btn-inner\">强化练习</span></span></div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div class=\"section-wrap false false\">\r\n");
	ViewBuilder.Append("                    <div class=\"section\">\r\n");
	ViewBuilder.Append("                        <span class=\"sprite pull-left sprite-section-smart-paper\"></span>\r\n");
	ViewBuilder.Append("                        <div class=\"overflow\">\r\n");
	ViewBuilder.Append("                            <h2><span class=\"fir fir-title\"><span class=\"fir-text\">模拟考试</span></span></h2>\r\n");
	ViewBuilder.Append("                            <div class=\"content\">\r\n");
	ViewBuilder.Append("                                <p>系统为你提供历年考试的真题试卷进行模拟考试</p>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"button-row\">\r\n");
	ViewBuilder.Append("                                <span id=\"paper\" class=\"btn btn-primary link-button gaq\">\r\n");
	ViewBuilder.Append("                                    <div class=\"btn-inner\">模拟考试</div>\r\n");
	ViewBuilder.Append("                                </span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div class=\"section-wrap false false\">\r\n");
	ViewBuilder.Append("                    <div class=\"section\">\r\n");
	ViewBuilder.Append("                        <span class=\"sprite pull-left sprite-section-continue\"></span>\r\n");
	ViewBuilder.Append("                        <div class=\"overflow\">\r\n");
	ViewBuilder.Append("                            <h2><span class=\"fir fir-title\"><span class=\"fir-text\">正式考试</span></span></h2>\r\n");
	ViewBuilder.Append("                            <div class=\"content\">\r\n");
	ViewBuilder.Append("                                <p>属于您自己的正式考试</p>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                            <div class=\"button-row\">\r\n");
	ViewBuilder.Append("                                <span id=\"myexam\" class=\"btn btn-primary link-button gaq\">\r\n");
	ViewBuilder.Append("                                    <div class=\"btn-inner\">正式考试</div>\r\n");
	ViewBuilder.Append("                                </span>\r\n");
	ViewBuilder.Append("                            </div>\r\n");
	ViewBuilder.Append("                        </div>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("            <div class=\"hide\"></div>\r\n");
	ViewBuilder.Append("        </div>\r\n");

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


	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
