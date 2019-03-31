<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller.exam" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FP_Exam" %>
<%@ Import namespace="FP_Exam.Model" %>

<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.9*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"zh-CN\" lang=\"zh-CN\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta name=\"renderer\" content=\"webkit\">\r\n");
	ViewBuilder.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge,chrome=1\">\r\n");
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n");
	ViewBuilder.Append("<title>" + examresult.examname.ToString().Trim() + " - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/exam.css\">\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-1.8.2.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-ui.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery.form.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/popup.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/exam.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery.nicescroll.min.js\"></");
	ViewBuilder.Append("script>\r\n");

	if (examinfo.iscopy==1)
	{


	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    if (window.Event)\r\n");
	ViewBuilder.Append("        function nocontextmenu(e) {\r\n");
	ViewBuilder.Append("        var ev = e ? e : window.event;\r\n");
	ViewBuilder.Append("        ev.cancelBubble = true\r\n");
	ViewBuilder.Append("        ev.returnValue = false;\r\n");
	ViewBuilder.Append("        if (ev.preventDefault) {\r\n");
	ViewBuilder.Append("            ev.preventDefault();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        if (ev.stopPropagation) {\r\n");
	ViewBuilder.Append("            ev.stopPropagation();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        return false;\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function nocopy(e) {\r\n");
	ViewBuilder.Append("        var ev = e ? e : window.event;\r\n");
	ViewBuilder.Append("        ev.cancelBubble = true\r\n");
	ViewBuilder.Append("        ev.returnValue = false;\r\n");
	ViewBuilder.Append("        if (ev.preventDefault) {\r\n");
	ViewBuilder.Append("            ev.preventDefault();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        if (ev.stopPropagation) {\r\n");
	ViewBuilder.Append("            ev.stopPropagation();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        return false;\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function norightclick(e) {\r\n");
	ViewBuilder.Append("        if (window.Event) {\r\n");
	ViewBuilder.Append("            if (e.which == 2 || e.which == 3)\r\n");
	ViewBuilder.Append("                return false;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        else\r\n");
	ViewBuilder.Append("            if (event.button == 2 || event.button == 3) {\r\n");
	ViewBuilder.Append("                event.cancelBubble = true;\r\n");
	ViewBuilder.Append("                event.returnvalue = false;\r\n");
	ViewBuilder.Append("                return false;\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    document.oncontextmenu = nocontextmenu; // for IE5+ \r\n");
	ViewBuilder.Append("    document.oncopy = nocopy;\r\n");
	ViewBuilder.Append("    document.onkeydown = function (event) //shield F5  //shift+F10 ctrl+R\r\n");
	ViewBuilder.Append("    {\r\n");
	ViewBuilder.Append("        event = event ? event : (window.event ? window.event : null); // ie firefox\r\n");
	ViewBuilder.Append("        if (event.keyCode == 116 || (event.shiftKey && event.keyCode == 121) || (event.ctrlKey && event.keyCode == 82)) {\r\n");
	ViewBuilder.Append("            event.keyCode = 0;\r\n");
	ViewBuilder.Append("            event.cancelBubble = true;\r\n");
	ViewBuilder.Append("            event.returnValue = false;\r\n");
	ViewBuilder.Append("            if (event && event.preventDefault)\r\n");
	ViewBuilder.Append("                event.preventDefault();\r\n");
	ViewBuilder.Append("            else\r\n");
	ViewBuilder.Append("                window.event.returnValue = false;\r\n");
	ViewBuilder.Append("            return false;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");



	}	//end if

	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    function nice() {\r\n");
	ViewBuilder.Append("        window.niceObj = $(\".rnlt2\").niceScroll({ cursorcolor: \"#6E737B\", cursoropacitymin: 1, cursorwidth: \"6px\", cursorborder: \"none\", cursorborderradius: \"4px\" });\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function niceback(b) {\r\n");
	ViewBuilder.Append("        if (!window.ascrail2000) return;\r\n");
	ViewBuilder.Append("        if (b) {\r\n");
	ViewBuilder.Append("            window.ascrail2000.css({\r\n");
	ViewBuilder.Append("                \"position\": \"fixed\",\r\n");
	ViewBuilder.Append("                \"z-index\": '9999',\r\n");
	ViewBuilder.Append("                'top': '100px'\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        } else {\r\n");
	ViewBuilder.Append("            window.ascrail2000.css({\r\n");
	ViewBuilder.Append("                \"position\": \"absolute\",\r\n");
	ViewBuilder.Append("                \"z-index\": '9999',\r\n");
	ViewBuilder.Append("                'top': '170px'\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        return true;\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        var ipt = $(\"label input\");\r\n");
	ViewBuilder.Append("        ipt.parent().removeClass(\"sd\");\r\n");
	ViewBuilder.Append("        ipt.filter(\":checked\").parent().addClass(\"sd\");\r\n");
	ViewBuilder.Append("        nice();\r\n");
	ViewBuilder.Append("        window.ascrail2000 = $('#ascrail2000');\r\n");
	ViewBuilder.Append("        $('.rnav').mouseover(function () {\r\n");
	ViewBuilder.Append("            niceback($('.hbx1').hasClass(\"fixed\"));\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    });\r\n");

	if (examinfo.iscopy==1)
	{

	ViewBuilder.Append("    document.oncontextmenu = new Function('event.returnValue=false;');\r\n");
	ViewBuilder.Append("    document.onselectstart = new Function('event.returnValue=false;');\r\n");

	}	//end if

	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    (window != top) && $(function () {\r\n");
	ViewBuilder.Append("        var rnav = $(\".rnav\");\r\n");
	ViewBuilder.Append("        $(window.parent).bind(\"scroll\", function () {\r\n");
	ViewBuilder.Append("            var tpd = $(parent.document.getElementById('frameContent')).offset().top;\r\n");
	ViewBuilder.Append("            rnav.css({\r\n");
	ViewBuilder.Append("                top: Math.max($(this).scrollTop() - tpd + 10, 52) + \"px\"\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        }).scroll();\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<noscript>\r\n");
	ViewBuilder.Append("   <iframe src=\"*\"></iframe>\r\n");
	ViewBuilder.Append("</noscript>\r\n");
	ViewBuilder.Append("<div class=\"hbx1\">\r\n");
	ViewBuilder.Append("  <div class=\"hbx2\">\r\n");
	ViewBuilder.Append("    <div class=\"hbx3\"><img src=\"" + webpath.ToString() + "sites/exam/statics/images/top.jpg\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"hbx4\">\r\n");
	ViewBuilder.Append("      <div class=\"fr\"><a href=\"javascript:;\" class=\"btnq1\" onclick=\"extemporeSave()\">保存答案</a><a href=\"#\" class=\"btnq2\" onclick=\"submitExam();return false;\">我要交卷</a> </div>\r\n");
	ViewBuilder.Append("      <span class=\"theTime\" id=\"thetime\">" + thetime.ToString() + "</span>\r\n");

	if (examresult.islimit==1)
	{

	ViewBuilder.Append("      <span class=\"line1\"></span><span class=\"write\">考试时间：" + FangPage.MVC.FPUtils.GetDate(examresult.starttime,"yyyy-MM-dd HH:mm") + "至" + FangPage.MVC.FPUtils.GetDate(examresult.endtime,"yyyy-MM-dd HH:mm") + "</span>\r\n");

	}
	else
	{

	ViewBuilder.Append("      <span class=\"line1\"></span><span class=\"write\">答题时间：" + examresult.examtime.ToString().Trim() + "分钟</span>\r\n");

	}	//end if

	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div class=\"hbx2\">\r\n");
	ViewBuilder.Append("<div class=\"rnav\">\r\n");
	ViewBuilder.Append("    <div class=\"rnavhd\">答题卡</div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavct\">\r\n");
	ViewBuilder.Append("      <ul class=\"rnlt1 fc\">\r\n");
	ViewBuilder.Append("        <li><span class=\"bg1\"></span>已答题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg3\"></span>未答题</li>\r\n");
	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("      <ul class=\"rnlt2 fc\" tabindex=\"5000\" style=\"overflow-y: hidden; outline: none;\">\r\n");
	int en = 0;
	

	loop__id=0;
	foreach(int examtopic in questionlist)
	{
	loop__id++;

	 en = en+1;
	
	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" id=\"fc_" + en.ToString() + "\" class=\"bg3\">" + en.ToString() + "</a></li>\r\n");

	}	//end loop

	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("      <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("          $(function () {\r\n");
	ViewBuilder.Append("              $('.rnlt2 a').click(function () {\r\n");
	ViewBuilder.Append("                  var top = $($(this).attr('href')).offset().top+120;\r\n");
	ViewBuilder.Append("                  $(window).scrollTop(top);\r\n");
	ViewBuilder.Append("                  return false;\r\n");
	ViewBuilder.Append("              });\r\n");
	ViewBuilder.Append("          });\r\n");
	ViewBuilder.Append("	</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavft\"></div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div class=\"wp wpa\">\r\n");
	ViewBuilder.Append("  <div class=\"wp2\">\r\n");
	ViewBuilder.Append("    <div class=\"wp3\">\r\n");
	ViewBuilder.Append("      <div class=\"wp4 wp4_none\">\r\n");
	ViewBuilder.Append("        <h1 class=\"qtTitle\">" + examresult.examname.ToString().Trim() + "" + GetPaper(examresult.paper).ToString() + "</h1>\r\n");
	ViewBuilder.Append("        <div class=\"bx1 pd1m mb20\">\r\n");
	ViewBuilder.Append("          <div>\r\n");
	ViewBuilder.Append("            <table class=\"tab1\">\r\n");
	ViewBuilder.Append("              <tbody>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                  <td>考生姓名：\r\n");

	if (user.realname!="")
	{

	ViewBuilder.Append("" + user.realname.ToString().Trim() + "\r\n");

	}
	else
	{

	ViewBuilder.Append("" + user.username.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("</td>\r\n");
	ViewBuilder.Append("                  <td>试卷总分：" + examresult.total.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                  <td>及格分数：" + examresult.passmark.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                  <td>答题时间：" + examresult.examtime.ToString().Trim() + "分钟</td>\r\n");
	ViewBuilder.Append("                  <td>答题总数：" + examresult.questions.ToString().Trim() + "题</td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("              </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <form id=\"testProcessForm\" name=\"testProcessForm\" action=\"exampost.aspx\" method=\"post\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"resultid\" name=\"resultid\" value=\"" + resultid.ToString() + "\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"starttime\" value=\"" + examresult.starttime.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"utime\" name=\"utime\" value=\"0\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"autotime\" value=\"" + examconfig.autotime.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"examtime\" value=\"" + examresult.examtime.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"isexam\" value=\"1\">\r\n");
	int topicnum = 0;
	

	loop__id=0;
	foreach(ExamResultTopic examtopic in examtopiclist)
	{
	loop__id++;


	if (examtopic.questions>0)
	{

	ViewBuilder.Append("            <input type=\"hidden\" id=\"qidlist_" + examtopic.id.ToString().Trim() + "\" name=\"qidlist_" + examtopic.id.ToString().Trim() + "\" value=\"" + examtopic.questionlist.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("            <div id=\"1\" class=\"tit1 pd1\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"tit1 pd1\">" + examtopic.title.ToString().Trim() + "<span class=\"ft3\">(共" + examtopic.questions.ToString().Trim() + "题，每题" + examtopic.perscore.ToString().Trim() + "分，共" + (examtopic.questions*examtopic.perscore).ToString().Trim() + "分)</span></div>\r\n");

	loop__id=0;
	foreach(ExamQuestion item in GetQuestionList(examtopic))
	{
	loop__id++;

	 topicnum = topicnum+1;
	

	if (item.type==1)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "\r\n");

	if (examtopic.type==6)
	{

	ViewBuilder.Append("（单选）\r\n");

	}	//end if

	ViewBuilder.Append("</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                " + Option(item.option,item.ascount,item.optionlist).ToString() + "\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd class=\"dAn fc\">\r\n");
	ViewBuilder.Append("                <span class=\"ft4 fl\">选择答案：</span>\r\n");
	ViewBuilder.Append("                 <span class=\"fl w2 bx7\">\r\n");

	loop__id=0;
	foreach(string str in answerarr)
	{
	loop__id++;


	if (loop__id<=item.ascount)
	{

	ViewBuilder.Append("                  <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" \r\n");

	if (str==item.useranswer)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"" + str.ToString() + "\">" + str.ToString() + "</label>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                  </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==2)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "\r\n");

	if (examtopic.type==6)
	{

	ViewBuilder.Append("（多选）\r\n");

	}	//end if

	ViewBuilder.Append("</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                " + Option(item.option,item.ascount,item.optionlist).ToString() + "\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd class=\"dAn fc\">\r\n");
	ViewBuilder.Append("                <span class=\"ft4 fl\">选择答案：</span>\r\n");
	ViewBuilder.Append("                 <span class=\"fl w2 bx7\">\r\n");

	loop__id=0;
	foreach(string str in answerarr)
	{
	loop__id++;


	if (loop__id<=item.ascount)
	{

	ViewBuilder.Append("                  <label><input type=\"checkbox\" id=\"_" + topicnum.ToString() + "\" \r\n");

	if (ischecked(str,item.useranswer))
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"" + str.ToString() + "\">" + str.ToString() + "</label>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                  </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==3)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd class=\"dAn fc\">\r\n");
	ViewBuilder.Append("                <span class=\"ft4 fl\">选择答案：</span>\r\n");
	ViewBuilder.Append("                 <span class=\"fl w2 bx7\">\r\n");

	if (item.useranswer=="Y")
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" checked=\"checked\" value=\"Y\">正确</label>\r\n");

	}
	else
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"Y\">正确</label>\r\n");

	}	//end if


	if (item.useranswer=="N")
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" checked=\"checked\" value=\"N\">错误</label>\r\n");

	}
	else
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"N\">错误</label>\r\n");

	}	//end if

	ViewBuilder.Append("                 </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==4)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + FmAnswer(item.title,item.id,item.useranswer,topicnum).ToString() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==5)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_" + item.id.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">填写答案</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" rows=\"5\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\">" + item.useranswer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==6)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_" + item.id.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p><img src=\"" + GetTxtImg(item.title,item.id).ToString() + "\"></p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">填写答案</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" rows=\"5\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\">" + item.useranswer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}	//end if


	}	//end if


	}	//end loop


	}	//end loop

	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <div class=\"ta_c mb10\"><a href=\"javascript:void()\" class=\"btnq3\" onclick=\"submitExam();return false;\">我要交卷</a></div>\r\n");
	ViewBuilder.Append("        <div style=\"clear:both;\"></div>\r\n");
	ViewBuilder.Append("      </form>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
