<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller._examresult" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FP_Exam" %>
<%@ Import namespace="FP_Exam.Model" %>

<%@ Import namespace="System" %>
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
	ViewBuilder.Append("    <title>成绩单打�?/title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=EmulateIE7\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("    <style type=\"text/css\">\r\n");
	ViewBuilder.Append("        <!--\r\n");
	ViewBuilder.Append("        /* Font Definitions */\r\n");
	ViewBuilder.Append("        @font-face {\r\n");
	ViewBuilder.Append("            font-family: 宋体;\r\n");
	ViewBuilder.Append("            panose-1: 2 1 6 0 3 1 1 1 1 1;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        @font-face {\r\n");
	ViewBuilder.Append("            font-family: Calibri;\r\n");
	ViewBuilder.Append("            panose-1: 2 15 5 2 2 2 4 3 2 4;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        @font-face {\r\n");
	ViewBuilder.Append("            font-family: \"\\@宋体\";\r\n");
	ViewBuilder.Append("            panose-1: 2 1 6 0 3 1 1 1 1 1;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        /* Style Definitions */\r\n");
	ViewBuilder.Append("        p.MsoNormal, li.MsoNormal, div.MsoNormal {\r\n");
	ViewBuilder.Append("            margin: 0cm;\r\n");
	ViewBuilder.Append("            margin-bottom: .0001pt;\r\n");
	ViewBuilder.Append("            text-align: justify;\r\n");
	ViewBuilder.Append("            text-justify: inter-ideograph;\r\n");
	ViewBuilder.Append("            line-height: normal;\r\n");
	ViewBuilder.Append("            font-size: 10.5pt;\r\n");
	ViewBuilder.Append("            font-family: Calibri;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        /* Page Definitions */\r\n");
	ViewBuilder.Append("        @page Section1 {\r\n");
	ViewBuilder.Append("            size: 595.3pt 841.9pt;\r\n");
	ViewBuilder.Append("            margin: 72.0pt 90.0pt 72.0pt 90.0pt;\r\n");
	ViewBuilder.Append("            layout-grid: 15.6pt;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        div.Section1 {\r\n");
	ViewBuilder.Append("            page: Section1;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        -->\r\n");
	ViewBuilder.Append("        .print {\r\n");
	ViewBuilder.Append("            width: 680px;\r\n");
	ViewBuilder.Append("            margin: 10px auto;\r\n");
	ViewBuilder.Append("            overflow: hidden;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        .auto-style1 {\r\n");
	ViewBuilder.Append("            height: 1.0cm;\r\n");
	ViewBuilder.Append("            width: 110pt;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        .auto-style2 {\r\n");
	ViewBuilder.Append("            height: 1.0cm;\r\n");
	ViewBuilder.Append("            width: 93pt;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        .auto-style3 {\r\n");
	ViewBuilder.Append("            height: 1.0cm;\r\n");
	ViewBuilder.Append("            width: 117pt;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        .auto-style4 {\r\n");
	ViewBuilder.Append("            height: 1.0cm;\r\n");
	ViewBuilder.Append("            width: 104pt;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        .page\r\n");
	ViewBuilder.Append("        {\r\n");
	ViewBuilder.Append("            page-break-after: always;\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("        .noprint{\r\n");
	ViewBuilder.Append("            visibility:hidden\r\n");
	ViewBuilder.Append("        }  \r\n");
	ViewBuilder.Append("    </style>\r\n");
	ViewBuilder.Append("    <script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("        function printWebPageByHideName(hideName) //打印函数  \r\n");
	ViewBuilder.Append("        {\r\n");
	ViewBuilder.Append("            if (!document.getElementsByName(hideName)) {\r\n");
	ViewBuilder.Append("                alert(\"打印失败\");\r\n");
	ViewBuilder.Append("                return;\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            var hideNum = document.getElementsByName(hideName).length;\r\n");
	ViewBuilder.Append("            //alert(hideNum);  \r\n");
	ViewBuilder.Append("            for (i = 0; i < hideNum; i++) {\r\n");
	ViewBuilder.Append("                document.getElementsByName(hideName)[i].style.display = \"none\";//打印时隐�? \r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            window.print();//打印  \r\n");
	ViewBuilder.Append("            for (i = 0; i < hideNum; i++) {\r\n");
	ViewBuilder.Append("                document.getElementsByName(hideName)[i].style.display = \"\";//打印后再显示出来  \r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }\r\n");
	ViewBuilder.Append("    </");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body style=\"\">\r\n");
	ViewBuilder.Append("    <div class=\"print\">\r\n");
	ViewBuilder.Append("         <input name=\"button2\" type=\"button\" onclick=\"printWebPageByHideName('btn');\" value=\"打印成绩单\"> \r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\">\r\n");
	ViewBuilder.Append("            <b><span style=\"font-size: 20pt; font-family: 宋体\">" + examresult.examname.ToString().Trim() + "</span></b>\r\n");
	ViewBuilder.Append("        </p>\r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\">\r\n");
	ViewBuilder.Append("            <b><span style=\"font-size: 20pt; font-family: 宋体\">成绩�?/span></b>\r\n");
	ViewBuilder.Append("        </p>\r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <br>\r\n");
	ViewBuilder.Append("        <div align=\"center\">\r\n");
	ViewBuilder.Append("            <table class=\"MsoNormalTable\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse; border: none;width:620px;\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr style=\"height: 1.0cm\">\r\n");
	ViewBuilder.Append("                        <td style=\"border: solid windowtext 1.0pt;width:150px;text-align:right\" class=\"auto-style1\">\r\n");
	ViewBuilder.Append("                            <span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">所在单位：</span>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: left\"><span lang=\"EN-US\" style=\"font-size: 10.5pt\">" + examresult.IUser.Department.name.ToString().Trim() + "</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr style=\"height: 1.0cm\">\r\n");
	ViewBuilder.Append("                        <td style=\"border: solid windowtext 1.0pt;width:150px;text-align:right\" class=\"auto-style1\">\r\n");
	ViewBuilder.Append("                            <span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">考生姓名�?/span>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: left\"><span lang=\"EN-US\" style=\"font-size: 10.5pt\">\r\n");

	if (examresult.IUser.realname!="")
	{

	ViewBuilder.Append("                                " + examresult.IUser.realname.ToString().Trim() + "\r\n");

	}
	else
	{

	ViewBuilder.Append("                                " + examresult.IUser.username.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("                             </span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr style=\"height: 1.0cm\">\r\n");
	ViewBuilder.Append("                        <td style=\"border: solid windowtext 1.0pt;width:150px;text-align:right\" class=\"auto-style1\">\r\n");
	ViewBuilder.Append("                            <span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">考试得分�?/span>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: left\"><span lang=\"EN-US\" style=\"font-size: 10.5pt\">" + (examresult.score*1.0).ToString().Trim() + "�?/span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                    <tr style=\"height: 1.0cm\">\r\n");
	ViewBuilder.Append("                        <td style=\"border: solid windowtext 1.0pt;width:150px;text-align:right\" class=\"auto-style1\">\r\n");
	ViewBuilder.Append("                            <span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">答卷耗时�?/span>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: left\"><span lang=\"EN-US\" style=\"font-size: 10.5pt\">" + (examresult.utime/60+1).ToString().Trim() + "分钟</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("            <br>\r\n");
	ViewBuilder.Append("            <br>\r\n");
	ViewBuilder.Append("            <table class=\"MsoNormalTable\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse; border: none;width:620px;\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr style=\"height: 1.0cm\">\r\n");
	ViewBuilder.Append("                        <td style=\"border: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; \" class=\"auto-style1\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\"><span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">试卷大题</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\" class=\"auto-style2\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\"><span lang=\"EN-US\" style=\"font-size: 10.5pt;font-weight:bold;\">错题�?总题�?/span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\" class=\"auto-style3\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\"><span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">大题总分</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;\" class=\"auto-style4\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\"><span style=\"font-size: 10.5pt; font-family: 宋体;font-weight:bold;\">我的得分</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");

	loop__id=0;
	foreach(ExamResultTopic examtopic in examtopiclist)
	{
	loop__id++;

	ViewBuilder.Append("                    <tr style=\"height: 1.0cm\">\r\n");
	ViewBuilder.Append("                        <td style=\"border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-top-style: none; border-top-color: inherit; border-top-width: medium;\" class=\"auto-style1\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\"><span style=\"font-size: 10.5pt; font-family: 宋体\">" + examtopic.title.ToString().Trim() + "</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"text-align:center;border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;\" class=\"auto-style2\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" style=\"text-align: center\"><span lang=\"EN-US\" style=\"font-size: 10.5pt\">" + examtopic.wrongs.ToString().Trim() + "/" + examtopic.questions.ToString().Trim() + "</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"text-align:center;border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;\" class=\"auto-style3\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" align=\"center\" style=\"text-align: center\"><span style=\"font-size: 10.5pt; font-family: 宋体\">" + Math.Round(examtopic.perscore*examtopic.questions,1).ToString().Trim() + "</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                        <td style=\"text-align:center;border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;\" class=\"auto-style4\">\r\n");
	ViewBuilder.Append("                            <p class=\"MsoNormal\" style=\"text-align: center\"><span lang=\"EN-US\" style=\"font-size: 10.5pt\">" + examtopic.score.ToString().Trim() + "</span></p>\r\n");
	ViewBuilder.Append("                        </td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("            <table width=\"100%\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td style=\"font-size: 16pt; font-family: 宋体;height:100px;text-align:right;padding-right:300px;\" align=\"left\">考生签名�?/td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("            <table width=\"100%\">\r\n");
	ViewBuilder.Append("                <tbody>\r\n");
	ViewBuilder.Append("                    <tr>\r\n");
	ViewBuilder.Append("                        <td style=\"font-size: 16pt; font-family: 宋体;height:30px;text-align:right;padding-right:150px;\">考试日期�? + FangPage.MVC.FPUtils.GetDate(examresult.endtime,"yyyy年MM月dd�?) + "</td>\r\n");
	ViewBuilder.Append("                    </tr>\r\n");
	ViewBuilder.Append("                </tbody>\r\n");
	ViewBuilder.Append("            </table>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
