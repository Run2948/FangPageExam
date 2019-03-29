<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.examreport" %>
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
	ViewBuilder.Append("<title>综合能力报告 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/report.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("<script src=\"" + plupath.ToString() + "jqchart/jquery.jqChart.min.js\" type=\"text/javascript\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<!--[if IE]>\r\n");
	ViewBuilder.Append("    <script lang=\"javascript\" type=\"text/javascript\" src=\"" + plupath.ToString() + "jqchart/canvas.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<![endif]-->\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + plupath.ToString() + "progressbar/jquery.progressbar.min.js\"></");
	ViewBuilder.Append("script>\r\n");

	ViewBuilder.Append("<!--[if lte IE 8]><link id=\"ie8Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie8.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if lte IE 7]><link id=\"ie7Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/lte-ie7.css\")}\"/><![endif]-->\r\n");
	ViewBuilder.Append("<!--[if gte IE 9]><link id=\"ie9Hack\" rel=\"stylesheet\" href=\"#{static(\"common/hack/gte-ie9.css\")}\"/><![endif]-->\r\n");


	ViewBuilder.Append("<!--[if gte IE 9]>\r\n");
	ViewBuilder.Append("  <style type=\"text/css\">\r\n");
	ViewBuilder.Append("    .gradient {\r\n");
	ViewBuilder.Append("       filter: none;\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("  </style>\r\n");
	ViewBuilder.Append("<![endif]-->\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(document).ready(function () {\r\n");
	ViewBuilder.Append("        $('#jqchart').jqChart({\r\n");
	ViewBuilder.Append("            title: { text: '考试成绩趋势图' },\r\n");
	ViewBuilder.Append("            axes: [\r\n");
	ViewBuilder.Append("                {\r\n");
	ViewBuilder.Append("                    location: 'left',//y轴位置，取值：left,right\r\n");
	ViewBuilder.Append("                    minimum: 0,//y轴刻度最小值\r\n");
	ViewBuilder.Append("                    maximum: 100,//y轴刻度最大值\r\n");
	ViewBuilder.Append("                    interval: 10//刻度间距\r\n");
	ViewBuilder.Append("                }\r\n");
	ViewBuilder.Append("            ],\r\n");
	ViewBuilder.Append("            series: [\r\n");
	ViewBuilder.Append("                //数据1开始\r\n");
	ViewBuilder.Append("                {\r\n");
	ViewBuilder.Append("                    type: 'line',//图表类型，取值：column 柱形图，line 线形图\r\n");
	ViewBuilder.Append("                    title: '分数',//标题\r\n");
	ViewBuilder.Append("                    data: [" + examresult.ToString() + "]//数据内容，格式[[x轴标题,数值1],[x轴标题,数值2],......]\r\n");
	ViewBuilder.Append("                },\r\n");
	ViewBuilder.Append("                //数据1结束		\r\n");
	ViewBuilder.Append("            ]\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $(\".progressBar\").progressBar({ showText: true, barImage: '" + plupath.ToString() + "progressbar/images/progressbg_red.gif' });\r\n");
	ViewBuilder.Append("        $('.sprite').click(function () {   // 获取所谓的父行\r\n");
	ViewBuilder.Append("            $(this).toggleClass(\"sprite-selected\");  // 添加/删除图标\r\n");
	ViewBuilder.Append("            $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
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
	ViewBuilder.Append("    <div class=\"report-wrap user-report-wrap row\">\r\n");
	ViewBuilder.Append("      <div class=\"span12 meta-area\">\r\n");
	ViewBuilder.Append("        <div class=\"box-wrap score-box\">\r\n");
	ViewBuilder.Append("          <div class=\"box\">\r\n");
	ViewBuilder.Append("            <div class=\"box-bd clearfix\">\r\n");
	ViewBuilder.Append("              <div class=\"user-score\">\r\n");
	ViewBuilder.Append("                <div class=\"left-column pull-left\">\r\n");
	ViewBuilder.Append("                  <div class=\"score-info forecast-score\">\r\n");
	ViewBuilder.Append("                    <div class=\"report-circle avg sprite-blue-circle\"><span class=\"number text-xxlarge\">" + avg_my.ToString() + "</span><span class=\"unit\">分</span> </div>\r\n");
	ViewBuilder.Append("                    <div class=\"lbl-wrap\"> <span class=\"lbl-large\">考试平均分</span></div>\r\n");
	ViewBuilder.Append("                  </div>\r\n");
	ViewBuilder.Append("                  <div class=\"item-row\"> <span class=\"lbl\">全站排名：</span> <span class=\"score\"><span class=\"number user-index\">" + avg_display.ToString() + "</span><span class=\"number total-user\"> / " + examusers.ToString() + "<span class=\"unit\">名</span></span></span> </div>\r\n");
	ViewBuilder.Append("                  <div class=\"item-row last\"> <span class=\"lbl\">全站平均分：</span> <span class=\"score\"><span class=\"number\">" + avg_total.ToString() + "</span><span class=\"unit\">分</span></span> </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div class=\"middle-column pull-left\">\r\n");
	ViewBuilder.Append("                  <div class=\"question-count\">\r\n");
	ViewBuilder.Append("                    <div class=\"report-circle avg sprite-red-circle\"> <span class=\"number text-xxlarge\">" + accuracy_my.ToString() + "</span> <span class=\"unit\">%</span> </div>\r\n");
	ViewBuilder.Append("                    <div class=\"lbl-wrap\"> <span class=\"lbl-large\">答题正确率</span> </div>\r\n");
	ViewBuilder.Append("                  </div>\r\n");
	ViewBuilder.Append("                  <div class=\"item-row\"> <span class=\"lbl\">全站排名：</span> <span class=\"score\"><span class=\"number user-index\">" + accuracy_display.ToString() + "</span><span class=\"number total-user\"> / " + examusers.ToString() + "<span class=\"unit\">名</span></span></span> </div>\r\n");
	ViewBuilder.Append("                  <div class=\"item-row last\"> <span class=\"lbl\">全站总正确率：</span> <span class=\"score\"><span class=\"number\">" + accuracy_total.ToString() + "</span><span class=\"unit\">%</span></span> </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div class=\"right-column\">\r\n");
	ViewBuilder.Append("                  <div id=\"jqchart\" style=\"width:100%;height:340px;\" class=\"trend-image-wrap\">\r\n");
	ViewBuilder.Append("                  </div>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("      <div class=\"span12\">\r\n");
	ViewBuilder.Append("        <div class=\"box-wrap\">\r\n");
	ViewBuilder.Append("          <div class=\"box\">\r\n");
	ViewBuilder.Append("            <div class=\"box-bd clearfix\">\r\n");
	ViewBuilder.Append("              <h3 class=\"text-label text-label-green\"><span class=\"text-label-inner bold\">能力图表</span></h3><h3 class=\"text-label text-label-green\"><span class=\"text-label-inner bold\">能力等级:" + user.UserGrade.name.ToString().Trim() + "</span></h3>\r\n");
	ViewBuilder.Append("              <div class=\"user-csk-table-wrap csk-table-wrap\">\r\n");
	ViewBuilder.Append("                <table class=\"csk-table table\">\r\n");
	ViewBuilder.Append("                  <thead>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                      <th class=\"name-col\">知识点</th>\r\n");
	ViewBuilder.Append("                      <th class=\"count-col\">答题量</th>\r\n");
	ViewBuilder.Append("                      <th class=\"count-col\">正确题</th>\r\n");
	ViewBuilder.Append("                      <th class=\"capacity-col last\">正确率</th>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                  </thead>\r\n");
	ViewBuilder.Append("                  <tbody>\r\n");

	loop__id=0;
	foreach(ExamLogInfo item in examloglist)
	{
	loop__id++;


	if (item.subcounts>0)
	{

	ViewBuilder.Append("                    <tr class=\"keypoint keypoint-level-0\">\r\n");
	ViewBuilder.Append("                      <td class=\"name-col\"><span class=\"text toggle-expand\"><span id=\"row_" + item.sortid.ToString().Trim() + "\" class=\"sprite sprite-expand\"></span>" + item.sortname.ToString().Trim() + "</span></td>\r\n");
	ViewBuilder.Append("                      <td class=\"count-col\">" + item.answers.ToString().Trim() + "道/" + item.questions.ToString().Trim() + "道</td>\r\n");
	ViewBuilder.Append("                      <td class=\"count-col\">" + (item.answers-item.wrongs).ToString().Trim() + "道</td>\r\n");
	ViewBuilder.Append("                      <td class=\"capacity-col\">\r\n");
	ViewBuilder.Append("                          <span class=\"progressBar\">" + item.accuracy.ToString().Trim() + "%</span>\r\n");
	ViewBuilder.Append("                      </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    " + GetChildSort(channelinfo.id,item.sortid,1).ToString() + "\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <tr class=\"keypoint keypoint-level-0\">\r\n");
	ViewBuilder.Append("                      <td class=\"name-col\"><span class=\"text toggle-expand\"><span class=\"sprite sprite-expand sprite-noexpand\"></span>" + item.sortname.ToString().Trim() + "</span></td>\r\n");
	ViewBuilder.Append("                      <td class=\"count-col\">" + item.answers.ToString().Trim() + "道/" + item.questions.ToString().Trim() + "道</td>\r\n");
	ViewBuilder.Append("                      <td class=\"count-col\">" + (item.answers-item.wrongs).ToString().Trim() + "道</td>\r\n");
	ViewBuilder.Append("                      <td class=\"capacity-col\"><span class=\"progressBar\">" + item.accuracy.ToString().Trim() + "%</span></td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                  </tbody>\r\n");
	ViewBuilder.Append("                </table>\r\n");
	ViewBuilder.Append("              </div>\r\n");
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
