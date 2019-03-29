<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>导出试卷</title>
    <link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
    <link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/tab.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <script type="text/javascript" src="../statics/js/jquery.form.js"></script>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        $(function () {
            var h = $(window).height() - 90;
            $("#table").height(h);
            var index = parent.layer.getFrameIndex(window.name);
            $("#btnclose").click(function () {
                parent.layer.close(index);
            });
            $("#btnuserok").click(function () {
                $("#frmpost").submit();
            })
        });
    </script>
</head>
<body>
    <form method="post" action="" name="frmpost" id="frmpost">
    <div id="table" class="newslistabout">
      <table cellpadding="0" cellspacing="0" border="0" style="width: 483px;height:135px; margin: 0px;">
        <tr>
            <td colspan="3" style="border: solid 1px #93C7D4; vertical-align:middle;height:100px;padding-left:10px;">
               <table colspan="3" style="height:40px;">
                   <tr>
                    <td style="width:70px;height:50px;">试卷纸张：</td>
                    <td>
                       <input id="papersize" name="papersize" value="a4" checked="checked" type="radio" />A4&nbsp;&nbsp;&nbsp;&nbsp;<input id="papersize" name="papersize" value="a3" type="radio" />A3
                    </td>
                </tr>
                <tr>
                    <td style="width:70px;">试卷类型：</td>
                    <td>
                       <input id="papertype" name="papertype" type="radio" checked="checked" value="0" />学生用卷（答案集中在卷尾）&nbsp;&nbsp;&nbsp;&nbsp;<input id="papertype" name="papertype" value="1" type="radio" />教师用卷（每题后面跟答案）<br />
                    </td>
                </tr>
               </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="25" align="right">
               <input type="button" name="btnuserok" value="下载" id="btnuserok" class="adminsubmit_short"/>
               <input type="button" name="btnclose" value="关闭" id="btnclose" class="adminsubmit_short"/>
            </td>
        </tr>
      </table>
    </div>
    </form>
</body>
</html>
