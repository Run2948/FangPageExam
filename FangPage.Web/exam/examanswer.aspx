<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.examanswer" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.Exam" %>
<%@ Import namespace="FangPage.Exam.Model" %>

<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.9*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"zh-CN\" lang=\"zh-CN\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("<title>答案解析 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/exam.css\">\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-1.8.2.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-ui.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/popup.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery.form.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/exam.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery.nicescroll.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");

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


	ViewBuilder.Append("</head>\r\n");
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
	ViewBuilder.Append("                'top': '235px'\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        } else {\r\n");
	ViewBuilder.Append("            window.ascrail2000.css({\r\n");
	ViewBuilder.Append("                \"position\": \"absolute\",\r\n");
	ViewBuilder.Append("                \"z-index\": '9999',\r\n");
	ViewBuilder.Append("                'top': '305px'\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        return true;\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        var ipt = $(\"label input\");\r\n");
	ViewBuilder.Append("        ipt.parent().removeClass(\"sd\");\r\n");
	ViewBuilder.Append("        ipt.filter(\":checked\").parent().addClass(\"sd\");\r\n");
	ViewBuilder.Append("        layer.use('extend/layer.ext.js');//弹出层插件\r\n");
	ViewBuilder.Append("        nice();\r\n");
	ViewBuilder.Append("        window.ascrail2000 = $('#ascrail2000');\r\n");
	ViewBuilder.Append("        $('.rnav').mouseover(function () {\r\n");
	ViewBuilder.Append("            niceback($('.hbx1').hasClass(\"fixed\"));\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("    function AddFav(qid, action) {\r\n");
	ViewBuilder.Append("        $.post(\"favajax.aspx\", {\r\n");
	ViewBuilder.Append("            qid: qid,\r\n");
	ViewBuilder.Append("            action: action\r\n");
	ViewBuilder.Append("        }, function (data) {\r\n");
	ViewBuilder.Append("            if (data.error > 0) {\r\n");
	ViewBuilder.Append("                alert(data.message);\r\n");
	ViewBuilder.Append("                return;\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            if (data.action == -1) {\r\n");
	ViewBuilder.Append("                $(\"#fav_\" + qid).html(\"<a href=\\\"javascript:AddFav(\" + qid + \",1);\\\">收藏本题</a>\");\r\n");
	ViewBuilder.Append("                layer.msg('取消收藏成功!', 2, -1);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else if (data.action =1) {\r\n");
	ViewBuilder.Append("                $(\"#fav_\" + qid).html(\"<a href=\\\"javascript:AddFav(\" + qid + \",-1);\\\">取消收藏</a>\");\r\n");
	ViewBuilder.Append("                layer.msg('收藏本题成功!', 2, -1);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }, \"json\");\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function EditNote(qid, num) {\r\n");
	ViewBuilder.Append("        var note=$('#note_' + qid).html();\r\n");
	ViewBuilder.Append("        layer.prompt({type: 3,title: '(" + examresult.examname.ToString().Trim() + ")第' + num + '题笔记', val:note},function (val) {\r\n");
	ViewBuilder.Append("            $.post(\"noteajax.aspx\", {\r\n");
	ViewBuilder.Append("                qid: qid,\r\n");
	ViewBuilder.Append("                note: val\r\n");
	ViewBuilder.Append("            }, function (data) {\r\n");
	ViewBuilder.Append("                if (data.error > 0) {\r\n");
	ViewBuilder.Append("                    alert(data.message);\r\n");
	ViewBuilder.Append("                    return;\r\n");
	ViewBuilder.Append("                }\r\n");
	ViewBuilder.Append("                $('#note_' + qid).html(val);\r\n");
	ViewBuilder.Append("                $('#shownote_' + qid).show();\r\n");
	ViewBuilder.Append("                layer.msg('笔记保存成功!', 2, -1);\r\n");
	ViewBuilder.Append("            }, \"json\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<noscript>\r\n");
	ViewBuilder.Append("    <iframe src=\"*\"></iframe>\r\n");
	ViewBuilder.Append("</noscript>\r\n");
	ViewBuilder.Append("<div class=\"hbx1\">\r\n");
	ViewBuilder.Append("  <div class=\"hbx2\">\r\n");
	ViewBuilder.Append("    <div class=\"hbx3\"><img src=\"" + webpath.ToString() + "sites/exam/statics/images/top.jpg\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"hbx4 hbx4a\">\r\n");
	ViewBuilder.Append("      <div class=\"fr\"> \r\n");
	ViewBuilder.Append("        <a href=\"javascript:;\" class=\"btnq1\" onclick=\"window.print()\">打印</a>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("      <a id=\"analisisLink\" href=\"examresult.aspx?resultid=" + resultid.ToString() + "\" class=\"tab3\">考试分析</a> <span class=\"tab3 tab3a\">答案解析</span> </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div class=\"hbx2\">\r\n");
	ViewBuilder.Append("  <div class=\"rnav\">\r\n");
	ViewBuilder.Append("    <div class=\"rnavhd\">试题卡</div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavct\">\r\n");
	ViewBuilder.Append("      <div class=\"mb10\"> 试题数：" + examresult.questions.ToString().Trim() + "题<br>\r\n");
	ViewBuilder.Append("        错题数：" + examresult.wrongs.ToString().Trim() + "题<br>\r\n");
	ViewBuilder.Append("        未答数：" + examresult.unanswer.ToString().Trim() + "题<br>\r\n");
	ViewBuilder.Append("        总得分：" + examresult.score.ToString().Trim() + "分 </div>\r\n");
	ViewBuilder.Append("      <ul class=\"rnlt1 fc\">\r\n");
	ViewBuilder.Append("        <li><span class=\"bg1\"></span>正确题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg2\"></span>错误题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg3\"></span>未答题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg4\"></span>主观题</li>\r\n");
	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("      <ul class=\"rnlt2 fc\" tabindex=\"5000\" style=\"overflow-y: hidden; outline: none;height:385px;\">\r\n");
	int en = 0;
	

	loop__id=0;
	foreach(ExamResultTopic examtopic in examtopicresultlist)
	{
	loop__id++;


	loop__id=0;
	foreach(ExamQuestion item in GetQuestionList(examtopic))
	{
	loop__id++;

	 en = en+1;
	

	if (item.type==5)
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" id=\"fc_" + en.ToString() + "\" class=\"bg4\">" + en.ToString() + "</a></li>\r\n");

	}
	else if (item.useranswer=="")
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" id=\"fc_" + en.ToString() + "\" class=\"bg3\">" + en.ToString() + "</a></li>\r\n");

	}
	else if (item.userscore>0)
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" id=\"fc_" + en.ToString() + "\" class=\"bg1\">" + en.ToString() + "</a></li>\r\n");

	}
	else
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" id=\"fc_" + en.ToString() + "\" class=\"bg2\">" + en.ToString() + "</a></li>\r\n");

	}	//end if


	}	//end loop


	}	//end loop

	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("      <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("          $(function () {\r\n");
	ViewBuilder.Append("              $('.rnlt2 a').click(function () {\r\n");
	ViewBuilder.Append("                  var top = $($(this).attr('href')).offset().top + 120;\r\n");
	ViewBuilder.Append("                  $(window).scrollTop(top);\r\n");
	ViewBuilder.Append("                  return false;\r\n");
	ViewBuilder.Append("              });\r\n");
	ViewBuilder.Append("          });\r\n");
	ViewBuilder.Append("	</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavft\"></div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("  <div class=\"wp wpa\">\r\n");
	ViewBuilder.Append("    <div class=\"wp2\">\r\n");
	ViewBuilder.Append("      <div class=\"wp3\">\r\n");
	ViewBuilder.Append("        <div class=\"wp4\">\r\n");
	ViewBuilder.Append("          <h1 class=\"t\" style=\"text-align:center;\">" + examresult.examname.ToString().Trim() + "</h1>\r\n");
	ViewBuilder.Append("          <div class=\"fc box\">\r\n");
	ViewBuilder.Append("            <div class=\"fl\">\r\n");
	ViewBuilder.Append("              <div class=\"img-sprite icon_count\">\r\n");
	ViewBuilder.Append("                <p class=\"count1\">您的分数</p>\r\n");
	ViewBuilder.Append("                <div class=\"count2\">" + examresult.score.ToString().Trim() + "</div>\r\n");
	ViewBuilder.Append("              </div>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("            <div class=\"fr\">\r\n");
	ViewBuilder.Append("              <p class=\"m\">\r\n");
	string ename = examresult.IUser.realname;
	

	if (ename=="")
	{

	 ename = examresult.IUser.username;
	

	}	//end if

	ViewBuilder.Append("                  考生：" + ename.ToString() + "，考试时间：" + FangPage.MVC.FPUtils.GetDate(examresult.examdatetime,"yyyy-MM-dd HH:mm:ss") + "\r\n");

	if (examresult.examid>0)
	{


	if (examresult.status==0)
	{

	ViewBuilder.Append("                  ，尚未完成答卷。\r\n");

	}
	else if (examresult.status==1)
	{

	ViewBuilder.Append("                  ，尚未阅卷。\r\n");

	}
	else if (examresult.status==2)
	{

	ViewBuilder.Append("                  ，已阅卷。\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("              </p>\r\n");
	ViewBuilder.Append("              <table>\r\n");
	ViewBuilder.Append("                <tr class=\"t\">\r\n");
	ViewBuilder.Append("                  <td>分数组成</td>\r\n");
	ViewBuilder.Append("                  <td>全体排名</td>\r\n");
	ViewBuilder.Append("                  <td>答卷耗时</td>\r\n");
	ViewBuilder.Append("                  <td>错题数量</td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                    <td>客观题" + examresult.score1.ToString().Trim() + "分 + 主观题" + examresult.score2.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                    <td>" + display.ToString() + "</td>\r\n");
	ViewBuilder.Append("                    <td>" + (examresult.utime/60+1).ToString().Trim() + "分钟</td>\r\n");
	ViewBuilder.Append("                    <td>" + examresult.wrongs.ToString().Trim() + "题</td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr class=\"t\">\r\n");
	ViewBuilder.Append("                    <td>试卷满分</td>\r\n");
	ViewBuilder.Append("                    <td>及格分数</td>\r\n");
	ViewBuilder.Append("                    <td>最高分数</td>\r\n");
	ViewBuilder.Append("                    <td>平均分数</td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                    <td>" + examresult.total.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                    <td>" + examresult.passmark.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                    <td>" + maxscore.ToString() + "分</td>\r\n");
	ViewBuilder.Append("                    <td>" + avgscore.ToString() + "分</td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("                  <tr>\r\n");
	ViewBuilder.Append("                    <td colspan=\"4\" style=\"text-align:left;width:100%\">&nbsp;&nbsp;&nbsp;&nbsp;\r\n");

	if (examresult.exnote!="")
	{

	ViewBuilder.Append(" 评语：" + examresult.exnote.ToString().Trim() + "，\r\n");

	}	//end if

	ViewBuilder.Append("                             获得经验值为" + examresult.exp.ToString().Trim() + "。\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                  </tr>\r\n");
	ViewBuilder.Append("              </table>\r\n");
	ViewBuilder.Append("            </div>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("          <!--head end -->\r\n");
	ViewBuilder.Append("          <form id=\"testProcessForm\" name=\"testProcessForm\" action=\"\" method=\"post\">\r\n");
	ViewBuilder.Append("            <input type=\"hidden\" name=\"resultid\" value=\"" + resultid.ToString() + "\">\r\n");
	ViewBuilder.Append("            <a id=\"1\"></a>\r\n");
	int topicnum = 0;
	

	loop__id=0;
	foreach(ExamResultTopic examtopic in examtopicresultlist)
	{
	loop__id++;


	if (examtopic.questions>0)
	{

	ViewBuilder.Append("            <div class=\"tit1 pd1\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"tit1 pd1\">" + examtopic.title.ToString().Trim() + "<span class=\"ft3\">(共" + examtopic.questions.ToString().Trim() + "题，每题" + examtopic.perscore.ToString().Trim() + "分，共" + (examtopic.questions*examtopic.perscore).ToString().Trim() + "分)</span></div>\r\n");

	loop__id=0;
	foreach(ExamQuestion item in GetQuestionList(examtopic))
	{
	loop__id++;

	 topicnum = topicnum+1;
	

	if (item.type==1)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id='" + (topicnum+1).ToString() + "'>" + topicnum.ToString() + "</span>\r\n");
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
	ViewBuilder.Append("              <dd class=\"dAn fc\"><span class=\"ft4 fl\">您的答案：</span> \r\n");
	ViewBuilder.Append("               <span class=\"fl w2 bx7\">\r\n");

	loop__id=0;
	foreach(string str in answerarr)
	{
	loop__id++;


	if (loop__id<=item.ascount)
	{

	ViewBuilder.Append("                <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" \r\n");

	if (str==item.useranswer)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"" + str.ToString() + "\" disabled=\"disabled\">" + str.ToString() + "</label>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if ((examresult.showanswer==1&&examconfig.showanswer==1)||roleid==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：<span class=\"ft11 ftc1\">" + item.answer.ToString().Trim() + "</span></div>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">本题得分：\r\n");
	ViewBuilder.Append("                <span id='" + item.id.ToString().Trim() + "' class=\"bx5 dis_ib\">\r\n");
	ViewBuilder.Append("                " + item.userscore.ToString().Trim() + "分\r\n");
	ViewBuilder.Append("                </span>\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span style=\"color:#ff0000\" id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==2)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id='" + (topicnum+1).ToString() + "'>" + topicnum.ToString() + "</span>\r\n");
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
	ViewBuilder.Append("              <dd class=\"dAn fc\"><span class=\"ft4 fl\">您的答案：</span> \r\n");
	ViewBuilder.Append("               <span class=\"fl w2 bx7\">\r\n");

	loop__id=0;
	foreach(string str in answerarr)
	{
	loop__id++;


	if (loop__id<=item.ascount)
	{

	ViewBuilder.Append("                <label><input type=\"checkbox\" name=\"answer_" + item.id.ToString().Trim() + "\" \r\n");

	if (ischecked(str,item.useranswer))
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"" + str.ToString() + "\" disabled=\"disabled\">" + str.ToString() + "</label>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if ((examresult.showanswer==1&&examconfig.showanswer==1)||roleid==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：<span class=\"ft11 ftc1\">" + item.answer.ToString().Trim() + "</span></div>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">本题得分：\r\n");
	ViewBuilder.Append("                <span id='" + item.id.ToString().Trim() + "' class=\"bx5 dis_ib\">\r\n");
	ViewBuilder.Append("                " + item.userscore.ToString().Trim() + "分\r\n");
	ViewBuilder.Append("                </span>\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span style=\"color:#ff0000\" id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==3)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id='" + (topicnum+1).ToString() + "'>" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd class=\"dAn fc\"><span class=\"ft4 fl\">您的答案：</span> \r\n");
	ViewBuilder.Append("               <span class=\"fl w2 bx7\">\r\n");

	if (item.useranswer=="Y")
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" checked=\"checked\" value=\"Y\" disabled=\"disabled\">正确</label>\r\n");

	}
	else
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"Y\" disabled=\"disabled\">正确</label>\r\n");

	}	//end if


	if (item.useranswer=="N")
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" checked=\"checked\" value=\"N\" disabled=\"disabled\">错误</label>\r\n");

	}
	else
	{

	ViewBuilder.Append("                 <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"N\" disabled=\"disabled\">错误</label>\r\n");

	}	//end if

	ViewBuilder.Append("                </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if ((examresult.showanswer==1&&examconfig.showanswer==1)||roleid==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：\r\n");
	ViewBuilder.Append("                <span class=\"ft11 ftc1\">\r\n");

	if (item.answer=="Y")
	{

	ViewBuilder.Append("                正确\r\n");

	}
	else if (item.answer=="N")
	{

	ViewBuilder.Append("                错误\r\n");

	}	//end if

	ViewBuilder.Append("                </span></div>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">本题得分：\r\n");
	ViewBuilder.Append("                <span id='" + item.id.ToString().Trim() + "' class=\"bx5 dis_ib\">\r\n");
	ViewBuilder.Append("                " + item.userscore.ToString().Trim() + "分\r\n");
	ViewBuilder.Append("                </span>\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span style=\"color:#ff0000\" id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==4)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id='" + (topicnum+1).ToString() + "'>" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + FmAnswer(item.title,item.id,item.useranswer).ToString() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if ((examresult.showanswer==1&&examconfig.showanswer==1))
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：\r\n");
	ViewBuilder.Append("                <span class=\"ft11 ftc1\">\r\n");
	ViewBuilder.Append("                " + item.answer.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </span></div>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">本题得分：\r\n");
	ViewBuilder.Append("                <span id='" + item.id.ToString().Trim() + "' class=\"bx5 dis_ib\">\r\n");
	ViewBuilder.Append("                " + item.userscore.ToString().Trim() + "分\r\n");
	ViewBuilder.Append("                </span>\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span style=\"color:#ff0000\" id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==5)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id='" + (topicnum+1).ToString() + "'>" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">您的答案</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" readonly=\"readonly\" name=\"answer_" + item.id.ToString().Trim() + "\">" + item.useranswer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if ((examresult.showanswer==1&&examconfig.showanswer==1)||roleid==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：\r\n");
	ViewBuilder.Append("                <span class=\"ft11 ftc1\">\r\n");
	ViewBuilder.Append("                " + item.answer.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </span></div>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">本题得分：\r\n");
	ViewBuilder.Append("                <span id='" + item.id.ToString().Trim() + "' class=\"bx5 dis_ib\">\r\n");
	ViewBuilder.Append("                " + item.userscore.ToString().Trim() + "分\r\n");
	ViewBuilder.Append("                </span>\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span style=\"color:#ff0000\" id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==6)
	{

	ViewBuilder.Append("             <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id='" + (topicnum+1).ToString() + "'>" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">您的答案</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" readonly=\"readonly\" name=\"answer_" + item.id.ToString().Trim() + "\">" + item.useranswer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if ((examresult.showanswer==1&&examconfig.showanswer==1)||roleid==1)
	{


	if (item.explain!="")
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">本题得分：\r\n");
	ViewBuilder.Append("                <span id='" + item.id.ToString().Trim() + "' class=\"bx5 dis_ib\">\r\n");
	ViewBuilder.Append("                " + item.userscore.ToString().Trim() + "分\r\n");
	ViewBuilder.Append("                </span>\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div style=\"color:#ff0000\" id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span style=\"color:#ff0000\" id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}	//end if


	}	//end loop


	}	//end if


	}	//end loop

	ViewBuilder.Append("          </form>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
