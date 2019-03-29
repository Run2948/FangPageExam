<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.questionview" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.Exam" %>
<%@ Import namespace="FangPage.Exam.Model" %>

<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：3.7*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"zh-CN\" lang=\"zh-CN\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n");
	ViewBuilder.Append("<title>" + pagenav.ToString() + " - " + pagetitle.ToString() + "</title>\r\n");
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


	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        var ipt = $(\"label input\");\r\n");
	ViewBuilder.Append("        ipt.parent().removeClass(\"sd\");\r\n");
	ViewBuilder.Append("        ipt.filter(\":checked\").parent().addClass(\"sd\");\r\n");
	ViewBuilder.Append("        layer.use('extend/layer.ext.js');\r\n");
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
	ViewBuilder.Append("            else if (data.action = 1) {\r\n");
	ViewBuilder.Append("                $(\"#fav_\" + qid).html(\"<a href=\\\"javascript:AddFav(\" + qid + \",-1);\\\">取消收藏</a>\");\r\n");
	ViewBuilder.Append("                layer.msg('收藏本题成功!', 2, -1);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }, \"json\");\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function EditNote(qid, num) {\r\n");
	ViewBuilder.Append("        var note = $('#note_' + qid).html();\r\n");
	ViewBuilder.Append("        layer.prompt({ type: 3, title: '(" + sortinfo.name.ToString().Trim() + ")第' + num + '题笔记', val: note }, function (val) {\r\n");
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
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<div class=\"hbx1\">\r\n");
	ViewBuilder.Append("  <div class=\"hbx2\">\r\n");
	ViewBuilder.Append("    <div class=\"hbx3\"><img src=\"" + webpath.ToString() + "sites/exam/statics/images/top.jpg\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"hbx4\">\r\n");
	ViewBuilder.Append("      <div class=\"fr\"><a href=\"testview_csk.aspx?channelid=2&sortid=" + sortid.ToString() + "\" class=\"btnq2\">专项练习</a></div>\r\n");
	ViewBuilder.Append("      <span class=\"write\" style=\"font-size:14px;font-weight:bold\">" + pagenav.ToString() + "</span> \r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div class=\"hbx2\">\r\n");
	ViewBuilder.Append("<div class=\"rnav\">\r\n");
	ViewBuilder.Append("    <div class=\"rnavhd\">" + sortinfo.name.ToString().Trim() + "</div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavct\">\r\n");
	ViewBuilder.Append("      <div class=\"mb10\"> 答题量：" + examloginfo.answers.ToString().Trim() + "/" + examloginfo.questions.ToString().Trim() + "题<br>\r\n");
	ViewBuilder.Append("        错题数：" + examloginfo.wrongs.ToString().Trim() + "题<br>\r\n");
	ViewBuilder.Append("        正确率：" + examloginfo.accuracy.ToString().Trim() + "%</div>\r\n");
	ViewBuilder.Append("      <ul class=\"rnlt1 fc\">\r\n");
	ViewBuilder.Append("        <li><span class=\"bg1\"></span>正确题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg2\"></span>错误题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg3\"></span>未答题</li>\r\n");
	ViewBuilder.Append("        <li><span class=\"bg4\"></span>问答题</li>\r\n");
	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("      <ul class=\"rnlt2 fc\">\r\n");
	int en = 0;
	

	loop__id=0;
	foreach(ExamQuestion item in questionlist)
	{
	loop__id++;

	 en = en+1;
	

	if (item.type==5)
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" class=\"bg4\">" + en.ToString() + "</a></li>\r\n");

	}
	else if (item.useranswer=="")
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" class=\"bg3\">" + en.ToString() + "</a></li>\r\n");

	}
	else if (item.userscore>0)
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" class=\"bg1\">" + en.ToString() + "</a></li>\r\n");

	}
	else
	{

	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" class=\"bg2\">" + en.ToString() + "</a></li>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavft\"></div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("<div class=\"wp wpa\">\r\n");
	ViewBuilder.Append("  <div class=\"wp2\">\r\n");
	ViewBuilder.Append("    <div class=\"wp3\">\r\n");
	ViewBuilder.Append("      <div class=\"wp4\">\r\n");
	ViewBuilder.Append("        <form id=\"testProcessForm\" name=\"testProcessForm\" action=\"testpost.aspx\" method=\"post\">\r\n");
	ViewBuilder.Append("            <a id=\"1\"></a>\r\n");
	ViewBuilder.Append("            <div class=\"tit1 pd1\"></div>\r\n");
	int perscore = 100;
	
	int topicnum = 0;
	

	loop__id=0;
	foreach(ExamQuestion item in questionlist)
	{
	loop__id++;

	 topicnum = topicnum+1;
	

	if (item.type==1)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                " + Option(item.option,item.ascount,item.optionlist).ToString() + "\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd class=\"dAn fc\">\r\n");
	ViewBuilder.Append("                <span class=\"ft4 fl\">您的答案：</span>\r\n");
	ViewBuilder.Append("                 <span class=\"fl w2 bx7\">\r\n");

	loop__id=0;
	foreach(string str in answerarr)
	{
	loop__id++;


	if (loop__id<=item.ascount)
	{

	ViewBuilder.Append("                  <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" \r\n");

	if (str==item.useranswer)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"" + str.ToString() + "\">" + str.ToString() + "</label>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                  </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if (examconfig.showanswer==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：<span class=\"ft11 ftc1\">" + item.answer.ToString().Trim() + "</span></div>\r\n");
	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">\r\n");

	if (item.isfav==1)
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",-1)\">取消收藏</a>&nbsp;&nbsp;\r\n");

	}
	else
	{

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/fav.gif\"><a id=\"fav_" + item.id.ToString().Trim() + "\" href=\"javascript:AddFav(" + item.id.ToString().Trim() + ",1)\">收藏本题</a>&nbsp;&nbsp;\r\n");

	}	//end if

	ViewBuilder.Append("                    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/note.png\"><a href=\"javascript:EditNote(" + item.id.ToString().Trim() + "," + topicnum.ToString() + ")\">编辑笔记</a>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("                <div id=\"shownote_" + item.id.ToString().Trim() + "\" class=\"mb10\" \r\n");

	if (item.note=="")
	{

	ViewBuilder.Append(" style=\"display:none\" \r\n");

	}	//end if

	ViewBuilder.Append(">您的笔记：\r\n");
	ViewBuilder.Append("                  <span id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==2)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                " + Option(item.option,item.ascount,item.optionlist).ToString() + "\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd class=\"dAn fc\">\r\n");
	ViewBuilder.Append("                 <span class=\"ft4 fl\">您的答案：</span>\r\n");
	ViewBuilder.Append("                 <span class=\"fl w2 bx7\">\r\n");

	loop__id=0;
	foreach(string str in answerarr)
	{
	loop__id++;


	if (loop__id<=item.ascount)
	{

	ViewBuilder.Append("                  <label><input type=\"checkbox\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" \r\n");

	if (ischecked(str,item.useranswer))
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"" + str.ToString() + "\">" + str.ToString() + "</label>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                  </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if (examconfig.showanswer==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：<span class=\"ft11 ftc1\">" + item.answer.ToString().Trim() + "</span></div>\r\n");
	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">\r\n");

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
	ViewBuilder.Append("                  <span id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
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
	ViewBuilder.Append("                <span class=\"ft4 fl\">您的答案：</span>\r\n");
	ViewBuilder.Append("                 <span class=\"fl w2 bx7\">\r\n");

	if (item.useranswer=="Y")
	{

	ViewBuilder.Append("                     <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" checked=\"checked\" value=\"Y\" disabled=\"disabled\">正确</label>\r\n");

	}
	else
	{

	ViewBuilder.Append("                     <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"Y\" disabled=\"disabled\">正确</label>\r\n");

	}	//end if


	if (item.useranswer=="N")
	{

	ViewBuilder.Append("                     <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" checked=\"checked\" value=\"N\" disabled=\"disabled\">错误</label>\r\n");

	}
	else
	{

	ViewBuilder.Append("                     <label><input type=\"radio\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"N\" disabled=\"disabled\">错误</label>\r\n");

	}	//end if

	ViewBuilder.Append("                 </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if (examconfig.showanswer==1)
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
	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">\r\n");

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
	ViewBuilder.Append("                  <span id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==4)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + FmAnswer(item.title,item.id,item.useranswer).ToString() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if (examconfig.showanswer==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：\r\n");
	ViewBuilder.Append("                <span class=\"ft11 ftc1\">\r\n");
	ViewBuilder.Append("                " + item.answer.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </span></div>\r\n");
	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">\r\n");

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
	ViewBuilder.Append("                  <span id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==5)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_" + item.id.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">您的答案：</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\">" + item.useranswer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if (examconfig.showanswer==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">正确答案：\r\n");
	ViewBuilder.Append("                <span class=\"ft11 ftc1\">\r\n");
	ViewBuilder.Append("                " + item.answer.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </span></div>\r\n");
	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">\r\n");

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
	ViewBuilder.Append("                  <span id=\"note_" + item.id.ToString().Trim() + "\">{item.note}</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==6)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_" + item.id.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">您的答案：</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\">" + item.useranswer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("              <dd>\r\n");

	if (examconfig.showanswer==1)
	{

	ViewBuilder.Append("                <div class=\"mb10\">答案解析：\r\n");
	ViewBuilder.Append("                  " + item.explain.ToString().Trim() + "\r\n");
	ViewBuilder.Append("                </div>\r\n");

	}	//end if

	ViewBuilder.Append("                <div class=\"mb10\">\r\n");

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
	ViewBuilder.Append("                  <span id=\"note_" + item.id.ToString().Trim() + "\">" + item.note.ToString().Trim() + "</span>\r\n");
	ViewBuilder.Append("                </div>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}	//end if


	}	//end loop

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
