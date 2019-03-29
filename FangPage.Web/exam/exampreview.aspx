<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller.exampreview" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FP_Exam" %>
<%@ Import namespace="FP_Exam.Model" %>

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
	ViewBuilder.Append("<title>试卷[" + examinfo.name.ToString().Trim() + "" + GetPaper(paper).ToString() + "]预览 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/exam.css\">\r\n");
	ViewBuilder.Append("<link href=\"" + adminpath.ToString() + "css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
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
	ViewBuilder.Append("        var index = layer.getFrameIndex(window.name);\r\n");
	ViewBuilder.Append("        $('#importpaper').click(function () {\r\n");
	ViewBuilder.Append("            index = $.layer({\r\n");
	ViewBuilder.Append("                type: 1,\r\n");
	ViewBuilder.Append("                shade: [0],\r\n");
	ViewBuilder.Append("                fix: false,\r\n");
	ViewBuilder.Append("                title: '导出试卷',\r\n");
	ViewBuilder.Append("                maxmin: false,\r\n");
	ViewBuilder.Append("                page: { dom: '#importpage' },\r\n");
	ViewBuilder.Append("                area: ['485px', '185px']\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $(\"#btnclose\").click(function () {\r\n");
	ViewBuilder.Append("            layer.close(index);\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $(\"#btnuserok\").click(function () {\r\n");
	ViewBuilder.Append("            $(\"#testProcessForm\").submit();\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("    });\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<form id=\"testProcessForm\" name=\"testProcessForm\" action=\"\" method=\"post\">\r\n");
	ViewBuilder.Append("<div class=\"hbx1\">\r\n");
	ViewBuilder.Append("  <div class=\"hbx2\">\r\n");
	ViewBuilder.Append("    <div class=\"hbx3\"><img src=\"" + webpath.ToString() + "sites/exam/statics/images/top.jpg\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"hbx4\">\r\n");
	ViewBuilder.Append("      <div class=\"fr\"><a id=\"importpaper\" href=\"javascript:void();\" class=\"btnq2\">导出试卷</a></div>\r\n");
	ViewBuilder.Append("      <span class=\"theTime\" id=\"thetime\">00:00:00</span><span class=\"line1\"></span><span class=\"write\">答题时间：" + examinfo.examtime.ToString().Trim() + "分钟</span>\r\n");

	if (examinfo.islimit==1)
	{

	ViewBuilder.Append("      <span class=\"line1\"></span>考试期限：" + FangPage.MVC.FPUtils.GetDate(examinfo.starttime,"yyyy-MM-dd HH:mm") + "~" + FangPage.MVC.FPUtils.GetDate(examinfo.endtime,"yyyy-MM-dd HH:mm") + "\r\n");

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
	ViewBuilder.Append("      <ul class=\"rnlt2 fc\">\r\n");
	int en = 0;
	

	loop__id=0;
	foreach(int examtopic in questionlist)
	{
	loop__id++;

	 en = en+1;
	
	ViewBuilder.Append("          <li><a href=\"#" + en.ToString() + "\" id=\"fc_" + en.ToString() + "\" class=\"bg3\">" + en.ToString() + "</a></li>\r\n");

	}	//end loop

	ViewBuilder.Append("      </ul>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    <div class=\"rnavft\"></div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("<div class=\"wp wpa\">\r\n");
	ViewBuilder.Append("  <div class=\"wp2\">\r\n");
	ViewBuilder.Append("    <div class=\"wp3\">\r\n");
	ViewBuilder.Append("      <div class=\"wp4\">\r\n");
	ViewBuilder.Append("        <h1 class=\"qtTitle\">" + examinfo.name.ToString().Trim() + "" + GetPaper(paper).ToString() + "</h1>\r\n");
	ViewBuilder.Append("        <div class=\"tit1 pd1\">考试说明</div>\r\n");
	ViewBuilder.Append("        <div class=\"bx1 pd1m mb20\">\r\n");
	ViewBuilder.Append("          <div>\r\n");
	ViewBuilder.Append("            <table class=\"tab1\">\r\n");
	ViewBuilder.Append("              <tbody>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                  <td>考试用户：\r\n");

	if (user.realname!="")
	{

	ViewBuilder.Append("" + user.realname.ToString().Trim() + "\r\n");

	}
	else
	{

	ViewBuilder.Append("" + user.username.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("</td>\r\n");
	ViewBuilder.Append("                  <td>试卷总分：" + examinfo.total.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                  <td>及格分数：" + examinfo.passmark.ToString().Trim() + "分</td>\r\n");
	ViewBuilder.Append("                  <td>答题时间：" + examinfo.examtime.ToString().Trim() + "分钟</td>\r\n");
	ViewBuilder.Append("                  <td>考试题数：" + examinfo.questions.ToString().Trim() + "题</td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("              </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"examid\" name=\"examid\" value=\"" + examid.ToString() + "\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"starttime\" name=\"starttime\" value=\"" + starttime.ToString() + "\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"utime\" name=\"utime\" value=\"0\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"autotime\" value=\"0\">\r\n");
	ViewBuilder.Append("          <input type=\"hidden\" id=\"examtime\" value=\"0\">\r\n");
	int topicnum = 0;
	

	loop__id=0;
	foreach(ExamTopic examtopic in examtopiclist)
	{
	loop__id++;

	string qidlist = "";
	

	if (examtopic.questions>0)
	{

	ViewBuilder.Append("            <div id=\"1\" class=\"tit1 pd1\"></div>\r\n");
	ViewBuilder.Append("            <div class=\"tit1 pd1\">" + examtopic.title.ToString().Trim() + "<span class=\"ft3\">(共" + examtopic.questions.ToString().Trim() + "题，每题" + examtopic.perscore.ToString().Trim() + "分，共" + (examtopic.questions*examtopic.perscore).ToString().Trim() + "分)</span></div>\r\n");

	loop__id=0;
	foreach(ExamQuestion item in GetQuestionList(examtopic.questionlist))
	{
	loop__id++;

	 topicnum = topicnum+1;
	

	if (qidlist!="")
	{

	 qidlist = qidlist+",";
	

	}	//end if

	 qidlist = qidlist+item.id;
	

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

	ViewBuilder.Append("                  <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"" + str.ToString() + "\">" + str.ToString() + "</label>\r\n");

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

	ViewBuilder.Append("                  <label><input type=\"checkbox\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"" + str.ToString() + "\">" + str.ToString() + "</label>\r\n");

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
	ViewBuilder.Append("                  <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"Y\">正确</label>\r\n");
	ViewBuilder.Append("                  <label><input type=\"radio\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\" value=\"N\">错误</label>\r\n");
	ViewBuilder.Append("                  </span>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==4)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_0\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">" + topicnum.ToString() + "</span>\r\n");
	ViewBuilder.Append("                <p>" + FmAnswer(item.title,item.id,topicnum).ToString() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}
	else if (item.type==5)
	{

	ViewBuilder.Append("            <dl class=\"st tm_zt_" + item.id.ToString().Trim() + "\">\r\n");
	ViewBuilder.Append("              <dt class=\"nobold\"><span class=\"num\" id=\"" + (topicnum+1).ToString() + "\">{topicnum}</span>\r\n");
	ViewBuilder.Append("                <p>" + item.title.ToString().Trim() + "</p>\r\n");
	ViewBuilder.Append("              </dt>\r\n");
	ViewBuilder.Append("              <dd>\r\n");
	ViewBuilder.Append("                <div class=\"ft4\">填写答案</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" id=\"_" + topicnum.ToString() + "\" name=\"answer_" + item.id.ToString().Trim() + "\"></textarea>\r\n");
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
	ViewBuilder.Append("                <div class=\"ft4\">填写答案</div>\r\n");
	ViewBuilder.Append("                <textarea class=\"jdt\" id=\"_" + topicnum.ToString() + "\" name=\"answer_4" + item.id.ToString().Trim() + "\"></textarea>\r\n");
	ViewBuilder.Append("              </dd>\r\n");
	ViewBuilder.Append("            </dl>\r\n");

	}	//end if


	}	//end if


	}	//end loop

	ViewBuilder.Append("        <input id=\"qidlist_" + examtopic.id.ToString().Trim() + "\" name=\"qidlist_" + examtopic.id.ToString().Trim() + "\" value=\"" + qidlist.ToString() + "\" type=\"hidden\">\r\n");

	}	//end loop

	ViewBuilder.Append("        <div style=\"clear:both;\"></div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div id=\"importpage\" style=\"display:none;\">\r\n");
	ViewBuilder.Append("    <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width: 483px;height:150px; margin: 0px;\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("            <td colspan=\"3\" style=\"border: solid 1px #93C7D4; vertical-align:middle;height:100px;padding-left:10px;\">\r\n");
	ViewBuilder.Append("               <table colspan=\"3\" style=\"height:40px;\">\r\n");
	ViewBuilder.Append("                   <tr>\r\n");
	ViewBuilder.Append("                    <td style=\"width:70px;height:50px;\">试卷纸张：</td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"papersize\" name=\"papersize\" value=\"a4\" checked=\"checked\" type=\"radio\">A4&nbsp;&nbsp;&nbsp;&nbsp;<input id=\"papersize\" name=\"papersize\" value=\"a3\" type=\"radio\">A3\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("                <tr>\r\n");
	ViewBuilder.Append("                    <td style=\"width:70px;\">试卷类型：</td>\r\n");
	ViewBuilder.Append("                    <td>\r\n");
	ViewBuilder.Append("                       <input id=\"papertype\" name=\"papertype\" type=\"radio\" checked=\"checked\" value=\"0\">学生用卷（答案集中在卷尾）&nbsp;&nbsp;&nbsp;&nbsp;<input id=\"papertype\" name=\"papertype\" value=\"1\" type=\"radio\">教师用卷（每题后面跟答案）<br>\r\n");
	ViewBuilder.Append("                    </td>\r\n");
	ViewBuilder.Append("                </tr>\r\n");
	ViewBuilder.Append("               </table>\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("            <td colspan=\"3\" height=\"25\" align=\"right\">\r\n");
	ViewBuilder.Append("               <input type=\"button\" name=\"btnuserok\" value=\"下载\" id=\"btnuserok\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("               <input type=\"button\" name=\"btnclose\" value=\"关闭\" id=\"btnclose\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
