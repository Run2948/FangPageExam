<%controller(FangPage.Exam.Controller._examresult) %>
<%using(System) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>成绩单打印</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <%plugin(jquery)%>
    <style type="text/css">
        <!--
        /* Font Definitions */
        @font-face {
            font-family: 宋体;
            panose-1: 2 1 6 0 3 1 1 1 1 1;
        }

        @font-face {
            font-family: Calibri;
            panose-1: 2 15 5 2 2 2 4 3 2 4;
        }

        @font-face {
            font-family: "\@宋体";
            panose-1: 2 1 6 0 3 1 1 1 1 1;
        }
        /* Style Definitions */
        p.MsoNormal, li.MsoNormal, div.MsoNormal {
            margin: 0cm;
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            line-height: normal;
            font-size: 10.5pt;
            font-family: Calibri;
        }
        /* Page Definitions */
        @page Section1 {
            size: 595.3pt 841.9pt;
            margin: 72.0pt 90.0pt 72.0pt 90.0pt;
            layout-grid: 15.6pt;
        }

        div.Section1 {
            page: Section1;
        }
        -->
        .print {
            width: 680px;
            margin: 10px auto;
            overflow: hidden;
        }
        .auto-style1 {
            height: 1.0cm;
            width: 110pt;
        }
        .auto-style2 {
            height: 1.0cm;
            width: 93pt;
        }
        .auto-style3 {
            height: 1.0cm;
            width: 117pt;
        }
        .auto-style4 {
            height: 1.0cm;
            width: 104pt;
        }
        .page
        {
            page-break-after: always;
        }
        .noprint{
            visibility:hidden
        }  
    </style>
    <script type="text/javascript">
        function printWebPageByHideName(hideName) //打印函数  
        {
            if (!document.getElementsByName(hideName)) {
                alert("打印失败");
                return;
            }

            var hideNum = document.getElementsByName(hideName).length;
            //alert(hideNum);  
            for (i = 0; i < hideNum; i++) {
                document.getElementsByName(hideName)[i].style.display = "none";//打印时隐藏  
            }
            window.print();//打印  

            for (i = 0; i < hideNum; i++) {
                document.getElementsByName(hideName)[i].style.display = "";//打印后再显示出来  
            }

        }
    </script>
</head>
<body style="" >
    <div class="print">
         <input name="button2" type="button" onclick="printWebPageByHideName('btn');" value="打印成绩单" /> 
        <br />
        <br />
        <p class="MsoNormal" align="center" style="text-align: center">
            <b><span style="font-size: 20pt; font-family: 宋体">${examresult.examname}</span></b>
        </p>
        <br />
        <br />
        <p class="MsoNormal" align="center" style="text-align: center">
            <b><span style="font-size: 20pt; font-family: 宋体">成绩单</span></b>
        </p>
        <br />
        <br />
        <br />
        <div align="center">
            <table class="MsoNormalTable" border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse; border: none;width:620px;">
                <tbody>
                    <tr style="height: 1.0cm">
                        <td style="border: solid windowtext 1.0pt;width:150px;text-align:right" class="auto-style1">
                            <span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">所在单位：</span>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;">
                            <p class="MsoNormal" align="center" style="text-align: left"><span lang="EN-US" style="font-size: 10.5pt">${examresult.IUser.Department.name}</span></p>
                        </td>
                    </tr>
                    <tr style="height: 1.0cm">
                        <td style="border: solid windowtext 1.0pt;width:150px;text-align:right" class="auto-style1">
                            <span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">考生姓名：</span>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;">
                            <p class="MsoNormal" align="center" style="text-align: left"><span lang="EN-US" style="font-size: 10.5pt">
                                <%if {examresult.IUser.realname}!="" %>
                                ${examresult.IUser.realname}
                                <%else %>
                                ${examresult.IUser.username}
                                <%/if %>
                             </span></p>
                        </td>
                    </tr>
                    <tr style="height: 1.0cm">
                        <td style="border: solid windowtext 1.0pt;width:150px;text-align:right" class="auto-style1">
                            <span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">考试得分：</span>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;">
                            <p class="MsoNormal" align="center" style="text-align: left"><span lang="EN-US" style="font-size: 10.5pt">${(examresult.score*1.0)}分</span></p>
                        </td>
                    </tr>
                    <tr style="height: 1.0cm">
                        <td style="border: solid windowtext 1.0pt;width:150px;text-align:right" class="auto-style1">
                            <span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">答卷耗时：</span>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;">
                            <p class="MsoNormal" align="center" style="text-align: left"><span lang="EN-US" style="font-size: 10.5pt">${(examresult.utime/60+1)}分钟</span></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <br />
            <table class="MsoNormalTable" border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse; border: none;width:620px;">
                <tbody>
                    <tr style="height: 1.0cm">
                        <td style="border: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; " class="auto-style1">
                            <p class="MsoNormal" align="center" style="text-align: center"><span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">试卷大题</span></p>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;" class="auto-style2">
                            <p class="MsoNormal" align="center" style="text-align: center"><span lang="EN-US" style="font-size: 10.5pt;font-weight:bold;">错题数/总题数</span></p>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;" class="auto-style3">
                            <p class="MsoNormal" align="center" style="text-align: center"><span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">大题总分</span></p>
                        </td>
                        <td style="border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium;" class="auto-style4">
                            <p class="MsoNormal" align="center" style="text-align: center"><span style="font-size: 10.5pt; font-family: 宋体;font-weight:bold;">我的得分</span></p>
                        </td>
                    </tr>
                    <%loop (ExamResultTopic) examtopic examtopiclist %>
                    <tr style="height: 1.0cm">
                        <td style="border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding: 0cm 5.4pt; border-top-style: none; border-top-color: inherit; border-top-width: medium;" class="auto-style1">
                            <p class="MsoNormal" align="center" style="text-align: center"><span style="font-size: 10.5pt; font-family: 宋体">${examtopic.title}</span></p>
                        </td>
                        <td style="text-align:center;border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;" class="auto-style2">
                            <p class="MsoNormal" style="text-align: center"><span lang="EN-US" style="font-size: 10.5pt">${examtopic.wrongs}/${examtopic.questions}</span></p>
                        </td>
                        <td style="text-align:center;border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;" class="auto-style3">
                            <p class="MsoNormal" align="center" style="text-align: center"><span style="font-size: 10.5pt; font-family: 宋体">${Math.Round(examtopic.perscore*examtopic.questions,1)}</span></p>
                        </td>
                        <td style="text-align:center;border-bottom: solid windowtext 1.0pt; border-right: solid windowtext 1.0pt; padding: 0cm 5.4pt 0cm 5.4pt; border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;" class="auto-style4">
                            <p class="MsoNormal" style="text-align: center"><span lang="EN-US" style="font-size: 10.5pt">${examtopic.score}</span></p>
                        </td>
                    </tr>
                    <%/loop %>
                </tbody>
            </table>
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="font-size: 16pt; font-family: 宋体;height:100px;text-align:right;padding-right:300px;"
                            align="left">考生签名：</td>
                    </tr>
                </tbody>
            </table>
            <table width="100%">
                <tbody>
                    <tr>
                        <td style="font-size: 16pt; font-family: 宋体;height:30px;text-align:right;padding-right:150px;">考试日期：${fmdate(examresult.endtime,"yyyy年MM月dd日")}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</body>
</html>
