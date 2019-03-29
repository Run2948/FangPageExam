<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.questionadd" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.Exam" %>
<%@ Import namespace="FangPage.Exam.Model" %>

<%@ Import namespace="FangPage.WMS.Model" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.9*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n");
	ViewBuilder.Append("    <title>添加编辑题目</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <link href=\"" + adminpath.ToString() + "css/masterpage.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("    <link href=\"" + adminpath.ToString() + "css/admin.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("editor") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\" src=\"" + adminpath.ToString() + "js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        $(function () {\r\n");
	ViewBuilder.Append("            KindEditor.create('#title,#answer5,#explain', {\r\n");
	ViewBuilder.Append("                resizeType: 1,\r\n");
	ViewBuilder.Append("                uploadJson: '" + webpath.ToString() + "tools/uploadajax.aspx?sortid=" + sortid.ToString() + "',\r\n");
	ViewBuilder.Append("                fileManagerJson: '" + webpath.ToString() + "tools/filemanagerajax.aspx',\r\n");
	ViewBuilder.Append("                newlineTag: \"br\",\r\n");
	ViewBuilder.Append("                pasteType : 1,\r\n");
	ViewBuilder.Append("                items: ['fontsize','forecolor', 'hilitecolor', 'bold', 'italic', 'underline',\r\n");
	ViewBuilder.Append("						'removeformat', '|', 'image', 'flash', 'media', '|', 'link', 'unlink'],\r\n");
	ViewBuilder.Append("                afterBlur: function () { this.sync(); }\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("            KindEditor.create('#option1_0,#option1_1,#option1_2,#option1_3,#option1_4,#option1_5,#option2_0,#option2_1,#option2_2,#option2_3,#option2_4,#option2_5', {\r\n");
	ViewBuilder.Append("                resizeType: 0,\r\n");
	ViewBuilder.Append("                uploadJson: '" + webpath.ToString() + "tools/uploadajax.aspx?sortid=" + sortid.ToString() + "',\r\n");
	ViewBuilder.Append("                fileManagerJson: '" + webpath.ToString() + "tools/filemanagerajax.aspx',\r\n");
	ViewBuilder.Append("                minWidth: \"300px\",\r\n");
	ViewBuilder.Append("                minHeight: \"80px\",\r\n");
	ViewBuilder.Append("                newlineTag: \"br\",\r\n");
	ViewBuilder.Append("                pasteType: 1,\r\n");
	ViewBuilder.Append("                items: ['forecolor', 'hilitecolor', 'bold', 'italic', 'underline',\r\n");
	ViewBuilder.Append("						'removeformat', '|', 'image'],\r\n");
	ViewBuilder.Append("                afterBlur: function () { this.sync(); }\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("            ShowTopic();\r\n");
	ViewBuilder.Append("            ShowAnswerOption(1);\r\n");
	ViewBuilder.Append("            ShowAnswerOption(2);\r\n");
	ViewBuilder.Append("            $(\"#btnsaveback\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("            $(\"#btnsavecontinue\").click(function () {\r\n");
	ViewBuilder.Append("                $(\"#action\").val(\"continue\");\r\n");
	ViewBuilder.Append("                $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("            $(\"#btnback\").click(function () {\r\n");
	ViewBuilder.Append("                window.location.href = \"" + reurl.ToString() + "\";\r\n");
	ViewBuilder.Append("            });\r\n");
	string navurl = "questionmanage.aspx";
	
	ViewBuilder.Append("            PageNav(\"" + GetSortNav(sortinfo,navurl).ToString() + "|添加编辑题目," + rawurl.ToString() + "\");\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        function ShowTopic() {\r\n");
	ViewBuilder.Append("            var v = $(\"#type\").val();\r\n");
	ViewBuilder.Append("            if (v == 1 || v == 2) {\r\n");
	ViewBuilder.Append("                $(\"#tr_content\").show();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else {\r\n");
	ViewBuilder.Append("                $(\"#tr_content\").hide();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            if (v == 4) {\r\n");
	ViewBuilder.Append("                $(\"#tk_title\").show();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else {\r\n");
	ViewBuilder.Append("                $(\"#tk_title\").hide();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            if (v == 5) {\r\n");
	ViewBuilder.Append("                $(\"#tr_answerkey\").show();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else {\r\n");
	ViewBuilder.Append("                $(\"#tr_answerkey\").hide();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            for (i = 1; i <= 5; i++) {\r\n");
	ViewBuilder.Append("                $(\"#tr_answer\"+ i).hide();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            $(\"#tr_answer\" + v).show();\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        function ShowAnswerOption(type) {\r\n");
	ViewBuilder.Append("            var v = $(\"#ascount\"+type).val();\r\n");
	ViewBuilder.Append("            for (i = 2; i <= 9; i++) {\r\n");
	ViewBuilder.Append("                $(\"#answer\" + type + \"_\" + i).hide();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            for (var i = 2; i <= v; i++) {\r\n");
	ViewBuilder.Append("                $(\"#answer\" + type + \"_\" + i).show();\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form name=\"frmpost\" method=\"post\" action=\"\" id=\"frmpost\">\r\n");
	ViewBuilder.Append("     <input type=\"hidden\" id=\"action\" name=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("     <div class=\"newslistabout\">\r\n");
	ViewBuilder.Append("     <table class=\"borderkuang\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n");
	ViewBuilder.Append("      <tbody>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("          <td class=\"newstitle\" bgcolor=\"#ffffff\">添加编辑题目，所在题库：" + sortinfo.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("      </tbody>\r\n");
	ViewBuilder.Append("      </table>\r\n");
	ViewBuilder.Append("      <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 题目类型： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <select id=\"type\" name=\"type\" onchange=\"ShowTopic()\">\r\n");

	if (ischecked(1,examconfig.questiontype))
	{

	ViewBuilder.Append("                <option value=\"1\" \r\n");

	if (type==1)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">单选题</option>\r\n");

	}	//end if


	if (ischecked(2,examconfig.questiontype))
	{

	ViewBuilder.Append("                <option value=\"2\" \r\n");

	if (type==2)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">多选题</option>\r\n");

	}	//end if


	if (ischecked(3,examconfig.questiontype))
	{

	ViewBuilder.Append("                <option value=\"3\" \r\n");

	if (type==3)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">判断题</option>\r\n");

	}	//end if


	if (ischecked(4,examconfig.questiontype))
	{

	ViewBuilder.Append("                <option value=\"4\" \r\n");

	if (type==4)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">填空题</option>\r\n");

	}	//end if


	if (ischecked(5,examconfig.questiontype))
	{

	ViewBuilder.Append("                <option value=\"5\" \r\n");

	if (type==5)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">问答题</option>\r\n");

	}	//end if


	if (ischecked(6,examconfig.questiontype))
	{

	ViewBuilder.Append("                <option value=\"6\" \r\n");

	if (type==6)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">打字题</option>\r\n");

	}	//end if

	ViewBuilder.Append("            </select>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("       </table>\r\n");
	ViewBuilder.Append("       <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 题目内容： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <span id=\"tk_title\" style=\"color:Red\">\r\n");
	ViewBuilder.Append("            填空题在需要用户作答的地方放(#answer)标签\r\n");
	ViewBuilder.Append("            </span>\r\n");
	ViewBuilder.Append("            <textarea id=\"title\" name=\"title\" style=\"width:100%;height:80px\" cols=\"80\" rows=\"2\">" + questioninfo.title.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table id=\"tr_answer1\" style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 答案选项：<br><br>\r\n");
	ViewBuilder.Append("            <select id=\"ascount1\" name=\"ascount1\" onchange=\"ShowAnswerOption(1)\" style=\"width:60px;margin-right:5px;\">\r\n");
	ViewBuilder.Append("                <option value=\"2\" \r\n");

	if (ascount==2)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">2个</option>\r\n");
	ViewBuilder.Append("                <option value=\"3\" \r\n");

	if (ascount==3)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">3个</option>\r\n");
	ViewBuilder.Append("                <option value=\"4\" \r\n");

	if (ascount==4)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">4个</option>\r\n");
	ViewBuilder.Append("                <option value=\"5\" \r\n");

	if (ascount==5)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">5个</option>\r\n");
	ViewBuilder.Append("                <option value=\"6\" \r\n");

	if (ascount==6)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">6个</option>\r\n");
	ViewBuilder.Append("            </select>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("           <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("             <tr>\r\n");
	ViewBuilder.Append("               <td id=\"answer1_1\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">A</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option1_0\" name=\"option1_0\" style=\"height:100px;width:320px\">" + questioninfo.option2[0].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer1\" id=\"answer1_a\" \r\n");

	if (questioninfo.answer=="a")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"A\" type=\"radio\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table> \r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("               <td id=\"answer1_2\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">B</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option1_1\" name=\"option1_1\" style=\"height:100px;width:320px\">" + questioninfo.option2[1].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer1\" id=\"answer1_b\" \r\n");

	if (questioninfo.answer=="b")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"B\" type=\"radio\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table>\r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("               <td id=\"answer1_3\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">C</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option1_2\" name=\"option1_2\" style=\"height:100px;width:320px\">" + questioninfo.option2[2].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer1\" id=\"answer1_c\" \r\n");

	if (questioninfo.answer=="c")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"C\" type=\"radio\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table> \r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("               <td id=\"answer1_4\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">D</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option1_3\" name=\"option1_3\" style=\"height:100px;width:320px\">" + questioninfo.option2[3].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer1\" id=\"answer1_d\" \r\n");

	if (questioninfo.answer=="d")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"D\" type=\"radio\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table>\r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("               <td id=\"answer1_5\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">E</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option1_4\" name=\"option1_4\" style=\"height:100px;width:320px\">" + questioninfo.option2[4].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer1\" id=\"answer1_e\" \r\n");

	if (questioninfo.answer=="e")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"E\" type=\"radio\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table> \r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("               <td id=\"answer1_6\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">F</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option1_5\" name=\"option1_5\" style=\"height:100px;width:320px\">" + questioninfo.option2[5].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer1\" id=\"answer1_f\" \r\n");

	if (questioninfo.answer=="f")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"F\" type=\"radio\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table>\r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("           </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table id=\"tr_answer2\" style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 答案选项：<br><br>\r\n");
	ViewBuilder.Append("            <select id=\"ascount2\" name=\"ascount2\" onchange=\"ShowAnswerOption(2)\" style=\"width:60px;margin-right:5px;\">\r\n");
	ViewBuilder.Append("                <option value=\"2\" \r\n");

	if (ascount==2)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">2个</option>\r\n");
	ViewBuilder.Append("                <option value=\"3\" \r\n");

	if (ascount==3)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">3个</option>\r\n");
	ViewBuilder.Append("                <option value=\"4\" \r\n");

	if (ascount==4)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">4个</option>\r\n");
	ViewBuilder.Append("                <option value=\"5\" \r\n");

	if (ascount==5)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">5个</option>\r\n");
	ViewBuilder.Append("                <option value=\"6\" \r\n");

	if (ascount==6)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">6个</option>\r\n");
	ViewBuilder.Append("            </select>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("           <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("             <tr>\r\n");
	ViewBuilder.Append("               <td id=\"answer2_1\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">A</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option2_0\" name=\"option2_0\" style=\"height:100px;width:320px\">" + questioninfo.option2[0].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer2\" id=\"answer2_a\" \r\n");

	if (questioninfo.answer=="a")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"A\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table> \r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("               <td id=\"answer2_2\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">B</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option2_1\" name=\"option2_1\" style=\"height:100px;width:320px\">" + questioninfo.option2[1].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer2\" id=\"answer2_b\" \r\n");

	if (questioninfo.answer=="b")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"B\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table>\r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("               <td id=\"answer2_3\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">C</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option2_2\" name=\"option2_2\" style=\"height:100px;width:320px\">" + questioninfo.option2[2].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer2\" id=\"answer2_c\" \r\n");

	if (questioninfo.answer=="c")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"C\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table> \r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("               <td id=\"answer2_4\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">D</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option2_3\" name=\"option2_3\" style=\"height:100px;width:320px\">" + questioninfo.option2[3].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer2\" id=\"answer2_d\" \r\n");

	if (questioninfo.answer=="d")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"D\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table>\r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("               <td id=\"answer2_5\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">E</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option2_4\" name=\"option2_4\" style=\"height:100px;width:320px\">" + questioninfo.option2[4].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer2\" id=\"answer2_e\" \r\n");

	if (questioninfo.answer=="e")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"E\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table> \r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("               <td id=\"answer2_6\">\r\n");
	ViewBuilder.Append("                   <table cellpadding=\"0\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff\">F</td>\r\n");
	ViewBuilder.Append("                           <td rowspan=\"2\"><textarea id=\"option2_5\" name=\"option2_5\" style=\"height:100px;width:320px\">" + questioninfo.option2[5].ToString().Trim() + "</textarea></td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                       <tr>\r\n");
	ViewBuilder.Append("                           <td style=\"align-content:center;background-color:#e9cdcd;padding-left:5px;\">\r\n");
	ViewBuilder.Append("                               <input name=\"answer2\" id=\"answer2_f\" \r\n");

	if (questioninfo.answer=="f")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"F\" type=\"checkbox\">\r\n");
	ViewBuilder.Append("                           </td>\r\n");
	ViewBuilder.Append("                       </tr>\r\n");
	ViewBuilder.Append("                   </table>\r\n");
	ViewBuilder.Append("               </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("           </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table id=\"tr_answer3\" style=\"width:100%;display:none\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 题目答案： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <label for=\"answer3_y\"><input type=\"radio\" name=\"answer3\" id=\"answer3_y\" \r\n");

	if (questioninfo.answer=="y")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"Y\">正确</label>\r\n");
	ViewBuilder.Append("            <label for=\"answer3_n\"><input type=\"radio\" name=\"answer3\" id=\"answer3_n\" \r\n");

	if (questioninfo.answer=="n")
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"N\">错误</label>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table id=\"tr_answer4\" style=\"width:100%;display:none\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 题目答案： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <span style=\"color:Red\">\r\n");
	ViewBuilder.Append("            填空题如果有多个空格，各个空格的答案之间用英文逗号(,)隔开。\r\n");
	ViewBuilder.Append("            <input id=\"upperflg\" name=\"upperflg\" \r\n");

	if (questioninfo.upperflg==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\" type=\"checkbox\">答案区分大小写，<input id=\"orderflg\" name=\"orderflg\" \r\n");

	if (questioninfo.orderflg==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" value=\"1\" type=\"checkbox\">答案区分顺序\r\n");
	ViewBuilder.Append("            </span>\r\n");
	ViewBuilder.Append("            <textarea id=\"answer4\" name=\"answer4\" style=\"width:100%;height:50px\" cols=\"20\" rows=\"2\">" + questioninfo.answer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table id=\"tr_answer5\" style=\"width:100%;display:none\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 题目答案： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <textarea id=\"answer5\" name=\"answer5\" style=\"width:100%;height:50px\" cols=\"20\" rows=\"2\">" + questioninfo.answer.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table id=\"tr_answerkey\" style=\"width:100%;display:none\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 答案关键词： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <span style=\"color:Red\">\r\n");
	ViewBuilder.Append("            问答题将根据设置的关键词进行评分,多个关键字之间用英文逗号(,)隔开。\r\n");
	ViewBuilder.Append("            </span>\r\n");
	ViewBuilder.Append("            <textarea id=\"answerkey\" name=\"answerkey\" style=\"width:100%;height:50px\" cols=\"20\" rows=\"2\">" + questioninfo.answerkey.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 答案解析： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("        <textarea id=\"explain\" name=\"explain\" style=\"width:100%;height:80px\" cols=\"20\" rows=\"2\">" + questioninfo.explain.ToString().Trim() + "</textarea>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 题目难度： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <select id=\"difficulty\" name=\"difficulty\" style=\"width:200px;\">\r\n");
	ViewBuilder.Append("			    <option value=\"0\" \r\n");

	if (questioninfo.difficulty==0)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">易</option>\r\n");
	ViewBuilder.Append("			    <option value=\"1\" \r\n");

	if (questioninfo.difficulty==1)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">较易</option>\r\n");
	ViewBuilder.Append("			    <option value=\"2\" \r\n");

	if (questioninfo.difficulty==2)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">一般</option>\r\n");
	ViewBuilder.Append("			    <option value=\"3\" \r\n");

	if (questioninfo.difficulty==3)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">较难</option>\r\n");
	ViewBuilder.Append("			    <option value=\"4\" \r\n");

	if (questioninfo.difficulty==4)
	{

	ViewBuilder.Append(" selected=\"selected\" \r\n");

	}	//end if

	ViewBuilder.Append(">难</option>\r\n");
	ViewBuilder.Append("			</select>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 随机题目： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <label for=\"status\"><input id=\"status\" name=\"status\" value=\"1\" \r\n");

	if (questioninfo.status==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">是/否允许组卷时随机抽取该题目</label>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"> 清除代码： </td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("            <label for=\"isclear\"><input id=\"isclear\" name=\"isclear\" value=\"1\" \r\n");

	if (questioninfo.isclear==1)
	{

	ViewBuilder.Append(" checked=\"checked\" \r\n");

	}	//end if

	ViewBuilder.Append(" type=\"checkbox\">是/否提交时清除Word多余的代码，用在从Word或网页拷贝题目时</label>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        <table style=\"width:100%;\" cellpadding=\"0\" cellspacing=\"0\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tr>\r\n");
	ViewBuilder.Append("        <td class=\"td_class\" style=\"width:80px\"></td>\r\n");
	ViewBuilder.Append("        <td>\r\n");
	ViewBuilder.Append("        <input type=\"button\" name=\"btnsaveback\" value=\"保存返回\" id=\"btnsaveback\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("        <input type=\"button\" name=\"btnsavecontinue\" value=\"保存继续\" id=\"btnsavecontinue\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("        <input type=\"button\" name=\"btnback\" value=\"返回\" id=\"btnback\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("        </tr>\r\n");
	ViewBuilder.Append("       </table>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</form>\r\n");


	if (ispost)
	{

	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    layer.msg('" + msg.ToString() + "', 0, 1);\r\n");
	ViewBuilder.Append("    var bar = 0;\r\n");
	ViewBuilder.Append("    count();\r\n");
	ViewBuilder.Append("    function count() {\r\n");
	ViewBuilder.Append("        bar = bar + 4;\r\n");
	ViewBuilder.Append("        if (bar < 20) {\r\n");
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
