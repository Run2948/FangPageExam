<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller.testview" %>
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
	ViewBuilder.Append("<html>\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<title>随机练习 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<style type=\"text/css\">\r\n");
	ViewBuilder.Append("body {\r\n");
	ViewBuilder.Append("	PADDING: 0px;\r\n");
	ViewBuilder.Append("	MARGIN: 0px auto;\r\n");
	ViewBuilder.Append("	WORD-BREAK: break-all;\r\n");
	ViewBuilder.Append("	WORD-WRAP: break-word;\r\n");
	ViewBuilder.Append("	FONT-SIZE: 12px;\r\n");
	ViewBuilder.Append("	font-family: \"Times New Roman\", Times, serif;\r\n");
	ViewBuilder.Append("	BACKGROUND: #f1f1f1;\r\n");
	ViewBuilder.Append("	TEXT-ALIGN: center;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".head {\r\n");
	ViewBuilder.Append("	height: 60px;\r\n");
	ViewBuilder.Append("	background: url(" + webpath.ToString() + "sites/exam/statics/images/navbg.png);\r\n");
	ViewBuilder.Append("	text-align: center;\r\n");
	ViewBuilder.Append("	padding-top: 10px;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append("a.a:hover {\r\n");
	ViewBuilder.Append("color:#000;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append("#header {\r\n");
	ViewBuilder.Append("	MARGIN: 0px auto;\r\n");
	ViewBuilder.Append("	HEIGHT: 60px;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append("#center-middle {\r\n");
	ViewBuilder.Append("	width: 584px;\r\n");
	ViewBuilder.Append("	margin: 0px auto;\r\n");
	ViewBuilder.Append("	margin-top: -44px\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".cbodywrap {\r\n");
	ViewBuilder.Append("	width: 578px;\r\n");
	ViewBuilder.Append("	background: #fff6b7;\r\n");
	ViewBuilder.Append("	margin-left: 16px;\r\n");
	ViewBuilder.Append("	_margin: 25px 0px 0px 25px;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append("#cbody {\r\n");
	ViewBuilder.Append("	WIDTH: 515px;\r\n");
	ViewBuilder.Append("	text-align: left;\r\n");
	ViewBuilder.Append("	line-height: 28px;\r\n");
	ViewBuilder.Append("	padding: 15px 15px 37px 15px;\r\n");
	ViewBuilder.Append("	height: auto;\r\n");
	ViewBuilder.Append("	background: url(" + webpath.ToString() + "sites/exam/statics/images/exambg.png) no-repeat 0 100%;\r\n");
	ViewBuilder.Append("	margin-left: 16px;\r\n");
	ViewBuilder.Append("	margin-top: 30px;\r\n");
	ViewBuilder.Append("	_margin: 10px 0px 0px 4px;\r\n");
	ViewBuilder.Append("	position: relative\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".ptksbtn {\r\n");
	ViewBuilder.Append("	width: 87px;\r\n");
	ViewBuilder.Append("	height: 81px;\r\n");
	ViewBuilder.Append("	background: url(" + webpath.ToString() + "sites/exam/statics/images/examtype1.png) no-repeat;\r\n");
	ViewBuilder.Append("	position: absolute;\r\n");
	ViewBuilder.Append("	left: -7px;\r\n");
	ViewBuilder.Append("	top: -7px\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".userfacemain {\r\n");
	ViewBuilder.Append("	width: 101px;\r\n");
	ViewBuilder.Append("	height: 101px;\r\n");
	ViewBuilder.Append("	margin: 0 auto;\r\n");
	ViewBuilder.Append("	position: relative;\r\n");
	ViewBuilder.Append("	margin-top: 32px;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".userfacemain .userface {\r\n");
	ViewBuilder.Append("	width: 101px;\r\n");
	ViewBuilder.Append("	height: 101px;\r\n");
	ViewBuilder.Append("	background: url(" + webpath.ToString() + "sites/exam/statics/images/userface02.png) no-repeat;\r\n");
	ViewBuilder.Append("	position: absolute;\r\n");
	ViewBuilder.Append("	margin-left: 18px\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".userfacemain img {\r\n");
	ViewBuilder.Append("	width: 101px;\r\n");
	ViewBuilder.Append("	height: 101px;\r\n");
	ViewBuilder.Append("	margin-left: 18px;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".IE6jiance {\r\n");
	ViewBuilder.Append("	width: 650px;\r\n");
	ViewBuilder.Append("	margin: 0px auto;\r\n");
	ViewBuilder.Append("	margin-top: 25px;\r\n");
	ViewBuilder.Append("	height: 35px;\r\n");
	ViewBuilder.Append("	line-height: 35px;\r\n");
	ViewBuilder.Append("	color: #de1c1c;\r\n");
	ViewBuilder.Append("	font-size: 14px;\r\n");
	ViewBuilder.Append("	background: url(" + webpath.ToString() + "sites/exam/statics/images/gth.png) no-repeat 0 4px;\r\n");
	ViewBuilder.Append("	padding-left: 30px\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".bottombg {\r\n");
	ViewBuilder.Append("	width: 578px;\r\n");
	ViewBuilder.Append("	height: 45px;\r\n");
	ViewBuilder.Append("	background: url(" + webpath.ToString() + "sites/exam/statics/images/bottom.png) no-repeat;\r\n");
	ViewBuilder.Append("	margin-left: 16px;\r\n");
	ViewBuilder.Append("	margin-top: -6px\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".clear {\r\n");
	ViewBuilder.Append("	clear: both;\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append(".blank10 {\r\n");
	ViewBuilder.Append("	height: 10px;\r\n");
	ViewBuilder.Append("	overflow: hidden\r\n");
	ViewBuilder.Append("}\r\n");
	ViewBuilder.Append("</style>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body style=\"background:url(" + webpath.ToString() + "sites/exam/statics/images/examjt.png) no-repeat 50% 0;color:#777\">\r\n");
	ViewBuilder.Append("<form name=\"frmpost\" method=\"post\" action=\"test.aspx\" id=\"frmpost\" onsubmit=\"layer.msg('系统正在组卷，请稍后...', 0, 1);\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" id=\"testtype\" name=\"testtype\" value=\"0\">\r\n");
	ViewBuilder.Append("  <div class=\"userfacemain\">\r\n");
	ViewBuilder.Append("    <div class=\"userface\"></div>\r\n");
	ViewBuilder.Append("    <img src=\"" + webpath.ToString() + "sites/exam/statics/images/noavatar_small.gif\" onerror=\"this.src='statics/images/noavatar_small.gif';\"></div>\r\n");
	ViewBuilder.Append("  <div id=\"container\">\r\n");
	ViewBuilder.Append("    <div id=\"header\"></div>\r\n");
	ViewBuilder.Append("    <div id=\"center-header\"></div>\r\n");
	ViewBuilder.Append("    <div id=\"center-middle\">\r\n");
	ViewBuilder.Append("      <div class=\"cbodywrap\">\r\n");
	ViewBuilder.Append("        <div id=\"cbody\">\r\n");
	ViewBuilder.Append("          <div class=\"ptksbtn\"></div>\r\n");
	ViewBuilder.Append("          <table width=\"100%\" height=\"163\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\">\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("              <td height=\"50\" colspan=\"2\" align=\"center\" style=\"background:#fffbe1\"><span style=\"font-size:17px;text-align: center;color:#444;font-weight:bold; font-family:'微软雅黑'\">随机练习</span></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("              <td height=\"30\" align=\"center\" colspan=\"2\" style=\"background:#fff url(" + webpath.ToString() + "sites/exam/statics/images/trbg.png) no-repeat 50% 100%;\">\r\n");
	ViewBuilder.Append("                练习用户：" + username.ToString() + "\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("              <td width=\"120\" height=\"30\" align=\"center\" style=\"background:#fff url(" + webpath.ToString() + "sites/exam/statics/images/trbg.png) no-repeat 0 100%; padding-left:20px;\">练习题数：</td>\r\n");
	ViewBuilder.Append("              <td align=\"left\" style=\"background:#fff url(" + webpath.ToString() + "sites/exam/statics/images/trbg.png) no-repeat -270px 100%; padding-left:5px;border-left:1px solid #f4ecd2\">\r\n");
	ViewBuilder.Append("                  <input id=\"limit\" name=\"limit\" value=\"50\" type=\"text\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("              <td height=\"30\" align=\"center\" style=\" padding-left:20px;background:#fff url(" + webpath.ToString() + "sites/exam/statics/images/trbg.png) no-repeat 0 100%;\">练习题型：</td>\r\n");
	ViewBuilder.Append("              <td align=\"left\" style=\"padding-left:5px;background:#fff url(" + webpath.ToString() + "sites/exam/statics/images/trbg.png) no-repeat -270px 100%;border-left:1px solid #f4ecd2\">\r\n");

	if (ischecked(1,examconfig.questiontype))
	{

	ViewBuilder.Append("                <input id=\"type\" name=\"type\" checked=\"checked\" value=\"1\" type=\"checkbox\">单选题\r\n");

	}	//end if


	if (ischecked(2,examconfig.questiontype))
	{

	ViewBuilder.Append("                <input id=\"type\" name=\"type\" checked=\"checked\" value=\"2\" type=\"checkbox\">多选题\r\n");

	}	//end if


	if (ischecked(3,examconfig.questiontype))
	{

	ViewBuilder.Append("                <input id=\"type\" name=\"type\" checked=\"checked\" value=\"3\" type=\"checkbox\">判断题\r\n");

	}	//end if


	if (ischecked(4,examconfig.questiontype))
	{

	ViewBuilder.Append("                <input id=\"type\" name=\"type\" checked=\"checked\" value=\"4\" type=\"checkbox\">填空题\r\n");

	}	//end if


	if (ischecked(5,examconfig.questiontype))
	{

	ViewBuilder.Append("                <input id=\"type\" name=\"type\" checked=\"checked\" value=\"5\" type=\"checkbox\">问答题\r\n");

	}	//end if


	if (ischecked(6,examconfig.questiontype))
	{

	ViewBuilder.Append("                <input id=\"type\" name=\"type\" checked=\"checked\" value=\"6\" type=\"checkbox\">打字题\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("          </table>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        <div class=\"clear\"></div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("      <div class=\"bottombg\"></div>\r\n");
	ViewBuilder.Append("      <div id=\"center-bottom\"></div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("  <div style=\"text-align: center; line-height:30px;\">\r\n");
	ViewBuilder.Append("    <input type=\"image\" name=\"starttest\" id=\"starttest\" src=\"" + webpath.ToString() + "sites/exam/statics/images/btn2.png\" style=\"border-width:0px;\">\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
