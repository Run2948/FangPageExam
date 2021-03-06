<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller.examtopicmanage" %>
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
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>试卷题目设置</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + adminpath.ToString() + "css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"" + adminpath.ToString() + "css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("<link href=\"" + webpath.ToString() + "sites/exam/admin/css/exam.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + adminpath.ToString() + "css/tab.css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + adminpath.ToString() + "js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	string navurl = "exammanage.aspx";
	
	ViewBuilder.Append("        PageNav(\"" + GetSortNav(sortinfo,navurl).ToString() + "|" + examinfo.name.ToString().Trim() + "," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        $('#checkall').click(function () {\r\n");
	ViewBuilder.Append("            $('input[name=chkid]').attr(\"checked\", this.checked)\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('.sprite').click(function () {   // 获取所谓的父行\r\n");
	ViewBuilder.Append("            $(this).toggleClass(\"sprite-selected\");  // 添加/删除图标\r\n");
	ViewBuilder.Append("            $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('.sprite-selected').click(function () {   // 获取所谓的父行\r\n");
	ViewBuilder.Append("            if ($(this).attr('class') == 'sprite-selected')\r\n");
	ViewBuilder.Append("            {\r\n");
	ViewBuilder.Append("                $(this).attr('class','sprite');\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else\r\n");
	ViewBuilder.Append("            {\r\n");
	ViewBuilder.Append("                $(this).attr('class', 'sprite-selected');\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('#btndisplay').click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"display\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('#btnaddpaper').click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"addpaper\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('#submitdel').click(function () {\r\n");
	ViewBuilder.Append("            if (confirm(\"你确定要删除该份试卷吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"delpaper\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('#btnsaveas').click(function () {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"saveas\");\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        var index = layer.getFrameIndex(window.name);\r\n");
	ViewBuilder.Append("        $('#outputpaper').click(function () {\r\n");
	ViewBuilder.Append("            index = $.layer({\r\n");
	ViewBuilder.Append("                type: 2,\r\n");
	ViewBuilder.Append("                shade: [0],\r\n");
	ViewBuilder.Append("                fix: false,\r\n");
	ViewBuilder.Append("                title: '导出试卷',\r\n");
	ViewBuilder.Append("                maxmin: false,\r\n");
	ViewBuilder.Append("                iframe: { src: 'outputpaper.aspx?examid=" + examid.ToString() + "&paper=" + paper.ToString() + "'},\r\n");
	ViewBuilder.Append("                area: ['485px', '185px']\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("    function DeleteExamTopic(examtopicid) {\r\n");
	ViewBuilder.Append("        if (confirm(\"您确定要删除该大题吗？删除之后将无法进行恢复\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"delete\");\r\n");
	ViewBuilder.Append("            $(\"#examtopicid\").val(examtopicid);\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    function DeleteTopic(examtopicid,tid) {\r\n");
	ViewBuilder.Append("        if (confirm(\"您确定要从试卷中取消加入该试题吗？\")) {\r\n");
	ViewBuilder.Append("            $(\"#action\").val(\"deletetopic\");\r\n");
	ViewBuilder.Append("            $(\"#examtopicid\").val(examtopicid);\r\n");
	ViewBuilder.Append("            $(\"#tid\").val(tid);\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form id=\"frmpost\" method=\"post\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"examtopicid\" id=\"examtopicid\" value=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"tid\" id=\"tid\" value=\"\">\r\n");
	ViewBuilder.Append("  <div class=\"ntab4\">\r\n");
	ViewBuilder.Append("        <div class=\"tabtitle\">\r\n");
	ViewBuilder.Append("          <ul id=\"mytab1\">\r\n");

	if (examinfo.papers>=1)
	{


	if (paper==1)
	{

	ViewBuilder.Append("              <li class=\"active\"><a href=\"?examid=" + examid.ToString() + "&paper=1\">A卷</a> </li>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <li class=\"normal\"><a href=\"?examid=" + examid.ToString() + "&paper=1\">A卷</a> </li>\r\n");

	}	//end if


	}	//end if


	if (examinfo.papers>=2)
	{


	if (paper==2)
	{

	ViewBuilder.Append("              <li class=\"active\"><a href=\"?examid=" + examid.ToString() + "&paper=2\">B卷</a> </li>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <li class=\"normal\"><a href=\"?examid=" + examid.ToString() + "&paper=2\">B卷</a> </li>\r\n");

	}	//end if


	}	//end if


	if (examinfo.papers>=3)
	{


	if (paper==3)
	{

	ViewBuilder.Append("              <li class=\"active\"><a href=\"?examid=" + examid.ToString() + "&paper=3\">C卷</a> </li>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <li class=\"normal\"><a href=\"?examid=" + examid.ToString() + "&paper=3\">C卷</a> </li>\r\n");

	}	//end if


	}	//end if


	if (examinfo.papers>=4)
	{


	if (paper==4)
	{

	ViewBuilder.Append("              <li class=\"active\"><a href=\"?examid=" + examid.ToString() + "&paper=4\">D卷</a> </li>\r\n");

	}
	else
	{

	ViewBuilder.Append("              <li class=\"normal\"><a href=\"?examid=" + examid.ToString() + "&paper=4\">D卷</a> </li>\r\n");

	}	//end if


	}	//end if


	if (examinfo.papers<4)
	{

	ViewBuilder.Append("            <li class=\"normal\"><a id=\"btnaddpaper\" href=\"javascript:void();\">添加试卷</a> </li>\r\n");

	}	//end if

	ViewBuilder.Append("          </ul>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td>\r\n");
	ViewBuilder.Append("      <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/delete.gif) 2px 6px no-repeat\"><a id=\"submitdel\" href=\"#\">删除试卷</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/add.gif) 2px 6px no-repeat\"><a href=\"examtopicadd.aspx?examid=" + examid.ToString() + "&paper=" + paper.ToString() + "\">添加大题</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/save.gif) 2px 6px no-repeat\"><a id=\"btnsaveas\" href=\"javascript:void();\">另存为</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/display.gif) 2px 6px no-repeat\"><a id=\"btndisplay\" href=\"javascript:void();\">保存排序</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/report.png) 2px 6px no-repeat\"><a href=\"../exampreview.aspx?examid=" + examid.ToString() + "&paper=" + paper.ToString() + "\" target=\"_blank\">试卷预览</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/down.gif) 2px 6px no-repeat\"><a id=\"outputpaper\" href=\"#\">导出试卷</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/refresh.gif) 2px 6px no-repeat\"><a href=\"" + pagename.ToString() + "?examid=" + examid.ToString() + "&paper=" + paper.ToString() + "\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/return.gif) 2px 6px no-repeat\"><a href=\"exammanage.aspx?sortid=" + sortid.ToString() + "\">返回</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\">\r\n");
	ViewBuilder.Append("               <strong>总分：<span id=\"total\" style=\"color:Red\">100</span>分，总题目数：<span style=\"color:Red\">" + examinfo.questions.ToString().Trim() + "</span>题</strong>\r\n");
	ViewBuilder.Append("              </li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    </table>\r\n");
	ViewBuilder.Append("    <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td>\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	double total = 0;
	

	loop__id=0;
	foreach(ExamTopic examtopic in examtopiclist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr style=\"color:#1317fc;\" class=\"thead\">\r\n");
	ViewBuilder.Append("              <td width=\"40\" align=\"center\"><div id=\"row_" + examtopic.id.ToString().Trim() + "\" \r\n");

	if (examtopic.id==examtopicid)
	{

	ViewBuilder.Append(" class=\"sprite-selected\" \r\n");

	}
	else
	{

	ViewBuilder.Append(" class=\"sprite\" \r\n");

	}	//end if

	ViewBuilder.Append("></div></td>\r\n");
	ViewBuilder.Append("        	  <td align=\"left\" valign=\"middle\">\r\n");
	ViewBuilder.Append("                 " + examtopic.title.ToString().Trim() + "(总题数<span style=\"color:Red\">" + examtopic.questions.ToString().Trim() + "</span>题，固定题<span style=\"color:Red\">" + examtopic.curquestions.ToString().Trim() + "</span>题，随机题<span style=\"color:Red\">" + (examtopic.questions-examtopic.curquestions).ToString().Trim() + "</span>题，每题<span style=\"color:Red\">" + examtopic.perscore.ToString().Trim() + "</span>分，共<span style=\"color:Red\">" + (examtopic.perscore*examtopic.questions).ToString().Trim() + "</span>分)\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td width=\"80\">所在题库</td>\r\n");
	ViewBuilder.Append("              <td width=\"180\">\r\n");
	ViewBuilder.Append("              <a style=\"color:#1317fc;\" href=\"examtopicadd.aspx?id=" + examtopic.id.ToString().Trim() + "&paper=" + paper.ToString() + "\">编辑</a>\r\n");
	ViewBuilder.Append("              <a style=\"color:#1317fc;\" href=\"javascript:DeleteExamTopic(" + examtopic.id.ToString().Trim() + ")\">删除</a>\r\n");
	ViewBuilder.Append("              <a style=\"color:#1317fc;\" href=\"questionselect.aspx?examtopicid=" + examtopic.id.ToString().Trim() + "&paper=" + paper.ToString() + "\">手工选题</a>\r\n");
	ViewBuilder.Append("              <a style=\"color:#1317fc;\" href=\"examtopicrandom.aspx?examtopicid=" + examtopic.id.ToString().Trim() + "&paper=" + paper.ToString() + "\">随机选题</a>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	 total = total+examtopic.perscore*examtopic.questions;
	
	int topicnum = 0;
	

	loop__id=0;
	foreach(ExamQuestion item in QuestionBll.GetQuestionList(examtopic.questionlist))
	{
	loop__id++;

	 topicnum = topicnum+1;
	
	ViewBuilder.Append("            <tr class=\"tlist child_row_" + examtopic.id.ToString().Trim() + "\" \r\n");

	if (examtopic.id!=examtopicid)
	{

	ViewBuilder.Append(" style=\"display:none;\" \r\n");

	}	//end if

	ViewBuilder.Append(" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td width=\"40\" align=\"center\">\r\n");
	ViewBuilder.Append("                  <input id=\"display_" + item.id.ToString().Trim() + "\" name=\"display_" + item.id.ToString().Trim() + "\" style=\"text-align:center;width:36px;\" value=\"" + topicnum.ToString() + "\" type=\"text\">\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"left\">\r\n");
	ViewBuilder.Append("              <strong>\r\n");
	ViewBuilder.Append("                      " + TypeStr(item.type).ToString() + "：\r\n");
	ViewBuilder.Append("                      " + FmAnswer(item.title).ToString() + "\r\n");
	ViewBuilder.Append("              </strong>\r\n");

	if (item.type==1||item.type==2)
	{

	ViewBuilder.Append("              <div style=\"height: 2px; overflow: hidden;\"></div>\r\n");
	ViewBuilder.Append("              " + Option(item.option,item.ascount).ToString() + "\r\n");

	}	//end if

	ViewBuilder.Append("              <div style=\"height: 5px; overflow: hidden; border-bottom-color: rgb(204, 204, 204); border-bottom-width: 1px; border-bottom-style: dashed;\"></div>\r\n");
	ViewBuilder.Append("              <div class=\"tips\">\r\n");

	if (item.type!=6)
	{

	ViewBuilder.Append("              <div style=\"color:Red\">\r\n");

	if (item.type==3)
	{


	if (item.answer=="Y")
	{

	ViewBuilder.Append("                 参考答案：正确\r\n");

	}
	else if (item.answer=="N")
	{

	ViewBuilder.Append("                 参考答案：错误\r\n");

	}	//end if


	}
	else
	{

	ViewBuilder.Append("                 参考答案：" + item.answer.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("              </div>\r\n");

	}	//end if

	ViewBuilder.Append("              <span style=\"color:Red\">难易程度：" + DifficultyStr(item.difficulty).ToString() + "，考过次数：" + item.exams.ToString().Trim() + "，做错次数：" + item.wrongs.ToString().Trim() + "</span><br>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("              <span style=\"color:Red\">答案解析：" + item.explain.ToString().Trim() + "</span> \r\n");

	}
	else
	{

	ViewBuilder.Append("              <span style=\"color:Red\">答案解析：暂无解析</span>\r\n");

	}	//end if

	ViewBuilder.Append("              </div>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.SortInfo.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("              <a style=\"color: #1317fc\" href=\"questionadd.aspx?id=" + item.id.ToString().Trim() + "&examid=" + examid.ToString() + "&examtopicid=" + examtopic.id.ToString().Trim() + "\">编辑试题</a>&nbsp;&nbsp;\r\n");
	ViewBuilder.Append("              <a style=\"color: #1317fc\" href=\"javascript:DeleteTopic(" + examtopic.id.ToString().Trim() + "," + item.id.ToString().Trim() + ")\">取消加入</a>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop


	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("          <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("              $(\"#total\").html('" + total.ToString() + "');\r\n");
	ViewBuilder.Append("          </");
	ViewBuilder.Append("script>\r\n");
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
